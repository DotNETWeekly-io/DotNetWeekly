# .NET 每周分享第 69 期

## 卷首语

![Image](https://github.com/user-attachments/assets/0e5db2b6-40d0-4689-84e5-106edf1b1ea2)

最近`.NET`社区的博主 Nick Chapsas 分享了[100 个你需要了解的 .NET 相关知识点](https://www.youtube.com/watch?v=8F-Pb-SKO5g)视频，内容非常丰富，值得每个 `.NET` 开发者去了解。

## 行业资讯

1、[MS Build 2025 汇总](https://devblogs.microsoft.com/dotnet/catching-up-on-microsoft-build-2025-essential-sessions-for-dotnet-developers/)

![Image](https://github.com/user-attachments/assets/602b2b18-bdc4-46a0-9b9b-a88c27f09ba7)

2025 年 MS Build 大会已经结束，这里是这次大会的简单终结

**✨ .NET + AI = Amazing** 
微软全力推进 AI 与 .NET 的融合，开发体验日益成熟： 

- **Microsoft.Extensions.AI** 正式发布  
- **Semantic Kernel** 和 **AI Agents** 支持 LLM 集成

**🔌 .NET Aspire 准备就绪** 
云原生应用栈从预览走向生产可用：

- 内建**可观察性**、**容错模式**、**一键部署**  
- 支持微服务、分布式系统开发落地

**🚀 开发者体验再升级**

- **C# 14** 引入多项语法改进  
- 支持 **无需项目文件直接运行 C# 脚本** (`dotnet run app.cs`)  
- 工具链全面集成 AI，提升开发乐趣与效率

**🔄 现代化更轻松**
**Copilot for .NET Upgrades** 帮助开发者自动分析、规划、执行老旧项目升级：  

- 从 .NET Framework/旧版本无缝迁移至现代 .NET  
- 全流程 AI 辅助，Satya Nadella 在主题演讲中重点介绍

> 📎 结语：Build 2025 展现了一个更智能、更现代、更轻盈的 .NET 世界，开发者迎来真正的 AI 时代。

## 文章推荐

1、[第四种依赖注入生命周期](https://andrewlock.net/creating-a-pooled-dependency-injection-lifetime/)

ASP.NET Core 默认提供三种依赖注入生命周期（`Transient`, `Scoped`, `Singleton`）。在某些情况下，比如对象创建成本高但线程不安全时，希望能实现“对象池”模式（Object Pooling）来优化性能。

首先定义一个 `IResttableService` 的接口，它包含了 `Reset` 方法，用来在放回对象池的时候重置服务。

```csharp
public interface IResettableService
{
    void Reset();
}
```

在定义一个 `IPoolServiced` 用来获取对象池化的实例对象，这是用户应用程序直接访问的接口

```csharp
public interface IPooledService<out T>
    where T : IResettableService
{
    T Value { get; }
}
```

接下来是 `DependenyPool<T>` 类，这是用来控制对象池行为逻辑。

```csharp
internal class DependencyPool<T>(IServiceProvider provider) : IDisposable
    where T : IResettableService
{
    private int _count = 0; // The number of instances in the pool
    private int _maxPoolSize = 3; // TODO: Set via options
    private readonly ConcurrentQueue<T> _pool = new();
    private readonly Func<T> _factory = () => ActivatorUtilities.CreateInstance<T>(provider);

    public T Rent()
    {
        if (_pool.TryDequeue(out var service))
        {
            return service;
        }
        return _factory();
    }

    public void Return(T service)
    {
        if (Interlocked.Increment(ref _count) <= _maxPoolSize)
        {
            service.Reset();
            _pool.Enqueue(service);
        }
        else
        {
            Interlocked.Decrement(ref _count);
            (service as IDisposable)?.Dispose();
        }
    }

    public void Dispose()
    {
        _maxPoolSize = 0;

        while (_pool.TryDequeue(out var service))
        {
            (service as IDisposable)?.Dispose();
        }
    }
}
```

- `Rent` 方法用来获取对象，如果对象池中存在，直接返回，否则调用 `Factory` 对象，直接通过 `DI` 获取
- `Return` 方法用来将对象重新返回对象池，如果对象池已经满了，调用 `Dispose` 方法
- `Dispose` 方法将对象池的对象全部释放掉


那么 `PoolService` 对象实现是包含了 `DependencyPool` 实例

```csharp
internal class PooledService<T> : IPooledService<T>, IDisposable
    where T : IResettableService
{
    private readonly DependencyPool<T> _pool;

    public PooledService(DependencyPool<T> pool)
    {
        _pool = pool;
        Value = _pool.Rent();
    }

    public T Value { get; }

    void IDisposable.Dispose()
    {
        _pool.Return(Value);
    }
}
```

我们在调用 `Value` 对象的使用，直接使用 `DependencyPool` 的 `Rent` 方法，在 `Dispose` 方法中，调用 `Return` 方法尝试将对象放回线程池。

那么最后的方法将它封装一个一个 `IServiceCollection` 的扩展方法

```csharp
public static class PoolingExtensions
{
    public static IServiceCollection AddScopedPooling<T>(this IServiceCollection services)
        where T : class, IResettableService
    {
        services.TryAddSingleton<DependencyPool<T>>();
        services.TryAddScoped<IPooledService<T>, PooledService<T>>();

        return services;
    }
}
```

我们这里将 `PooledServie` 作为 Scope 类型注册到依赖注入的容器中。如果想要在生产环境中使用这种服务，可以选择 `Microsoft.Extensions.ObjectPool.DependencyInjection` 包来完成同样的工作


```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MyService>();
builder.Services.AddPooled<MyPooledClass>(options => options.Capacity = 64);

var app = builder.Build();
```

2、[IEnumerable 和 IAsyncEnumberable 对比](https://blog.elmah.io/ienumerable-vs-iasyncenumerable-in-net-streaming-vs-buffering/)

🧩 什么是 IEnumerable<T>
- 同步接口，使用 foreach 迭代。
- 通过 GetEnumerator() 返回枚举器，使用 MoveNext() 和 Current 进行迭代。
- 在处理大型或慢速数据源时会阻塞调用线程。
- 适用于 List<T>、Array 和 Dictionary<TKey, TValue> 等集合。
- 加载数据时会一次性将所有数据缓冲到内存中。

⚙️ 什么是 IAsyncEnumerable<T>
- 异步接口，使用 await foreach 逐项异步迭代。
- 引入于 .NET Core 3.0 和 C# 8.0，位于 System.Collections.Generic 命名空间中。
- 通过流式处理逐项加载数据，减少内存占用。
- 适用于数据库查询、文件流和网络响应等高延迟数据源。

🧪 实例：EF Core 中的对比测试
项目设置
- 使用 In-Memory 数据库创建控制台应用 IEnumIAsyncDemo。
- 模型 Book 包含 Id、Title 和 Author 属性。
- 使用 SeedData 类生成 100,000 条记录。

```csharp
Benchmark.Measure("IEnumerable (Buffered)", () =>
{
    var books = db.Books.ToList(); // 缓冲所有数据
    foreach (var book in books)
    {
        var temp = book.Title; // 模拟轻量处理
    }
});

await Benchmark.MeasureAsync("IAsyncEnumerable (Streamed)", async () =>
{
    await foreach (var book in db.Books.AsAsyncEnumerable())
    {
        var temp = book.Title; // 模拟轻量处理
    }
});
```

- 在处理 100,000 条记录时，IAsyncEnumerable 显示出更低的内存使用和更快的执行时间。
- 将数据量增加到 1,000,000 条记录后，IAsyncEnumerable 的内存优势更加明显。
- 由于使用的是 In-Memory 数据库，异步操作的优势主要体现在内存使用上。在实际场景中，如网络或数据库 I/O，IAsyncEnumerable 的非阻塞特性会带来更大的性能提升。

3、[写给.NET程序员们：你所不知道的Python社区](https://zhuanlan.zhihu.com/p/1915390980070309981)

作者以其在 Python 社区两年的经验（兼职）为例，探讨了 Python 在 AI 领域日益增长的影响力。

文章指出，即使是微软，在开发如 Autogen、Graphrag 等知名开源项目时，也选择使用 Python 和 Golang 而非 .NET。这表明大公司深知没有一种编程语言能够通吃所有市场，不同团队会根据项目需求中立选择最合适的语言，而非盲目追捧“亲儿子”技术。这并非意味着微软放弃 .NET，而是采取了多元化策略。

此外，作者列举了 NumPy、Pandas、Tensorflow 等一系列以“G”为单位下载量的著名 Python 数据科学库，强调了 Python 在数据分析、可视化及机器学习领域的广泛应用和强大生态系统。

对于 Python 性能受诟病的问题，文章认为，语言性能并非决定其市场地位的唯一因素。Python 之所以长期霸榜，在于其高度专注于数据科学等领域，而这些场景对性能要求不高，更注重分析和出图效率。

作者反思了 .NET 在机器学习领域（如 ML.NET）的尝试，并对比了 Semantic Kernel 和其 Python 竞品 LangChain 的差距。他建议 .NET 社区“知难而退”，专注于其擅长的工控、医疗、金融、航空等领域，而非盲目争夺数据科学或前端市场（如 Blazor），以免误导开发者学习“没有用的东西”。

## 视频推荐

1、[直面Blazor的未来](https://www.youtube.com/watch?v=2uLGXe95kTo)

Nick和Blazor的首席项目经理Dan Roth讨论了Blazor的未来和担忧。

主要观点：

- .NET发布节奏： 每年一次的发布节奏足以交付重要功能，且STS和LTS版本质量无差异，建议保持更新。
- Blazor现状与未来： Blazor每月有数十万活跃用户，且每年两位数增长。它是微软推荐的Web UI解决方案，没有停止投入的计划，并强调其基于开放Web标准，不会步Silverlight后尘。
- Blazor应用场景： 旨在解决Web应用开发中跨生态系统的负担，特别适合资源有限的团队，使.NET开发者能构建全栈应用。微软内部大量使用Blazor。
- Blazor局限性： 不适用于所有场景，如需数百万并发用户或对下载大小、性能高度优化的公共应用，JavaScript可能更优。Blazor WebAssembly有约1MB的额外下载开销，Blazor Server对服务器资源消耗较大。
- Blazor下一步（.NET 10）： 重点改进交互式服务器端渲染（状态持久化）、静态服务器端渲染、加载时间性能、WebAssembly诊断工具和安全（如Passkey）。
- 职业发展： JavaScript框架职位数量仍远超Blazor，但.NET作为长期投资仍是可靠选择。
- 个人选择： Dan Roth创业后端会选.NET，前端会根据应用类型从Blazor静态服务器端渲染开始。他最喜欢Python。

2、[Jeffery Richter教你如何设计Rest API](https://www.youtube.com/watch?v=9Ng00IlBCtw&list=PL9XzOCngAkqs4m0XdULJu_78nM3Ok3Q65&index=3)

Jeffery Richter 是 `.NET` 中著名的 `CLR via C#` 书的作者， 在视频列表中分享了他对设计 REST API 的经验和最佳实践。

3、[基于ASP.NET Core和Blazor的Web开发的未来](https://www.youtube.com/watch?v=l32fiBG9YeQ)

该视频由ASP.NET Core团队的产品经理Daniel Roth和Mike Kistler主讲，介绍了ASP.NET Core的现状、未来发展方向，以及它如何与.NET平台的新功能（特别是AI和云服务）集成。

**主要内容**

- ASP.NET Core的现状与重要性： 自2016年发布以来，ASP.NET Core已迅速发展，每月有超过200万开发者使用，并支持微软的许多大型服务，以其行业领先的性能著称。
- 与AI的集成： ASP.NET Core与.NET平台的新AI功能无缝集成，提供Microsoft Extensions AI库、Evaluations库、Vector Data、AI项目模板等，帮助开发者构建AI驱动的应用程序。
- .NET Aspire： 这是一个用于集成AI和云服务的工具，帮助构建、测试和部署云原生应用，提供OpenTelemetry、健康检查、HTTPS等功能，并能快速连接到Redis、Postgres SQL和AI服务 。

ASP.NET Core的未来（.NET 10的重点领域）：

- 安全性： 支持Passkey认证、OAuth 2刷新令牌自动刷新，并简化安全功能的使用。
- 应用可观测性和诊断： 增加Kestrel内存池、身份验证、授权和Blazor特定指标，并为Blazor WebAssembly添加诊断工具。
- 目标性能改进： 优化Kestrel内存池、JSON反序列化性能、反伪造令牌处理和Blazor WebAssembly应用的启动时间。
- 解决主要痛点和空白： 为Minimal APIs添加自动输入验证、支持服务器发送事件、改进OpenAPI生成、支持JSON Patch。Blazor方面将改进状态持久性、弹性、统一不同渲染模式行为、QuickGrid组件和JavaScript互操作性。

视频鼓励开发者试用.NET 10预览版并提供反馈，并预告.NET Conf 2025将于11月发布.NET 10。

4、[AI对.NET程序员的影响](https://www.youtube.com/watch?v=-aawQMmcbso)

Gavin Lon探讨了人工智能（AI）对.NET开发人员的影响，强调AI是长期趋势。

核心要点：

- AI提升生产力： GitHub Copilot等AI工具正改变代码编写方式，自动化重复任务，优化建议，并检测潜在错误 
- AI是工具非替代品： AI无法取代人类开发者，人类仍需理解需求、整合代码、修复错误和定制化
- AI在测试调试中的应用： AI能辅助测试和调试，预测故障并优化测试覆盖率。Azure DevOps中的AI驱动测试套件可优化测试执行、自动化和分析。
- AI在云和DevOps中的应用： 微软Azure整合AI以优化资源分配、安全监控和.NET应用自动化扩展
- 适应性与技能再利用： AI可能取代重复性高、判断要求低的岗位，但开发者需重新利用技能，持续学习以保持竞争力
- AI作为编码伙伴： AI旨在辅助而非取代开发者，人类仍负责架构设计、战略决策和确保软件满足实际需求
总而言之，AI正在重塑软件开发，带来效率提升和新的工作方式。开发者应拥抱AI，将其视为合作伙伴，并保持敏捷和适应性

5、[C# 泛型约束中的 notnull](https://www.youtube.com/watch?v=aMYKkR9UueY)

在 C# 泛型方法中，参数类型可以做一些限制，比如要求是 `class`, `struct` 或者是继承了某个接口，比如 `IComparable`。其实 C# 泛型参数的类型约束还支持 `notnull`, 例子如下

```csharp
static void InvokeNotNullable<T>(T t) where T : notnull
{
    Console.WriteLine("Interface: " + t);
}

string? nullableStr = null;
InvokeNotNullable(nullableStr);
```

这样的话，编译器会给出警告信息，因为发现传入的参数可能为 `null` 

```txt
warning CS8714: The type 'string?' cannot be used as type parameter 'T' in the generic type or method 'InvokeNotNullable<T>(T)'. Nullability of type argument 'string?' doesn't match 'notnull' constraint.
```

6、[C# 变得很酷](https://www.youtube.com/watch?v=zHNxbJeEaVM&ab_channel=Awesome)

这段视频探讨了 C# 语言和 .NET 生态系统在近年来的演变，指出它们已经从传统的企业级笨重形象转变为现代化、高效且有趣的开发工具。

主要内容包括：

- C# 语言的现代化： C# 已经加入了许多现代语言特性，如 Lambda 表达式、Async/Await、模式匹配、记录（Records）、顶级语句和可空类型等，同时保持了向后兼容性 。特别提到了记录（Records）简化了不可变数据结构，模式匹配使代码更清晰，可空引用类型在编译时捕获空引用错误。LINQ提供了类似 SQL 的集合查询功能 ，最小 API 允许用极少代码创建 HTTP API，源生成器可以在编译时生成代码。

- .NET 平台的演进： 从 Windows 专属的 .NET Framework 发展到跨平台、开源和模块化的 .NET Core，现在简称为 .NET。它可以在 Windows、macOS、Linux、Docker 和云环境运行，性能卓越 。平台模块化且统一，只有一个 SDK 用于构建各种应用类型，包括 API、桌面应用、移动应用、云函数和游戏。集成了 ASP.NET Core、Blazor、.NET MAUI、Entity Framework Core、gRPC 和内置依赖注入。提供了一流的 CLI 工具，并支持 Visual Studio Code 等编辑器。整个平台在 .NET Foundation 的支持下是开源的。

7、[当Python遇见.NET：同时使用这两种语言构建AI解决方案](https://www.youtube.com/watch?v=fDbCqalegNU)

![Image](https://github.com/user-attachments/assets/cd39898c-f23f-4ba2-92d6-5f0b75d731f2)

如何结合 Python 和 .NET 的优势来构建 AI 解决方案。

核心观点：

- 结合 Python 与 .NET： 视频强调应同时利用 Python 在数据科学、机器学习和 AI 领域的庞大库生态系统，以及 .NET 在企业支持、速度和应用程序支持方面的优势。
- Sea Snakes 项目： Anthony Shaw 团队开发了 Sea Snakes 项目，允许将 Python 无缝嵌入 .NET 进程，实现 Python 线程和内存与 .NET 共享，并直接导入 Python 模块。这避免了耗时的跨进程通信。

Sea Snakes 的优势：

- 简化开发： 通过源生成器，Python 函数可直接映射到 .NET 方法，无需手动桥接代码。
- 性能提升： 避免了数据编码、序列化和反序列化的开销，尤其在处理 AI/ML 大量浮点数矩阵时，能高效内存共享。
- 易于部署和依赖管理： 提供扩展方法自动下载配置 Python 环境，并支持从 requirements.txt 安装依赖 [[23:43]。
- 透明集成： .NET 开发者无需了解 Python 即可使用其功能。
- 视频通过演示展示了 Sea Snakes 如何在 .NET 应用中调用 Python 函数、管理依赖，并集成 Hugging Face 上的 AI 模型进行对象检测。
项目状态与展望：
Sea Snakes 是一个开源项目，目前处于开发阶段，旨在确保与 Python 和 .NET 生态系统的兼容性和性能。它为 .NET 开发者打开了访问庞大 Python AI 生态系统的大门

## 开源项目

1、[AspNet.Security.OAuth.Providers](https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers)

AspNet.Security.OAuth.Providers 是一个开源库，为 ASP.NET Core 提供了大量的 OAuth 2.0 和 OpenID Connect 身份验证提供程序（authentication providers）。这个库极大地扩展了 ASP.NET Core 默认支持的身份验证选项，让你的应用程序能够轻松地集成各种第三方服务进行用户登录和数据访问。

主要特点和用途

这个库的主要目的是让开发者能够方便地将应用程序与多种社交媒体、云服务和其他平台的账户关联起来。例如，如果你想让用户通过他们的 GitHub、LinkedIn、Twitter、Salesforce 甚至像 Battle.net 这样的游戏平台账户登录你的 ASP.NET Core 应用，AspNet.Security.OAuth.Providers 就能帮你实现。

它通过提供 即插即用（plug-and-play） 的 NuGet 包，简化了配置过程。每个提供程序都封装了与特定服务进行 OAuth/OpenID Connect 握手和令牌交换所需的复杂逻辑，你只需在 Startup.cs 或 Program.cs 中添加几行代码即可启用。

工作原理
当你使用这个库中的一个提供程序时，它会作为 ASP.NET Core 身份验证中间件的一部分运行。当用户尝试通过某个第三方服务登录时，流程大致如下：

- 重定向：应用程序将用户重定向到第三方服务的登录页面。
- 授权：用户在该服务上授权你的应用程序访问其信息。
- 回调：第三方服务将用户重定向回你的应用程序，并带上一个授权码。
- 令牌交换：你的应用程序使用授权码与第三方服务交换访问令牌（access token）和/或 ID 令牌（ID token）。
- 用户身份识别：应用程序使用这些令牌获取用户的基本信息（如用户ID、姓名、邮箱等），并为用户创建或查找本地身份。

为什么选择它？

- 丰富多样的提供程序：支持数百种常见的和利基的第三方服务。
- 简化集成：大大减少了手动配置 OAuth/OpenID Connect 的复杂性。
- 开源与社区支持：作为一个活跃的开源项目，它有持续的维护和社区贡献。
- 与 ASP.NET Core 无缝集成：遵循 ASP.NET Core 的身份验证模型和约定。

总之，如果你正在开发一个需要与各种第三方服务进行身份验证的 ASP.NET Core 应用程序，AspNet.Security.OAuth.Providers 库将是一个非常有价值的工具，它能帮助你节省大量开发时间并降低集成难度。

2、[NetPad](https://github.com/tareqimbasher/NetPad)

![Image](https://github.com/user-attachments/assets/d1f0e760-bd4b-485e-9771-0237c92437c6)

NetPad 是一个跨平台的 C# 在线编辑器与“代码操场”（playground），无需项目配置即可即时编写并运行 C# 代码，它旨在成为非 Windows 平台上类似 LINQPad 的轻量、可在线运行且功能丰富的工具。


主要功能
- 即时运行：打开应用，写代码，点击“Run”即可查看结果
- 快速原型与测试：适合实验、学习新特性等用途
- 数据可视化：支持在脚本运行后以交互方式展示数据 
- 数据库查询：可通过 LINQ 或 SQL 查询数据库，并查看结构
- 自定义实用脚本：可编写并保存脚本用于日常任务自动化 
- UI 主题与样式自定义：基于 Monaco 编辑器，可配置主题和特定 CSS 样式
- 内置 Util 提供工具函式：包括缓存、Dump、打开文件/URL 等辅助功能

平台与安装
- 支持 Windows、macOS 和 Linux（包括 Linux 的 .deb、snap、pacman 和 rpm 包）
- 要求安装 .NET SDK 6 或更高版本，如需数据库功能，还需额外的 EF Core CLI 工具 
- 有 Electron 和更轻量的 “native shell（vNext）” 两种发行版本