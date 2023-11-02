using AutoMapper;
using HackerNews.Models.Stories;
using HasckerNews.Api.ViewModels;

namespace HackerNews.Api.Mappers;

public class StoryProfile : Profile
{
    public StoryProfile()
    {
        CreateMap<Story, StoryViewModel>()
            .ForMember(dest => dest.PostedBy, opt => opt.MapFrom(src => src.By))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Descendants));
    }
}
