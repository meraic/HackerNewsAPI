using HackerNews.Models.Stories;

namespace HackerNews.Services;

public interface IHackerNewsService
{
    Task<IEnumerable<Story>> GetBestStories(int nStories);

    Task<Story> GetStoryById(int id);
}
