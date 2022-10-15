# .NET 每周分享第 24 期

## 卷首语

[Docker SQL Server 使用](https://www.twilio.com/blog/containerize-your-sql-server-with-docker-and-aspnet-core-with-ef-core)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/DockerSQLServer.png)

SQL Server 是 `.NET` 中广泛使用的关系型数据库，一般我们需要在本地机器上安装，但是这样会带来一些缺陷，比如会和其他引用和程序共享一个数据库，或者在 `Linux` 环境中测试就比较困难。这篇文章介绍了如何在 `Docker` 中安装 `SQL Server`。

## 行业资讯

1、[Entity Framework Core 7 (EF7) RC2发布](https://devblogs.microsoft.com/dotnet/announcing-ef7-release-candidate-2)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/EFCore.jfif)

Entity Framework Core RC2现在已经发布，RC2版本包含GA版本计划的所有功能。主要包括下面内容：

- 映射到 `SQL Server` JSON列
- `ExecuteUpdate` 和 `ExecuteDelete` (批量更新)
- 改进了 `SaveChanges` 的性能
- 每个具体类型的表 (TPC) 继承映射
- 自定义逆向工程模板 （Database First）
- 可定制的模型生产约定
- 用于插入、更新和删除的存储过程映射
- 新增和改进的拦截器和事件
- 查询增强功能，包括更多分组依据和分组连接翻译

## 文章推荐

1、[.NET 中调用 JavaScript](https://devblogs.microsoft.com/dotnet/use-net-7-from-any-javascript-app-in-net-7)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/dotnetCallJS.png)

`.NET 7` 为在基于 `JavaScript` 的应用程序中的 `WebAssembly` 提供了改进的支持，包括丰富的 `JavaScript` 互操作机制。 .NET 7 中的 `WebAssembly` 支持是 `Blazor WebAssembly` 应用程序的基础，但也可以独立于 Blazor 使用。现有的 `JavaScript` 应用程序可以使用 .NET 7 中扩展的 `WebAssembly` 支持来重用 `JavaScript` 中的 .NET 库或构建全新的基于 .NET 的应用程序和框架。 `Blazor WebAssembly` 应用程序还可以使用新的 `JavaScript` 互操作机制来优化与 `JavaScript` 和 `Web` 平台的交互。在这篇文章中，我们将了解 .NET 7 中新的 `JavaScript` 互操作支持，并使用它来构建经典的 `TodoMVC` 示例应用程序。

2、[Linq 学习](https://anthonygiretti.com/2022/09/29/net-learn-linq-as-you-never-have-before/?utm_source=isaacl&utm_medium=twitter&utm_campaign=link&WT.mc_id=link-twitter-isaacl)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/LINQUse.png)

这份文档详细的介绍了几乎所有的 `LINQ` 基本使用场景和方法。
例如：

- 过滤
- 查询投影
- 聚合
- 查询数量
- 合并
- 队列比较
- 查询元素
- 转换数据类型
- 分组
- 集合操作，连接

3、[RateLimter 中间件介绍](https://blog.maartenballiauw.be/post/2022/09/26/aspnet-core-rate-limiting-middleware.html)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/RateLimter.png)

`ASP.NET Core 7` 引入了 `RateLimit` 中间件，通过它可以保护我们资源（CPU，内存和磁盘 I/O）不会因为大量访问而导致服务不够用。这篇播客详细介绍了如何使用这个中间件：

- 全局的访问限制
- 内置不同种类的 RateLimter
- 自定义 RateLimiter
- 不同 API 的 RateLimiter

4、[.NET 名字的故事](https://www.cnet.com/tech/tech-industry/net-name-ties-microsoft-in-knots)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/DotNETName.png)

现在不少大 V 呼吁微软将 `.NET` 重新命名为 `dotnet`，因为这样更加有助于市场推广和搜索。这篇文章带你回顾了 `.NET` 名字刚刚推出的时候，媒体和社区对其的困惑。

6、[ASP.NET Core 的配置验证](https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-in-dotnet-6)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/ASP.NETCoreConfigure.png)

`IConfiguration` 和 `IOption` 是 `ASP.NET Core` 引入的配置组件，`IConfiguration` 可以提供多种多样的配置来源，比如 `JSON` 文件，环境变量和命令行参数等等。再通过 `IOption` 将它转换成一个  POCO 对象，但是这样会带来一个两个问题：

- 如果配置由于 `typo` 导致某些 key 不匹配，这样就会变成默认值
- 由于配置的 `Value` 格式没有满足一定的要求，导致其变成非法值

在 `ASP.NET Core 6` 中支持校验配置的类型值

- POCO 对象

```csharp
public class SlackApiSettings
{
    [Required, Url]
    public string WebhookUrl { get; set; }
    [Required]
    public string DisplayName { get; set; }
    public bool ShouldNotify { get; set; }
}
```

- JSON 配置源

```json
{
  "SlackApi": {
    "Url": "http://example.com/test/url",
    "DisplayName": "My fancy bot",
    "ShouldNotify": true
  }
}
```

- 注册配置

```csharp
builder.Services.AddOptions<SlackApiSettings>()
    .BindConfiguration("SlackApi") // 👈 Bind the SlackApi section
    .ValidateDataAnnotations() // 👈 Enable validation
    .ValidateOnStart(); // 👈 Validate on app start
```

7、[如何在 ASP.NET Core 中配置多个JSON格式](https://thomaslevesque.com/2022/09/19/using-multiple-json-serialization-settings-in-aspnet-core)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/JsonConfigure.png)

`ASP.NET Core` 拥有丰富的扩展性，用户可以根据自己需求来自定义拓展来完成。这篇文章以不同的 `Controller` 使用不同的 `JSON` 格式化需求为例子，展示了 `ASP.NET Core` 的扩展性。

## 开源项目

1、[ASP.NET Core 最佳实践](https://github.com/davidfowl/AspNetCoreDiagnosticScenarios)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/AspDotNETCore.png)

这个仓库介绍了 `ASP.NET Core` 应用程序在使用的时候的注意点，还有异步方法在调用的时候最佳实践。

2、[Figma 导出成 MAUI](https://github.com/jsuarezruiz/figma-to-maui-graphics)

![image](https://dotnetweeklyimages.blob.core.windows.net/024/exposeMAUI.png)

Figma 是专业的 UI 在线设计软件，几乎每个设计师都在使用它。而 MAUI 是跨平台的 UI 设计库，自然而然我们希望将 Figma 中的设计自动转换成 MAUI 的设计代码，而这个库就能帮助我们做到这一点。
