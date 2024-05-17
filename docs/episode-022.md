# .NET 每周分享第 22 期

## 卷首语

[IEnumerable 不为人知的陷阱](https://www.youtube.com/watch?v=cLsmW7a8MkU&ab_channel=NickChapsas)

![image](https://user-images.githubusercontent.com/11272110/188250895-f3857c2e-18e8-4b3e-bf17-fdda612a6ae4.png)

```Csharp
IEnumerable<Person> GetPersons()
{
    var lines = File.ReadAllLines("./Person.csv");
    foreach (var line in lines)
    {
        var segments = line.Split(',');
        yield return new Person(segments[0], int.Parse(segments[1]));
    }
}
var persons = GetPersons();
var cnt = persons.Count();
Console.WriteLine($"There are {cnt} persons");

foreach (var person in persons)
{
    Console.WriteLine(person);
}
record Person(string Name, int Age);
```

对于上述代码，`GetPersons` 方法会调用几次呢？直觉告诉我们只会执行一次，但是并非如此，在

- `Count()`
- `foreach`

两个方法中都调用了 `GetPerson` 的实现，也就是执行了两边，为什么会这样呢？其实编译器会将 `GetPersons` 方法转换成下面的实现

```csharp
private IEnumerable<Person> GetPersons()
{
    return new <GetPersons>d__1(-2);
}
```

而 `<GetPersons>d__1` 类型为定义

```csharp
private sealed class <GetPersons>d__1 : IEnumerable<Person>
{
    private int <>1__state;
    private Person <>2__current;
    private int <>l__initialThreadId;
    Person IEnumerator<Person>.Current
    {
        get { return <>2__current; }
    }

    public <GetPersons>d__1(int <>1__state)
    {
        this.<>1__state = <>1__state;
        <>l__initialThreadId = Environment.CurrentManagedThreadId;
    }
    private bool MoveNext()
    {
        int num = <>1__state;
        if (num != 0)
        {
            if (num != 1)
            {
                return false;
            }
            <>1__state = -1;
            return false;
        }
        <>1__state = -1;
        <>2__current = new Person("fenga", 10);
        <>1__state = 1;
        return true;
    }
    IEnumerator<Person> IEnumerable<Person>.GetEnumerator()
    {
        if (<>1__state == -2 && <>l__initialThreadId == Environment.CurrentManagedThreadId)
            {
                <>1__state = 0;
                return this;
            }
            return new <GetPersons>d__1(0);
        }
    }
```

而 `Work` 方法编译的结果如下

```csharp
public void Work()
{
    IEnumerable<Person> persons = GetPersons();
    Console.WriteLine(Enumerable.Count(persons));
    IEnumerator<Person> enumerator = persons.GetEnumerator();
    try
    {
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }
    }
    finally
    {
        if (enumerator != null)
        {
            enumerator.Dispose();
        }
    }
}
```

在 `Count()` 方法中，调用了 `Eunerable.Count()` 方法

```Csharp
int count = 0;
using (IEnumerator<TSource> e = source.GetEnumerator())
{
    checked
    {
        while (e.MoveNext)
        {
            count++;
        }
    }
 }
```

所以重点来到了生成的 `GetEnumerator` 方法，

```csharp
IEnumerator<Person> IEnumerable<Person>.GetEnumerator()
{
    if (<>1__state == -2 && <>l__initialThreadId == Environment.CurrentManagedThreadId)
    {
        <>1__state = 0;
        return this;
    }
    return new <GetPersons>d__1(0);
 }
```

可以清楚的知道，在第二次迭代的时候，重新创建了一个 `<GetPersons>d_1` 对象实例，在其中的 `MoveNext` 方法中，又重新执行对象的创建。

那么怎么解决这个问题呢？答案是将其转换成具体的类型，比如 `List<Person>` 对象。

## 行业资讯

1、[.NET 7 性能提高汇总](https://devblogs.microsoft.com/dotnet/dotnet-conf-focus-on-maui-recap/)

![image](https://user-images.githubusercontent.com/11272110/189510878-94449fed-05d9-41d4-a9e8-878a0b25f387.png)

`.NET 7` 即将要在两个月后发布，最近 `Stephen Toub` 发表了一篇文章，着重介绍了 `.NET 7` 在性能提升上做出的努力。

## 文章推荐

1、[.NET 6 中的文件读写](https://adamsitnik.com/files/Fast_File_IO_with_DOTNET_6.pdf)

![image](https://user-images.githubusercontent.com/11272110/188250252-ce0fc1db-312d-4873-8b83-7a0b154bd4b1.png)

.NET 6 对文件 I/O 操作上有很大的提升，这篇文档带你详细分析这方面的提升。

2、[.NET SDK 将提供内置容器支持](https://devblogs.microsoft.com/dotnet/announcing-builtin-container-support-for-the-dotnet-sdk/)

![image](https://user-images.githubusercontent.com/11272110/189487357-76fe268c-dad2-4a0a-a016-40842ffd40cd.png)

在 .NET 诞生之初，它就对 `Container` 做了大量的支持。虽然我们可以通过 `Dockerfile` 来配置，但是新的 `.NET` SDK 已经内置了对它的支持，比如在 `csproj` 文件中配置相关信息。

3、[.NET 开发者必备的工具箱](https://jesseliberty.com/2022/09/02/a-dozen-utilities-every-net-programmer-needs/)

![image](https://user-images.githubusercontent.com/11272110/188252184-edf16def-438c-409d-bcee-2d24e6fe7fe8.png)

这篇文章列出了一个 `.NET` 开发者必备的工具箱。

4、[.NET 6 中新的 Timer](https://www.ilkayilknur.com/a-new-modern-timer-api-in-dotnet-6-periodictimer)

![image](https://user-images.githubusercontent.com/11272110/189487562-9974344a-ada7-4901-b469-d43184aba671.png)

在 `.NET 6` 之前，在 `C#` 中如下的的这些计时器

- `System.Threading.Timer`
- `System.Timers.Timer`
- `System.Windows.Forms.Timer`
- `System.Web.UI.Timer`
- `System.Windows.Threading.DispatcherTimer`

一般我们用的最多的是 `System.Threading.Timer` 类型，一个典型的用法如下

```csharp
Timer timer = new Timer(new TimeCallback(DoWork), null, 1000, 100);

static void DoWork(object state)
{
     // ...
}
```

`DoWork` 在定时器每次 `Tick` 的时候执行，但是使用 `Callback` 方式，这样做增加的复杂度和资源泄露的问题。 在 `.NET 6` 中引入了 `PeriodicTimer` 类型，使用方式也非常直接

```csharp
var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
while (await timer.WaitForNextTickAsync())
{
    //Business logic
}
```

这种使用方式的好处有：

- 没有使用回调的方式
- 支持 `CancellationToken` 方式取消定时器
- 支持异步调用

5、[深入解析 Async/Await 生成的代码](https://itnext.io/async-await-what-happens-under-the-hood-eef1de0dd881)

![image](https://user-images.githubusercontent.com/11272110/189509971-3a02f739-eb5e-43ed-84e5-995c26f74c18.png)

`async/await` 是 `C#` 中的异步的实现，我们也知道编译器在后台完成了大量的工作。那么这篇文章就逐行介绍一个异步方法背后生成的代码。

6、[LINQ 插图展示](https://steven-giesel.com/blogPost/d65c5411-a69b-489f-b73f-18ce0ed8678d?utm_source=csharpdigest&utm_medium=web&utm_campaign=427)

![image](https://user-images.githubusercontent.com/11272110/189510239-6c047f99-ec21-41a7-b2cc-4188b94b0415.png)

Linq 很难懂？这篇给文章通过手绘的方式展示了各个 Linq 的功能，简单而明了。

7、[GC Internal 教程](https://www.youtube.com/playlist?list=PLpUkQYy-K8Y-wYcDgDXKhfs6OT8fFQtVm)

![image](https://user-images.githubusercontent.com/11272110/189510517-d1328027-615e-4261-83d1-87fcb9950f9b.png)

这是一系列 `.NET GC` 的教程，详细介绍了 `.NET` GC 的全部内容，包含了

- Mark Phase
- Concurrent Mark Phase
- Plan Phase
- Sweep Phase
- Compact Phase
- Allocation
- Generations
- GC Roots

## 开源项目

1、[ILSpy](https://github.com/icsharpcode/ILSpy)

ILSpy 是一个开源的 .NET 程序集浏览器和反编译工具，该工具支持 PDB,ReadyToRun, Metadata 还有其他类型的文件信息，同时该工具是跨平台的。

2、[ML.NET](https://github.com/dotnet/machinelearning)

ML.NET 允许开发人员在其 .NET 应用程序中轻松生成、训练、部署和使用自定义模型，而无需事先具备开发机器学习模型的专业知识或使用其他编程语言（如 Python 或 R）的经验。该框架提供从文件和数据库加载数据，支持数据转换，并包括许多 ML 算法。
