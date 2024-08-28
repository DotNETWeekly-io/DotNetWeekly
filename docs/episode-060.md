# .NET 每周分享第 60 期

## 卷首语

![image](https://github.com/user-attachments/assets/c154119f-7625-41a2-b0e7-fb021b902762)

每个上了年纪的 C# 的开发者几乎都用过 `Resharper` 这个插件，在 `Visual Studio` 还没有像现在强大之前，`Resharper` 插件让我们写出更加优秀的 `C#` 代码。今年是 `Resharper` 插件问世的 20 周年，第一个版本出现在 `Visual Studio 2005` 上，有了 `Resharper` 的成功，JetBrains 公司才后续推出了 `Rider`, `dotPeek` ,`dotTrace` 等相关软件。

## 文章推荐

1、[20 个 Visual Studio 必须要知道的快捷键](https://www.youtube.com/watch?v=nhEHXQGVmaQ&ab_channel=MicrosoftVisualStudio)

Visual Studio 有很多快捷键，这些  20 个快捷键是最有用的

1. Ctrl + K, C: 注释掉某行
2. Ctrl+ K, U： 撤回注释
3. Ctrl + K, D: 格式化文档
4. Ctrl + , : 调出查询面板
5. Ctrl + Q:  调出功能查询面板
6. Ctrl + G: 跳转到某一行
7. Alt + `/` : 调出 Co-pilot
8. Ctrl + K, S: 添加代码域
9. Ctrl + R, R: 重命名
10. Ctrl + Space： 建议命名
11. prop： 创建属性
12. ctor: 创建构造函数
13. Ctrl+L:  剪切行代码并且拷贝到历史粘贴板
14. Ctrl + Shift +L: 删除行
15. Alt + 上下方向键： 移动某一行的位置
16. Ctrl + Alt +B: 显示该文档所有断点
17. Ctrl + Shift + F9: 删除所有断点
18. Ctrl + M +O: 合并所有代码块
19. Ctrl + M +M : 展示所有代码快
20. Ctrl+ R + W: 展示所有的空白行

2、[NugetAudit](https://devblogs.microsoft.com/nuget/nugetaudit-2-0-elevating-security-and-trust-in-package-management/)

![image](https://github.com/user-attachments/assets/62626607-3442-4707-a09b-57d5e44d0d1e)

`NugetAudit` 2.0 已经发布，它可以帮助我们解决 .NET 项目依赖中出现 ``Vulnerability` 的问题。 举个例子，如果你的项目中引用了 `Newontsoft.Json` 9.0 版本，但是由于这个版本有安全性的漏洞，那么在编译这个项目的时候，就会出现一个 Warning 信息。那么该如何解决这个问题呢？

- 直接升级这个有问题的依赖
- 如果是间接引用的包导致的问题，比如项目包引用关系如下 `A->B->C`, 如果 `C` 包出现问题，那么我们应该优先升级 `A`, 其次是 `B`，最后是 `C` 包。 

如果想要 `Suppress` 某个有个问题的报告，可以在 `csproj` 中的配置相关内容

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <!--  other parts of the project left out of this example -->
  <ItemGroup>
    <NuGetAuditSuppress Include="https://github.com/advisories/GHSA-6qmf-mmc7-6c2p" />
  </ItemGroup>
</Project>
```

3、[理解 `IQueryable<T>`](https://dev.to/rasheedmozaffar/understanding-iqueryable-in-c-4n37)

`IQueryable` 是 `System.Linq` 命名空间中的一个类，它继承 `IEnumerable` 的接口，也即是所有的 `Linq` 表达式都可以用在 `IQueryable` 中，但是 `IQueryable` 中有一个重要的属性 `Expression` ，那么它是做什么的呢？

```csharp

List<FamousPerson> famousPeople = [
  new FamousPerson(1, "Sandy Cheeks", false),
  new FamousPerson(2, "Tony Stark", true),
  new FamousPerson(3, "Captain Marvel", true),
  new FamousPerson(4, "Captain America", true),
  new FamousPerson(5, "SpongeBob SquarePants", false),
  new FamousPerson(6, "Hulk", false)
];

IQueryable<FamousPerson> famousAndCanFly =
    famousPeople.AsQueryable().Where(x => x.CanFly);

famousAndCanFly = famousAndCanFly.Where(x => x.Id < 3);

famousAndCanFly = famousAndCanFly.Where(
    x => x.Name.Contains("s", StringComparison.OrdinalIgnoreCase));

Console.WriteLine(famousAndCanFly.Expression);

class FamousPerson
(int id, string name, bool canFly) {
  public bool CanFly => canFly;

  public int Id => id;

  public string Name => name;
}
```

输出的结果如下

```txt
System.Collections.Generic.List`1[FamousPerson].Where(x => x.CanFly).Where(x => (x.Id < 3)).Where(x => x.Name.Contains("s", OrdinalIgnoreCase))
```

一旦有了 `Expression` 这样的树结构，那么不同的数据提供者根据 Expression 实现自己的方式，比如 `EF Core` 可以将表达式树转换成相应的 SQL 语句，这样也能根据不同的情况进行优化。

4、[自定义类型集合表达式](https://andrewlock.net/behind-the-scenes-of-collection-expressions-part-5-adding-support-for-collection-expressions-to-your-own-types/)

C# 12 中引入了集合初始化的特性，这样可以通过非常简单的方式来初始化一个集合，比如

```csharp
List<int> values = [1, 2, 3];
```

那么该如何定义自己的类型也支持这种集合初始化方式呢？最直接的方法是实现 `IEnumerable` 或者 `IEnumerable<T>`  接口，然后实现 `Add(T val)` 的方法

```csharp
public class MyCollection: IEnumerable // Implementing the non-generic IEnumerable
{
    // Backing collection that contains the data
    private readonly List<int> _list = new();

    // Implement the required member of IEnumerable
    public IEnumerator GetEnumerator() => _list.GetEnumerator();

    // The required Add() method for collection initializers
    public void Add(int val)
    {
        _list.Add(val);
    } 
}

MyCollection<int> myCollection = new() { 1, 2, 3, 4 };
```

另外一种方式是使用 `CollectionBuilder` 注解

```csharp
[CollectionBuilder(typeof(MyCollection), nameof(Create))]
public class MyCollection
{
    // 👇 This is the method the collection expression calls
    // It must take a ReadOnlySpan<> of the values and return an instance
    // of the collection
    public static MyCollection Create(ReadOnlySpan<int> values) => new(values);

    private readonly int[] _values;
    public MyCollection(ReadOnlySpan<int> values)
    {
        // Because all the values are provided in the constructor, we can
        // use an array backing type instead of a list, which is more efficient
        // to create, and doesn't need to expose a mutation Add() method
        _values = values.ToArray();
    }

    // Must have a GetEnumerator() method that returns an IEnumerator implementation
    public IEnumerator<int> GetEnumerator() => _values.AsEnumerable().GetEnumerator();
}
```

5、[ASP.NET Core 中式 Open Telemetry](https://www.youtube.com/watch?v=MHJ0BHfWhRw&ab_channel=NickChapsas)

![image](https://github.com/user-attachments/assets/0d5c30ee-118f-45c4-a8d2-e062b769fd8a)

Open Telemetry 是一个公开的 Telemetry 标准，那么该如何在 `ASP.NET Core` 应用程序中集成它呢？这个视频给了一个非常直接的 demo。

6、[C# 中 GUID 在数据库中的缺陷](https://www.youtube.com/watch?v=n17U7ntLMt4&ab_channel=ZoranHorvat)

![image](https://github.com/user-attachments/assets/8a9a2bf9-a943-4512-be65-79c560b19a65)

随着分布式应用程序越来越流行，很对应用程序中的对象采用 `GUID` 作为主键，如果使用 `SQL Server` 作为存储的的话，会出现一个性能上的问题。

```csharp
public class Book
{
    public Guid Id { get; set; } 
    public string? Name { get; set; }
}

public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books {get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
           .HasKey(x => x.Id);
        base.OnModelCreating(modelBuilder);
    }
}
var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseSqlServer(connectionString)
    .Options;
ApplicationDbContext context = new ApplicationDbContext(options);
context.Books.Add(new Book { Name = "6 book", Id = Guid.NewGuid()});
context.Books.Add(new Book { Name = "7 book", Id = Guid.NewGuid()});
context.Books.Add(new Book { Name = "8 book", Id = Guid.NewGuid()});
context.Books.Add(new Book { Name = "9 book", Id = Guid.NewGuid()});
context.Books.Add(new Book { Name = "10 book", Id = Guid.NewGuid()});
context.SaveChanges();
```

这里 `Book` 对象的 `Id` 属性的类型是 `GUID`, 也是也是数据库表中的主键， 在插入对象的时候，通过 `Guid.NewGuid()` 的方法生成该字段。看上去好像没什么问题，但是如果我们查看数据库的实现细节的话， 我们可以看到 `Id` 索引是 `Clustered` 类型。

![image](https://github.com/user-attachments/assets/8bbc5855-4c53-4ff9-82a6-e7bf2ae8e20c)

这样每次插入数据，都需要在物理磁盘中按照此索引调整。但是在应用程序中 `Guid.NewGuid` 方法并不能保证单调的，这样导致每次插入的时候必须进行大量 I/O 操作，影响性能。 所以解决办法是在构造数据库的时候，指定该索引不是 `Clustered`

```csharp
modelBuilder.Entity<Book>()
      .HasKey(x => x.Id)
      .IsClustered(false);
```

另外一种方法是让 `SQL Server` 自己生成 GUID，这样能够保证生成的 Index 值是自增的。

```csharp
modelBuilder.Entity<Book>()
      .Property(x => x.Id)
      .ValueGeneratedOnAdd();
```

7、[高性能 C# 代码](https://www.youtube.com/watch?v=2SXr48OYxbA&list=WL&index=2&ab_channel=NDCConferences)

这是一个演讲分享，作者介绍了在实际工作中关于高性能 `C#` 代码的实践，主要包含下面几个主题

1. `Span<T>`, `ReadOnlySpan<T>` 和 `Memory<T>` 
2. ArrayPool
3. `System.IO.Pipelines` 和 `ReadOnlySequence<T>`
4. `System.Text.Json`

8、[StringSyntax](https://www.youtube.com/watch?v=-7CJOU0QJC0&ab_channel=NickChapsas)

![image](https://github.com/user-attachments/assets/98ce8ff5-7ba1-41ec-8305-ad31a77e60ca)

`StringSyntax` 是随着 `.NET 7` 推出的功能，它主要是解决一个问题，让 IDE 根据方法的定义，提供有效的字符串参数的定义。举个例子，比如方法的定义如下

```csharp
void SomeDatetime(string val)
{
}
```

这里 `SomeDatetime` 接受一个字符串，它是一个 `DateTime` 类型的 format 类型。但是问题是字符串的类型太自由了，一步留神就会导致一个无法检查的错误。那么 `StringSyntax` 就是解决这个问题。

```csharp
void SomeDatetime([StringSyntax(StringSyntaxAttribute.DateTimeFormat)]string val)
{
}
```

这里的参数有一个 `[StringSyntax(StringSyntaxAttribute.DateTimeFormat)]` 的注解，它表明这个参数接受一个 `Datetime Format` 类型，比如 `yyyy-mm-dd` 等等。这样编辑器在调用这个方法的时候，就会提供智能相关的提示。除了 `Datetime Format`, 还有其他类型，比如 `GUID`, `JSON`, `Regex` 等等。

9、[ASP.NET Core 最佳实践](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-8.0)

这是 `ASP.NET Core` 官方文档中的最佳实践内容

1. 使用缓存
2. 理解 hot code paths
3. 避免阻塞调用
4. 将大的对象返回小的页面
5. 返回 `IEnumerable<T>` 之前需要使用 `ToListAsync` 方法
6. 避免大对象分配
7. 优化数据访问和 I/O
8. 使用 `HttpClientFactory`
9. 通用代码运行快，比如中间件
10. 不要在 HTTP Requests 中运行长时间运行的任务
11. 前端访问的资源尽可能简化
12. 压缩响应
13. 使用最新的 ASP.NET Core 代码
14. 最小化异常
15. 避免同步读写请求和响应的 body
16. 使用 `ReadFromAsync` 而不是 `Request.Form`
17. 避免将把请求的大 body 读入内存中
18. 不要将 `IHttpContextAccessor.HttpContext` 保存在字段中
19. 不要在多线程中访问 `HttpContext`
20. 在请求完成后使用 `HttpContext`
21. 不要在后台线程中捕获 `HttpContext`
22. 不要在后台线程中捕获注入的服务对象
23. 不要在相应body 开始后修改响应码和头
24. 不要在调用 `Next()` 当你开始写响应 body

10、[Channel 类型使用](https://dotnetcoretutorials.com/using-channels-in-net-core-part-2-advanced-channels/)

![image](https://github.com/user-attachments/assets/58bd9ebe-20df-41d3-b292-4d5a5028b055)

`Channel` 是 C# 类型，它可以帮助我们实现类似生产者（Producer）和消费者(Consumer)的设计模式，比如

```csharp
var myChannel = Channel.CreateUnbounded();
for(int i=0; i < 10; i++){
    await myChannel.Writer.WriteAsync(i);
}
while(true)
{
     var item = await myChannel.Reader.ReadAsync();
     Console.WriteLine(item);
}
```

那么它和其它使用 `Queue` 实现有什么区别呢？

- 明确责任

如果使用 `Queue` 的话，实现的大概如下:

```csharp
class MyProducer {
    private readonly Queue<int> _queue;
    public MyProducer(Queue<int> queue) {
        _queue = queue;
    }
}
class MyConsumer {
    private readonly Queue<int> _queue;
    public MyConsumer(Queue<int> queue) {
        _queue = queue;
    }
}
```

我们需要将 `Queue` 的实例作为 `Producer` 和 `Consumer` 的构造函数，这个要求我们只能在 `Producer` 中使用 `Enqueue` 而且 `Consumer` 中使用 `Dequeue`, 但是这个只能要求开发者保持这个约束。如果使用 `Channel` 的话，可以规避这个问题

```csharp
class MyProducer{
    private readonly ChannelWriter<int> _channelWriter;
    public MyProducer(ChannelWriter<int> channelWriter){
        _channelWriter = channelWriter;
    }
}

class MyConsumer{
    private readonly ChannelReader<int> _channelReader;
    public MyConsumer(ChannelReader<int> channelReader){
        _channelReader = channelReader;
    }
}
```

我们可以传递不同的 `Channel` 中的实现给不同的 `Producer` 和 `Consumer`。

- 关闭功能

`Channel` 中的 `Writer` 可以通知该 `Channel` 已经关闭，这样 `Consumer` 可以释放响应的资源

```chsarp
_ = Task.Factory.StartNew(async () => {
    for (int i = 0; i < 10; i++) {
        await channel.Writer.WriteAsync(i);
        await Task.Delay(10);
    }

    channel.Writer.Complete();
});

try
{
    while(true) {
    var item = await channel.Reader.ReadAsync();
    Console.WriteLine(item);
    }
}
catch (ChannelClosedException)
{
    Console.WriteLine("Channel is closed");
}
```

`Writer.Complete()` 方法可以让 `Reader.ReadAsync()` 方法抛出 `ChannelClosedException`。当然也可以使用 `await foreach` 方法避免捕获异常

```csharp
await foreach (var item in channel.Reader.ReadAllAsync()) {
    Console.WriteLine(item);
    await Task.Delay(100);
}
```

- 配置模式

除了 `UnboundedChannel`， 也可以使用 `BoundedChannel`, 它可以配置 `Channel` 在写入的行为

```csharp
var channelOptions = new BoundedChannelOptions(10) {
    FullMode = BoundedChannelFullMode.Wait
};

var myChannel = Channel.CreateBounded<int>(channelOptions);
```

比如这里我们创建了只有 10 大小的 `Channel`, 而且在当 `Channel` 满了，后续的写入处于等待状态。

11、[理解 Stack Trace](https://blog.elmah.io/understanding-net-stack-traces-a-guide-for-developers/)

![image](https://github.com/user-attachments/assets/7ce9fbd7-2b8a-4bfe-bb9a-b2ce1cade520)

理解 C# 代码中的异常调用栈（Stack Trace） 对于我们 debug 非常有帮助，`C#` 中有如下几种异常调用栈信息

- Stack Trace

```txt
System.NullReferenceException: Object reference not set to an instance of an object.
   at ConsoleApp.Program.MethodA() in C:\projects\ConsoleApp\Program.cs:line 20
   at ConsoleApp.Program.Main(String[] args) in C:\projects\ConsoleApp\Program.cs:line 10
```

调用栈的第一行指出了抛异常的内容，是 `NullReferenceException`, 下面紧接着是调用的反顺序。不过值得注意的是，它每一行都有三个空格。

- Inner Exception Stack Trace

当你的代码抛出异常后，被 `catch` 语句捕获后，然后重新的抛出新的异常，可以原本的异常将设置为最外面的异常的 `InnerException` 属性

```txt
Unhandled exception. System.ApplicationException: application error
 ---> System.NullReferenceException: Object reference not set to an instance of an object.
   at Program.<Main>$(String[] args) in C:\Users\fenga\tizan\codebase\episodes\exceptiondemo\Program.cs:line 6
   --- End of inner exception stack trace ---
   at Program.<Main>$(String[] args) in C:\Users\fenga\tizan\codebase\episodes\exceptiondemo\Program.cs:line 10
```

这里用 `--->` 指出下面的异常是来自 `InnerException` 而 `--- End of inner exception stack trace --` 表示 `InnerException` 已经结束，下面的的内容就是原本最外面异常的调用栈。

- Aggregated Exception Stack Trace

`Aggregated Exception` 用来封装多个异常，通常发生在并行或者异步中代码代码中

```txt
System.AggregateException: One or more errors occurred. (One of the identified items was in an invalid format.) (Object reference not set to an instance of an object.)
 ---> System.FormatException: One of the identified items was in an invalid format.
   at ConsoleApp.A.X() in C:\projects\ConsoleApp\A.cs:line 13
   at Program.<Main>$(String[] args) in C:\projects\ConsoleApp\Program.cs:line 9
   --- End of inner exception stack trace ---
 ---> (Inner Exception #1) System.NullReferenceException: Object reference not set to an instance of an object.
   at ConsoleApp.B.Y() in C:\projects\ConsoleApp\A.cs:line 21
   at Program.<Main>$(String[] args) in C:\projects\ConsoleApp\Program.cs:line 18
<---
```

第一行的 `AggregateExcpetion` 用括号标记出每个异常的 `Message`, 在这里用两个异常。接着下面的 `--->` 开头的部分，标记出每个异常的调用栈信息。

- Fattened Stack Trace

在异步代码中，有时候程序运行在不同的代码片段中。

```txt
Azure.Messaging.ServiceBus.ServiceBusException: The lock supplied is invalid. Either the lock expired, or the message has already been removed from the queue, or was received by a different receiver instance. (MessageLockLost). For troubleshooting information, see https://aka.ms/azsdk/net/servicebus/exceptions/troubleshoot.
   at Azure.Messaging.ServiceBus.Amqp.AmqpReceiver.ThrowLockLostException()
   at Azure.Messaging.ServiceBus.Amqp.AmqpReceiver.DisposeMessageAsync(Guid lockToken, Outcome outcome, TimeSpan timeout)
   at Azure.Messaging.ServiceBus.Amqp.AmqpReceiver.CompleteInternalAsync(Guid lockToken, TimeSpan timeout)
   at Azure.Messaging.ServiceBus.Amqp.AmqpReceiver.<>c.<<CompleteAsync>b__43_0>d.MoveNext()
--- End of stack trace from previous location ---
   at Azure.Messaging.ServiceBus.ServiceBusRetryPolicy.<>c__22`1.<<RunOperation>b__22_0>d.MoveNext()
--- End of stack trace from previous location ---
   at Azure.Messaging.ServiceBus.ServiceBusRetryPolicy.RunOperation[T1,TResult](Func`4 operation, T1 t1, TransportConnectionScope scope, CancellationToken cancellationToken, Boolean logRetriesAsVerbose)
   at Azure.Messaging.ServiceBus.ServiceBusRetryPolicy.RunOperation[T1](Func`4 operation, T1 t1, TransportConnectionScope scope, CancellationToken cancellationToken)
   at Azure.Messaging.ServiceBus.Amqp.AmqpReceiver.CompleteAsync(Guid lockToken, CancellationToken cancellationToken)
   at Azure.Messaging.ServiceBus.ServiceBusReceiver.CompleteMessageAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken)
   at Azure.Messaging.ServiceBus.ReceiverManager.ProcessOneMessage(ServiceBusReceivedMessage triggerMessage, CancellationToken cancellationToken)
```

在这里使用 `--- End of stack trace from previous location ---` 作为分隔符，表明代码执行到这里，然后去其他地方。这个经常发生了 `await` 结束后，继续执行面的代码。
