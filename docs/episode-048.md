# .NET 每周分享第 48 期

## 卷首语

Happy New Year.

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/244e65c7-fded-486f-8c7c-34e5b158b4c4)

[Tiobe](https://www.tiobe.com/tiobe-index/) 发布了年度语言， `C#` 以 +1.43% 的增长率荣获 2023 年度开发语言。

## 文章推荐

1、[按条件加载中间件](https://blog.elmah.io/conditionally-add-middleware-in-asp-net-core/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/42527584-a5dd-42e6-b20d-8be74e6d40ae)

在 ASP.NET Core 应用程序中，我们通常注册中间件的方式定义自己的业务开发逻辑，如果有些中间你需要在满足特定的条件的时候才会执行，那么可以使用 `UseWhen` 的拓展方法。

中间件

```csharp
internal class TimingMiddleware
{
    private readonly RequestDelegate _next;

    public TimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopWatch = Stopwatch.StartNew();

        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-took"] = $"{stopWatch.ElapsedMilliseconds} ms";
            return Task.CompletedTask;
        });

        await _next(context);
    }
}
```

注册中间件

```csharp
app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/api"),
    builder => builder.UseMiddleware<TimingMiddleware>()
);
```

这里只有请求的 `path` 中包含了 `api` 才会执行 `TimingMiddleware`。从源码上看，它是通过创建一个新的 `IApplication` ，在条件满足的时候使用新的  `IApplication` 执行。

2、[WPF 已经死了](https://avaloniaui.net/Blog/is-wpf-dead)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/194e1aa9-ec48-468a-ad65-d14320d0ce54)

WPF 是 Windows 平台上开发桌面应用程序的利器，随着 `.NET` 的开源和 `WPF` 并不支持其他桌面系统，WPF  的生态前景不容乐观，和 `Avalonia` 比起来，不管是开源社区的积极性和下载量，都有取代的可能。

3、[SearchValue](https://endjin.com/blog/2024/01/dotnet-8-searchvalues-string-search-performance-boost)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c6cbaef5-d5ab-4b75-8970-2b17dec13589)

.NET 一直致力于性能提升，有一种性能提升是 “先付款，再收益”的模式。那么“付款”的形式有两种

1. 在编译的时候，比如 build task 或者 source generator
2. 在运行的时候，存在冷启动的问题

.NET 8 中的 `SearchValue` 就是第二种。

4、[var 关键字](https://www.reddit.com/r/dotnet/comments/18o1zsx/is_using_var_okay/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a567cd8e-764b-4239-b707-e3345b3f25c3)

这是 Reddit 上的一个讨论

> 使用 `var` 关键字是否可行

原帖作者认为 `var` 关键字使得变量类型变得不清晰。但是我认为 `var` 类型是不错的类型

1. 使用 IDE 可以很方便的查看
2. 不适用 var， 是的一些代码的长度变得非常长，比如 `List<int> list = new List<int>()`
3. 在 `Linq` 中，var 可以避免写一些隐式类型，比如 `var obj = list.Select(i => new { Val = i; Name = i.ToString()})`

5、[.NET 平台密码管理最佳实践](https://dev.to/asimmon/evolutive-and-robust-password-hashing-using-pbkdf2-in-net-34pc)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a2595635-b466-4474-ac74-f191f0d1519b)

在密码存储过程中，最重要的一点的是不能存储用户密码的原文，因为哪怕是数据库泄露，也不会产生密码泄露的问题。在安全领域还有一个重要原则是不能自己发明安全领域的算法。`.NET` 提供了 `PBKDF2` 算法来提供加密，而且在 `ASP.NET Core Identity` 包中使用。
整个流程包含四个步骤：

1. 用户提供密码
2. 使用一个 Salt 
3. 使用一个伪随机算法比如 SHA-256
4. 迭代若干次

代码如下

```csharp
var password = "Correct Horse Battery Staple"u8;
const int keySize = 256 / 8;
var salt = RandomNumberGenerator.GetBytes(keySize);
const int iterations = 600000;
var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, keySize);
```

6、[关闭 DbConnection?](https://khalidabuhakmeh.com/what-should-i-dispose-with-dotnet-database-connections)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/fe51f4ae-3668-4a8f-be8f-b099bb890a9d)

在 database 中我们会涉及到 `DbConnection`, `DbCommand` 和  `DbReader` , 它们都有 `Close` 的方法用来释放相应的资源，那么我们需要依次调用它们吗？本文作者查看了 `SqliteConnection` 的实现，发现在调用 `DbConnection` 的 `Close` 方法会关闭相关的 `DbCommand`, 而调用 `DbCommand` 的 `Close` 方法，也会调用关联的的 `DbReader` 的 `Close` 方法，所以没有必要依次调用它们的 `Close` 方法。

## 开源项目

1、2023 .NET 开源统计

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/ef953a38-aca5-4437-bdef-43d8ec2ff9d6)
![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6cac978e-6df3-43bd-ba50-72234c93237c)

2、[2024 年 .NET 技能提升](https://github.com/milanm/DotNet-Developer-Roadmap)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/81b576cf-748e-40f1-8e75-7418ca327a8a)

新的一年，查看一下这个开源项目列出的 .NET 技能提升清单.

3、[TestContainer](https://dotnet.testcontainers.org/)

`TestContainer` 是一个集成测试的工具，我们都知道单元测试是应该不依赖外部服务，比如数据库，网络。我们都需要在单元测试中将他们抽象。但是我们在集成测试中还是需要完整的测试他们，所以 `TestContainer` 可以借助容器化工具完成完整的测试。

4、[One Billion Row Challenge](https://github.com/buybackoff/1brc#results)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/7c94f7d6-f5fe-4863-8525-b0c325de5a4d)

在 GitHub 上出现了一个有趣的挑战，使用任何编程语言读取十亿行的文件，该文件的内容是一个气象站获取的温度采样点，输出的内容是该气象站的平均温度，最低温度和最高温度。可以使用任何优化的方法，让这个程序在最短的时间内完成。这个repo 包含了 `.NET` 编程语言的实现。