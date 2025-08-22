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
    {
      "servers": {
        "nuget": {
          "type": "stdio",
          "command": "dnx",
          "args": ["NuGet.Mcp.Server","--prerelease","--yes"]
        }
      }
    }

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
version: 2
updates:
  - package-ecosystem: "nuget"
    directory: "/"
    schedule:
      interval: "weekly"
  
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

通过 OOP 架构拆分、并行化算法与现代化磁盘访问策略，ReSharper 2025.2 在启动、编辑、重构与 Razor/Blazor 工作流中的平均响应时间大幅降低，显著改善大规模 .NET 代码库的日常开发体验。官方鼓励用户升级并反馈实际效果，以持续