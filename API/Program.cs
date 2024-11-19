using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Create a service scope to access services like DataContext
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync(); // Applies any pending migrations to the database
    await Seed.SeedUsers(context);         // Seeds the database with initial user data if empty
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();

/*
Explanation of Key Components

1. ApplicationServices: Configures application-specific services, including the database connection and other dependencies for the app.

2. IdentityServices: Sets up services needed for authentication, specifically using JWT tokens for secure access.

3. ExceptionMiddleware: Custom middleware that globally handles exceptions, providing a consistent error response.

4. CORS Configuration: Allows HTTP requests from specific origins, enabling front-end applications (like Angular) hosted on localhost:4200 to communicate with the API.

5. Authentication and Authorization: 
   - `app.UseAuthentication()` enables JWT-based authentication.
   - `app.UseAuthorization()` enforces authorization policies for secure endpoints.

6. MapControllers: Maps controller routes, allowing API endpoints to be accessed.

7. Service Scope Creation:
   - `using var scope = app.Services.CreateScope();` creates a new scope to access scoped services.
   - Inside the scope, `DataContext` is retrieved and used to apply migrations and seed initial data.

8. Database Migration and Seeding:
   - `context.Database.MigrateAsync()` applies any pending migrations to ensure the database schema is up-to-date.
   - `Seed.SeedUsers(context)` seeds the database with initial data if there are no existing users.

9. Error Logging:
   - If an exception occurs during migration or seeding, it is logged using `ILogger<Program>`.
*/
