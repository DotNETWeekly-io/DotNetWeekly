# .NET 每周分享第 2 期

## 开卷语

![](https://dotnetweeklyimages.blob.core.windows.net/002/anders.jpeg)

最近 `C#` 之父 _Anders Hejlsberg_ 接受了一个专访，回顾了一下在 `C#` 开发过程中历程和对最新版本的思考。有趣的是，采访的主持人是 _Anders_ 的亲弟弟。

[Youtube](https://youtu.be/K3qf8gRFESU)

采访中包含了这些问题：

- 为什么选择编程语言作为职业生涯的主要工作内容？
- 关于开源的看法，不单单代码开源，还要开发开源。
- 虽然现在专注于 `TypeScript`，那么现在在 `C#` 团队中担任的角色。
- 对于目前 `C# 10` 有什么看法。
- 由于疫情的影响，团队中敏捷性开发。
- `TypeScript` 对工业界带来的影响。

## 行业资讯

1、[.NET Ketchup](https://dotnetketchup.com/)

怎样实时的抓住 `.NET` 的最新动态呢？访问 [.NET Ketchup](https://dotnetketchup.com/)，它会定时的抓取最新的 `.NET` 信息，比如官方博客，Youtube，Twitter 等等。而且它和周刊一样，以按照周的形式组织信息源。

2、[一张图了解 .NET 发展历程](https://www.reddit.com/r/dotnet/comments/rcg391/net_history_timeline_its_not_finished_yet_so_if/)

Reddit 有人发布了一张 `.NET` 的发展历程

![](https://dotnetweeklyimages.blob.core.windows.net/002/dotnetroadmap.png)

- 上面是操作系统，Visual Studio， C# 语言
- 下面是 CLR 的版本

## 文章推荐

1、[使用正确的方式日志记录异常信息](https://blog.stephencleary.com/2020/06/a-new-pattern-for-exception-logging.html)

如果你用日志来记录异常，通常会写这样的代码

```Csharp
try
{
  ...
}
catch (Exception e)
{
  _logger.LogError(e, "Unexcepted error");
  throw;
}
```

对于早期*刀耕火种*的时期日志，这样做是没有问题的，但是现代日志有更加丰富的功能，称之为语义化日志或者结构化日志。在遇到异常的时候，`runtime` 会在栈向上查找，在匹配后展开（unwind），这样的问题就会将日志的上下文丢弃了。而 `.NET` 支持异常过滤器，如果在异常过滤器中记录日志，就能保留下日志的上下文。

```Csharp
try
{
    ...
}
catch (Exception e) when (False(() => _logger.LogError(e, "Unexpected error.")))
{
    throw;
}

public static bool False(Action action)
{
    action();
    return false;
}
```

2、[Jeffery Snover 专访](https://evrone.com/jeffrey-snover-interview)

[Jeffery Snover](https://en.wikipedia.org/wiki/Jeffrey_Snover) 是微软的 `Technical Fellow` 也是 `PowerShell` 的发明人，这是一篇最近对他的专访，介绍了 `PowerShell` 发明背后的故事，还分享一些有趣的观点

- PowerShell 诞生的三个节点

  1.  在处理 `XML` 的时候，需要一种管道的处理方式
  2.  尝试用 `.NET` 实现通用的 Shell
  3.  编写了 Monad Manifesto

- Linux 是一种基于文件的操作系统，而 Windows 是基于 API。这个也就导致了 `Bash` 基于文本这种非结构化数据，而 `PowerShell` 更专注于结构化数据。
- 对于 `PowerShell` 的推广，我们更加关注于别人是否用了我们的工具取得了更大的成功，而不是他们是否在使用我们的工具。

3、[使用 HttpClient 地正确方法](https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/)

`HttpClient` 是 C# 广泛使用的类，但是大部分人都错误的使用了它，比如：

```Csharp
using(var httpClient = new HttpClient())
{
}
```

虽然 `HttpClient` 实现了 `IDisposable` 接口，但是本质上它是一个共享的对象，调用 `Dispose` 方法并没有关闭对应的 `TCP` 对象，如果频繁地调用会消耗机器地所有 `Socket` 接口。 那么该如何修复这个问题呢？

- 使用单个 `HttpClient` 对象
- 使用 `Microsoft.Extension.HttpFactory` 库

## 开源项目

1、 [构建 Resilient 的引用程序 - Polly](https://github.com/App-vNext/Polly)

![](https://dotnetweeklyimages.blob.core.windows.net/002/polly.jpeg)

我们都知道一个最基本的事实

> 网络不可靠

那么为了构建一个 **Resillient** 的代码，需要我们着重考虑这些情况，`Polly` 这个库就够帮助我们简单地完成这份工作，主要包含以下这些策略（Policy）

- 重试 （Retry）
- 熔断器 （Circuit Breaker）
- 回退 （Fallback）
- 超时 （Timeout）
- 隔断 （Bulk）
- 缓存 （Cache）
- 组合 （Combination）

2、[检测你的代码是否正确使用异步](https://www.poppastring.com/blog/fixing-sync-over-async-issues-in-net)

`async` 和 `await` 已经在 `.NET` 生态中存在了好多年了，但是大家在写代码的时候，还是会使用 `Result`, `Wait` 和 `GetAwaiter()` 等方法让异步代码编程同步执行。微软推出了 `Microsoft.VisualStudio.Threading.Analyzers` 这个扩展包，它可以检测代码中是否出现异步代码但是同步执行的情况。
