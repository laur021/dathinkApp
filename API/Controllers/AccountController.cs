using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// AccountController inherits from BaseApiController
// Uses DataContext dependency injection via constructor
public class AccountController(DataContext dataContext) : BaseApiController
{
    // Registers a new user
    [HttpPost("register")] // POST endpoint at account/register
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        // Checks if a user with the same username already exists
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        // Creates a new instance of HMACSHA512 for hashing the password
        using var hmac = new HMACSHA512();
        
        // Creates a new AppUser instance with the hashed password and salt
        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),  // Sets username to lowercase for consistency
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),  // Hashes password
            PasswordSalt = hmac.Key  // Stores the salt used to create the hash
        };

        // Adds the new user to the Users table in the database
        dataContext.Users.Add(user);
        
        // Saves the changes to the database asynchronously
        await dataContext.SaveChangesAsync();

        // Returns the created user
        return user;
    }
    
    // Authenticates a user with provided username and password
    [HttpPost("login")] // Default POST endpoint for login
    public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
    {
        // Finds the user by username (case-insensitive)
        var user = await dataContext.Users.FirstOrDefaultAsync(x =>
            x.UserName.ToLower() == loginDto.Username.ToLower());
        
        // If the user is not found, return Unauthorized with message
        if (user is null) return Unauthorized("Invalid username");

        // Initializes HMACSHA512 with the stored salt for the user
        using var hmac = new HMACSHA512(user.PasswordSalt);

        // Computes a hash for the input password using the user's stored salt
        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        // Compares each byte of the computed hash with the stored hash
        for (int i = 0; i < ComputeHash.Length; i++)
        {
            if (ComputeHash[i] != user.PasswordHash[i])  return Unauthorized("Invalid Password");  // Returns Unauthorized if any byte doesn't match
        }

        // If the password matches, returns the authenticated user
        return user;
    }

    // Checks if a user with the specified username exists in the database
    private async Task<bool> UserExists(String username)
    {
        // Uses a case-insensitive comparison to see if any user matches the provided username
        return await dataContext.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
