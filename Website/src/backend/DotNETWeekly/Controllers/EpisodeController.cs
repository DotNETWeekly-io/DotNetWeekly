using DotNETWeekly.Data;
using DotNETWeekly.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNETWeekly.Controllers
{
    [Route("api")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        private readonly IMemoryCache _cache;

        public EpisodeController(IDataRepository dataRepository, IMemoryCache cache)
        {
            _dataRepository = dataRepository;
            _cache = cache;
        }

        [HttpGet("episodes")]
        [AllowAnonymous]
        public async Task<IEnumerable<Episode>> GetAllEpisodes()
        {
            return await _dataRepository.GetEpisodesAsync();
        }

        [HttpGet("episodes/{episodeId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEpisode(int episodeId)
        {
            Episode? episode;
            if (_cache.TryGetValue<Episode>(episodeId, out episode))
            {
                return Ok(episode);
            }
            episode =  await _dataRepository.GetEpisodeByIdAsync(episodeId);
            if (episode == null)
            {
                return NotFound();
            }
            _cache.Set(episodeId, episode);
            return CreatedAtAction(nameof(GetEpisode), episode);
        }

        [HttpPost("episodes")]
#if !DEBUG
        [Authorize]
#endif
        public async Task<int> CreateEpisode(EpisodeRequest episodeRequest)
        {
            var episode = new Episode
            {
                Title = episodeRequest.Title,
                Content = episodeRequest.Content,
            };
            return await _dataRepository.AddEpisodeAsync(episode);
        }

        [HttpPut("episodes")]
#if !DEBUG
        [Authorize]
#endif
        public async Task UpdateEpisode(Episode episodeRequest)
        {
            var episode = new Episode
            {
                Id = episodeRequest.Id,
                Title = episodeRequest.Title,
                Content = episodeRequest.Content,
            };
            await _dataRepository.UpdateEpisodeAsync(episode);
        }
    }
}
