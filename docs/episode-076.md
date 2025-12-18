.NET 每周分享第 76 期
## 行业资讯
1、 [.NET Conf 2025 回顾](https://devblogs.microsoft.com/dotnet/dotnet-conf-2025-recap/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1092.png)
.NET Conf 2025 活动回顾总结
.NET Conf 2025 圆满落幕，本次大会正式发布了 .NET 10 和 Visual Studio 2026，展示了 .NET 生态系统在 AI、云原生和性能方面的重大进展。

1. 重磅发布
.NET 10 (LTS)：正式发布的长期支持版本（支持至 2028 年 11 月），在性能、安全性和开发效率上达到了新高度。

Visual Studio 2026：采用全新的 FluentUI 设计，提供更现代的视觉体验。集成了 GitHub Copilot 作为 AI 结对编程助手，并优化了 Hot Reload 和 Razor 编辑体验。

.NET Aspire 13：从单纯的 .NET 扩展为支持多语言（如 Python、JavaScript）的云原生分布式应用框架。

2. AI 核心能力
Microsoft Agent Framework：进入公开预览阶段，帮助开发者构建能够自主决策、执行任务并进行自然语言交互的智能代理。

GitHub Copilot 升级：

应用现代化：利用 AI 自动将旧版 .NET 应用迁移至 .NET 10 及其最新特性。

AI 测试增强：自动生成单元测试并修复失败的测试用例。

MCP (Model Context Protocol) C# SDK：支持该开放协议，使 AI 代理能轻松连接外部工具和数据源。

3. 开发语言新特性
C# 14：引入了字段支持属性（Field-backed properties）、扩展属性、增强的 Span<T> 转换以及分部属性和构造函数。

F# 10：重点优化了代码清晰度、一致性和并行编译性能。

4. 性能与安全
极致性能：JIT 编译器增强（更好的内联和去虚拟化）、支持 AVX10.2 和 Arm64 SVE 硬件加速、NativeAOT 产物更小更快。

安全防御：新增对后量子加密（Post-Quantum Cryptography）的支持，通过 CNG 应对未来计算威胁。

5. 社区与生态
.NET 势头强劲：全球每月有超过 700 万活跃开发者使用 VS 系列产品；.NET 10 开发过程中合并了超过 2.3 万个社区 PR。

合作伙伴：与 Intel、AMD、Arm、Samsung、Red Hat 及 Canonical 等硬件和系统厂商深度合作，优化底层运行效率。

全球影响力：大会提供了针对中国开发者的微信/Bilibili 本地化转播及中文字幕。

2、 [VS 2026 正式发布](https://devblogs.microsoft.com/visualstudio/visual-studio-2026-is-here-faster-smarter-and-a-hit-with-early-adopters/)

- 微软宣布 Visual Studio 2026 正式发布，基于从 Insiders 频道收集的大量开发者反馈打造。本次周期中修复了超过 5,000 个用户报告的缺陷，并实现了 300 项功能请求，参与预览的开发者数量创历史新高。

- 性能全面提升：启动更快，大型 .NET 解决方案加载到可交互的时间显著缩短；在加载过程中界面保持更高响应性，UI 卡顿时间相较 VS 2022 减少超过 50%。这些优化使大规模项目和企业级仓库的日常开发更加流畅。

- AI 原生的“智能开发环境”：在调试、性能分析和应用现代化等关键场景中，AI 提供上下文相关的辅助与洞察，旨在减少摩擦、提升效率而不改变既有工作方式。新增 C# 与 C++ 智能代理，面向专业开发者的高精度与高效率需求，拓展能力同时保持可控性。

- GitHub Copilot 深度集成：持续成为 VS 中最常用特性之一。Profiler Agent 可自动发现性能瓶颈并引导优化，提升诊断与调优速度。借助 Copilot 的应用现代化能力，加速迁移至 .NET 10 及最新 C++ 构建工具；Copilot 的 C++ 新能力现处于私有预览。

- 体验与生产力改进：引入更灵活的设置系统、现代化的 UI 重设计，以及大量细节打磨与“纸切口”修复，整体使用体验更顺畅。新增对 Mermaid 图的支持，便于在 IDE 内表达架构与流程。

- 兼容性与扩展生态：与 VS 2022 的项目与扩展完全兼容，超 4,000 个现有扩展可直接使用，无需迁移。VS 2026 将 IDE 与构建工具解耦，允许独立更新 IDE 而不影响 .NET/C++ 工具链；同时提供按月自动更新带来持续的特性与设计改进。Insiders 频道可与稳定版并行安装，便于抢先体验新功能。

- 获取与授权：VS 订阅用户登录后自动激活；使用产品密钥的订阅者可在 my.visualstudio.com 获取。独立的 Professional 许可证将于 2025 年 12 月 1 日在 Microsoft Store 提供。更多详情可查阅 Release Notes，并在 Visual Studio 开发者社区反馈使用体验。

3、 [.NET 10 发布](https://devblogs.microsoft.com/dotnet/announcing-dotnet-10/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1083.png)
- 微软发布 .NET 10（LTS），支持至 2028-11-10，强调统一平台、AI 赋能与端到端性能/安全改进；同步提供 Visual Studio 2026 与 C# Dev Kit 更新。
- 性能：JIT 优化（内联、去虚拟化、结构体参数代码生成）、硬件加速（AVX10.2、Arm64 SVE），GC 写屏障改进降低暂停；NativeAOT 体积与启动更优。
- 语言：C# 14 引入字段托管属性、扩展属性/方法、Span<T> 转换、?.=、lambda 参数修饰、集合表达式扩展、部分属性/构造、ref struct 接口实现；F# 10 提升清晰性与性能，支持并行编译预览、and! 并发等待、尾调用优化、可控警告与访问器修饰。
- 库与安全：后量子密码学（ML-DSA/ML-KEM/Composite/HashML-DSA），网络（WebSocketStream、macOS TLS 1.3）、JSON 反序列化防重复、AES KeyWrap with Padding 等。
- Aspire 13：简化 AppHost/模板与 CLI，前端静态站点、并行部署、证书与连接字符串一致化；多语言编排与容器工作流增强，Dashboard 支持 OIDC。
- AI：Microsoft Agent Framework 统一多智能体模式（顺序/并发/交接/群聊/Magentic），提供 Dev UI 与 AG-UI 协议、ASP.NET Core 模板；Microsoft.Extensions.AI/VectorData 提供统一 IChatClient 抽象与遥测；MCP 首等支持与服务器模板便捷扩展工具/服务。
- ASP.NET Core/Blazor：内存池自动驱逐、Passkey（WebAuthn/FIDO2）、AOT 模板增强；Blazor 状态持久化、断线恢复与电路暂停/恢复，脚本与资源加载优化、响应默认流式、源生成表单验证、JS 互操作与 E2E 测试增强；API 生成 OpenAPI 3.1（含 YAML、XML 注释），内置验证、SSE、错误响应统一；新增度量与诊断工具。
- .NET MAUI：更新至 Android 16/iOS 26 绑定与启动性能；HybridWebView 初始化/JS 异常、请求拦截，MediaPicker 多选与压缩，SafeArea 改进；XAML 全局命名空间与源生成器提速；Aspire 集成与布局诊断。
- EF Core 10：SQL Server/Azure SQL 支持向量数据与距离函数、原生 JSON 类型；复杂类型可空/JSON 映射/struct 支持，ExecuteUpdate 直接更新 JSON；LINQ 新 LeftJoin/RightJoin、参数集合优化、命名过滤器与日志安全。
- 桌面与工具链：WinForms/WPF/WinUI 3 质量与性能提升；Visual Studio 2026 深度集成 Copilot（自适应粘贴、Profiler/Debugger 代理、基准模板等）与现代化体验；C# Dev Kit 提供 SLNX、Razor 编辑与覆盖率；SDK CLI 增强（统一测试平台、补全、容器镜像、dnx、--cli-schema、平台化工具、SLNX）；NuGet 默认审计、漏洞自动修复。
- 支持策略：.NET 10 为 LTS（三年）；下载、文档与 .NET Conf 2025 会话已上线，建议生产环境升级以获性能与安全红利。



## 文章推荐
1、 [C# 13 中的 OverloadResolutionPriority Attribute](https://www.meziantou.net/understanding-overloadresolutionpriority-attribute-in-csharp-13.htm)

C# 13 新增 [OverloadResolutionPriority] 特性，用于影响编译器在重载解析时的选择优先级。该特性主要面向库作者，帮助在保持二进制兼容的同时引入新的重载或行为。默认情况下，编译器会优先选择与调用点“最匹配”的签名，例如无参数重载会优先于带默认参数的重载，这可能阻碍新 API 行为的生效。

- 背景与问题：当现有方法 Test() 需要演进为 Test([CallerMemberName] string name = "") 时，直接修改签名会造成二进制破坏；添加一个新重载虽然避免破坏，但编译器仍会选择旧的无参版本，使新重载无法被调用。

- 解决方案：通过在旧方法上标注 [OverloadResolutionPriority(-1)] 降低其优先级，编译器将在兼容的前提下选择优先级更高（数值更大）的重载，从而在不破坏既有调用的情况下，让新行为生效。优先级默认为 0，数值越高越优先，越低则越不优先。

- 隐式转换场景：当存在需要隐式转换的重载（如 byte 与 int）时，可提升某个重载的优先级以引导编译器做出更合理的选择。例如为 byte 重载设置优先级 1，使 Test(1) 走 byte，而 Test(1000) 保持为 int，避免不必要的转换或错误选择。

- 作用范围限制：该特性仅在同一类内的方法、构造函数、属性之间生效；无法跨类影响扩展方法之间的优先级（例如不同扩展方法宿主类之间的竞争不受此特性控制）。

- Span/ReadOnlySpan 用例：下一个版本的 C# 将把 Span<T> 视为一等公民，允许 ReadOnlySpan<T> 的扩展方法被 Span<T> 直接调用。为减少中间转换与调用间接，可通过将 Span 重载设为较低优先级、优先选择 ReadOnlySpan 重载，提升 JIT、内联与裁剪效果，并避免冗余的桥接调用。

- 强制调用低优先级重载：当需要明确调用较低优先级的重载，可借助中间委托进行签名匹配，例如 ((Action)Sample.Foo)() 可强制调用 Foo()，否则编译器可能选择优先级更高的 Foo(int a = 0)。也可通过反射或 UnsafeAccessor 达成类似目的。

- 可用性与兼容：该特性在 .NET 9 中提供，适用于方法、构造函数、属性。如果目标框架低于 .NET 9，可使用 Polyfill（如 NuGet 包 Meziantou.Polyfill）或自定义实现（位于 System.Runtime.CompilerServices 命名空间的 Attribute），保证源码兼容并为未来升级做好准备。

总体而言，[OverloadResolutionPriority] 为重载解析提供显式、可维护的控制手段，在库演进、API 设计及性能优化（尤其 Span/ReadOnlySpan 相关场景）中具有实用价值。

2、 [使用 Zed 编辑器编写 C# 代码](https://andrewlock.net/trying-out-the-zed-editor-on-windows-for-dotnet-and-markdown/)

本文记录作者在 Windows 上试用 Zed 编辑器以取代 VS Code 处理 .NET 与 Markdown 的体验与结论。动机：VS Code 变得不够“轻快”，启动与切换不再敏捷，且 C# Dev Kit 经常在项目中生成 sln 文件、引入解决方案视图等“类 IDE”体验；作者希望有一个启动快、编辑流畅、适合小型 .NET 实验和优良 Markdown 写作的工具。

Zed 概览与上手：Zed 由 Rust 编写，强调性能；2025 年 10 月推出 Windows 版本。安装简便，可导入 VS Code 设置和键位。首次体验显示：窗口与文件打开、标签切换、工作区切换、输入响应均非常顺滑，显著快于 VS Code。

.NET 支持：
- 通过扩展安装 fminkowski 的 C# 插件（基于 OmniSharp LSP），获得语法高亮与基本重构/代码操作，接近早期 VS Code C# 体验。
- 主要缺口：无 Razor/cshtml 支持（OmniSharp 局限，混合语言解析困难）。对一般编辑影响不大，但需知悉。
- 主题与图标可选（如 JetBrains Rider 主题），但图标对 C# 不够完善。

Markdown 支持：
- 内置语法高亮与预览，但功能相对“骨感”。对比 VS Code + Markdown All in One、Spell Right、Word Count 等生态，缺失：
  - 快捷键（如 Ctrl+B 加粗当前词）。
  - 标题/段落级别折叠。
  - 内链与图片的智能提示/插入快捷方式，图片预览及与源文档的联动滚动。
  - 写作时频繁的词建议弹窗，干扰较大。
- 可用 CodeBook 做拼写检查，但整体体验仍弱于 VS Code。

快捷键与设置：
- 虽可导入 VS Code 键位，但多处肌肉记忆快捷键需手动映射（多光标、格式化、查引用等）。
- 可通过 JSON 设置关闭 AI（disable_ai: true）、自定义保存行为、主题、最小地图等；对不需要 AI 的用户较友好。

结论：Zed 性能与打磨出色，.NET 基本可用但缺少 Razor；Markdown 功能短板是当前无法替代 VS Code 的主因。若后续补齐 Markdown 关键功能，作者愿考虑迁移；目前仍以 VS Code 写作为主，Zed 作为持续关注对象。

3、 [C# 14 属性拓展方法生成更加干净代码](https://www.telerik.com/blogs/extension-properties-csharp-14-game-changing-feature-cleaner-code)

- 背景与痛点：自 C# 3（2007）起，扩展方法成为为现有类型“加功能”的标准手段，但只能扩展“方法”，无法表达类型的“特征”。像 IsSuccess 这类语义更接近属性的判断，只能写成 IsSuccess()，既不直观也不对齐面向对象的属性/行为区分。

- 新特性（C# 14/.NET 10）：引入扩展成员（extension members），在一个 extension 块中为某类型新增扩展属性、索引器以及静态扩展成员。原有扩展方法语法完全兼容保留。

- 基本语法：在静态扩展类中使用 extension(Type receiver) 声明接收者，块内可定义属性等成员；访问形式与实例属性一致，如 response.StatusCode.IsSuccess。相关扩展可以在一个块中分组，改善可读性与组织性。

- HttpStatusCode 示例：通过 extension(HttpStatusCode status) 定义 IsSuccess/IsRedirect/IsClientError/IsServerError 等布尔属性，并组合出 Category 分类属性，使 HTTP 响应处理从 if-else 范式转为语义化的属性读取，调用侧更简洁可读。

- 静态扩展成员：使用 extension(HttpStatusCode)（无实例接收者）可添加静态成员，如 OkStatus、NotFoundStatus、BadRequestStatus 以及 FromInt 工厂方法。这让工厂、常量、工具方法以“类型自带”的方式呈现。

- 更多实用示例：对 string 增加 IsEmpty/IsWhitespace/IsValidEmail/IsNumeric；对 ICollection<T> 增加 IsEmpty/HasMultipleItems/HasSingleItem；对 Task<T> 增加静态 Completed/Cancelled 与实例 IsCompleted/HasFailed/Error 等，统一常见状态/校验的表达方式。

- 性能与实现：扩展成员是语法糖，编译为常规静态方法调用，与传统扩展方法生成的 IL 等价，CLR 无额外开销。像 response.StatusCode.IsSuccess 与调用 IsSuccess() 在 IL 层面一致。

- 限制与注意：
  - 不能扩展字段，因此无法添加带自动字段的属性（如 public string Prop { get; set; }）。
  - 某些非常规泛型类型参数顺序的扩展仍需沿用旧的扩展方法语法。
  - 现有扩展方法不受影响，可与扩展成员并存。

- 影响与价值：扩展成员让“特征用属性、行为用方法”的设计意图得以落地。API/库作者可提供更自然的扩展点，调用者无需记忆某能力是方法还是属性，语义更贴近领域模型，代码更易读、更可维护。

4、 [.NET 10 网络提升](https://devblogs.microsoft.com/dotnet/dotnet-10-networking-improvements/)

- HTTP 改进
  - WinHttpHandler 证书验证优化：当设置自定义 ServerCertificateValidationCallback 时，.NET 10 在 WinHttpHandler 中新增按“服务器 IP 地址”缓存已验证证书，避免每个请求都重建证书链并回调验证。新特性为安全起见采用显式启用的 AppContext 开关：AppContext.SetSwitch("System.Net.Http.UseWinHttpCertificateCaching", true)。启用后，同一连接期间的重复请求可显著降低累积耗时。
  - 新增 HTTP 动词 QUERY：为在请求体中携带查询详情且保持安全、幂等的场景提供选项（例如 GET URI 长度受限且服务器不支持带 body 的 GET）。当前处于标准化提案阶段，已提供常量 HttpMethod.Query，完整帮助方法将待 RFC 发布后补充。
  - Cookies：将 CookieException 构造函数公开，便于在应用中显式抛出并处理 Cookie 相关错误。

- WebSockets
  - 新增 WebSocketStream：以 Stream 为中心的抽象，屏蔽帧分片、缓冲与 EndOfMessage 处理，简化文本和二进制协议的收发。
    - CreateReadableMessageStream/CreateWritableMessageStream：面向消息的读写，结束流即表示 EOM；非常适合一次性 JSON 消息或单条二进制负载。
    - Create：用于持续协议（如行式文本协议），可与 StreamReader、JsonSerializer、压缩/序列化器等配合使用。
    - 通过 Dispose 实现优雅关闭；可用 closeTimeout 控制握手等待时长。与传统手动循环+内存缓冲相比，减少拷贝与状态判断，读写路径更自然。

- 安全
  - macOS 客户端 TLS 1.3 支持（可选）：切换到新的苹果网络 API 后，客户端 SslStream 可启用 TLS 1.2/1.3（不含服务端）。通过 AppContext.SetSwitch("System.Net.Security.UseNetworkFramework", true) 或环境变量 DOTNET_SYSTEM_NET_SECURITY_USENETWORKFRAMEWORK=1 启用。
  - 协商套件暴露统一：弃用 SslStream 的 KeyExchangeAlgorithm/HashAlgorithm/CipherAlgorithm 等过时枚举，推荐以 NegotiatedCipherSuite 为唯一信息来源（TlsCipherSuite 与 IANA 对齐）。QuicConnection 同样新增 NegotiatedCipherSuite，保证一致性与准确性。

- 网络基础原语
  - Server-Sent Events（SSE）格式化器：在已有解析器基础上新增写入支持。
    - WriteAsync(IAsyncEnumerable<SseItem<string>>, Stream) 用于纯字符串数据。
    - WriteAsync<T>(…, Action<SseItem<T>, IBufferWriter<byte>>) 支持任意类型，开发者负责序列化为 UTF‑8 文本。
    - SseItem<T> 新增 EventId 与 ReconnectionInterval，支持发送 id 与 retry 字段，便于客户端断线重连与状态保持；与解析器可实现完整往返。
  - IPAddress 新增静态验证：IPAddress.IsValid(string)/IsValidUtf8(…u8) 便捷校验 IPv4/IPv6；同时 IPAddress 与 IPNetwork 实现 IUtf8SpanParsable<T>，提升 UTF‑8 直解析能力与性能。
  - Uri 取消长度上限：为支持 data: URI（如内嵌 Base64 图片）等场景移除原先约 <64KB 的限制。
  - 新增媒体类型常量：MediaTypeNames.Yaml，覆盖 yml/yaml 内容类型需求。

整体而言，.NET 10 在 HTTP、WebSocket、TLS 与基础网络类型方面提供了性能、可用性与可观测性的一系列改进，同时保持安全默认与可控的 opt-in 开关，便于按需采用。

5、 [.NET 10 是如何提升 AIS 服务的性能](https://endjin.com/blog/2025/12/how-dotnet-10-boosted-ais-dotnet-performance-by-7-percent-for-free)

这篇文章介绍了 endjin 维护的开源高性能 AIS 消息解析库 Ais.Net 在不同 .NET 版本上的基准测试结果，重点是 .NET 10.0 再次带来的“零改动”性能提升。该库最初发布于 .NET Core 3.1 时代，目标框架为 netstandard2.0/2.1，因此仅通过将应用升级运行时版本即可获益，无需修改或重发库版本。

核心结论：
- 在原有代码不变的情况下，Ais.Net 在 .NET 10.0 上相较 .NET 9.0 提升约 6%～7%，延续了连续五代 .NET 带来“免费午餐”的趋势。累计自 .NET Core 3.1 起，吞吐提升已超过 2.1 倍。
- 内存表现稳定，摊销分配成本仍为每条记录 0 字节，总体内存占用仅为数 KB（随具体功能略有差异）。

基准方法与环境：
- 两项基准均基于包含 100 万条 AIS 记录的文件：
  1) 轻负载“上限”测试：每条消息处理最少工作量，用于测算单线程最大吞吐。
  2) 略接近实用的工作负载：读取并检查多项属性，更贴近真实应用。
- 为保障跨版本可比性，主要结果在同一老台式机（Intel Core i9-9900K）上运行；并对 .NET 8.0 与 9.0 最新补丁复测以排除运行时更新与系统噪声影响（如需确保空闲、关闭无关进程）。

关键结果（台式机，处理 100 万条消息）：
- .NET 8.0：
  - 上限：5.72M msg/s（Inspect 174.7 ms，4 KB）
  - 现实：4.75M msg/s（Read 210.5 ms，5 KB）
- .NET 9.0：
  - 上限：6.38M msg/s（Inspect 156.7 ms，4.13 KB）
  - 现实：5.20M msg/s（Read 192.3 ms，4.13 KB）
- .NET 10.0：
  - 上限：6.75M msg/s（Inspect 148.1 ms，4.14 KB）
  - 现实：5.58M msg/s（Read 179.3 ms，4.14 KB）
- 相比 .NET 9.0：上限 +6%，现实 +7%。

在更现代硬件（Surface Laptop Studio 2，i7-13800H）上，同一基准可达：
- 上限：10.14M msg/s（Inspect 98.62 ms，3.93 KB）
- 现实：8.76M msg/s（Read 114.41 ms，3.93 KB）

总体上，跨 .NET 版本的 JIT、GC 与基础库优化为该类消息解析/流处理型工作负载持续带来吞吐提升，且不牺牲内存效率。建议对性能敏感的应用升级至 .NET 10.0 以获得即时收益。相关资源：Ais.Net（核心解析库）、Ais.Net.Models（模型）、Ais.Net.Receiver（接收端）、Ais.Net.Notebooks（示例笔记本），均位于 GitHub 组织 ais-dotnet。

6、 [ASP.NET Core 中策略模式的依赖注入](https://blog.elmah.io/using-strategy-pattern-with-dependency-injection-in-asp-net-core/)

本文围绕在 ASP.NET Core 中结合依赖注入实现策略模式的实践展开，旨在解决随业务增长而愈发复杂的条件选择逻辑（如多种折扣算法选择）带来的可维护性问题。策略模式通过将可替换的算法封装为独立策略，并在运行时按输入选择合适的策略，贯彻开闭原则，避免冗长且脆弱的 if-else/switch 代码。

首先，作者用“会员折扣”示例说明不用策略模式的弊端：在单一服务中以 if-else 判断 regular/vip 计算折扣。此实现存在：违反开闭原则（新增会员需改核心方法）、高耦合（单类承载所有规则）、测试困难（每次改动需全量回归）、扩展性差（规则增多即“条件地狱”）等问题。

随后给出控制台程序的策略模式重构步骤：
- 定义策略接口 IDiscountStrategy，暴露 ApplyDiscount(amount)。
- 为每种算法实现独立策略类（如 RegularDiscount、VipDiscount）。
- 上下文类 DiscountService 仅持有 IDiscountStrategy 并委托计算，不再内含业务分支。
- 程序入口按用户输入选择具体策略并调用 DiscountService，完成解耦与可替换。

在 ASP.NET Core API 中进一步演示与 DI 结合的最佳实践：
- 仍以 RegularDiscount 与 VipDiscount 为示例策略。
- DiscountService 注入 Func<string, IDiscountStrategy> 策略工厂，通过键（如“vip”）在运行时解析具体策略，避免上下文依赖容器细节。
- 控制器 PricingController 暴露 GET 接口，接收 type 与 amount，返回折扣与最终价格。
- Program.cs DI 配置要点：
  - services.AddTransient<RegularDiscount/VipDiscount>() 注册无状态策略。
  - services.AddSingleton<Func<string, IDiscountStrategy>>(sp => key => switch 映射键到具体策略 sp.GetRequiredService<...>)，并设定默认策略（如 Regular）。
  - services.AddScoped<DiscountService>() 注册上下文服务；启用 Swagger 便于测试。

扩展示例展示开闭原则的落地：新增 StudentDiscount 策略，仅需新增类、注册 services.AddTransient<StudentDiscount>()，并在工厂 switch 中增加“student”分支，无需修改既有策略或上下文。

结论是：策略模式能以模块化方式替代复杂条件分支，提升可维护性与可扩展性，且借助 ASP.NET Core DI 的工厂委托可实现按键动态解析策略。文末提供完整示例仓库：StrategyPatternDemo 与 StrategyPatternApi。

7、 [C# 14 中隐式 Span 转换](https://endjin.com/what-we-think/talks/csharp-14-new-feature-implicit-span-conversions)

在这场面向 .NET 10 / C# 14 的技术分享中，Ian Griffiths 系统讲解了“内置隐式 Span 转换”这一新特性，如何让 Span/ReadOnlySpan 的使用更自然，提升性能并简化 API 设计，同时指出与旧版库的兼容性风险与修复建议。

背景与动机：
- Span/ReadOnlySpan 自 C# 7/.NET Core 2.1 引入，支持零拷贝切片，统一堆、栈及非托管内存的数据处理，广泛用于高性能场景（如 System.Text.Json）。
- 以 Span 作为参数的 API 能覆盖数组、字符串与栈上数据，避免复制、减少堆分配；扩展方法对 Span 也很常见。

C# 13 的痛点：
- 将 T[] 传给接收 Span<T>/ReadOnlySpan<T> 的泛型方法时，类型推断常失败，需显式提供类型实参。
- 扩展方法接收者不应用用户自定义隐式转换，导致对数组/字符串的 Span 扩展方法无法用点号直接调用。
- 用户自定义隐式转换不可链式叠加多个；而 Span 类型自身的隐式转换已占用“配额”，限制了进一步组合。

C# 14 的关键变化：
- 新增“内置隐式 Span 转换”，区别于用户自定义：
  - T[] -> Span<T>/ReadOnlySpan<T>
  - Span<T> -> ReadOnlySpan<T>（协变）
  - string -> ReadOnlySpan<char>
  - 支持从 T[] 到 ReadOnlySpan<U> 的协变（当存在 T -> U 的引用隐式转换或接口实现）。
- 新的类型推断规则：当形参为 Span/ReadOnlySpan，实参为数组时可正确推断类型，无需显式泛型类型实参。
- 扩展方法解析更新：会考虑内置 Span 转换，使针对 Span 的扩展方法可直接用于数组/字符串等。
- 与用户自定义转换配合：数组->Span 为内置转换后，可与一个用户自定义隐式转换组合，实现从数组“一步”到目标类型。

兼容性与潜在问题：
- 旧库可能出现重载二义性。例如 xUnit 的 Assert.Equal 某些重载在 C# 14 下因数组可隐式到 ReadOnlySpan 而变得同等匹配，导致编译歧义。修复策略：
  - 添加更优匹配的新重载（如 ReadOnlySpan 与数组的特定组合）。
  - 使用 OverloadResolutionPriority 属性打破平局。
- 名称冲突示例：LINQ 的 Reverse(IEnumerable<T>) 与 MemoryExtensions.Reverse(Span<T>) 同名。字符串可隐式为 ReadOnlySpan<char> 后，二者同时可用，需通过 AsEnumerable 显式化意图以选中 LINQ 版本。

实践影响与建议：
- 优先以 Span/ReadOnlySpan 设计 API，调用方（数组/字符串）体验更顺畅、零拷贝更普适。
- 扩展方法更自然、更强大，方法签名更简洁。
- 库作者需回归审视重载集与调用点，添加必要重载或优先级标注，确保在 C# 14 下无二义性与行为改变。

结论：内置隐式 Span 转换使 Span 成为“第一等公民”，在性能与可用性上显著提升，但也要求对旧代码与库进行适配以避免编译期歧义与意外解析变化。





## 开源项目
1、 [SharpIDE](https://github.com/MattParkerDev/SharpIDE)

SharpIDE 是一个面向 .NET 开发者的现代、跨平台、开源 IDE，采用 .NET 与 Godot 构建。目前项目仍在积极开发（WIP），欢迎社区参与与贡献。仓库强调“可本地构建运行”，具体步骤请参见仓库中的 CONTRIBUTING.md；如果在使用或构建过程中遇到问题，建议通过 Issue 反馈。

项目目标是提供一个轻量但功能完善的 .NET 开发生态工作台，覆盖从编辑、构建到运行（以及后续调试、包管理与测试）的常规开发闭环。基于 Godot 的界面与 .NET 技术栈的组合，为跨平台体验打下基础。

当前已具备或展示中的核心能力包括：
- 代码补全（Completions）：为编辑器提供智能提示，提升输入效率与准确性。
- 代码操作/重构（Code Action/Refactoring）：支持常见的代码修复与重构操作，加速日常维护与迭代。
- 符号信息（Symbol Info）：在浏览代码时获取符号级别的上下文与定义信息，便于理解与导航。
- Razor 语法高亮（Razor Syntax Highlighting）：为 Razor 文件提供高亮显示，提升前后端混合场景中的编辑可读性。
- 运行（Run）：可直接在 IDE 内触发运行，缩短从编辑到验证的链路。
- 构建（Build）：集成构建能力，便于快速验证编译通过与产物生成。
- 解决方案选择器（Solution Picker）：支持解决方案维度的选择与管理，符合 .NET 工程组织结构。

以下功能处于进行中（WIP），后续将逐步完善：
- 调试（Debug）：调试能力仍在打磨中，未来将更好地覆盖断点、变量查看等开发者常用调试流程。
- NuGet：NuGet 包管理器集成仍在开发中，预计提供依赖搜索、安装、更新等操作。
- 测试资源管理器（Test Explorer）：测试发现与运行能力在建设中，目标是更顺畅的测试驱动与回归工作流。

综上，SharpIDE 目前已经涵盖了编辑、语法高亮、符号信息、重构、构建与运行等基础环节，能支撑常规的日常开发。调试、包管理与测试相关能力正逐步完善。项目对外开放、欢迎贡献，建议开发者参考 CONTRIBUTING.md 获取本地构建与运行指南；如遇异常或有改进建议，可在仓库中提交 Issue 或 PR，协助推动功能完善与稳定性提升。



