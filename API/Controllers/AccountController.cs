using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext dataContext, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        return Ok();
        // using var hmac = new HMACSHA512();
        
        // var user = new AppUser
        // {
        //     UserName = registerDto.Username.ToLower(),
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //     PasswordSalt = hmac.Key
        // };

        // dataContext.Users.Add(user);
        
        // await dataContext.SaveChangesAsync();

        // return new UserDto
        // {
        //     Username = user.UserName,
        //     Token = tokenService.CreateToken(user)
        // };
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(x =>
            x.UserName.ToLower() == loginDto.Username.ToLower());
        
        if (user is null) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < ComputeHash.Length; i++)
        {
            if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(String username)
    {
        return await dataContext.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}

/*
Explanation:
AccountController: This controller manages user account registration and login.

Register Method:
- Receives a RegisterDto with the user's chosen username and password.
- Checks if the username already exists in the database.
- Uses HMACSHA512 to hash the user's password and generates a password salt.
- Creates a new AppUser instance with the username, hashed password, and salt.
- Adds the user to the database and saves the changes.

Login Method:
- Receives a LoginDto containing the username and password.
- Searches the database for a user with the provided username.
- If no user is found, returns an Unauthorized response.
- If found, uses the stored salt to hash the provided password and compares it byte-by-byte with the stored hash.
- Returns an Unauthorized response if the hashes do not match; otherwise, returns the authenticated user.

UserExists Helper Method:
- Checks the database to see if any user already has the specified username (case-insensitive).
*/
