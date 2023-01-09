# .NET 每周分享第 30 期

## 卷首语

新年快乐！

## 行业资讯

1、[Oracle EF Core 库发布](https://medium.com/oracledevs/announcing-oracle-entity-framework-core-7-d89a2495b7db)

![image](https://dotnetweeklyimages.blob.core.windows.net/030/oracle.png)

上个月 Oracle 发布了 Entity Framework Core7 支持库。

2、[2022 年度视频](https://devblogs.microsoft.com/dotnet/top-dotnet-videos-live-streams-of-2022/)

2022 年度 `.NET` 社区视频汇总。

3、[2022 年度文章](https://devblogs.microsoft.com/dotnet/top-dotnet-blog-posts-of-2022/)

2022 年度 `.NET` 社区博客汇总。

## 文章推荐

1、[使用 Tuple 进行拆包](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct)

![image](https://dotnetweeklyimages.blob.core.windows.net/030/tuple.png)

Python 语言中一大特色就是 `tuple`，通过它可以返回多个数据。C# 中的 `Tuple` 类型也能完成同样的事情，比如定义一个这样的方法

```csharp
static (string, int, double) QueryCityData(string name)
{
    if (name == "New York City")
        return (name, 8175133, 431.456);
    return (string.Empty, 0, 0);
}
```

1.  显式指定字段类型

```csharp
(string city1, int population1, double area1) = QueryCityData("New York City");
```

2. 使用 `var` 类型

```csharp
var (city2, population2, area2)   = QueryCityData("");
```

3. 使用部分 var 类型

```csharp
(string city3, var popluation3, var area3) = QueryCityData("");
```

4. 使用已经声明的变量

```csharp
string city4 = "redmond";
(city4, var popluation4, var area4) = QueryCityData("New York City");
```

5. 使用 `_` 丢弃部分字段

```csharp
var (city5, _, _) = QueryCityData(string.Empty);
```

对于有些数据类，如果定义了 `Deconstruct` 方法，那么也可以使用类似 `tuple` 的用法

```csharp
public class Person
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public Person(string fname, string mname, string lname,
                  string cityName, string stateName)
    {
        FirstName = fname;
        MiddleName = mname;
        LastName = lname;
        City = cityName;
        State = stateName;
    }

    // Return the first and last name.
    public void Deconstruct(out string fname, out string lname)
    {
        fname = FirstName;
        lname = LastName;
    }

    public void Deconstruct(out string fname, out string mname, out string lname)
    {
        fname = FirstName;
        mname = MiddleName;
        lname = LastName;
    }

    public void Deconstruct(out string fname, out string lname,
                            out string city, out string state)
    {
        fname = FirstName;
        lname = LastName;
        city = City;
        state = State;
    }
}

var p = new Person("John", "Quincy", "Adams", "Boston", "MA");
var (fName, lName, city, state) = p;
```

对于外部的类， 可以选择使用拓展方法来支持这个功能这个功能

```csharp
public static class NullableExtensions
{
    public static void Deconstruct<T>(
        this T? nullable,
        out bool hasValue,
        out T value) where T : struct
    {
        hasValue = nullable.HasValue;
        value = nullable.GetValueOrDefault();
    }
}

int? val = 10;
var (existing, value) = val;
```

像 `record` 类型的数据，编译器会自动创建一个 `Deconstruct` 方法。 那么其中的奥秘是什么呢？答案是在使用拆包的使用，编译器会自动将其转换成调用 `Deconstruct` 方法。

```csharp
var p = new Person("John", "Quincy", "Adams", "Boston", "MA");
p.Deconstruct(out var fName, out var lName, out var city,  out var state) ;
```

2、[接口默认方法](https://code-maze.com/csharp-default-interface-method/)

![image](https://dotnetweeklyimages.blob.core.windows.net/030/defaultmethod.png)

`C# 8` 改进了 `Interface` 的行为，除了定义方法之外，还可以提供具体的默认方法，比如说下面的方法：

```csharp
public interface ICalendar
{
    public DateTime Date { get; set; }
    public string ShowMessage()
    {
        return "Default Calendar";
    }
}
```

那么就会带来一个问题，如果一个具体的类实现了多个接口，而且这些接口定了两个相同的默认方法。这就导致了诸如 `C++` 中多重继承的[钻石问题](https://www.educative.io/answers/what-is-a-diamond-problem-in-object-oriented-programming)。C# 8.0 解决这个问题是要求实现类必须重新实现这个默认方法。

3、[Await Task 还是 Return Task?](https://www.youtube.com/watch?v=aaC16Fv2zes&ab_channel=NickChapsas)

有下面两个方法

```csharp
public Task<string> GetNoAwait()
{
    HttpClient client = new HttpClient();
    return client.GetStringAsync("htttps://api.github.com/users/gaufung");
}

public async Task<string> GetAwait()
{
    HttpClient client = new HttpClient();
    return await client.GetStringAsync("https://api.github.com/users/gaufung")
}
```

在调用的时候代码

```csharp
await GetNoAwait();
await GetAwait();
```

那么两者有什么区别呢？

- 性能
  没有 `await` 的方法的性能比较好，如果在一些库中可以选择这种方式

- 正确性

如果在异步方法中存在 `using` 语句，没有 `await` 的方法可能是错误的，因为

```csharp
public Task<string> GetNoAwait()
{
    using HttpClient client = new HttpClient();
    return client.GetStringAsync("https://api.github.com/users/gaufung");
}
```

会转换成这样的代码

```csharp
public Task<string> GetNoAwait()
{
    HttpClient httpClient = new HttpClient();
    try
    {
        return httpClient.GetStringAsync("https://api.github.com/users/gaufung");
    }
    finally
    {
        if (httpClient != null)
        {
            ((IDisposable)httpClient).Dispose();
        }
    }
  }
```

从中可以看出 `GetStringAsync` 方法返回时，`httpClient` 就在 `finaaly` 语句中被释放资源。由于异步的特性，该代码可能会出现 bug

- Debug

没有 `Await` 的语句中，如果出现异常，调用栈只会展示到调用方而不是真正的位置。

4、[C# 代码中 Performance 最佳实践](https://minidump.net/performance-best-practices-in-c-b85a47bdd93a)

Kevin Gosse 是 `.NET` 社区著名的开发者，尤其是在编译器方面，这篇文章他描述了如何写出高性能的 `C#` 代码。

- 不要在异步代码中使用同步代码
- `ConfigurationAwiat` 在非异步代码中无效
- 编译异步方法返回 `void`
- 尽可能比避免 `async` 修饰符
- 避免使用 `culture-sensitive` 比较
- 避免使用 `ConcurrentBag<T>`
- 不要使用 `ReaderWriterLock<T> / ReaderWriterLockSlim<T>`
- 使用 `Lambda` 表达式而不是 `MethodGroup`
- 将 `Enum` 转换成 `String`
- 为 `Struct` 提供相等方法
- 避免在 `Struct` 接口中装箱
- 使用 `Task.Run` 而不是 `Task.Factory.StartNew`
- ...

5、[.NET Tips & Tricks 集锦](https://linkdotnet.github.io/tips-and-tricks/)

这是一份开源 `.NET` 的 `Tips & Tricks` 文档， 主要包含

- Array
- Async/Await
- Blazor
- Collections
- Debugging
- Dictionary
- Exceptions
- LINQ
- Strings
- ValueTuples
- EF Core
- Logging
- Nuget
- Regular Expression

## 开源项目

1、[C# 初学者教程](https://www.youtube.com/watch?v=Z5JS36NlJiU&ab_channel=dotnet)

![image](https://dotnetweeklyimages.blob.core.windows.net/030/csharpbeginner.png)

最近微软推出了 `C#` 初学者教学视频，通过网页版交互式学习方式对入门者也是非常友好。

2、[ScottPlot](https://github.com/dfinke/PowerShellScottPlot)

![image](https://dotnetweeklyimages.blob.core.windows.net/030/scottplot.png)

`Polyglot` 是微软推出的 `Jupyter` 的 `.NET` 核，通过它可以在 `Jupyter Notebook` 中编写 `C#` , `F#` 或者 `Powershell` 代码。`ScottPlot` 是 `Powershell` 支持的扩展，通过它可以绘制出精美的统计图。

3、[2022 年 .NET Foundation 最活跃的项目](https://www.dfnprojets.com)

![image](https://dotnetweeklyimages.blob.core.windows.net/030/opensource.png)
