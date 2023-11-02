using HackerNews.Models.Stories;
using LazyCache;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace HackerNews.Services;


public class HackerNewsService : IHackerNewsService
{
    private const int SOTRY_CACHE_EXPIRATION_MINUTES = 15;

    private readonly HttpClient _httpClient;
    private readonly ILogger<HackerNewsService> _logger;
    private readonly IAppCache _cache;

    private enum HackerNewsEndpointType
    {
        BestStoriesEndpoint,
        StoryByIdEndpoint
    }

    private const string BestStoriesEndpoint = "v0/beststories.json";
    private const string StoryByIdEndpoint = "v0/item";

    public HackerNewsService(HttpClient httpClient, ILogger<HackerNewsService> logger, IAppCache cache)
    {
        _httpClient = httpClient;
        _logger = logger;
        _cache = cache;
    }

    public async Task<Story> GetStoryById(int storyId)
    {
        return await _cache.GetOrAddAsync($"story-{storyId}", async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(SOTRY_CACHE_EXPIRATION_MINUTES);

            string url = $"{StoryByIdEndpoint}/{storyId}.json";
            var response = await _httpClient.GetAsync(new Uri(url, UriKind.Relative));

            if (!response.IsSuccessStatusCode)
            {
                await LogHackerNewApiError(response, HackerNewsEndpointType.StoryByIdEndpoint);
                throw new Exception("Something went wrong, error occurred");
            }

            return await response.Content.ReadFromJsonAsync<Story>();
        });
    }

    public async Task<IEnumerable<Story>> GetBestStories(int nStories)
    {
        var url = $"{BestStoriesEndpoint}?orderBy=\"$priority\"&limitToFirst={nStories}";

        try
        {
            var response = await _httpClient.GetAsync(new Uri(url, UriKind.Relative));

            if (!response.IsSuccessStatusCode)
            {
                await LogHackerNewApiError(response, HackerNewsEndpointType.BestStoriesEndpoint);
                throw new Exception("Something went wrong, error occurred");
            }

            var stories = new List<Story>();

            var bestStoryIds = await response.Content.ReadFromJsonAsync<IEnumerable<int>>();

            if (bestStoryIds == null || !bestStoryIds.Any())
            {
                return Enumerable.Empty<Story>();
            }

            var storyTasks = bestStoryIds.Distinct().Select(async storyId =>
            {
                stories.Add(await GetStoryById(storyId));
            });


            Task.WaitAll(storyTasks.ToArray());

            return stories.OrderByDescending(story => story.Score);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hacker News Best Stories endpoint returned unexpected error.");
            throw;
        }
    }

    private async Task LogHackerNewApiError(HttpResponseMessage response, HackerNewsEndpointType endpointType)
    {
        var error = await response.Content.ReadAsStringAsync();

        _logger.LogError($"{GetErrorSource(endpointType)} response status {response.StatusCode} and message {error}");
    }

    private string GetErrorSource(HackerNewsEndpointType endpointType) => endpointType switch
    {
        HackerNewsEndpointType.BestStoriesEndpoint => "Best Stories",
        _ => "Unknown"
    };
}