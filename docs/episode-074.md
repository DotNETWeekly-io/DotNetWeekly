.NET 每周分享第 74 期
## 行业资讯
1、 [JetBrain .NET Day](https://blog.jetbrains.com/dotnet/2025/09/18/announcing-jetbrains-net-days-online-2025/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1059.png)
JetBrains 宣布举办面向 .NET 社区的在线活动 “JetBrains .NET Days Online 2025”。活动为期两天，提供高密度的实践演讲、现场演示和实战技巧，由社区讲师与 JetBrains .NET 团队共同呈现。参会信息如下：
- 时间：2025 年 10 月 8–9 日
- 形式：在线直播，支持实时聊天互动
- 时区：中欧夏令时 CEST（UTC+2）；议程时间均为 CEST
- 费用：免费
- 回放：所有会话将提供点播
- 报名：lp.jetbrains.com/dotnet-days-2025

议程重点涵盖分布式系统、架构设计、语言特性、消息系统、跨平台、测试驱动开发、事件驱动版本化、生成式 AI 与 .NET 集成、函数式编程、性能与诊断、异步编程、云日志成本优化等主题，示例包括：
- 使用 .NET Aspire 构建分布式系统（Jason Taylor）
- ASP.NET Core 的整洁架构（Steve Smith）
- C# 可空性 #nullable enable 的实践与威力（Maarten Balliauw）
- 为架构选择合适的消息系统/队列（Poornima Nayar）
- .NET 应用跨平台（Uno Platform，Steve Bilogan）
- 基于 Blazor 的测试驱动开发（Scott Sauber）
- 事件驱动系统的版本化策略（Laila Bougria）
- 利用 Semantic Kernel 与 .NET Aspire 构建可扩展的 GenAI 应用（Mehdi Hadeli）
- F# 函数式编程：潮流还是价值（Ken Bonny）
- dotMemory 与 Akka.NET：快速定位每日 2 GB 泄漏（Aaron Stannard）
- C# 中的 async/await 深入讲解（Stefan Pölz）
- 低成本征服分布式云日志的分步指南（Stathis Fylaktos）
- GenAI 是否意味着 TDD 的终结（Johan Smarius）

如何参与：
- 在上述注册页面报名
- 按时线上观看并通过实时聊天与演讲者和参会者互动

关注渠道：
- 关注 @JetBrainsRider 与 @ReSharper 的 X/Twitter 账号，或持续关注 JetBrains .NET 官方博客获取更新

该活动适合 .NET 开发者、架构师、测试工程师与技术负责人，聚焦于可操作的最佳实践与工具链应用，支持回看以便深入学习与复盘。

2、 [.NET STS 调整为 24 个月](https://devblogs.microsoft.com/dotnet/dotnet-sts-releases-supported-for-24-months/)

- 微软将 .NET 标准期限支持（STS）从18个月延长至24个月，自 .NET 9 起生效。这意味着 .NET 9 的支持终止日期延至 2026-11-10，与 .NET 8（LTS）同日结束。长期支持（LTS）政策不变，仍为3年，或在下一代继任版本发布后12个月结束。

- 年度节奏与支持模型：
  - .NET 每年11月发布一个主版本；
  - 偶数版本为 LTS（3年支持），例如 .NET 8：2023-11-14 发布，最新补丁 8.0.20（2025-09-09），EoS：2026-11-10；
  - 奇数版本为 STS（现改为24个月支持），例如 .NET 9：2024-11-12 发布，最新补丁 9.0.9（2025-09-09），EoS：现为 2026-11-10（原计划为 2026-05-12）。

- 变更动机：
  - .NET 生态中越来越多的功能以 Out-of-Band（OOB，带外）形式发布，如 .NET Aspire、Microsoft.Extensions.AI、C# Dev Kit 等。这些 OOB 组件常依赖年度主线中的更新包版本。
  - 许多企业因合规或策略选择只用 LTS。如果它们安装某个 OOB 组件，而该组件依赖的包版本来自 STS（例如从 .NET 8 LTS 拉入 .NET 9 STS 的包），会导致运行时的部分组件转为 STS 生命周期管理，从而比 LTS 更早结束支持，产生风险。
  - 为避免上述“依赖错配”和 OOB 采用阻碍，将 STS 延长至24个月，使其与前一代 LTS 在同一天结束支持。这样，即使 OOB 引入了 STS 依赖，也不会比坚持使用 LTS 更早失去支持。

- 影响与收益：
  - 对坚持 LTS 的团队：引入 OOB 时的支持期不再缩短，降低了合规与维护风险。
  - 对愿意尝试 STS 的团队：更长的支持窗口（24个月）为评估与迁移提供充足缓冲。
  - .NET 8 与 .NET 9 将在 2026-11-10 同步结束支持，便于统一规划升级节奏。

- 注意事项：
  - .NET 家族并非所有组件与 .NET 主版本共享同一生命周期。一些组件以 OOB 形式独立发布，拥有独立的支持策略；本次变更不影响这些组件。具体请参阅 .NET 支持生命周期政策页面。
  - 若你已计划从 .NET 9 升级至 .NET 10，仍应按计划推进，.NET 10 将带来更多新能力与性能改进。



## 文章推荐
1、 [Unsafe 代码最佳实践](https://learn.microsoft.com/en-us/dotnet/standard/unsafe-code/best-practices)

该文面向在 C# 中编写或审阅 unsafe 代码的开发者，系统梳理了常见不可靠模式、其风险与缓解策略。核心前提是：GC 可在任意位置中断并搬移对象且更新所有受跟踪引用；任何让 GC“看不见”的指针或错误的引用形状都可能导致堆损坏、随机崩溃与难以重现的缺陷。

主要不可靠模式与建议：
- 非受跟踪托管指针：使用 Unsafe.AsPointer(ref T) 将 byref 转为原生指针会令 GC 无法更新该地址，导致悬空写。应使用 fixed/GCHandle 在明确范围内固定对象，并避免指针逃逸出 fixed 块。
- 依赖运行时内部细节：不要读写对象头、依赖填充（padding）内容、假设类型大小/偏移、反射访问非公开成员或修改 readonly/static readonly 字段。不要假设字符串/静态字段/LOH 等不可搬移；需用 fixed 保证正确性。
- 无效托管指针：byref 的值不仅影响用户代码，也会被 GC观察，创建越界/外部 byref 属于错误；需要指针算术时应改用已固定的原生指针。
- 类型重解释：Unsafe.As 在 struct↔struct/class↔class 的重解释可能让随机值被 GC当作引用。优先字段级复制或 Source Generator/AutoMapper；必要时用 Unsafe.BitCast 并理解其不提供完整正确性保证。
- 绕过写屏障与非原子操作：通过原生指针或按字节复制 Struct（含 GC 字段）会绕过写屏障且非原子，易引入可见性/撕裂读写问题。避免使用 CopyBlock/向量化对 GC 引用的批量操作。
- 对象生命周期假设：不要假设 this 或委托在方法末尾仍存活；在需要时使用 GC.KeepAlive 或 SafeHandle，并确保通过 Marshal.GetFunctionPointerForDelegate 的委托被保持存活。
- 跨线程访问局部变量（含 GC 引用）为未定义行为；改用堆或非托管内存。
- 手动移除边界检查：优先让 JIT消除；度量性能、在必要时用 Debug.Assert 保护。
- 访问合并与非对齐：现代 JIT 已优化安全写法（如 "False".CopyTo）。避免手工合并导致非原子或非对齐访问，必要时使用 Read/WriteUnaligned，并了解各平台代价/异常风险。
- 结构体的位序列化：含填充或非可移植成员（如 bool/GC字段）不要做按位复制/比较；仅对有限原始类型安全，建议字段级序列化或使用库。
- 空 byref 与编译器优化：避免创建空 byref；不要丢弃 byref 解引用结果。
- stackalloc：总是赋值给 Span/ReadOnlySpan 以获取边界检查；避免循环中使用并限制大小，对长度做范围校验。
- 固定大小缓冲区：用 InlineArray 取代 fixed buffer 获取更安全的边界检查与默认零初始化。
- 指针+长度/零终止的 API：用 Span/Memory/数组切片替代；P/Invoke 时传递显式长度或转为 string。
- 字符串不可变：不要通过 unsafe 修改字符串；使用 String.Create 等安全构造。
- 原始 IL 与 [SkipLocalsInit]/Unsafe.SkipInit：除非确有收益且理解风险，否则避免；ArrayPool 的 Rent/Return 要限定同一作用域以防“归还后仍使用”。
- 布尔与整数转换：用三元或比较，避免 Unsafe 重解释。
- 互操作与并发：优先使用 CsWin32/CsWinRT；记住内存安全与线程安全是正交的，SIMD 读写往往无边界/原子保证。
- 测试与告警：可用 SharpFuzz 做模糊测试；重视编译器警告，切勿因“无警告”而断言 unsafe 代码正确。

2、 [ .NET 生态的缺陷和优势](https://platform.uno/blog/the-gaps-and-richness-of-the-net-ecosystem/)

- 概览：现代 .NET 强大、开源、跨平台，工具成熟（含 AI），但在平台覆盖、视觉设计器、统一渲染、设计交付、UI 组件、架构模式与工具选择等方面存在缺口；微软鼓励生态伙伴填补。本文介绍 Uno Platform 与 Uno Platform Studio 如何面向这些痛点提供补全与增益。

- 平台覆盖：虽然 Blazor（Web）与 .NET MAUI（移动/桌面）可混合使用，跨平台共享代码的同质性与覆盖度仍有限。Uno Platform 采用单项目模式，统一支持 Windows、macOS、iOS、Android、Linux 与 WebAssembly，提供丰富的库、扩展与工具，加速跨平台应用的设计与开发。

- 视觉设计：WinUI/Xamarin/.NET MAUI 长期缺少跨平台可视化设计器，Hot Reload 难以满足复杂、数据绑定与多形态设备的设计需求。Uno Platform Studio 的 Hot Design 可将正在运行的 .NET 应用转化为设计器，设计时与运行时实时同步、可用真实数据调试，在不打断开发流程的情况下进行像素级调优，显著提升内环效率。

- UI 渲染：Uno 提供 Skia 与 Native 两种渲染模式。Skia 以硬件加速画布统一绘制（Metal/OpenGL/WebGL），在各平台确保一致外观和性能；Native 使用平台原生控件与 API，便于深度原生体验与集成。两者可按场景混用，兼顾一致性与原生特性。

- 设计到代码：Uno Platform Figma 插件结合 Material Toolkit，可一键将 Figma 设计高保真生成 C#/XAML 标记，支持迭代更新，降低设计-开发交接成本，提高设计实现一致性。

- UI 组件栈：自动兼容 WinUI、Windows Community Toolkit，以及面向 WinUI 2/3/UWP 的开源控件；支持嵌入 .NET MAUI 生态的主流第三方组件（如 Syncfusion、Telerik、DevExpress、Esri、GrapeCity 等）。Uno Toolkit 还提供面向多端响应式的高层控件，减少重复造轮子。

- 架构模式：内置传统 MVVM（双向绑定、依赖属性、导航）与现代 MVUX（单向不可变数据流，内置绑定），并提供样例与模板，便于团队根据规模与偏好选择、落地最佳实践。

- 工具与语言灵活性：支持 XAML 声明式标记与 C# 声明式 UI 混合使用；IDE 可自由选择 Visual Studio、VS Code、Rider，跨 Windows/macOS/Linux，并支持云端开发（GitHub Codespaces、GitPod）。

- 生态定位与价值：围绕“平台优先、伙伴优先”的策略，Uno 作为开源平台扩展 .NET 的平台到达与开发者体验，补齐 Linux 与浏览器等目标平台缺口，连通企业级工作流，提供更同质的共享代码与更高生产力，助力从移动、桌面到 Web/嵌入式的现代应用构建。

3、 [C# 中索引性能比较和内部实现](https://medium.com/@pavel.romash/indexers-in-c-performance-comparison-internals-0d88885ac780)

- 目标与方法：文章比较 C# 中常见索引器（数组、List<T>、Span<T>、多维数组、交错数组）的汇编实现与性能特征。在 x64 环境下给出成功路径指令序列，并用微操作数（uop）、融合微操作（fused uops）、延迟（latency）与倒数吞吐（reciprocal throughput）作为度量，不做基准测试以避免缓存命中/未命中造成波动。
- 数组索引器：访问 array[index] 时，先比较 index 与数组长度（长度位于对象头后 8 字节偏移处 [r8+8]），越界则抛异常。元素数据从 0x10 偏移处开始，int 为 4 字节，地址计算为 [r8+rax*4+0x10]。成功路径约 4 条指令，数据依赖强，但流程简单。
- List<T> 索引器：list[index] 先对 List 的 Count 做边界检查（第二字段，偏移 0x10），再取内部数组引用（第一字段，偏移 8），随后对内部数组再次做边界检查并取值。成功路径约 7 条指令，比数组多 3 条，来源于两次检查与一次解引用。
- Span<T> 索引器：Span<int> 为值类型，包含 length 与数据指针。span[index] 先检查长度（[r8+8]），再按指针直接偏移取值 [ptr+index*4]，无需对象头与数据对齐填充的额外偏移。成功路径约 5 条指令；标注 [Intrinsic]，编译器/运行时会进行特殊处理。
- 多维数组（[, ]）：支持非零下界。访问 arr[i,j] 时先用下界修正各维索引（sub），再分别检查与各维长度（cmp/jae）。数据为平铺存储，扁平索引计算为 i*len2 + j（imul/add），最终按 4 字节元素读取。指令较多，但内存连续。
- 交错数组（[][]）：外层为引用数组。步骤为先在外层按指针大小（8 字节）取内层数组引用，再在内层按元素大小（4 字节）取值；两次边界检查。由于内层数组分别分配，内存位置可能分散。
- 存储布局要点：托管数组对象头包含方法表指针（8 字节），长度字段紧随其后；数组数据从 0x10 开始。List<T> 第一字段为内部数组引用，第二字段为 Count。Span<T> 直接持有裸数据指针与长度，指令数少但仍需边界检查。
- 微架构指标解读：不同指令的微操作数、融合情况、延迟与吞吐不同，影响总体成本；强数据依赖使指令级并行受限。比对时不仅看指令条数，还需考虑指令类型与依赖链。
- 缓存与内存分配影响：多维数组连续内存有利于缓存局部性与顺序访问；交错数组内存分散可能降低局部性，但在对象生命周期短、避免进入 LOH、减少碎片整理（compacting）触发等场景可能更合适。
- 总结：数组与 Span<T> 成本最低；List<T> 由于包装数组存在额外检查与解引用；多维数组指令更多但具备优良的连续性；交错数组适合特定分配/生命周期策略。实际性能仍依赖访问模式与缓存行为。

4、 [Microsoft Agent Framework](https://devblogs.microsoft.com/dotnet/introducing-microsoft-agent-framework-preview/)

微软发布 Microsoft Agent Framework 预览版，这是面向 .NET 开发者的代理开发框架，旨在大幅简化从单代理到多代理工作流的构建、托管、监控与评估。文中首先界定“代理”和“工作流”：代理是为达成目标的系统，具备推理决策（LLM、搜索、规划）、工具使用（MCP、代码执行、外部 API）和上下文感知（聊天历史、知识库等）；工作流则将复杂目标拆解为可执行步骤，并可由多代理协作加速。

核心能力与基础：
- 基于 Semantic Kernel（编排）、AutoGen（多代理协作与研究技术）、Microsoft.Extensions.AI（统一 AI 构件），提供可靠灵活的 API。
- 通过 AIAgent 抽象与 IChatClient 标准接口，几乎可无缝切换模型提供方（OpenAI、Azure OpenAI、Foundry Local、Ollama、GitHub Models 等），并兼容 Foundry Agents、OpenAI Assistants、Copilot Studio 等生态。

入门与示例：
- 先决条件：.NET 9 SDK 及具备 models 权限的 GitHub PAT，设置环境变量 GITHUB_TOKEN。
- 安装包：Microsoft.Agents.AI（预览）、OpenAI、Microsoft.Extensions.AI.OpenAI、Microsoft.Extensions.AI。
- 快速创建“写作代理”ChatClientAgent，通过 IChatClient 调用 GitHub Models（如 gpt-4o-mini），几行代码即可生成短篇故事。

多代理与工作流：
- 通过 Microsoft.Agents.Workflows 包，将“写作代理”与“编辑代理”顺序编排为单一工作流代理对外服务。
- 支持多种工作流模式：顺序、并行、接力（handoff）、群聊（GroupChat，含轮询管理）。
- 典型场景包括研究管线、内容生产线、客服流程等。

工具与行动：
- 借助 AIFunctionFactory 将外部函数/服务注入代理（如格式化内容、获取作者），并可扩展至 MCP 服务器、托管工具（Code Interpreter、Bing Grounding 等），实现数据库/搜索/第三方服务访问。

托管与部署：
- 与 ASP.NET Minimal API、依赖注入、配置、Middleware 原生集成；通过 AddAIAgent 注册代理、在路由中编排工作流并暴露 REST 端点。
- 无需新部署模型，按常规 .NET 应用方式部署，适用于任何 .NET 运行环境。

监控与可观测性：
- 一行启用 OpenTelemetry（WithOpenTelemetry），采集会话流、模型使用（令牌与成本）、性能指标与错误。
- 与 Aspire、Azure Monitor、Grafana 等平台集成，支持敏感遥测数据以丰富仪表盘。

评估与质量保障：
- 集成 Microsoft.Extensions.AI.Evaluations，实现自动化测试、质量指标、回归检测与 A/B 测试，纳入 CI/CD。

关键点：简单易用、可渐进扩展、建立在成熟技术之上并面向生产可用，帮助 .NET 开发者快速构建、编排、托管与优化 AI 代理系统。

5、 [更加安全的方式发布 NuGet 包](https://devblogs.microsoft.com/dotnet/enhanced-security-is-here-with-the-new-trust-publishing-on-nuget-org/)

文章宣布在 nuget.org 引入“Trusted Publishing（受信发布）”，为使用 GitHub Actions 发布 NuGet 包提供更简单、更安全的方式。核心变化是用 GitHub OIDC 短期令牌换取一次性、短时有效（约 1 小时）的临时 NuGet API Key，取代需要存储、轮换且易泄漏的长期密钥。每个工作流作业的 OIDC 令牌对应一把临时密钥，做到按需签发、最小暴露面。

主要优势：
- 无长期敏感凭据：仓库或 CI 不再存储持久化 API Key。
- 短期凭据：密钥即取即用、快速过期，降低泄漏风险。
- 一作业一密钥：令牌与临时密钥严格绑定，发布最小化授权。

使用步骤：
- 在 nuget.org 登录后进入“Trusted Publishing”页面，创建策略，指定包所有者（用户或组织）、GitHub 仓库所有者与仓库名、工作流文件路径（如 .github/workflows/release.yml），可选配置环境（GitHub Actions environments）。
- 在 GitHub Actions 工作流中启用 OIDC（permissions: id-token: write），使用 NuGet/login@v1 将 OIDC 令牌交换为临时 API Key，并在 dotnet nuget push 中引用该输出。推荐使用 nuget.org 用户名（Profile 名称）而非邮箱，并通过机密变量传入（如 secrets.NUGET_USER）。

工作原理：
- GitHub 为作业签发 OIDC 令牌。
- NuGet/login@v1 将令牌发送至 nuget.org。
- nuget.org 按策略验证令牌，返回临时 API Key。
- 随后立刻使用该密钥执行 dotnet nuget push（因密钥约 1 小时过期）。

策略生命周期与归属：
- 私有仓库引导期为 7 天：新策略默认临时激活 7 天，首次成功“NuGet 登录”（令牌换密钥）后策略永久激活，并与不可变的 GitHub 标识绑定。若错过窗口，可在页面手动重新激活再试。
- 策略归属与适用范围：策略由用户或组织拥有，仅适用于其拥有的包。
- 组织状态变更：若创建者失去组织成员身份，或组织被锁定/删除，策略会禁用并提示；恢复访问后自动重新激活。

迁移指引：
- 已用 GitHub Actions 发布的项目，可创建受信发布策略、移除仓库或 CI 中存储的长期 NuGet API Key，添加 NuGet/login@v1 并使用其输出密钥执行发布，即完成迁移，免去后续密钥管理。

该功能遵循 OpenSSF 的 Trusted Publishing 指南，旨在在更广泛生态中推动安全、低摩擦的包发布体验。

6、 [运行时表达式树](https://blog.elmah.io/expression-trees-in-c-building-dynamic-linq-queries-at-runtime/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1052.png)
表达式树（Expression Trees）是 C# 中用于以树状结构表示代码的不可变数据结构，每个节点可代表常量、参数、二元运算、方法调用等。它们允许在运行时构建和组合代码，尤其适合基于用户输入动态生成 LINQ 查询。诸如 Entity Framework 等 LINQ 提供程序会解析表达式树，将其翻译为 SQL，从而实现查询的延迟执行与可移植性。

主要用例包括：
- 动态 LINQ 提供程序：将表达式树翻译为底层查询（如 SQL）。
- 运行时代码生成：把表达式树的 Lambda 编译为委托以执行。
- 元编程与代码检查：在运行时分析代码结构。
- 构建动态查询：根据不同条件生成表达式谓词，适合搜索过滤器和复杂查询。

示例一展示了运行时加法：构建 ParameterExpression（输入参数）、ConstantExpression（常量 5）、BinaryExpression（加法），再创建 Expression<Func<int,int>> 并 Compile 成委托，执行时传入 10 返回结果。该过程体现了表达式树如何逐步拼装并转为 IL 执行。

示例二演示按任意属性动态过滤 Building 列表：通过 Expression.Parameter 指定输入类型，Expression.Property 按字符串属性名获取属性，Expression.Constant 指定比较值，Expression.Equal 构造比较体，最后生成 Expression<Func<Building,bool>> 用于 AsQueryable().Where(lambda)。更换 propertyName 和 filterValue，即可在运行时按不同字段过滤。需要注意：若对表达式树执行 Compile，则其结果仅能在内存中执行，无法被 EF 转换为 SQL；要进行服务器端翻译，应直接传递未编译的表达式树给提供程序。

优势：
- 构建自定义查询生成器，适配业务特定的复杂条件。
- 降低为多条件查询编写大量样板代码的复杂度。
- 更易读的动态 LINQ 语法，替代手工拼装表达式树。
- 可借助 Dynamic LINQ 库以字符串表达式简化使用。

局限：
- 不支持所有 C# 结构（如循环、goto、try/catch、插值字符串、UTF-8 字面量、元组比较等）。
- 不可变：修改需重建树。
- Compile 有额外编译开销；委托执行虽快但通常略慢于纯 C#。
- 编译后的表达式不可被 LINQ 提供程序翻译为 SQL。

最佳实践：
- 仅在确有动态需求时使用，静态查询用常规 LINQ 更简单。
- 缓存已编译委托以复用，避免重复编译开销。
- 将复杂树拆分为可复用的构建器方法，提升可维护性。
- 使用 System.Linq.Dynamic.Core 等库减少手工构建负担。

7、 [.NET 10 GC](https://maoni0.medium.com/preparing-for-the-net-10-gc-88718b261ef2)

.NET 9 默认启用了 DATAS，但由于 .NET 9 非 LTS，很多用户将在 .NET 10 首次体验该 GC 功能。与多数“开箱即用”的 GC优化不同，DATAS 更具用户可见性，可能改变应用的内存和吞吐特性，因此需要在升级前做准备与评估。

DATAS（Dynamic Adaptation To Application Size）针对两类场景：其一是在内存受限（如容器/k8s）且负载具突发性的服务，通过在非峰值时主动缩小堆、峰值时适度增长，使堆大小更可预测，有助于设置合理的内存 request/limit 并利用 HPA；其二是小型工作负载使用 Server GC 时，避免因堆数=核心数而出现不成比例的堆膨胀，让堆规模与应用实际需求更匹配。

传统 Server GC主要依据各代的存活率触发 GC，不会适配应用规模，并且堆数与可用核心数绑定，导致不同核心数下同一应用的堆大小差异显著。DATAS 则以应用的 LDS（Live Data Size，近似为老年代占用减碎片）为参照，在不同核心数下保持相近的堆规模。对于仅因 Server GC 内存过大而选 Workstation GC 的场景，DATAS 可能更优；若工作负载本身不需要并行收集，继续使用 Workstation GC 即可。

DATAS的核心机制包括：通过 BCD（Budget Computed via DATAS）为 gen0 设定上限预算，以 LDS 为函数并带上下限夹持；通过目标 TCP（Throughput Cost Percentage，默认 2%，可用 GCDTargetTCP 配置）维持合理的吞吐成本，负载变轻时自动降低预算与堆大小。保守内存（conserve memory）策略用于调节全堆收集触发与老年代预算。DATAS启动时从 1 个堆开始，随负载适配堆数，无需用户手动设置 GCHeapCount。

不适用或需谨慎的场景：
- 无法或不打算利用释放的内存（可用 GCDynamicAdaptationMode 关闭 DATAS）。
- 启动性能至关重要（DATAS 从 1 堆扩容会影响启动阶段吞吐）。
- 完全不容忍吞吐回退；或主要进行 gen2 GC（常见于临时大对象过多）。

调优与配置要点：
- 通过 GCDGen0GrowthPercent、GCDGen0GrowthMinFactor 增大 BCD，使 gen0 预算更接近无 DATAS 的水平；目标 TCP 可用 GCDTargetTCP 调整。
- 去除固定 GCHeapCount 以启用 DATAS，并根据需要保留 GCNoAffinitize。
- 通过 PerfView/TraceEvent 近似观察 TCP（% Pause Time）和 gen0 预算（Gen0 Alloc MB）；DATAS 的 SizeAdaptationTuning 事件可程序化读取 TotalSOHStableSize（LDS）与 TcpToConsider（TCP）。

8、 [使用 Copilot Profiler Agent 优化 .NET 应用程序](https://devblogs.microsoft.com/visualstudio/copilot-profiler-agent-visual-studio/)

微软宣布在 Visual Studio 2026 Insiders 中推出 Copilot Profiler Agent，这是一款与 GitHub Copilot 深度集成的 AI 性能助手，旨在让性能诊断不再停留于繁杂的调用树与难懂的数值，而是以“问题-建议-验证”的闭环形式，快速定位瓶颈、提出可执行优化并自动验证成效。

该代理的核心能力包括：
- 分析 CPU 使用、.NET 对象分配与运行时行为，聚焦最昂贵的性能热点；
- 生成或优化 BenchmarkDotNet 基准测试，量化问题与改进；
- 提出可直接落地的性能优化建议，并辅助应用代码更改；
- 通过前后对比指标验证修复效果，形成引导式优化循环；
- 与 Copilot Chat 无缝配合，可通过 @profiler 或自然语言发问触发（需在 Copilot Chat 工具菜单启用）。

文中演示展示了在 SharpZipLib 上的实际流程：代理自动运行既有基准、指出瓶颈、指导并应用有针对性的修复，再次运行基准以验证改进，最终获得可量化的性能提升。为验证通用性，团队将代理对准最常用的前 100 个开源库和应用，成果包括：发现隐藏瓶颈、给出切实可行的改进建议、自动生成基准进行验证，并将优化以 PR 形式回馈到真实项目（如 CSVHelper、NLog、Serilog 等），获得维护者正向反馈。

在微软内部“自举”使用中，代理同样带来启发式优化。例如某团队在为包裹字典的类增加 IEnumerable 支持后出现内存与时间代价上升，经与代理多轮迭代后发现无需实现 IEnumerable，只需暴露 GetEnumerator 并转发给内部字典（利用 .NET 对 foreach 的“鸭子类型”支持），即可显著降低分配与开销。这类针对分配优化的洞察，帮助工程师快速发现非直觉性的改进点。

当前，Copilot Profiler Agent 已支持高 CPU 使用分析与 .NET 对象分配/内存使用分析，更多能力即将推出。适用场景涵盖游戏引擎调优、服务端性能优化、桌面 UI 加速等。开发者可在最新的 Visual Studio 2026 Insiders 中体验，并通过调查反馈使用结果，以推动工具持续改进。



## 视频推荐
1、 [为什么创业公司不选择 .NET 或者 C#](https://www.youtube.com/watch?v=lmhFfOM4LXw)

该视频讨论了为什么初创公司和小型企业通常不选择 .NET 作为其技术栈，尽管现代 .NET 非常优秀。作者 Nick Chapsas 分享了他的观察和分析，并加入了自己作为使用 .NET 的初创公司创始人的见解。

1. 初创公司常用的技术栈

初创公司通常倾向于选择以下技术栈：
* **MERN 堆栈**：MongoDB、Express、React 和 Node.js（或任何相关的 JS/TypeScript）。
* **Python 生态**：FastAPI、Django 和 Flask。
* **其他**：GoLang 和 Ruby on Rails 较少，Java (Spring/Quarkus) 和 .NET 则更为罕见。

2. .NET 未被广泛使用的原因分析

 历史污名 (Stigma)
* .NET 仍带有旧版本的历史污名，即与“遗留系统”（legacy）和“厂商锁定”（vendor locked）相关联 。
* 只要老一辈的高级或首席开发者持续传播这种负面看法，这种刻板印象就难以消除，即使 Aspire 等新项目试图改善形象 。

成本与招聘难度
* 许多初创公司错误地认为 Python 和 JavaScript/TypeScript 的开发者更便宜 。
    * **初级开发者**：JS/TypeScript 的初级开发者确实更便宜 。
    * **高级开发者**：但优秀的高级前端开发者（如 React）和 Python 开发者（由于 AI 爆炸性增长）的薪资通常比同等技能水平的 C# 开发者更高。
* **招聘难度**：C# 开发者在人才市场上相对较少，导致招聘难度客观上更大。市场上 JS/TypeScript 开发者数量遥遥领先 。

追求产品交付而非代码美学
* 对于初创公司来说，**产品本身是最重要的**，而不是代码架构（如整洁架构、DDD）。
* 初创公司需要快速推出 MVP（最小可行产品）以验证价值，往往将产品交付的优先级置于代码质量之上。
* 开发者可能会抱怨 AI 编写的代码缺乏“艺术性”，但企业只关心产品能否正常工作和销售 。

快速原型开发能力
* 在快速构建原型方面，JS/Python/Django/Express 等技术仍然比 .NET 更快，即使 .NET Minimal APIs 正在追赶，但它们已经在 JS/Python 生态中存在多年。

3. AI 时代对 .NET 的影响

* **AI 编码支持**：目前，AI 模型对 JS 和 Python 的支持更好。如果使用 Razor Page 或 Blazor 等 .NET 技术进行 AI 辅助编码（Vibe Code），通常需要更多的解释和手动调整，而 JS/Python 则更容易得到快速、良好的结果 。
* **微软的 AI 战略**：微软已将 .NET 从 Azure 部门转移到新的**核心 AI 平台和工具**部门之下，表明未来 .NET 将深度整合 AI 功能 。作者认为，对于初创公司来说，这种深度 AI 集成将是一个重要的差异化因素 。

4. 结论与建议

* **.NET 是一个好的技术栈吗？**：
    * **是**，如果你本身就熟悉它 。
    * **否**，如果你是一个有新想法且不了解 .NET 的开发者，你可能会选择 JS 或 Python 来更快地推出 MVP，证明产品价值 。
* **建议**：开发者不应该有“隧道视野”（tunnel vision），应根据公司当前阶段的目标来选择最合适的工具，不要沉迷于架构复杂性。

2、 [.NET Event Framework 取消](https://www.youtube.com/watch?v=c8Q1qA1kyO0)

这个视频讨论了微软决定取消原计划在 .NET 9 或 .NET 10 中推出的事件处理框架（Eventing Framework），作者对此表示非常失望。

1. 事件处理框架的提出与取消
初始构想：2024 年 1 月，微软 .NET Core 团队提出要为 .NET 9 创建一个事件处理框架，用于处理来自各种消息队列提供商（如 Service Bus、Kafka、Kinesis，甚至 Cosmos DB 变更 Feed）的消息。

官方取消：原计划在 .NET 9/10 中发布，但微软最终决定取消这个独立的事件处理框架，并于近期正式宣布停止推进。

2. 社区争议与官方解释
社区担忧：许多现有的消息处理库（如 MassTransit、Wolverine）的开发者对此表示不满，认为 .NET 已经是“自带电池”的框架，官方的介入会扼杀生态系统和开发者选择。历史上，微软推出官方内置功能（如 DI、日志）曾导致流行的开源库被商业化或被边缘化。

微软理由：微软在取消公告中表示，他们听取了社区的反馈，认为提出的担忧，特别是关于功能重复、范围蔓延和对生态系统影响的担忧是有效的，并得出结论“不会增加价值，反而会造成破坏”。

3. 作者的观点与质疑
不相信官方理由：作者对微软“不破坏生态”的说辞表示高度质疑，因为微软过去十年一直在通过内置功能来取代或影响社区库（例如 Minimal APIs 取代 Carter，内置缓存取代 Fusion Cache）。

真正的推测原因：作者认为，真正的理由是时间不足，因为微软正在将大量精力和资源集中投入到 .NET Aspire 项目中。他认为取消项目是微软对外展示“善意”的一种公关行为。

总结：作者对于错过微软可能带来的新设计感到遗憾，因为消息处理是几乎每个开发者都会用到的核心概念。





