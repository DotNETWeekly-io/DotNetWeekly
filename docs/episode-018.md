# .NET 每周分享第 18 期

## 卷首语

微软公司选择在每年的 11 月份发布新的 `.NET` 版本，主要分为两种类型

![net](https://dotnetweeklyimages.blob.core.windows.net/018/DotnetEdition.png)

- Long Term Support (LTS): 长期支持版本，通常为偶数版，比如 `.NET 6`
- Current: 是当前版本，通常为奇数版，比如 `.NET 5`.

除此之外，还有一个版本叫做 `Preview`, 它是预览版，为了提前体验新的功能通常社区喜欢尝鲜的用户会安装该版本。

但是很多人有一种这样的固有观点：

> 只使用 LTS 的版本

其实是不对的，除了 `Preview` 版本，`Current` 和 `LTS` 都有一定的支持时间，分别为 3 年和 18 个月。在这期间，这些版本会得到完全的支持，比如功能提升，安全漏洞升级等等。在后续的 6 月期间，只有维护支持，通常只有安全漏洞升级。按照微软的版本支持政策，只要定期升级版本，就能得到完整的支持。而且使用新的版本，比如从 `.NET Core 3.1` 迁移到 `.NET 5`, 会得到很大的性能上的提升，而且几乎没有任何 break change.

## 行业资讯

1、[MAUI 和 DevOps 结合](https://devblogs.microsoft.com/dotnet/devops-for-dotnet-maui/)

![Maui and DevOps](https://dotnetweeklyimages.blob.core.windows.net/018/MauiAndDevOps.png)

MAUI 已经推出很久了，那么如何使用 CI/CD 来构建和部署 MAUI 的应用程序呢？这篇文章提供了参考。

2、[VS Code Server 来了](https://dotnetweeklyimages.blob.core.windows.net/018/VSCodeServer.png)

![VS Code Server](https://dotnetweeklyimages.blob.core.windows.net/018/VSCodeServer.png)

自从微软发布了 `VS Code` 之后，几乎占据了大部分编辑器的市场份额。之前 `VS code` 专注于桌面市场，随着新冠疫情依旧肆虐，居家办公越来越常见，远程开发变得越发流行。之前 `VS Code` 借助 `Remove Development` 插件，实现了远程办公。但是 `VS code` 团队想要更近一步，希望在浏览器上完成开发工作，这样手机，平板上也能进行开发。

最近 `VS Code Server` 进入预发布状态，它可以在一台远程服务器上安装服务，然后通过 [vscode.dev](http://vscode.dev) 进行访问。

3、[一键部署.NET 到 AWS](https://aws.amazon.com/blogs/developer/aws-announces-a-streamlined-deployment-experience-for-net-applications)

![Dotnet publish to AWS](https://dotnetweeklyimages.blob.core.windows.net/018/PublishAWS.png)

我们都知道 `Visual Studio` 和 `Azure` 结合的非常好，比如 C# 项目 `Publish` 按纽可以一键部署到 `Azure` 服务上，这也是微软的商业策略。但是不可否认的是 `AWS` 是最大的云服务提供商，最近 Amazon 推出了 `VS` 插件，可以一键将 C#代码部署到 `AWS` 中。

## 文章推荐

1、[C# 实现神经网络](https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/)

![Neural Network](https://dotnetweeklyimages.blob.core.windows.net/018/NeuralNetwork.png)

神经网络是目前人工智能的支持体系，虽然说现在流行的框架比如 PyTourch, TensorFlow 使我们不需要关心神经网络的具体实现，但是从头到尾实现一个神经网络可以是我们的基础更加牢固。这篇文章介绍了如何用 C# 实现一个神经网络，并且复习神经网络的基本知识。

2、[C#中的切片和范围](https://code-maze.com/csharp-ranges-and-indices/)

![Ranges and Indices](https://dotnetweeklyimages.blob.core.windows.net/018/RangesAndIndices.png)

C# 8 引入了索引 （index) 和范围 （range），可以支持类似 python 的索引和范围查询。

- 索引

```csharp
public static string GetFirst(string[] names)
{
   var index = new Index(0);
   return names[index];
}

public static string GetLastMethod1(string[] names)
{
   var index = new Index(1, true);
   return names[index];
}
```

这里引入了一个结构 `Index`，它接受一个参数表示索引值，第二个参数表示是否从尾部开始；而对于从尾部开始，可以使用语法糖 `^`。

```csharp
public static string GetLastMethod2(string[] names)
{
    return names[^1];
}

```

- 范围

  提供了 `Range` 的结构体:

```csharp
    public Range(Index start, Index end);
```

注意 `start` 表示包含，而 `end` 则是不包含。

```csharp
public static string[] GetFirstTwoElements(string[] arr)
{
    var start = new Index(0);
    var end = new Index(2);
    var range = new Range(start, end);
    return arr[range];
}
```

和 `Index` 一样，Range 也支持语法糖方式

```csharp
public static string[] GetAll(string[] arr)
{
   return arr[..];
}

public static string[] GetFirstThreeElements(string[] arr)
{
   return arr[..3];
}

public static string[] GetLastThreeElements(string[] arr)
{
   return arr[^3..];
}

public static string[] GetThreeElementsFromMiddle(string[] arr)
{
   return arr[3..6];
}
```

3、[FirstOrDefault 和 SingleOrDefault 的区别](https://www.youtube.com/watch?v=ZTWl2s8ScMc&ab_channel=NickChapsas)

![FirstOrDefault and SingleOrDefault](https://dotnetweeklyimages.blob.core.windows.net/018/FirstOrSingle.png)

- 如果集合没有元素: `SingleOrDefault` 和 `FirstOrDefault` 都返回类型的默认值
- 如何集合只有一个元素： `SingleOrDefault` 和 `FirstOrDefault` 都返回该元素
- 如何集合包含多个元素： `SingleOrDefault` 抛出异常，而 `FirstOrDefault` 返回第一个元素

4、[HttpClient 和 RestSharp 比较](https://code-maze.com/httpclient-vs-restsharp)

`HttpClient` 是 C# 内置库中的网络请求客户端，而且它也支持各种 HTTP 请求方式；[RestShap](https://github.com/restsharp/RestSharp) 是一款开源的 Http 客户端，而且支持同步和异步的操作。那么他们之间的差异如何呢？首先比较一下性能的差异：

- GET

  | Method                |     Mean |   Error |  StdDev | Allocated |
  | --------------------- | -------: | ------: | ------: | --------: |
  | CreateTodo_HttpClient | 251.9 ms | 4.88 ms | 5.42 ms |     43 KB |
  | CreateTodo_RestSharp  | 253.1 ms | 4.99 ms | 6.32 ms |    109 KB |

- POST

  | Method                |     Mean |   Error |  StdDev | Allocated |
  | --------------------- | -------: | ------: | ------: | --------: |
  | CreateTodo_HttpClient | 251.9 ms | 4.88 ms | 5.42 ms |     43 KB |
  | CreateTodo_RestSharp  | 253.1 ms | 4.99 ms | 6.32 ms |    109 KB |

- PUT

  | Method                |     Mean |   Error |   StdDev | Allocated |
  | --------------------- | -------: | ------: | -------: | --------: |
  | UpdateTodo_HttpClient | 254.0 ms | 3.93 ms |  4.82 ms |     10 KB |
  | UpdateTodo_RestSharp  | 250.1 ms | 4.95 ms | 10.65 ms |     93 KB |

- DELETE

  | Method                |     Mean |   Error |  StdDev | Allocated |
  | --------------------- | -------: | ------: | ------: | --------: |
  | UpdateTodo_HttpClient | 254.1 ms | 4.78 ms | 8.24 ms |     10 KB |
  | UpdateTodo_RestSharp  | 258.8 ms | 4.87 ms | 8.65 ms |     85 KB |

总结看来， `HttpClient` 和 `RestSharp` 几乎没有任何性能上的差距，再内存分配上，`HttpClient` 拥有更好的表现。

在实现机制上来说，`HttpClient` 对每个请求使用连接池中的连接进行处理，而且只支持异步方法。虽然 `HttpClient` 实现了 `IDispose` 接口，但是调用 `Dispose` 方法就会关闭底层的连接。这样如果多次调用 `HttpClient` 的方法，这样需要重复打开连接。一般而言使用 `HttpClientFactory` 可以解决这个问题。

而 `RestSharp` 是对 `HttpClient` 的封装，但是它支持同步和异步的方法，而且对于 `JSON`, `XML` 格式支持很好。在最新的几个版本中，解决了原生 `HttpClient` 的一些已知的问题。

## 开源项目

1、[F#入门文档](https://leanpub.com/essential-fsharp)

![Essential F#](https://dotnetweeklyimages.blob.core.windows.net/018/EssentialFsharp.png)

F# 入门文档。

2、[Awesome Blazor](https://github.com/AdrienTorris/awesome-blazor)

![Awesome Blazor](https://dotnetweeklyimages.blob.core.windows.net/018/AwesomeBlazor.png)

该项目中汇总了许多与 Blazor 相关的内容，包含但不限于 Blazor 介绍、应用模板、不同种类的示例项目、教程、库和扩展、效率工具、文章、书籍推荐、课程等等。如果想要学习如何编写 Blazor 应用可以在该项目中查找到很多非常有用的推荐内容，同时该项目提供了[网站](https://jsakamoto.github.io/awesome-blazor-browser)可以对项目中的条目进行搜索。
