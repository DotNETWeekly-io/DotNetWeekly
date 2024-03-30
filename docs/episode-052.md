# .NET 每周分享第 52 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/5e208358-5523-4dbe-8806-99ee8729a2f2)

[garent](https://github.com/microsoft/garnet) 是微软研究院推出的内存 `key-value` 存储系统，它可以完全兼容 `Redis` 的所有协议，而且在性能上比 `Redis` 还要好，而且这个完全是使用 `C#` 开发，只需要 执行 `dotnet run -c Release -f net8.0` 命令就可以将服务端运行起来。 最近 `Redis` 也修改协议，或许这是 `garent` 的一个机会。

## 文章推荐

1、[ASP.NET Core Scoped Service](https://www.youtube.com/watch?v=FSjCGdkbiCA&ab_channel=MilanJovanovi%C4%87)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a77dfd6f-a8bf-45b2-87b0-49e518697a81)

在`ASP.NET Core`中，依赖注入有三种类型的服务，分别为`Transient`，`Scoped`和`Signleton` ，其中对于 `Transient` 和 `Singleton` 比较容易理解，而且如果高级别的服务依赖于低级别的服务，程序在运行的时候会抛出异常，比如 `Singleton` 的服务依赖于一个 `Transient` 的服务。

对于 `Scoped` 服务，该怎么解决这个问题呢，比如

```csharp
internal class WeatherReport (
    ILogger<WeatherReport> logger,
   WeatherSerivce weahtherService) 
: BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
}

builder.Services.AddHostedService<WeatherReport>();
builder.Services.AddScoped<WeatherSerivce>();
```

这里的 `WeatherReport` 是一个注册为 `singleton` 的服务，而 `WeatherService` 是注册为 `Scoped` 类型的服务，所以在运行的时候就会抛出异常。该怎么解决这个问题呢？

```csharp
internal class WeatherReport (
    ILogger<WeatherReport> logger,
    IServiceScopeFactory serviceScopeFactory) 
: BackgroundService
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
            using var scope = serviceScopeFactory.CreateScope();
            var weatherService = scope.ServiceProvider. GetRequiredService<WeatherSerivce>();
    }
}
```
在这里，`WeatherReport` 依赖了一个 `ISeviceScopeFactory` 对象，通过创建一个 `IServiceScope` 对象，就能获得 `WeatherService` 的服务，并且在执行完毕了，`IServiceScope` 对象会调用 `Dispose` 方法释放当前创建的对象。

那么在 `ASP.NET Core` 中什么时候会使用 `IServiceScope` 呢？答案是每一次 `HTTP` 请求的处理声明周期内，会分享这个 `IServiceSecope` 对象，比如

```csharp
public class RequestLoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, AppDbContext appDbContext)
    {
        appDbContext.ApiRequests.Add(new ApiRequest(context.TraceIdentifier));
        await appDbContext.SaveChangesAsync();
        await next(context);
    }
}

app.MapGet("current-request", async (HttpContext httpContext, AppDbContext dbContext) =>
{
    var entity = dbContext.ApiRequests.Local.FindEntry(httpContext.TraceIdentifier);

    return entity?.Entity;

});

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("db"));
```

在这里，`AddDbContext` 会将 `AppDbContext` 注册为一个 `Scoped` 类型的服务，那么每个请求中间件中看到的 `AppDbContext` 都是同一个对象。

2、[集合表达式](https://blog.jetbrains.com/dotnet/2024/03/26/collection-expressions-using-csharp-12-in-rider-and-resharper/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6c59a66f-2003-42d8-88a3-fff9ae3875aa)

`C#  12` 引入了集合表达式，它简化了集合的字面表达方式，比如之前

```csharp
var array = new[] { 1, 2 };
var spread = array.Concat(new[] { 3, 4 });
```

那么现在就可以这么

```csharp
int[] array = [1, 2];
int[] spread = [..array, 3, 4];
```

注意由于 `C#`集合是强类型的，所以不能使用 `var` 方式去定义。我们都知道这些背后都是编译器帮助实现的，那么可以借助 [sharplab.io](https://sharplab.io/) 展示生成 `C#` 代码。而且也可以自定义类型来支持集合表达式。

3、[Rust for .NET](https://microsoft.github.io/rust-for-dotnet-devs/latest/introduction.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/859caeae-c198-4c82-b9fa-72e7c113070e)

这是微软开源的一本 `Rust` 语言学习手册，主要是针对已经熟悉 `.NET` 开发的程序员。

4、[.NET 开发者期望社区毁灭？](https://aaronstannard.com/dotnet-eventing-backslide/)

最近 `.NET 9 ` 开发计划中的一个 [Epic](https://github.com/dotnet/aspnetcore/issues/53219) 引来了巨大的讨论，它主要讨论是在 `.NET 9` 中引入事件框架。但是 `.NET` 社区中已经包含了很多这样的框架。这个给开发人员带来的疑问：为什么微软作为第一方为什么要吃掉开源社区的市场？
这篇文章的作者分析了这种现象，并且认为这种做法是伤害 `.NET` 社区的生态。

5、[MSBuild 新体验](https://devblogs.microsoft.com/visualstudio/experimental-msbuild-editor/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0e072bd2-641f-482e-9939-8a049c543741)

MSBuild 是 `.NET` 生态中非常重要的组成部分，在 `C#` 项目中体现为 `csproj` 文件。不管是经验丰富还是萌新，对于直接修改 `csproj` 文件都是一个充满挑战的事情。Vistual Studio 最近为 `csproj` 文件编辑提供了新的功能，更多的智能提示和补全，已经错误提示。

6、[Stephen Toub 教你从头开始写异步](https://www.youtube.com/watch?v=R-z2Hv-7nxk&ab_channel=dotnet)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3caf7e22-679b-4264-a954-c5507534106a)

`.NET` 社区的大佬 `Stephen Toub` 教你从头写一个异步实现，这个将会帮助你更加容易理解 `C#` 的异步。

## 开源项目

1、[spectre.console](https://github.com/spectreconsole/spectre.console)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/88d9ff95-ad2e-4301-ac08-5db3431b31fd)

Spectre.Console 是 `dotnet` 的一款开源库，它能够绘制出非常漂亮的控制台交互页面。

2、[dotnet-outdated](https://github.com/dotnet-outdated/dotnet-outdated)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/8811f3b7-adca-475a-a136-d6762f56ee6a)

`dotnet-outdated` 是一款检测 `dotnet` 项目的依赖是否出现过时的情况。安装只需要执行 `dotnet outdated` 命令，就可以知道那些包出现了最新的版本。

3、[WireMock.Net](https://github.com/WireMock-Net/WireMock.Net)

`WireMock-Net` 是一款模拟服务端的库，这样可以在单元测试或者集成测试中访问远端的 HTTP 服务器，并且设定相应的返回结果。
