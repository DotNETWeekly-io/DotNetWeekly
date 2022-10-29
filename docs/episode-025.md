# .NET 每周分享第 25 期

## 卷首语

下个月就是每年的 `.NET` 发布新版本的时候，大家都有什么期待呢？

## 行业资讯

1、[Imagesharp 离开 .NET Foundation](https://dotnetfoundation.org/blog/2022/10/20/imagesharpupdate)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/imagesharp.png)

最近 `.NET Foundation` 宣布了著名的图像处理库 `Imagesharp` 离开了 `.NET Foundation` 组织，因为他们修改了开源软件的协议，变成了一个付费使用的库。

2、[Microsoft Ignite 2022 中 ASP.NET Core 7 介绍](https://learn.microsoft.com/en-us/events/ignite-2022/brk203h-hidden-gems-live-coding-with-net-7)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/aspnetcore7.png)

前一阵子 Microsoft Ignite 大会上，`Damian Edwards` 和 `David Fowler` 展示了 `ASP.NET Core 7` 的一些新特性，没有幻灯片，只有实际的例子，包含了下面的主题

- RateLimiter
- Logging Middleware
- Output Caching
- Problem Detail
- Hook setup
- Native AOT

## 文章推荐

1、[如何扩展 ASP.NET Core 应用程序](https://speakerdeck.com/davidfowl/scaling-asp-dot-net-core-applications)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/davidfowler.png)

David Fowler 的一个演讲的 Slide，从内存，CPU，I/O 的角度如何拓展的 `ASP.NET Core` 的应用程序。

2、[Task.WaitAll 异常处理方法](https://thesharperdev.com/csharps-whenall-and-exception-handling/)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/waitall.png)

C# 中的 `WaitAll` 可以接受一些列异步的 `Task` 并且等所有的 `Task` 都完成之后才会自己返回。如果其中一个任务抛出异常，那么 `WaitAll` 只会抛出第一个异常。所以该怎么解决这个问题，这篇文章给出一个可行的方案

3、[自定义 HTTP 的方法](https://khalidabuhakmeh.com/adding-experimental-http-methods-to-aspnet-core)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/httpquery.png)

标准的 `HTTP` 请求方法有 `GET`, `POST`, `PUT` 等等。那么如果想要自定义一个方法，比如 `QUERY`，改如何实现呢？ `ASP.NET Core` 非常有扩展性，这篇文章介绍了怎么完成这件事。

4、[C# 最差实践](https://code-maze.com/csharp-programming-mistakes/)

C# 代码中有很多规范和陷阱，这篇文章介绍了其中的一部分。

6、[OData 在 ASP.NET Core 中使用](https://code-maze.com/aspnetcore-webapi-using-odata/)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/odata.png)

OData 是微软提出的基于 `Rest` 的客户-服务端请求，通过客户端的请求的参数的不同，服务端就可以返回不同的参数，而且服务端不需要编写相应的逻辑，而且请求的参数和普通的 Rest 请求不一样。

```bash
https://localhsot/odata/companies?$filter=Size gt 20
```

该请求是从 `Company` 实体中返回属性 `Size` 大于 20 是实体，而 `Controller` 中定义如下

```Csharp
[EnableQuery]
[HttpGet("{id}")]
public SingleResult<Company> Get([FromODataUri] int key)
{
    return SingleResult.Create(_repo.GetById(key));
}
```

7、[高难度学习 .NET](https://www.slideshare.net/petabridge/net-systems-programming-learned-the-hard-waypptx)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/dotnetcsharp.png)
Aaron Stannard 是 `Akka.NET` 的作者，这里有一份演讲的 Slide，介绍了如何是用 `C#` 的高级特性来提高 `Akka.NET` 的性能。

## 开源项目

1、[ASP.NET Core 的 Clean Architecture 模板](https://github.com/jasontaylordev/CleanArchitecture)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/cleanarchiture.png)

在创建 `ASP.NET Core` 应用程序的时候， `Visual Studio` 模板已经做的足够好了。这个开源项目遵顼 `Clean Architecture` 的原则，提供了模板，包含了大量现代化软件开发的框架和类库。

2、[Cake](https://github.com/cake-build/cake)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/cake.png)

虽然 `dotnet` 命令行工具已经非常工具已经非常强大了，但是我们仍然想要拥有一个类似 `Cmake` 一样的编译，测试工具。`Cake` 就是这样一个开源项目，让 `C#` 项目也具有强大的功能。

3、[十个免费的 VS 主题推荐](https://blog.dotnetsafer.com/best-visual-studio-2022-themes/)

![image](https://dotnetweeklyimages.blob.core.windows.net/025/vstheme.png)

在颜值即正义的时代，挑选一款漂亮的 `Visual Studio` 主题也是一件重要的事情，这篇文章介绍了作者认为比较好看的主题，你认为呢？
