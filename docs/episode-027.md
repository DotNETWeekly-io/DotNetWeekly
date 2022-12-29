# .NET 每周分享第 27 期

## 卷首语

[.NET Conf 所有视频列表](https://www.youtube.com/watch?v=8V_BUGFKdaI&list=PLdo4fOcmZ0oVlqu_V8EXUDDnPsYwemxjn&index=1&ab_channel=dotnet)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/.netconfi.png)

`.NET Conf 2022` 已经结束了，这里的 Youtube 播放清单是今年的所有视频集合。

## 行业资讯

1、[Fleet 支持 C#](https://blog.jetbrains.com/dotnet/2022/11/17/csharp-support-in-fleet/?utm_campaign=fleet&utm_content=blog-post-cs-fleet&utm_medium=referral&utm_source=twitter)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/fleet.png)

`Fleet` 是 JetBrains 公司最新推出的集成开发环境，它支持远程开发，并且通过 LSP 支持个多种开发语言。最近 `JetBrains` 宣布 `Fleet` 已经支持 C# 语言。

2、[JetBrains .NET Day 视频汇总](https://www.youtube.com/playlist?list=PLQ176FUIyIUbSS1HBVrqEyNHpfSh166KW)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/.netopenday.png)

今年 10 月 25 号，`JetBrains` 公司举办了 `.NET Day`, 会议是线上举办的，但是丝毫不影响会议的质量，这里是分享视频汇总。

## 文章推荐

1、[Stackoverflow 使用 .NET](https://devblogs.microsoft.com/visualstudio/learn-github-in-visual-studio-learning-series/)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/so.png)

[Stack Overflow](https://stackoverflow.com/) 是编程行业著名的网站，也是使用 `.NET` 技术开发大型互联网平台的优秀案例。在最近的 `.NET Conf` 上，`Stack Overflow` 的产品经理分享了该网站 `.NET` 迁移带来的性能上提升。

2、[ASP.NET Core 真的有那么快吗？](https://dusted.codes/how-fast-is-really-aspnet-core)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/performance.png)

这张图也许大家并不陌生，常常用来宣传 `.NET` 的性能比其他编程语言的优势，该结果是基于 `TechEmpower` 的性能测试平台。这篇文章详细分析了测试的实现，发现事实上性能优势并没有这样夸张。

3、[C#11 实例展示](https://www.youtube.com/watch?v=cqCBhkNroDI&ab_channel=NickChapsas)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/Csharp11.png)

随着 `.NET 7` 的发布， `C#` 11 也已经发布，这个通过实例详细介绍了 `C#11` 的特性，比如

- 原生字符串
- 列表匹配
- attribute 的泛型
- `nameof` 方法的拓展
- 原生 `utf8` 字符串
- 字符串插入换行
- 泛型数学
- 必选成员
- 文件范围类型
- `span` 的模式匹配
- 自动默认值的结构体
- `method group` 提高
- `IntPtr` 支持
- `ref` 字段和 `scope` 变量

4、[C# 11 中的 UnreachableException](https://www.youtube.com/watch?v=s_NrqRI7Gnc&ab_channel=NickChapsas)

假设有这样的一段代码

```csharp
enum Status
{
    NotStart,
    Doing,
    Done
}

var status = GetStatus();

var text = status switch
{
    Status.Doing => "Doing",
    Status.NotStart => "Not status",
    Status.Done => "Done",
};
```

一般而言 `status` 的 `swtich` 语句将所有的可能的枚举值都列出来，但是编译器一般会让你加一个 `default` 分支，因为 `GetStatus` 方法的实现可能是这样

```csharp
Status GetStatus()
{
    return (Status)4;
}
```

这样就会就会到达 `default` 分支，一般我们会抛出一个 `ArgumentException` 这样的异常。在 `.NET 7` 中引入了一个 `UnreachableException` 异常类型。

```csharp
var text = status switch
{
    Status.Doing => "Doing",
    Status.NotStart => "Not status",
    Status.Done => "Done",
    _ => throw new UnreachableException($"Try to parse {status}")
};
```

5、[多个 Linq Query 的连接](https://www.milanjovanovic.tech/blog/why-i-write-tall-linq-queries)

`Linq` 的语句可以链式执行，那么你一定见过下面这种形式

```csharp
dbContext.Animals.Where(animal => animal.HasBigEars)
    .OrderBy(animal => animal.IsDangerous).Select(
        animal => (animal.Id, animal.Name)).ToList();
```

虽然看起来没啥大的问题，但是有三个缺点

- 可读性不高
- 可解释性不高
- 拓展和维护性不高

那么我们怎么提高这种 `LINQ` 的语句呢

> Linq 应当变高，而不是变宽

所以优化的方向是

```csharp
dbContext
    .Animals
    .Where(animal => animal.HasBigEars)
    .OrderBy(animal => animal.IsDangerous)
    .Select(animal => (animal.Id, animal.Name))
    .ToList();
```

6、[循环迭代的优化方法](https://www.youtube.com/watch?v=cwBrWn4m9y8&ab_channel=NickChapsas)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/span.png)

`Span` 是 `C#` 中的一大优化，通常对于数组和列表，在访问他们的时候可以提高性能，在最近的版本中，`CollectionsMarshal` 和 `MemoryMarshal` 包含了优化的空间

```csharp
List<int> _list = Enumerable.Range(1, 100).Select(i => random.Next()).ToList();
Span<int> asSpan = CollectionsMarshal.AsSpan(_list);
ref var searchRef = ref MemoryMarshal.GetReference(asSpan);
for (int i = 0; i < asSpan.Length; i++)
{
    var item = Unsafe.Add(ref searchRef, i);
    Dosomething(item);
}
```

## 开源项目

1、[Humanizer](https://github.com/Humanizr/Humanizer)

![image](https://dotnetweeklyimages.blob.core.windows.net/027/humanizer.png)

在软件开发过程中，少不了将程序的结果和真正的用户交互。那么简单的 `ToString` 方法有时候并不能达到人类可读性的要求。 而 `Humanizer` 可以帮助我们做到这一点。

```csharp
enum Status
{
    FirstValue,
    [Description("This is second value")]
    SecondValue,
}
var chineseCulture = new CultureInfo("zh-Hans");

// string
"User_not_found".Humanize(), //User not found
"User_not_found".Humanize(LetterCasing.AllCaps), // USER NOT FOUND
"user not found".Humanize(LetterCasing.Sentence), // User not found
"DNA".Humanize(), // DNA
"user_not_found_HTML".Humanize(), // user not found HTML
"User not found".Dehumanize(), // UserNotFound
"User not found".Underscore(),  // user_not_found
"This is user that cannot find in database".Truncate(10, "..."), // This is...
Status.FirstValue.Humanize(), // First value
Status.SecondValue.Humanize(), // This is second value
"child".Pluralize(), // children

// date
DateTime.UtcNow.AddDays(-1).Humanize(), // yesterday
DateTime.UtcNow.AddDays(2).Humanize(), //2 days from now
DateTime.UtcNow.AddMinutes(62).Humanize(), // an hour from now

// timespan
TimeSpan.FromMicroseconds(2341231).Humanize(), // 2 seconds
TimeSpan.FromMicroseconds(123123412).Humanize(2), // 2 minutes, 3 seconds
TimeSpan.FromDays(7).Humanize(), // 1 week
TimeSpan.FromDays(7).Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Day), // 7 days

// number
1.ToWords(), // one
12.ToOrdinalWords(), // twelfth
42.Millions().ToString(), //42000000
69.Gigabytes().ToString(), // 69 GB

// culture
1.ToWords(culture:chineseCulture), // one
DateTime.UtcNow.AddDays(-7).Humanize(culture: chineseCulture), // 7 天前
```
