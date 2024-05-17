# .NET 每周分享第 33 期

## 卷首语

之前微软发布了一篇名为 [为什么选择.NET](https://devblogs.microsoft.com/dotnet/why-dotnet/) 的文章，作者对它进行了思考

1.  这篇文章的目标读者是谁？

这篇文章对于新手来说有些复杂，而对于老手则过于简单。它更适合那些已经在使用其他语言的开发人员对其他编程语言进行评估的人阅读。

2. `.NET` 的设计点是什么

该文章提出了四个设计原则：生产力、性能、安全和可靠性。然而，作者认为最重要的原则是生产力。最新版本的 .NET 7，例如，只需要三到四行代码就可以创建一个简单的 ASP.NET Core 应用程序：

```csharp
var app = WebApplication.Create();
app.MapGet("/", (string? name) => "Hello {name ?? "World"}!");
app.Run();
```

此外，微软正在积极采用行业标准，例如 gRPC 和 OpenTelemetry 等。

3. 这篇文章会改变人们对 .NET 的看法吗？

这篇文章只会对已经对 .NET 感兴趣或者正在考虑使用它的人有所帮助。

## 行业资讯

1、[Visual Studio Extension 升级 .NET 项目](https://devblogs.microsoft.com/dotnet/upgrade-assistant-now-in-visual-studio/)

![image](https://user-images.githubusercontent.com/11272110/224491081-d82e048e-58d1-47d3-ac5f-8029e75fa9e6.png)

微软发布了一个 Visual Studio 插件，它可以帮助你将 .NET 应用程序升级到最新的版本，也支持从 .NET Framework 迁移到 .NET Core 版本。它支持的类型有：

- ASP.NET
- 类库
- 控制台
- WPF
- WinForms

2、[.NET 8 预览版](https://devblogs.microsoft.com/dotnet/announcing-dotnet-8-preview-1/)

![image](https://user-images.githubusercontent.com/11272110/224482631-3618af5b-02fd-4b2d-a844-65091ad20bfb.png)

.NET 8 的预览版已经发布了，那么有那些方面的更新呢？

1. Native AOT
2. .NET Container Images
3. Runtime 和 Libraries
4. .NET SDK
5. Linux Support
6. CodeGen
7. ...

## 文章推荐

1、[VS 代码智能提示](https://devblogs.microsoft.com/visualstudio/intellicode-api-usage-examples/)

![image](https://user-images.githubusercontent.com/11272110/224482455-c890daa0-ccf8-468f-ba3c-9be577b9d68d.png)

当我们使用 API 的时候，很多时候希望知道如何使用这些 API，当然查看官方文档是一个正确选择。但是如何我们能够查找大部分在真正项目中的使用方式就更好了。`Visual Studio` 最新的预览版为超过 100 K 的有名的 API 提供了 GitHub 查找功能。

2、[Async and await 可以省略吗？](https://blog.stephencleary.com/2016/12/eliding-async-await.html)

在异步的方法中，可以掉 `async` 和 `await` 关键字，比如

```csharp
async Task<string> Get(string url)
{
  return new HttpClient().GetStringAsync(url);
}
```

这样的好处在于它避免了编译器创建一个状态机，降低了 `GC` 的压力，而且更少的生成代码，使得程序运行速度更快。但是这样有三种情况下的陷阱需要注意：

1. Using 语句

```csharp
public async Task<string> GetWithKeywordsAsync(string url)
{
    using (var client = new HttpClient())
        return await client.GetStringAsync(url);
}

public Task<string> GetElidingKeywordsAsync(string url)
{
    using (var client = new HttpClient())
        return client.GetStringAsync(url);
}
```

在这个例子中, `GetElidingKeywordsAsync` 方法是有 bug 的，因为 `HttpClient` 在返回之后就释放了，导致异常。

2. Exception

```charp
public async Task<string> GetWithKeywordsAsync()
{
    string url = /* Something that can throw an exception */;
    return await DownloadStringAsync(url);
}

public Task<string> GetElidingKeywordsAsync()
{
    string url = /* Something that can throw an exception */;
    return DownloadStringAsync(url);
}
```

如果在异步方法中抛出异常，那么在调用方抛出的异常位置是不一样的

```csharp
var task = GetWithKeywordsAsync();
var result = await task; // Exception thrown here

var task = GetElidingKeywordsAsync(); // Exception thrown here
var result = await task;
```

3. AsyncLocal

`AsyncLocal<T>` 可以保证异步代码调用不影响程序状态，比如说

```csharp
static AsyncLocal<int> context = new AsyncLocal<int>();

static async Task MainAsync()
{
    context.Value = 1;
    Console.WriteLine("Should be 1: " + context.Value);
    await Async();
    Console.WriteLine("Should be 1: " + context.Value);
}

static async Task Async()
{
    Console.WriteLine("Should be 1: " + context.Value);
    context.Value = 2;
    Console.WriteLine("Should be 2: " + context.Value);
    await Task.Yield();
    Console.WriteLine("Should be 2: " + context.Value);
}
```

如果省略掉 `async` 和 `await` 之后，行为就不一致了

```csharp
static AsyncLocal<int> context = new AsyncLocal<int>();

static async Task MainAsync()
{
    context.Value = 1;
    Console.WriteLine("Should be 1: " + context.Value);
    await Async();
    Console.WriteLine("Should be 1: " + context.Value); // Is actually "2" - unexpected!
}

static Task Async()
{
    Console.WriteLine("Should be 1: " + context.Value);
    context.Value = 2;
    Console.WriteLine("Should be 2: " + context.Value);
    return Task.CompletedTask;
}
```

那么我们什么时候可以省略掉 `async` 和 `await` 呢？

1. 一行代码

```csharp
Task<string> PassthroughAsync(int x) => _service.PassthroughAsync(x);
```

2. Override 方法

```csharp
async Task<string> OverloadsAsync(CancellationToken cancellationToken)
{
    ... // Core implementation, using await.
}
Task<string> OverloadsAsync() => OverloadsAsync(CancellationToken.None);
```

3、[C# 代码中的注释](https://code-maze.com/csharp-different-types-of-comments/)

C# 中的注释有那类型呢？

1.  单行注释

这是最简单的注释，`//` 开头的后面的都是注释

```csharp
const double PI = 3.14; // pie
```

2. TODO 类型
   当我们开发的过程，需要将代办的内容放在代码中，可以在注释中使用 `TODO` 标准，这样可以在 `Visual Studio` 中 `View / Task List` 中查看

```csharp
// TODO: Instead of a constant, ask the user for the speed
var time = 5; // time of falling is 5 seconds
```

3. 多行注释

如果我们的注释并不能在同一行展示，那么需要多行注释

```csharp
// Gravity 9.81 meters per second squared
const double GRAVITY = 9.81;
var time = 5; // time of falling is 5 seconds
/*
  To calculate the speed at which the object will hit the floor,
  we have to use the simple formula:
  speed = gravity * time of falling
*/
var speed = GRAVITY * time;
```

4. 文档注释

C# 还有一种注释，它是文档中一部分，通常是以 `///` 开头。通过它可以自动话生成程序文档，以方便调用者查看。

```csharp
/// <summary>
///
/// </summary>
/// <param name="price"></param>
/// <param name="percentageOfTax"></param>
/// <param name="priceIsWithTax"></param>
/// <returns></returns>
public static double CalculateTax(double price, double percentageOfTax, bool priceIsWithTax = false)
{
    double percentage = percentageOfTax / 100;
    return (priceIsWithTax)
        ? price * percentage / (1 + percentage)
        : price * percentage;
}
```

除了 `summary`, `param` 等之外，还有个 `c`, `code`, `example` 等等。

4、[C# Clean Architecture](https://blog.ndepend.com/clean-architecture-for-asp-net-core-solution/)

![image](https://user-images.githubusercontent.com/11272110/224476505-3e01c667-4437-443d-a199-1a146fe61c9c.png)

对于 `ASP.NET Core` 的应用程序，如遵循 `Clean Architecture` 的设计原则的话，可以拆分为四个部分

- Domain
- Application
- WebUI
- Infrastructure

它们之间遵循下面的设计规范

- 每一层只能外面依赖里卖弄
- 应用层只处理创建，编辑和持久化实体对象
- 基础设施保证可以更换
- UI 层包含 `Controller` 和 `View`

5、[C# 查找调用方法名](https://code-maze.com/csharp-how-to-find-caller-method/)

在代码中，我们很多时候需要知道函数的调用方是谁，通常有三个目的

- 调式
- 日志输出
- 性能调试

那么在 `C#` 中有三种实现方式

1. StackTrace

```csharp
public static void DoWork()
{
    PrintCallerName();
}
public static void PrintCallerName()
{
    MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
    string callerMethodName = caller.Name;
    string calledMethodName = MethodBase.GetCurrentMethod().Name;
    Console.WriteLine("The caller method is: " + callerMethodName);
    Console.WriteLine("The called method is: " + calledMethodName);
}
```

`StackTrace` 可以捕获调用的上下文

2. StackFrame

```csharp
public static void PrintCallerNameWithoutStack()
{
    MethodBase caller = new StackFrame(1, false).GetMethod();
    string callerMethodName = caller.Name;
    string calledMethodName = MethodBase.GetCurrentMethod().Name;

    Console.WriteLine("The caller method is: " + callerMethodName);
    Console.WriteLine("The called method is: " + calledMethodName);
}
```

StackFrame 跟 `StackTrace` 同理，只不过跳过了 `StackTrace` 的创建。

3. CallerMemberName

C# 还有一个属性叫做 `CallerMemberName`, 它可以获取调用者的信息

```csharp
public static void PrintCallerNameWithCallerMemberNameAttribute([CallerMemberName] string callerMethodName = "")
{
    string calledMethodName = MethodBase.GetCurrentMethod().Name;

    Console.WriteLine("The caller method is: " + callerMethodName);
    Console.WriteLine("The called method is: " + calledMethodName);
}
```

6、[EF Core 中的 LINQ](https://www.youtube.com/watch?v=1Ld3dtnTrMw&ab_channel=dotnet)

![image](https://user-images.githubusercontent.com/11272110/224474127-76d820ad-6588-4137-afed-8b0b46c1e570.png)

LINQ 在 `C#` 编程中广泛使用，但是又不同的用途，比如 `LINQ to Object` ， `LINQ to SQL`。在 `EF Core` 中 `LINQ` 是怎么工作的呢？这个视频详细讨论了这个内容。

7、[为 NuGet 编写高质量 README](https://devblogs.microsoft.com/nuget/write-a-high-quality-readme-for-nuget-packages/)

![image](https://user-images.githubusercontent.com/11272110/224462144-1e03c722-8b23-4b3e-b002-4d45a7a30531.png)

`README` 文件是客户接触某个工具，库或者软件第一个东西。比如当我们把我们的库推送 `NuGet` 中以方便全世界的开发者使用，那么 `README` 就更加重要了，那么这里有一些建议写出高质量的 `README` 文件

- 开始一个清楚和简洁的说明
- 解释如何使用它
- 具体的例子
- 更多资源的链接
- 提供截图和可视化内容
- 写下限制料件
- 提供具体的代码
- 让它更加具有吸引力

一些正面的例子

- [Azure.Storage.Blobs](https://www.nuget.org/packages/Azure.Storage.Blobs)
- [Azure.Extensions.AspNetCore.Configuration.Secrets](https://www.nuget.org/packages/Azure.Extensions.AspNetCore.Configuration.Secrets)
- [GitInfo](https://www.nuget.org/packages/GitInfo/3.0.0-alpha)
- [MySqlConnector](https://www.nuget.org/packages/MySqlConnector/2.3.0-beta.1)
- [System.IO.Abstractions](https://www.nuget.org/packages/System.IO.Abstractions)

## 开源项目

1、[ONNX](https://devblogs.microsoft.com/dotnet/generate-ai-images-stable-diffusion-csharp-onnx-runtime/)

![image](https://user-images.githubusercontent.com/11272110/224481815-11afd250-b336-4671-9fed-d5d9940f57bb.png)

`ONNX` 是一个开源的 AI 模型，而 `ONNX Runtime` 提供了大量的不同开发语言的接口，其中就有 C#，通过它你可以

1. 训练一个支持 `ONNX` 的热门机器学习的框架
2. 将你的模型转换成 `ONNX`
3. 加载和消费其他的模型并且用 `C#` 来调用它
