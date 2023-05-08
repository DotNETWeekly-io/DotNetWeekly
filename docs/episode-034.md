# .NET 每周分享第 34 期

## 卷首语

上周有个朋友问了一个问题，怎么实现在多线程中使用 `Dictionary`, 最直接想法是使用 `ConcurrentDictionary` 类型。但是这个类太重了， 因为在性能重要的应用场景中，并不是一个很好的选择。如果直接使用 `lock` 方法，又太简单粗暴，因为更多使用场景是多线程读，而不是写。后来就问了一下 `ChatGPT`，看到它又什么建议：

Q: How to use the C# dictionary in in multiple threads?

A:
When using a Dictionary in C# with multiple threads, you need to take into account that Dictionary is not thread-safe by default. This means that if multiple threads try to access or modify the Dictionary concurrently, it may lead to unexpected behavior such as race conditions, deadlocks, or inconsistent results.
To use a Dictionary safely in multiple threads, you can use one of the following approaches:

1. Locking
   You can use a lock statement to synchronize access to the Dictionary from multiple threads. This approach ensures that only one thread can access the Dictionary at a time, which eliminates race conditions and ensures consistency.
   Here's an example of how to use locking with a Dictionary:

```csharp
Dictionary<string, int> dict = new Dictionary<string, int>();
object dictLock = new object();
// Thread 1
lock (dictLock){
    if (!dict.ContainsKey("key")){
        dict.Add("key", 1);
    }else{
        dict["key"]++;
    }
}
// Thread 2
lock (dictLock){
    if (!dict.ContainsKey("key")){
        dict.Add("key", 1);
    }else{
        dict["key"]++;
    }
}
```

2. ConcurrentDictionary: Alternatively, you can use the ConcurrentDictionary class, which is thread-safe by design and allows concurrent access from multiple threads without the need for locking.

Here's an example of how to use a ConcurrentDictionary:

```csharp
ConcurrentDictionary<string, int> dict = new ConcurrentDictionary<string, int>();
// Thread 1
dict.AddOrUpdate("key", 1, (key, value) => value + 1);
// Thread 2
dict.AddOrUpdate("key", 1, (key, value) => value + 1);
```

In this example, the ConcurrentDictionary is accessed by multiple threads using the AddOrUpdate method, which adds a new key-value pair if the key does not exist, or updates the existing value if the key already exists. The ConcurrentDictionary ensures that the updates are atomic and thread-safe, so there is no need for locking.
Overall, the choice of approach depends on the specific requirements and performance considerations of your application. If you have a low number of threads accessing the Dictionary infrequently, the locking approach may be sufficient. However, if you have a high number of threads accessing the Dictionary frequently, the ConcurrentDictionary may be a better option.

看上去不是想要的，继续第二轮提问：

Q: except the ConcurrentDictionary, any else?

A:

Yes, there are a few other thread-safe alternatives to using a ConcurrentDictionary in C#. One such alternative is the ReaderWriterLockSlim class.
The ReaderWriterLockSlim class allows multiple threads to read from a shared resource simultaneously, but only one thread to write to the resource at a time. This can be useful in scenarios where reads are more frequent than writes.
Here's an example of how to use a Dictionary with ReaderWriterLockSlim:

```csharp
Dictionary<string, int> dict = new Dictionary<string, int>();
ReaderWriterLockSlim dictLock = new ReaderWriterLockSlim();
// Thread 1 (Read)
dictLock.EnterReadLock();
int value;
if (dict.TryGetValue("key", out value)){
    Console.WriteLine(value);
}
dictLock.ExitReadLock();
// Thread 2 (Write)
dictLock.EnterWriteLock();
if (!dict.ContainsKey("key")){
    dict.Add("key", 1);
}else{
    dict["key"]++;
}
dictLock.ExitWriteLock();
```

In this example, the ReaderWriterLockSlim object is used to synchronize access to the Dictionary. The EnterReadLock method is called before reading from the Dictionary, and the ExitReadLock method is called after reading is complete. Similarly, the EnterWriteLock method is called before writing to the Dictionary, and the ExitWriteLock method is called after writing is complete.
Overall, the choice of approach depends on the specific requirements and performance considerations of your application. If you have a high number of reads and few writes, the ReaderWriterLockSlim approach may be more performant than using a ConcurrentDictionary. However, if you have a high number of writes, or if you require more complex thread synchronization, a ConcurrentDictionary or other synchronization mechanism may be a better option.

这一次它给出了我之前从来不知道的一个类 `ReaderWriterLockSlim`，而且通过文档查询，它的确满足我们的需求。因此各种 GPT 将来会是软件开发过程中一部分，每当遇到问题的时候，都可以求助它得到更多的提示。

## 行业资讯

