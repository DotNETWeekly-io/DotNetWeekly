# .NET 每周分享第 36 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c9d52e73-b127-4e8c-b5f6-79389a2ac9c5)

今年 [Microsoft Build](https://build.microsoft.com/en-US/home) 大会在 5 月份召开，其中最重要的关键词就是 `AI` 和 `Copliot`。当然对于 `.NET`也是有涉及，主要是在性能上的提升，还介绍了 Windows11 的 `DevHome`， 可以非常方便开发者进行环境搭建，开发机器性能监控和 `GitHub` 看板功能。

## 行业资讯

1、[Fleet C# 支持更新](https://blog.jetbrains.com/dotnet/2023/05/04/csharp-support-in-fleet-solution-view-unit-testing-and-more/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0349a44d-94a0-4e75-9f97-933f4c60c182)

JetBrains 更新了 Fleet 对 C# 支持的特性，主要包括

1. 解决方案视图
2. Build/Rebuild/Clean 操作
3. 单元测试

2、[Visual Studio UI 更新](https://devblogs.microsoft.com/visualstudio/visual-studio-ui-refresh/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/69d07887-7763-4795-be3c-6e1613f12630)

Visual Studio 团队打算遵循 `Fluent UI` 的为 VS 重新设计 UI 界面，主要处于下面三个考虑

- 一致性
- 可访问行
- 效率

下拉菜单对比：

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9194ca38-5a64-4004-885a-8ef0b00a8ec2)

2、[Visual Studio Sticky 滚动](https://devblogs.microsoft.com/visualstudio/sticky-scroll-stay-in-the-right-context/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0a5d58fd-3f39-48fb-920f-4f07c07d9a88)

`Sticky` 是一个非常使用的功能，非常类似在 `Excel` 中将表头固定，然后向下翻动行。在编写代码的时候，有很多 `for`, `foreach` 等循环，如果循环体非常长，或者存在嵌套循环，那么这个功能还是非常有用的。

## 文章推荐

1、[NET 8 中 NativeAOT 编译](https://www.youtube.com/watch?v=G5RiC9qQNvw&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a70c58c5-7b50-4e49-9b5a-bb2d32f79603)

.NET 8 中支持 `Native AOT` 编译，也就是说编译出来的应用程序就是原生的应用程序，而不是需要 `.NET` 运行时才能的执行，跟 `Rust` 和 `Go` 一样。
只需要在 `csproj` 文件中配置 `<publishAot>true<publishaot>` 即可。

2、[C# IDisposable 和 Go defer 的区别](https://blog.cellfish.se/2023/05/go-for-c-developers-defer-is-not.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c2d120db-6ddc-4052-9d7b-237dbe15d393)

`C#` 和 `Go` 中有两个非常相似的功能叫，分别为 `IDisposable` 和 `defer`， 通常是在方法执行完毕后，执行一些清理工作。比如关闭开打的文件流和网络 `socket`。但是这两者还是有一些区别

- `IDisposable` 并不保证的 `Dispose` 方法肯定被调用，除非放在 `using` 的语句块中，而且变量离开了作用域就会执行。
- `defer` 只会在函数 `return` 之后才会执行，而且肯定会执行。

3、[正确的输出日志](https://www.youtube.com/watch?v=PvQGVmozCdU&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/1d7b4497-959a-4e67-8bf2-dd07ac54fb92)

日志是应用程序中不可或缺的的组成部分，随着 `Microsoft.Extensions.Logging` 扩展包推出，不同的日志输出源只要配置好 `LoggerProvider` 就可以了。 `ILogger` 的 `LogInformation`, `LogWarning` 方法就是公开的接口，方法签名是这样的

```csharp
/// <summary>
/// Formats and writes an informational log message.
/// </summary>
/// <param name="logger">The <see cref="ILogger"/> to write to.</param>
/// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
/// <param name="args">An object array that contains zero or more objects to format.</param>
/// <example>logger.LogInformation("Processing request from {Address}", address)</example>
public static void LogInformation(this ILogger logger, string? message, params object?[] args)
```

请注意第三个参数 `message`, 但是注释中却是 `message template format`。所以我们的日志是应该这样

```chsarp
logger.LogInformation("data: {Data}, {now}", Data, DateTime.Now);
```

而不是这样

```csharp
logger.LogInformation($"data: {Data}, {DateTime.Now}");
```

应为日志库是将模板缓存下来，而不是在输出的时候，就开始构造日志字符串。benchmark 测试

```csharp
[MemoryDiagnoser]
public class BenchmarkLogging
{
    public static readonly ILogger logger;

    public static readonly Random random;

    private int Data => random.Next();

    static BenchmarkLogging()
    {
        logger = new LoggerFactory().CreateLogger<BenchmarkLogging>();
        random = new Random();
    }

    [Benchmark]
    public void Templated()
    {
        logger.LogInformation("data: {Data}, {now}", Data, DateTime.Now);
    }

    [Benchmark]
    public void Interpolated()
    {
        logger.LogInformation($"data: {Data}, {DateTime.Now}");
    }

    [Benchmark]
    public void Formatted()
    {
        logger.LogInformation(string.Format("data: {0}, {1}", Data, DateTime.Now));
    }
}
```

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/48fb7cbb-55fa-4ab2-80f0-785ef6964d63)

可以看出，模板方法在性能和内存方面都优势。

4、[.NET 云应用程序](https://www.infoq.com/presentations/net-apps-cloud/)

[![image](https://dotnetweeklyimages.blob.core.windows.net/035/openai.png)](https://www.infoq.com/presentations/net-apps-cloud/)

最近 `InfoQ` 组织了一场讨论 `.NET` 和云应用迁移的讨论，主要包含下面主题

- `.NET` 开发者在面对云的主要痛点
- 最小的迁移成本
- 当迁移到云之后，下一步该怎么办
- 选择 `Serverless` 还是 `Kubernetes`
- 托管容器平台
- 除了 `App Service` 还有其他最有选择
- ....

## 开源项目

1、[Visual Studio 环绕选择](https://github.com/madskristensen/Surrounder)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/246dc60e-3ebb-4a38-b61e-32d65c7215d8)

在编辑器中，我们常常需要一个功能就选择为一段文本或者代码添加双引号，括号等等。通常我们的操作是分别找到开始位置和结束位置，然后分别输入开始字符和结束字符。`Surrounder` 插件可以帮助我们方便的完成，只要选择该文件，然后输入开始字符，结束字符会自动开结尾添加上。
