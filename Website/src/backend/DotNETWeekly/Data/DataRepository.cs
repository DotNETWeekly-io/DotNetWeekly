using Dapper;

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

        public async Task<int> AddOrUpdateEpisode(Episode episode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstAsync<int>(@"EXEC dbo.Episode_Post
                    @Id=@Id, @Title=@Title, @Content=@Content, @CreateTime=@CreateTime",
                    episode);
            }
        }

        public async Task<int> CreateRecord(Record record)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstAsync<int>(@"EXEC dbo.Record_Post
                    @EpisodeId=@EpisodeId, @Title=@Title, @Link=@Link, @Content=@Content, @Category=@Category, @CreateTime=@CreateTime", record);
            }
        }

        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var episodes = await connection.QueryAsync<Episode>("EXEC dbo.Episode_Content_Get_ById @EpisodeId=@EpisodeId", new {EpisodeId = id});
                return episodes.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Episode>> GetEpisodesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Episode>("EXEC dbo.Episodes_Title_Get");
            }
        }
    }
}
