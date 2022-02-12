namespace DotNETWeekly.Services
{
    using Data;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Net.Http.Headers;

    using Models;

    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateEpisodeHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<UpdateEpisodeHostedService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDataRepository _dataRepository;

        private Timer? _timer;

        public UpdateEpisodeHostedService(
            IHttpClientFactory httpClientFactory,
            IDataRepository dataRepository,
            ILogger<UpdateEpisodeHostedService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _dataRepository = dataRepository;
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating episodes");
            _timer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void Update(object? state)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.github.com/repos/gaufung/DotNetWeekly/contents/docs")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/vnd.github.v3+json" },
                    { HeaderNames.UserAgent, "dotnetweekly" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogInformation("Fetching the episodes successfully.");
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var files = await JsonSerializer.DeserializeAsync<GithubFile[]>(contentStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });
                if (files == null)
                {
                    return;
                }
                var existingEpisodes = await _dataRepository.GetEpisodesAsync();
                foreach (var file in files)
                {
                    var episode = existingEpisodes.FirstOrDefault(p => p.Title == file.Title);
                    await UpdateEpisode(file, episode);
                }
            }
            else
            {
                _logger.LogWarning($"Failed to fetch the episodes. status code {httpResponseMessage.StatusCode}");
            }
        }

        private async Task UpdateEpisode(GithubFile file, Episode? episode)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, file.Url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/vnd.github.v3+json" },
                    { HeaderNames.UserAgent, "dotnetweekly" }
                }
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Fetch {file.Name} successfully");
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var fileContent = await JsonSerializer.DeserializeAsync<GithubFileContent>(contentStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });
                if (fileContent == null)
                {
                    _logger.LogWarning($"Failed to read {file.Name} content");
                }
                if (episode == null)
                {
                    await _dataRepository.AddEpisodeAsync(new Episode()
                    {
                        Title = file.Title,
                        Content = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(fileContent.Content)),
                    });
                }
                else
                {
                    await _dataRepository.UpdateEpisodeAsync(new Episode()
                    {
                        Id = episode.Id,
                        Title = file.Title,
                        Content = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(fileContent.Content)),
                    });
                }
            }
            else
            {
                _logger.LogWarning($"Failed to fetch {file.Name}. Status code {httpResponseMessage.StatusCode}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping updating the episode");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        class GithubFile
        {
            private static Regex regex = new Regex(@"episode-(?<index>\d+)\.md"); 

            public string Name { get; set; }

            public string Url { get; set; }

            public string Type { get; set; }

            private string _title;

            internal string Title
            {
                get
                {
                    if (string.IsNullOrEmpty(_title))
                    {
                        var match = regex.Match(Name);
                        if (match.Success)
                        {
                            var index = int.Parse(match.Groups["index"].Value);

                            _title =  $".NET 周刊第 {index} 期";
                        }
                        else
                        {
                            _title = string.Empty;
                        }
                    }
                    return _title;
                }
            }
        }

        class GithubFileContent
        {
            public string Content { get; set; }
        }
    }
}
