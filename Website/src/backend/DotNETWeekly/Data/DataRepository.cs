using Dapper;
using System.Linq;

using DotNETWeekly.Models;

using Microsoft.Data.SqlClient;
namespace DotNETWeekly.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<int> CreateEpisode(Episode episode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstAsync<int>(@"EXEC dbo.Episode_Post
                    @Title=@Title, @Introduction=@Introduction, @CreateTime=@CreateTime",
                    episode);
            }
        }

        public async Task<int> CreateRecord(Record record)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstAsync<int>(@"EXEC dbo.Record_Post
                    @EpisodeId=@EpisodeId, @Title=@Title, @Content=@Content, @Category=@Category, @CreateTime=@CreateTime", record);
            }
        }

        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                Episode episode = default; 
                return (await connection.QueryAsync<Episode, Record, Episode>(
                    "EXEC dbo.Episode_Get_ById @EpisodeId=@EpisodeId",
                    map: (q, a) =>
                    {
                        if (episode == null)
                        {
                            episode = q;
                        }
                        if (episode.Records == null)
                        {
                            episode.Records = new List<Record>();
                        }
                        episode.Records.Add(a);
                        return episode;
                    }, new { EpisodeId = id }
                    )).FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Episode>> GetEpisodesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Episode>("EXEC dbo.Episodes_Get");
            }
        }
    }
}
