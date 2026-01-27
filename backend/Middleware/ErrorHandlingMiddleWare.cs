using System.Net;
using System.Text.Json;
using KichBackendApp.Models.Exceptions;

namespace KichBackendApp.Middleware;

public class ErrorHandlingMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleWare> _logger;
    
    public ErrorHandlingMiddleWare(RequestDelegate next, ILogger<ErrorHandlingMiddleWare> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        object response;
        HttpStatusCode statusCode;

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    message = validationEx.Message,
                    errors = validationEx.Errors,
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow
                };
                _logger.LogWarning(validationEx, "Validation error: {Message}", validationEx.Message);
                break;

            case AuthenticationException authEx:
                statusCode = HttpStatusCode.Unauthorized;
                response = new
                {
                    message = authEx.Message,
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow
                };
                _logger.LogWarning(authEx, "Authentication error: {Message}", authEx.Message);
                break;

            case NotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                response = new
                {
                    message = notFoundEx.Message,
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow
                };
                _logger.LogWarning(notFoundEx, "Resource not found: {Message}", notFoundEx.Message);
                break;
            
            case UnauthorizedException unauthorizedEx:
                statusCode = HttpStatusCode.Unauthorized;
                response = new
                {
                    message = unauthorizedEx.Message,
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow
                };
                _logger.LogWarning(unauthorizedEx, "No authorization: {Message}", unauthorizedEx.Message);
                break;

            case ArgumentException argEx:
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    message = argEx.Message,
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow
                };
                _logger.LogWarning(argEx, "Invalid argument: {Message}", argEx.Message);
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                response = new
                {
                    message = "An unexpected error occurred",
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow
                };
                _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}