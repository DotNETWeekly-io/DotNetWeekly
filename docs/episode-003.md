# .NET 每周分享第 3 期

## 开卷语

![xmas](https://dotnetweeklyimages.blob.core.windows.net/003/xmas.webp)

圣诞节快乐！

## 行业资讯

1、[.NET 中文官网来了](https://dotnet.microsoft.com/zh-CN/)

近日，`.NET` 中文官网已经上线。虽然说英语是每个优秀程序员必备的技能，但是作为非母语的开发人员，中文文档对于新人入手是一个不错的选择。

2、[创建 Visual Studio Widget](https://developercommunity.visualstudio.com/t/Visual-Studio-should-have-customizable-w/1586166)

与 Windows 11 中的 Widget 一下，Visual Studio 团队打算为其增加一个类似 Kanban 的功能 `Widget`，通过它可以避免在不同的应用程序之间跳转。

![widget](https://dotnetweeklyimages.blob.core.windows.net/003/vs-widget.png)

3、[.NET Conf 2021 中国大会回顾](https://www.cnblogs.com/shanyou/p/15707498.html)

![conf](https://dotnetweeklyimages.blob.core.windows.net/003/dotnet-conf-china.png)

上周一年一度的中国 `.NET Conf` 圆满落幕，这是中国 `.NET` 开发者的峰会，也是学习，分享和拓展 `.NET` 生态的重要途径。由于疫情原因，今年的大会全部由线上举办。但是大会的质量并没有因此而下降，这篇文章带你回顾一下本次大会。

## 文章推荐

1、[C# 10 特性一览](https://thomaslevesque.com/2021/11/04/a-quick-review-of-csharp-10-new-language-features/)

`C# 10` 已经推出一个多月了，这篇文章快速浏览一下最新版本的特定，主要有

- Record structs
- Static abstract members in interfaces
- Lambda improvements
- Extended property patterns
- File-scoped namespace
- Global usings and implicit usings
- Parameterless constructors and field initializers in structs
- Mix declarations and variables in deconstruction

2、[字符串插入在 .NET 6 中的提升](https://btburnett.com/csharp/2021/12/17/string-interpolation-trickery-and-magic-with-csharp-10-and-net-6)

字符串插值已经在 `C#` 代码中广泛使用，但是之前的实现的还是最终通过 `string.Format(...)` 方法实现，这里可能存在无数次的装箱（Boxing) 和拆箱（Unboxing) 的操作。那么在 `.NET 6` 中有了哪些改进了？
在 `.NET 6` 中引入了 `InterpolateStringHandler` 这个类，它会根据不同的情况采用不同实现的策略。

3、[Linq via C#](https://weblogs.asp.net/dixin/linq-via-csharp)

![linq](https://dotnetweeklyimages.blob.core.windows.net/003/linq.jpeg)

`Linq` 是 `C#` 开发者中的*利器*， 但是很多时候，我们知道如何使用 `API`, 但是关于 `Linq` 背后的实现又掌握多少呢？这一些列文章可以帮你了解 `Linq` 的种种，主要分为三块

- `.NET` 和 `C#`
- `Linq` 可以操纵的数据类型
- `Linq` 背后的理论支持

## 开源项目

1、[依赖注入框架 Autofac](https://autofac.org/)

![autofac](https://dotnetweeklyimages.blob.core.windows.net/003/autofac.jpeg)

`Microsoft.Extension.DependencyInjection` 是在 `ASP.NET Core` 中广泛使用的依赖注入框架，在 `.NET` 生态中还有一个著名的框架叫 `autofac`。作为一个老牌的依赖注入框架，它提供了很多 `M.E.DependencyInjection` 不具备的功能

- 不同类型的生命周期
- MetaData 服务
- 惰性初始化
- ...

如果你向在你的 `ASP.NET Core` 应用程序中使用 `autofac`，可以参考[这篇文章](https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html)。

2、[Entity Framework Plus](https://entityframework-plus.net/)

![efplus](https://dotnetweeklyimages.blob.core.windows.net/003/ef-plus.jpeg)

`Entity Framework` 是一个优秀的 `ORM` 框架，但是由于设计取舍的问题，一些必要（Must Have）的特性的缺失在一些情况下会导致性能不够理想。因此 `Entity Framework Plus` 插件可以解决这个问题，它主要有一下的功能

- 批量操作
- Linq 支持
- 查询增强功能
