using System;
using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() 
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace) 
                : new ApiException(context.Response.StatusCode, ex.Message, "internal server error");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}

// This ExceptionMiddleware class handles unhandled exceptions during HTTP request processing.
// It tries to process the request with the next middleware in the pipeline, and if an exception occurs, it catches it.
// The exception is logged with an error level using the provided logger.
// The response is set to JSON format with a status code of 500 (Internal Server Error).
// Based on the environment, the error response includes the full stack trace (development) or a generic error message (production).
// The response is serialized to JSON with camel case formatting and written to the HTTP context as the response.
