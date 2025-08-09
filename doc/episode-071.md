.NET 每周分享第 71 期
## 行业资讯
1、 [.NET Conf 2025 内容招募](https://devblogs.microsoft.com/dotnet/dotnet-conf-2025-announcing-the-call-for-content/)
微软宣布 .NET Conf 2025 正式开启议题征集，截止日期为 2025 年 8 月 31 日 23:59 PDT。大会将于 11 月 11–13 日在线举行，重点发布 .NET 10，并深入探讨 .NET Aspire 新特性及 AI 场景。官方期望社区提交 30 分钟（含 Q&A）的分享，每位讲者限 1 场，可按所在时区远程呈现。

优选内容：
- Web：ASP.NET Core、Blazor 等实战
- 移动/桌面：.NET MAUI、遗留系统现代化
- AI/ML：ML.NET、AI 集成案例
- IoT/Edge：嵌入式与设备端方案
- 游戏：Unity 或原生 .NET 游戏开发
- 云与容器：微服务、云原生实践
- DevOps：高效 CI/CD、部署策略
- 开源：库/工具分享或贡献体验

评审更看重真实项目经验、架构剖析与生产最佳实践，而非简单功能介绍。建议在提案中附上过往演讲或项目演示视频，帮助评委了解讲者风格。征集入口已开放（sessionize.com/net-conf-2025），大会免费、面向全球开发者，鼓励首次登台的新人投稿，与 .NET 社区共同庆祝年度盛会。

## 视频推荐
1、 [.NET GraphQL 学习](https://www.youtube.com/watch?v=YL07NyBXC7M&ab_channel=NickChapsas)
在本期视频中，Nick Chapsas 通过一个从零开始的演示，带大家快速了解如何在 .NET 平台上使用 GraphQL（以 HotChocolate 为例）构建 API，并与传统 REST 风格进行对比。

1. GraphQL 基础概念：Nick 首先解释 GraphQL 的核心优势——客户端按需获取数据、减少 over-fetching/under-fetching 以及单一端点带来的简化调用。随后展示 Schema、Query、Mutation、Subscription 的组成，并强调类型系统保证了强一致性与自描述能力。

2. 创建项目与安装依赖：使用 `dotnet new web` 快速生成最简 API，再通过 NuGet 引入 `HotChocolate.AspNetCore`、`HotChocolate.Data` 等包，开启 GraphQL 中间件。

3. 定义模型与 Schema：
   • Code-First 方式：直接在 C# 类上加特性 `[GraphQLName]`、`[GraphQLDescription]` 等生成对象类型；查询方法以 `Query` 类承载。
   • 展示了字段选择、嵌套对象、枚举与非空类型的映射。

4. 数据获取与性能：
   • 集成 EF Core，演示 HotChocolate 的 Data Loader 机制，以批量查询和缓存避免 N+1 问题。
   • 使用 `UsePaging`、`UseFiltering`、`UseSorting` 快速为列表提供分页、过滤与排序能力。

5. 安全与验证：通过 `Authorize` 特性结合 ASP.NET Core 身份验证管道，实现基于声明或角色的字段级访问控制。

6. 工具链：演示 Banana Cake Pop（HotChocolate 自带的 IDE）实时编写/测试查询，并展示 Schema 文档和 Introspection 带来的开发体验。

7. 与 REST 的对比与适用场景：Nick 总结 GraphQL 适合需要灵活查询、前端迭代频繁、数据模型复杂的项目；而纯 CRUD、简单读写接口 REST 依然高效。

整体来看，本视频在 30 分钟左右的时间里覆盖了从零搭建、核心语法到进阶优化的完整流程，新手可快速上手，已有 REST 经验的开发者亦能对 GraphQL 的价值形成清晰认知。