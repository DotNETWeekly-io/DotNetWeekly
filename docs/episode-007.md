# .NET 每周分享第 7 期

## 开卷语

新春快乐

## 文章推荐

1、[优化 .NET 性能的 11 个最佳实践](https://michaelscodingspot.com/cpu-bound-memory-bound/)

![net](https://dotnetweeklyimages.blob.core.windows.net/007/performance.jpeg)

应用程序的性能通常会分为两种

- Memory-Bound
- CPU-Bound

如果我们的 `.NET` 程序的性能出现了问题，那么首先就需要确定属于哪方面出现了瓶颈，针对不同的情况，有不同的调优方法。

2、[.NET Framework 和 .NET Core 介绍](https://procodeguide.com/dotnet/getting-started-net-core-framework/)

![netfxcore](https://dotnetweeklyimages.blob.core.windows.net/007/dotnet-core-framework.png)

`.NET Framework` 和 `.NET Core` 是 `.NET` 世界讨论最多的两个话题。一个代表了过去，一个代表了现在和未来。这系列文章能够再一次帮你回顾一下它们的联系和区别。

3、[避免 Logger 错误的使用姿势](https://www.youtube.com/watch?v=bnVfrd3lRv8&ab_channel=NickChapsas)

在任何一个应用程序中日志是非常重要的，在 `.NET` 中广泛使用的日志库就是 `Microsoft.Extensions.Logger` ，但是在使用的时候如果使用不注意，会导致性能方面的问题。

```Csharp
private ILogger<Program> _logger;
public void Method() {
   _logger.LogInformation("This is the {0} days",  days);
}
```

当 `_logger` 的输出 level 小于或者等于 `Infomation` 的时候，才会输出结果。但是 `LogInformation` 的方法的签名是

```Csharp
public static void LogInformation(this ILogger logger, string? message, params object?[] args)
{
     logger.Log(LogLevel.Information, message, args);
}
```

所以即使 `LogLevel=Warning`，也会带来了装箱（Box）的过程，增加了内存的消耗。因此需要在日志输出前进行判断。

```Csharp
public void Method() {
   if (_logger.IsEnable(LogLevel.Infomation)
   {
           _logger.LogInformation("This is the {0} days",  days);
   }
}
```

4、[使用 DateTimeOffset](https://ardalis.com/why-use-datetimeoffset/?utm_sq=gyiamfvfod)

![datetimeoffset](https://dotnetweeklyimages.blob.core.windows.net/007/datetimeoffset.png)

`DataTime` 是在 `C#` 中广泛使用地类型，但是它最大地问题是它没有包含时区信息，而是根据运行系统确定时区，所以导致了这样一个问题。

```Csharp
var rightNow = new DateTime(2022, 1, 30, 10, 50, 0);
rightNow.ToLocalTime(); // 1/30/2022 6:50:00 PM
rightNow.ToUniversalTime(); // 1/30/2022 2:50:00 AM
```

- ToLocalTime() 默认是从 UTC + 0 转换到本地时间
- ToUniversalTime() 默认是从本地时间转化成 UTC+0 时间

这样导致了在不同的机器上执行的结果是不一致的，这是我们需要 `DataTimeOffset` 类型，其包含了时区信息。

```Csharp
var rightNow = new DateTime(2022, 1, 30, 10, 50, 0);
DateTimeOffset rightNowHere = new DateTimeOffset(rightNow);
rightNowHere.ToLocalTime(); // 1/30/2022 10:50:00 AM +08:00
rightNow.ToUniversalTime(); //  1/30/2022 2:50:00 AM
```

4、[ASP.NET Core 6 中性能提升](https://devblogs.microsoft.com/dotnet/performance-improvements-in-aspnet-core-6/)

![aspnetcore](https://dotnetweeklyimages.blob.core.windows.net/007/aspnet-core-performance.png)

得益于 `.NET 6` 在性能方面的提升，`ASP.NET Core 6` 在性能方面也到了提升，这边文章带你展示了这些提升的示例。

- Span`<T>`：使用 `Span<T>` 可以大幅提高字符串操作方面性能的提升。
- Idle Connection: 在 `ASP.NET Core` 应用程序中广泛使网络链接，`ASP.NET Core 6` 中通过三种类型的数据类型来提高性能。
- Blazor： 之前 `byte[]` 和 `Javascript` 中进行数据交换的使用 `Base64` 编码，这并不是一个高效的做饭，在 `ASP.NET Core` 中使用 `byte[]` 和 `Uint8Array` 进行转换。
- ...

## 开源项目

1、[防御性编程](https://github.com/safakgur/guard)

![guard](https://dotnetweeklyimages.blob.core.windows.net/007/guard.png)

防御性编程要求在函数在接受参数的时候，验证这些参数的合法性，比如说

```Csharp
public Person(string name, int age)
{
    if (name == null)
        throw new ArgumentNullException(nameof(name), "Name cannot be null.");

    if (name.Length == 0)
        throw new ArgumentException("Name cannot be empty.", nameof(name));

    if (age < 0)
        throw new ArgumentOutOfRangeException(nameof(age), age, "Age cannot be negative.");

    Name = name;
    Age = age;
}
```

这样的话参数越多，函数中验证部分的逻辑就越长。`Guard` 这个库可以帮助我们的很好地验证函数地参数。

```Csharp
using Dawn; // Bring Guard into scope.

public Person(string name, int age)
{
    Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
    Age = Guard.Argument(age, nameof(age)).NotNegative();
}
```

2、[PowerShell RDP](https://github.com/DarkCoderSc/PowerRemoteDesktop)

![ps](https://dotnetweeklyimages.blob.core.windows.net/007/powershell-rdp.png)

我们都知道 `PowerShell` 很 `Power`, 这个开源项目实现了通过 `PowerShell` 实现了 `RDP (Remote Desktop Protocols)` 。
