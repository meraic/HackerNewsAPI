using HackerNews.Api.Utils;

namespace HackerNews.Api.Middleware;

public class ResponseAppVersionMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseAppVersionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add(CustomHeaders.AppVersion, AppVersionUtils.GetAssemblyVersion());

        await _next(context);
    }
}

