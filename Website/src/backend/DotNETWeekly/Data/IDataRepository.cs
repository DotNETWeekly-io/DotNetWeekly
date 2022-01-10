namespace DotNETWeekly.Data
{
    using Models;

    public interface IDataRepository
    {
        Task<IEnumerable<Episode>> GetEpisodesAsync();

        Task<Episode> GetEpisodeByIdAsync(int id);

        Task<int> AddOrUpdateEpisode(Episode episode);

        Task<int> CreateRecord(Record record);

    }
}
