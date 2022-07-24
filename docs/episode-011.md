# .NET 每周分享第 11 期

## 行业资讯

1、[Visual Studio 25 周年纪念](https://devblogs.microsoft.com/visualstudio/happy-25th-birthday-visual-studio/)

![vs](https://dotnetweeklyimages.blob.core.windows.net/011/vs-25.png)

25 年前，`Visual Studio` 诞生了，作为*宇宙第一 IDE*，引导和启发了无数人进入了软件开发领域，如今它已经进入了第 25 个年月。最近的版本是 `Visual Stuido 2022`， 也是第一款 64 位的 IDE。微软的目标是将它作为任何编程语言，任何目标平台的开发环境第一选择。事实上，`Visual Studio` 差不多已经做到了，[官方视频活动](https://www.youtube.com/watch?v=hATPpSCQ6u8&feature=emb_imp_woyt&ab_channel=MicrosoftVisualStudio)。

## 文章推荐

1、[C# 属性的演化](https://www.youtube.com/watch?v=RqdZCq-2GNM)

![csharp](https://dotnetweeklyimages.blob.core.windows.net/011/property.jpeg)

C# 中的属性是非常重要的设计，区别于 `Java` 中的 字段和 `getter, setter` 方法。 C# 几乎在每个版本中都为属性增加了新的功能。接下来这篇文章带你回顾其中的变化。

1. C# 1.0

```Csharp
public class User
{
    private string _firstName;
    public string FirstName
    {
       get { return _fristName; }
       set { _firname = value; }
    }
}
```

在 1.0 时代，c# 的属性就比 `Java` 中的 Getter 和 Setter 领先了一大截。，通过 `get, set` 两个关键字，避免了编写大量的方法。

2. C# 3.0

```Csharp
public class User
{
    public string FirstName {get; private set; }
}
```

在 2.0 中， 不需要显式的写一个字段，而是通过通过编译器为其增加一个隐藏字段 `<FirstName>_BackingField`。同时也可以设置 `get` 和 `set` 的访问权限。

4. C# 6.0

```Csharp
public class User
{
    public string FirstName {get; set; } = "Foo";
    public string LastName => "bar";
}
```

在 6.0 中，属性可以设置初始化值，也可以采用 `Lambada` 表达式。

5. C# 8.0

```Csharp
public class User
{
    public string FirstName {get; set; }
}

var person = new User { FirstName = "Foo" };
if (person is { FirstName : "Foo" } )
{
    //....
}
```

在 8.0 中，对于属性可以进行拆包判断。

6. C# 9

```Csharp
public class User
{
     public string FirstName { get; set;}
     public DateOnly DateOfBirth { get; init }
}
```

属性的 `set` 的权限可以设置为 `private` 来控制访问权限，所以需要在构造函数中，设置改属性的值。在 9.0 版本中，`init` 可以让设置只发生第一次，后续的设置编译器会报错。

7. C# 10

```Csharp
public reocrd User(string FirstName, string LastName);
```

在 1.0 版本中引入了 `record`， 如果一个类只有属性，然后可以采用 `record`, 这样只需要在构造的时候声明属性。

2、[ASP.NET Core pipeline 是如何构建的](https://www.stevejgordon.co.uk/how-is-the-asp-net-core-middleware-pipeline-built)

![aspnet](https://dotnetweeklyimages.blob.core.windows.net/011/asp-net-core-pipeline.png)

这篇文章带你一步步探索 `ASP.NET Core` 是如何构建中间件的 pipeline.

3、[正确使用 DateTime 的日期](https://code-maze.com/remove-time-from-datetime-csharp/)

C# 内置的库提供了时间的类 `DateTime` ， 不过这个类既包含了日期，也包含了时间。所以我们需要正确的区分它们

1. 使用 `Date` 属性

`DateTime` 中的 `Date` 属性只返回日期

```Csharp
var date1 = new DateTime(2022, 02, 14, 10, 40, 00);
var date2 = new DateTime(2018, 10, 18, 11, 23, 34);
Console.WriteLine(date1.Date.ToString()); //2/14/2022 12:00:00 AM
Console.WriteLine(date2.Date.ToString()); //10/18/2018 12:00:00 AM
```

2. 使用 `ToString()` 格式化数据

Date 的 `ToString` 方法可以自定义日期输出格式

```Csharp
var date1 = new DateTime(2022, 02, 14, 10, 40, 00);
var date2 = new DateTime(2018, 10, 18, 11, 23, 34);
Console.WriteLine("Hide the time part:");
Console.WriteLine(date1.Date.ToString("MM/dd/yyyy")); //02/14/2022
Console.WriteLine(date2.Date.ToString("dd/MM/yyyy")); //18/10/2018
```

当然也可以使用 `ToShortDateString` 转换成短的日期表达

```Csharp
var date3 = new DateTime(2022, 02, 14, 10, 40, 00);
Console.WriteLine("Short Date Value:");
Console.WriteLine(date3.Date.ToShortDateString()); //2/14/2022
```

3. 使用 `DateOnly` 类型

在 `.NET 6` 中增加了一个新的类型 `DateOnly`，它可以用来只表示日期类型

```Csharp
var date = new DateTime(2021, 7, 8, 11, 10, 9);
var dateOnly = new DateOnly(date.Year, date.Month, date.Day);

Console.WriteLine(dateOnly); //7/8/2021
```

4、[如何构建一个 Windows 服务](https://csharp.christiannagel.com/2022/03/22/windowsservice-2/)

![windows](https://dotnetweeklyimages.blob.core.windows.net/011/service.png)

Windows 服务是一类长期运行的应用程序，在 `Linux` 中也叫做守护进程。如何在 `.NET6` 中如何开发一个 `Service` 呢？

1. 实现 `IHostedService` 接口

在 `IHost` 服务是一种长时间运行的容器，它会管理所有实现 `IHostedService` 的服务，并且长时间运行。通常这些服务都是通过依赖注入完成的

```Csharp
public class Worker : BackgroundService
{
    protected override async Task ExexcuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
```

这里 `BackgroundService` 实现了 `IHostedService`

2. 转换 Windows Service 服务

`Microsoft.Extensions.Hosting.WindowsService` 包提供了一站式服务

```Csharp
IHost host = Host.CreateDefaultBuilder(args)
      .ConfigureService(service => {
        services.AddHostedService<Worker>();
     }
    ).UseWindowsService()
   .Build();
```

3. 安装 `Windows` 服务

```bash
sc create "Sample Service" binPath = "./SimpleWorkerService.exe"
```

[sc](https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/sc-create) 命令用力管理 Windows 服务

4. Linux 守护进程

那么在 `Linux` 中如何开发一个守护进程呢？ 在前面的例子中，我们使用 `UseWindowsService` 拓展方法，在 `Linux` 中只要安装 `Microsoft.Extensions.Hosting.WindowsService` 包，然后使用 `UseSystemd` 拓展方法。

## 开源项目

1、[更加优雅的抛出异常](https://github.com/mantinband/throw)

![exception](https://dotnetweeklyimages.blob.core.windows.net/011/exception.png)

正确的数据才能带来正确地结果，对于不正确的数据，需要通过抛出异常让调用者知道出现了未知的问题。通常的做法是手动抛出一个异常

```Csharp
throw new Exception();
```

如果我们有很多判断条件，比如字符串有非空，长度大小，开始字符等限制条件，那么就需要写很多判断条件并依次抛出异常，这样做一点也不优雅。`throw` 这个开源库可以很优雅的解决这个问题

- 判断字符长度是否为 3

```Csharp
name.ThrowIfNull()
    .IfEmpty()
    .IfLongerThan(3);
```

- 判断大小

```Csharp
dateTime.Throw().IfLessThan(DateTime.Now.AddYears(20));
```

- 判断集合大小

```Csharp
collection.Throw().IfCountLessThan(5);
```

等等，本质上讲通过 `C#` 的拓展方法来完成各种判断方法，当然也可以自定义自己的判断逻辑

```Csharp
namespace Throw
{
    public static class ValidatableExtensions
    {
        public static ref readonly Validatable<string> IfFoo(this in Validatable<string> validatable)
        {
            if (string.Equals(validatable.Value, "foo", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("String shouldn't equal 'foo'", validatable.ParamName);
            }

            return ref validatable;
        }
    }
}
```

调用方法： `"foo".Throw().IfFoo();`

2、[System.CommandLine 文档](https://docs.microsoft.com/en-us/dotnet/standard/commandline/)

![commandline](https://dotnetweeklyimages.blob.core.windows.net/011/sys-command-line.png)

`System.CommandLine` 库可以帮助开发命令行的应用程序，用它来解析命令行的参数。目前的官方的 `System.CommandLine` 库的文档已经开放。
