# .NET 每周分享第 071 期

## 开源项目

1、[TickerQ](https://github.com/Arcenox-co/TickerQ): TickerQ 是一个专为 .NET 生态打造的高性能、无反射后台任务调度器，其核心借助 Source Generator 在编译期生成任务注册与调度代码，运行时零反射、开销极小。官方将功能拆分为三大 NuGet 包：TickerQ 核心、TickerQ.EntityFrameworkCore 持久化扩展，以及提供实时监控的 TickerQ.Dashboard。

2、[纯.NET实现的2D像素编辑器](https://github.com/PixiEditor/PixiEditor): PixiEditor 是一个由 C#/.NET 8 构建的跨平台 2D 图像编辑器，目标是为像素绘制、数字绘画、矢量图形与动画等多种场景提供"一站式"解决方案。

3、[AIApiTracer](https://github.com/Cysharp/AIApiTracer): AIApiTracer 是 Cysharp 团队开发的一款轻量级、本地化的 AI 请求跟踪工具。

## 文章推荐

1、[使用架构测试处理 CancellationToken 和 Sealed 类](https://steven-giesel.com/blogPost/feac44f3-2c6e-4994-80c3-e2a17efbb8f3/using-architecture-tests-for-cancellationtokens-and-sealed-classes): 本文介绍如何利用架构测试（Architecture Tests）在 .NET 项目中自动检查两类常见的编码规范：1) 控制器方法必须接收 CancellationToken，并且参数名称符合 token/cancellationToken；2) 以 View 或 Model 结尾的 DTO 类应被标记为 sealed。作者借助 NetArchTest.Rules 提供的 fluent API，结合反射遍历程序集，动态筛选所有以 Controller 结尾并继承 ControllerBase 的类型，再枚举公开实例方法，找出缺少 CancellationToken 或命名不规范的方法，并使用 Shouldly 断言确保测试失败时能给出精确列表。

2、[.NET 8 到 .NET 10 迁移指导](https://www.mobilize.net/blog/dotnet8-to-dotnet10-migration-guide): 博客指出，.NET 10 将于 2025 年 11 月正式发布并成为 .NET 8 的下一代 LTS 版本（支持至 2028 年）。作者建议：时间规划：.NET 8 结束支持日为 2026-11-10，应在 2025 年 Q3 开始使用预览版（目前已到 Preview 6）测试，预留 3-6 个月完成评估、升级与回归。

3、[C# MCP SDK 升级](https://devblogs.microsoft.com/dotnet/mcp-csharp-sdk-2025-06-18-update/): 微软 .NET 团队在官方博客发布文章，宣布 **MCP（Model Context Protocol）C# SDK 已全面支持 2025-06-18 最新协议规范**。此次升级为 .NET 开发者在构建 AI 应用时带来了更丰富、也更安全的能力，主要亮点如下：全新的身份验证协议、Elicitation（信息征询）机制、结构化工具输出、结果中的资源链接、Schema 与元数据改进、快速上手。

## 视频推荐

1、[dotnet run app.cs 后的隐藏的功能](https://www.youtube.com/watch?v=473o5AWkJec&ab_channel=NickChapsas): Nick Chapsas 在这段视频中深入展示了 .NET 10（预览版）中新加入的"dotnet run app.cs"脚本化功能，并补充了官方演示中遗漏的细节。

2、[.NET MCP 学习](https://www.youtube.com/watch?v=DpyjAKmNwpI&t=1876s&ab_channel=NickChapsas): 本视频由 Nick Chapsas 邀请 Dan Clarke 讲解如何在 .NET 中快速上手 Model Context Protocol（MCP）。MCP 让 LLM 以统一的协议调用外部服务，被众多厂商与社区迅速采纳。演示中 Dan 创建了一个「Podcast MCP Server」，只需在 `Program.cs` 中调用 `builder.Services.AddMcpServer()` 并用 `[McpTool]` 标注方法，即可把方法暴露为 LLM 可调用的「Tool」。

## 行业资讯

1、[dotinsight 7 月份集锦](https://blog.jetbrains.com/dotnet/2025/07/15/dotinsights-july-2025/): 这一期 dotInsights（2025 年 7 月）由 JetBrains .NET 团队整理，定位为 .NET 开发者的月度资讯汇。开篇用一个 C# 小技巧──利用 @ 前缀让保留字成为变量名──作为"冷知识"，随后发布了 JetBrains .NET Days Online 2025 的演讲者招募信息。