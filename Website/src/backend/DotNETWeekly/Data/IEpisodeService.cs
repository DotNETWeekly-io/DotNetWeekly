
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotNETWeekly.Data
{
    using Models;
    public interface IEpisodeService
    {
        Task<IEnumerable<EpisodeSummary>> GetEpisodeSummaries(CancellationToken token);

        Task AddEpsidoeSummary(EpisodeSummary episodeSummary, CancellationToken token);

        Task UpdateEpisodeSummary(string id, EpisodeSummary episodeSummary, CancellationToken token);

        Task<IEnumerable<Episode>> GetEpisodes(CancellationToken token);

        Task AddEpisode(Episode episode, CancellationToken token);

        Task<Episode> GetEpisode(string id, CancellationToken token);
        
        Task UpdateEpisode(string id, Episode episode, CancellationToken token);

        Task DeleteEpisodeSummary(string id, CancellationToken token);

        Task DeleteEpisode(string id, CancellationToken token);

    }
}
