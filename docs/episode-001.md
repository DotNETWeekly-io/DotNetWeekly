# .NET 每周分享第 1 期

## 开卷语

![](https://dotnetweeklyimages.blob.core.windows.net/001/world-of-dotnet.svg)

各种各样的原因，`.NET` 在国内的流行程度仍然赶不上 `Java`, `C/C++` 这样的编程语言。作为 `.NET` 生态的参与者，常常想能不能做一些事情来改变这个情况。由于语言的原因，很多外网 `.NET` 社区的内容并没有在国内传播过来。因此 `DotNET Weekly` 将会专注于分享全球 `.NET` 社区内容，通常会包含下面三种的类型的资源：

- 行业资讯
- 文章推荐
- 开源项目

通常周刊会在周末发布。

> Stay Turned

## 行业资讯

1、[Open .NET 来了?](https://www.infoq.cn/article/ut0oDCTQmT7Sdu5Ega2k)

上个月一群人 Fork 了 .NET 平台下的开源项目，并且成立 Open .NET 组织，导火索是微软在 .NET 6 发布的时候，将 CLI 工具中删除了 Hot Reload 相关的代码，使它变成了 Visual Studio 2022 独占功能。这个引发了微软在开发者社区的信任危机，他们希望借助 Open .NET 来摆脱微软对 .NET 的掌控。

2、[.NET Conf 回顾](https://devblogs.microsoft.com/dotnet/net-conf-2021-recap-videos-slides-demos-and-more/)

上个月微软举办了一年一度的 `.NET Conf` , 在会议中发布了 `.NET 6` 和 `Visual Studio`, 这篇文章可以帮你回顾一下这次会议和其中的亮点。

- Roslyn 和 AI 的整合。
- C# 10 的介绍。
- 使用 `.NET 6` 开发出一个跨平台（Web，Desktop，Mac, iOS, Android）的播客应用示例。
- Q & A Session。

还有一个 `.NET Conf` 2021 的 [Youtube Playlist](https://www.youtube.com/playlist?list=PLdo4fOcmZ0oVFtp9MDEBNbA2sSqYvXSXO)。

3、[Microsoft 技术社区](https://techcommunity.microsoft.com/)

[StackOverflow](https://stackoverflow.com/) 是业界知名的的技术社区，上面有无数个问题得到了解答。Microsoft 也推出了自己的技术社区，专注于微软推出的技术，包含但不局限于 `C#`, `ASP.NET Core`, `Visual Studio`， `Windows` 等等。

## 文章推荐

1、[如何给 .NET 社区做贡献](https://rion.io/2017/04/28/contributing-to-net-for-dummies/)

作者分享了如何给 .NET 社区做共享，那怕你仍然还是一个新手。作者提出了一般人会提出的四个问题，并且一一回答它们

- 我需要一个指导人来帮助我因为这些 issue 已经超出了我的理解范围。
- 所有简单的 issue 已经被别人处理了。
- 由于这个是 issue 已经被提出好久，如果我去做的话，我好像被暴露成一个垃圾开发者。
- 我从来没有在这个规模的项目开发过，我可能干不好。

2、[如何 Review .NET 代码？ 来听听别人的观点](https://levelup.gitconnected.com/my-tips-for-net-code-review-f1a47feece43)

在 DevOps 开发流程中，我们常常会 Review 别人的代码。那么该如何 Review 呢？这位 Microsoft MVP 给出了若个建议，这些不单单是给 Reviewer，而且对开发人员还有很大的帮助。

3、[学习 .NET GC](https://tooslowexception.com/net-gc-internals-mini-series/)

`Pro .NET Memory Management` 图书的作者在 YouTube 上连载 .NET GC 的分享，内容十分翔实。无论是否从事 .NET 相关开发与否，都能从中学到 GC 相关的知识。

## 开源项目

1、 [单元测试框架](https://github.com/moq/moq4)

在软件开发中，单元测试是必不可少的部分。但是代码中存在一些外部的依赖，因此需要对它们进行 **Mock**。在 `.NET` 平台，最有名的框架就是 `Moq`，借助它可以帮助我们有效编写单元测试，并且辅助我们遵循面向接口编程原则。

2、[FluentAssertions](https://fluentassertions.com/)

不管在什么单元测试框架中，单元测试的形式一般如下

```Csharp
var result = GetResult();
Assert.AreEqual("Hello World", result);
```

这样的测试表明，我们期望得到的值是 `Hello World`。但是这样要求我们记住 `actual` 和 `expect` 的参数位置，对于其他类型的比较，比如集合，需要写很多琐碎的代码来判断结果是否满足预期。

而 `FluentAssertions` 就非常直接

```Csharp
var result = GetResult();
result.Should().Be("Hello World");
```

在这里我们通过 `Should` 的拓展方法，然后进行判断。主要有两个好处：

- 避免 `Assert` 的引入，使判断的流程线性化
- 通过 `Should()` 返回的类型的方法，使不同类型可以定制化比较方法，比如对于 `Guid`, `Collection` 或者 `Date` 都有相应额外的操作。
