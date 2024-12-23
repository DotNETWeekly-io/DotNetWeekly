# .NET 每周分享第 59 期

## 卷首语

![image](https://github.com/user-attachments/assets/e857570e-b6f0-40be-8923-7d0ec627995e)

Jeffrey Snover 是 PowerShell 之父，这篇[访谈](https://corecursive.com/building-powershell-with-jeffrey-snover/)分享了过去关于 PowerShell 的一些故事，以及他对未来的一些展望。

## 行业资讯


1、[Rider 中 Task 视图](https://blog.jetbrains.com/dotnet/2024/07/02/how-to-use-the-tasks-view-in-jetbrains-rider/)

![image](https://github.com/user-attachments/assets/bd08cea9-ca9f-4316-8e09-3edb126573f3)

`Task` 是 `C#` 并发和异步编程中重要的概念， `Rider` 编辑器中增加一个新的功能，可以查看当前进行中所有的 Task 的状态， 比如 `Active`, `Scheduled`, `Awaiting`, `Blocking` 和 `Deadlocked`。

## 文章推荐

1、[Azure Function 迁移 .NET 8 步骤](https://blog.elmah.io/lessons-learned-after-migrating-azure-functions-to-isolated-functions-on-net-8/)

![image](https://github.com/user-attachments/assets/8b9c0a0f-3c4d-4e1d-8ed1-30d43b84cbd9)

Azure Function 将要移除 `In-Process` 模式，从而转向 `Isolated` 模式。那么在迁移的过程中需要注意什么呢？
首先这个两者的区别是：

- `In-Process` 模式: `function` 执行在 self host 的进程中
- `Isolated` 模式： `self host` 或其中一个进程来执行 `function`

首先需要一个 `Startup.cs` 文件

```csharp
[assembly:FunctionsStartup(typeof(MyFunctionApp.Startup))]
namespace MyFunctionApp;
public class Startup : FunctionsStartup {
  public override void Configure(IFunctionsHostBuilder builder) {
    builder.Services.AddLogging(logging => { logging.AddConsole(); });
#pragma warning disable CS0618  // Type or member is obsolete
    builder.Services.AddSingleton<IFunctionFilter, MyExceptionFilter>();
#pragma warning restore CS0618  // Type or member is obsolete
  }
}
```

这个文件和之前的 `ASP.NET Core` 类型，可以添加依赖注入。

之后相应 `Function` 文件

```csharp
namespace MyFunctionApp；
public class MyFunction {
  private readonly IMyDependency myDependency;

  public UptimeChecker(IMyDependency myDependency) {
    this.myDependency = myDependency;
  }

  [FunctionName("MyFunction")]
  public async Task Run([ TimerTrigger("0 */5 * * * *") ] TimerInfo myTimer,
                        ILogger log) {
    // Execute
  }
}
```

之后 `local.settings.json` 文件如下

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
  }
}
```

`csproj` 文件如下

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <AzureFunctionsVersion>v4</AzureFunctionsVersion>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Sdk.Functions" Version="4.2.0" />
    <PackageReference Include="Microsoft.Azure.Functions.Extensions" Version="1.1.0" />
  </ItemGroup>
  <ItemGroup>
    <None Update="host.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Update="local.settings.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      <CopyToPublishDirectory>Never</CopyToPublishDirectory>
    </None>
  </ItemGroup>
</Project>
```

由于现在是 `.NET 6` 版本，我们需要升级到 `.NET 8`，所以要继续处理

```xml
<TargetFramework>net8.0</TargetFramework>
<OutputType>Exe</OutputType>
<PackageReference Include="Microsoft.Azure.Functions.Worker" Version="1.10.0" />
<PackageReference Include="Microsoft.Azure.Functions.Worker.Extensions.Timer" Version="4.0.1" />
<PackageReference Include="Microsoft.Azure.Functions.Worker.Sdk" Version="1.7.0" />
```

创建一个 `program.cs` 文件

```csharp
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((context, app) =>
    {
        app.UseMiddleware<MyExceptionMiddleware>();
    })
    .ConfigureServices(services =>
    {
        var dependency = new MyDependency();
        services.AddSingleton<IMyDependency>(dependency);
    })
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
    })
    .Build();
```

这里部分的内容的是来自之前的 `Startup.cs` 文件。之后更新 `local.settings.json` 文件

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
  }
}
```

那么最后的 `Function` 文件修改

```csharp
namespace MyFunctionApp {
public class MyFunction
(IMyDependency myDependency, ILogger<MyFunction> logger) {
  private readonly IMyDependency myDependency = myDependency;
  private readonly ILogger<MyFunction> logger = logger;

  [Function("MyFunction")]
  public async Task Run([ TimerTrigger("0 */5 * * * *") ] TimerInfo myTimer) {
    // Execute
  }
}
}
```

2、[Powershell 中获取 Credentials](https://www.alitajran.com/securely-store-credentials-powershell/)

在 PowerShell 中有两种方式读取 `Credentail` 内容，一种是交互式，一种是非交互式。各自的的实现方式如下

- 交互式

```powershell
$Creds = Get-Credential
```

![image](https://github.com/user-attachments/assets/3735e9d4-9f12-42ce-a0e3-ad453be1c715)

如果在 Windows PowerShell 下的话，出现一个 UI 形式的对话框
![image](https://github.com/user-attachments/assets/0c165b12-7463-4c72-aced-8c7c4a1e97c7)

还有一种交互的方式是执行下面的代码

```powershell
$Creds = [System.Management.Automation.PSCredential]::new((Read-Host -Prompt "Enter username"), (Read-Host -Prompt "Enter password" -AsSecureString))
```

- 非交互式

非交互式的方式是读取某个文件，通常是将 `credential` 内容存在一个编码后的 `XML` 文件中

```powershell
$Creds | Export-CliXml -Path "C:\creds\credential.xml"
```

之后可以读取这个文件并且得到 `Crendetial` 对象

```powershell
$Creds = Import-CliXml -Path "C:\creds\credential.xml"
```

3、[理解 `IQueryable<T>`](https://dev.to/rasheedmozaffar/understanding-iqueryable-in-c-4n37)

`IQueryable` 是 `System.Linq` 命名空间中的一个类，它继承 `IEnumerable` 的接口，也即是所有的 `Linq` 表达式都可以用在 `IQueryable` 中，但是 `IQueryable` 中有一个重要的属性 `Expression` ，那么它是做什么的呢？

```csharp

List<FamousPerson> famousPeople = [
  new FamousPerson(1, "Sandy Cheeks", false),
  new FamousPerson(2, "Tony Stark", true),
  new FamousPerson(3, "Captain Marvel", true),
  new FamousPerson(4, "Captain America", true),
  new FamousPerson(5, "SpongeBob SquarePants", false),
  new FamousPerson(6, "Hulk", false)
];

IQueryable<FamousPerson> famousAndCanFly =
    famousPeople.AsQueryable().Where(x => x.CanFly);

famousAndCanFly = famousAndCanFly.Where(x => x.Id < 3);

famousAndCanFly = famousAndCanFly.Where(
    x => x.Name.Contains("s", StringComparison.OrdinalIgnoreCase));

Console.WriteLine(famousAndCanFly.Expression);

class FamousPerson
(int id, string name, bool canFly) {
  public bool CanFly => canFly;

  public int Id => id;

  public string Name => name;
}
```

输出的结果如下

```txt
System.Collections.Generic.List`1[FamousPerson].Where(x => x.CanFly).Where(x => (x.Id < 3)).Where(x => x.Name.Contains("s", OrdinalIgnoreCase))
```

一旦有了 `Expression` 这样的树结构，那么不同的数据提供者根据 Expression 实现自己的方式，比如 `EF Core` 可以将表达式树转换成相应的 SQL 语句，这样也能根据不同的情况进行优化。

4、[.NET 8 中 Resilient HttpClient](https://www.youtube.com/watch?v=kNzssE7Ir60&t=617s&ab_channel=NickChapsas)

![image](https://github.com/user-attachments/assets/4d8d9267-0b86-4200-a884-e127cf52c353)

我们知道 `Polly` 是 `.NET` 中著名的 Resilience 库，比如在使用 `HttpClient` 访问外部资源的时候，通常需要配置重试等机制。现在在 `ASP.NET Core` 中，可以使用 `Microsoft.Extensions.Http.Resilience` 来提高 `HttpClient` 的健壮性。

- 手动创建 ResiliencePipeline

```csharp
var pipeline = new ResiliencePipelineBuilder()
     .AddRetry(new Polly.Retry.RetryStrategyOptions(){
        ShouldHandle = new PredicateBuilder().Handle<Exception>(),
        BackoffType = DelayBackoffType.Exponential,
        MaxRetryAttempts = 3,
        UseJitter = true,
    })
    .Build();

var response = await pipeline.ExecuteAsync(async (ct) => await httpclient.GetAsync(url, ct));
```

- 注册 ResiliencePipeline

为了避免多次构建 `ResiliencePipeline`, 我们可以将构建好的 `ResiliencePipeline` 存放在依赖注入容器中

```csharp
builder.Services.AddResiliencePipeline("default", x => {
    x.AddRetry(new Polly.Retry.RetryStrategyOptions(){
        ShouldHandle = new PredicateBuilder().Handle<Exception>(),
        BackoffType = DelayBackoffType.Exponential,
        MaxRetryAttempts = 3,
        UseJitter = true,
    })
    .Build();
});

public class MyService(IHttpClientFactory, ResiliencePipelineProvider<string> pipelineProvider)
{
    public async Task Do()
    {
      var pipeline = pipelineProvider.GetPipeline("default");
      var response = await pipeline.ExecuteAsync(async (ct) => await httpclient.GetAsync(url, ct));
    }
}
```

- HttpClient Resilience Handler

为了进一步方便使用 `HttpClient`, 我们可以将 `ResiliencePipeline` 作为 `HttpClientHandler` 的一种内置实现，简化代码逻辑

```csharp
builder.Services.AddHttpClient<HttpBinService>().AddStandardResilienceHandler();
public class HttpBinService
{
    private readonly HttpClient _httpClient;

    public HttpBinService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<int> GetHttpBin()
    {
        var response = await _httpClient.GetAsync("https://httpbin.org/get");
        if (response.IsSuccessStatusCode)
        {
            return (int)response.StatusCode;
        }
        return (int)response.StatusCode;
    }
}
```

5、[ASP.NET Core 8 为什么这么快](https://x.com/julioc/status/1810667823464870146)

![image](https://github.com/user-attachments/assets/92a85f9a-a7e1-4fbb-9654-794356093a54)

ASP.NET Core 8 为什么这么快，作者列出了 10 点原因。

- 动态 PGO
根据代码的运行时行为自动优化代码，从而显著提高许多应用程序的性能。

- AVX512 支持
一组指令，可以一次处理 512 位数据，并提供了新的类型如 `Vector512<T>` 来利用这些指令。

- 改进的 JIT 编译器
增加了各种优化，例如分支消除、条件移动、常量折叠、边界检查等。

- 增强的 GC
通过后台标记、并发固定和减少碎片，使其更高效和可扩展。

- 改进的 Mono 运行时
通过 LLVM AOT、分层编译和解释器优化，为移动和 Web 应用提供更好的性能。

- 改进的线程和并行库
更快的线程池、改进的任务取消、减少的锁开销等。

- 改进的反射和异常处理
更快的元数据访问、减少分配和优化的异常过滤。

- 改进的原语和集合
更快的操作、减少装箱、改进的哈希和新的分析器。

- 改进的字符串、数组和跨度
更快的操作、减少分配、改进的 UTF8/ASCII 支持和新的正则表达式引擎。

- 改进的文件 I/O、网络、JSON 和加密库
更快的操作、减少分配、改进的 HTTP/3 支持和新功能。

6、[组织 ASP.NET Core 中 program.cs 文件](https://www.youtube.com/watch?v=0PCFdmb7kxo&list=WL&index=3&t=17s&ab_channel=AmichaiMantinband)

在 ASP.NET Core 8 默认采用了 Minimal API 风格，应用程序的入口地址在 `Program.cs` 文件，主要涉及到依赖注入和 WebApplicationBuilder 初始化。那么该如何组织这个文件呢？这里作者推荐了一种方式

1. 依赖注入服务

首先创建一个 `ServiceCollectionsExtensions.cs` 文件，使用 C# 拓展方法定义 `AddServices` 方法

```csharp
public static IServiceCollection AddServices(this IServiceCollection services)
{
     // add customized services
     return services;
}
```

2. 初始化 WebApplicationBuilder

创建一个 `WebApplicationExtensions` 文件，并且定义好每个中间件注入的方法，比如

```csharp
public static WebApplicationBuilder InitlizeDatabase(this WebApplicationBuilder app)
{
    return app;
}
```

所以完整的 `Program.cs` 文件内容如下

```csharp
var builder = WebApplication.CreateBuilder(args)
{
   builder.Services
        .AddServices()
       .AddControllers();
}

var app = builder.Build();
{
    app.MapControllers();
   app.InitializeDatabase();
}

app.Run();
```

7、[Rate Limiting in ASP.NET Core](https://lnkd.in/enMWVJjh)

![image](https://github.com/user-attachments/assets/c3236562-e9e6-4676-b81a-cf8e66533f0a)

限流可以使您的API更加健壮和安全。

它是一种限制服务器或API请求数量的技术。

有四种流行的限流算法：

固定窗口
滑动窗口
令牌桶
并发
然而，这些都是全局限流器，实际应用中很少实用。实际上，您希望对特定用户或IP地址进行限流。您可以使用 .NET 的限流服务和分区器来实现这一点。

如果要在分布式系统中应用限流，必须在反向代理（如 YARP）上定义限流策略。幸运的是，YARP 内置了对 .NET 限流的支持。

8、[ASP.NET Core 中使用 Serilog](https://blog.postsharp.net/serilog-aspnetcore?mtm_medium=paid&mtm_source=dotnetkicks&mtm_campaign=serilog-aspnet)

![image](https://github.com/user-attachments/assets/f0325403-5837-48a4-9111-963b5ee23a36)

大部分 ASP.NET Core 使用的是内置 `Microsoft.Extensions.Logging` 包来实现日志输出，但是 `Serilog` 和 `M.E.Loging` 兼容，并且有下面几点更加突出的功能

- 上下文和丰富器：虽然 Microsoft.Extensions.Logging 支持作用域，但它们相当有限且冗长。Serilog 上下文使得添加自定义上下文信息到日志中更加容易。丰富器提供了自动方式来添加常用信息，例如相关 ID。
- 接收器、格式化和过滤：Microsoft.Extensions.Logging 支持的日志提供程序数量有限，而 Serilog 提供了大约一百种不同的接收器，包括记录到文件和各种云服务及数据库。Serilog 还支持高级的格式化和过滤选项。
- 结构化日志记录：Microsoft.Extensions.Logging 对结构化日志记录的支持非常有限，而 Serilog 是围绕结构化日志记录构建的。这使得查询和分析日志变得更加容易，尤其是在拥有大量日志的情况下。

首先安装 `Serilog.AspNetCore` 包，然后配置相应的日志

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSerilog();
var app = builder.Build();
app.UseSerilogRequestLogging();
```

那么该如何在请求中使用 `Logger` 对象呢？

```csharp
[ApiController]
[Route( "[controller]" )]
public class WeatherForecastController( ILogger<WeatherForecastController> logger ) : ControllerBase

logger.LogDebug(
    "Returning weather forecast for the {days} days after today: {@forecast}",
    days,
    forecast );
```

当请求过来的时候，就会输出格式化日志。
Serilog 还支持往当前日志的 context 中添加额外的信息

```csharp
public sealed class PushPropertiesMiddleware : IMiddleware
{
    public async Task InvokeAsync( HttpContext context, RequestDelegate next )
    {
        var requestId = Guid.NewGuid().ToString();

        using (LogContext.PushProperty( "Host", context.Request.Host ))
        using (LogContext.PushProperty( "RequestId", requestId ))
        {
            await next( context );
        }
    }
}

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console( outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties}{NewLine}{Exception}" )
```

![image](https://github.com/user-attachments/assets/5d94886d-f2fc-4b2c-8825-de7ddb7a37d8)

9、[如何处理不同平台支持的代码](https://weblog.west-wind.com/posts/2024/Jul/18/Dealing-with-Platform-Specific-Classes-and-Methods-in-CrossPlatform-NET)

![image](https://github.com/user-attachments/assets/84baa0c8-1e04-4a4b-bed0-173c3c7de186)

`.NET` 开源后一直声明式跨平台的开发语言，但是由于一些历史原因，部分 API 仍然是 Windows 独有的，那么如果在代码中使用了他们，就会得到一个编译的时候的警告或者错误，那么该怎么解决他们呢？有三种方法

1. 忽略错误

如果编译发出警告的信息，可以通过在上下文见天 `#pragma` 来关掉警告

```csharp
#pragma warning disable CA1416
            Type type = Type.GetTypeFromProgID(progId);
#pragma warning restore CA1416
```

或者在 `csproj` 文件中添加 `NoWarn` 的属性

```xml
<PropertyGroup>
  ...
  <NoWarn>CA1416</NoWarn>
</PropertyGroup>
```

2. 配置 `SupportedOSPlatform` 

在使用的方法中添加 `SupportedOSPlatform` 的注解

```csharp
[SupportedOSPlatform("windows")]
public static object CreateComInstance(string progId)
{
    Type type = Type.GetTypeFromProgID(progId);
    if (type == null)
        return null;

    return Activator.CreateInstance(type);
}
```

在这个这种情况下，如果项目的运行时包含了 `Windows`， 那么就不会出现编译警告

```xml
<TargetFramework>net8.0-windows</TargetFramework>
```

3. 配置单独的库

另外一种方式将 Windows 相关的代码按照条件编译单独设置

```csharp
#if NETFRAMEWORK   // .NET Framework only
...
#endif
```

在这里，只有 `.NET Framework` 运行时才看到相关的方法的定义。

## 开源项目

1、[Shouldly](https://github.com/shouldly/shouldly)

![image](https://github.com/user-attachments/assets/bb353299-124c-4078-9461-f86265833b48)

Shouldly 可以输出更好的单元测试错误结果

```csharp
[TestMethod]
public void BuiltinTest() {
  string map = "hello world";
  Assert.AreEqual(map.IndexOf("world"), 3);
}

[TestMethod]
public void ShouldTest() {
  string map = "hello world";
  map.IndexOf("world").ShouldBe(3);
}
```

输出的结果

![image](https://github.com/user-attachments/assets/f2d09739-817b-4593-b1f0-9197f2dc3ba4)