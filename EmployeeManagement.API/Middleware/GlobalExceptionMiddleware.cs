using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Middleware;

public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try { await next(context); }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception for {Path}", context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ProblemDetails { Status = 500, Title = "An unexpected error occurred.", Detail = "Please contact support if the problem persists." });
        }
    }
}
