using System;

namespace API.Errors;

public class ApiException(int statusCode, string message, string? details) //details is for stacktrace
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
