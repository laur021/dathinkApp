using System;
using API.Data;
using API.Data.Repositories;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;


namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) //"this" mean extension of method
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(option =>
        {
            option.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

/*
Summary of the added services:
1. Controllers - Registers controllers in the API to handle HTTP requests.
2. DbContext - Configures the DbContext for the application's database interactions, here using SQLite.
3. CORS - Enables CORS to allow or restrict cross-origin requests.
4. Token Service - Adds the TokenService with a scoped lifetime, meaning a new instance will be provided per HTTP request, which is optimal for data access services.
*/