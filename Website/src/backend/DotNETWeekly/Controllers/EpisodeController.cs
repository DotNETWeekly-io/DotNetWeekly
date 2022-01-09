using Microsoft.AspNetCore.Mvc;
using DotNETWeekly.Data;
using DotNETWeekly.Models;
using Microsoft.AspNetCore.Authorization;

namespace DotNETWeekly.Controllers
{
    [Route("api")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public EpisodeController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet("episodes")]
        public async Task<IEnumerable<Episode>> GetAllEpisodes()
        {
            return await _dataRepository.GetEpisodesAsync();
        }

        [HttpGet("episodes/{episodeId:int}")]
        public async Task<IActionResult> GetEpisode(int episodeId)
        {
            var episode =  await _dataRepository.GetEpisodeByIdAsync(episodeId);
            if (episode == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetEpisode), episode);
        }

        [HttpPost("episodes")]
        [Authorize]
        public async Task<int> CreateEpisode(EpisodeRequest episodeRequest)
        {
            var episode = new Episode
            {
                Title = episodeRequest.Title,
                CreateTime = DateTime.UtcNow,
                Introduction = episodeRequest.Introduction,
            };
            return await _dataRepository.CreateEpisode(episode);
        }

        [HttpPost("records")]
        [Authorize]
        public async Task<int> CreateRecord(RecordRequest recordRequest)
        {
            var record = new Record
            {
                Title = recordRequest.Title,
                Link = recordRequest.Link,
                Content = recordRequest.Content,
                EpisodeId = recordRequest.EpisodeId,
                Category = recordRequest.Category,
                CreateTime = DateTime.UtcNow,
            };
            return await _dataRepository.CreateRecord(record);
        }
    }
}
