# .NET 每周分享第 32 期

## 卷首语

[为什么选择 .NET](https://devblogs.microsoft.com/dotnet/why-dotnet/)

![image](https://user-images.githubusercontent.com/11272110/219825763-baa8f119-c799-41b9-9982-1f02d2f80006.png)

最近 `.NET` 团队发布了一篇文章，介绍了自从 `.NET` 开源以来，微软和社区为 `.NET` 的发展和壮大做出的各种努力和付出。这也是为什么我们要选择 `.NET` 的原因。

## 行业资讯

1、[.NET 语言更新策略](https://devblogs.microsoft.com/dotnet/update-to-the-dotnet-language-strategy/)

![image](https://user-images.githubusercontent.com/11272110/218291917-aff30019-4d5c-4776-802d-21ed2675fff1.png)

在 `.NET` 平台上有三种编程语言可以运行，分别是 `C#`， `F#` 和 `Visual Basic` 三种语言，那么微软官方对三种语言的支持承若是怎样的呢？

- C# 是广泛使用的编程语言：1）广泛和积极的创新；2）保持这门语言的内在精神；3）服务于最广大的开发者；4）最大程度保证兼容；5）保持支持。
- F# 用来探索函数式语言的可能性：1）保持语言的统治性和前瞻能力；2）依赖社区提供重要的库，开发工具和工作场景；3）支持 .NET 平台的其他更新和保持互操作性；4）保持低入门槛。
- VB 是兼容已经存在的代码：1）保持一个直接和可操作的编程语言；2）稳定的设计；3）保持可接受的方法和语法；4）持续提高 VS 中的体验；5）所有工作都在 VB 核心中。

## 文章推荐

1、[组织你的 .NET 项目](https://www.jamesmichaelhickey.com/how-to-structure-your-dot-net-solutions-design-and-trade-offs/)

如果你作为一个创业公司的 CTO，需要为自己的产品和服务开发一套服务，你会又很多问题需要考虑

**代码方面**

- 单个解决方案
- 拆分多个项目

**部署方面**

- 一次部署
- 使用负债均衡器部署多个服务
- 特定请求发送给特定的服务器
- 微服务部署

**通信**

- 内存消息通信
- API 请求通信
- 消息队列

**数据库**

- 单个数据库
- 多个数据库
- 数据库分片

这些选择都是基于业务的需求不停的演变，这篇文章介绍为什么需要选择这个措施，有哪些 `Trade-off`， 以及具体的例子。

2、[EventSource 学习](https://www.youtube.com/watch?v=yWpuUHXLhYg&ab_channel=CodeOpinion)

Event Source 是广泛使用的持久化状态的工具， `Marten` 是 `.NET` 平台使用的的 `Event` 库，`Jermery Miller` 也是 `Marten` 是维护者。

3、[C# Web 安全开发建议](https://dev.to/bytehide/net-security-headers-a-senior-developers-guide-150d)

![image](https://user-images.githubusercontent.com/11272110/218293000-2b684fb1-a021-468f-a869-1c570e07825b.png)

我们都知道 Web 是不安全的，那么作为 `.NET` Web 开发工程师，需要在服务端做好这些防护工作。

1. X-XSS-Protection Header

跨站脚本攻击是一件常见的的攻击手段，它可以通过在用户页面自动执行一些脚本就可以达到攻击的页面. `HTTP Header` 中的 `X-XSS-Proection` 可以阻止脚本执行

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.Use(async (context, next) =>
    {
        context.Response.Header.Add("Content-Security-Policy", "default-src 'self';");
        await next();
    });
    app.UseMvc();
}
```

2. Content-Security-Policy Header
   CSP 允许你那些资源可以在浏览器中执行

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.Use(async (context, next) =>
    {
        Context.Response.Header.Add("Content-Security-Policy", "default 'self';");
        await next ();
    });
}
```

3. X-Frame-Options Headers

攻击者可以通过 `ClickJacking` 方式对 UI 进行重绘，这样可以误导用户错误点击，`X-Frame-Options` Header 可以限制这样的行为。

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.Use(async (context, next) =>
    {
        context.Response.Header.Add("X-Frame-Options", "DENY");
        await next();
    })
}
```

4.  HSTS
    我们都知道 `HTTP` 协议不安全，应该选择 `HTTPS` 版本，但是仍然有办法绕过 `HTTPS` 而是直接使用 `HTTP`。 `HSTS` 可以强制使用 `HTTPS` 协议。

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
   app.UseHsts();
}
```

4、[Powershell 入门系列教程](https://twitter.com/khalilApriday/status/1623920383979290624)

![image](https://user-images.githubusercontent.com/11272110/219817957-f0c2582a-9714-427d-b482-2831033f3c3c.png)

这是一个 Powershell 学习资源的 twitter thread， 主要包含一下内容

- [自动化管理员任务](https://learn.microsoft.com/en-us/training/paths/powershell/)
- [Powershell 大师课程](https://www.youtube.com/playlist?list=PLlVtbbG169nFq_hR7FcMYg32xsSAObuq8)
- [Powershell IT 专业人员教程](https://kamilpro.com/powershell-for-it-professionals/)
- [微软官方教程](https://learn.microsoft.com/en-us/shows/getting-started-with-microsoft-powershell/)
- [PSkoans](https://github.com/vexx32/PSKoans)
- [Powershell 7 训练课程](https://www.linkedin.com/learning/powershell-7-essential-training?src=aff-ref&trk=aff-ir_progid.8005_partid.259799_sid._adid.647232&clickid=yJLQBuWowxyNU3VyfNU4iUJJUkA3KoznK3IVVU0&mcid=6851962469594763264&irgwc=1)

5、[C# 异步编程模式](https://code-maze.com/asynchronous-programming-patterns-dotnet/)

![image](https://user-images.githubusercontent.com/11272110/219822754-28e63aba-6f97-4a85-9307-62ce231d4191.png)

在 `C#` 的历史发展过程中，异步编程经历过三种模式，分别为

- 异步编程模式 (Asynchronous Programming Model, APM)
- 基于事件的异步模式 （Event-Based Asynchronous Pattern, EAP)
- 基于任务的异步模式 （Task-Based Asynchrouous Pattern, TAP)

1. APM
   包含三种主要成分

- 两个方法分别表示开始和结束的异步操作
- 一个 `AsyncCallback` 的委托，用来表示操作的完成
- 可选的 `state` 属性表示依赖

```csharp
public class ApmFileReader
{
    private byte[]? _buffer;
    private const int InputReportLength = 1024;
    private FileStream? _fileStream;

    public void BeginReadAsync()
    {
        _buffer = new byte[InputReportLength];
        _fileStream = File.OpenRead("User.txt");
        _fileStream.BeginRead(_buffer, 0, InputReportLength, ReadCallbackAsync, _buffer);
    }

    public void ReadCallbackAsync(IAsyncResult iResult)
    {
        _fileStream?.EndRead(iResult);
        var buffer = iResult.AsyncState as byte[];
    }
}
```

2. EAP
   使用 C# 的事件是实现异步操作，这些操作在各自的线程上操作，然后通过异步来通知。

```csharp
public void GetUserAsync(int userId, object userState)
{
    AsyncOperation operation = AsyncOperationManager.CreateOperation(userState);

    ThreadPool.QueueUserWorkItem(state =>
    {
        GetUserCompletedEventArgs args;
        try
        {
            var user = GetUser(userId);
            args = new GetUserCompletedEventArgs(null, false, user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            args = new GetUserCompletedEventArgs(e, false, null);
        }

        operation.PostOperationCompleted(_operationFinished, args);

    }, userState);
}
```

3. TAP

这是目前 `C#` 推荐的异步编程模型，通过 `Task`, `async` 和 `await` 关键字实现异步功能。

```csharp
public Task<int> OperationName1Async(int param)
{
    // more code
    return Task.FromResult(1);
}

public async Task<int> OperationName2Async(int param)
{
    // more code with await
    return 1;
}
```

6、[C# 中 String 的优化机制](https://www.youtube.com/watch?v=Xeq8YGEyyp8&ab_channel=NickChapsas)

String 类型在 `C#` 中是一种不可变的引用类型，但是有一个特性往往被大家忽略了，也就是 `Intern` 功能。简单来说就是 `CLR` 会将明面字符串在内存中储存一起来，如果有相同的变量使用同一个字符串，那么就会共享这一个内存空间。

```csharp
var dotnetweekly1 = ".net weekly";
var dotnetweekly2 = ".net weekly";

Console.WriteLine(dotnetweekly1 == dotnetweekly2); //true
Console.WriteLine(object.ReferenceEquals(dotnetweekly1, dotnetweekly2)); //true

string dotnet = ".net";
string weekly = " weekly";
string dotnetweekly = dotnet + weekly;
Console.WriteLine(".net weekly" == dotnetweekly); //true
Console.WriteLine(object.ReferenceEquals(".net weekly", dotnetweekly));// false;

const string dotnet3 = ".net";
const string weekly3 = " weekly";
string dotnetweekly3 = dotnet3 + weekly3;
Console.WriteLine(".net weekly" == dotnetweekly3); //true
Console.WriteLine(object.ReferenceEquals(".net weekly", dotnetweekly3));// true;

```

7、[Anders Hejlsberg 专访](https://www.aarthiandsriram.com/p/our-dream-conversation-anders-hejlsberg?sd=pf)

播客博主采访了 `Anders Hejlsberg`，讨论了下面的内容：

- Anders 早期的经历，包括电脑，编程语言和病毒
- Turbo Pascal 的经历
- 早期开发 `C#` 和 `.NET Framework` 经历
- Typescript 的现状
- 关于其他语言的看法，比如 `Rust`, `Golang` 等等。

## 开源项目

1、[stride](https://github.com/stride3d/stride)

![image](https://user-images.githubusercontent.com/11272110/218292385-622972ee-c7ab-4bb2-9ca8-e836bf7ee4ce.png)

Stride 是最近 `C#` 开源的游戏引擎库。

2、[CommunityToolkit](https://github.com/CommunityToolkit/dotnet)

CommunityToolkit` 是微软官方维护的一组工具类集合， 主要包含下面四种类型

- `CommunityToolkit.Common` 共有类集合
- `CommunityToolkit.Mvvm` MVVM 设计模式需要的类库
- `CommunityToolkit.Diagnoscitcs` 参数任何和错误检查的工具库
- `CommunityToolkit.HighPerformance` 高性能的 API 和类型库
