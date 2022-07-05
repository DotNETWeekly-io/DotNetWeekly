using DotNETWeekly.Models;

using Microsoft.Azure.Cosmos;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotNETWeekly.Data
{
    using Microsoft.Extensions.Options;

    using Options;

    public class CosmosDbEpisodeService : IEpisodeService
    {
        private readonly CosmosDbOptions _options;

        public CosmosDbEpisodeService(IOptionsSnapshot<CosmosDbOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public async Task AddEpisode(Episode episode, CancellationToken token)
        {
            var container = await GetOrCreateContainer(false, false, token);
            await container.CreateItemAsync(episode, cancellationToken: token);
        }

        public async Task AddEpsidoeSummary(EpisodeSummary episodeSummary, CancellationToken token)
        {
            var container = await GetOrCreateContainer(true, false, token);
            await container.CreateItemAsync(episodeSummary, cancellationToken: token);
        }

        public async Task<IEnumerable<Episode>> GetEpisodes(CancellationToken token)
        {
            var container = await GetOrCreateContainer(false, true, token);
            var query = new QueryDefinition("SELECT * FROM c");
            var queryIterator = container.GetItemQueryIterator<Episode>(query);
            var episodes = new List<Episode>();
            while (queryIterator.HasMoreResults)
            {
                var response = await queryIterator.ReadNextAsync();
                foreach (var item in response)
                {
                    episodes.Add(item);
                }
            }
            return episodes;
        }

        public async Task<Episode> GetEpisode(string id, CancellationToken token)
        {
            var container = await GetOrCreateContainer(false, true, token);
            PartitionKey key = new (id);
            return await container.ReadItemAsync<Episode>(id.ToString(), key);
        }

        public async Task<IEnumerable<EpisodeSummary>> GetEpisodeSummaries(CancellationToken token)
        {
            var container = await GetOrCreateContainer(true, true, token);
            var query = new QueryDefinition("SELECT * FROM c");
            var queryIterator = container.GetItemQueryIterator<EpisodeSummary>(query);
            var episodeSummary = new List<EpisodeSummary>();
            while (queryIterator.HasMoreResults)
            {
                var response = await queryIterator.ReadNextAsync();
                foreach (var item in response)
                {
                    episodeSummary.Add(item);
                }
            }
            return episodeSummary;    
        }

        public async Task UpdateEpisode(string id, Episode episode, CancellationToken token)
        {
            var container = await GetOrCreateContainer(false, false, token);
            await container.UpsertItemAsync<Episode>(episode);
        }

        public async Task UpdateEpisodeSummary(string id, EpisodeSummary episodeSummary, CancellationToken token)
        {
            var container = await GetOrCreateContainer(true, false, token);
            await container.UpsertItemAsync<EpisodeSummary>(episodeSummary);
        }

        private async Task<Container> GetOrCreateContainer(bool isSummary, bool readOnly, CancellationToken token)
        {
            CosmosClient client = new CosmosClient(_options.EndPoint, readOnly ? _options.PrimaryReadOnlyKey :  _options.PrimaryKey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(_options.DatabaseName, cancellationToken: token);
            Container container = await database.CreateContainerIfNotExistsAsync( isSummary ? _options.EpisodeSummaryContainer :  _options.EpisodeContainer, "/id", cancellationToken: token);
            return container;
        }

        public async Task DeleteEpisodeSummary(string id, CancellationToken token)
        {
            var container = await GetOrCreateContainer(true, false, token);
            PartitionKey key = new(id);
            await container.DeleteItemAsync<EpisodeSummary>(id, key, cancellationToken: token);
        }

        public async Task DeleteEpisode(string id, CancellationToken token)
        {
            var container = await GetOrCreateContainer(false, false, token);
            PartitionKey key = new(id);
            await container.DeleteItemAsync<Episode>(id, key, cancellationToken: token);
        }
    }
}
