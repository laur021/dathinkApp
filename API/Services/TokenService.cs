using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");

        if (tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}


// Explanation of Key Components:
// Token Key Retrieval and Validation:

// Retrieves the TokenKey from configuration and ensures it is at least 64 characters long for security.
// Claims Creation:

// Sets up claims that will be embedded in the token, allowing access to user data (e.g., user.UserName).
// Token Signing:

// Uses HMACSHA512 for signing the token, ensuring token integrity and authenticity with the symmetric key.
// Token Descriptor:

// Specifies the token's properties, including the claims, expiration (7 days), and signing credentials.
// Token Creation and Return:

// Generates the token using JwtSecurityTokenHandler and returns it as a string, ready to be sent to the user.
