# .NET 每周分享第 071 期

## 开源项目

1、[TickerQ](https://github.com/Arcenox-co/TickerQ)

TickerQ 是一个专为 .NET 生态打造的高性能、无反射后台任务调度器，其核心借助 Source Generator 在编译期生成任务注册与调度代码，运行时零反射、开销极小。官方将功能拆分为三大 NuGet 包：TickerQ 核心、TickerQ.EntityFrameworkCore 持久化扩展，以及提供实时监控的 TickerQ.Dashboard。

2、[纯.NET实现的2D像素编辑器](https://github.com/PixiEditor/PixiEditor)

PixiEditor 是一个由 C#/.NET 8 构建的跨平台 2D 图像编辑器，目标是为像素绘制、数字绘画、矢量图形与动画等多种场景提供"一站式"解决方案。

3、[AIApiTracer](https://github.com/Cysharp/AIApiTracer)

AIApiTracer 是 Cysharp 团队开发的一款轻量级、本地化的 AI 请求跟踪工具。

## 文章推荐

1、[使用架构测试处理 CancellationToken 和 Sealed 类](https://steven-giesel.com/blogPost/feac44f3-2c6e-4994-80c3-e2a17efbb8f3/using-architecture-tests-for-cancellationtokens-and-sealed-classes)

本文介绍如何利用架构测试（Architecture Tests）在 .NET 项目中自动检查两类常见的编码规范：1) 控制器方法必须接收 CancellationToken，并且参数名称符合 token/cancellationToken；2) 以 View 或 Model 结尾的 DTO 类应被标记为 sealed。

2、[.NET 8 到 .NET 10 迁移指导](https://www.mobilize.net/blog/dotnet8-to-dotnet10-migration-guide)

博客指出，.NET 10 将于 2025 年 11 月正式发布并成为 .NET 8 的下一代 LTS 版本（支持至 2028 年）。作者建议：时间规划、破坏性变更、迁移步骤、工具支持等方面的详细指导。

3、[C# MCP SDK 升级](https://devblogs.microsoft.com/dotnet/mcp-csharp-sdk-2025-06-18-update/)

微软 .NET 团队在官方博客发布文章，宣布 MCP（Model Context Protocol）C# SDK 已全面支持 2025-06-18 最新协议规范。此次升级为 .NET 开发者在构建 AI 应用时带来了更丰富、也更安全的能力。

## 行业资讯

1、[dotinsight 7 月份集锦](https://blog.jetbrains.com/dotnet/2025/07/15/dotinsights-july-2025/)

这一期 dotInsights（2025 年 7 月）由 JetBrains .NET 团队整理，定位为 .NET 开发者的月度资讯汇。

## 视频推荐

1、[dotnet run app.cs 后的隐藏的功能](https://www.youtube.com/watch?v=473o5AWkJec&ab_channel=NickChapsas)

Nick Chapsas 在这段视频中深入展示了 .NET 10（预览版）中新加入的"dotnet run app.cs"脚本化功能，并补充了官方演示中遗漏的细节。

2、[.NET MCP 学习](https://www.youtube.com/watch?v=DpyjAKmNwpI&t=1876s&ab_channel=NickChapsas)

本视频由 Nick Chapsas 邀请 Dan Clarke 讲解如何在 .NET 中快速上手 Model Context Protocol（MCP）。MCP 让 LLM 以统一的协议调用外部服务，被众多厂商与社区迅速采纳。