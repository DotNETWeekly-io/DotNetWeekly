# .NET 每周分享第 68 期

## 卷首语

![Image](https://github.com/user-attachments/assets/aa64f5f2-fea2-49f0-bb08-e0dbfe05c5cc)

最新的`.NET 10`预览版中，推出了一个令人兴奋的功能，它将大大简化开发的繁琐程度和新人的学习曲线。没有解决方案文件(sln)，没有工程项目文件（csproj），只需要一个代码文件 (cs) 即可。

```csharp
Console.WriteLine("hello world");
```

使用 `dotnet run app.cs` 命令即可输出 `hello world`。如果项目使用了第三方库，那么可以通过类似条件编译一样引入第三方库

```csharp
#:package Newtonsoft.Json@13.*
var json = JsonConvert.SerializeObject(new { Name = "John", Age = 30 });
Console.WriteLine(json);
```

这个新的命令还支持使用 SDK，比如想要运行一个 `Web` 应用程序

```csharp
#:sdk Microsoft.NET.Sdk.Web

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
```

直接使用 `dotnet run app.cs` 就可以本地启动一个 `ASP.NET Core Web API` 的应用程序。
最后如果想要转换为传统的工程项目格式，可以使用 `dotnet project covert app.cs` 命令即可。

## 行业资讯

1、[MAUI的未来](https://x.com/migueldeicaza/status/1922409129567563855)

在最近的微软裁员中，MAUI一些工程师收到了影响，估计 MAUI 的未来会受到影响。

## 文章推荐

1、[为什么要用 Aspire](https://csharp.christiannagel.com/2025/05/08/why-dotnet-aspire/)

![Image](https://github.com/user-attachments/assets/4cb7f42b-40b0-42a4-83b5-c070f3196db7)


开发与部署的统一框架

.NET Aspire 是一个旨在简化开发与部署流程的框架，适用于开发人员和 DevOps 工程师。它通过以下方式提升开发体验：

- **基础设施自动化**：通过 Docker 集成，自动拉取所需的服务（如 SQL Server、Redis 等），并建立可视化的服务关系。
- **资源监控**：在开发过程中，实时监控内存使用、服务调用频率和数据库访问时长，帮助开发者及时发现并解决性能问题。
- **部署灵活性**：支持多种部署方式，包括 Microsoft Azure、Kubernetes 和 Docker Compose，且不强制要求使用特定的部署方式。

逐步集成至现有项目

.NET Aspire 允许将其功能逐步集成到现有项目中，无需完全重构。以下是集成的基本步骤：

1. **创建 API 项目**：使用 `dotnet new webapi` 创建一个基础的 Web API 项目。
2. **添加 AppHost 项目**：使用 .NET Aspire CLI 创建一个 AppHost 项目，并将其与 API 项目关联。
3. **配置服务默认设置**：通过添加 `Weather.ServiceDefaults` 库，启用结构化日志、分布式追踪和指标收集。
4. **启动与监控**：运行 AppHost 项目后，使用仪表板查看服务状态、日志和性能指标。

部署与发布

.NET Aspire 提供了新的 Aspire CLI 工具，简化了部署过程。通过配置应用模型，可以为 Azure、Kubernetes 和 Docker Compose 创建发布工件。即使不使用 .NET Aspire 进行部署，现有的 .NET 应用仍然可以正常运行。

渐进式采用

.NET Aspire 支持渐进式采用，开发者可以根据项目的实际需求，选择性地使用其功能。无论是在本地开发环境中提升开发效率，还是在生产环境中增强监控能力，.NET Aspire 都能提供相应的支持。

结论

.NET Aspire 是一个强大的工具，旨在简化开发和部署流程。通过其自动化的基础设施管理、实时的资源监控和灵活的部署选项，开发者可以更专注于业务逻辑的实现，而将复杂的基础设施管理交给框架处理。无论是新项目的开发，还是现有项目的优化，.NET Aspire 都是一个值得考虑的选择。

2、[清理 nuget 缓存](https://steven-giesel.com/blogPost/ef7e9271-3b8d-4658-988f-b48bbd11e320/clearing-nuget-caches)

NuGet 缓存机制虽然提高了构建效率，但在某些情况下（如版本冲突、包损坏或调试本地包）需要清除缓存。文章详细介绍了 NuGet 的缓存位置、类型，以及清除方式。

NuGet 缓存类型

1. **HTTP 缓存（Http Cache）**

- 存储从 NuGet 源下载的 `.nupkg` 文件
- 默认位置：`%userprofile%\.nuget\packages\http-cache`

2. **全局包缓存（Global packages）**

- 存储所有使用的 NuGet 包解压后的内容
- 默认位置：`%userprofile%\.nuget\packages`

3. **临时缓存（Temp Cache）**

- 临时使用的缓存文件
- 默认位置：`%temp%\NuGetScratch`

使用 CLI 工具

```bash
dotnet nuget locals global-packages --clear
dotnet nuget locals http-cache --clear
dotnet nuget locals temp --clear

dotnet nuget locals all --clear
```

3、[.NET 实验性 GC 表现](https://blog.applied-algorithms.tech/a-sub-millisecond-gc-for-net)

本文介绍了 .NET 运行时中一个名为 Satori 的实验性垃圾回收器（GC）。Satori 在多个关键性能指标上相较于传统的 Server GC 模式表现出显著的提升。

**主要成果**

- 中位暂停时间：提升 50 倍
- 99% 分位暂停时间：提升超过 100 倍
- 堆大小：减少至原来的三分之一

这些改进使得 Satori 特别适用于对延迟敏感的应用场景，如实时系统和高频交易等。

自动垃圾回收简化了内存管理，但也带来了不可预测的“stop-the-world”暂停，这在大型或高吞吐量应用中可能导致性能瓶颈。

**Satori 的优势**

- 极低的暂停时间：适用于对延迟要求极高的应用
- 更小的堆内存占用：提高资源利用率
- 更高的吞吐量：减少 GC 对应用性能的影响

**如何试用 Satori**

Satori 目前作为实验性功能在 GitHub 上提供，开发者可以访问 [Satori 的 GitHub 页面](https://github.com/dotnet/runtime/discussions/115627) 获取最新信息。

## 视频推荐

1、[Scoped 服务的依赖注入生命周期](https://www.youtube.com/watch?v=AF224eUmWLc&ab_channel=NickChapsas)

在 `Microsoft.Extensions.DependencyInjection` 中，所有注入到容器的服务有三种类型的生命周期

- Singleton
- Scoped
- Transient

在构建 `IServiceProvider`  服务的时候，有一个 `ServiceProviderOptions` 类型的参数，其中 `ValidateScope` 属性是用来做什么的呢？

```csharp
interface IGenerater
{
    string Generate();
}

class Generater : IGenerater
{
    public string Generate() => Guid.NewGuid().ToString();
}

class MyService
{
    private readonly IGenerater _generater;

    public MyService(IGenerater generater)
    {
        _generater = generater;
    }
}
```

这里有两个服务，其中 `MyService` 服务依赖一个 `IGenerater` 服务，如果我们注册服务的是这样的

```csharp
var services = new ServiceCollection();
services.AddScoped<IGenerater, Generater>();
services.AddSingleton<MyService>();

var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
{
    ValidateScopes = true,
});
```

运行的时候，会有这样的错误

```txt
Unhandled exception. System.AggregateException: Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: MyService Lifetime: Singleton ImplementationType: MyService': 
Cannot consume scoped service 'IGenerater' from singleton 'MyService'.)
```

原因是 `Singleton` 服务如果依赖 `Scoped` 服务的话，在 `Scoped` 服务完成之后，并不会被垃圾回收收集，从而导致内存泄漏。所以正确的做法是，`MyService` 依赖于 `IServiceScopeFactory` 服务。

```csharp
class MyService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MyService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public string GetNewGuid()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var generater = scope.ServiceProvider.GetRequiredService<IGenerater>();
        return generater.Generate();
    }
}
```

在使用依赖服务的时候，使用 `CreateScope` 方法创建一个 Scope

## 开源项目

1、[pgvector](https://github.com/pgvector/pgvector-dotnet)

pgvector-dotnet 是一个为 .NET 平台（包括 C#、F# 和 Visual Basic）提供的开源库，旨在简化与 PostgreSQL 数据库中 pgvector 扩展的集成。pgvector 是一个 PostgreSQL 扩展，专门用于存储和查询高维向量，广泛应用于语义搜索、推荐系统、图像检索等领域。
🔧 核心功能

- 向量数据支持：提供 Vector、HalfVector、SparseVector 等数据类型，支持存储单精度、半精度和稀疏向量。
- 数据库库兼容性：支持与 Npgsql、Dapper、Entity Framework Core 和 Npgsql.FSharp 的集成，使得在 .NET 环境中操作 PostgreSQL 向量数据变得更加便捷。
- 扩展支持：支持 PostgreSQL 的 vector 扩展，允许在数据库中创建向量列，并执行向量插入、更新、查询等操作。
- 示例项目：提供多个示例，展示如何使用 OpenAI 生成嵌入、使用 Cohere 处理二进制嵌入、实现混合搜索、稀疏搜索、推荐系统、主题建模、水平扩展等功能。

2、[IdGen](https://github.com/RobThree/IdGen)

![Image](https://github.com/user-attachments/assets/3a2bc8ea-5cfc-4153-9fd1-9714393e057a)

IdGen 是一个为 .NET 平台设计的高性能、分布式、无协调的 ID 生成器，灵感来源于 Twitter 的 Snowflake 项目。该库生成 64 位（实际为 63 位）时间排序的唯一 ID，适用于需要高并发、低延迟且可扩展的场景，如分布式系统、数据库主键生成等。

核心特性

- **结构化 ID**：ID 由时间戳、生成器 ID 和序列号三部分组成，支持自定义结构。
- **高并发支持**：每毫秒内可生成数十万 ID。
- **可配置时间源**：默认使用系统时钟，也可自定义 `ITimeSource` 实现。
- **支持多种部署方式**：适用于单机、分布式、容器化等多种环境。
- **与 Azure Functions 兼容**：支持在 Azure Functions 等无状态环境中使用。

3、[ZLinq](https://github.com/Cysharp/ZLinq)

`Zlinq` 是 `Linq` 的增强版，它通过减少内存分配的方式提高性能。使用方式也非常简单，只需要一个一行代码即可

```csharp
using ZLinq;

var seq = source
    .AsValueEnumerable() // only add this line
    .Where(x => x % 2 == 0)
    .Select(x => x * 3);

foreach (var item in seq) { }
```

而且 `Zlinq` 还支持 `Span<T>` 使用 linq 语法。

4、[ImageGlass](https://github.com/d2phap/ImageGlass)

ImageGlass 是一款轻量级、开源的图片查看器，专为 Windows 系统设计，旨在提供快速、简洁且直观的图像浏览体验。支持超过 80 种常见图像格式，包括 WebP、GIF、SVG、PNG、HEIC 等，适用于日常图像浏览和设计工作流程。

核心特性

- **高兼容性**：支持多种图像格式，如 WebP、GIF、SVG、PNG、JXL、HEIC 等。
- **现代化界面**：提供干净、直观的用户界面，支持自定义主题。
- **轻量级设计**：启动迅速，占用系统资源少。
- **扩展支持**：支持插件和主题包，用户可根据需求进行扩展。
- **幻灯片功能**：支持图片幻灯片播放，提供平滑过渡效果。
- **EXIF 信息查看**：内置 EXIF 元数据查看工具，方便查看图片属性。
- **批量操作**：支持批量重命名、调整大小等功能，提高工作效率。

5、[CacheCow](https://github.com/aliostad/CacheCow)

CacheCow 是一个用于 ASP.NET Core 和 ASP.NET Web API 的 HTTP 缓存中间件库，符合 HTTP Caching 标准（RFC 7232/7234）。该项目旨在提升 Web API 的性能和效率，通过智能地利用客户端缓存与服务器端缓存机制，减少不必要的数据传输和计算开销。

**核心功能**

- ✅ 支持 ETag 和 Last-Modified 条件请求处理
- ✅ 自动生成缓存相关的 HTTP 响应头（如 `Cache-Control`, `ETag`, `Last-Modified`）
- ✅ 支持内存缓存（In-Memory）、SQL Server 和 Redis 缓存提供程序
- ✅ 可扩展的缓存策略机制（支持自定义缓存策略）
- ✅ 与 ASP.NET Core 中间件管道无缝集成

**组件模块**

- `CacheCow.Server`: 提供服务器端缓存处理器，用于 ASP.NET Core 和 Web API。
- `CacheCow.Client`: 提供客户端缓存代理，可缓存 GET 请求结果。
- `CacheCow.Common`: 定义通用接口和实体类，如 `ICacheStore`、缓存键、缓存策略等。
- `CacheCow.RedisCacheStore`: Redis 缓存提供支持。
- `CacheCow.SqlServerCacheStore`: SQL Server 缓存实现。

**使用场景**

- 减少频繁访问的数据接口压力
- 降低响应延迟、提升系统吞吐能力
- 利用标准 HTTP 缓存机制，无需客户端改动即可提高缓存命中率

6、[Chell](https://github.com/mayuki/Chell)

Chell 是一个为 .NET 应用程序提供类 Shell 脚本体验的库和执行工具，灵感来自 [google/zx](https://github.com/google/zx)。

✨ 主要特点

- 类 Shell 脚本体验：使用 C# 编写脚本，享受类似 bash 或 cmd 的体验。
- 自动转义和数组展开：处理命令行参数时自动进行转义和数组展开。
- 流和进程管道：支持将进程的标准输入/输出连接到其他进程或流。
- 实用的脚本工具和快捷方式：提供如 Cd、Echo、Sleep 等方法，简化脚本编写。
- 跨平台支持：兼容 Windows、Linux 和 macOS。
- LINQPad 友好：可在 LINQPad 中方便地使用。

7、[DotNetCore.SKIT.FlurlHttpClient.Wechat](https://github.com/fudiwei/DotNetCore.SKIT.FlurlHttpClient.Wechat)

- 作者很专注在频繁维护这个库，社区里也很低调
- 几乎涵盖了所有的微信API，不像某些库只支持常用的API。
- 文档完整，且免费（不像某些库必须买书、买课）
- 基于Flurl.Http实现，不存在实例化HttpClient导致的Socket泄漏问题
- 支持异步（Async）调用
- 支持 System.Text.Json（默认）和 Newtonsoft.Json两种序列化方法