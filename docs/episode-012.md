# .NET 每周分享第 12 期

## 行业资讯

1、[GitHub Code Copilot for Visual Studio 正式发布 ](https://github.blog/2022-03-29-github-copilot-now-available-for-visual-studio-2022/)

![](https://dotnetweeklypics.blob.core.windows.net/012/github-co-pilot.webp)

去年 `GitHub` 发布了 `Code Copilot` 之后，引起了行业内巨大的反馈，~人工智能取代程序员的日子即将到来~，但是只支持 `Visual Studio Code`。最近 `GitHub` 发布了 `Code Copilot` for `Visual Studio` 的插件，这样在宇宙第一 IDE 中编写代码，尤其是 `.NET` 的相关代码，也能得到人工智能的加持。

2、[MAUI RC 发布](https://devblogs.microsoft.com/dotnet/dotnet-maui-rc-1/)

千呼万唤始出来，MAUI 终于到达了 RC 阶段（Release Candidate），这也就意味着 MAUI 的 SDK 已经基本稳定了，希望尝鲜的小伙伴可以搞起来了。

3、[Nuget 统一包依赖管](https://devblogs.microsoft.com/nuget/introducing-central-package-management/)

![](https://dotnetweeklypics.blob.core.windows.net/012/nuget.png)

`NuGet` 是 `.NET` 平台上使用的包管理工具，而依赖管理是包管理的核心功能，历史上 `.NET` 有两种方式管理项目的依赖

1. `package.config` 文件：这是一个 `XML` 文件，工程来引用这个文件来管理依赖。
2. `<PackageReference />`  文件，这是工程 XML 文件中的元素，用来表示这个工程的依赖。

对于小型项目，这两种方式都没有问题，但是如果一个解决方案包含了多个工程，那么管理这么多的工程的依赖就带来了挑战。
在 `NuGet 6.2` 中，引入了 `Directory.Package.Props` 文件，这也是一个 `XML` 文件，

```XML
<Project>
  <ItemGroup>
    <PackageVersion Include="Newtonsoft.Json" Version="13.0.1" />
  </ItemGroup>
</Project>
```

那么就可以在项目文件中引用这个 `Newtonsoft.Json` 而无需制定版本。

```XML
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" />
  </ItemGroup>
</Project>
```

在一个解决方案中，可以有多个 `Directory.Package.Props` 文件，而项目工程文件查找最近的 `Directory.Package.Props`。

## 文章推荐

1、[Visual Studio 中的 .editorconfig](https://www.youtube.com/watch?v=CQW5b58mPdg&ab_channel=IAmTimCorey)

每个人都有自己的编程风格，对于独立项目，这个没有问题。但是如果是团队合作的项目，不同的代码风格会增加合作的难度，因此在 Visual Studio 的项目中，增加一个 .editorconfig 文件，它可以规范代码风格，甚至配置 Severity 的等级。

2、[CancellationToken 的介绍](https://blog.stephencleary.com/2022/02/cancellation-1-overview.html)

![](https://dotnetweeklypics.blob.core.windows.net/012/cancellation.jpg)

`CancellationToken`  是 `.NET` 平台统一的取消模型，通过它可以取消某些操作，比如超时，取消事件发生等等。当然也有一些注意点

1. 合作模型

通常分为两个部分，取消请求发起方和请求响应方，响应方的方法签名如下

```Csharp
async Task DoSomethingAsync(int data, CancellationToken cancellationToken)
{
    ...
}
// or:
async Task DoSomethingAsync(int data, CancellationToken cancellationToken = default)
{
    ...
}
```

响应方可以选择接受取消的请求，也可以选择拒绝取消请求。注意这种使用方式并不会真正取消操作

```Csharp
async Task DoSomethingAsync(CancellationToken cancellationToken)
{
    var test = await Task.Run(() =>
    {
        // Do something, ignoring cancellationToken
    }, cancellationToken);
    ...
}
```

2. CancellationTokenSource

除了使用一些库，它会包含一些内置的 `CancellationToken`，我们都会要求自己创建相关的 `CancellationToken`，它一般是由 `CancnellationTokenSource` 创建，它可以创建一系列 `Token`，而且每个 `Token` 都是包含指向创建的 `CancellationTokenSource` 对象。注意在使用 `CancellationTokenSource` 的时候，使用 `using` 语句来释放托管的资源

3. 响应取消操作

在取消请求得到响应了，会抛出 `OperationCanceledException` 的异常，所以在调用方捕获这个异常即可正确的处理

```Csharp
async Task DoSomethingAsync()
{
    using CancellationTokenSource cts = new();

    .. // Wire up something that may cancel the CTS.

    try
    {
        await DoThingAsync(cts.Token);
    }
    catch (OperationCanceledException)
    {
        .. // Special cancellation handling
    }
    catch (Exception ex)
    {
        .. // Normal error handling; log, etc.
    }
}
```

如果一个请求是由另外的 `CancellationToken` 发出的，那么需要判断请求发出的正确的对象

```Csharp
async Task DoSomethingAsync()
{
    Environment.FailFast("Bad code; do not use!");

    using CancellationTokenSource cts = new();

    .. // Wire up something that may cancel the CTS.

    try
    {
        await DoThingAsync(cts.Token);
    }
    catch (OperationCanceledException ex) when (ex.CancellationToken == cts.Token)
    {
        .. // Special cancellation handling for "our" cancellation only.
    }
}
```

4. 轮询取消请求

在响应段的代码中，需要知道取消请求是否发生。通常是在循环发生的开始，执行 `ThrowIfCancellationRequested` 方法，如果取消请求发生，则抛出 `OperationCanceledException` 异常。

```Csharp
void DoSomething(CancellationToken cancellationToken)
{
    while (!done)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Thread.Sleep(200); // do synchronous work
    }
}
```

注意不要使用 `IsCancellationRequested` 属性来判断取消是否发生，因为这样，调用方不知道取消操作是否发生。

```Csharp
void DoSomethingForever(CancellationToken cancellationToken)
{
    Environment.FailFast("Bad code! Do not use!");
    while (!cancellationToken.IsCancellationRequested)
    {
        Thread.Sleep(200); // do work
    }
}
```

3、[Timer 类介绍](https://code-maze.com/timer-csharp/)

![](https://dotnetweeklypics.blob.core.windows.net/012/Timer.png)

定时器 (Timer) 广泛使用在应用程序开发中，在达到一定的事件后，执行相关的操作。那么关于 `System.Timers.Timer` 类，由哪些需要注意的地方呢？

`Timer` 类实现了 `IDisposable` 接口。
设置 timer.AutoReset = false 可以发生一次事件。
事件注册 Elapsed 既可以是 Lambda 表达式，也可以是异步形式。
一般事件触发的委托由线程池的线程执行，但是在 `Windows Forms` 这样的 UI 应用程序中，UI 必须由 UI 线程更新，所以可以设置 `SynchronizingObject` 属性，通常为 `Control` 对象。

和其他 Timer 的区别

- System.Threading.Timer: 只会执行一次
- System.Windows.Form.Timer: UI 单线程的计时器

4、[如何将一个字符串转换成标题的样式](https://code-maze.com/csharp-convert-string-title-case/)

英文中，标题是有相关的的格式要求，主要有

- 冠词小写
- 连词小写
- 名词首字母大写
- 动词首字母大写
- 形容词首字母大写
- 动词首字母大写
- 介词小写
- ...

那么这么多的规则，在 .NET 中该怎么实现呢？基础库已经帮我们实现了改功能

```Csharp
var textInfo = new CultureInfo("en-US", false).TextInfo;
Console.WriteLine(textinfo.ToTitleCase("a tale oF tWo citIes")); // A Tale of Two Cities
```

## 开源项目

1、[Avaloniaui 开源 UI 库](http://avaloniaui.net/)

![](https://dotnetweeklypics.blob.core.windows.net/012/avaloniaui.png)

**Avaloniaui** 是 `.NET` 开源 UI 库，相对于 `MAUI`, 它更加是一种桌面跨平台 UI 平台，主要支持

- Windows
- Linux
- MacOS

它采用了 `Model-View` 框架，如果熟悉 `WPF` 应用开发，会发现非常容易上手。

2、[.NET nanoFramework](https://www.nanoframework.net/)

![](https://dotnetweeklypics.blob.core.windows.net/012/nano.png)

开发 `IoT` 或者嵌入式设备的应用程序，通常会选择 `C/C++` 这样的开发语言，`C#` 这类带 GC 的语言通常不在考虑的范围之内，`NanoFramework` 这个开源项目提供了一种选择，它通过裁剪过的 `runtime` 和 `library`，是 `.NET` 应用程序运行在 `IoT` 这样内存和闪存有限的设备中。
