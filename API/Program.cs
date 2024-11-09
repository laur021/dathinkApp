using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Explanation of Key Components

// ApplicationServices: Connection, Services etc.

// IdentityServices: Authentication

// CORS Configuration: Allows HTTP requests from specific origins, enabling front-end applications (like Angular) hosted on localhost:4200 to communicate with the API.

// Dependency Injection: Registers ITokenService with TokenService for generating JWT tokens.

// JWT Authentication Setup: Configures JWT authentication with a secret key (retrieved from configuration) for token validation, allowing secure access to protected endpoints.

// Middleware Setup: Configures CORS, authentication
