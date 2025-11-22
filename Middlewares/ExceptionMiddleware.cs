using Florin_API.DTOs.Exception;
using Florin_API.Exceptions.Auth;
using Florin_API.Exceptions.User;

namespace Florin_API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ExceptionDTO
        {
            Message = exception.Message,
            Type = exception.GetType().Name
        };

        context.Response.StatusCode = exception switch
        {
            BadCredentialsException => StatusCodes.Status401Unauthorized,
            UserAlreadyExistsException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
