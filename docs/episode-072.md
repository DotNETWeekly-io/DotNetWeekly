.NET 每周分享第 72 期
## 行业资讯
1、 [nuget MCP server 发布](https://devblogs.microsoft.com/dotnet/nuget-mcp-server-preview/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1002.png)
微软在 .NET Blog 宣布推出 NuGet MCP Server 预览版。该服务器实现了开放的 Model Context Protocol (MCP)，可作为 AI 助手与 NuGet 生态间的桥梁，向大型语言模型提供最新的包版本、漏洞信息以及自动化升级工具，解决模型离线数据过时和冲突解析难题。

核心特性
• 实时包版本发现：查询配置源（含私有源）上的最新、兼容版本。
• 安全更新：自动将存在漏洞的包提升到“最小可修复版本”，降低破坏性。
• 版本更新：基于项目目标框架，升级到最高兼容版本，并利用 Microsoft Research 合作开发的 NuGetSolver 算法解决依赖冲突。

部署与前置条件
• 依赖 .NET 10 Preview 6 SDK。
• 以 NuGet 包形式分发，安装命令示例：
  dnx NuGet.Mcp.Server --prerelease --yes
• 可通过@版本号锁定特定预览版。
• 标准 MCP 客户端配置片段：
  ```
  {
    "servers": {
      "nuget": {
        "type": "stdio",
        "command": "dnx",
        "args": ["NuGet.Mcp.Server","--prerelease","--yes"]
      }
    }
  }
  ```

开发工具集成
• Visual Studio：在解决方案同级或用户根目录放置 .mcp.json 即可自动启动。
• VS Code：在 .vscode/mcp.json 中添加同样配置，或点击扩展提供的快捷链接。
• GitHub Copilot Coding Agent：需在仓库新增 copilot-setup-steps 工作流安装 .NET 10，并在仓库设置–Copilot–Coding agent 中填入 MCP 配置；随后 Copilot 可借助服务器进行依赖分析、补丁建议与 PR 生成。

版本状态与反馈
当前为预览版，功能仍在快速迭代。团队欢迎通过 NuGet/Home 提交问题与改进建议，以共同完善 .NET 开发者的 AI 驱动依赖管理体验。

2、 [.NET Conf 2025 内容招募](https://devblogs.microsoft.com/dotnet/dotnet-conf-2025-announcing-the-call-for-content/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-977.png)
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



## 文章推荐
1、 [使用 copilot debug .NET 应用](https://devblogs.microsoft.com/dotnet/github-copilot-diagnostics-toolset-for-dotnet-in-visual-studio/?hide_banner=true)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1020.png)
微软在 Visual Studio 中推出「Copilot Diagnostics」工具集，为 .NET 调试与性能分析引入深度 AI 支持，目标是减少开发者在断点、异常及性能排查上的重复劳动，加速定位并修复问题。

主要能力概览  
• 断点与跟踪点建议：Copilot 会分析当前代码与上下文，自动生成精准的条件表达式或 Tracepoint 动作，无需手动配置。  
• 断点失效诊断：当断点无法命中时，可直接询问 Copilot，它会检查符号、构建配置、优化级别等常见原因，并给出修复步骤。  
• IEnumerable 可视化 + LINQ 辅助：集合数据以表格形式展示，用户可用自然语言让 Copilot 生成或改写 LINQ 查询，快速筛出异常数据。  
• LINQ Hover 解释：调试期间悬停 LINQ 语句，Copilot 会解释其含义、评估执行结果并指出潜在性能隐患。  
• 异常助手：捕获异常时，Copilot 总结错误、推测根因并提供针对性代码修改建议，而不只是展示栈追踪。  
• 变量与返回值分析：悬停变量或查看方法返回值时，可调用 Copilot 获取值异常的可能原因及上下文说明。  
• 并行栈分析：在 Parallel Stacks 窗口，Copilot 为每条线程生成摘要，并自动检测死锁、卡顿或崩溃的线索。

性能分析能力  
• CPU Usage、Instrumentation、.NET Allocation 工具均加入 Auto Insights，总结热点路径、高耗时函数及零长度数组等问题。  
• “Ask Copilot” 按钮支持进一步查询，如循环优化、内存分配减少等实用指导。  
• 微软正计划推出更“代理式”体验，使非性能专家也能轻松完成诊断与优化。

整体价值  
Copilot Diagnostics 并非取代开发者技能，而是嵌入 IDE、贴合上下文，自动提供信息与修复思路，帮助工程师把时间集中在真正的业务逻辑与功能交付上。目前博客还附带两段演示视频，展示调试与 Profiler 场景下的实际用法。

2、 [DependaBot Nuget 性能提升](https://devblogs.microsoft.com/dotnet/the-new-dependabot-nuget-updater/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1008.png)
• 文章介绍了 GitHub Dependabot NuGet 更新器的全新版本。核心变化是彻底抛弃原先以 Ruby 脚本手动解析 csproj / packages.config 的“混合”方案，转而百分之百使用原生 .NET 工具链：NuGet 官方客户端库、MSBuild API 及 dotnet CLI。
• 性能与稳定性显著提升：内部测试套件执行时间从 26 min 缩短到 9 min，提速 65%；更新成功率由 82% 提升至 94%，大幅减少人工干预。
• 依赖发现：借助 MSBuild 的真实项目评估流程，能够正确处理条件编译的 PackageReference、Directory.Build.props/targets 中的版本定义、MSBuild 变量，以及其他复杂引用模式，避免以往“猜测式”解析带来的漏检。
• 依赖解析：新增冲突解决算法。遇到存在漏洞的传递依赖时，更新器会先尝试升级上游包至安全版本；若无可用版本，则自动在项目中增加显式引用，使 NuGet 的“直接依赖优先”规则生效，从而消除漏洞。同时，若某个包系列（如 Microsoft.Extensions.*）需要保持版本一致，工具会整体协同升级，避免碎片化。
• 环境一致性：现在严格遵循 global.json 中指定的 SDK 版本；结合 Dependabot 先前推出的 SDK 升级器，可在自动升级与团队自定义之间取得平衡。
• 完整支持 Central Package Management。能够识别并更新 Directory.Packages.props 中的集中版本信息，也能处理项目级 overrides 与传递依赖。
• 源兼容性：由于使用了 NuGet 官方库，现已原生支持所有符合规范的 v2 / v3 源，包括 nuget.org、Azure Artifacts、GitHub Packages 及企业私有源，并自动处理 API Key、PAT 等认证方式及源映射。
• 开发体验：失败时给出更清晰、可操作的错误消息；用户无需修改 dependabot.yml 就能自动获得新特性。
• 迁移意义：代码基于 C# 重写，方便社区以熟悉的 .NET 开发流程参与贡献，也为后续支持新特性、改进企业场景、加强诊断奠定基础。
• 新版已在 GitHub 全量上线。未使用 Dependabot 的项目可通过极简配置启用：
```yaml
version: 2
updates:
  - package-ecosystem: "nuget"
    directory: "/"
    schedule:
      interval: "weekly"
```  
总结：依托原生 .NET 工具链的全新更新器提供了更快、更准、更可靠的包管理体验，同时保持零配置迁移与更好的可扩展性。

3、 [Resharper 性能提升](https://blog.jetbrains.com/dotnet/2025/08/14/resharper-performance-improvements-2025/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1006.png)
ReSharper 2025.2 以“快”作为首要目标，从架构、算法到 I/O 路径进行了成体系的提速与减负。

• Out-of-Process（OOP）模式首次成为稳定特性，核心分析进程与 Visual Studio 解耦，键入延迟和 IDE 卡顿显著减少；可在 Options → Environment → Products & Features 勾选“Run ReSharper in separate process”启用。

• Razor/Blazor 支持升级：跳过对 _ViewImports/_ViewStart 的重复处理，Find Usages、代码检查与补全更快；引用组件时自动插入 @using；修复多处误报与性能缺陷，对大型 .razor/.cshtml 集合尤为明显。

• Rename 重构深度优化：冲突检测去重并并行化“验证可疑引用”阶段，内存占用更低；进度条反馈更细致，长耗时操作不再“无响应”；同时修复若干正确性问题。

• 所有 In-place 重构（Rename、Change Signature 等）改为异步执行，输入阶段完全非阻塞，重构分析在后台完成，彻底消除“大方案改名时的卡键”现象。

• 解决方案加载与初始索引重写 I/O 策略：放弃针对 HDD 的顺序文件读取，充分利用 SSD 并行吞吐；同时协调后台守护线程，减少 UI Freeze 次数与时长，整体冷启动与首次分析常可缩短数十秒。

• 全新 Early Go-to：在索引尚未完成时即响应 Ctrl+T，提供即时文件级导航；待符号加载完毕后，通过 “Update results” 切换到完整 Go-to-Everywhere（类型、符号、文本搜索）。在百万行级别项目上可节省近一分钟等待。当前早期模式仅支持文件搜索，后续计划扩展。

通过 OOP 架构拆分、并行化算法与现代化磁盘访问策略，ReSharper 2025.2 在启动、编辑、重构与 Razor/Blazor 工作流中的平均响应时间大幅降低，显著改善大规模 .NET 代码库的日常开发体验。官方鼓励用户升级并反馈实际效果，以持续迭代性能与稳定性。

4、 [Workleap 是如何强制 `.NET` 代码标准](https://anthonysimmon.com/workleap-dotnet-coding-standards/)

Workleap 为在数百个 .NET 项目中统一并强制执行编码规范，推出了一个开源 NuGet 包，将 .editorconfig、Roslyn 分析规则与关键 MSBuild 属性封装于一处，实现“一包即配”。本文总结其动机、实施过程与收益。

1. 背景与痛点  
   • 多年来，各仓库通过复制粘贴方式散布大量 .editorconfig、StyleCop 配置，导致规则漂移与风格不一致。  
   • PR 审查中约有三成评论是样式争议，拉长合并周期并损害团队氛围。  
   • 构建阶段警告被忽视，Nullable、AnalysisLevel 等默认值过低，隐藏性能、安全隐患。

2. 解决方案设计  
   • 放弃 StyleCop，全面启用 Roslyn 分析器与 Code Fix。  
   • 精细定义 200+ 行代码风格规则与 800+ 质量/性能/安全规则，并评估对编译时长的影响，确保零或微增。  
   • 通过 MSBuild 属性启用 TreatWarningsAsErrors（CI/Release）、EnforceCodeStyleInBuild、NuGetAudit 等；在新项目模板中默认开启 Nullable。  
   • 引入 Microsoft.CodeAnalysis.BannedApiAnalyzers 统一屏蔽常见危险 API。  
   • 以上配置打包为单一 NuGet 包，利用 SDK 自动导入 .editorconfig 与 props/targets，免去手动复制。

3. 推广与落地  
   • 先在核心产品试点并收集“非符合示例”，以“正向羞耻”方式展示潜在缺陷，赢得技术负责人支持。  
   • 迁移成本小：IDE Code Fix + 文档指引，80% 以上项目在短期内完成升级。

4. 收获  
   • 代码评审更专注业务逻辑，样式争议显著减少，合并速度提升。  
   • 统一风格、性能与安全基线，提高整体代码质量；少量大型解决方案编译时间最多缩短 20%。  
   • 开发者借助分析器提示加深对 C# 语言与最佳实践的理解。  
   • 删除上百个分散的 .editorconfig 与 Build props，降低维护成本。

5. 开源与可定制性  
   • 项目已在 GitHub 与 NuGet 发布，配有自动升级脚本和测试基线；其他组织可直接引用或 Fork 并调整规则。

通过标准化工具链与自动化规则分发，Workleap 将“编码规范的执行”转化为“依赖管理”，在提高生产效率的同时强化了质量与安全。

5、 [在浏览器中运行 .NET 但不使用 Blazor](https://andrewlock.net/running-dotnet-in-the-browser-without-blazor/)

本文作者演示了如何直接在浏览器中运行 .NET 程序，而**完全不依赖 Blazor**，而是直接使用 Blazor 之下的 WebAssembly 运行时。

1. 安装模板与创建工程
通过安装 Microsoft.NET.Runtime.WebAssembly.Templates（本文使用 .NET 10 preview 6）即可获得 `wasmbrowser` 等三个模板，然后 `dotnet new wasmbrowser` 生成示例项目。

2. 代码结构
• Program.cs 是顶层程序，核心逻辑放在 `StopwatchSample` 静态类中；通过 `[JSImport]` 将 JavaScript 函数映射到 C#；通过 `[JSExport]` 把 C# 方法暴露给 JS 调用，实现与页面交互。
• index.html 只是普通静态页面，引用 `_framework/dotnet.js` 和 `main.js` 等资源。
• main.js 负责启动 WASM 运行时、注入 JS 函数、调用 `runMain()` 运行 C# 主函数，并把页面事件（如按钮点击）映射到 `StopwatchSample` 导出的 API。

3. 发布与指纹化
`dotnet publish -c Release` 会自动 Trim 框架、生成 gzip/brotli 资源，并在 .NET 10 中支持前端静态资源指纹（cache busting）。要启用，需要：
- 在 html 中添加 `importmap` 与 `#[.{fingerprint}]` 占位符；
- 在 csproj 中设置 `OverrideHtmlAssetPlaceholders` 与 `<StaticWebAssetFingerprintPattern>`。
模板存在一个缺陷，需给 `<StaticWebAssetFingerprintPattern>` 加 `Expression` 才能正确为 `.js` 文件加指纹。

4. 体积优化
默认发布后体积约 6.8 MB（未压缩）。启用 `<InvariantGlobalization>true</InvariantGlobalization>` 可删除 ICU 数据，体积降至 4.3 MB，压缩后约 1.4 MB。

总体来看，这种“裸 WASM + .NET”方案更加轻量、灵活，适合只需在浏览器里运行少量 .NET 逻辑、而不想引入完整 Blazor 组件模型的场景，同时也展示了 .NET 10 在 WASM 端的工具链改进。

6、 [.NET 开发者学会的 Prompts](https://devblogs.microsoft.com/dotnet/5-copilot-chat-prompts-dotnet-devs-should-steal-today/)

本文面向 .NET 开发者，总结了 5 个可直接复用的 GitHub Copilot Chat 提示，帮助在理解代码、测试、异步化、安全审计与数据准备等环节显著提效。

- 解释此段代码并给出优化建议：将相关 C# 文件加入 Copilot Chat，请其先解释代码意图，再从性能、可读性与可维护性提出重构与优化方案。适合接手遗留项目或久未维护模块，能快速建立上下文并发现隐藏改进点。

- 为此方法/类编写单元测试：将光标置于目标方法或类，要求生成基于 xUnit、MSTest 或 NUnit 的单元测试。可覆盖常见路径与边界条件，补齐遗漏用例，提升覆盖率并在紧迫迭代下保障质量。

- 将此代码改为 async/await：请 Copilot 将同步实现重写为 async/await 异步模式，以提升可伸缩性与 UI/请求响应性，符合现代 .NET 的并发与异步最佳实践，减少阻塞并改善整体用户体验。

- 查找并修复潜在安全问题：让 Copilot 审查片段中的常见漏洞，如 SQL 注入、XSS、不充分输入校验等，并输出修复建议（参数化查询、编码/过滤、验证策略等），作为上线前的额外安全把关。

- 为此模型生成示例数据或 Mock 对象：快速生成贴近真实的样例数据或 Mock 对象，用于 API 原型开发、单元/集成测试与演示，便于模拟真实场景、降低准备测试数据与环境的成本。

文章强调，上述提示只是起点。建议在实践中根据团队技术栈与工作流进行调整和组合，以形成可复用的高效开发模板。更多可定制的提示与实践可参考 Awesome GitHub Copilot Customizations 仓库。通过善用 Copilot Chat，开发者可节省理解和改造代码的时间、提升测试与安全保障能力，并加速交付节奏。



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



## 开源项目
1、 [Facet](https://github.com/Tim-Maes/Facet)

### Issue Summary

Facet 是一个基于 C# Source Generator 的工具集，目标是在编译期为领域模型自动生成轻量级“切面”(Facet)——如 DTO、API Model、投影类型等——从而消除重复的样板代码并保证零运行时开销。

主要思想  
• “Facetting” 即在编译期从完整模型中切出关注的子集。开发者仅需声明要保留/排除的成员，Facet 负责生成对应的类型、构造函数、LINQ 投影表达式及映射逻辑。  
• 生成结果可以是 class、record、struct 或 record struct，支持可变/不可变以及值类型场景。  
• 通过 Source Generator 完成，所有映射和投影表达式在编译期确定，不引入反射或额外运行时代价。

核心特性  
• 属性 / 字段包含或排除：可精准控制暴露的成员，避免敏感信息泄漏。  
• 自动构造函数：快速从源实体映射到 Facet；可选 LINQ Expression 版本，直接在 IQueryable 上应用。  
• 自定义映射：实现 IFacetMapConfiguration / IFacetMapConfigurationAsync 接口即可注入同步或异步的映射逻辑；支持依赖注入。  
• 多环境扩展：与任意 LINQ Provider 兼容；提供专门的 Facet.Extensions.EFCore 包，支持 EF Core 6+ 的异步查询、投影、并行批量映射。  
• 反向映射：通过 UpdateFromFacet 等扩展方法，可将 Facet 修改同步回实体，并返回变更追踪结果，便于审计。

生态组件  
1. Facet：核心 Source Generator，生成类型、投影、映射代码。  
2. Facet.Extensions：与 LINQ 提供程序无关的扩展方法。  
3. Facet.Mapping：更复杂的静态/异步映射配置，支持 DI。  
4. Facet.Extensions.EFCore：面向 EF Core 的异步扩展与优化。

快速上手  
```bash
dotnet add package Facet
dotnet add package Facet.Extensions          # LINQ 助手
dotnet add package Facet.Extensions.EFCore   # EF Core 支持
```
示例：  
```csharp
[Facet(typeof(User))]
public partial class UserFacet { }

var dto  = user.ToFacet<UserFacet>();            // 单个对象
var dtos = users.SelectFacets<UserFacet>();      // IQueryable 投影
```

高级用法  
• 使用 exclude / IncludeFields 精细控制成员。  
• 定义自定义 Mapper 实现复杂字段拼接、异步 I/O、DI 注入服务等。  
• 在 EF Core 查询中直接调用 ToFacetsAsync / SelectFacet，提高性能并减少手工 Select。  
• 通过 UpdateFromFacetWithChanges 进行反向更新并获取变更列表。

适用场景  
• API 层 DTO 生成、前后端分离项目的轻量投影  
• CQRS/查询模型，与 EF Core、Dapper 等 LINQ Provider 集成  
• 需要快速按需暴露属性且保证类型安全、性能敏感的系统

Facet 将“模型切面”理念与 Source Generator 结合，显著减少 DTO 与映射代码的维护成本，并保持高性能、强类型的优势。



