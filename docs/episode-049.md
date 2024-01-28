# .NET 每周分享第 49 期

## 卷首语

C# 成为年度语言? 这里有一个有趣的不同的[观点](https://www.youtube.com/watch?v=5omutyj6N0Q&t=409s&ab_channel=NickChapsas)。

在过去的一段时间，`.NET` 社区最大的新闻是 `C#` 被 Tiobe 评选为 2023 年度编程语言。几乎所有的媒体都在欢呼雀跃，那么事实真的如此吗？

1. Tiobe 评选的标准是增加量
2. 结果并不会改变立刻任何事情
3. Tiobe 的选择的可靠性不高

## 文章推荐

1、[StringValue 探秘](https://andrewlock.net/a-brief-look-at-stringvalues/)

在 `Microsoft.Extensions.Primitives` 包中有一个类型是 `StringValues`, 它可以用在表示 `HTTP` Header 中，可以表达三种情况，

1. 没有 Header
2. 单个 Header
3. 多个 Header

那么在 `ASP.NET Core` 中，该如何表示这个对象呢？

- 朴素表达方式

使用 `Dictionary<string, string[]>` 类型表达，因为上述三种情况都可以表示，但是这个存在两个问题：

1. 大部分情况下，只有一个值，但是还是需要多个情况
2. 将单个值存储为一个数组，增加内存分配

- `NameValueCollection`

在 `ASP.NET` 中使用 `NameValueCollection` 类型，但是它本质还是使用 `ArrayList` 存储内容， 所以还是不够优雅。

在 `StringValues` 使用一个 `object` 表示上述的三种情况，也是一个 `readonly struct` 类型。

2、[.NET 8 中 Random](https://henriquesd.medium.com/net-8-new-randomness-methods-f2422f55320f)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/467d1215-43b6-4500-b877-ff290315c463)

.NET 8 在 `Random` 类中提供了一些新的方法，比如

1. GetItems

当我们在一个集合中随机选择一定数量时候，可以使用 `GetItems`， 比如 `Random.Shard.GetItems`。

2. Shuffle

如果对一个集合进行混洗，可以使用 `Shuffle`, 比如 `Random.Shard.Shuffle`。

3、[.NET 8 中并行启动 IHostedService](https://www.youtube.com/watch?v=n4tzpRB2lzc&ab_channel=MilanJovanovi%C4%87)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/d39de28d-cf1f-4415-b72a-0e300b8390d4)

在 `IHost` 中，在执行 `Run` 方法的时候，会获取所有的注册的 `IHostedService` 取出来，然后执行 `StartAsync` 方法。要注意的是这个方法是顺序执行的，在 `.NET 8` 中，可以设置 `HostOption` 的 `ServicesStartConcurrently` 和 `ServicesStopConcurrently` 方法让启动或者停止并行执行。

```csharp
new HostBuilder()
    .ConfigureServices(service =>
    {
        service.Configure<HostOptions>(options =>
        {
            options.ServicesStartConcurrently = true;
            options.ServicesStopConcurrently = true;
        });
        service.AddHostedService<FooService>();
        service.AddHostedService<BarService>();
    })
    .Build()
    .Run();
 
internal class FooService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Start Foo");
        await Task.Delay(2000);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stop Foo");
        await Task.Delay(2000);
    }
}

internal class BarService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Start Bar");
        await Task.Delay(1000);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stop Bar");
        await Task.Delay(1000);
    }
}
```

4、[查看 JIT 代码的三种方法](https://www.meziantou.net/how-to-get-assembly-code-generated-by-the-jit-for-a-csharp-method.htm)

C# 支持查看 `JIT` 生成的机器代码，那么该如何查看它们呢？有三种方法

1. DOTNET_JitDisasm="Method"

```csharp
Foo.Bar(); 
class Foo
{
    public static void Bar()
    {
        Console.WriteLine("Hello World!");
    }
}
```

如果设置 `DOTNET_JitDisasm="BAR"` ，那么在执行这个方法的时候，就能看到生成 `JIT` 代码

2. Disasmo

`Disasmo` 是 Visual Studio 的插件，用来查看生成的 JIT 代码。

3. Sharplab

[Sharplab](https://sharplab.io/) 是一个非常有用的 Web 站点，它可以对相关的代码生成相应的 JIT 代码方便查看。

5、[DTO 和 POCO 的区别](https://ardalis.com/dto-or-poco/#sq_hhjrkq9ir7)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e2214e4d-d9da-47b1-9ab6-8390bc1a46f4)

`DTO` 和 `POCO` 是在开发工程中经常遇到的概念，那么它们区别是怎样的呢？

- DTO

DTO 是 `Data Transfer Object` 的简写，它表明只是传递数据，而不包含逻辑和行为。在 C# 中就是只有只有属性，而不是有方法或者属性的注解。

- POCO

POCO 是 `Plain Old CLR Object` 的简称，它表明这个类不依赖任何第三方的库或者框架。

6、[.NET 最快的 1BRC](https://hotforknowledge.com/2024/01/13/1brc-in-dotnet-among-fastest-on-linux-my-optimization-journey/#results-table)

在今年开始的时候，`GitHub` 爆火了 ”十亿行挑战" 活动。从原先的 `Java` 语言到所有开发语言。在 `.NET` 范围内，目前最快记录是 `1.297` 秒，作者介绍了自己的优化之路。

## 开源项目

1、[NuGet Resolver](https://devblogs.microsoft.com/nuget/introducing-nugetsolver-a-powerful-tool-for-resolving-nuget-dependency-conflicts-in-visual-studio/)

[image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0edc9aba-4365-4ae2-939e-4438b8c5d66b)

在一些大型 `.NET` 项目中，常常会遇到依赖冲突的问题，比如

- 无法解析依赖的一致性问题
- 依赖包不包含适合项目的资源包
- 依赖的包版本大于最终解析版本

等等问题，`Nuget` 团队和 MSR 团队合作，推出了 `NuGetSolver` 中 `Visual Studio` 插件，它可以解决上述的问题。但是目前有一些限制

1. 只支持 `nuget.org` 源
2. 不支持多个源
3. 不支持自动更新版本
4. 不支持 preview 版本
5. `package.config` 等老式版本不支持
6. 只支持编译依赖冲突

2、[Rx.NET](https://github.com/dotnet/reactive)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/2ef55501-fc5d-4081-a80c-26643315fcfd)

`Rx.NET` 是一个开源的 `.NET` 事件库，可以帮助实现响应式编程，这样你的代码只需要响应对应的事件。

```csharp
using System.Reactive.Linq;

IObservable<long> ticks = Observable.Timer(
    dueTime: TimeSpan.Zero,
    period: TimeSpan.FromSeconds(1));

ticks.Subscribe(tick => Console.WriteLine($"Tick {tick}"));

Console.ReadKey();
```

3、[MSTest Runner](https://devblogs.microsoft.com/dotnet/introducing-ms-test-runner/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9a70f750-b25b-465f-a0a6-fd7998f4b357)

微软推出了 `MSTest Runner` 这个测试框架，它更加轻量级，测试更快并且提供更加便携的测试机制。主要修改两个地方

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <!-- Enable the MSTest runner, this is an opt-in feature -->
    <EnableMSTestRunner>true</EnableMSTestRunner>
    <!-- We need to produce an executable and not a DLL -->
    <OutputType>Exe</OutputType>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="MSTest" Version="3.2.0" />
  </ItemGroup>

</Project>
```

这样只需要执行生成的 `exe` 就可以运行单元测试。