using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class AccountController(DataContext dataContext) : BaseApiController
{
    // private readonly DataContext _dataContext = dataContext;

    [HttpPost("register")] //account/register
    public async Task<ActionResult<AppUser>> Register([from]string username, string password)
    {
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };

        dataContext.Users.Add(user);

        await dataContext.SaveChangesAsync();

        return user;
    }
}

