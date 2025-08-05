# .NET 每周分享第 71 期
## 行业资讯
1、 [ .NET 悬赏计划开始](https://msrc.microsoft.com/blog/2025/07/.net-bounty-program-now-offers-up-to-40000-in-awards/)
微软安全响应中心（MSRC）在最新博文中宣布，将 .NET 漏洞悬赏计划全面升级，并把单个漏洞的最高奖金从 2 万美元提升至 4 万美元。

• 适用范围：.NET 6/7/8（含预览版）、ASP.NET Core、Entity Framework Core、Blazor，以及 Windows Forms、WPF 等核心运行时与开发框架。
• 奖金梯度：根据漏洞影响面与利用难度，从 2,000 美元到 40,000 美元不等；可导致远程代码执行、权限提升或突破关键安全边界的漏洞奖金额最高。
• 提交要求：需提供完整 PoC、漏洞成因分析，并在 Visual Studio “发布”配置下编译；严禁在生产环境或真实用户数据上测试。
• 处理时效：微软承诺 72 小时内确认漏洞是否符合悬赏范围，30 天内给出初步修复方案；研究人员协助补丁验证可再获 10% 额外奖励。
• 计划成效：过去一年已收到 170 份有效报告，向 45 名研究人员支付超过 25 万美元奖金。

微软表示，随着 .NET 在云端与本地的广泛部署，强化生态安全对客户与开发者至关重要，希望借助全球安全社区力量，进一步提升 .NET 平台的安全韧性。研究人员可通过 MSRC 门户提交报告，并使用官方 Docker 镜像进行本地验证，以避免影响真实服务。
2、 [dotinsight 7 月份集锦](https://blog.jetbrains.com/dotnet/2025/07/15/dotinsights-july-2025/)
这一期 dotInsights（2025 年 7 月）由 JetBrains .NET 团队整理，定位为 .NET 开发者的月度资讯汇。

开篇用一个 C# 小技巧──利用 @ 前缀让保留字成为变量名──作为“冷知识”，随后发布了 JetBrains .NET Days Online 2025 的演讲者招募信息。

核心内容是对社区优质文章与视频的精选链接，主题覆盖面广：
• 代码质量与重构：Emily Bache 演示如何借助 Adapt Parameter 安全地重构遗留代码；Stefan Pölz/Eva Ditzelmüller 详解 Dispose 模式。
• 性能与平台：Steven Giesel 分享 .NET 10 的性能特性；Khalid Abuhakmeh 探索 ASP.NET Core 中的 Server-Sent Events；Jeremy D. Miller 讨论 Wolverine 的“低仪式”Railway 编程。
• 框架与实战：Leomaris Reyes 介绍 MAUI DrawingView；Dennis Doomen 推出 .NET Library Starter Kit；多篇文章讨论 API 合约、OAuth 新流、Task 并发异常处理等。
• 前沿与思考：文章和视频探讨 AI 招聘、公平性、以及“氛围编码”等话题。

“.NET Guide 精选”栏目提炼了两篇教程：Ian Griffiths 的高性能 JSON 序列化（依赖 C#11 代码生成）以及 Khalid Abuhakmeh 的 HTTP 请求时间线分析技巧。

“JetBrains 新闻”板块列出近期产品动态：ReSharper VS Code 公开预览、Rider 2025.2 EAP 的调试与监控增强、OpenTelemetry 插件、SQL/NoSQL 查询语言支持，以及多次小版本补丁发布。同时，GameDev Days 2025 也开始征集演讲者。

整篇通讯既提供了学习资料，又呈现 JetBrains 生态进展，并附有订阅入口与联系方式，鼓励开发者持续关注。

## 文章推荐
1、 [使用架构测试处理 CancellationToken 和 Sealed 类](https://steven-giesel.com/blogPost/feac44f3-2c6e-4994-80c3-e2a17efbb8f3/using-architecture-tests-for-cancellationtokens-and-sealed-classes)
本文介绍如何利用架构测试（Architecture Tests）在 .NET 项目中自动检查两类常见的编码规范：1) 控制器方法必须接收 CancellationToken，并且参数名称符合 token/cancellationToken；2) 以 View 或 Model 结尾的 DTO 类应被标记为 sealed。作者借助 NetArchTest.Rules 提供的 fluent API，结合反射遍历程序集，动态筛选所有以 Controller 结尾并继承 ControllerBase 的类型，再枚举公开实例方法，找出缺少 CancellationToken 或命名不规范的方法，并使用 Shouldly 断言确保测试失败时能给出精确列表。对于 sealed 校验，同样通过类型后缀过滤，然后调用 BeSealed 规则验证，最后输出未 sealed 的类名。作者强调，这类架构测试并不关注业务逻辑，而是将团队约定固化为自动化检查，能在 CI 阶段及早暴露设计偏差；同时可配合分析器保证 CancellationToken 在调用链中被继续传递。总体而言，文章示范了如何在项目中用少量代码构建“编码守门人”，既提高一致性，又避免日后大规模重构成本。
2、 [ .NET 8 到 .NET 10 迁移指导](https://www.mobilize.net/blog/dotnet8-to-dotnet10-migration-guide)
博客指出，.NET 10 将于 2025 年 11 月正式发布并成为 .NET 8 的下一代 LTS 版本（支持至 2028 年）。作者建议：

• 时间规划：.NET 8 结束支持日为 2026-11-10，应在 2025 年 Q3 开始使用预览版（目前已到 Preview 6）测试，预留 3-6 个月完成评估、升级与回归。

• 破坏性变更：文章按技术域列举所有 Breaking Changes，需要同时考虑 .NET 9 与 .NET 10 累积影响。例如默认容器镜像改用 Ubuntu；ActivitySource 行为调整；HttpClient 证书吊销检查默认在线；DriveInfo 在 Linux 返回真实文件系统类型；macOS 不再支持 OpenSSL；单文件发布的本地库搜索路径变更等。EF Core、C# 编译器、MSBuild、Blazor 及 SDK 亦有大量 API 删除或默认行为改变，文中给出迁移前后代码片段与 AppContext 开关。

• 迁移步骤：先更新 <TargetFramework>net10.0</TargetFramework>，然后处理编译警告（建议 TreatWarningsAsErrors）；升级或替换第三方包；在 CI 中双版本并行验证；关注跨平台差异（如时间/时区、加密 API、容器基础镜像）。

• 工具支持：使用 .NET Upgrade Assistant（VS 插件或 CLI）可自动修改项目文件、替换过时 API，并生成报告；结合 Roslyn 分析器可完成自定义规则。

• 常见陷阱：忽略警告、依赖库未及时更新、大型单体一次性切换压力过大、UTC 行为变化导致数据偏移等。作者建议分阶段升级、充分自动化测试，并及早在预览版环境中发现问题。

总体而言，.NET 10 带来了性能、安全及标准化改进，但只要提前规划、利用官方工具并保持测试驱动，迁移成本可控，且能确保未来三年的长期支持。
3、 [C# MCP SDK 升级](https://devblogs.microsoft.com/dotnet/mcp-csharp-sdk-2025-06-18-update/)
### 文章概要

微软 .NET 团队在官方博客发布文章，宣布 **MCP（Model Context Protocol）C# SDK 已全面支持 2025-06-18 最新协议规范**。此次升级为 .NET 开发者在构建 AI 应用时带来了更丰富、也更安全的能力，主要亮点如下：

1. **全新的身份验证协议**  
   新规范将认证服务器与资源服务器职能拆分，可与现有 OAuth 2.0 / OpenID Connect 流程无缝集成。官方推荐在生产环境启用资源指示器与严格的令牌校验，提升安全性。

2. **Elicitation（信息征询）机制**  
   服务器可以在交互过程中向用户“追问”额外上下文，实现更动态的 AI 体验。SDK 在服务端提供 `ElicitAsync` 扩展方法，在客户端通过配置 `ElicitationHandler` 处理征询请求，使控制台 / GUI / Web 客户端都能与用户交互式收集数据。

3. **结构化工具输出**  
   工具方法可通过 `McpServerTool(UseStructuredContent = true)` 明确声明返回结构化数据。SDK 会自动生成 JSON Schema 并在 `tools/list` 响应中暴露，调用结果也会在 `structuredContent` 字段中返回，让大模型或宿主程序更容易解析与推理。

4. **结果中的资源链接**  
   工具现在可在返回值中直接嵌入 `ResourceLinkBlock`，为客户端提供资源发现与后续操作入口，适用于生成报告、文件或业务实体等场景。

5. **Schema 与元数据改进**  
   - `_meta` 字段支持范围扩大，可存放版本、作者、自定义标签等信息。  
   - 名称（name）与标题（title）分离，既保持机器友好标识，又能向用户展示易读标题。

6. **快速上手**  
   通过 `dotnet add package ModelContextProtocol --prerelease` 即可获取新版 SDK。官方文档与示例仓库已同步更新，欢迎社区参与反馈与贡献。

> 总结：本次更新显著提升了 MCP C# SDK 的安全性、交互性与数据结构表达能力，为 .NET 开发者构建 AI 助手、自动化工具或复杂业务集成提供了更坚实的基础。立即升级体验这些新特性，拥抱更智能的 .NET 未来！

## 视频推荐
1、 [dotnet run app.cs 后的隐藏的功能](https://www.youtube.com/watch?v=473o5AWkJec&ab_channel=NickChapsas)
Nick Chapsas 在这段视频中深入展示了 .NET 10（预览版）中新加入的“dotnet run app.cs”脚本化功能，并补充了官方演示中遗漏的细节。

1. 基础能力：单个 C# 文件无需项目即可通过 `dotnet run app.cs` 执行，Windows、Linux、macOS 均支持。结合 VS Code 扩展，即可获得语法高亮与调试。
2. Shebang 支持：在文件顶部加入 `#!/usr/bin/env dotnet run` 并赋予可执行权限后，可直接把 `app.cs` 作为可执行脚本运行，使用体验类似 Node/Python。
3. 隐藏命令：除了 run，还可显式执行 `dotnet restore app.cs` 与 `dotnet build app.cs` 来完成包还原和编译；对应的临时项目文件完全隐式生成。
4. NuGet 引用：可在文件顶部用 `#r "nuget: Humanizer, 2.14.1"` 引入包，若不写版本号则默认解析第一个版本；可使用通配符限定次要或修订版本。
5. 局限性：当前无法引用其他 .cs 文件；团队正讨论新增 `import` 指令以支持目录或文件级引用。作者也希望未来能提供 `dotnet compile app.cs` 直接生成 EXE/AOT 二进制。
6. 设计动机：一方面降低新手门槛、与脚本语言竞争；另一方面为替代 PowerShell／bash 等自动化脚本提供 C# 方案，适用于 CI/CD、基础设施部署等场景。
7. 反响：社区总体正面，认为对自动化、教学演示、快速原型特别有价值；也有人担忧微软是否应优先投入此方向。

作者最后呼吁开发者提出需求与建议，以帮助团队完善该特性。
2、 [.NET MCP 学习](https://www.youtube.com/watch?v=DpyjAKmNwpI&t=1876s&ab_channel=NickChapsas)
本视频由 Nick Chapsas 邀请 Dan Clarke 讲解如何在 .NET 中快速上手 Model Context Protocol（MCP）。MCP 让 LLM 以统一的协议调用外部服务，被众多厂商与社区迅速采纳。演示中 Dan 创建了一个「Podcast MCP Server」，只需在 `Program.cs` 中调用 `builder.Services.AddMcpServer()` 并用 `[McpTool]` 标注方法，即可把方法暴露为 LLM 可调用的「Tool」。Tool 描述文字决定了模型是否会选择调用它，返回值可用强类型或字符串 JSON。 

客户端可在 VS Code 的 Copilot Agent、Rider 或自定义代码中通过标准 IO、SSE 及可流式 HTTP 与 MCP Server 通讯，标准 IO 场景下只需在 `mcp.json` 写入 `dotnet run` 命令即可本地启动服务。示例展示了：1）在 VS Code 中选择工具并由模型自动决定调用；2）在代码里通过 `IMcpClient` 结合 OpenAI `IChatClient` 以自然语言检索带标签的播客；3）在 Server 端借助「Sampling」反向调用 LLM，把标题改写成幽默风格。 

MCP 还支持「Prompt 模板」与「Resource」：开发者可预设常用提示词模板或附加文件/链接，用户在聊天时通过 / 命令一键插入，或将资源上下文发送给模型。 

最后 Dan 演示了 Playwright MCP Server，模型自动对网站进行无头浏览、找出错误并生成报告，进一步展示了多工具协同带来的开发新范式。他提醒：设计工具要单一职责、粒度细，以便模型按需组合调用；部署可使用 Docker 镜像，通过 `docker run` 即可在本地以标准 IO 方式启动。整套流程表明 MCP 简单易用却威力巨大，为未来“对话式开发”奠定了基础。

## 开源项目
1、 [基于.NET的Toml文件读写库Tomlet](https://github.com/SamboyCoding/Tomlet)
Tomlet 是一个完全基于 .NET、零依赖的 TOML 序列化/反序列化库，完整实现了 TOML 1.0.0 规范。它采用“模型驱动”方式：可直接把任意对象序列化为 TOML 字符串，也能把 TOML 文本映射回对象；同时提供 Document/Value API 便于逐级访问、修改键值。

在序列化时 Tomlet 会重新排序文档以提升可读性：先输出简单键值，再输出子表，再输出表数组；小型表和数组会自动内联，可用属性或标志禁止内联。库能够解析、保留并重新写出注释，并通过 TomlInlineComment / TomlPrecedingComment 属性让模型类自带注释。

默认映射基于反射，支持字段和属性，可用 TomlProperty 指定键名，用 TomlNonSerialized 或 [NonSerialized] 忽略成员；若需特殊格式可通过 RegisterMapper 自定义序列化/反序列化逻辑。解析方面提供 Parse、ParseFile 等方法，返回的 TomlDocument/TomlTable 支持 GetString、GetInteger、GetArray 等强类型读取及 ContainsKey、子表导航。

库对错误处理做了细粒度设计，所有解析与映射异常均继承自 TomlException，覆盖重复键、无效数字、表数组冲突等四十余种场景，便于定位问题。实现只依赖标准库，因此可无缝用于 Unity、Blazor、控制台或服务器应用。

总体而言，Tomlet 兼顾易用性、规范完整性与性能，适合在 .NET 项目中读取或生成 TOML 配置文件，尤其适合需要保持注释与排版友好度的场景。
2、 [TickerQ](https://github.com/Arcenox-co/TickerQ)
TickerQ 是一个专为 .NET 生态打造的高性能、无反射后台任务调度器，其核心借助 Source Generator 在编译期生成任务注册与调度代码，运行时零反射、开销极小。官方将功能拆分为三大 NuGet 包：TickerQ 核心、TickerQ.EntityFrameworkCore 持久化扩展，以及提供实时监控的 TickerQ.Dashboard。

调度模型支持两类作业：一次性 TimeTicker 与周期性 CronTicker。开发者仅需在普通方法上加上 [TickerFunction] 特性即可将其声明为任务，随后可通过 ITimeTickerManager / ICronTickerManager 以代码动态创建、取消或重试任务。每个任务可配置最大重试次数与自定义间隔，失败后自动按策略回退。

若启用 EF Core 持久化，TickerQ 会将任务队列、锁与执行历史存入数据库，并使用分布式锁与所有权跟踪在多节点实例间协调执行，确保高可用与一次性语义。同时支持在应用重启后自动取消已错过的任务。

Dashboard 通过中间件形式注入，默认路由 /tickerq-dashboard，实时展示系统状态、队列深度、活动任务、执行历史等，并支持基础身份认证保护。

注册使用非常简洁，只需在 Program/Startup 调用 AddTickerQ，可选设置最大并发、全局异常处理器、EF 持久化与 Dashboard 路径；完成后调用 app.UseTickerQ 启用调度引擎。如果不使用 UseModelCustomizerForMigrations，则需在 DbContext 手动 ApplyConfiguration。

TickerQ 与 .NET DI 深度集成，任务方法可直接注入服务，并支持 CancellationToken 优雅取消。项目采用 MIT/Apache-2.0 双 License，文档完善，欢迎社区贡献。
3、 [纯.NET实现的2D像素编辑器](https://github.com/PixiEditor/PixiEditor)
PixiEditor 是一个由 C#/.NET 8 构建的跨平台 2D 图像编辑器，目标是为像素绘制、数字绘画、矢量图形与动画等多种场景提供“一站式”解决方案。

核心特性：
1. 多工具集：内置 Pixel Art、Painting、Vector 三套工具，可在同一画布上混合栅格与矢量图层，并输出 PNG、JPG、SVG、GIF、MP4 等格式。
2. 动画与时间轴：2.0 版本加入帧动画和时间轴，支持逐帧绘制或借助节点系统制作程序化动画，未来还将加入矢量关键帧。
3. 节点渲染系统：所有图层、效果都抽象为节点，可自由组合实现平铺纹理、着色器动画乃至简单 3D 形体渲染，为高阶用户提供强扩展性。
4. UI 体验：借鉴 Photoshop/GIMP 的经典布局，降低学习成本，并默认提供深色主题。

运行与构建：官方发布了 1.0 稳定版（Steam 获 93% 好评）和 2.0 公测版；源码使用 LGPL-3.0 许可，依赖 .NET 8 SDK 与 wasi-sdk，克隆仓库后初始化子模块、配置 WASI_SDK_PATH，并执行 `dotnet workload install wasi-experimental` 即可在桌面端编译运行。

社区生态：项目官网、论坛与 Discord 提供支持，开发者可参考 Contributing Guide 贡献代码或扩展节点。PixiEditor 致力于在开源社区的协作下，持续向功能完备的通用 2D 编辑平台进化。
4、 [AIApiTracer](https://github.com/Cysharp/AIApiTracer)
AIApiTracer 是 Cysharp 团队开发的一款轻量级、本地化的 AI 请求跟踪工具。

核心定位：
1. 作为本地开发环境中的反向代理，它能拦截并转发对 OpenAI、Anthropic、Azure OpenAI、xAI 以及其他 OpenAI 兼容接口的 HTTP 调用；
2. 在不依赖任何外部云服务的前提下，将请求与响应内容以可读、友好的方式展示在 Web UI（默认 http://localhost:8080）中，方便开发者调试 Prompt、验证参数或比较不同模型结果；
3. 仅在内存中保存最近 1000 条记录，不做持久化，强调“本地临时调试”而非生产级审计或团队共享。

快速上手：
• Docker：`docker run -p 8080:8080 ghcr.io/cysharp/aiapitracer:latest`；
• 或从 Release 下载二进制执行 `./AIApiTracer --urls http://localhost:8080/`。
随后只需把 SDK 或 curl 的 API BaseUrl 改为 `http://localhost:8080/endpoint/{provider}`，例如 OpenAI 改为 `/endpoint/openai/v1`,Anthropic 改为 `/endpoint/anthropic`, 即可透明地记录流量。

安全与扩展：
• OpenAI 兼容代理默认关闭，开启需设置环境变量 `AIApiTracer__EnableOpenAICompatForwarding=true`，否则可能沦为开放代理风险；
• 计划支持 Google Vertex AI、Amazon Bedrock 等更多服务。

总结：AIApiTracer 提供了“像 Cloudflare AI Gateway Logs 但完全本地可用”的体验，适合个人或小型团队在开发阶段对多家大型模型服务做快速调试、对比和排障。