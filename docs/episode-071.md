# .NET 每周分享第 071 期

## 开源项目
1、[TickerQ](https://github.com/Arcenox-co/TickerQ)
   TickerQ 是专为 .NET 生态打造的高性能、无反射后台任务调度器，核心依赖 Source Generator 实现零反射、极小开销。支持一性 TimeTicker 与周期性 CronTicker，EF Core 持久化，Dashboard 实时监控，MIT/Apache-2.0 双 License，文档完善，欢迎社区贡献。

2、[纯.NET实现的2D像素编辑器](https://github.com/PixiEditor/PixiEditor)
   PixiEditor 是由 C#/.NET 8 构建的跨平台 2D 图像编辑器，集成 Pixel Art、Painting、Vector 三套工具，支持动画与时间轴，节点渲染系统，UI 体验类 Photoshop，LGPL-3.0 许可，支持桌面端编译运行，社区生态完善。

3、[AIApiTracer](https://github.com/Cysharp/AIApiTracer)
   AIApiTracer 是 Cysharp 团队开发的轻量级、本地化 AI 请求跟踪工具，支持 OpenAI、Anthropic、Azure OpenAI 等 HTTP 调用，Web UI 展示请求与响应，支持 Docker 部署，适合开发阶段多家大模型服务快速调试、对比和排障。

## 文章推荐
1、[使用架构测试处理 CancellationToken 和 Sealed 类](https://steven-giesel.com/blogPost/feac44f3-2c6e-4994-80c3-e2a17efbb8f3/using-architecture-tests-for-cancellationtokens-and-sealed-classes)
   介绍如何利用架构测试自动检测 Controller 方法参数命名规范和 DTO 类 sealed 校验，结合 NetArchTest.Rules fluent API，CI 阶段自动化检测团队约定，提升一致性，避免后期重构成本。

2、[.NET 8 到 .NET 10 迁移指导](https://www.mobilize.net/blog/dotnet8-to-dotnet10-migration-guide)
   .NET 10 将于 2025 年 11 月发布，成为 .NET 8 的下一代 LTS。文章梳理迁移时间规划、Breaking Changes、迁移步骤、工具支持与常见陷阱，建议分阶段升级、充分自动化测试，保障长期支持。

3、[C# MCP SDK 升级](https://devblogs.microsoft.com/dotnet/mcp-csharp-sdk-2025-06-18-update/)
   微软 .NET 团队发布 MCP C# SDK 最新协议规范，支持全新身份验证协议、Elicitation 机制、结构化工具输出、资源链接、Schema 与元数据改进，提升安全性与交互性，适合 AI 助手、自动化工具集成。

## 行业资讯
1、[dotinsight 7 月份集锦](https://blog.jetbrains.com/dotnet/2025/07/15/dotinsights-july-2025/)
   JetBrains .NET 团队整理的 7 月度资讯，涵盖代码质量、性能、平台、框架实战、前沿思考等优质文章与视频，展示 JetBrains 生态进展，鼓励开发者持续关注。

## 视频推荐
1、[dotnet run app.cs 后的隐藏的功能](https://www.youtube.com/watch?v=473o5AWkJec&ab_channel=NickChapsas)
   Nick Chapsas 深入演示 .NET 10 新增的 `dotnet run app.cs` 脚本化功能，支持 shebang、NuGet 引用、隐藏命令，降低新手门槛，适合自动化脚本、CI/CD 场景。

2、[.NET MCP 学习](https://www.youtube.com/watch?v=DpyjAKmNwpI&t=1876s&ab_channel=NickChapsas)
   Nick Chapsas 邀请 Dan Clarke 讲解如何在 .NET 中快速上手 Model Context Protocol（MCP），演示 MCP Server、客户端 VS Code Copilot Agent、Rider 等标准 IO 场景，展示对话式开发新范式。
