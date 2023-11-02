using HackerNews.Models.Common.OperationResult;

namespace HackerNews.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var error = new OperationError(
                FailReason.InternalServerError,
                "Error executing the request use correlationId provided to find the error in App Insights.");

            await context.Response.WriteAsJsonAsync(error);
        }
    }

}

