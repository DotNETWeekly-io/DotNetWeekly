# .NET 每周分享第 23 期

## 卷首语

[MAUI Blazor App 教程](https://www.c-sharpcorner.com/article/build-a-blazor-hybrid-app-with-net-maui-for-cross-platform-application)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/muai.png)

在本文中介绍了下列内容：

- 什么是 Hybrid Blazor App
- 什么是.NET MAUI
- .NET MAUI Blazor app
- 开始使用 .NET MAUI Blazor App
- .NET MAUI Blazor App 的先决条件
- 使用 Visual Studio 创建 .NET MAUI Blazor 应用
- 在 Windows 中运行和测试 .NET MAUI Blazor 应用
- 在 Android 模拟器中运行并测试 .NET MAUI Blazor 应用
- 为什么要使用 .NET MAUI Blazor 应用
  - 使用 Blazor，我们可以同时使用 .NET 和 C# 来构建前端 Web UI 和后端逻辑
  - 我们可以对前端 Web UI 和后端逻辑使用相同的 C# 技能集，这将加速开发并降低成本。
  - 我们不需要对使用哪些前端框架而感到疑惑。
  - 我们可以开发 .NET MAUI for Windows、安卓、iOS、macOS、Web 的跨平台应用程序。
  - 适用于跨平台应用的相同 C# 和 .NET
  - 共享代码和 UI 布局
  - 共享设计和一致的 UI
  - 共享相同的逻辑，测试
  - 适用于所有平台的原生应用
  - 可以使用 Windows 或 Mac 进行开发，并且可以在每个设备上运行
  - 我们可以使用单个项目，这将易于维护和工作
  - 我们可以使用所有 .NET 库和 C# 特性
  - 最新技术和强大的社区支持

## 行业资讯

1、[ASP.NET Core RC1 发布](https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-7-rc-1)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/aspdotnetcore7.png)

随着 `.NET 7` RC1 发布， `ASP.NET Core 7` 的 RC 也发布了，其中包含了若干个更新，现在可以体验起来。