1、[Visual Studio 17.5](https://devblogs.microsoft.com/visualstudio/visual-studio-2022-17-5-performance-enhancements/)

Visual Studio 17.5 已经发布，该版本改进了以下几个方面

1. 提高了.NET SDK 等项目的构建速度
2. 支持从外部源进行反编译
3. 更新了线程窗口
4. 改进了 Razor 和 C#编程体验
5. 增加了快速添加功能
6. 支持代码覆盖率统计

2、[Azure Developer - .NET Day](https://devblogs.microsoft.com/dotnet/announcing-azure-developers-dotnet-day/)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/dotnetday.png)

在西雅图时间的 4 月 5 日，微软将主办 .NET 专场的 Azure Developer 活动，届时将有演讲嘉宾出席。

## 文章推荐

1、[Rider 中使用 Git GUI](https://andrewlock.net/working-with-git-in-jetbrains-rider/)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/gitrider.png)

尽管通常情况下人们在命令行中使用 `Git`，但许多集成开发环境（IDE）都集成了 `Git` 的图形用户界面（GUI）操作。在本文中，作者分享了使用 Rider 编辑器时的 Git 使用体验。

2、[.NET 2023 路线图](https://www.youtube.com/watch?v=KCBH1fEIrxc&ab_channel=NickChapsas)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/roadmap.png)

知名博主 `Nick Chapsas` 分享了他的 .NET 2023 年中期路线图。

3、[ChatGPT 4 重构代码](https://www.youtube.com/watch?v=y_NHMGZMb14&ab_channel=NickChapsas)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/refactor.png)

大家都知道 `ChatGPT` 可以协助我们编写代码，而经过进一步升级的 `ChatGPT 4` 在编程方面有了更为显著的进步。在这个视频中，我们让 `ChatGPT 4` 对代码进行了重构，并提出了相应的要求。结果表明，`ChatGPT 4` 在这项任务中表现出色。

4、[.NET 生态发展历程](https://andrewlock.net/understanding-the-dotnet-ecosystem-the-evolution-of-dotnet-into-dotnet-7/)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/dotnethistory.png)

本文探讨了.NET 的发展历程，主要内容包括：

1. 回顾了.NET Framework、Mono 和 Unity 等各种实现，这些实现都有自己的基础库和应用程序模型。
2. 每个平台的基础库都提供了.NET 数据类型，但细微的差异存在。
3. .NET 5 统一了这些平台。
4. .NET 6 则统一了运行时和工具链。
5. .NET 每年都会发布新版本，并将其分为长期支持版本（LTS）和短期支持版本（STS）。

5、[async & await 是如何工作的](https://devblogs.microsoft.com/dotnet/how-async-await-really-works/)

这是一篇来自 `Stephen Toub` 的长文，他介绍整个 `.NET` 异步的历史和 `async`，`await` 的发展历程，非常值得一读。

## 开源项目

1、[ChatGPT .NET SDK](https://github.com/marcominerva/ChatGptNet)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/sdk.png)

ChatGPT 是目前备受瞩目的技术创新，而且随着 OpenAPI 开放 WebAPI 接口，各行各业都可以利用 ChatGPT 来提高生产力。ChatGptNET 是一个开源库，将 WebAPI 封装成 SDK，为大家提供了更便捷的接入方式。

2、[FluentValidation](https://github.com/FluentValidation/FluentValidation)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/validator.png)

FluentValidation 是一个开源的 `C#` 验证库，它可以验证某个对象事否满足要求。

```csharp
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Forename).NotEmpty().WithMessage("Please specify a first name");
        RuleFor(x => x.Discount).NotEqual(0).When(x => x.HasDiscount);
        RuleFor(x => x.Address).Length(20, 250);
        RuleFor(x => x.PostCode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
    }
    private bool BeAValidPostcode(string postcode)
    {
        //....
        return true;
    }
}

public class Customer
{
    public string Surname { get; set; }
    public string Forename { get; set; }
    public double Discount { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public bool HasDiscount { get; set; }
}
```

调用 `Validate` 方法可以得到验证的结果

```csharp
var customer = new Customer();
var validator = new CustomerValidator();
ValidationResult results = validator.Validate(customer);
Console.WriteLine(results.IsValid);
results.Errors.ForEach(Console.WriteLine);
```

3、[CSharpREPL](https://github.com/waf/CSharpRepl)

![image](https://dotnetweeklyimages.blob.core.windows.net/034/repl.png)

`CSharpRepl` 时一个开源的 `C#` 的 REPL，它有一下的功能

1. 智能提示
2. 主题管理
3. 添加引用
   - 动态库 `#r "AssemblyName"`
   - 其他项目 `#r "path/to/projects.csproj"`
   - Nuget 包 `# "nuget: package"`
