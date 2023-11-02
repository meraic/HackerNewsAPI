using HackerNews.Api.Extensions;
using HackerNews.Services;

namespace HackerNews.Api.Modules;

public static class HackerNewsModule
{
    public static IServiceCollection AddHackerNewsApiClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        var hackerNewsApiBaseUrl = configuration["HackerNewsApi:BaseUrl"];

        if (hackerNewsApiBaseUrl == null)
        {
            throw new Exception("HackerNews BaseUrl is not configured in appSettings config");
        }

        services.AddScoped<IHackerNewsService, HackerNewsService>();

        services.ConfigureHttpClient<IHackerNewsService, HackerNewsService>(hackerNewsApiBaseUrl);

        return services;
    }
}