2、[.NET 7 RC1 发布](https://devblogs.microsoft.com/dotnet/announcing-dotnet-7-rc-1)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/dotnet7rc.png)

毫无疑问，一年一度的 `.NET` 发布日期即将到来。最近 `.NET 7 RC1` 已经发布了，包含以下的重点

- MAUI
- Cloud Native 支持
- ARM 64 性能提升
- Modernization 无痛升级
- Performance 提升

3、[.NET 7 在 arm 上性能提升](https://devblogs.microsoft.com/dotnet/arm64-performance-improvements-in-dotnet-7)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/dotnet7arm64.png)

`.NET` 从 Day 1 就是完全开放的，不仅仅是 `Windows`, `Linux` 或者 `MacOS`，而且能够运行在不同的 `CPU` 指令集中。最近即将发布的 `.NET 7` 在 `ARM64` 上有着很大的性能提升。

4、[Winget 管理 .NET SDK](https://devblogs.microsoft.com/dotnet/dotnet-now-on-windows-package-manager)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/winget.png)

`Winget` 是微软官方发布的 `Windows` 包管理器，通过它可以通过命令行安装一些软件和包。现在通过它可以安装 `.NET` SDK。

- winget search Microsoft.DotNet
- winget Install Microsoft.DotNet.SDK.6
- winget uninstall Microsoft.DotNet.SDK.6

`Winget`主要命令如下：
| 命令 | 用途 |
| --------- | -------------------------- |
| install | 安装给定的程序包 |
| show | 显示包的相关信息 |
| source | 管理程序包的来源 |
| search | 查找并显示程序包的基本信息 |
| list | 显示已安装的程序包 |
| upgrade | 显示并执行可用升级 |
| uninstall | 卸载给定的程序包 |
| hash | 哈希安装程序的帮助程序 |
| validate | 验证清单文件 |
| settings | 打开设置或设置管理员设置 |
| features | 显示实验性功能的状态 |
| export | 导出已安装程序包的列表 |
| import | 安装文件中的所有程序包 |

## 文章推荐

1、[MadsTorgersen 采访](https://dotnetcore.show/episode-104-c-sharp-with-mads-torgersen)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/madstorgersen.png)

Mads 目前是 `C#` 语言的首席架构师，这篇播客是对他的采访，主要分为下面几个方面

- 背景介绍，和 `Anders` 那里学到的团队的内容
- C# 中的 `async/await` 对其他语言的影响
- C# 的语言和 `.NET` 版本的分离对开发的体验
- 如何对 `C#` 社区做出贡献

2、[Seal class 有助于提高性能](https://www.youtube.com/watch?v=d76WWAD99Yo&ab_channel=NickChapsas)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/sealclass.png)

`Sealed` 修饰符 `C#` 语法的一部分，它表示这个类不能被继承，不为人知道的是它在性能上有很好的提升。
如果 `sealed` 修饰的类，那么得到下面的好处

- `override` 方法可以直接调用而不是查看虚方法表，也就是这些方法可以内联
- `is` 和 `as` 这样的类型转换可以高效实现，因为不需要比较类型本身而不是潜在的继承关系
- 数组不需要进行考虑协变的问题
- 创建 `Span` 类型的时候不要验证其真正的类型

3、[C# 11 的新特性以及它能够解决的问题](https://rubikscode.net/2022/09/19/c-11-top-5-features-in-the-new-c-version)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/csharp11.png)

`C#11` 即将发布，这里详细分析了 5 个重要的功能

1. 原始字符串
   在 `C#11` 中可以通过至少 3 个 `"` 表示一个原生字符串，这样避免了繁琐的转义操作

2. Attribute 泛型

过去 `Attribute` 是不支持泛型，类型需要作为一个属性参数表示，`C# 11` 解除了这个限制。

3. 数学方法的通用支持

定义接口的抽象方法，可以使数学计算更加方便。

4. 列表匹配

增加的列表匹配的方法

5. Struct 属性

对于 `init` 的 `Struct` 属性，可以设定默认值。

4、[使用 PlayWright 测试 ASP.NET Core 应用程序](https://www.twilio.com/blog/test-web-apps-with-playwright-and-csharp-dotnet)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/playwright.png)

在本篇文章中首先介绍了`Playwright`该库的一些特性，例如：

- 支持多种测试类型（单元测试、集成测试、端到端测试）
- 无需手动安装浏览器和该浏览器兼容的驱动程序
- 支持多种浏览器：Chromium、Edge、Safari、Firefox
- 通过.NET(C#)，TypeScript，JavaScript，Python 和 Java 提供跨平台和跨语言支持
- 支持通过[Codegen](https://playwright.dev/dotnet/docs/codegen-intro)录制脚本。

然后在本文其他大部分内容中通过`NUnit`和`Playwright`，一步一步讲述了如何对一个`ASP.NET Core` Web 应用进行了单元测试。

6、[ASP.NET Core 中如何将 Delegate 转换成 RequestDelegate](https://stackoverflow.com/questions/73426685/how-is-delegate-being-cast-to-requestdelegate-in-asp-net-core/73427800#73427800)

StackOverflow 中某位用户提出了一个问题：在下列代码中`handler`参数是如何从`Delegate`类型转换为`RequestDelegate`类型的。另一位用户给出了回答具体详情请查阅文章。

```Csharp
    public static RouteHandlerBuilder MapGet(
            this IEndpointRouteBuilder endpoints,
            string pattern,
            Delegate handler)
        {
            return MapMethods(endpoints, pattern, GetVerb, handler);
        }
    public static IEndpointConventionBuilder MapMethods(
           this IEndpointRouteBuilder endpoints,
           string pattern,
           IEnumerable<string> httpMethods,
           RequestDelegate requestDelegate)
        {
            // 略
        }
```

## 开源项目

1、[Apache Thrift](https://github.com/apache/thrift)

![image](https://dotnetweeklyimages.blob.core.windows.net/023/thrift.png)

图片为 Thrift 架构设计

Thrift 是一个轻量级的、与语言无关的技术栈，可以用于点对点 RPC 实现。Thrift 为数据传输、数据序列化和应用程序级处理提供了清晰的抽象和实现。Thrift 的代码生成系统采用了简单的定义语言作为输入，并生成跨编程语言的代码，通过这些抽象代码来构建可互操作的 RPC 客户端和服务器。

Thrift 使用不同编程语言编写的程序可以轻松共享数据和调用远程过程。当前支持的[28 种语言](https://github.com/apache/thrift/blob/master/LANGUAGES.md)。可以参考的 Thrift[指南](https://diwakergupta.github.io/thrift-missing-guide/#_versioning_compatibility)
