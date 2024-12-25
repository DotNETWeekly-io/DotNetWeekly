# .NET 每周分享第 63 期

## 卷首语

![image](https://github.com/user-attachments/assets/2eeb1037-7fd7-44e9-ab49-d0cc1de910b7)

随着 `.NET` 开源，每一年 `.NET` 在性能上都有显著的提升，而且官方也提供详细的文章来阐述这些内容，这里整理了至今所有的有关的文章：

.NET 9
https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-9/

.NET 8
https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/

.NET 7
https://devblogs.microsoft.com/dotnet/performance_improvements_in_net_7/ 

.NET 6
https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-6/

.NET 5
https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-5/

.NET 3.0
https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-core-3-0/

.NET 2.1
https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-core-2-1/ 

.NET 2.0
https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-core/

## 行业资讯

1、[如何用 Stopwatch 正确的计算耗时](https://www.youtube.com/watch?v=Lvdyi5DWNm4&ab_channel=NickChapsas)

![image](https://github.com/user-attachments/assets/5268c04e-ff5c-4454-a6b5-f28f86608fba)

是否升级最新的 `.NET 9`, 这个 Twitter Thread 下面有很多讨论。

2、[.NET Conf 2024 Session Youtube播放列表](https://www.youtube.com/playlist?list=PLdo4fOcmZ0oXeSG8BgCVru3zQtw_K4ANY)

![image](https://github.com/user-attachments/assets/390b3863-2b27-47fb-ac88-05e129ab7b9e)

2024 年 .NET Conf 已经结束了，这里的 YouTube 列表包含了今年所有的视频内容。

## 文章推荐

1、[.NET 9 性能提升](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-9/)

![image](https://github.com/user-attachments/assets/0a14e040-4fb9-4143-86e2-010653df9aeb)

C#  中的 `Stopwatch` 类用来测量程序运行的耗时，通常的代码逻辑是这样的

```csharp
var sw = Stopwatch.StartNew()
// do your wok
sw.Stop();
Console.WriteLine($"Elapsed time: {sw.Elapsed.Microseconds} ms");
```

从性能角度来看，这段代码的问题是 `Stopwatch` 是要给 `class` 类型，每次都会在堆上分配一个对象，增加垃圾回收的负担。所以 `Stopwatch` 类提供了一个更加高效的实现

```csharp
long startTime = Stopwatch.GetTimestamp();
// do you work
var elapsedTime = Stopwatch.GetElapsedTime(startTime);
Console.WriteLine($"Elapsed time: {elapsedTime.Microseconds} ms");
```

该方法调用的系统函数中的 `tick` 的方法，然后根据不同的 `tick` 的差值，计算出中间经历的时间。这个过程中，不会进行任何堆内存分配。

2、[EF Core 代码性能优化建议](https://dev.to/antonmartyniuk/how-to-increase-ef-core-performance-for-read-queries-in-net-2fk9)

EF Core 查询优化建议：

1. 创建索引
2. 优化查询投影
3. 对于只读查询，使用 `AsNoTracking`
4. 使用积极加载 （Eager Loading）
5. 使用分页查询结果
6. 使用编译查询
7. 使用 `SplitQuery` 避免迪尔卡乘积爆炸
8. 使用原生 `Sql` 语句查询
9. 引入缓存

3、[OpenSSF 支持 .NET 生态](https://devblogs.microsoft.com/nuget/openssf-scorecard-for-net-nuget/)

![image](https://github.com/user-attachments/assets/c58f06ee-8342-4295-b051-d603188de54b)

OpenSSF Scorecard 是由开源安全基金会提供的安全评估工具。现在 `.NET` 的开源维护者可以使用 GitHub Action 生成项目的安全系数，该系数为 0 到 10. 越高表示项目的安全系数越高。它主要由下面几个用处

1. 明确安全危险
2. 提高软件质量
3. 赢得用户信任
4. 满足合规要求
5. 透明安全标准
6. 鼓励安全最佳实践
7. 阻止供应链攻击

该检查包含以下内容

1. 是否包含二进制文件
2. 是否代码分支保护
3. 是否有 CI
4. 是否有 OpenSSF 的最佳实践证书
5. 是否有代码评估
6. 是否有不同的贡献者
7. 是否有危险的 workflow
8. 是否依赖更新工具
9. 是否有 Fuzzing 工具
10. 是否有 License
11. 是否在维护中
12. 是否固定依赖版本
13. 是否打包
14. 是否包含安全政策
15. 是否签名发布
16. 是否有token 管理权限
17. 是否有未解决的安全隐患
18. 是否有 webhook

4、[ASP.NET Core 中的正确的异常处理](https://www.youtube.com/watch?v=-TGZypSinpw&ab_channel=NickChapsas)

在 [RFC 7807](https://datatracker.ietf.org/doc/html/rfc7807) 中，规定了可读的响应来避免更多的错误的内容规范。在 ASP.NET Core 的应用程序中可以优雅的方式完成这个要求。

1. 定义异常

对应不用的业务，可以定制不同的异常类型

```csharp
[Serializable]
public class ProblemException : Exception
{
    public string Error { get;}
    public string Message { get; }
    public ProblemException(string error, string message) : base(message)
    {
        Error = error;
        Message = message;
    }
}
```

2. 定义 `ExceptionHandler`

```csharp
public class ProblemExceptionHandler : IExceptionHandler
{

    private readonly IProblemDetailsService _problemDetailsService;

    public ProblemExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ProblemException problemException)
        {
            return true;
        }

        var problemDetails = new ProblemDetails();
        problemDetails.Status = StatusCodes.Status400BadRequest;
        problemDetails.Title = problemException.Error;
        problemDetails.Detail = problemException.Message;
        problemDetails.Type = "Bad Request";

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
            });
    }
}
```

3. 注册服务并且注册中间件

```csharp
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();
app.UseExceptionHandler();
```

4. 添加额外信息

```csharp
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});
```

这样在任何需要错误的地方，只需要抛出 `ProblemException` 即可。

5、[如何调试 LINQ](https://michaelscodingspot.com/debug-linq-in-csharp/)

![image](https://github.com/user-attachments/assets/d86f43e5-c840-4f21-88ac-cc2c35f31c91)

`Linq` 是 C# 语言中的特色，我们通常会使用链式的方式将一连串的 `Linq` 语句组合起来达到想要的目的。但是这种方法有一个问题是调试起来比较麻烦，那么有什么办法解决这个问题呢？

-  使用 Visual Studio 中的 QuickWatch

`QuickWatch` 是 `C#` 中高级调试功能，可以在断点处，右键选择 `Quickwatch` 方式，在 `QuickWatch` 中输入查询语句，可以动态显示查询结果。

![image](https://github.com/user-attachments/assets/d0c29a2d-96f6-41bc-b988-75879085dda1)

- 使用条件断点或者执行断点

当我们使用断点的功能的时候，可以选择断点发生的条件，或者每次执行的额外的操作，比如日志。

- 使用日志中间件

在 `Debug` 模式下，将调试信息输出到 `Debug` 窗口

```csharp
public static IEnumerable<T> LogLINQ<T>(this IEnumerable<T> enumerable, string logName, Func<T, string> printMethod)
{
#if DEBUG
    int count = 0;
    foreach (var item in enumerable)
    {
        if (printMethod != null)
        {
            Debug.WriteLine($"{logName}|item {count} = {printMethod(item)}");
        }
        count++;
        yield return item;
    }
    Debug.WriteLine($"{logName}|count = {count}");
#else   
    return enumerable;
#endif
}
```

- 使用 `OzCode` Linq 功能

`OzCode` 是 Visual Studio 的插件，它可以辅助我们进行 debug, 在 `Linq` 中，对于每个查询，可以显示其操作之后的数据集。

![image](https://github.com/user-attachments/assets/54b2ffe0-0355-41b4-9b5f-45d2d13b9b3e)

6、[揭秘2025年微软.NET长支持版本(LTS)](https://blog.inedo.com/dotnet/demystifying-lts/)

![image](https://github.com/user-attachments/assets/88956806-6e1a-4cb7-aa47-8e464f9fab7d)

提起 `.NET` 会涉及到下面这些平台， `.NET Framework`, `NET Core` , `.NET Standard` 。 微软为每个平台都有自己的生命周期，具体的详细细节如下：

NET Version | End of Support Date | Supported?
-- | -- | --
.NET Framework 1.0 – 4.6.1 | April 26. 2022 | ✘ Out of Support
.NET Framework 3.5 | January 9,2029 | 🆗 Supported for now
.NET Framework 4.6.2 | January 12, 2027 | 🆗 Supported for now
.NET Framework 4.7 – 4.72 | 2032+ (approx.) | ✅ Supported
.NET Framework 4.8 | Indefinite | ✅ Supported
.NET Framework 4.8.1 | Indefinite | ✅ Supported
.NET Core 1.0 – 3.1 | December 13, 2022 | ✘ Out of Support
.NET 5 | May 10. 2022 | ✘ Out of Support
.NET 6 | November 12. 2024 | ⚠ Supported (Ending Soon)
.NET 7 | May 14, 2024 | ⚠ Supported (Ending Soon)
.NET 8 | November 10, 2026 | ✅ Supported

7、[2024年不同语言需要多少内存来跑1百万并行任务？](https://hez2010.github.io/async-runtimes-benchmarks-2024/)

这是不同语言的协程的内存消耗情况对比，在 `C#` 中就是 `Task` 类型。比较的结果非常令人映像深刻，在一百万的协程的标准下，C# 或者 AOT 的内存消耗跟 Rust 这种原生字节的编程语言一致。

![image](https://github.com/user-attachments/assets/04c30670-0db8-4c03-b2a6-3a1414abf606)

8、[.NET集中式包管理](https://medium.com/@MilanJovanovicTech/central-package-management-in-net-simplify-nuget-dependencies-1f6c744f79d7)

如果你的项目解决方案中包含了很多 C# 工程项目，但是由于管理或者其他原因，同一个依赖包包含了不同的版本。这样会增加管理的复杂程度，比如当要升级版本的时候，需要更改所有的出现的地方。而且如果项目之间存在引用关系，会导致版本冲突的问题。所以 `.NET` 提供了一个集中包管理机制。首先在项目目录下创建一个 `Directory.Packages.props` 文件

```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="Newtonsoft.Json" Version="13.0.3" />
    <PackageVersion Include="Serilog" Version="4.1.0" />
    <PackageVersion Include="Polly" Version="8.5.0" />
  </ItemGroup>
</Project>
```
这样在项目中的 `csproj` 文件，就不需要制定版本
```xml
<ItemGroup>
  <PackageReference Include="Newtonsoft.Json" />
  <PackageReference Include="AutoMapper" />
  <PackageReference Include="Polly" />
</ItemGroup>
```
如果在项目中覆盖掉版本，可以 `VersionOverride` 属性

```xml
<PackageReference Include="Serilog" VersionOverride="3.1.1" />
```

当然也可以制定某个包让每个工程项目使用

```csharp
<ItemGroup>
  <GlobalPackageReference Include="SonarAnalyzer.CSharp" Version="10.3.0.106239" />
</ItemGroup>
```

## 开源项目

1、[UnitsNet](https://github.com/angularsen/UnitsNet)

UnitsNet 是  `C#` 中处理各种物理单位的库，比如以长度为例，有 `米`, `英尺`, `海里` 等等。通常我们需要查阅相关的资料来了解他们之间的转换关系，过程非常繁琐。但是 `UnitsNet` 库解决了我们的痛点，这样就不用在复杂的转换中引入错误。

```csharp
using UnitsNet;

Length length = Length.FromMeters(1);
Console.WriteLine(length);
Length length1 = new Length(2, UnitsNet.Units.LengthUnit.Meter);


System.Console.WriteLine($"cm: {length.Centimeters}");
System.Console.WriteLine($"m: {length.Meters}");
System.Console.WriteLine($"km: {length.Kilometers}");
System.Console.WriteLine($"in: {length.Inches}");
System.Console.WriteLine($"ft: {length.Feet}");
System.Console.WriteLine($"yd: {length.Yards}");
System.Console.WriteLine($"mi: {length.Miles}");
System.Console.WriteLine($"nmi: {length.NauticalMiles}");
```

1 m
cm: 100
m: 1
km: 0.001
in: 39.37007874015748
ft: 3.280839895013123
yd: 1.0936132983377078
mi: 0.0006213711922373339
nmi: 0.0005399568034557236

2、[.NET Debugging 开源书](https://michaelscodingspot.com/free-book/)

![image](https://github.com/user-attachments/assets/bc890975-8867-4583-a172-3a0274404c72)

![image](https://github.com/user-attachments/assets/246a518b-bd50-4149-aaf6-ab7392c1202b)

这是一本开源的 `.NET` 调试开源书籍，主要包含下面这些主题

- Advanced debugging techniques with Visual Studio
- .NET Core and .NET Framework on Windows, Linux, and Mac
- Performance issues
- Memory leaks and memory pressure issues
- ASP.NET slow performance and failed requests
- Debugging third-party code
- Debugging production code on the cloud
- Crashes and hangs

3、[使用 C# 构建 VB 6.0](https://github.com/BAndysc/AvaloniaVisualBasic6)

![image](https://github.com/user-attachments/assets/411a5935-c620-47cf-82b2-58c81283321a)

这是一个有意思的项目，使用 Avalonia 构建了 Visual Basic 6 的编辑器。

4、[ToastFish](https://github.com/Uahh/ToastFish)

![image](https://github.com/user-attachments/assets/2466aff7-6f34-4c03-b7d6-18f72fafa256)

使用 `.NET` 开发的软件，使用 Windows 10 的通知栏来背单词。

5、[TodoApi](https://github.com/davidfowl/TodoApi)

来自 `.NET` 社区的 David Fowler 的开源 `.NET` 示范项目，包含了两个部分

- Todo.Web: 使用 ASP.NET Core 的 Blazor WASM 前端
- Todo.Api: 使用 ASP.NET Core API 后端和 Minmal APIs

通过这个简单项目，可以学习相关最佳实践。

6、[commandline](https://github.com/commandlineparser/commandline)

Commandline 是一个开源包，用来完成命令行应用程序中的参数，它有如下特色

1. 支持 `.NET Framework 4+`, `.NET Standard`, `Mono` 和 `.NET Core`
2. 没有其他库的依赖
3. 只有一个重要函数 `CommandLine.Parser.Default.ParseArguments(...)` 来解析参数请求
4. 支持各种命令行参数形式

```csharp
class Program
{
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.Verbose)
                       {
                           Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                       }
                       else
                       {
                           Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                       }
                   });
        }
 }
```

7、[FileSignatures](https://github.com/neilharvey/FileSignatures)

![image](https://github.com/user-attachments/assets/419227ff-e062-468f-89e0-6d824dfe35a2)

我们在判断一个文件的类型的时候，不能只看文件的后缀名。但是每种文件的都会有一种特殊的字节来表明其类型，也叫做魔数（Magic Number)。 `FileSignatures` 这个开源库实现了大部分常见的类型，并且可以读取相关字节来判断类型。

```csharp
var inspector = new FileFormatInspector();
var stream = new FileStream("test.doc", FileMode.Open);
var format = inspector.DetermineFileFormat(stream);
Console.WriteLine(format.Extension);
```

比如这个文件的拓展类型是 `doc` 类型，但是实际上是 `xlsm` 类型文件，这个库可以精确的检测出来。