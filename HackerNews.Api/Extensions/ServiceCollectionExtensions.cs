using Polly.Timeout;
using Polly;
using System.Net.Sockets;
using Serilog;

namespace HackerNews.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureHttpClient<TInterface, TImplementation>(this IServiceCollection services,
        string baseUrl,
        int timeoutSeconds = 45)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var retryPolicy = Policy
            .Handle<TimeoutRejectedException>()
            .Or<HttpRequestException>()
            .Or<SocketException>()
            .OrResult<HttpResponseMessage>(m => !m.IsSuccessStatusCode)
            .RetryAsync(onRetry: (result, _) => Log.Warning(
                $"Retry Request. " +
                $"URI: ${result?.Result?.RequestMessage?.RequestUri}; " +
                $"StatusCode: {result?.Result?.StatusCode}; " +
                $"ExceptionMessage: {result?.Exception?.Message};"));

        var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(timeoutSeconds));
        var policy = retryPolicy.WrapAsync(timeoutPolicy);

        services
            .AddHttpClient<TInterface, TImplementation>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                // Can set this one to max so it won't mess with policy timeout mechanism
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
            })
            .AddPolicyHandler(policy);
    }
}

