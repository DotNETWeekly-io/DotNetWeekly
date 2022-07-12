using Microsoft.Extensions.Options;

namespace HttpClientSampleTest
{
    [TestClass]
    public class GitHubServiceTest
    {

        [TestMethod]
        public void FetchTest()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Get, "https://api.github.com/repos/DotNETWeekly-io/DotNetWeekly/contents/doc")
              .Respond(System.Net.HttpStatusCode.OK);
            httpClientFactory.Setup(p => p.CreateClient(Options.DefaultName)).Returns(new HttpClient(mockHttp));

            var service = new GitHubService(httpClientFactory.Object);
            Assert.AreEqual(service.Fetch(), System.Net.HttpStatusCode.OK);
        }
        
    }
}