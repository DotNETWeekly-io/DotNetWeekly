# .NET 每周分享第 21 期

## 卷首语

[IAsyncEnumerable 接口](https://code-maze.com/csharp-async-enumerable-yield/)

`C# 8` 引入了 `IAsyncEnumerable` 类型接口，通过它可以实现异步枚举类型的迭代。假设需要定义了一个方法

```csharp
public async Task<IEnumerable<int>> GetAllItems()
```

那么一个直接实现方法如下

```csharp
static async Task<IEnumerable<int>> FetchItems()
{
    List<int> Items = new List<int>();
    for (int i = 1; i <= 10; i++)
    {
        await Task.Delay(1000);
        Items.Add(i);
    }
    return Items;
}
```

但是输出的结果如下

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3c456797-b3e5-46f9-8f3b-7a7b877870ea)

可以看出来，集合是在内容全部提取完毕之后，才依次输出。我们知道 `C#` 提供了 `yield return` 方法可以部分返回数据，而不是等全部执行完毕。那么改成这样事否可以呢？

```csharp
static async Task<IEnumerable<int>> FetchItems()
{
    for (int i = 1; i <= 10; i++)
    {
        await Task.Delay(1000);
        yield return i;
    }
}
```

不幸的是出现编译错误

> The body of 'Program.FetchItems()' cannot be an iterator block because `Task<IEnumerable<int>>` is not an iterator interface type

这时候需要换成 `IAsyncEnuermable` 类型

```csharp
static async IAsyncEnumerable<int> FetchItems()
{
    for (int i = 1; i <= 10; i++)
    {
        await Task.Delay(1000);
        yield return i;
    }
}
```

注意，不需要返回一个 `Task` 类型，在调用的时候，可以在 `foreach` 之前 `await` 标识符

```csharp
static async Task Main(string[] args)
{
    Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: Start");
    await foreach (var item in FetchItems())
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {item}");
    }
    Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: End");
}
```

效果如下

