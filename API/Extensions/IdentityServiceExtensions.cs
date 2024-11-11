using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }
}

/*
Summary of the added services:
1. Authentication - Configures JWT Bearer authentication, specifying JwtBearerDefaults as the default scheme.
2. Token Validation Parameters:
   - Validates the signing key to ensure token authenticity.
   - Configures the signing key using a symmetric security key derived from a secret token key in configuration.
   - Disables issuer and audience validation, which may be configured depending on the security requirements.
*/
