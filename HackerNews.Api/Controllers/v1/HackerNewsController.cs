using AutoMapper;
using HackerNews.Api.Mappers;
using HackerNews.Services;
using HasckerNews.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/hackernews")]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;
        private readonly ILogger<HackerNewsController> _logger;
        private readonly IMapper _mapper;

        public HackerNewsController(ILogger<HackerNewsController> logger, IHackerNewsService hackerNewsService, IMapper mapper)
        {
            _logger = logger;
            _hackerNewsService = hackerNewsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<StoryViewModel>> GetBestStories(int nStories)
        {
            var bestStories = await _hackerNewsService.GetBestStories(nStories);

            return bestStories.Select(story => story.ToViewModel(_mapper));
        }
    }
}