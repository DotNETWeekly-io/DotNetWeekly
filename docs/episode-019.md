# .NET 每周分享第 19 期

## 卷首语

[C#11的新特性和改进前瞻](https://www.cnblogs.com/hez2010/p/whats-new-in-csharp-11.html)

![C#11](https://dotnetweeklyimages.blob.core.windows.net/019/CSharp11.jpg)

.NET 7 的开发还剩下一个多月就要进入 RC，C# 11 的新特性和改进也即将敲定。该文按照方向从 5 个大类来进行介绍，提前向大家讲述了 C# 11 的新特性和改进都有那些。

文章的主要内容：
- 类型系统的改进
  - 抽象和虚静态方法
  - 泛型Attribute
  - ref字段和scoped ref
  - 文件局部类型
  - required成员
- 运算改进
  - checked运算符
  - 无符号右移运算符
  - 移位运算符放开类型限制
  - IntPtr、UIntPtr 支持数值运算
- 模式匹配改进
  - 列表模式匹配
  - 对 Span<char> 的模式匹配
- 字符串处理改进
  - 原始字符串
  - UTF-8字符串
  - 字符串插值允许换行
- 其他改进
  - struct自动初始化
  - 支持对其他参数名进行nameof
  - 自动缓存静态方法的委托

## 行业资讯

1、[.NET 7 支持 Rate Limiter](https://devblogs.microsoft.com/dotnet/announcing-rate-limiting-for-dotnet)

![Rate Limiter](https://dotnetweeklyimages.blob.core.windows.net/019/RateLimiter.png)

限流是服务可靠性的一种方式，如果我们的资源不能承受巨大的请求量，需要拒绝额外的请求。在 .NET 7 中包含了内置一些 Rate Limiter ，都放在 System.Threading.RateLimiting 包中，主要有四种类型:
1. Concurrency limit
2. Token bucket limit
3. Fixed window limit
4. Sliding window limit


2、[专注于 MAUI 的 .NET conf 来了](https://devblogs.microsoft.com/dotnet/announcing-dotnet-maui-focus-reactor-community-events)

![.NET Conf](https://dotnetweeklyimages.blob.core.windows.net/019/DotNetConf.png)

[.NET Conf：Focus on MAUI](https://focus.dotnetconf.net/?utm_campaign=savedate&utm_medium=blog&utm_source=dotnet) 是一个为期一天的免费直播活动，将于太平洋时间 8 月 9 日上午 9 点开始，来自社区和 Microsoft 团队的演讲者将参与开发并使用 .NET 多平台应用 UI。了解如何使用 .NET MAUI 使用单个代码库为 Android、iOS、macOS 和 Windows 构建原生应用。聆听构建 .NET MAUI 框架和工具的团队以及构建 .NET MAUI 应用程序、库、组件和控件的专家的意见。

## 文章推荐

1、[C# unsafe使用](https://code-maze.com/unsafe-code-csharp)

![C# unsafe](https://dotnetweeklyimages.blob.core.windows.net/019/CSharpUnsafe.png)

我们都知道，`C#` 是一门托管语言，也就是代码运行的特定的运行时 (`runtime`) 上，这样运行时的垃圾回收机制会帮我们管理好内存。但是 `C#` 也支持非托管的代码，这样就可以像 `C/C++` 一样操作指针，自己管理内存够空间，在 `C#` 中只要一个 `unsafe` 关键字即可。
什么时候我们会用到非托管代码呢？两种情况：
1. 需要和 `C/C++` 这样的语言进行交互的，比如 `P/Invoke`
2. 需要性能方面加强的地方

2、[异常抛出的区别](https://twitter.com/BelloneDavide/status/1549080459052736512)

![异常抛出](https://dotnetweeklyimages.blob.core.windows.net/019/ThrowEx.png)

在 `catch` 语句，如果还要将异常往调用层抛，那么有两种方式
- `throw`
- `throw ex`

在第一种方式中，它会将全部调用栈信息抛出; 而第二种则则只会抛出 `throw ex` 所在位置及以后的调用栈信息。

3、[C#中Base64使用方式](https://code-maze.com/base64-encode-decode-csharp)

![C# Base64](https://dotnetweeklyimages.blob.core.windows.net/019/Base64.png)


Base64 是字符串操作中一个重要部分，注意 `Base64` 不是加密方式，而是一种转码方式。所以分为两种情况
- 编码
- 解码

`System.Convert` 类的 `ToBase64String` 是将一个字节数组转为 Base64 的字符串。注意这个方法的签名

```csharp
public static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions option)
```

第一个参数是 `byte[]` 类型， 这样对于要编码的字符串需要转换成字符串, 那么不同的 `Encoding` 方式选择，比如 `UTF-8`, `UTF-16` 等等；`offset` 和 `length` 参数可以指定传入字节的子数组；最后 `option` 可以格式化输出结果。

`FromBaseString` 将 Base64 的字符串转换为字节数组，方法签名

```csharp
public static byte[] FromBase64String(string s);
```

注意返回的结果是 `byte[]` 类型，如果想要完成解码的过程，需要选择特定的 `Encoding` 的 `GetString`方法。

4、[.NET应用程序作为Linux服务](https://code-maze.com/aspnetcore-running-applications-as-linux-service)

![Systemd](https://dotnetweeklyimages.blob.core.windows.net/019/systemd.png)

`Systemd` 是 `Linux` 中广泛使用的的工具，它可以将一个应用程序作为一个服务启动， 这篇文章介绍了如何将 `.NET` 应用程序作为 `Linux` 的一个服务。


5、[APISIX 和 ASP.NET Core 集成](https://techcommunity.microsoft.com/t5/web-development/manage-net-microservices-apis-with-apache-apisix-api-gateway/m-p/3583980)

![APISIX](https://dotnetweeklyimages.blob.core.windows.net/019/APISIX.png)

`APISIX` 是一个著名的开源网关框架，在微服务时代，这是非常重要的一部分。这篇文章介绍了如何在 `.NET` 应用程序中使用 APISIX。

## 开源项目

1、[NLog](https://github.com/NLog/NLog)

![NLog](https://dotnetweeklyimages.blob.core.windows.net/019/Nlog.jpg)

NLog 是 .NET 的免费日志记录平台，具有丰富的日志路由和管理功能。它使您可以轻松地为应用程序生成和管理高质量的日志，无论其大小或复杂性如何。

它可以处理从任何 .NET 语言发出的诊断消息，使用上下文信息对其进行扩充，根据您的首选项设置其格式，并将它们发送到一个或多个目标（如文件或数据库）。NLog可以很容易地写入几个[targets](https://nlog-project.org/config/?tab=targets)（数据库、文件、控制台）并动态更改日志记录配置。NLog支持[结构化](https://github.com/NLog/NLog/wiki/How-to-use-structured-logging)和传统日志记录。

NLog的重点：高性能，易于使用，易于扩展和灵活配置。

2、[MiniExcel](https://github.com/MiniExcel/MiniExcel)

![mimiExcel](https://dotnetweeklyimages.blob.core.windows.net/019/miniExcel.jpg)

简单、高效避免OOM的.NET处理Excel查、写、填充数据工具。

![mimiExcel](https://dotnetweeklyimages.blob.core.windows.net/019/MiniExcelWork.png)

目前主流框架大多需要将数据全载入到内存方便操作，但这会导致内存消耗问题，MiniExcel 尝试以 Stream 角度写底层算法逻辑，能让原本1000多MB占用降低到几MB，避免内存不够情况。
特点：
- 低内存耗用，避免OOM、频繁 Full GC 情况
- 支持即时操作每行数据
- 兼具搭配 LINQ 延迟查询特性，能办到低消耗、快速分页等复杂查询轻量，不需要安装 Microsoft Office、COM+，DLL小于150KB简便操作的 API 风格