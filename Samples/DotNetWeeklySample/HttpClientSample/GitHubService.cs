using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class GitHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GitHubService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory=httpClientFactory;   
        }

        public System.Net.HttpStatusCode Fetch()
        {
            string url = "https://api.github.com/repos/DotNETWeekly-io/DotNetWeekly/contents/doc";
            var httpClient = _httpClientFactory.CreateClient();
            var result = httpClient.GetAsync(url).Result;
            return result.StatusCode;
        }
    }
}
