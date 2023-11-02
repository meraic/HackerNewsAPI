using FluentAssertions;
using HackerNews.Models.Stories;
using HackerNews.Services;
using LazyCache;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace HackerNews.Api.Tests;

public class HackerNewsServiceTests
{
    private const string BaseUrl = @"https://hacker-news.firebaseio.com/";
    private HttpClient _httpClient;
    private ILogger<HackerNewsService> _logger;
    private IAppCache _cache;
    private IHackerNewsService _hackerNewsService;

    [SetUp]
    public void Setup()
    {
        _httpClient = Substitute.For<HttpClient>();
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _logger = Substitute.For<ILogger<HackerNewsService>>();
        _cache = new CachingService();

        _hackerNewsService = new HackerNewsService(
            _httpClient,
            _logger,
            _cache);
    }

    [Test]
    public async Task GetStoryById_Returns_Story_From_HackerNews_Api()
    {
        int storyId = 38087573;
        var story = await _hackerNewsService.GetStoryById(storyId);

        story.Should().NotBeNull();

        story.Should().BeOfType<Story>();

        story.Id.Should().Be(storyId);

        story.Title.Should().Be("Firefox got faster for real users in 2023");

        story.Url.Should().Be(@"https://hacks.mozilla.org/2023/10/down-and-to-the-right-firefox-got-faster-for-real-users-in-2023/");
    }

    [Test]
    public async Task GetBestStories_Returns_BestStories_From_HackerNews_Api()
    {
        int nStories = 50;
        var bestStories = await _hackerNewsService.GetBestStories(nStories);

        bestStories.Should().NotBeNull();
        bestStories.Count().Should().Be(50);
    }
}
