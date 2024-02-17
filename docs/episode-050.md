# .NET 每周分享第 50 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9f500382-b917-496f-a6b3-231e00c38abc)

最近微软在自己的招聘网站上发布了职位，要求熟练使用 `C#`，而且工作内容是负责 `Microsoft 365` 产品。那么是否意味着微软要摒弃 `C#` 拥抱 `Rust` 呢？
这个话题在 [Reddt](https://www.reddit.com/r/dotnet/comments/1aezqmg/came_across_a_job_posting_on_microsoft_career/) 上也又相关的讨论，我的看法是，没有语言都有自己擅长的使用场景，这个招聘信息并没有太多的值得注意的地方。

## 行业资讯

1、[Aspire 项目会死吗](https://www.youtube.com/watch?v=2L68EldtKFo&ab_channel=NickChapsas)

Reddit 上有一个用户说 `Aspire` 项目会在 5 年内死亡，视频作者对这个看法发表自己的观点：

1. 每个大的公司都会停止开发一些产品和项目。
2. Aspire 仍然处于 Preview 状态，但是不停的提供新的功能

但是令人担心的是 `Aspire` 很多包都是微软维护的，这个并不能保证随时能得到更新。

2、[Dev Tunnel](https://devblogs.microsoft.com/dotnet/dev-tunnels-a-game-changer-for-mobile-developers/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/35b15737-4405-46b4-8939-a0b08c545c15)

Dev Tunnel 是 Visual Studio 推出的实验性功能，它主要解决的是在移动开发领域，应用程序在和后端服务通信时候调试问题，比如本地开发的时候只能通过 `localhost` 访问，而移动应用模拟器并不能直接访问开发机器。这给调试工作增加了困难。而 Dev Tunnel 可以提供一个域名可以直接访问，而且调式仍然发生在本地。

## 文章推荐

1、[HTTP Redirect 特殊处理 Authentication Header](https://ardalis.com/http-file-not-sending-auth-header/)

在 `ASP.NET Core` 中，如果返回 `301 redirect` 这样的请求，那么后续的请求会将 `Authentication` 头部信息丢失，这是因为 `HTTP` 协议的规范。

2、[Polly 库中 Chaos Engineering](https://devblogs.microsoft.com/dotnet/resilience-and-chaos-engineering/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/87de5767-1c84-40a6-ad6b-d9fd433ffb79)

`Polly` 8.0 版本引入了 `Chaos Engineering` 功能，它能够主动引入 Fault 的机制，这样要求我们的应用程序能够主动进行 `Resilience` 相关的操作。它主要包含四种类型的策略

- Fault: 引入异常
- Outcome: 插入假的输出
- Latency: 增加延迟
- Behavior: 开启额外行为

在 `HttpClient` 中示例

1. 创建 `HttpClient`

```csharp
var httpClientBuilder = builder.Services.AddHttpClient<TodosClient>
(client => client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));
```

2. 引入 `ResilienceHandler`

```csharp
httpClientBuilder.AddResilienceHandler("chaos", (builder, context) =>
{
    var chaosManager = context.ServiceProvider.GetRequiredService<IChaosManager>();
    builder
        .AddChaosLatency(new ChaosLatencyStrategyOptions
        {
            EnabledGenerator = args => chaosManager.IsChaosEnabledAsync(args.Context),
            InjectionRateGenerator = args => chaosManager.GetInjectionRateAsync(args.Context),
            Latency = TimeSpan.FromSeconds(5),
        })
        .AddChaosFault(new ChaosFaultStrategyOptions
        {
            EnabledGenerator = args => chaosManager.IsChaosEnabledAsync(args.Context),
            InjectionRateGenerator = args => chaosManager.GetInjectionRateAsync(args.Context),
            FaultGenerator = new FaultGenerator().AddException(() => new InvalidOperationException("Chaos strategy injection!"))
        })
        .AddChaosOutcome(new ChaosOutcomeStrategyOptions<HttpResponseMessage>
        {
            EnabledGenerator = args => chaosManager.IsChaosEnabledAsync(args.Context),
            InjectionRateGenerator = args => chaosManager.GetInjectionRateAsync(args.Context),
            OutcomeGenerator = new OutcomeGenerator<HttpResponseMessage>().AddResult(() => new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError))
        });
});
```

这样，当我们使用 `TodosClient` 的时候，在发起 `HTTP` 请求的时候就会有几率触发相应的 `Fault` 行为。

3、[ASP.NET Core Functional Test](https://www.youtube.com/watch?v=ASa8wXMXwrQ&ab_channel=MilanJovanovi%C4%87)

不同于单元测试，功能测试（或者集成测试）是一种端到端的测试，它会尽可能的测试软件或者系统对外界暴露的接口测试，而且外部依赖保持一致。在 `ASP.NET Core` 中，我们对外暴露的是 Web API 请求，依赖可能是数据库，文件系统访问等等。我们可以借助两个库完成上述的的要求

1. Microsoft.AspNetCore.Mvc.Testing

该库的作用是拷贝应用程序的依赖至测试功能，并且内容根目录内容至测试工程下，并且提供了 `WebApplicationFactory` 来启动测试服务。

```csharp
public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            // Add Integration test Dbcontext
        });

        builder.UseEnvironment("Development");
    }
}
```

2. TestContainer

这个库帮助我们运行一个 Container，然后将我们的外部依赖，比如数据库给运行起来，这样就能在测试的时候直接连接该数据库。

```csharp
PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
.WithImage("Postgres:latest")
.WithDatatabase("runtrack")
.WithUserName("User")
.WithPassword("password")
.Build()
```

这样就能获得一个 `Postgre` 数据库，在我们 `WebApplicationFactory` 构造 `WebHost` 的时候，将它作为数据库的来源。


4、[TerminalLogger](https://www.meziantou.net/enable-the-new-terminallogger-in-dotnet-8-sdk-automatically.htm)

.NET 8 Build 引入了新的日志输出框架: `TerminalLogger`, 相对于其他默认的日志输出有如下的优势

1. Warning 和 Error 安装目前框架分组
2. 通过颜色让输出更加容易阅读
3. 超链接到build 主要输出
4. 展示每个 build target 耗时
5. 编译最终结果更加清晰

- 默认日志输出

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/ea4b769d-68cf-48fb-825b-a35876e62a07)

- TerminalLogger 输出

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/48748132-fa97-44db-88e9-0b8eb2143729)

5、[C# 11 中检查集合空](https://www.meziantou.net/checking-if-a-collection-is-empty-in-csharp.htm)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/b46855c3-789e-4ac3-93f0-669d9ac9878d)

在 `C# 11` 中包含了模式匹配功能可以帮助写出更加优雅的代码，尤其是集合的 `Null` 或者空判断。

```csharp
var collection = new Collection<object>();

if (collection is null or [])
{
    Console.WriteLine("collection is not null or empty");
}

var array = (string[])null;

if (array is null or [])
{
    Console.WriteLine("array is null or empty");
}

array = Array.Empty<string>();

if (array is null or [])
{
    Console.WriteLine("array is null or empty");
}
```

## 开源项目

1、[coravel](https://github.com/jamesmh/coravel)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/718b2174-e839-44f7-a763-5d8d41f044b7)

`Coravel` 是 `.NET` 生态中用来实现定时任务的库，不同 `Hangfire` 或者 `Quartz`，它支持异步和依赖注入编程方式，而且支持 `Fluent` 编程方式，在 ASP.NET Core 中的实例

```csharp
builder.Services.AddScheduler();
app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule(() =>
    {
        Console.WriteLine("Every second.");
    })
    .EverySecond();
});
```

2、[EF Core SQL debug 插件](https://marketplace.visualstudio.com/items?itemName=GiorgiDalakishvili.EFCoreVisualizer)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/36ce7684-b28a-4ce3-b2ce-893409cd2b74)

Visual Studio 一款插件，可以展示 `EF Core` 查询语句的执行计划，这个对查询优化工作非常有帮助。