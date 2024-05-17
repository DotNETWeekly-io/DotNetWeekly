# .NET 每周分享第 29 期

## 卷首语

[新版本的 .NET GC 出问题了？](https://maoni0.medium.com/is-the-regression-in-gc-or-something-else-38f10018dd21)

![image](https://user-images.githubusercontent.com/11272110/209459788-71ad31a6-3b3c-445e-ba55-610c854593a0.png)

当我们的 `.NET` 应用程序从 `X` 版本升级到 `Y` 版本，如果发现了在 `GC` 上表现出现了 `Regression`, 事否怀疑 `.NET GC` 模块导致的呢？ `Maoni` 这篇文章介绍了如何解决这个问题。

只从 `.NET Core 2.0` 开始，`.NET` 应用程序可以自行加载其他 `GC` 实现。所以在遇到 `.NET GC` 发生 regression 的时候，可以配置 `clrgc.dll` 开进行替换。

```json
{
  "runtimeOptions": {
    "configProperties": {
      "System.GC.Name": "clrgc.dll"
    }
  }
}
```

或者使用环境变量

```shell
set DOTNET_GCName=clrgc.dll
```

如果 GC 仍然出现问题，则需要像 `.NET` 团队提出 issue，否则通过常规的 GC 问题分析方法来分析应用程序。

## 行业资讯

1、[Visual Studio 添加文件的功能优化](https://devblogs.microsoft.com/visualstudio/adding-new-files-just-got-a-lot-faster/)

![image](https://user-images.githubusercontent.com/11272110/209459014-31ac1e5a-fdf9-4251-be29-eff92b66602c.png)

`Visual Studio` 更新的了 `Add New File` 功能，不再弹出一个包含全部模板的对话框，而是直接一个简单的对话框，只要输入文件名即可创建，它包含了这些特色

- 无需浏览模板
- 支持嵌套文件夹
- 支持任何文件拓展名
- 支持多个文件创建

2、[Visual Studio 支持 markdown](https://devblogs.microsoft.com/visualstudio/write-markdown-without-leaving-visual-studio/)

![image](https://user-images.githubusercontent.com/11272110/209460252-6c5054f8-3a11-4e36-aa9e-d14a7f4ebb51.png)

`Markdown` 是广泛使用的格式，在过去很长一段时间，`Visual Studio` 不支持这种格式。最新版的 `Visual Studio` 将会改变这个现状。

## 文章推荐

1、[IL 语言的 Call 方法解析](https://washi.dev/blog/posts/confusing-decompilers-with-call/)

![image](https://user-images.githubusercontent.com/11272110/209420615-d944fbcd-331a-4d45-b930-c6627a62c5d3.png)

`call` 和 `callvirt` 是 IL 代码中常见的两个调用方法，那么这两者的区别在哪呢？这篇给文章带你分析其中的奥秘。

2、[Required Member 揭秘](https://blog.ndepend.com/c-11-required-members)

`Required` 修饰符是 `C# 11` 引入的，它的主要目的是为了让某些字段或者属性在构造的时候，必须要进行初始化，否则编译器会报错，这篇文章详细介绍这个新的功能。

3、[Stopwatch 高效使用](https://www.youtube.com/watch?v=NTz99yN2urc&ab_channel=NickChapsas)

![image](https://user-images.githubusercontent.com/11272110/209458142-a9113afb-8081-4765-814f-5959e1108bc2.png)

`Stopwatch` 是 C# 编程中广泛使用的类，它用来记录操作的耗时，比如

```C#
var stopwatch = Stopwatch.StartNew();
//.....
stopwatch.Stop();
Timespan ts = stopwatch.Elapsed;
```

对于简单的应用程序，这样没有任何问题。但是 `Stopwatch` 是一个 `Class` 类型，也就是说每次操作都会在堆上分配一个空间。对于 `hot path` 路径中的代码，对内存压力还是比较大。

`Stopwatch` 类中有一些静态方法，它可以避免内存的分配

```csharp
var start = Stopwatch.GetTimestamp();
///
var elapse = Stopwatch.GetElapsedTime(start);
```

如果更近一步，将它们封装一个 `ValueStopwatch` 结构

```csharp
public struct ValueStopwatch
{
    private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;
    private long _startTimestamp;
    public bool IsActive => _startTimestamp != 0;
    private ValueStopwatch(long startTimestamp)
    {
        _startTimestamp = startTimestamp;
    }
    public static ValueStopwatch StartNew() => new ValueStopwatch(Stopwatch.GetTimestamp());
    public TimeSpan GetElapsedTime()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
        }
        long end = Stopwatch.GetTimestamp();
        long timestampDelta = end - _startTimestamp;
        long ticks = (long)(TimestampToTicks * timestampDelta);
        return new TimeSpan(ticks);
    }
}
```

4、[快速将多个工程文件添加到解决方案中](https://ardalis.com/add-all-projects-to-solution/)

![image](https://user-images.githubusercontent.com/11272110/209458668-b5db63bc-92e2-463a-b8a7-b7aac3f463d4.png)

`dotnet sln` 命令可以将动态将工程文件 (`csproj`) 文件添加到解决方案中，但是如果工程文件特别多，那么怎么使用 `dotnet` 命令添加到解决方案中呢？

答案是可以通过这个命令

```shell
dotnet new sln -n Everything
dotnet sln ./Everything.sln add (ls -r **/*.csproj)
```

5、[高效组织 Minimal API](https://www.milanjovanovic.tech/blog/how-to-structure-minimal-apis)

![image](https://user-images.githubusercontent.com/11272110/209458783-c12870e4-025b-44a4-8c3a-671932f7fa6a.png)

`Minimal API` 是 `.NET 6` 推出的新功能，通过 `IEndpointRouteBuilder` 接口的拓展方法可以代替 `Controller` 类。但是这样带来一个问题，如果 `API` 特别多，那么单个文件中代码会非常多。

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/products", async (AppDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Products.ToListAsync());
});
app.MapPost("/products", async (Product product, AppDbContext dbContext) =>
{
    dbContext.Products.Add(product);
    await dbContext.SaveChangesAsync();
    return Results.Ok(product);
});
app.Run();
```

一种解决方案是通过拓展方法，将一组 `API` 封装成拓展方法

```csharp
public static class ProductsModule
{
    public static void RegisterProductsEndpoints(this IEndpointRouteBuilder  endpoints)
    {
        endpoints.MapGet("/products", async (AppDbContext dbContext) =>
        {
            return Results.Ok(await dbContext.Products.ToListAsync());
        });
        endpoints.MapPost("/products", async (Product product, AppDbContext dbContext) =>
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return Results.Ok(product);
        });
    }
}
```

另外一种方法是通过 [Carter](https://github.com/CarterCommunity/Carter) 库

```csharp
public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (AppDbContext dbContext) =>
        {
            return Results.Ok(await dbContext.Products.ToListAsync());
        });
        app.MapPost("/products", async (Product product, AppDbContext dbContext) =>
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return Results.Ok(product);
        });
    }
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
var app = builder.Build();
app.MapCarter();
app.Run();
```

6、[Visual Studio 支持 JSON 转换为类](https://learn.microsoft.com/en-us/visualstudio/ide/reference/paste-json-xml?view=vs-2022)

![image](https://user-images.githubusercontent.com/11272110/209460379-cba2433d-1a86-4bc2-9271-077581b74218.png)

在应用程序开发过程中，有时候我们需要将 `JSON` 表示为 C# 的应用程序中的类。`Visual Studio` 可以自动将拷贝的 `JSON` 字符串转换成一个类。

## 开源项目

1、[函数式编程](https://learn.microsoft.com/zh-cn/shows/c9-lectures-erik-meijer-functional-programming-fundamentals)

![image](https://user-images.githubusercontent.com/11272110/209458122-5842c253-23e4-4063-aec7-04830027302f.png)

Erik Meijer 博士介绍的函数式编程课程。

2、[.NET 定时任务库](https://www.quartz-scheduler.net/)

![image](https://user-images.githubusercontent.com/11272110/209459316-3f93962f-332c-4a73-82eb-e83f5bb850df.png)

`Quartz.NET` 是 `.NET` 社区的开源任务调度库。
