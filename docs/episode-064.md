# .NET 每周分享第 64 期

## 卷首语

![image](https://github.com/user-attachments/assets/05b61728-0fe6-43aa-9bc1-4f8d7ae586d0)

2024 年度 `.NET` 官方博客上最热门的[文章](https://devblogs.microsoft.com/dotnet/top-dotnet-blogs-posts-of-2024/)内容如下：

- .NET 9 发布： 每一年 `.NET` 新的版本发布都会引起大量的关注。今年也不例外，包含了 Runtime，库， SDK， C# 13， ASP.NET Core 等等其他内容的更新。
- .NET 9 性能提升：只从 `.NET Core 2.0` 开始，每一年的 `.NET` 发布都会包含性能上提升。今年也不例外。当然也可以查看所有的性能提升文章来了解历史。
- .NET Aspire: Aspire 是简化云原生的 `.NET` 项目，在 `.NET 9` 中也不例外。
- .NET Smart Components: AI 是这两年的热门词，`.NET` 推出了智能组件，拥抱 AI 时代浪潮
- C# 12 博客系列：C# 12 包含了很多新的内容，比如主构造函数，集合表达式等等
- 更多 AI 内容： 比如 `Microsoft.Extensions.AI`, `Github Copliot` 等等
- 其他开发组件：比如 MAUI，Blazor, OpenAPI 等等

## 行业资讯

1、[FluentAssertions修改为商业版权](https://xceed.com/products/unit-testing/fluent-assertions/)

![Image](https://github.com/user-attachments/assets/4089941c-fdf3-4a8f-89fb-d4b92cd52cd8)

.NET 社区广泛使用的单元测试工具 FluentAssertions 近期宣布，从 V8.0 版本起将采用商业许可模式：新版本代码在开源协议（MIT）的基础上新增商业许可要求。 这一调整意味着开发者若需使用 V8.0+ 版本，需为 每位用户 支付 129 美元 的商业授权费用（个人项目或非商业用途仍可免费使用）。

**社区反应与争议**

反对声音：许多开发者对此次变更表达了不满，认为开源项目突然转向商业化的做法破坏了社区信任。部分用户指出，此前贡献的代码可能被用于商业闭源版本，存在伦理争议。

官方解释：项目维护者表示，商业许可是为了确保项目可持续发展，资金将用于支持核心团队开发和维护。

**开发者应对方案**

- 维持现有版本：可继续免费使用 V8.0 之前的 MIT 协议版本（如 7.x），但需放弃新功能与后续更新。

- 切换替代方案：

1. 原生断言库（如 Assert）：无需依赖第三方库，但语法简洁性和可读性较差。

2. Shouldly：语法与 FluentAssertions 高度相似，且保持 MIT 协议，迁移成本较低。

3. 其他选项：如 NFluent、Verify（侧重快照测试）等。

4. 购买商业许可：适合依赖新功能且预算充足的团队。

## 文章推荐

1、[微软正在伤害开源社区？](https://www.youtube.com/watch?v=PiT-441KR3s&ab_channel=NickChapsas)

这个视频中，作者提出一个了观察到的现象，很多 `.NET` 社区中著名的开源库逐步被微软开发的替代品作为 `.NET` 的一部分，比如

- Nancy -> Minimal API
- SwashBuckle.AspNETCore  -> OpenAPI
- Newtonsoft.JSON -> System.Text.JSON
- Autofac -> Microsoft.Extensions.DependencyInjection
- ...

这个行为给社区的人感觉是 Microsoft 会 “抄袭” 社区的开发者，然后推出替换他们的库，最终这些开发者的开源库就消失了，你怎么看这件事？

2、[Yarp 介绍](https://www.youtube.com/watch?v=gJqJ1yg5FoA)

本视频介绍了YARP（Yet Another Reverse Proxy）在 ASP.NET Core 中的应用。作者首先讲解了反向代理的作用，如负载均衡、流量分发、提升安全性等。然后，他演示了如何在 ASP.NET Core 中快速集成 YARP，包括安装 NuGet 包、配置路由、添加目标服务器等。视频中还展示了如何动态调整 YARP 配置，实现流量自动分配，以及如何用 YARP 帮助网站升级，使新旧版本共存。最后，作者强调 YARP 的灵活性，可以自定义规则、处理API请求、增强安全等，是.NET开发者的重要工具。

3、[什么是 .NET Vector](https://brandewinder.com/2024/09/01/should-i-use-simd-vectors/)

![image](https://github.com/user-attachments/assets/5ce81db4-8170-4c93-93e7-4d0fc01e214d)

`.NET` 中的 `System.Numerics` 命令空间下包含了 `Vector<T>` ，是一种利用了 CPU 的 `SIMD` 特性加速计算的类型。`SIMD` 是 `Single Instruction, Multiple Data` 的简称，即使一个 CPU 指令可以完成多个数据操作。举例来说，`1 + 2 + 3 + 4` 这个操作只需要一个 CPU 指令，而不是 `1+2, +3 + 4` 三个指令，这样就大大节约了执行效率。注意，每个 `Vector<T>` 类型的数据容量是有限的，比如

```csharp
double[] x = {1.0, 2.0, 3.0, 4.0, 5.0};
var vec = new Vector<double>(x);
Console.WriteLine(vec);
```

这里输出只是 `1.0, 2.0, 3.0, 4.0`， 因为 `Vector<double>` 大小只能是 4。那么通过一个简单的例子来看看 `Vector` 类型在性能上的优势

```chsarp
private double[] _x;
private double[] _y;

[Params(100, 1000)]
public int Size { get; set; }

[GlobalSetup]
public void Setup() {
  _x = new double[Size];
  _y = new double[Size];
  var random = Random.Shared;
  for (int i = 0; i < Size; i++) {
    _x[i] = random.NextDouble() * 100;
    _y[i] = random.NextDouble() * 100;
  }
}

[Benchmark]
public double DistinaceNaive() {
  double sum = 0;
  for (int i = 0; i < Size; i++) {
    sum += (_x[i] - _y[i]) * (_x[i] - _y[i]);
  }

  return Math.Sqrt(sum);
}

[Benchmark]
public double DistanceVector() {
  double sum = 0;
  for (int i = 0; i < Size; i += Vector<double>.Count) {
    var x = new Vector<double>(_x, i);
    var y = new Vector<double>(_y, i);
    var diff = x - y;
    sum += Vector.Dot(diff, diff);
  }
  return Math.Sqrt(sum);
}
```

效果看上去还不错

| Method         | Size | Mean      | Error    | StdDev   | Allocated |
|--------------- |----- |----------:|---------:|---------:|----------:|
| DistinaceNaive | 100  |  77.03 ns | 0.870 ns | 0.772 ns |         - |
| DistanceVector | 100  |  50.72 ns | 0.250 ns | 0.195 ns |         - |
| DistinaceNaive | 1000 | 869.27 ns | 4.373 ns | 3.414 ns |         - |
| DistanceVector | 1000 | 500.64 ns | 0.604 ns | 0.504 ns |         - |

借助 `Span`，我们可以让运行更加快

```csharp
[Benchmark]
public double DistanceVectorSlice() {
  var xSpan = _x.AsSpan();
  var ySpan = _y.AsSpan();
  double sum = 0;
  for (int i = 0; i < Size; i += Vector<double>.Count) {
    var x = new Vector<double>(xSpan.Slice(i, Vector<double>.Count));
    var y = new Vector<double>(ySpan.Slice(i, Vector<double>.Count));
    var diff = x - y;
    sum += Vector.Dot(diff, diff);
  }

  return Math.Sqrt(sum);
}
```

结果如下：

| Method              | Size | Mean      | Error    | StdDev   | Allocated |
|-------------------- |----- |----------:|---------:|---------:|----------:|
| DistinaceNaive      | 100  |  77.59 ns | 0.457 ns | 0.405 ns |         - |
| DistanceVector      | 100  |  50.10 ns | 0.114 ns | 0.101 ns |         - |
| DistanceVectorSlice | 100  |  35.85 ns | 0.387 ns | 0.323 ns |         - |
| DistinaceNaive      | 1000 | 870.46 ns | 4.928 ns | 4.115 ns |         - |
| DistanceVector      | 1000 | 500.08 ns | 0.521 ns | 0.462 ns |         - |
| DistanceVectorSlice | 1000 | 417.79 ns | 4.832 ns | 4.284 ns |         - |

4、[如何诊断内存泄漏](https://www.youtube.com/watch?v=ImeiUzbdMzc)

本视频由 .NET 垃圾回收器 (GC) 的架构师 Moni 讲解如何诊断托管内存泄漏。她首先解释了内存泄漏的定义，强调 GC 无法回收仍被引用的对象。接着，她介绍了内存测量方法，讨论了虚拟内存、提交内存和工作集的区别，并分析了 GC 在不同代 (Gen 0, 1, 2) 运行时的内存变化。她还讲解了GC 触发条件（如内存压力、分配阈值），并介绍了PerfView 等工具如何帮助定位泄漏。此外，Moni 分享了新 API，可以在生产环境中监控 GC 运行情况，从而优化 .NET 应用程序的内存管理。

5、[.NET对象的生命周期 - 从分配内存到被回收](https://www.youtube.com/watch?v=1Qmvme70w9c)

本视频由 .NET GC（垃圾回收器）架构师 Moni 详细讲解**.NET 对象的内存管理全过程**，涵盖硬件、操作系统和垃圾回收机制。她首先介绍了对象的内存分配，使用 SOS 调试工具分析 .NET 对象在 GC 堆中的存储方式。接着，她探讨了GC 的分代机制（Gen 0、1、2），并解释了 GC 如何决定何时进行回收。她还分析了虚拟内存与物理内存的映射，介绍了操作系统的分页机制（Paging），以及 TLB（转换后备缓冲区）如何加速地址转换。最后，她强调了缓存一致性（Cache Coherency）对性能的影响，并探讨了优化 GC 性能的方法，如减少内存分配、控制对象生命周期等。

7、[使用 M.E.AI 调用 Azure OpenAI](https://markheath.net/post/getting-started-ai-extensions)

![Image](https://github.com/user-attachments/assets/f6d453e6-5060-42ad-823d-d11e23ec50a0)

`Microsoft.Extensions.AI` 是微软推出的官方 C# 开发 AI 大模型包。文章以 `Azure OpenAI` 为例，分别使用 `Microsoft Entra ID` 和 `App Key` 两种认证方式来开发 AI 对话应用程序。

8、[学习C# 13 的 5 个顶级功能](https://www.linkedin.com/posts/aramt87_top-5-features-in-c-13-ugcPost-7281946918522380288-Y0uU?utm_source=share&utm_medium=member_desktop)

C# 13 随着 `.NET 9` 一并推出，这篇文章介绍了其中 5 个特性

1. Params 集合

在 `C#` 之前的版本，`params` 的参数只能接受一个 Array 类型，现在可以接受任何集合类型

```csharp
static void Implode<T>(params ReadOnlySpan<T> books) {
    foreach (var book in books) {
        Console.WriteLine(book);
    }
}

static void Print(params IEnumerable<string> composers) {
    Console.WriteLine(string.Join(", ", composers));
}
```

2. 新的 `Lock` 类型

增加另一个 `Lock` 类型，它支持 `Entry()`, `Exist()` 操作来达到原子操作，`TryEnter()` 来立即尝试获取锁，`EnterScope` 是一种间接的获取锁的操作

```csharp
var logger = new Logger("log.txt");
Parallel.For(0, 10, i => {
    logger.Log($"Log message {i}");
});

class Logger 
{
    private readonly Lock _lock = new Lock();
    private readonly string _logFilePath;

    public Logger(string logFilePath) {
        _logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        using (_lock.EnterScope())
        {
            File.AppendAllText(_logFilePath, $"{DateTime.Now} - {message}\n");
        }
    }
}
```

3. `\e` 转义

在字符串中，提供了 `\e` 来表示转义的 `\x1b`, 这样避免了 `1b` 这样字符出现在 `\x` 之后导致错误的转义。

```csharp
string redText = "\e[31mThis is red text\e[0m";
System.Console.WriteLine(redText);

string boldText = "\e[1mThis is bold text\e[0m";
System.Console.WriteLine(boldText);
```

4. `^` 倒数索引

这个类似 `Python` 中索引方式类似， 从最后向前索引

```csharp
class PlayList
{
    public string[] Tracks { get; set; } = new string[3];
}

var playList = new PlayList()
{
    Tracks = {
        [^1] = "Track 1",
        [^2] = "Track 2",
        [^3] = "Track 3"
    }
};

foreach (var track in playList.Tracks)
{
    System.Console.WriteLine(track);
}
```

5. 移除 `ref struct` 和 `ref` 变量的限制

C# 13 允许 `ref` 作为泛型的中类型参数

```csharp
T Identity<T>(T item) where T : allow ref struct
{
  return item;
}

var span = Idenity(new Span<int>(new int[5]));
```

9、[Kotlin与C#的比较](https://ttu.github.io/kotlin-is-like-csharp/)

![Image](https://github.com/user-attachments/assets/3a6dad1b-aaad-4188-af52-1ab2b6970c1f)

本文详细比较了 `Kotlin` 和 `C#` 语言，可以看出这个两个语言非常相似。

10、[开源作者专访: Jimmy Bogard和AutoMapper](https://www.youtube.com/watch?v=dZ_ZVZS-FNc)

本视频介绍了AutoMapper的起源和发展。AutoMapper 诞生于 ASP.NET MVC 早期，为了解决手动编写ViewModel的低效问题。作者最初在项目中发现，每位开发者都会以不同方式创建 ViewModel，导致维护困难。于是，他开发了 AutoMapper，自动将领域模型映射到 ViewModel，减少重复代码，提高一致性。随着项目发展，它被开源，并获得社区支持。作者还分享了开源项目的维护挑战，如贡献者管理、长期维护责任等。此外，他强调了开源项目的资金困境，希望有更多企业支持开发者，使开源可持续发展。

11、[开源作者专访：Brad Wilson和xUnit](https://www.youtube.com/watch?v=CHNNDSAOJqU)

本视频探讨了 XUnit.NET 的开源发展历程及其对社区的影响。XUnit.NET 由 Jim Newkirk 和 Brad Wilson 共同创建，最初是微软的内部工具，后被开源。Brad 介绍了开源工作的好处，如提升职业发展、参与社区互动，以及在 ASP.NET 领域的演讲机会。他分享了 XUnit.NET 版本演进（V1 到 V3）的架构变更，如跨平台支持（Mono、Linux、iOS、Android）和 .NET Core 适配。此外，他讲述了 开源维护的挑战，如代码贡献、法律问题、.NET 基金会的支持等。XUnit.NET 最初托管于 CodePlex，后迁移至 GitHub，成为 .NET 测试框架的重要组成部分。

12、[开源作者专访：Nicholas Blumhardt，Autofac和Serilog](https://www.youtube.com/watch?v=z2itsGL4P3c)

本视频介绍了 Serilog 和 Autofac 这两个 .NET 生态中的重要开源项目。演讲者回顾了其参与开源的经历，讲述了如何从个人兴趣出发，逐步建立并维护这些项目。Serilog 最初是为了解决结构化日志记录的需求，而 Autofac 则专注于依赖注入。随着项目的发展，它们吸引了越来越多的贡献者，并在企业环境中得到广泛应用。演讲者强调，开源的成功不仅仅是代码的广泛使用，更重要的是社区的共同维护。他还探讨了 Rust 生态与 .NET 生态在标准库设计上的不同策略，并分享了如何在企业支持下保持开源项目的长期可持续发展。

13、[开源作者专访：Shaun Walker, DotNetNuke和Oqtane](https://www.youtube.com/watch?v=yZ4mVpBFaWg)

本视频回顾了 DotNetNuke (DNN) 开源项目的成长历程及开源管理的挑战。DNN 由 Shaun Walker 在 2003 年基于微软的 IBuySpy Portal 开发，并逐步演变成一个完整的 .NET Web 应用框架。微软在 2004 年资助了该项目，鼓励社区参与，并在 DNN 发展过程中提供支持。演讲者讲述了 开源商业化的挑战，包括微软停止资金支持后，他如何将 DNN 转变为商业公司，使其自给自足。他强调 开源管理的复杂性，包括社区维护、许可证选择、开发者激励等。此外，他讨论了 .NET 基金会的作用 及 Blazor 相关的新项目 Octane，展望开源在 .NET 生态中的未来。

14、[开源作者专访：Aaron Stannard 和 Akka.NET](https://www.youtube.com/watch?v=CJXFkc8g0_4)

本视频讲述了 Akka.NET 的开源发展历程及其商业化之路。演讲者最初因项目需求开始研究 Actor Model，发现 .NET 生态中缺乏类似 Akka（Scala） 的解决方案，于是在 2013 年开始开发 Akka.NET。团队面临 技术挑战（如网络层 Bug）并逐步优化，最终在 2014 年发布 NuGet 包。随着社区用户增长，演讲者发现咨询需求旺盛，于 2015 年创立 Petabridge，提供 企业培训、咨询和商业插件，实现盈利。他强调 开源商业模式 依赖免费核心+增值服务，同时吸引社区贡献者的挑战在于激励机制，许多贡献者因工作需求加入，并推动项目优化。

15、[开源作者专访：Dennis Doomen 和 FluentAssertions](https://www.youtube.com/watch?v=zLjCuPb7n1U)

本视频讲述了 Fluent Assertions 这一开源项目的发展历程及维护挑战。最初，该项目由演讲者与同事共同开发，并在 CodePlex 上开源，后迁移至 NuGet，逐步获得用户关注。演讲者强调了 开源管理的困难，包括如何平衡时间、维护质量、处理社区贡献等。他认为 可读性是单元测试的核心，希望 Fluent Assertions 能提高 .NET 代码的可维护性。此外，他分享了 开源的影响，从最初几百次下载到如今被微软官方 SDK 采用，并成为开发者生态的一部分。最后，他鼓励企业开源非核心代码，以促进创新和社区合作，同时强调 保持项目方向 是开源管理的重要责任。

16、[重新授权或者死亡？](https://aaronstannard.com/relicense-or-die/)

![Image](https://github.com/user-attachments/assets/7f0ca334-976e-4c45-99f2-ecefaa48000f)

最近 `FluentAssertion` 库升级为商业版本引起了很多讨论，这篇文章对这个事件做了分析。首先这件事情的影响是那些，当然是对一些用户的，比如不使用最新的 `8.0` 版本，或者付钱。对于有些依赖这个库的其他开源项目，需要替换掉这个库，对于这种只使用在单元测试的中的库，问题不大。
那么这件事情和之前 `Moq` 项目 `Sponorlink`  事件比起来有什么不同呢？区别在于这次没有有效的沟通，作为 `.NET` 用户，需要倾听开源维护者的心声，最好去激励他们。
最后，我们需要作什么？对于一个 `.NET` 开源项目，有两个选择：

- 维护者通过调整为商业授权来维护项目
- 维护者放弃这个项目

第二个选项是我们不愿面对的，那么我们只有通过下面方式针对第一种情况的发生

- 负责商业授权，掏钱
- 不升级（当然有安全漏洞的风险）
- Fork 项目并重启炉灶
- 使用替代者
- 祈求微软这样的大公司出一个自己的替换版本

那么我们正确的办法是提前主动阻止这种事情的发生：赞助它，购买支持计划，贡献项目等等。

17、[.NET 8和.NET 9的API区别对比](https://github.com/dotnet/core/pull/9602)

![Image](https://github.com/user-attachments/assets/942b40ee-43d1-43fd-b452-8de7174027fb)

`.NET 9` 和 `.NET 8` 在 API 上的不同完全列表。

## 开源项目

1、[STranslate](https://github.com/zggsong/stranslate)

![image](https://github.com/ruanyf/weekly/assets/49741009/4bedf5c6-16ae-4ca2-bdc1-63a2371177ba)

| 快捷键 | 功能 | 演示 |
| :-- | :-- | :-- |
| `Alt` + `A` | 打开软件界面，输入内容按`Enter`翻译(使用`Shift`+`Enter`换行) | ![](https://cdn.jsdelivr.net/gh/ZGGSONG/STranslate@main/img/input.gif) |
| `Alt` + `D` | 复制当前鼠标选中内容并翻译 | ![](https://cdn.jsdelivr.net/gh/ZGGSONG/STranslate@main/img/crossword.gif) |
| `Alt` + `S` | 截图选中区域内容并翻译 | ![](https://cdn.jsdelivr.net/gh/ZGGSONG/STranslate@main/img/screenshot.gif) |
| `Alt` + `G` | 打开主界面 | ![](https://cdn.jsdelivr.net/gh/ZGGSONG/STranslate@main/img/open.gif) |
| `Alt` + `Shift` + `D` | 打开监听鼠标划词，鼠标滑动选中文字立即翻译 | ![](https://cdn.jsdelivr.net/gh/ZGGSONG/STranslate@main/img/mousehook.gif) |
| `Alt` + `Shift` + `S` | 完全离线文字识别(基于PaddleOCR) | ![](https://cdn.jsdelivr.net/gh/ZGGSONG/STranslate@main/img/ocr.gif) |
| `Alt` + `Shift` + `F` | 静默OCR(OCR后自动复制到剪贴板) | ![](https://cdn.jsdelivr.net/gh/ZGGSONG/STranslate@main/img/silentocr.gif) |


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