![image](https://user-images.githubusercontent.com/11272110/187018546-2cfd69fa-1015-48b3-8720-30ca62d2991a.png)

## 行业资讯

1、[.NET Conf Focus on MAUI 活动回顾](https://devblogs.microsoft.com/dotnet/dotnet-conf-focus-on-maui-recap/)

![image](https://user-images.githubusercontent.com/11272110/185733261-4e1f5ee4-e439-4608-8b55-ac6b380c900c.png)

前一阵子关于 `MAUI` 的 `.NET Conf` 如期举办，会议包含了很多有趣的内容，包含了大量的录像和实例代码。

2、[C# 11 Preview](https://devblogs.microsoft.com/dotnet/csharp-11-preview-august-update/)

![image](https://user-images.githubusercontent.com/11272110/187016963-12ad0153-e517-4caf-9574-0b6f7098beba.png)

C# 11 马上就要发布了，这篇文章带你介绍了 `C#11` 包含的特性，主要分为三大块

1. 对象初始化

- Required 字段

对象的属性，可以添加 `Required` 描述符表示该属性在初始化的必需提供

```csharp
public class Person
{
    public required string FirstName { get; init; }
    public required string LastName {get; init; }
}
```

2. 数学支持

- 接口的静态或者虚拟方法
- 右移操作
- 无符号右移操作
- ...

3. 开发者效率

`nameof` 操作符可以用在方法参数中

```csharp
[return: NotNullIfNotNull(nameof(url))]
string? GetTopLevelDomainFromFullUrl(string? url)
```

## 文章推荐

1、[如何使用 .NET Memory Tool](https://devblogs.microsoft.com/visualstudio/choosing-a-net-memory-profiler-in-visual-studio-part-1/)

![image](https://user-images.githubusercontent.com/11272110/185724326-fbc9f3e4-de06-4c30-9b04-8a93392e7e3d.png)

内存泄露是 `.NET` 应用程序排查的难点，因为它们不是仅仅通过编写代码或者编译的时候就能发现，需要在程序运行的过程中，通过分析内存分布情况才能发现。 `Visual Studio` 内置了量中重要的工具 `Memory Usage` 和 `.NET Object Allocation Tracking`， 通过它们我们能够发现内存的异样。

2、[Azure App Service Kestrel + YARP 迁移](https://azure.github.io/AppService/2022/08/16/A-Heavy-Lift.html)

![image](https://user-images.githubusercontent.com/11272110/185725983-200263df-2162-4151-a7f4-ee37e94686b2.png)

Azure App Service 是 Azure 平台上的 `PaaS`，开发者可以开发 `Web` 服务，而无需关心底层 VM，外部网络等等。因为这些基础项目都交给了 `Azure` 平台来处理，最近 `Azure App Service` 的网络已经切换到 `Kestrel` 和 `YARP`， 这篇文章详细介绍了这些内容。

3、[如何并行执行多个异步任务？](https://code-maze.com/csharp-execute-multiple-tasks-asynchronously/)

![image](https://user-images.githubusercontent.com/11272110/185732478-7c6b4e02-14cc-4281-8734-7ef599288519.png)

对于异步编程，`C#` 已经有很好的编程模型，通过 `async` 和 `await` 两个关键字实现了复杂的状态机转移。如果在应用程序开发的过程中，需要执行多个异步操作，那么该怎么办呢？

1. 顺序执行

第一个方法就比较直接，依次执行这些异步操作，比如

```csharp
public async Task<FinalResult> DoWorkAsync()
{
    var result1 = await _provider.GeResult1();
    var result2 = await _provider.GetResult2();
    var result3 = await _provider.GetResult3();
    return new FinalResult(result1, result2, result3);
}
```

按照 `C#` 的异步机制，`GetResult2` 的方法只会在 `GetResult1` 方法执行完毕之后，依次类推。

2. Task.WhenAll 方法

`Task.WhenAll` 接受一个 Task 类型的可变参数，它表示只有所有这些 `Task` 执行完毕之后，这个方法才会继续执行下去。

```csharp
public async Task<FinalResult> DoWorkAsync()
{
    var task1= _provider.GeResult1();
    var task2 = _provider.GetResult2();
    var task3 = _provider.GetResult3();
    await Task.WhenAll(task1, task2, task3);
    var result1 = await task1;
    var result2 = await task2;
    var result3 = await task3;
    return new FinalResult(result1, result2, result2);
}
```

当 `await Task.WhenAll` 返回的的时候，表明 `task1, task2, task3` 都执行完毕，所以 `await task1` 等方法肯定会直接返回。当然也可以是使用 `task.Result` 拿到结果。

4、[WPF 教程](https://www.cnblogs.com/zh7791/category/1213318.html)

WPF [博客](https://www.cnblogs.com/zh7791/category/1213318.html)和[视频](https://www.bilibili.com/video/BV1nY411a7T8?share_source=copy_web&vd_source=ae0052b5bee1bb204a25538e2fdec399)教学内容。

5、[ASP.NET Core Controller 返回值类型](https://code-maze.com/aspnetcore-web-api-return-types/)

![image](https://user-images.githubusercontent.com/11272110/187017261-31b2f030-e353-4727-a038-34f894a942ab.png)

`ASP.NET Core` 的 `Controller` 的返回值有三种类型

- 特定类型
- `IActionResult`
- `IActionResult<T>`

那么它们之间有什么区别呢？

1. 特定类型

```csharp
[HttpGet]
public List<Employee> Get() =>
    _repository.GetEmployees();
```

特定类型的返回值会将其序列化成 `JSON` 字符串和 200 状态码。但是如果是在其中发生了异常，只能返回 `500` 的状态码。

2. IActionResult

`IActionResult` 支持返回多个实现类型，比如说 `404 Not Found` 错误，可以调用 `ControllerBase` 中的 `NotFound` 方法，而正确的返回结果，可以调用 `Ok` 方法。

```csharp
[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    if (!_repository.TryGetEmployee(id, out Employee? employee))
    {
        return NotFound();
    }

    return Ok(employee);
}
```

3. `IActionResult<T>`

该返回结果包含了类型，除了支持 `IActionResult` 的功能，还可以直接返回 `T` 类型对象

```csharp
[HttpGet("{id}")]
public ActionResult<Employee> GetById(int id)
{
    if (!_repository.TryGetEmployee(id, out var employee))
    {
        return NotFound();
    }
    return employee;
}
```

## 开源项目

1、[NUnit](https://github.com/nunit/nunit)

NUnit 是一个适用于所有 .NET 语言的单元测试框架。最初版本由 java 单元测试框架 JUnit 移植，当前的生产版本 NUnit3 已完全重写，目前具有许多新功能和对各种 .NET 平台的支持。

2、[Bogus](https://github.com/bchavez/Bogus)

![image](https://user-images.githubusercontent.com/11272110/187018564-0e58390d-1498-4e01-a7ca-6f72073dbe5b.png)

在软件开发过程中，测试是并不可少的。但是在测试过程准备一些测试数据可就比较繁琐了。`Bogus` 开源库可以帮助你自动生成测试数据，而且看上去跟真实的很类似。
