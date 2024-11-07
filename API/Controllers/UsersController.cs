using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] //api/users
public class UsersController(DataContext dataContext) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
    {
        var users = await _dataContext.Users.ToListAsync();

        if (!users.Any()) return NotFound("Not found.");

        return users;
    }
    [HttpGet("{id:int}")] //api/users/3
    public async Task<ActionResult<AppUser>> GetUserAsync(int id)
    {
        var user = await _dataContext.Users.FindAsync(id); //use find instead of FirstOrDefault

        if (user is null) return NotFound("Not found."); 

        return user;
    }
}
