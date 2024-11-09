using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext dataContext) : BaseApiController
{
    [AllowAnonymous] //overrides the authorize
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
    {
        var users = await dataContext.Users.ToListAsync();

        if (!users.Any()) return NotFound("Not found.");

        return users;
    }
    [Authorize]
    [HttpGet("{id:int}")] //api/users/3
    public async Task<ActionResult<AppUser>> GetUserAsync(int id)
    {
        var user = await dataContext.Users.FindAsync(id); //use find instead of FirstOrDefault

        if (user is null) return NotFound("Not found."); 

        return user;
    }
}

// Explanation:
// UsersController: This controller provides endpoints to retrieve user data from the database.

// GetUsersAsync Method:
// An HTTP GET request that returns all users from the database.
// Uses ToListAsync to asynchronously retrieve the list of users.
// If no users are found, returns a NotFound result with a message; otherwise, returns the list of users.

// GetUserAsync Method:
// An HTTP GET request to retrieve a specific user by their id.
// Uses FindAsync for an efficient lookup by primary key.
// If the user with the specified id does not exist, returns a NotFound response with a message; otherwise, returns the user.
