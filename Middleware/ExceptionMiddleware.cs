using Florin_API.Exceptions;
using System.Net;
using System.Text.Json;

namespace Florin_API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            message = exception.Message,
            type = exception.GetType().Name
        };

        switch (exception)
        {
            case BadCredentialsException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response = new { message = "Invalid credentials", type = nameof(BadCredentialsException) };
                break;
            case EmailAlreadyExistsException:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                response = new { message = "Email already exists", type = nameof(EmailAlreadyExistsException) };
                break;
            case UserNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new { message = exception.Message, type = nameof(UserNotFoundException) };
                break;
            case CategoryNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new { message = exception.Message, type = nameof(CategoryNotFoundException) };
                break;
            case DuplicateCategoryNameException:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                response = new { message = exception.Message, type = nameof(DuplicateCategoryNameException) };
                break;
            case TransactionNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new { message = exception.Message, type = nameof(TransactionNotFoundException) };
                break;
            case InvalidRefreshTokenException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response = new { message = "Invalid refresh token", type = nameof(InvalidRefreshTokenException) };
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new { message = "An error occurred while processing your request", type = exception.GetType().Name };
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}
