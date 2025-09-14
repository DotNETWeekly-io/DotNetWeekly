.NET 每周分享第 73 期
## 行业资讯
1、 [.NET 10 预览版 1](https://devblogs.microsoft.com/dotnet/dotnet-10-rc-1/)

.NET 10 Release Candidate 1（RC1）已发布，并提供 go-live 许可，可在生产环境使用。本版本可在 Visual Studio 2026 Insiders 与 VS Code（配合 C# Dev Kit）中获得支持。整体以质量与稳定性为重点，附带多项库与框架层面的改进，并提供完整发行说明与参与渠道。

- 库（Libraries）：
  - 密码学：引入 ML-DSA External Mu；后量子密码学（PQC）API 达到“API 完整”；新增基于 UTF-8 的十六进制字符串转换支持。
  - 数值与内存：新增 Tensor、TensorSpan、ReadOnlyTensorSpan，便于高性能张量计算与切片访问。
- 运行时、SDK、C#、F#、Visual Basic：本次 RC1 主要为质量改进，不包含新增功能。
- ASP.NET Core 与 Blazor：
  - 增强导航中的持久化组件状态支持，改善大型应用的状态管理与路由体验。
  - 新增 ASP.NET Core Identity 指标，便于身份认证/授权系统的运行监测与可观测性。
  - 强化 Minimal APIs 与 Blazor 的验证体验，提升输入与模型验证一致性。
  - OpenAPI 架构生成优化，改进 API 描述准确性与互操作性。
- .NET MAUI：
  - 增加诊断与指标跟踪，便于应用健康与性能观测。
  - 新增 HybridWebView 事件；RefreshView 引入 IsRefreshEnabled 属性。
  - .NET for Android 提供实验性的 CoreCLR 运行时支持，探索性能与统一性。
- Windows Forms：
  - 深色模式（Dark Mode）现已全面集成。
  - 明确 ControlStyles.ApplyThemingImplicitlyUsage 用法；渲染、异步与状态管理多项优化。
- WPF：聚焦质量，无新增功能。
- Entity Framework Core：
  - SQL Server 矢量搜索支持，适配向量数据库检索场景。
  - SQL Server 原生 JSON 类型支持。
  - Cosmos DB 引入全文与混合搜索能力。
  - 支持复杂类型与参数化集合的填充策略改进。
- 容器镜像：以质量为主，无新功能。

上手与资源：
- 安装 .NET 10 SDK；Windows 用户建议使用 Visual Studio 2026 Insiders，或 VS Code + C# Dev Kit。
- 参与 .NET 社区 Standup 与 “.NET Unboxed” 系列，了解团队演示与讨论。
- 在 GitHub Discussions 获取发行通告、完整发行说明与反馈渠道，并订阅 RSS。
- 可查阅 “What’s new” 与“Breaking Changes”页面，覆盖 .NET 10、C# 14、ASP.NET Core、MAUI、EF Core、WinForms、WPF 等。

2、 [Visual Studio 2026 预览版](https://devblogs.microsoft.com/visualstudio/visual-studio-2026-insiders-is-here/)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1042.png)
- 微软发布 Visual Studio 2026 Insiders，这是一次面向未来的重要版本，同时引入全新的 Insiders Channel，取代长期使用的 Preview Channel，成为提前体验新特性的官方渠道。该版本围绕三大方向演进：AI 深度融入开发流程、在企业级规模上的显著性能提升、以及以 Fluent UI 为核心的现代化设计。

- AI 一体化开发体验：AI 不再是外挂式功能，而是“织入”到日常编码节奏中。它能在你进入陌生代码库时帮助理解上下文，基于仓库历史建议常见测试类型，并保持文档与注释与代码同步；在“粘贴并修复”场景中，编辑器会自动按项目既有模式与约定改写片段，只需确认即可落地；性能问题分析不再凭猜测，建议基于真实 Trace 与基准测试，修复方案更有把握。代码评审在本地就能获得关于正确性、性能与安全的可执行洞察，在打开 PR 之前就能发现并解决问题。Profiler Agent 进一步协助定位与修复性能瓶颈。整个过程中，IDE 承担繁琐事务，开发者保留判断与决策，目标是提高速度同时提升代码质量。

- 极速性能：该版本显著加速了最常见的开发循环，包括启动、打开/导航大型解决方案、构建与按 F5 调试。首次启动更迅速，大型方案更轻盈，从想法到运行的等待时间进一步缩短。优化覆盖 x64 与 Arm64 架构，并对大代码库场景尤为友好；在频繁的分支切换、全量构建与快速调试迭代中，能更好地维持“心流”状态。

- 现代化外观与体验：基于 Fluent UI 的全面视觉焕新带来更清晰的线条、更易读的图标与更合理的留白布局，选项组织更直观，个性化设置更轻松。扩展管理流程被理顺，内置 11 款新的色调主题，既支持长时间专注也强调默认可访问性，让界面在高强度工作下更舒适、更有方向感。

- 获取方式与兼容性：可与早期版本并行安装；从 Visual Studio 2022 可导入组件与设置快速上手。微软提供 Insiders 下载入口、GitHub Copilot Free、发行说明与开发者社区反馈渠道，并承诺每月更新，持续带来性能改进、设计优化与 AI 创新。

- 已知问题：当前存在已知问题，可能导致部分用户出现错误提示；官方提供了说明与跟踪工单以便关注与解决。

- 总体目标是让开发者更少与工具对抗，把时间投入到真正困难的问题上。通过上下文化、规范一致性与可验证的 AI 建议，结合系统性性能优化与现代 UI，VS 2026 Insiders 致力于在大型工程与高频迭代下显著提升生产力与体验。



## 文章推荐
1、 [.NET 中删除文件至回收站](https://www.meziantou.net/moving-files-and-folders-to-recycle-bin-in-dotnet.htm)

![image](https://raw.githubusercontent.com/DotNETWeekly-io/DotNetWeekly/master/assets/images/issue-1041.png)
这篇文章介绍了在 .NET（Windows 平台）中将文件或文件夹移动到回收站的实用方法，区别于 File.Delete/Directory.Delete 的“永久删除”。通过调用 Windows Shell API（COM 自动化）模拟资源管理器的删除行为，用户可从回收站恢复误删内容。

核心思路：
- .NET 标准库没有直接将文件送至回收站的内置 API，因此借助 Windows Shell 的 COM 接口 Shell.Application 来实现。
- 通过 ShellSpecialFolderConstants 枚举中代表回收站的特殊文件夹常量（值 0xa），获取回收站的 Folder 对象。
- 使用 Folder.MoveHere 方法，将指定路径的文件或目录移动到回收站。

实现步骤（C# 示例）：
1. 使用 Type.GetTypeFromProgID("Shell.Application", throwOnError: true) 获取 Shell.Application 的 COM 类型，并通过 Activator.CreateInstance 创建实例。这里设置 throwOnError: true 以便在类型解析失败时抛出异常，利于诊断。
2. 通过 shellApp.Namespace(0xa) 获取回收站 Folder 对象。0xa 对应 Windows Shell 的回收站特殊目录标识。
3. 调用 recycleBin.MoveHere(path) 将目标移动到回收站。path 可以是文件路径，也可以是目录路径。
4. 封装为方法 MoveToRecycleBin(string path)，即可在项目中复用。

使用示例：
- MoveToRecycleBin(@"C:\path\to\file.txt");
- MoveToRecycleBin(@"C:\path\to\directory");

关键点与行为说明：
- 该方法适用于文件与文件夹，效果与在资源管理器中按 Delete 将项目送入回收站一致，而非物理删除。
- 基于 COM 的动态调用（dynamic）简化了与 Shell 对象的交互，避免手写复杂的互操作定义。
- 该方案依赖 Windows Shell，因此仅适用于 Windows 环境。在非 Windows 平台或无图形 Shell 的环境下不可用。
- 文中引用了微软文档：ShellSpecialFolderConstants（用于定位回收站）与 Folder.MoveHere（执行移动）。开发者可参考官方文档了解更多可选参数与行为差异。

总体来说，文章给出了一个简洁、可直接复制使用的 .NET 代码片段，用于安全删除（可恢复）的业务场景，便于在桌面应用或需要提供“撤销删除”体验的程序中集成。

2、 [.NET Tool 使用和开发](https://andrewlock.net/using-and-authoring-dotnet-tools/)

- 文章介绍了 .NET 工具（dotnet tools）的使用方式与作者ing要点，聚焦在不同运行时环境兼容性、多目标构建与 RollForward 策略，以及在 CI 中测试与安装工具的实用技巧。

- 概念与安装方式
  - .NET 工具通过 NuGet 分发，使用 .NET SDK 安装，可全局安装或局部安装到项目目录。
  - 局部工具通过工具清单（.config/dotnet-tools.json）固定工具及版本，命令包括：
    - 初始化清单：dotnet new tool-manifest
    - 安装（写入清单）：dotnet tool install <package>
    - 恢复：dotnet tool restore
    - 运行：dotnet tool run <command>、dotnet <command>，以及 .NET 10 preview 6 起可用的 dnx/dotnet dnx 一次性执行（自动按清单版本）
  - 适用场景：全局工具适合通用性强、不需按项目固定版本的工具；局部工具适合需要按项目锁定版本的场景（如构建脚本工具）。

- 兼容性与多目标构建
  - .NET 工具本质是框架依赖（Framework-dependent）应用，运行时版本需满足目标 TFM（如目标 net8.0 则需安装 .NET 8 运行时）。
  - 为覆盖更多用户环境，可在 csproj 使用 <TargetFrameworks> 同时面向多个 TFM（如 netcoreapp3.1、net6.0、net7.0、net8.0、net9.0 等）。
  - 代价：
    - 受限于最低 TFM 可用 API，可能限制新特性使用。
    - NuGet 包体积增大，导致 restore/dnx 操作变慢。

- RollForward 策略（与多目标互补）
  - 若不想为每个新 .NET 主版本都打包，可在 csproj 设置 <RollForward>Major</RollForward>（例如仅构建 net6.0），允许在更高版本运行时上运行。
  - 该设置写入 runtimeconfig.json（rollForward），使安装了 .NET 6+ SDK 的环境可运行工具。
  - 注意：跨主版本并非 100% 兼容，但实践中通常可行。即便多目标打包，也建议设置 RollForward 以提升对未来版本（如 .NET 10）的即时可用性。

- CI 与本地测试技巧
  - 本地验证 nupkg：使用 --source 指向本地包目录，--tool-path 指向隔离安装路径，避免误装远程包与污染全局缓存。
  - 预发行版安装需加 --prerelease（SDK 5+ 支持）。
  - 固定版本安装时若需降级，使用 --allow-downgrade（适用于 dotnet tool update/install）。
  - 在 CI 中应显式指定工具版本以保证可重复性，必要时结合 --allow-downgrade 处理已存在版本的环境。

- 要点总结
  - 根据场景选择全局/局部工具；项目内建议使用工具清单锁定版本。
  - 兼容性策略可组合：多目标构建覆盖主流环境，RollForward 提升对未来运行时的适配与安装成功率。
  - 利用 --source、--tool-path、--prerelease、--allow-downgrade 等参数提升测试与部署的可控性与稳健性。

3、 [VisualStudio 中可视化展示 EF Core 查询](https://devblogs.microsoft.com/dotnet/ef-core-visualizer-view-entity-framework-core-query-plan-inside-visual-studio/)

文章介绍了 EFCore.Visualizer——一款在 Visual Studio 内直接查看与分析 Entity Framework Core 查询执行计划的扩展。EF Core 通过强类型 LINQ 生成针对目标数据库的 SQL；但在复杂模型与查询下，生成 SQL 可能欠佳，或因缺失/误用索引导致性能下降。尽管 EF Core 提供日志以捕获生成 SQL 和识别慢查询，要找出根因仍需分析数据库的执行计划。

EFCore.Visualizer 通过在调试时为 IQueryable<> 提供调试器可视化器，捕获当前 LINQ 查询，向数据库请求执行计划，并以可视化形式显示生成 SQL 与执行计划。它支持主流 RDBMS（SQL Server、PostgreSQL、MySQL、SQLite、Oracle），可自动识别提供程序。将执行计划带入 IDE，避免在 VS 与数据库管理工具之间来回切换，缩短开发者“内循环”，有助于在编码/调试现场快速验证与优化查询。

安装可在 Visual Studio 扩展管理器搜索“EFCore.Visualizer”，或从 Visual Studio Marketplace 获取。使用时，在断点处悬停任意 IQueryable，点击“Query Plan Visualizer”即可查看。文中示例以 BloggingContext 为例，Posts 表在 PublishedAt 上建有索引。初始查询使用 post.PublishedAt.Year == 2010，生成的 SQL 对列进行函数运算，属于非 SARGable，导致 SQL Server 无法利用索引并触发表扫描。将查询改写为范围过滤（PublishedAt >= 2010-01-01 且 < 2011-01-01）后，执行计划显示使用索引查找（Index Seek），性能得到改善。可视化器同时展示生成 SQL 与计划，便于对比与验证改写成效。

实现原理是把 LINQ 转换为 ADO.NET 命令，向数据库引擎请求计划，并用不同渲染库展示：SQL Server 使用 html-query-plan，PostgreSQL 使用 pev2，其他数据库使用 treeflex。

已知限制包括：不支持带有缩减终止算子的查询（如 Count、Min、First 等）；当查询过于复杂或与数据库的网络较慢时，获取执行计划可能超出 Visual Studio 自定义可视化器的 5 秒超时，当前无法延长该超时。该扩展开源，代码可在 GitHub 查看，另有 Visual Studio Toolbox 节目对其进行演示。

4、 [C# 获取 Windows 下其他知名的文件夹](https://www.meziantou.net/accessing-windows-known-folders-in-csharp-with-shgetknownfolderpath.htm)

Windows 提供一组“已知文件夹”（Known Folders），例如 Documents、Pictures、Music 等。.NET 中可通过 Environment.GetFolderPath 访问，但其覆盖范围受限于 Environment.SpecialFolder 枚举。当需要访问更多已知文件夹（例如 Desktop、Downloads 等）时，需调用 Win32 API SHGetKnownFolderPath 才能获得路径。文章介绍了两种可行方案：直接使用 Win32 API，或使用现成的 NuGet 库简化调用。

- 使用 SHGetKnownFolderPath：
  - 每个已知文件夹在 Windows SDK 的 KnownFolders.h 中都有对应的 GUID（示例路径：C:\Program Files (x86)\Windows Kits\10\Include\10.0.26100.0\um\KnownFolders.h）。
  - 通过 Microsoft.Windows.CsWin32 生成 C# P/Invoke 绑定，直接调用 PInvoke.SHGetKnownFolderPath。
  - 基本调用模式：提供目标 Known Folder 的 GUID、KNOWN_FOLDER_FLAG（如 KF_FLAG_DEFAULT），hToken 传 null 获取当前用户路径，接收返回的路径指针。
  - 将返回的路径转换为字符串后，调用 Environment.ExpandEnvironmentVariables 展开可能包含的环境变量占位符。
  - 使用 Marshal.FreeCoTaskMem 释放由 Shell API 分配的内存，避免泄漏。
  - 通过返回的结果（例如 result.Succeeded 或 HRESULT 值）判断是否成功；失败时抛出 Win32Exception，并包含错误码与上下文信息。

- 使用 NuGet 包 Meziantou.Framework.FullPath：
  - 若不想维护 GUID、P/Invoke 签名和内存释放逻辑，可使用该库提供的强类型 API。
  - 示例：FullPath.GetKnownFolderPath(KnownFolder.Downloads) 可直接获取下载文件夹路径，调用简单且安全。
  - 库内部封装了对 SHGetKnownFolderPath 的调用与错误处理，减少样板代码与平台细节负担。

要点小结：
- Environment.GetFolderPath 仅适用于 SpecialFolder 枚举列出的子集；当目标文件夹不在此枚举中时，应改用 SHGetKnownFolderPath。
- GUID 清单与语义以 Windows SDK 为权威来源；借助 CsWin32 可自动生成可靠的互操作绑定。
- 正确的内存管理与错误处理至关重要：展开环境变量、释放返回的内存、根据 HRESULT 进行异常处理。
- 若以易用性与可维护性为先，推荐使用 Meziantou.Framework.FullPath，避免手写 P/Invoke。

5、 [.NET 命令行测试提升](https://devblogs.microsoft.com/dotnet/dotnet-test-with-mtp/)

- 文章介绍了 .NET 10 中 dotnet test 与 Microsoft.Testing.Platform（MTP）的原生集成及其带来的改进。dotnet test 支持 MSTest、xUnit、NUnit、TUnit 等测试框架，并可基于两类测试平台运行：VSTest 与 MTP。MTP 为轻量、可移植方案，嵌入测试项目中，可在 CLI、CI、Visual Studio Test Explorer 与 VS Code Test Explorer 中统一运行，无需额外依赖应用。
- 演进概览：在 .NET 9 中，使用 MTP 需通过 VSTest 模式绕行（使用 -- 分隔传参，并设置 MSBuild 属性 TestingPlatformDotnetTestSupport），存在体验与能力限制。.NET 10 引入原生集成，通过在解决方案或代码库根目录添加 dotnet.config 即可启用：
  [dotnet.test.runner]
  name = "Microsoft.Testing.Platform"
  配置作用域仅限仓库/解决方案级，不支持用户/机器级全局配置。
- 关键收益：
  - 统一执行体验：更紧密的 SDK 集成，成为 .NET 测试执行的未来方向。
  - 性能提升：加速启动、发现与执行，支持不同 TFM 的测试程序集并行执行，适合大型套件与 CI 场景。
  - 诊断增强：结构化日志、更清晰的输出，基于 ANSI 终端的更佳 UI/UX，便于排障。
  - 动态参数与可扩展：测试参数由已注册扩展决定，具备更强可定制性。
  - 灵活筛选：新增基于通配模式（globbing）的测试模块过滤，便于直接对预构建程序集执行测试。
- 兼容性与迁移：
  - 全有或全无：一旦在 dotnet.config 启用 MTP，解决方案内所有测试项目必须迁移到 MTP，不能混用 VSTest 与 MTP。
  - 清理属性：.NET 10 原生集成后，可移除旧属性，如 TestingPlatformDotnetTestSupport、TestingPlatformShowTestsFailure（失败默认展示）。
- 命令与选项变更：
  - 默认执行：dotnet test 在当前目录运行。
  - 重大变更：不再支持直接使用路径（如 dotnet test MyProject.csproj）。需使用显式目标选项，且互斥：
    - --project <项目路径>
    - --solution <解决方案路径>
    - --test-modules "<通配模式>"（可配合 --root-directory 指定根目录；用于预构建的程序集；此模式下不适用构建相关选项）
  - 并行控制：--max-parallel-test-modules <N> 限制并行测试模块数。
  - 输出控制：--no-ansi、--no-progress、--output Normal|Detailed 控制终端输出与详细程度。
  - 传统构建/运行选项仍可用：-a/--arch、-c/--configuration、-f/--framework、--os、-r/--runtime、-v/--verbosity、--no-build、--no-restore。
  - MSBuild 属性传递方式保持不变：-p:Name=Value 或 --property:Name=Value（支持多项）。
- 总结：通过 dotnet.config 启用 MTP，可获得更高性能、更佳诊断与更清晰的命令语义。建议先迁移单个测试项目验证，再逐步推广至整个解决方案。





## 开源项目
1、 [nuget-server](https://github.com/kekyo/nuget-server)

- 项目简介：nuget-server 是基于 Node.js 的现代化 NuGet v3 服务器，实现了关键的 v3 API，兼容 dotnet restore 和标准 NuGet 客户端，内置现代 Web UI，支持查询、版本下载、手动上传与用户管理。无需数据库，直接在文件系统存储 .nupkg 与 .nuspec。

- 主要特性：
  - 10 秒快速启动，NuGet v3 API 兼容。
  - 文件系统存储，无外部数据库依赖；支持图标存储。
  - 发布采用 HTTP POST 二进制上传（/api/publish），避免 multipart 在网关/代理上的兼容性问题。
  - 基本认证与可选的完整访问认证；反向代理与 baseUrl/Forwarded 头处理。
  - Web UI 支持多包拖拽上传、用户管理（新增/删除/重置）、用户自助重置 API 密码与修改密码。
  - 支持从其他 NuGet 服务器批量导入包；提供 Docker 镜像（amd64/arm64）。

- 系统与运行：
  - 依赖 Node.js 20.18.0+；安装：npm install -g nuget-server；默认端口 5963，v3 端点为 /v3/index.json。
  - 常用参数：--port、--package-dir、--base-url、--users-file、--max-upload-size-mb。
  - 客户端必须使用 v3 源：dotnet nuget add source https://host/v3/index.json --protocol-version 3。

- 存储与备份：默认 ./packages，按包名/版本分层存放 .nupkg/.nuspec/icon，直接打包目录即可备份与恢复。

- 配置方式：命令行 > 环境变量 > config.json（可通过 --config-file 指定）。可配置日志级别、代理信任名单、上传大小、重复包策略（overwrite/ignore/error）、缺失包响应模式（默认返回空数组，也可设置为 404）。

- 认证与账号：
  - 认证模式：none（默认）、publish（仅发布需认证）、full（所有操作需登录）。
  - 首次通过 --auth-init 交互创建管理员与 users.json。
  - 区分 UI 登录密码与 API 密码；API 密码用于 NuGet 客户端访问/发布。密码使用盐哈希存储，支持 zxcvbn 强度校验（默认最小 2）。
  - 建议使用 HTTPS，避免明文凭证被嗅探。

- 反向代理：在公网域名/网关部署时需设置 --base-url 或配置 trusted proxies 以正确生成 v3 子端点 URL。

- Docker：
  - 映射卷：/data（config.json、users.json）、/packages（包存储）；容器以 UID 1001 运行，需确保宿主目录权限。
  - 示例：docker run -p 5963:5963 -v ./data:/data -v ./packages:/packages kekyo/nuget-server:latest。
  - 支持 docker-compose 与 Podman/systemd 自启；提供多架构构建脚本（需 QEMU）。

- 导入工具：--import-packages 可从其他 NuGet 服务器发现并逐个下载导入，保留图标，支持认证，支持进度显示与错误记录。

- API 覆盖：/v3/index.json、包内容与下载、registrations 索引等核心端点。支持 CI 非交互式初始化（通过环境变量注入管理员凭据）。许可证：MIT。

2、 [QRCoder](https://github.com/codebude/QRCoder)

QRCoder 是一个纯 C# 编写的开源二维码生成库，采用 MIT 许可，由 Raffael Herrmann 自 2013 年起维护。项目不依赖第三方库（根据目标框架可能使用 System.Drawing.Common 与 System.Text.Encoding.CodePages），通过 NuGet 提供稳定版包，并可在 GitHub Packages 获取 CI 构建以体验最新功能。项目提供完整 Wiki、发布说明以及作者博客（英文/德文）作为参考资料。

安装方式支持两种：直接克隆仓库或通过 NuGet 安装（Install-Package QRCoder）。基本用法非常简洁：使用 QRCodeGenerator 生成 QRCodeData，再用具体渲染器（例如 PngByteQRCode）输出二维码图像字节。典型示例为 CreateQrCode("要编码的文本", QRCodeGenerator.ECCLevel.Q) 并通过 GetGraphic(20) 生成 PNG 字节数组。库兼容 .NET Framework、.NET Core、.NET Standard 与 .NET，全平台支持情况可参考 Wiki 的目标框架兼容表。

在渲染输出方面，QRCoder 提供多种渲染器以适配不同场景与格式，包括：
- QRCode（位图）、ArtQRCode、AsciiQRCode、Base64QRCode、BitmapByteQRCode
- PdfByteQRCode、PngByteQRCode、PostscriptQRCode、SvgQRCode
- UnityQRCode（通过 QRCoder.Unity）、XamlQRCode（通过 QRCoder.Xaml）
不同渲染器在目标框架上的可用性存在差异，需查阅 Wiki 的兼容性表。

针对常见业务数据，QRCoder 提供 PayloadGenerator 帮助生成结构化的二维码内容字符串，使扫描器能触发特定行为。以 WiFi 为例，仅需 PayloadGenerator.WiFi("SSID","Pass", WPA) 生成负载，再传入 CreateQrCode 即可。CreateQrCode 方法支持多种重载：可直接传入字符串或 Payload 对象；可沿用 Payload 中设置的 QR 版本、纠错等级（默认 M）与 ECI 模式（自动检测），也可在调用处显式覆盖 ECCLevel。

PayloadGenerator 支持的类型广泛，覆盖支付、通信与业务标准，包括：BezahlCode、比特币及类比特币地址（BTC/BCH/LTC）、书签、日历事件（iCal/vEvent）、联系人（MeCard/vCard）、地理位置、Girocode、邮件、MMS、Monero、一次性密码（OTP）、电话、俄罗斯付款单（ГОСТ Р 56042-2014）、Shadowsocks 配置、Skype 呼叫、SlovenianUpnQr、SMS、SwissQrCode（ISO-20022）、URL、WhatsApp 消息与 WiFi 等。

项目首页包含构建状态、代码覆盖率与 NuGet 包状态徽章。更多使用示例、进阶渲染选项与版本变更记录可在 Wiki 与 Release Notes 中获取。

3、 [支持模式匹配的文件路径搜索库Glob.cs](https://github.com/mganss/Glob.cs)

- 项目概述：Glob.cs 是一个用于 .NET 的路径通配（globbing）库，采用单文件实现，便于集成与分发。支持 netstandard2.0 和 .NET Framework 4.6.1，可通过 NuGet 包 Glob.cs 获取。仓库提供持续集成与覆盖率徽章，便于了解构建与测试状态。

- 核心特性：
  - 可配置大小写敏感性，适配不同文件系统或场景需求。
  - 可选择仅匹配目录，满足只遍历文件夹的过滤场景。
  - 支持可注入的文件系统实现（基于 System.IO.Abstractions），便于隔离文件系统进行单元测试与模拟。
  - 可取消长时间运行的匹配过程，适用于大规模目录树的扫描中途终止。
  - 文件系统错误处理可选：抛出异常或继续跳过，增强在非理想环境下的健壮性。
  - 可选错误日志输出，调用方可提供日志实现以记录问题。
  - 惰性目录树遍历，提升性能与资源利用效率。
  - 支持限制通配符“**”的递归展开深度，控制扫描范围与成本。

- 匹配语法：
  - ?：匹配单个字符。
  - *：匹配零个或多个字符。
  - **：匹配零个或多级递归目录，例如 a\**\x 可匹配 a\x、a\b\x、a\b\c\x 等。
  - [...]：字符集匹配，语法与 .NET 正则的字符组一致（如包含与取反等）。
  - {group1,group2,...}：模式分组匹配，支持嵌套组与模式，例如 {a\b,{c,d}*}。

- 使用方式：
  - 通过 NuGet 安装 Glob.cs 后，即可使用静态方法展开模式获得匹配路径集合，例如 `Glob.Expand(@"c:\\windows\\system32\\**\\*.dll")`。
  - 结合可注入文件系统、可取消操作以及错误处理与日志选项，可在生产与测试环境中灵活控制性能、可靠性与可观测性。

- 适用场景：文件/目录批量匹配、构建与部署脚本中的文件筛选、工具链中的资源发现、跨平台或不同大小写规则下的路径处理，以及需要受控遍历深度与稳健错误处理的批量文件操作等。



