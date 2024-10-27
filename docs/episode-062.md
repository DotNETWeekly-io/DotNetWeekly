# .NET 每周分享第 62 期

## 卷首语

![image](https://github.com/user-attachments/assets/5738ef1b-5e42-44a5-b63a-9548705ec998)

最近 `Rider` 集成开发环境软件宣布非商业用户免费试用。`Rider` 是 `JetBrains` 公司推出的一款跨平台的 .NET 开发工具，支持 `C#`、`VB.NET`、`F#`、`ASP.NET`、`ASP.NET Core`、`Xamarin` 等技术。`Rider` 采用了 `ReSharper` 引擎，提供了强大的代码分析、重构、代码生成等功能。这个消息非常有助于 `.NET` 开发者，尤其是 `Mac` 和 `Linux` 用户。

## 行业资讯

1、[Unity .NET 现代化的计划](https://discussions.unity.com/t/coreclr-and-net-modernization-unite-2024/1519272)

![image](https://github.com/user-attachments/assets/dca8891b-396d-4c2a-a6f9-972558396957)

`Unity` 官方介绍了 `Unity` 和 `.NET`  结合的发展规划

- 和 `.NET 8 CoreCLR` 集成
- Brust 集成
- MSBuild 集成
- 代码重载支持

当让这部分工作还没有一个具体的时间表。

## 文章推荐

1、[.NET 9 性能提升](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-9/)

![image](https://github.com/user-attachments/assets/54301e5b-9d15-456f-b308-56705eb37f93)

几乎每年 `.NET` 版本发布的时候，`Stephen Toub` 都会发布一篇博客关于 新的 `.NET` 运行时的性能提升。今年也不例外，`.NET 9` 在性能方面也有显著的提升。

2、[Option Monad](https://www.youtube.com/watch?v=Phn2Mu8zoxM&ab_channel=MilanJovanovi%C4%87)

![image](https://github.com/user-attachments/assets/ef05e1d6-cd95-4d79-9c33-5aef2789dc1b)

`Option Monad` 是一种编程范式，它可以解决编程中 `Null` 问题，在 `C#` 中我们可以使用 `nullable` 类型来解决这个问题，但是 `Option Monad` 已经广泛使用在其他编程语言中。

首先我们定义个 `Mayb<T>` 的类型

```csharp
public abstract class Maybe<T>
{
    private Maybe()
    {

    }

    public static Maybe<T> None => new NULL();

    private sealed class NULL : Maybe<T>
    {

    }

    public sealed class Some : Maybe<T>
    {
        public Some(T value) => Value = value;
        public T Value { get; }
    }

    public bool TryGetValue(out T value)
    {
        if (this is Some some)
        {
            value = some.Value;
            return true;
        }
        else
        {
            value = default(T);
            return false;
        }
    }

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        if (this is Some s)
        {
            return some(s.Value);
        }
        else
        {
            return none();
        }
    }

    public void Match(Action<T> some, Action none)
    {
        if (this is Some s)
        {
            some(s.Value);
        }
        else
        {
            none();
        }
    }
}
```

这里 `Maybe<T>` 是一个抽象类型，而 `Some<T>` 和 `Null<T>` 是两个具体的子类，而且定义 `None` 的静态属性为 `Null<T>`的具体实现。除此之外，还定义了 `TryGetValue` 和 `Match` 两个方法方便使用。

这样我们的方法定义的返回值如下

```csharp
Maybe<string> GetLogContent(int id)
{
    var filename = $"log{id}.txt";
    if (File.Exists(filename))
    {
        return new Maybe<string>.Some(File.ReadAllText(filename));
    }
    else
    {
        return Maybe<string>.None;
    }
}
```

那么使用的使用有很多方法

```csharp
var content = GetLogContent(1);

// Type Cast
if (content is Maybe<string>.Some some)
{
    Console.WriteLine(some.Value);
}
else
{
    Console.WriteLine("No content found");
}

// TryGetValue
if (content.TryGetValue(out var value))
{
    Console.WriteLine(value);
}
else
{
    Console.WriteLine("No content found");
}

// Match
content.Match(
    some: value => Console.WriteLine(value),
    none: () => Console.WriteLine("No content found")
);
```

3、[Visual Studio 断点管理](https://devblogs.microsoft.com/visualstudio/organize-your-breakpoints-like-a-pro/)

![image](https://github.com/user-attachments/assets/e5f01d43-872d-4b95-8bc5-15c673f2ead8)

Breakpoint Group 是 Visual Studio  新的功能，它可以将一系列的 breakpoint 合并起来，然后统一管理。

4、[.NET 字典解剖](https://dunnhq.com/posts/2024/anatomy-of-the-dotnet-dictionary/)

`Dictionary<KT, VT>` 类型是 `C#` 中广泛使用的类型，这边文章通过源码，详细介绍了具体的实现，主要包含字典类型的初始化，添加，读取，以及当发生冲突的时候处理逻辑。

5、[和 Stephen Toub & Scott Hanselman 学习并行编程](https://www.youtube.com/watch?v=18w4QOWGJso&ab_channel=dotnet)

Scott Hanselman 和 Steven Tobe 讨论了在 .NET 应用中使用并行处理来优化性能，重点讲解了线程、任务和并行结构的性能优化技术。他们首先解释了并行处理的基础知识，即通过多核同时运行多个任务以缩短处理时间，并回顾了 .NET 任务并行库（TPL）的发展，特别是 Parallel.For 和 Parallel.ForEach 简化了工作负载管理，使并行处理更易于开发人员使用。

视频中的重点之一是避免“伪共享”。这是一个性能问题，当线程访问存储在一起的数据时，导致 CPU 缓存行冲突，引发效率低下。为避免此问题，建议采用缓存行优化技术，并平衡工作负载，确保任务在核心之间均匀分配。

他们还介绍了 Partitioner 类，帮助将任务高效分配到多个线程，提升高需求应用的性能。为调试并行代码，Visual Studio 工具可以跟踪线程执行情况，帮助开发人员发现性能瓶颈。Scott 强调，分析和测试对于微调并行实现至关重要，以确保性能优化的效果最大化。

6、[EF Core 中优雅的删除数据](https://www.youtube.com/watch?v=Uj_BijUStqw&ab_channel=NickChapsas)

![image](https://github.com/user-attachments/assets/574dc060-fa0c-4de8-9d43-d0b37f4130a7)

在数据库中，如果从要删除一条数据，其中的耗时是非常大的，因为涉及到物理的 I/O, 也要重新构建索引。通常我们会在数据库表中增加一个 `IsDeleted` 字段，在删除的时候，直接将该字段设置为 `true`，那么在后续查询中，跳过改字段为 `true` 的数据。

`EF Core` 中有一种更加优雅的方法。

1. 在 `OnModelCreating` 方法添加全局 `filter`. 

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
     modelBuilder.Entity<Person>().HasQueryFilter(x => !x.IsDeleted);
}
```

2. 在查询的过程中自动添加改查询条件

```csharp
[HttpGet]
[Route("api/people/list")]
public async Task<ActionResult> Get()
{
    var people = await _context.Persons.ToListAsync();
    return Ok(people);
}
```

3. 忽略全局查询条件

```csharp
[HttpGet]
[Route("api/people/all")]
public async Task<ActionResult> GetAll()
{
    var people = await _context.Persons.IgnoreQueryFilters().ToListAsync();
    return Ok(people);
}
```

7、[.NET 8 和 9 迁移](https://www.mobilize.net/blog/net-8-vs-net-9-whats-the-difference-and-should-you-migrate)

![image](https://github.com/user-attachments/assets/cfa0a8d6-d2e2-4ceb-95bf-3cf78a9ae6e1)

`.NET 9`  在下个月就正式发布，当对于 `.NET 8` 有值得升级吗？
首先 `.NET 8` 有什么新的内容

- Native AOT
- Performance 提升
- 云原生
- Blazor 优化

那么 `.NET 9` 有下面的提升

- 更多的性能优化
- 新的语言功能
- 提高的异步编程模型
- 持续的云原生开发体验
- 安全方面提升
- 专注 AI 和机器学习

那么升级到 `.NET 9` 是否值得呢？

- 新的功能
- 性能提升
- 长期的技术支持

所以升级到 `.NET 9` 是一个值得尝试的原则。

## 开源项目

1、[EFCorePowerTools](https://github.com/ErikEJ/EFCorePowerTools)

EF Core Power Tools 是 `Visual Studio` 的插件，它包含了很多的实用的的工具，比如 `Reverse Engineering` 选项可以根据数据库生成 `EF Core` 相关的 `Entity` 代码，也可以生成 `DbContext` ， 也可以查看各个表之间的关系等等。

2、[Microsoft.Extensions.AI](https://devblogs.microsoft.com/dotnet/introducing-microsoft-extensions-ai-preview/)

![image](https://github.com/user-attachments/assets/bc890975-8867-4583-a172-3a0274404c72)

`Microsoft.Extension.AI` 包是微软推出 `AI` 功能拓展包，和其他 `Microsoft.Extensions.*` 包一样，`Microsoft.Extensions.AI.Abstractions`  包定义了一系列接口，而其他包定义了具体的 LLM 实现方式，比如:

```csharp
IChatClient client =
    environment.IsDevelopment ?  
    new OllamaChatClient(...) : 
    new AzureAIInferenceChatClient(...); 
```

使用这个包，可以有如下的好处

- 统一 API
- 灵活性
- 方便使用
- 组件化
