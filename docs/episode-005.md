# .NET 每周分享第 5 期

## 开卷语

中文互联网中关于 `.NET` 存在着大量的偏见，比如

- 不能跨平台
- 收费
- 性能差
- 生态差
- ...

关于这些争论，一般不会去争辩什么，因为：

- 这些偏见并不会因为在网络争辩而纠正
- 世界上并没有一种完美的编程语言，只有被人骂的和没人用的两种。

## 行业资讯

1、[EF 7 Plan](https://devblogs.microsoft.com/dotnet/announcing-the-plan-for-ef7/)

`Entity Framework` 是 `.NET` 中广泛使用的 ORM 框架，`EF Core` 是基于 `.NET Core` 而且在 `EF Core 7` 中将会移除 `Core`, 升级为 `EF 7`, 现在 `EF` 团队发布 `EF 7` 的 Feature Request 计划，主要包含:

- JSON 数据存储
- 批量更新
- 生命周期勾子
- ...

除此之外，还有周边生态建设工作

- 分布式事务
- `dotnet ef` 工具集成

2、[.NET MAUI Preview 11 发布](https://devblogs.microsoft.com/dotnet/announcing-dotnet-maui-preview-11/)

![](https://docs.microsoft.com/en-us/dotnet/maui/media/what-is-maui/maui.png)

虽然 `MAUI` 并没有随着 `.NET 6` 一同发布，但是微软并没有停止对 `MAUI` 的开发工作，最近 `Preview 11` 随着 `Visual Studio 2022 17.1 Preview 2` 一同发布，此次更新包含的内容有

- Fluent Design System
- Multi-Windows
- Template and C# 10
- Documentation

## 文章推荐

1、[ASP.NET Core 和 Spring Boot 比较](https://medium.com/@putuprema/spring-boot-vs-asp-net-core-a-showdown-1d38b89c6c2d)

![](https://miro.medium.com/max/1400/1*3HDwsy-GXeiJTQVAoHj49w.png)

`Java` 和 `C#` 都是成熟的工业级软件开发语言，在 `Web` 后端开发方面，`Java` 有 `Spring` 框架，而 `C#` 有 `ASP.NET Core`, 这篇文章带你分析这两个框架在后端开发的区别，主要有

- 控制器 （Controller）
- 数据绑定和验证 （Model Binding & Validation）
- 异常处理 （Exception Handling）
- 数据访问 （Repository & ORM）
- 依赖注入 （Dependency Injection）
- 权限管理（Authentication & Authorization）
- 性能 （Performance）

2、[旧闻回顾：Visual Studio 会毁掉你的心智吗？](http://charlespetzold.com/etc/DoesVisualStudioRotTheMind.html)

![](https://agetintopc.com/wp-content/uploads/2021/01/Visual-Studio-Software-Download-Setup-Free.png)

这是一篇十几年前的演讲稿，作者是开发 `Window Form` 应用程序，在 `Visual Studio 2005` 发布之后，对其功能做了一些*吐槽*。

- 智能感知 （IntelliSense） : 作者认为 `IntelliSense` 并不会让我们变成一个好的程序员，而是一个编码快速的程序员。
- 代码生成（Generated Code）: 在使用 `Visual Studio` 创建一个 `Windows Form` 应用程序的时候，会自动生成若干代码，这些代码是 `Visual Studio` 不想让你知道的。
- 设计页面 （Designers）： `Visual Studio` 引入了交互式的设计页面，这是一个好的技术进步。
- 资源脚本： `Visual Studio` 隐藏了资源的，不是通过显式代码形式。而且每个 Control 的默认行为非常丑陋。
- ...

作者认为他更倾向于用纯文本的方式开发应用程序。

站在 2022 年的今天，你该如何看待他的观点呢？

## 开源项目

1、[2021 .NET 最活跃的 Microsoft 的开源项目](https://pbs.twimg.com/media/FICYOcDWUAIFkXw?format=png&name=900x900)

![](https://pbs.twimg.com/media/FICYOcDWUAIFkXw?format=png&name=900x900)

这里是 2021 年最活跃的由 Microsoft 维护或者支持 `.NET` 开源项目列表，主要 `Pull Request`， `Commit` 和 `New Contributor` 三个指标统计。

2、[Benchmarkdotnet 库](https://benchmarkdotnet.org/articles/overview.html)

![](https://repository-images.githubusercontent.com/12191244/e327c900-f194-11e9-8d50-db9acd1690af)

在进行代码性能分析的时候，我们需要准确地知道其运行时间和内存消耗数据。同时针对不同的环境和数据量的情况，也需要进行相关分析。

> Code is cheaper, show me statistic.

`BenchmarkDotnet` 库就是 `.NET` 世界的性能测量工具。它的功能有

- 支持统计学模型
- 不同运行环境统计，比如 CLR， Mono，CoreCLR 等等
- 丰富的统计图标输出
- 基准测试
- 交叉参数组合
- 内存诊断
- ...
