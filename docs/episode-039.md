# .NET 每周分享第 39 期

## 卷首语

`C#` 12 的新功能预览

1. `nameof` 可以访问成员变量，包括初始化，静态变量和注解
2. 内联数组： 可以标记 `[System.Runtime.CompilerServices.InlineArray(10)]` 方式在内存中创建 10 个元素
3. `Interceptors`: 这是一项实验功能，可以让特定的方法调用路由到不同的代码。

## 行业资讯

1、[Azure AD 改名 Microsoft Entra](https://devblogs.microsoft.com/dotnet/azure-ad-microsoft-entra/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/100f7b9f-7874-4892-91ad-772f2b6781af)

微软改名部继续工作，将 `Azure AD` 改名为 `Microsoft Entra`。这个改名对于 `.NET` 开发者而言，没有任何影响。

## 文章推荐

1、[最小的 Hello world 程序](https://blog.washi.dev/posts/tinysharp/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/107f17ea-0b91-4dae-b388-38d3bcaf94f7)

`Hello World` 是每个编程语言的第一个 demo， 那么对于 `C#` 而言，一个简单的 `Hello world` 应用程序会暂用多大的磁盘空间呢？

```csharp
using System;
namespace ConsoleApp1;
internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
```

这段代码在 `.NET Framework 4.7.2` 版本下， 生成可执行文件的大小为 `5kb`，那么作者是如何一步步将它减少体积的呢？

- 移除 `Nullable` 属性
- 手动创建 `.NET Module` 
- 移除引入和基础重定位
- 删除名称 metadata
- 删除更多的 metadata
- 不适用 `System.Console.WriteLine`

最终的 `Hello World` 文件大小定格在 476 字节。


2、[分析器认为有错误写法](https://www.youtube.com/watch?v=v7GAQfWnco0&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c5560288-8190-4a04-a7e0-22649fe1736d)

在 `.NET 8` 中， `Code Analysis` 为 C# 代码更多的检查，比如下面 6 种例子

1. ConstantExpected 

```csharp
public static void ProcessValue([ConstantExpected (Min = 20)]int value)
{
}
```
如果方法的参数增加了 `ConstantExpected` 的注解，那么要求方法的调用必须传入常量

2. Linq 中的 Any

```csharp
public static bool HasElement(string[] strings)
{
      return strings.Any();
}
```

通常 `Linq` 中的 `Any()` 方法可以判断是否存在元素，但是如果是具体的类型，比如 `Array`, `List` 等，则分析器提示使用 `strings.Length !=0` 的方式。

3. Split

```csharp
public static string[] SplitText(string text)
{
    return text.Split(new char[] { ' ', ',' });
}
```

在这个方法中，每次调用 `SplitText` 方法，都会创建一个 `char[]` 对象，这样增加了内存压力，所以分析器建议将 array 对象设置为一个只读对象，并且只初始化一次。


4. Cast

```csharp
var test = new int[] { 1 }.Cast<string>();
```

这个代码在运行的时候会抛出 `InvalidCastExpection` ，现在分析器会检测到这个错误并且在编译的时候给出这样提示。


在 Runtime repo 中的 [issue](https://github.com/dotnet/runtime/issues/78442)，它列出可以增加的分析功能。

3、[你应该学习 Blazor 吗？](https://www.youtube.com/watch?v=OUUlO8fQOfE&ab_channel=IAmTimCorey)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/aecb0b83-4296-4481-a80a-845839f7396f)

`Blazor` 的未来是怎样的？我们需要学习吗？这个视频给了一些介绍。

4、[高效连接字符串和字符](https://www.meziantou.net/micro-optimization-concatenating-a-string-with-a-char-using-string-concat.htm)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6b28e5f0-a7dd-4d6f-8048-79277efaee20)

C# 中字符串的 `+` 操作符是转换为 `string.Concat` 方法调用。如果是一个字符串连接一个字符呢？有没有什么其他方法，并且能够使用更高的性能呢？
`string.Concat` 有另一个方法签名，接受的参数类型是 `ReadOnlySpan<Char>`, 所以只需要将 `char` 类型转化为 `ReadOnlySpan<char>` 即可

```csharp
[MemoryDiagnoser]
public class StringConcatBenchmark
{
    private string Part1 = "abc";
    private char Part2_Char = 'd';
    private string Part2_String = "d";
    private string Part3 = "efg";

    [Benchmark(Baseline = true)]
    public string Operator_String_Char_String() => Part1 + Part2_Char + Part3;

    [Benchmark]
    public string Operator_String_String_String() => Part1 + Part2_String + Part3;

    [Benchmark]
    public string String_Concat() => string.Concat(Part1, new ReadOnlySpan<char>(in Part2_Char), Part3);
}
```

benchmark 结果如下
![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/f4bbaf0a-8b0a-4bd4-b1b9-d247cb0afedd)

5、[Powershell 定制自动任务](https://www.techtarget.com/searchwindowsserver/tutorial/Learn-how-to-create-a-scheduled-task-with-PowerShell)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/efd264f9-4d63-4e2e-af3e-86d8a13eec3e)

如果我们想要在 Widnows 中注册一个定时任务，使用 PowerShell 那么该怎么做呢？

1. 注册一个Action
```powershell
$Action = New-ScheduledTaskAction -Execute 'pwsh.exe' -Argument '-NonInteractive -NoLogo -NoProfile -File "C:\MyScript.ps1"'
```
首先通过 `New-ScheduledTaskAction` 注册一个 Action，它知名了执行的命令和相关的参数

2. 定制一个 Trigger

```powershell
$Trigger = New-ScheduledTaskTrigger -Once -At 3am
```

定义一个 `Trigger` 用来什么时候启动这个任务，比如每次早上 3 点运行

3. 配置

```csharp
$Settings = New-ScheduledTaskSettingsSet
```
通过从 `Setting` 配置任务的额外信息，比如执行超时，是否 retry 等等。

4. 创建 Task

```csharp
$Task = New-ScheduledTask -Action $Action -Trigger $Trigger -Settings $Settings
```
根据 `Action`, `Trigger` 和  `Setting` 创建要给 Task

5. 注册任务

```csharp
Register-ScheduledTask -TaskName 'My PowerShell Script' -InputObject $Task
```
目前任务是保存在内存中，通过 `Register-ScheduleTask` 将这个对象注册到操作系统中。

6、[写出 Clean Code 的 8 个技巧](https://www.milanjovanovic.tech/blog/8-tips-to-write-clean-code)

如果有这样一段 C# 代码，该如何优化呢？

```csharp
public void Process(Order? order)
{
    if (order != null)
    {
        if (order.IsVerified)
        {
            if (order.Items.Count > 0)
            {
                if (order.Items.Count > 15)
                {
                    throw new Exception(
                        "The order " + order.Id + " has too many items");
                }

                if (order.Status != "ReadyToProcess")
                {
                    throw new Exception(
                        "The order " + order.Id + " isn't ready to process");
                }

                order.IsProcessed = true;
            }
        }
    }
}
```

那么 8 个建议重构更好的代码
- 尽早返回
- 合并更多的 If 判断条件
- 使用 `Linq` 让代码更加简洁
- 使用特定的方法来重构 If 判断条件
- 抛出自定义异常
- 将魔数转换为常量
- 将字符串转化为枚举类型
- 使用返回结果类型，而不是直接修改输入

7、[如何正确的使用 HttpClient 类](https://www.milanjovanovic.tech/blog/the-right-way-to-use-httpclient-in-dotnet)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6c6497f7-7f93-4c6b-8a7e-e1c191cd793d)

`HttpClient` 类是 `C#` 代码中广泛使用的类型，那么该怎么最好的使用这个类型呢？

1. 普通方式

```csharp
 var client = new HttpClient();
//...
```

2. 使用 `IHttpClientFactory` 

```csharp
var client = _factory.CreateClient();
//....
```

`Microsoft.Extensions.Http` 推荐的使用方式


3. Name Client

```csharpservices.AddHttpClient("github", (serviceProvider, client) =>
{
    var settings = serviceProvider
        .GetRequiredService<IOptions<GitHubSettings>>().Value;
    client.BaseAddress = new Uri("https://api.github.com");
});

 var client = _factory.CreateClient("github");
```

3. Type client

```csharp
services.AddHttpClient<GitHubService>((serviceProvider, client) =>
{
    var settings = serviceProvider
        .GetRequiredService<IOptions<GitHubSettings>>().Value;
    client.BaseAddress = new Uri("https://api.github.com");
});

public class GitHubService
{
    private readonly HttpClient client;

    public GitHubService(HttpClient client)
    {
        _client = client;
    }

    public async Task<GitHubUser?> GetUserAsync(string username)
    {
        GitHubUser? user = await client
            .GetFromJsonAsync<GitHubUser>($"users/{username}");

        return user;
    }
}
```
这样在应用程序中，只需要在依赖注入框架中获取 `GithubSerivce` 对象即可，注意这是一个 `transient` 注入方式。

## 开源项目

1、[MSBuilder 编辑器](https://marketplace.visualstudio.com/items?itemName=mhutch.MSBuildEditor)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/43e574ec-2a6a-4b8e-aef2-3f6b69956eef)

`MSBuild Editor` 插件是用来管理 C# 的 MSBuild 文件，主要有下面几个功能

- 智能提示
- 导航
- 快速查询
- 验证和分析
- 重构和代码修复
- schemas 修改
- 导入

2、[TextUtility](https://github.com/PowerShell/TextUtility)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/2824c29a-5bbe-45a8-a8c0-793a711901eb)

`Microsoft.Powershell.TextUtility` 是 `Powershell` 中的文本处理库，只要包含一下三个函数

1. Compare-Text

它实现了 [diff-match-path](https://github.com/google/diff-match-patch) 

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/36386ad2-3f90-485c-9c31-8f5e5974fc87)

2. ConvertFrom-Base64/ConvertTo-Base64

它主要是将字符串转换成 Base64 或者将 Base64 转成成字符串。

```powershell
> ConvertTo-Base64 "dotnetweekly"
# ZG90bmV0d2Vla2x5
> ConvertFrom-Base64 "ZG90bmV0d2Vla2x5"
# dotnetweekly
```

3. ConvertFrom-TextTable

这个主要是将文本的表格转换成一个实体对象

```csharp
> $string = $("a".."z";"A".."Z";0..9) -join ""
> 1..10 | %{$string}|convertfrom-texttable -noheader -columnoffset 0,15,23,40,55 | ft
```
![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/1827e126-e582-40a5-9570-ba7e92bb6102)

