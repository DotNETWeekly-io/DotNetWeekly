using Dapper;

using DotNETWeekly.Models;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DotNETWeekly.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<int> AddEpisodeAsync(Episode episode)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstAsync<int>(@"EXEC dbo.Episode_Post
                    @Title=@Title, @Content=@Content", episode);
            }
        }

        public async Task DeleteEpisodeAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(@"EXEC dbo.Episode_Delete
                    @Id=@Id", new { Id = id });
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

        public async Task UpdateEpisodeAsync(Episode episode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.QueryAsync<Episode>("EXEC dbo.Episode_Put @Id=@Id, @Title=@Title, @Content=@Content", episode);
            }
        }
    }
}
