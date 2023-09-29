# .NET 每周分享第 42 期

## 卷首语

为什么 Startup 不选择 .NET 作为后端语言

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/cfd81057-5150-4148-afcf-ec293902da89)

`Reddit` 上的一个[讨论](https://www.reddit.com/r/dotnet/comments/16fu7o0/why_isnt_dotnet_core_popular_among_startups/?share_id=m43b3lhRz0cwGG0GLrSew&utm_content=1&utm_medium=ios_app&utm_name=ioscss&utm_source=share&utm_term=1&rdt=61720)

> 为什么创业公司不选择 .NET 

高赞回答列出了下面几个原因

1. 创业公司吸引年轻人，而年轻人更加喜欢 Javascript
2. 创业公司通常是全栈，Javascript 适合全栈开发
3. 对于 `C#` 和 `.NET` 有着传统的看法
4. 过去一段事件 `.NET` 社区一些错误决定
5. `.NET` 这种基于约束的方法很难让人喜欢
6. 人力资源方面的挑战
7. 不喜欢多线程
8. 面向对象编程的恐惧
9. 创业公司喜欢 Mac 电脑
10. 认为 `.NET` 和 `Java` 太主流了


## 行业资讯

1、[C# 学习证书](https://devblogs.microsoft.com/dotnet/csharp-certification-training-series/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/8c6a7176-9ec6-4fc2-9e75-f62f2f539b6e)

微软联合 `freecCodeCamp` 推出了 `C#` 培训课程，在其中可以获得相关的证书。

2、[Visual Studio 团队茶话会](https://devblogs.microsoft.com/visualstudio/visual-studio-tea-technology-miniseries/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/d5baebd4-5ff2-4850-be1a-a47288476075)

10 月 4 号，`Visual Studio` 团队会组织一个茶话会，邀请团队人来分享关于 `Visual Studio` 的其他不为人知的方面，比如如何将 idea 转换为 feature，如何 debug，如何用 Visual Studio 创建移动应用程序。


## 文章推荐

1、[EF Core 中的乐观锁](https://www.milanjovanovic.tech/blog/solving-race-conditions-with-ef-core-optimistic-locking)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c38c012c-e5f1-4b72-9570-90f6ce10580e)


在数据库应用程序中，我们通常需要考虑并发问题。比如一个酒店订阅系统，我们不能在同一个时间段将一个房间分发给两个订单。应用程序的逻辑一般是这样的

1. 查询特定房间在特定时间段的订阅情况
2. 如果被预定，返回订阅失败
3. 否则，调用付款等其他服务
4. 更新房间信息并且保存

这些步骤不是在同一个 `Transaction` 完成的，这就带来一个问题，如果在第 1 步之后，房间被其他用户约定了，那么在第 4 步的时候，就会覆盖前面一个订阅的结果。
那么 `SQL Server` 中的乐观锁就能解决这个问题，对于表中的都会内置一个 `rowversion` 列，当这一行被更新后，就会自动更改，那么过程就变成这样
1. 查询特定房间在特定时间段的订阅情况
2. 如果被预定，返回订阅失败
3. 否则或者该行数据和 `rowversion`，调用付款等其他服务
4. 在更新的时候只有 `rowversion` 和之前上一步获取的相同的时候，才更新成功，否则失败。

体现在代码中是这样的

```csharp
public class Apartment
{
    public Guid Id { get; set; }
    [Timestamp]
    public byte[] Version { get; set; }
}
```

或者在 `Fluent API` 中

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Apartment>()
        .Property<byte[]>("Version")
        .IsRowVersion();
}
```

这样在调用 `SaveSchanges` 方法的时候，会抛出 `DbUpdateConcurrencyException` 异常

2、[于 Moq 中的 SponsorLink 作者的观点](https://codecodeship.com/blog/2023-09-07-daniel-cazzulino)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/062a5f1e-5134-49b9-8281-d64685e4e457)

前一阵子 `Moq` 库的 `SponserLink` 的事情在 `.NET` 社区掀起了巨大的讨论。在事件平息之后，作者采访了 `Moq` 的作者聊了一下这件事的来龙去脉，包含了下面的话题
- 作者对开源认识
- 使用 `SponerLink` 的效果
- 事件发生后 `Moq` 的现状
- 对后续的规划和展望 


3、[Weak Reference](https://steven-giesel.com/blogPost/675b75fc-2c1b-43da-9ff8-42962ca8159b)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/165fa6d2-4bd6-4bb2-86f4-5781b2d724fc)


C# 作为一个托管的编程语言，也就是说我们并不需要关心内存的问题。但是有很多情况下会导致内存泄漏，比如事件的的委托。

```csharp
public class Publisher
{
    public event Action<string> OnChange = delegate { };
    public void Raise(){
        OnChange("hello world");
    }
    public string Label => "Hi";
}

public class Subscriper : IDisposable
{
    private bool disposed = false;
    public Subscriper() {
        Console.WriteLine("ctor");
    }
    ~Subscriper()  {
        Console.WriteLine("descrt");
        Dispose(false);
    }
    public void Dispose(){
        Console.WriteLine("dispose");
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed) {
            disposed = true;
        }
    }
    public void WriteLine(string s){
        Console.WriteLine(s);
    }
}
```
比如这里定义了一个 `Puber` 和一个 `Suber` 类，那么在应用程序中

```csharp
Subscriper sub;
Publisher publisher = new Publisher();

for (int i = 0; i < 20; i ++)
{
    sub = new Subscriper();
    // publisher.OnChange += sub.WriteLine;
}
Console.ReadKey();
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine();
Console.WriteLine(publisher.Label);
```

当调用  `GC.Collect` 之后，`~Subscriper` 方法就会被多次执行，因为被 GC 给回收。但是如果我们将 `publisher.OnChange += sub.WriteLine;` 恢复，所有的 `~Subscriper` 都不会执行，因为从 `GC` 角度来看，它注册了一个 `Puber` 的事件。这样导致了内存泄露。

在 `C#` 中有一个叫做 `WeakReference` 类型，它能够跳过在 `GC` 对标记阶段。该类型有两个重要属性 `IsAlive` 表明引用的对象是否活着，`Target` 能够获得该对象。

```csharp
public class WeakEvent
{
    private readonly List<WeakReference> _listeners = new List<WeakReference>();

    public void AddListener(Action<string> handler)
    {
        _listeners.Add(new WeakReference(handler));
    }

    public void Raise()
    {
        for(int i = _listeners.Count - 1; i >= 0; i--)
        {
            WeakReference wr = _listeners[i];
            if (wr != null)
            {
                if (wr.IsAlive)
                {
                    ((Action<string>)wr.Target)("hell world");
                } 
                else
                {
                    _listeners.RemoveAt(i);
                }
            }
        }
    }
}

public class Publisher
{
    private readonly WeakEvent _event = new WeakEvent();
    public void Raise(){
        _event.Raise();
    }

    public void Register(Action<string> handler){
        _event.AddListener(handler);    
    }
    public string Label => "Hi";
}


public class Subscriper : IDisposable
{
    private bool disposed = false;
    public Subscriper(){
        Console.WriteLine("ctor");
    }

    ~Subscriper(){
        Console.WriteLine("descrt");
        Dispose(false);
    }

    public void Dispose(){
        Console.WriteLine("dispose");
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose managed resources here
            }
            // Dispose unmanaged resources here
            disposed = true;
        }
    }

    public void WriteLine(string s)
    {
        Console.WriteLine(s);
    }
}
```

这时候，我们在调用下面这段代码的时候，由于 `sub` 的 `WriteLine` 注册对象是 `WeakReference`, 那么 GC 就会将他们全部回收。

```csharp
Subscriper sub;
Publisher publisher = new Publisher();

for (int i = 0; i < 20; i ++)
{
    sub = new Subscriper();
    publisher.OnChange += sub.WriteLine;
}
Console.ReadKey();
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine();
Console.WriteLine(publisher.Label);
```

4、[.NET 8 Performance 提高](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3d45fbe0-fdf0-417e-bceb-9f5ee6c3644c)

一年一度的 `.NET` 形成提升的文章又来了，今年轮到 `.NET 8`。这是一篇接近 20 页的文章，如果你有时间，可以快速浏览一下。

5、[VS for Mac 退役的思考](https://www.youtube.com/watch?v=M7V9ZzxPtVc&ab_channel=IAmTimCorey)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9c106622-73c9-4902-a0f4-00c6983d1c1c)

前一阵子微软宣布明年将不再支持 `Visual Studio for Mac`, 当时引起了巨大的争论，现在作者分享了它对这件事情的看法
- 为什么微软创建 `.NET`? 答案就是挣钱，不管是买授权，还是和其他产品绑定，或者做咨询甚至是获取声誉，都是为了挣钱。
- `Visual Studio` 并不是一个天生跨平台的软件，有很多 windows 上的依赖。
- `VS Code` 是在 Linux 和 Mac 上很好的选项
- 这个决定看上去太着急了，尤其对于 MAUI 的发展
- 微软需要更多的 `Why` 


6、[迁移 ASP.NET 到 ASP.NET Core](https://www.jimmybogard.com/tales-from-the-net-migration-trenches/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/47a3c336-1ec5-495a-a6dc-82e48a0ef0b0)

这是一些列博客，作者介绍了如何将 `ASP.NET Framework` 的应用程序迁移到 `ASP.NET Core`, 作者采取了一种渐进式的迁移方式。通过一个空的 `ASP.NET Core` 的应用程序，然后将请求转发原本的应用程序，然后逐步迁移其他 Controller. 

7、[EF Core 中不同的 load 方式](https://blog.jetbrains.com/dotnet/2023/09/21/eager-lazy-and-explicit-loading-with-entity-framework-core/)

在数据库中，我们通过外键的方式将不同的表链接起来，在 EF Core 中，我们可以通过这种方式来多表查询，主要有三种方式

1. Eager loading

这种方式是通过 `Include` 语句来包含子表

```csharp
var invoices = db.Invoices
    .Include(invoice => invoice.InvoiceLines)
    .ToList();
// All invoices are already loaded...
foreach (var invoice in invoices)
{
    // ...including all their Invoice lines
    foreach (var invoiceLine in invoice.InvoiceLines)
    {
        // ...
    }
}
```


2. Lazy loading
这种方式只有在进行对子表属性访问的时候，才会发送查询请求

```csharp
var invoices = db.Invoices
    .ToList();

// All invoices are already loaded...
foreach (var invoice in invoices)
{
    // ...invoice lines are queried when accessed...
    foreach (var invoiceLine in invoice.InvoiceLines)
    {
        // ...the related product is also queried when accessed
        var product = invoiceLine.Product;

        // ...
    }
}
```


3. Explicit loading


通过 `Load()` 方法显示的获取相应的子表

```csharp
var invoices = db.Invoices
    .ToList();

// All invoices are already loaded...
foreach (var invoice in invoices)
{
    // ...but you'll have to explicitly load invoice lines when they are needed
    db.Entry(invoice).Collection(p => p.InvoiceLines).Load();

    foreach (var invoiceLine in invoice.InvoiceLines)
    {
        // ...
    }
}
```