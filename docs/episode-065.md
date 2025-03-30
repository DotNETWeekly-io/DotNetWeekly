# .NET 每周分享第 65 期

## 卷首语

![Image](https://github.com/user-attachments/assets/82fecc55-41a4-4bb6-aad3-fb5668b0a96e)

最近，`Typescript` 项目的负责人 Anders Hejlsberg 宣布了 `Typescript` 的编译器将会使用 `Go` 重写，其中最大的原因是能够获得 10 倍性能提升，该消息在社区引起了广泛的讨论，其中很多人认为 Anders Hejlsberg 是 `C#` 的发明人，为什么不使用 `C#` 来重写呢？

## 行业资讯

1、[TypeScript被迁移到Go，专访Anders Hejlsberg](https://www.youtube.com/watch?v=10qowKUW82U)

![Image](https://github.com/user-attachments/assets/7f8dac17-508c-49d4-8c1d-c1800295f658)

最近 `TypeScript` 圈子最大的新闻是微软宣布 `Typescript` 的编译器将会由 `Go` 实现，从而实现了 10 倍性能的提升。这里有 `Anders Hejlsberg` 专访，解答了很多问题，比如

- 为什么有这个项目
- 为什么不是 `Rust` 或者 `C#`
- Go 的并发和原生的特性对内存和性能方面的提升
- 除了编译器之外，还有什么其他变化
- 现有的项目怎么并存
- 其他的生态工具有什么计划

## 文章推荐

1、[为什么 TraceBit 选择 C#](https://tracebit.com/blog/why-tracebit-is-written-in-c-sharp)

![Image](https://github.com/user-attachments/assets/8fc60f2c-d5b8-4f8c-883b-4e6f670dbefe)

Tracebit 是一家做安全的初创企业，其 CTO 发布了一篇文章，分析了为什么他们选择 C# 作为他们的技术栈

1. 生产力
2. 免费和开源
3. 跨平台
4. 流行度
5. 内存安全
6. 垃圾回收
7. 静态类型
8. 稳定性
9. 社区生态
10. 工具链
11. 性能
12. 功能性

2、[使用 C# 编写基于 Deepseek R1 模型的应用程序](https://devblogs.microsoft.com/dotnet/start-building-an-intelligent-app-with-dotnet-and-deep-seek/)

目前 `DeepSeek` 是 AI 社区的热门词条，因为它是一个媲美 `ChatGPT 4o` 的开源大语言模型，现在 Azure AI 平台上可以部署这个 R1 模型。这篇文章介绍了如何使用 `C#` 开发基于 `DeepSeek` 模型的对话程序。

1. 在 Azure AI Foundry 上部署 `DeepSeek R1` 模型，获取 `Endpoint` 和 `Key`
2. 创建 `C#` 应用程序，并且安装 `Microsoft.Extensions.AI.AzureAIInference` 安装包
3. 编写对话应用程序

```csharp
using System;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;

var endpoint = new Uri("https://DeepSeek-R1-jhinm.eastus2.models.ai.azure.com");
string key = Environment.GetEnvironmentVariable("AZURE_CHAT_KEY") ?? throw new InvalidOperationException("Missing environment variable 'AZURE_CHAT_KEY'");
IChatClient client = new ChatCompletionsClient(endpoint, new Azure.AzureKeyCredential(key)).AsChatClient("DeepSeek-R1");

var response = await client.CompleteAsync("What is the meaning of life, the universe, and everything?");
Console.WriteLine(response.Message);
```

3、[.NET 迁移的隐形成本](https://tuxcare.com/blog/the-hidden-costs-of-framework-migration-moving-from-net-6-to-8/)

`.NET` 团队每年都会发布新的版本，而且每个偶数版本是 LTS，每个版本的生命周期是三年。所以当一个正在使用版本超出的生命周期，我们就需要升级我们代码的运行时。但是升级并不是无痛的，有些隐藏成本是我们没有注意的。

1. 依赖问题：我们的应用程序有很多其他库的依赖，如果相关的库并没有升级新的版本，或者和新的版本不兼容，那么就会带来迁移成本
2. 运行时本身问题：比如版本冲突，MSBuild 任务修改，特定框架的修改等等
3. 企业级运用程序：对于企业级应用程序，稳定是非常重要的，修改一行代码仅仅是为了升级运行时而导致问题是不可接受的
4. 现实世界的挑战：授权认证修改，依赖注入修改，配置和host 修改

那么我们该如果做好 `.NET` 升级呢?

1. 做好依赖项目审计
2. 做好临时混合运行时，逐步升级版本
3. 做好抽象层，方便后续升级
4. 记录不兼容修改

4、[.NET基金会的未来](https://aaronstannard.com/future-of-dotnet-foundation/)

作者指出，随着 .NET Core 的成功，.NET 从一个微软主导的闭源平台转型为一个开源、跨平台的技术栈，逐渐吸引了大量开发者的关注与参与。文章强调，.NET 基金会的目标是推动 .NET 技术在全球范围内的普及，增强其在开源社区中的影响力，并通过构建一个更加开放和包容的生态系统，鼓励更多的开发者参与到项目中。

尽管 .NET 基金会已经取得了显著的进展，但作者认为仍然面临一些挑战，包括如何进一步加强社区治理、促进项目间的合作，以及优化开发者对贡献的体验。文章提到，随着 .NET 的发展，基金会不仅需要继续扩大其技术的影响力，还需加大对开发者的支持和培训，提升其参与度。

此外，文章还探讨了 .NET 基金会如何应对新兴的开源趋势，以及如何与其他开源技术生态体系合作，确保 .NET 技术的长期可持续发展。总之，作者 强调了 .NET 基金会在开源社区中的重要作用，并对其未来充满信心，认为基金会将继续推动技术创新、提高开发者参与度，推动开源文化的蓬勃发展。

5、[.NET 9 中网络性能提升](https://devblogs.microsoft.com/dotnet/dotnet-9-networking-improvements/)

在 .NET 9 中，网络功能得到显著提升，重点体现在 HTTP/3 支持、多连接能力、Windows 代理自动更新以及社区贡献等方面。
首先，.NET 9 引入了对 HTTP/3 多连接的支持。默认情况下，HTTP/3 使用单连接模型，可能在高并发时成为性能瓶颈。新版本允许启用多个 HTTP/3 连接（通过设置 SocketsHttpHandler.EnableMultipleHttp3Connections = true），以支持大规模并发请求。在微软的性能测试中，开启此特性后，在处理 10,000 并行请求时的每秒请求数（RPS）从约 23,000 提升到接近 289,000，提升非常显著，特别适合微服务、后端服务间通信等高负载场景。
其次，Windows 平台上的 .NET 9 现已支持系统代理设置的动态更新。此前，当系统代理发生更改时，.NET 应用需要重启才能生效。而现在，HttpClient 会自动检测代理变动并立即应用，有利于在企业环境下部署和维护应用。
此外，.NET 9 网络栈的多个改进来自社区贡献，包括性能优化、错误修复等，这进一步增强了 .NET 开发的开放性和协作性。
除了以上重点，.NET 9 还对 HttpClientFactory 和与旧版 .NET Framework 的兼容性进行了改进，增强了开发灵活性和兼容能力。
总之，.NET 9 的网络改进将显著提升应用在高并发、高性能和动态网络环境下的适应力，为构建现代化、高效的网络应用提供了更坚实的基础。

7、[.NET9中如何优化内存分配](https://medium.com/@anderson.buenogod/optimizing-memory-allocations-in-net-9-0ff0d54a3e9a)

这篇文章主要探讨了在 .NET 9 环境下如何有效优化内存分配，以提升应用程序的性能与稳定性。作者首先指出，过多的内存分配会导致频繁的垃圾回收，进而造成延迟和吞吐量下降，因此减少不必要的分配对于关键业务系统尤为重要。

文中重点强调了几种常见的内存分配场景以及如何避免：其一，频繁实例化大对象容易触发高代数垃圾回收，作者建议尽量复用对象或使用内存池来减少重复创建；其二，对于小型对象或字符串，作者推荐利用构造方法或其他更轻量的方式来合并分配；其三，文中强调了对不可变（immutable）类型与结构（struct）的正确使用能在一定程度上优化内存消耗。

作者还介绍了 .NET 9 在垃圾回收和运行时层面的改进，如更多针对服务器场景的 GC 调优选项，使应用在高负载下依旧能保持较好的内存管理效率。此外，文中提及的一些工具和方法，如使用 BenchmarkDotNet 等性能分析工具、搭配 Visual Studio Diagnostics 以及内存探查器对分配情况进行可视化跟踪，也有助于开发者更好地定位热点问题并验证优化效果。

最后，作者总结道，内存优化并非一劳永逸，需要结合业务特点与运行环境做有针对性的改进。尤其是在高流量或实时性要求较高的应用中，减少每一次请求或处理环节中的冗余分配，对整体性能而言至关重要。通过正确应用 .NET 9 的新特性和一系列最佳实践，开发者可以在满足业务需求的同时最大限度地减少资源消耗，提升应用的稳定性与可扩展性。

8、[2025 年度 .NET 学习路线](https://github.com/milanm/DotNet-Developer-Roadmap)

2025 年度`.NET`学习路线图和细节。该路线图是一个开源项目，包含了 `.NET` 开发者需要掌握的技能和知识点。它分为多个模块，包括基础知识、前端开发、后端开发、数据库、云计算等。每个模块下又细分为多个子模块，涵盖了具体的技术和工具。

## 视频推荐

1、[为什么以及如何定制.NET GC](https://www.youtube.com/watch?v=om8YFyTO5ik)

**.NET 与 JVM GC 的差异**

- Java 提供多种 GC 策略和高度可配置参数，.NET 则采取简洁高层的设计。
- Java 用户习惯使用复杂参数配置，而 .NET 则强调自动调优，减少“cargo cult”式配置。

**当前 .NET GC 模式**

- .NET 提供四种 GC 模式（Server/Workstation × Concurrent/Non-Concurrent）。
- 所有 GC 模式代码集中在一个庞大的 gc.cpp 文件中（超过 40K 行）。

**CLR Hosting 与 GC 局限性**

- 通过 CLR Hosting 可部分影响内存分配，但无法改变 GC 行为本身。
- 可定制的唯一“突破口”是通过 VirtualAlloc 等替代方法影响内存分配策略。

**Local GC（自定义 GC）**

- 自 .NET Core 2.0 起支持替换默认 GC，实现自定义逻辑。
- 通过环境变量指向自定义 GC DLL，即可替代默认实现。

**实践示例：Zero GC**

- 演讲者实现了一个“Zero GC”（仅分配不回收）的示例。
- 展示了如何使用 C++ 实现基本的接口（IGCHeap、IGCHandleManager 等）。
- 提供了完整的手工内存管理和 handle 存储方式，并通过简单的 WebAPI 应用进行了压力测试。

2、[Dictionary 中的避免两次查询操作](https://www.youtube.com/watch?v=8dI_nsmcW-4&ab_channel=NickChapsas)

问题背景：在常见操作如 `ContainsKey` + 添加值时，Dictionary 会重复进行哈希计算，影响性能。
	
**常规方法缺陷：**

- 多次访问同一 Dictionary，增加 CPU 负担。
- TryGetValue + 赋值 也存在重复查找问题。

**优化方法:**

- 使用 .NET 的 CollectionsMarshal.GetValueRefOrAddDefault 方法。
- 可通过“引用”方式直接获取 Dictionary 中的值或添加默认值，只访问一次 Dictionary，性能更高。

**进阶扩展**

- 自定义 GetOrAdd 和 TryUpdate 扩展方法，实现类似 ConcurrentDictionary 的功能。
- 使用 Unsafe.IsNullRef 检查引用是否为空，确保类型安全。

**实用建议**

- 在高频调用场景（hot path）中使用此技巧可提升整体性能。
- 小心使用 unsafe 方法，避免引发程序不稳定。

3、[.NET9 中的 Blazor 介绍](https://www.youtube.com/watch?v=MPFFLMautHw)

本视频是由 DotNetMastery 的 Bhrugen Patel 主讲的 Blazor 入门课程的第一部分，面向初学者，主要介绍了如何使用 Visual Studio 创建 Blazor Server 项目，并深入讲解了 Blazor 的基本概念和数据绑定。

Blazor 简介：Blazor 是 .NET 的前端框架，可使用 C# 替代 JavaScript 开发 Web 应用。

- 项目创建：使用 Visual Studio 创建 Blazor Web App，选择 Server 渲染和全局交互模式。
- 组件与路由：创建 Razor 组件、定义路由、添加导航链接。
- 数据绑定：演示了一元绑定（@product.Name）和双向绑定（@bind）方式。
- 交互优化：通过 @bind:event="oninput" 实现实时更新。
- 练习与实践：讲解如何用表单输入、checkbox、dropdown 实现交互，并设置小作业练习。

4、[.NET 垃圾收集器的过去、现在和未来](https://www.youtube.com/watch?v=ki0jIyh0VHc)

本视频深入访谈 .NET 垃圾回收器（GC）的设计者 Patrick Dussud，讲述 GC 的作用、演变及背后的技术哲学。

- GC 基本职责：自动内存管理，识别无引用对象并回收，避免 C++ 中手动释放带来的复杂性和错误。
- GC 优势：通过智能压缩（compaction）实现比手动释放更高效的内存利用，提升缓存命中率。
- 引用图遍历机制：GC 通过“根集”开始（局部变量、静态字段等），递归标记所有可达对象。
- Finalizer 问题：析构顺序不可控，可能导致依赖关系断裂。推荐使用显式 Close() 替代隐式析构。
- GC 性能优化：通过观察应用运行时的行为动态调整策略，例如代际回收（Gen0/Gen2）、碎片检测、并发回收。
- 历史回顾：从 VBScript、JScript 到 JVM 再到 CLR，Patrick 一步步构建了微软的 GC 技术。
- 未来方向：降低延迟、提升并发回收效率，特别针对服务器大内存场景。

## 开源项目

1、[ErrorProne.NET](https://github.com/SergeyTeplyakov/ErrorProne.NET/)

`ErrorPront.NET` 是谷歌的 `error-prone` 的项目的衍生品，只不过之前的项目是基于 Java 语言的。 C# 代码可以很容易的通过 `Roslyn` 获得项目的语法树，这样可以很方便的在编译的时候进行错误分析。主要包含如下

- Null 引用

```csharp
public class Result
{
    public bool Success = false;
}

public static Result ProcessRequest() => null;

// Result of type 'Result' should be observed
ProcessRequest();
//~~~~~~~~~~~~
```

- 可疑相等判断

```csharp
public class FooBar : IEquatable<FooBar>
{
    private string _s;

    // Suspicious equality implementation: parameter 'other' is never used
    public bool Equals(FooBar other)
    //                        ~~~~~
    {
        return _s == "42";
    }
}
```

- 异常处理

```csharp
try
{
    Console.WriteLine();
}
catch (Exception e)
{
    // Suspicious exception handling: only e.Message is observed in exception block
    Console.WriteLine(e.Message);
    //                  ~~~~~~~
}

try
{
    Console.WriteLine();
}
catch (Exception e)
{
    // Exit point 'return' swallows an unobserved exception
    return;
//  ~~~~~~
}
try
{
    Console.WriteLine();
}
catch (Exception e)
{
    // Incorrect exception propagation: use 'throw' instead
    throw e;
    //    ~
}
```

- 异步 `ConfigureAwait` 

```csharp
[assembly: UseConfigureAwaitFalse()]
public class UseConfigureAwaitFalseAttribute : System.Attribute { }

public static async Task CopyTo(Stream source, Stream destination)
{
    // ConfigureAwait(false) must be used
    await source.CopyToAsync(destination);
    //    ~~~~~~~~~~~~~~~~~~
}
```

2、[HybridCache](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/hybrid?view=aspnetcore-9.0)

.NET 9 中将会推出 `HybridCache` 将会统一现有的缓存接口，目前 `.NET` 包含了 `IMemoryCache` 和 `IDistributedCache` 两个接口，但是有一些问题

1. 如果多个请求都出现了缓存 miss 情况，那么就会导致调用方承受很大的压力
2. 当数据源发生更改，会出现数据源和缓存不一致情况

`HybridCache` 统一了缓存访问的接口，通常只有一个重要方法 `GetOrCreateAsync`, 它接受一个 `Key` 和工厂方法获取这个 `Value`。它包含了两级缓存：

- 主缓存： IMemoryCache 实现
- 次缓存：IDistributedCache 实现

在获取缓存对象的时候，首先调用主缓存，如果 miss 就访问次缓存，如果还是 miss， 就调用工厂方法构建一个对象，并且更新对象到主和次缓存中。

```csharp
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddHybridCache();
services.AddTransient<AppService>();
var provider = services.BuildServiceProvider();
var appService = provider.GetRequiredService<AppService>();
Parallel.For(0, 10, (i) =>
{
    var result = appService.GetValue("World");
});

Console.ReadKey();

public class AppService
{
    private readonly HybridCache _hybridCache;

    public AppService(HybridCache hybridCache)
    {
        _hybridCache = hybridCache;
    }

    public ValueTask<string> GetValue(string name)
    {
        return _hybridCache.GetOrCreateAsync<string>(name, (token) =>
        {
            return ValueTask.FromResult(ExpensiveCall(name));
        });
    }

    private static string ExpensiveCall(string name)
    {
        Console.WriteLine("Retriving");
        Thread.Sleep(1000*Random.Shared.Next(0, 1));
        return "Hello " + name;
    }
}
```

2、[NetArchTest](https://github.com/BenMorris/NetArchTest)

NetArchTest 是 Java 生态的中的 `ArchUnit` 的 `C#` 版本， 它主要通过单元测试的方式验证整个项目架构是否满足要求，至少是从代码层面。比如代码是否依赖某个外部库，否则某些接口的实现是满足要求。当然也可以自己定义规则

```csharp
[TestMethod]
public void TestMethod1()
{
    var result = Types.InCurrentDomain()
        .That()
        .ResideInNamespace("NetArchTestDemo")
        .ShouldNot()
        .HaveDependencyOn("Newtonsoft.Json")
        .GetResult()
        .IsSuccessful;
    Assert.IsTrue(result);
}

[TestMethod]
public void MyTestMethod()
{
    var result = Types.InCurrentDomain()
        .That().ImplementInterface(typeof(IWidget))
        .Should().BeSealed()
        .GetResult()
        .IsSuccessful;
    Assert.IsTrue(result);
}
```

3、[InterpolatedParser](https://github.com/AntonBergaker/InterpolatedParser)

InterpolatedParser  是一个非常有意思的开源项目，它是 `C#` 字符串插值的反操作。这个非常方便我们在日志文本找到我们想要的内容，举个例子,  如果我们的字符串是 `Size is 69`，那么如何在字符串中找到 `69` 这个值呢？如果我们只是对字符串操作，非常繁琐，那么使用这个库就非常简单

```csharp
string input = "size is 69";
int x = 0;
InterpolatedParser.Parse(input, $"size is {x}");
Console.WriteLine(x);
```

这样我们就很方便的得到 `69` 数值。

如果我们的字符串有复杂的模式，该库也能处理：

```csharp
List<int> numbers = null!;

InterpolatedParser.Parse(
    "Winning numbers are: 5,10,15,25",
   $"Winning numbers are: {numbers:,}");
Console.WriteLine(string.Join(";", numbers));
```