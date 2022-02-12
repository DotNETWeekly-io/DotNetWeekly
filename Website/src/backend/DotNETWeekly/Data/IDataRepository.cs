namespace DotNETWeekly.Data
{
    using Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataRepository
    {
        Task<IEnumerable<Episode>> GetEpisodesAsync();

        Task<Episode> GetEpisodeByIdAsync(int id);

        Task<int> AddEpisodeAsync(Episode episode);

        Task UpdateEpisodeAsync(Episode episode);

        Task DeleteEpisodeAsync(int id);

    }
}
