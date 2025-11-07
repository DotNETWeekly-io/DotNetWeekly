.NET 每周分享第 75 期
## 行业资讯
1、 [.NET Conf 2025](https://devblogs.microsoft.com/dotnet/get-ready-for-dotnet-conf-2025/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1078.png)
.NET Conf 2025 将于 11 月 11-13 日在线举行，为期三天的免费虚拟大会，聚焦 .NET 平台的最新进展、开源项目与开发者工具，并正式发布 .NET 10，同时带来 Visual Studio 2026 的深度讲解。今年重点涵盖云原生开发（Aspire）、将现有应用快速升级至 .NET 10，以及智能应用构建（AI），包括全新的 Microsoft Agent Framework 与 Model Context Protocol（MCP）支持。所有内容可通过 dotnetconf.net 观看直播（YouTube/Twitch），大会结束后录播将发布到 YouTube，演示源码也会开源在 GitHub。

第一天（11 月 11 日，PST 8AM-6PM）以 .NET 10 发布主题演讲开场，由 Scott Hanselman 及 .NET/Visual Studio 团队介绍 C# 14、运行时/库/编译器性能提升、ASP.NET Core 更新、Blazor 增强、.NET MAUI、Aspire 以及 AI 驱动开发与 Copilot 能力。重点技术会话包括：Dustin Campbell 的 C# 14 新特性；Stephen Toub 的 .NET 10 性能优化盘点；Daniel Roth 的 Blazor 改进（WebAuthN/Passkeys、诊断与性能）；Maddy Montaquila 与 Damian Edwards 的 Aspire 云原生框架；Jeremy Likness 的 Agentic AI 开发模式；David Ortinau 的 .NET MAUI 新功能与工具链改进。全天直播互动可提问，最后还有 Code Party 抽奖。

第二天（11 月 12 日，PST 9AM-5PM）以 Azure 主题演讲（Scott Hunter、Paul Yuknewicz）开启，展示 .NET 与 Azure 的云原生能力：Azure Container Apps、AKS、Functions 及面向 .NET 的 AI 服务。随后深入专题包括：构建远程 MCP 服务器、.NET 与 Redis 集成、Aspire 扩展与定制（David Fowler、Damian Edwards，涵盖自定义集成与仪表板扩展）、Aspire 的愿景与路线、AI Foundry for .NET、将现有 .NET 应用现代化并云化。下午则围绕高级主题：Microsoft.Testing.Platform 新测试平台、.NET 安全研究与实践、NuGet 更新、容器镜像与运行优化、以及在 Visual Studio 中的 AI 驱动测试流程。详细议程见 dotnetconf.net/agenda。

第三天（11 月 13 日，直播 PST 5AM-5PM；YouTube Premiere PST 5PM-3AM）为社区日，全球讲者分享生产经验与创新项目。直播与首映内容涵盖：ASP.NET Core 10 的 Clean Architecture（Ardalis）；从 Xamarin.Forms 迁移至 .NET MAUI 的难点与实战（Iris Classon）；OpenTelemetry 下日志/指标/追踪的可观测性（Yulia Samoylova）；为既有 ASP.NET Web API 添加 MCP 能力（Jonathan “J.” Tower）；C# 12-14 语言演进（Bill Wagner）；高性能地形仿真架构（Dax Pandhi）；ASP.NET Core 中 Passkeys 的实战指南（Maarten Balliauw）；Nullable 引用类型的正确应用（Shawn Wildermuth）；以及更多主题，如 C64 的 C# 现场编码、AI 辅助诊断工具、Raspberry Pi IoT 啤酒酿造、用 Copilot 自动化 Aspire 文档、F# 集成模式、安全优先开发与 GitHub 工具、JSON 解析器迁移、社区工具包盘点等。

此外，11 月 14 日将举行面向初学者的 Student Zone，围绕 C#/.NET 入门与项目实践（AI、Web、移动、游戏）。官方亦鼓励参与全球线下 .NET Conf 社区活动。大会为开发者提供与 .NET/Visual Studio 团队及社区专家交流的机会，共同迎接 .NET 10 的发布与生态升级。

2、 [Uno 和 .NET 官方合作](https://platform.uno/blog/announcing-unoplatform-microsoft-dotnet-collaboration/)

Uno Platform 宣布与微软 .NET 团队建立技术协作，时间点与 .NET 10 RC2 发布同步。该合作旨在回馈并强化 .NET 生态，聚焦跨平台应用开发的关键技术环节，使开发者更快获得平台最新能力与更高性能。

主要进展如下：
- 面向 .NET MAUI 的核心组件贡献，确保 .NET for Android 与 Google 最新发布的 Android 16 QPR2 和 API-36.1 对齐。为适配平台更新，团队改造了绑定基础设施与工具链，引入对“次级 SDK 版本”（minor versions）的支持，突破此前仅支持整数版本的限制。
- 该工作历时约一个半月，由 Uno 与 .NET 工程师深度协作完成，重点提升了 Java 互操作能力、Android 工具的集成与 API 对齐，并更新了运行时支持，以保证新平台能力能稳定、顺畅地被 .NET 应用使用。
- 在 .NET 运行时层面，正探索对官方仓库的贡献以启用 WebAssembly 多线程（WASM multithreading）。这是 Uno 社区高度呼声的特性，将显著提升 .NET Web 应用的性能与可扩展性，尤其在计算密集或并发场景中带来收益。
- 将与微软共同参与 .NET MAUI 核心仓库（.NET for Android、.NET for iOS）的 PR 审查与推进，加速高质量改进的交付节奏，提升跨平台堆栈的整体稳定性与演进速度。
- 与微软共同维护 SkiaSharp。Uno 团队此前已贡献 Wasm 与 Lottie 支持；未来将持续加大投入。SkiaSharp 是 Uno 渲染的图形引擎基础，覆盖移动、Web、桌面与嵌入式平台，其稳定与增强直接关系到跨平台界面一致性与性能表现。

关于 Uno Platform：其为 Apache 2.0 开源项目，帮助 .NET 应用以单一代码库覆盖桌面、移动、Web 与嵌入式。团队同时提供商业化工具 Uno Platform Studio，具备 Hot Reload、Hot Design、设计到代码等能力，且支持所有操作系统与 IDE，旨在显著加速开发迭代。

展望未来，Uno 与微软的协作将持续深入，围绕 .NET for Android、.NET for iOS、.NET 运行时以及 SkiaSharp 等关键组件投入工程资源，使 .NET 在跨平台开发上的性能、能力与平台对齐更加出色，帮助开发者更快、更稳地构建覆盖多端的现代应用。官方亦在 .NET 10 RC2 的发布博客中释出相关协作信息。



## 文章推荐
1、 [Copilot Studio 组使用 .NET WebAssembly 提高性能](https://devblogs.microsoft.com/dotnet/copilot-studio-dotnet-wasm/)

- 背景与目标：微软 Copilot Studio 是面向企业的对话式 AI 与自动化平台，低代码构建智能 Copilot 与机器人，但其运行时以 .NET 为核心，并将 .NET WebAssembly（WASM）用于在浏览器直接运行 C#。这让 Power Fx 公式实时评估、智能校验与响应式体验在客户端实现，同时与服务器端复用相同的解析与验证逻辑，确保行为一致性与可靠性。

- 执行模型与启动优化：在浏览器中通过 web worker 加载 .NET WASM，将重计算从主线程剥离以保持 UI 流畅。采用双模式加载策略并行启动解释（含部分 JIT）与 AOT：解释模式启动快、体积小，可立即交互；AOT 在发布阶段预编译为高性能 WASM 字节码，体积更大但运行更快。启动时并行下载与初始化两者，先用解释模式响应，待 AOT 就绪后无缝接管，迁移状态并释放解释引擎内存，兼顾首屏速度与持续性能。

- 互操作与动态加载：借助 [JSImport]/[JSExport] 实现 .NET 与 JavaScript 双向调用；动态 import dotnet.js 以配置并启动 .NET WASM 运行时；通过 withResourceLoader 精细控制资源加载（如程序集与配置），在多 web worker 实例中高效获取与解压 Brotli 压缩的 .br 文件，降低下载与初始化开销。

- 迁移到 .NET 8 的收益：引入长期支持与安全更新，并带来编译与打包优化。关键指标包括：WASM 引擎体积缩减约 55%；页面与机器人加载时间降低约 44%–56%（视网络条件而定）；命令执行响应加快约 26%–35%（视机器人复杂度而定）；WASM 编译与发布速度提升约 45%。这些优化同时减少带宽与 CDN 成本。

- 成本与缓存策略：JIT 与 AOT 共享的系统 DLL 采用内容哈希并可缓存，避免重复下载；更小的引擎与快速构建缩短 CI/CD 流水线时间与资源消耗，提升团队迭代效率。

- 开发者生产力：使用统一的 dotnet publish 生成 WASM 包，易于融入既有 DevOps；调试服务器与监控工具帮助可视化 JS↔WASM 交互与载荷，快速定位问题；支持预览包快速反馈；以 NPM 形式分发 WASM 模块，便于前端团队集成。

- 业务场景与前景：在浏览器运行 .NET 使低码与专业开发者能构建高性能的对话式与自治式 AI；支撑生成式回答、工具调用与与微软生态的无缝集成；增强自动化、分析与跨平台扩展能力。未来将持续试验与采纳新版本 .NET 的特性，进一步提升性能并与 JavaScript 工具链更紧密整合。

2、 [理解 Random 线程安全](https://steven-giesel.com/blogPost/cc2a3f52-9d35-43d2-9687-c276a2de8e07/linkedin-are-you-still-using-new-random-everywhere)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1075.png)
这篇文章针对 LinkedIn 上“到处使用 new Random 是邪恶的”的说法进行澄清与技术解读。帖子声称 new Random 依赖系统时钟，在紧密循环中多次创建会产生相同结果，并将其与线程安全的 Random.Shared 对比。作者指出：for 循环本身与线程并发无关，而在现代 .NET 中，这些结论已不再成立。

核心背景是 .NET Framework 与现代 .NET（.NET Core/.NET 5+）的实现差异：
- 在 .NET Framework 中，new Random 默认种子来自 DateTime.Now.Ticks，时间分辨率约为 16ms。在极短时间内频繁创建实例可能得到相同种子，进而返回相同 Next(...) 结果。
- 在现代 .NET 中，Random 的默认实现使用 Xoshiro256** 算法（XoshiroImpl），其种子通过操作系统提供的非加密随机源获取（Interop.Sys.SystemNative_GetNonCryptographicallySecureRandomBytes）。这意味着在现代运行时里，紧密循环 new Random 不会再因时钟分辨率导致相同结果。

关于线程安全：
- Random.Shared 提供一个可并发使用的线程安全实例，内部实现为 ThreadSafeRandom，通过 [ThreadStatic] 为每个线程维护独立的 XoshiroImpl 实例，避免竞争。
- 是否“线程安全”取决于使用方式。共享同一个 Random 实例跨多个线程并发调用会出现竞争与异常行为（可通过并行获取大量随机数并统计异常值来观察）。相反，在并发代码中每个线程/任务各自创建自己的 Random 实例（例如在 Parallel.For 内部 new Random）通常没有线程安全问题，但会产生不必要的对象分配。
- 在这种并发场景下，Random.Shared 与“每线程各自 new Random”在行为上相近（前者更省分配且明确线程安全）。

兼容性注意：
- Random 的构造函数中包含类型检查：若为基类 Random 则使用 XoshiroImpl；若为派生类型，为兼容旧行为会使用 CompatDerivedImpl。这意味着对继承 Random 的自定义类型可能仍走旧实现路径。

实践建议：
- 单线程或临时使用场景，new Random 在现代 .NET 中是安全且可用的。
- 并发场景优先使用 Random.Shared，避免跨线程共享同一 Random 实例。
- 不要在紧密循环中无意义地频繁分配 Random；若确需多次调用，复用同一线程上下文中的实例或使用 Random.Shared 更合适。

3、 [理解 .NET 中最严重的漏洞](https://andrewlock.net/understanding-the-worst-dotnet-vulnerability-request-smuggling-and-cve-2025-55315/)

文章解析了微软于 2025-10-14 发布的 .NET 安全公告 CVE-2025-55315（CVSS 9.9），本质为 ASP.NET Core 中的 HTTP 请求走私（Request Smuggling）漏洞。请求走私源于代理与后端服务器对“无效/模糊”HTTP 报文解析不一致，导致代理认为是一条请求，而后端将其拆解为两条，从而绕过代理的安全策略。

该漏洞的特定成因是 HTTP/1.0/1.1 的 Transfer-Encoding: chunked 搭配“块扩展”（chunk extensions）解析的宽松处理。当块扩展行结尾使用非法的单独换行符（例如仅 \n 而非规范的 \r\n）时，不同实现对行终止的处理存在差异：有的把 \n 当作行结束，有的忽略直到找到 \r\n。攻击者可构造如“2;\n”这类畸形块头，让前端代理视为一条完整请求，而 Kestrel 则将后续数据分裂为第二条隐藏请求，实现“走私”。

利用面可因应用而异，常见影响包括：绕过身份或 CSRF 校验、伪造头部导致权限提升（例如注入 X-SSL-CLIENT-CN 冒充受信客户端）、访问本应被代理阻挡的内部端点（如 /admin）、缓存投毒、凭据或敏感数据外泄、SSRF 与注入等。即便没有显式代理，只要应用直接读取/转发请求流（如使用 HttpRequest.Body/BodyReader），也可能形成“系统间”解析不一致而受影响。

修复方式是取消宽松解析：Kestrel 现严格校验块扩展行结尾，若非 \r\n 则抛出 KestrelBadHttpRequestException 并返回 400。虽然提供了 AppContext 开关可回退旧行为，但不建议启用。漏洞仅影响 HTTP/1.0/1.1；HTTP/2/3 使用二进制分帧，不支持 chunked，因此不受此类问题影响。

缓解建议：尽快升级至已修补版本（.NET 8.0.21+、9.0.10+、10.0.0-rc2+；ASP.NET Core 2.3 on .NET Framework 需升级 Kestrel.Core 至 2.3.6）。自包含部署需重新发布。旧版本（<= .NET Core 3.0、3.1、.NET 5、.NET 6［除非由 HeroDevs 支持］、.NET 7）不再获补丁，应迁移至受支持版本。若暂无法升级，可在前置代理处阻断畸形请求（例如 Azure App Service 已在其 YARP 代理层修补）；也可强制仅允许 HTTP/2/3，但可能影响仍用 HTTP/1.1 的客户端。

自查方式：通过 dotnet --info 核对运行时版本；或发送带非法块扩展的测试请求，未修补的 Kestrel通常会“挂起等待”最终超时，修补后则立即返回 400。文中提示 IIS 可能同样受影响（非官方测试），使用其他代理/服务器时需向供应商确认防护状态。

4、 [Struct 中的 inline](https://steven-giesel.com/blogPost/e89d7156-f3fd-4152-b78a-cb908bc43226/inlining-and-structs-in-c)

这篇技术博文探讨了 C# 的内联（Inlining）与结构体（struct）在性能方面的关系，重点说明内联如何减少结构体按值传递时的复制开销，从而优化方法调用的成本。

内联是编译器/运行时优化，将方法调用替换为方法体本身，避免调用开销，但可能增加代码体积。C# 提供 MethodImpl 属性可用以提示 JIT：MethodImplOptions.AggressiveInlining 用于建议内联，MethodImplOptions.NoInlining 用于建议不内联。但这些仅是提示，JIT 可基于自身策略选择忽略。

结构体作为值类型，按值传递，调用方法时会复制整个结构体到新的栈帧。如果结构体较大，复制成本显著，因此通常建议结构体保持不可变并尽量小，以减轻复制开销。文章提出的关键点是：当方法被内联时，方法调用及其新的栈帧被“消除”，从而也就不需要把结构体复制到新栈帧，这使得传递结构体在内联场景下变得更便宜。

文中通过一个基准测试对比了内联与非内联的差异。示例定义了一个包含大量属性（A 到 Z）的结构体 SomeBigStruct，并分别对两个方法标注 NoInlining 与 AggressiveInlining，方法仅返回属性 F，再在基准中对该方法调用两次求和。结果显示：
- 非内联（NoInlining）：平均约 3.3646 ns；
- 内联（AggressiveInlining）：平均 0.0000 ns。
当减少结构体属性数量（例如仅 A 到 N）时，非内联的平均耗时降至约 2.2966 ns，而内联仍为 0.0000 ns。这表明非内联路径上的主要时间来自结构体复制，并且复制成本随结构体大小增加而上升。作者也强调该基准操作数过少，应谨慎解读，但趋势清晰。

进一步利用 sharplab.io 检视 JIT 汇编，非内联版本包含多次 vmovdqu/vmovq 把结构体块复制到栈并进行调用，指令数量明显；而内联版本仅有 mov、add、ret 等少数指令，直接读取属性并返回，直观显示复制被消除。

文章要点包括：
- 内联可避免结构体按值传递时的复制，降低调用成本；
- AggressiveInlining/NoInlining 是提示，最终由 JIT 决定；
- 结构体应倾向不可变且小型以减小复制开销；
- 可借助 BenchmarkDotNet 与 sharplab.io 验证行为与汇编输出；
- 微基准需谨慎，但示例清楚展示了内联对结构体传递的性能影响。

5、 [使用 Microsoft Agent Framework 构建 Agent](https://devblogs.microsoft.com/dotnet/upgrading-to-microsoft-agent-framework-in-your-dotnet-ai-chat-app/)

文章介绍如何将基于 .NET AI App Templates 的基础聊天应用升级为使用 Microsoft Agent Framework 的智能代理系统，从而支持多步推理、工具调用、上下文管理与多代理协作，并融入依赖注入、Middleware、Telemetry 等 .NET 常用模式。

- 背景与能力：Microsoft Agent Framework（预览）面向 .NET 的 AI 代理开发，支持使用工具与函数访问 API/数据库、跨会话维持上下文、基于指令与数据进行自主决策、以及多代理协调。框架基于 Microsoft.Extensions.AI，并与 DI、OpenTelemetry 无缝集成。

- 前置条件：.NET 9 SDK、VS/VS Code、Azure OpenAI（或 GitHub Models）、安装 .NET AI App Templates，理解 Blazor 与 AI 基本概念。

- 第一步（模板应用）：通过“AI Chat Web App”模板创建项目，结构包含 Web（Blazor UI）、AppHost（.NET Aspire 编排）与 ServiceDefaults。Program.cs 配置 Azure OpenAI 的聊天模型（如 gpt-4o-mini）与 Embeddings（text-embedding-3-small）、Sqlite 向量存储与 DataIngestor（启动时导入 wwwroot/Data 下的 PDF），并注入 SemanticSearch。初始 Chat.razor 直接使用 IChatClient 流式响应，同时在组件内定义 SearchAsync 作为工具函数，适用于入门但在关注点分离、测试与复杂编排方面受限。

- 第二步（引入 Agent Framework）：在 Web 项目添加 Microsoft.Agents.AI、Hosting、OpenAI 等包。将搜索功能封装为独立的 SearchFunctions 服务，并使用 [Description] 提供工具语义。通过 builder.AddAIAgent 注册键控代理“ChatAgent”，从 DI 获取 IChatClient 与 SearchFunctions，使用 chatClient.CreateAIAgent 配置代理的 name、instructions、description 与 tools（AIFunctionFactory.Create 绑定工具），开启 OpenTelemetry。更新 Chat.razor 改为注入 IServiceProvider，在 OnInitialized 中通过 GetRequiredKeyedService<AIAgent> 解析代理；将响应流逻辑替换为 aiAgent.RunStreamingAsync，实现由代理统一负责推理与工具调用。

- 第三步（运行与观测）：借助 .NET Aspire 进行服务发现、统一日志与健康检查；首次运行配置 Azure OpenAI 的订阅、资源与模型部署。测试过程中，代理会在需要时调用搜索工具，并返回带文件名与页码的引用；Aspire 仪表板可观测工具调用、参数与响应组合过程。

- 进阶场景：可扩展更多工具（如 WeatherFunctions），注册多代理（ResearchAgent、WritingAgent、CoordinatorAgent）并通过函数委派实现协作；支持自定义 Middleware 进行前后处理与日志。

- 最佳实践与性能：为工具撰写清晰的描述、针对工具与代理流程编写测试、通过应用洞察与 Aspire 监控令牌用量、调用模式与错误率；根据场景选择流式或非流式响应，优化工具调用条件。支持使用 azd 将应用部署到 Azure（含 OpenAI 资源、Container Apps、Application Insights）。总体提升了架构的可维护性、可测试性与可观测性，并便于多代理编排。

6、 [ .NET 安全小组](https://devblogs.microsoft.com/dotnet/announcing-dotnet-security-group/)

微软宣布成立“.NET 安全小组”（.NET Security Group），旨在与分发 .NET 的组织协作，在每月的补丁星期二同步发布安全修复，使尽可能多的 .NET 部署快速、可预测地完成安全更新。该举措延续并扩展了自 2016 年起与 Red Hat 等少量伙伴的私下合作，现公开并扩大对符合条件的发行方开放，成员包括 Canonical、IBM、Red Hat 和微软等。

核心目标是将漏洞信息提前共享给可信伙伴，使其在公开披露（CVE）前获取源代码补丁，提前构建、验证并发布二进制包，缩短从漏洞公开到各自发行版提供更新的时间，提升整个 .NET 生态的安全韧性。这体现了“上游开源项目”的理念，也与微软推动降低 .NET 构建成本的 dotnet/dotnet 仓库工作相呼应，便利各方以低成本分发安全修复。

微软强调，安全是 .NET 平台的基石，广泛应用于金融、医疗、政府等关键行业，细微漏洞也可能产生巨大影响。除微软官方发行版外，多个组织会从源代码构建并分发自己的 .NET 版本（涵盖多种 Linux 发行版及独立软件供应商）。随着一些组织提出获取其“生命周期终止”（EOL）业务相关补丁的诉求，微软决定公开该小组并明确其目标：成员须积极参与上游 .NET 项目并发布受支持版本的构建，以此展现对生态的承诺与可信度。

加入流程与预期：
- 申请：需填写 .NET Security Group 的入驻表单（Intake Form）。
- 审核与信任：基于所共享信息的敏感性质，将进行业务真实性与安全风险审核，并核对贸易制裁、禁止/关注名单；通常需数天至数周，且每年复审一次。
- 法务与签署：通过审核后签署项目协议，如未有保密协议（NDA）需一并签署。
- 入组与通告：完成入组后，成员将每月在公开披露前约一周收到涉及受支持 .NET 版本的 CVE 信息与源补丁，用于提前构建和发布更新。

结语：若您正在分发自有 .NET 版本且希望提升用户安全保障，可通过“.NET Security Group Application”申请加入该小组。作者为 .NET 团队的首席产品经理 Jamshed Damkewala。

7、 [使用 CancellationToken](https://steven-giesel.com/blogPost/080baaef-27d4-4d98-b0a8-9c3ab96c335e/use-cancellationtokens)

- 文章讨论在 API 中使用 CancellationToken 的重要性，强调其不仅影响本地代码的资源释放，还能让外部系统（如数据库、HTTP 服务）在请求被取消后主动终止操作，从而整体节省 CPU、内存与 IO 资源。
- 示例以一个长时间运行的 SQL Server 查询（对 sys.all_objects 做多次 CROSS JOIN）说明问题：当 REST 请求被客户端取消时，如果未传递取消令牌，后台 SQL 查询仍会继续执行。可通过查询 sys.dm_exec_requests（status = 'running'）验证，即使请求已中止，服务器端任务仍在运行。
- 如果为数据库操作传入 CancellationToken（例如使用 CancellationTokenSource 并将令牌传给 ExecuteSqlAsync），在触发取消后会抛出 OperationCanceledException，同时服务器端也会终止正在执行的查询。再次查看 sys.dm_exec_requests 可确认查询已停止。这体现了取消的双向效用：代码及时释放资源，外部系统也能响应取消。
- 类似地，HttpClient 支持传入 CancellationToken，取消后会中止请求，如果对端也是支持取消令牌的 .NET 后端，便可协同处理取消流程，实现端到端的资源节省和更优的可伸缩性。
- 不同外部系统对取消的支持程度可能存在差异。例如 SQLite 提供 sqlite3_interrupt，可被 C# 层（如 EF）利用，但具体实现和挂钩方式需视库而定。尽管支持不一，仍建议在调用链中普遍传递取消令牌，以便当前或未来的提供方能更好地处理取消。
- 关于“是否总是传入 CancellationToken”的问题，文章指出应谨慎使用，避免数据不一致。示例中将用户、地址、角色分步 SaveChangesAsync，如果在第二步被取消，会造成用户已创建但地址/角色未保存的状态不一致。
- 建议通过降低 SaveChangesAsync 次数（例如只在末尾统一保存）或使用事务，在最终提交时一次性保证一致性。对涉及第三方持久化的场景，需设计好补偿与一致性策略，确保请求取消不会导致系统进入无效状态。
- 总结：在支持的调用上尽量传递 CancellationToken，并确保上下游系统能正确处理取消，以提升资源利用与伸缩性。同时在数据写入路径中配合事务或统一提交，防止取消导致的部分写入与一致性问题。





## 开源项目
1、 [DotNET-GenAI](https://github.com/googleapis/dotnet-genai)

Google Gen AI .NET SDK（googleapis/dotnet-genai）为 .NET 开发者提供集成 Google 生成式模型的统一接口，支持 Gemini Developer API 与 Vertex AI 两类服务。库面向 .NET 8.0 与 netstandard2.1，并提供完整 API 参考（GitHub Pages）。

- 安装与命名空间
  - 通过命令安装：dotnet add package Google.GenAI
  - 使用命名空间：Google.GenAI 与 Google.GenAI.Types

- 客户端初始化
  - Gemini Developer API：使用 API Key 创建 Client；也可通过环境变量方式自动加载凭据。
  - Vertex AI：提供 project、location，并将 vertexAI 设为 true；亦支持通过环境变量配置使用 Vertex AI、项目与区域。
  - 也可在不显式传参时，直接 new Client() 使用环境变量完成初始化。

- 模型与类型
  - 模型推理能力经由 client.Models 暴露；模型字符串如 “gemini-2.0-flash” 等。
  - 常用参数与结构体位于 Google.GenAI.Types 命名空间。

- 文本/多模态生成
  - GenerateContentAsync：输入 contents，返回 Candidates/Content/Parts；文本通常位于 Parts[0].Text。
  - 可通过 GenerateContentConfig 调整输出，如：
    - SystemInstruction：为系统指令设定上下文与行为提示。
    - Temperature：控制随机性，越低越确定；MaxOutputTokens 限制输出长度。
    - SafetySettings：按 HarmCategory 配置拦截阈值（如仇恨言论）。
  - JSON 响应：通过 ResponseMimeType 设置为 application/json，并提供 ResponseSchema（Schema 类型）定义返回结构；建议不要在提示中重复模式示例，以免影响质量。
  - 流式生成：GenerateContentStreamAsync 支持异步流式返回增量片段，便于边生成边消费。

- 图像相关能力
  - 生成图片（GenerateImagesAsync）：示例模型 “imagen-3.0-generate-002”；支持 NumberOfImages、AspectRatio、SafetyFilterLevel、PersonGeneration、IncludeSafetyAttributes/IncludeRaiReason、OutputMimeType 等配置。
  - 超分（UpscaleImageAsync）：仅 Vertex AI 支持；可设置 upscaleFactor（如 x2）与 EnhanceInputImage。
  - 编辑图片（EditImageAsync）：仅 Vertex AI 支持；通过参考图像列表（RawReferenceImage/MaskReferenceImage）与 EditMode 配置，示例模型 “imagen-3.0-capability-001”。
  - 图像分割（SegmentImageAsync）：仅 Vertex AI 支持；配置 SegmentMode、MaxPredictions，返回 GeneratedMasks。

- 响应结构与处理
  - 文本生成：从 response.Candidates[0].Content.Parts[0].Text 读取。
  - 图像生成与处理：从 response.GeneratedImages.First().Image 获取生成结果。
  - 分割结果：从 response.GeneratedMasks.First().Mask 获取掩码。

- 文档参考
  - 详细模型能力与默认参数，请参阅 Vertex AI 与 Gemini API 官方文档。
  - 完整 SDK API 参考在项目的 GitHub Pages 提供。



