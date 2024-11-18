using System;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsersAsync()
    {
        var users = await userRepository.GetUsersAsync();

        var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

        if (!usersToReturn.Any()) return NotFound("Not found.");

        return Ok(usersToReturn);
    }

    [HttpGet("{username}")] //api/users/string
    public async Task<ActionResult<MemberDto>> GetUserAsync(string username)
    {
        var user = await userRepository.GetUserByUsernameAsync(username); //use find instead of FirstOrDefault

        var userToReturn = mapper.Map<MemberDto>(user);

        if (userToReturn is null) return NotFound("Not found."); 

        return userToReturn; //return mapper.Map<MemberDto>(user);
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
