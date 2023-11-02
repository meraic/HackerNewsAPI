using AutoMapper;
using HackerNews.Models.Stories;
using HasckerNews.Api.ViewModels;

namespace HackerNews.Api.Mappers;

public static class StoryMappingExtensions
{
    /// <summary>
    /// Maps a <see cref="Story"/> to <see cref="StoryViewModel"/>
    /// </summary>
    /// <param name="model"></param>
    /// <param name="mapper"></param>
    /// <returns>AgencyViewModel</returns>
    public static StoryViewModel ToViewModel(this Story model, IMapper mapper)
    {
        return mapper.Map<StoryViewModel>(model);
    }
}
