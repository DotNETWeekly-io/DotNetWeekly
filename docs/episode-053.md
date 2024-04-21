# .NET 每周分享第 53 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/ffb35c45-aaf6-49f5-a9a5-3711daa0f6bf)

`Github Copliot Chat` 现在已经完全支持`Visual Studio`。

## 行业咨询

1、[解决方案文件新格式](https://www.youtube.com/watch?v=D0MxmDWk4t0&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a015609a-0e83-4af0-9379-147a36d7b535)

在 `.NET` 项目中工程文件，比如 `csproj` ，其中的内容大家都非常熟悉，但是对于解决方案文件 `sln` 文件，大多数人都望而却步，因为其中的内容太复杂了。在 Visual Studio 新版的 preview 功能，可以选择 `slnx` 的格式，该格式中内容非常简单明了,
比如

```xml
<Solution>
  <Project Path="RecordTypes.csproj" />
</Solution>
```

2、[查看 .NET 源码](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollection?view=dotnet-plat-ext-8.0)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0b9999dc-6045-4276-9ef0-0a1d166b2335)

Microsoft 官方文档增加了一个新功能，当我们查看某一个类的时候，可以点击 `Source` 链接查看源码。

## 文章推荐

1、[ASP.NET Core HttpClient 请求优化](https://www.youtube.com/watch?v=pJDIdIcOH6s&ab_channel=MilanJovanovi%C4%87)

`HttpClient` 是在 `.NET` 应用程序中广泛使用的类，它可以通过 HTTP 协议访问外部资源。其中在 `ASP.NET Core` 中可以更加优雅的方式使用他们。

1. 使用 HttpClient 服务

首先将外部资源封装成一个服务，并且接受一个 `HttpClient` 对象

```csharp
public class GitHubService 
{
    private readonly HttpClient _httpClient;
    public GitHubService(HttpClient httpClient) {
        _httpClient = httpClient;
    }
    public async Task<GithubFile[]?> GetFile(string org, string repo, string folder) {
         //...
    }
}
```

然后在依赖注入容器中注入该服务，并且配置相应的 `HttpClient` 基本信息

```csharp
builder.Services.AddHttpClient<GitHubService>((sp, httpclient) =>
{
    var setting = sp.GetRequiredService<IOptions<GithubSetting>>().Value;
    httpclient.BaseAddress = new Uri(setting.BaseUrl);
});
```

2.  使用 Refit

[Refit](https://github.com/reactiveui/refit) 是一个 C# 的开源库，你只需要定义好配置访问资源的接口，该库可以生成相应的实现

```csharp
public interface IGithubApi
{
    [Get("/repos/{org}/{repo}/contents/{folder}")]
    Task<GithubFile[]?> GetGithubFiles(string org, string repo, string folder);
}
```

这里 `[Get("/repos/{org}/{repo}/contents/{folder}")]` 注解是来自 `Refit` 的定义。然后将 `IGithubApi` 接口注入到容器中

```csharp
builder.Services.AddRefitClient<IGithubApi>()
    .ConfigureHttpClient((sp, httpclient) =>
    {
        var setting = sp.GetRequiredService<IOptions<GithubSetting>>().Value;
        httpclient.BaseAddress = new Uri(setting.BaseUrl);
    });
```

3. 继承 `DelegatingHandler` 抽象类

如果 `HttpRequest` 还有其他配置信息，比如授权的什么，我们实现一个 `DeletingHandler` 类

```csharp
public class GithubAuthHandler : DelegatingHandler
{
    private readonly GithubSetting _githubSetting;
    public GithubAuthHandler(IOptions<GithubSetting> options) {
        _githubSetting = options.Value;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        request.Headers.Add("Accept", _githubSetting.Accept);
        request.Headers.Add("User-Agent", _githubSetting.UserAgent);
        return base.SendAsync(request, cancellationToken);
    }
}
```

然后在容器中配置该类

```csharp
builder.Services.AddTransient<GithubAuthHandler>();
builder.Services.AddRefitClient<IGithubApi>()
    .ConfigureHttpClient((sp, httpclient) =>
    {
        var setting = sp.GetRequiredService<IOptions<GithubSetting>>().Value;
        httpclient.BaseAddress = new Uri(setting.BaseUrl);
    }).AddHttpMessageHandler<GithubAuthHandler>();
```

2、[使用 GitHub Action 创建测试报告](https://seankilleen.com/2024/03/beautiful-net-test-reports-using-github-actions/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/2dc14090-9963-4cfe-a250-9efcf5ebb97e)

从 `Azure DevOps` 迁移到 `GitHub Action` 的时候，测试报告是丢失的功能，所以这篇文章介绍如何在 `GitHub Action` 中生成`.NET` 应用程序包含测试结果的方法。

3、[.NET Web 开发历史](https://www.irisclasson.com/2024/03/29/the-history-of-.net-web-development-3rd-edition/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/859e6c1e-1706-4015-987a-fd3f3a99fa65)

这本书介绍了 `.NET` 平台 Web 发展历史。

4、[C# double decimal](https://sharplab.io/#v2:C4LgTgrgdgNAJiA1AHwAICYAMBYAUBgRjzzgFMBjASwFsBDAGwAIyq76BnCaxgXkc2oBuEgHsIAI3qlmYyaU7c+mOMNwAzEWAAUlKMEaVe/QQcYAeRgUzWTlRIgCUeAN55G75hRoMFjREoA6AiE3DzhZKV9/fiCVPABfPFQCAE4tABIAIhZvJgUQRmcctgV4zIdVZLSs4oZGfMLaji4yiqA=)

在 `.NET` 中有 `double` 和 `decimal` 类型，那么它们的区别是怎样的，这里有一个简单的例子

```csharp
decimal decimalsum = 0m;
double doublesum = 0d;
for(int i = 0; i < 1000; i++)
{
    decimalsum += 0.1m;
    doublesum += 0.1d;
}
Console.WriteLine($"decimal sum: {decimalsum}");
Console.WriteLine($"decimal sum: {decimalsum}")
```

得到的输出结果如下:

```shell
decimal sum: 100.0
double sum: 99.9999999999986
```

5、[异步高级功能](https://www.youtube.com/watch?v=GQYd6MWKiLI&list=WL&index=1&ab_channel=NDCConferences)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/20ca55f0-f4bd-43fd-8ceb-e1607778c858)

异步 `await` 是 C# 中特色，这个演讲中，作者分享了异步中的高级用法

1. 使用 `await` 而不是 `Wait()` 或者 `Result`
2. 使用 `Fire and Forget` 方式处理无结果任务
3. 避免 `return await` 方法
4. 使用 `ConfigureAwait(false)`
5. 使用 `ConfigureAwait(ConfigureAwaitOptions options)` 方法
6. 使用 `ValueTask`
7. 对于流数据，使用 `IAsyncEnumerable`
8. 使用 `waitAsync(CancellationToken)`
9. 使用 `IAsyncDisposable`

6、[.NET Container 支持](https://devblogs.microsoft.com/dotnet/streamline-container-build-dotnet-8/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/d0d66b66-737a-4b8b-a875-bbf02567cfe2)

`.NET 8` 是支持 `container` 中最大的一个版本，这篇文章详细介绍了其中的内容，包括

1. 使用 `dotnet publish` 命令发布一个 container 镜像
2. 为不同的 Linux 发行版本创建镜像
3. 创建一个不同语言的镜像
4. ...

## 开源项目

1、[JSON Everthing](https://github.com/gregsdennis/json-everything)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6e68403c-3ba7-405d-90e5-6f3037b34463)

`JSON-Everything` 是 `.NET` 下关于 JSON 处理的通用项目，比如 `JSON` schema 的生成和校验， JSON Path 和其他功能。