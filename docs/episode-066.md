# .NET 每周分享第 66 期

## 卷首语

![Image](https://github.com/user-attachments/assets/4c9b406e-fdf9-4bbc-8c39-3c21febf659b)

Jimmy Bogard 宣布将开源项目 AutoMapper 和 MediatR 商业化，以保障项目的长期可持续发展，并改善社区建设与用户支持；与此同时，MassTransit 也发布了 v9，并转向商业授权模式，计划为企业级用户提供更强保障，同时维持对 v8 版本的开源支持直至 2026 年。这些举措反映出知名开源项目为了生存与发展，正逐步探索新的商业化道路。
你如何看待开源项目逐步走向商业化以实现可持续发展的趋势？

## 行业资讯

1、[Microsoft.Build.Sql 正式 GA](https://techcommunity.microsoft.com/blog/azuresqlblog/the-microsoft-build-sql-project-sdk-is-now-generally-available/4392063)

![Image](https://github.com/user-attachments/assets/a4ce79f2-b2cd-4f29-b833-6dd076f2cd0c)

Microsoft.Build.Sql 项目 SDK 正式发布，​ 该 SDK 提供了一个跨平台框架，将数据库开发纳入整体开发流程，使数据库对象可以像其他应用程序组件一样，通过源代码管理和自动化流水线进行部署。​

SQL 项目是一种基于 .NET 的项目类型，可将一组 SQL 脚本编译成数据库工件（.dacpac），用于手动或持续部署。​ 开发者可以手动编写 T-SQL 脚本，或通过图形化的架构比较界面和 SqlPackage 提取命令等自动化工具生成这些脚本。​ 无论是使用 SQL Server、Azure SQL，还是 Fabric 中的 SQL，SQL 项目都提供了统一的开发标准和工具生态系统。​ 这些工具包括 SqlPackage CLI、适用于 VS Code 的 SQL 数据库项目扩展，以及 Visual Studio 中的 SQL Server Data Tools。​

SQL 项目的构建过程包含数据库模型验证，可在代码提交或部署前提前验证 SQL 语法。​ 此外，项目构建中还可以启用反模式的代码分析，以检测可能影响数据库设计和性能的问题，并将这些分析集成到团队的持续集成或预提交检查中。​

与原始的 SQL 项目相比，转换为 Microsoft.Build.Sql SDK 的项目支持 .NET 8，实现了跨平台开发和自动化环境。​SDK 风格的项目文件格式简化了配置，默认包含项目文件夹结构中的所有 .sql 文件。​ 数据库引用功能允许项目通过项目引用、.dacpac 工件引用和新的包引用方式，整合跨数据库引用或多个开发周期的对象。​ 包引用功能提升了数据库发布周期的灵活性和可管理性，简化了引用工件的版本控制和管理。​

现有的 SQL 项目可以根据需要转换为 Microsoft.Build.Sql 项目 SDK。​ 原始的 SQL 项目在 SQL Server Data Tools（SSDT）中仍受支持，支持周期遵循 Visual Studio 的生命周期，为现有项目提供了长期支持。

## 文章推荐

1、[用.NET 和 C#创建一个 MCP Server](https://devblogs.microsoft.com/dotnet/build-a-model-context-protocol-mcp-server-in-csharp/)

![Image](https://github.com/user-attachments/assets/7e70d05b-c65d-42b9-83cc-58e3e3030a79)

MCP（Model Context Protocol）是一种标准化协议，定义了 AI 模型（如大型语言模型，LLMs）与外部工具和数据源之间的通信方式。它允许模型通过统一的接口调用外部工具，实现更复杂的任务和增强的功能。​

使用 C# 构建 MCP 服务器的步骤：​

1. 创建控制台应用程序： 使用 dotnet new console 创建新的控制台应用程序。
2. 添加必要的 NuGet 包： 包括 ModelContextProtocol（MCP C# SDK）和 Microsoft.Extensions.Hosting。
3. 配置服务器： 在 Program.cs 中设置服务器，使用标准输入/输出（stdio）作为传输方式，并扫描程序集中的工具。
4. 定义工具： 通过添加带有 [McpServerToolType] 和 [McpServerTool] 属性的方法，定义服务器提供的功能。例如，创建一个简单的 Echo 工具，返回输入的消息。

在 VS Code 中运行和测试： 配置 mcp.json 文件，集成到 VS Code 的 GitHub Copilot Agent 模式中，测试工具的调用。

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();


[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Hello from C#: {message}";

    [McpServerTool, Description("Echoes in reverse the message sent by the client.")]
    public static string ReverseEcho(string message) => new string(message.Reverse().ToArray());
}
```

2、[dotnet-cli 支持新的 slnx 文件格式](https://devblogs.microsoft.com/dotnet/introducing-slnx-support-dotnet-cli/)

![Image](https://github.com/user-attachments/assets/e03e7f4f-bda7-432d-8c6a-498e3c05cb72)

我们知道之前的解决方案文件(sln) 的文件内容几乎是不可读的，因为其中包含了很多 `GUID`。而且如果和别人的项目发生了冲突，修改起来也是非常繁琐，所以微软推出了新的基于 `XML` 格式的新的解决方案格式 (slnx).

这个功能在最新版的 `Visual Studio` 中可以选择性打开，现在 `.NET CLI` 最新版本也可以支持将现有的解决方案转换成新的格式，之前格式

```txt
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "my-app", "my-app\my-app.csproj", "{EEDA50C6-521D-484C-87D2-054955E7C5E0}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Debug|x64 = Debug|x64
		Debug|x86 = Debug|x86
		Release|Any CPU = Release|Any CPU
		Release|x64 = Release|x64
		Release|x86 = Release|x86
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Debug|x64.ActiveCfg = Debug|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Debug|x64.Build.0 = Debug|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Debug|x86.ActiveCfg = Debug|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Debug|x86.Build.0 = Debug|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Release|Any CPU.Build.0 = Release|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Release|x64.ActiveCfg = Release|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Release|x64.Build.0 = Release|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Release|x86.ActiveCfg = Release|Any CPU
		{EEDA50C6-521D-484C-87D2-054955E7C5E0}.Release|x86.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
```

执行下面命令 `dotnet sln migrate`, 结果如下

```xml
<Solution>
  <Configurations>
    <Platform Name="Any CPU" />
    <Platform Name="x64" />
    <Platform Name="x86" />
  </Configurations>
  <Project Path="my-app/my-app.csproj" />
</Solution>
```

3、[一个后端工程师从 Go 转到 C#的体验](https://www.reddit.com/r/csharp/comments/1jlu007/experience_of_switching_from_go_to_c/)

Reddit 帖子介绍了从 Go 转向 C# 的经验介绍

✅ C# 的优势
- 丰富的标准库：​C# 提供了大量内置功能，减少了对第三方库的依赖。
- 强大的 IDE 支持：​Visual Studio 和 Rider 等工具提升了开发效率。
- 面向对象编程：​C# 的面向对象特性使代码结构更清晰，易于维护。​

⚠️ 面临的挑战
- 学习曲线：​C# 的复杂性高于 Go，需要更多时间掌握。
- 构建速度：​ 相比 Go，C# 的编译时间较长。
- 部署复杂性：​C# 应用的部署流程相对复杂，尤其是在跨平台部署时。

4、[搭建了 Serverless 平台](https://www.linkedin.com/pulse/i-built-serverless-platform-1-day-so-can-you-alfred-white-kaihf/)

🧠 背景与动机

在为新员工撰写关于 Microsoft Orleans 的内部文档时，作者突发奇想：既然 Orleans 可以构建“纳米服务”（比微服务更小的服务单元），是否可以利用它快速搭建一个无服务器平台？

🛠️ 技术选型

- 语言与框架： C# + Orleans + gRPC
- 执行代码语言： JavaScript（因其流行度和广泛支持）
- 架构划分：
- 控制平面（Control Plane）： 负责注册和管理处理程序
- 数据平面（Data Plane）： 处理实际的请求和响应

⚙️ 控制平面设计

使用 gRPC 定义了两个主要方法：​
- RegisterHandler： 注册新的处理程序
- QueryUsage： 查询资源使用情况 ​

通过这些接口，开发者可以动态部署和管理无服务器函数。

🔄 数据平面实现

数据平面负责执行注册的 JavaScript 代码。​Orleans 的虚拟 Actor 模型简化了状态管理，使得每个处理程序可以独立运行，类似于传统的函数即服务（FaaS）模型。

💡 关键收获

- 快速原型开发： 利用现有工具和框架，可以在极短时间内搭建一个功能完整的无服务器平台。
- Orleans 的优势： 其虚拟 Actor 模型非常适合构建细粒度的服务单元，降低了系统复杂性。
- gRPC 的便利性： 提供了强类型的接口定义和自动生成的客户端代码，提升了开发效率。

5、[使用现代化 .NET 特性来优化内存使用](https://mijailovic.net/2025/04/10/memory-optimizations/)

![Image](https://github.com/user-attachments/assets/d4e8d365-4274-41c0-987d-fbe18d348e93)

这篇文章主要总结了在 .NET 8/9 中进行内存优化的几大要点：

- 真实需求优先

优化前要先用性能分析器（如 PerfView）确认存在真实内存问题，避免无谓优化。

- 字符串格式化优化

推荐使用字符串插值（$"{var}"）代替 + 拼接、StringBuilder、string.Join 等方法，不仅更快、更省内存，还更易读。使用原始字符串字面量（"""多行内容"""）可以简化复杂模板字符串。

- 集合容量优化

使用集合初始化器（如 new List<T> { ... }）时，默认初始容量小，元素增多会导致多次扩容，浪费内存和 CPU 资源。

明确指定集合容量（如 new Dictionary<TKey, TValue>(8)）能显著减少内存分配和提高性能。

对 List<T>，推荐使用集合表达式（collection expressions），不仅语法更简洁，还能直接设置合适容量。

- 栈上分配内存（stackalloc）

对于小型、临时的缓冲区（如加密哈希计算），可以用 stackalloc 在栈上分配内存，跳过 GC，提高效率。一般推荐小于 512–1024 字节以内使用。

- 大小写无关的哈希优化

避免 ToLower()/ToUpper() 再 GetHashCode() 的写法。

改用 GetHashCode(StringComparison.OrdinalIgnoreCase)。

多对象组合哈希时，用 HashCode.Add(obj, StringComparer.OrdinalIgnoreCase)。

- 字节数组转十六进制字符串

使用 `Convert.ToHex` 方法比 `BitConverter` 更加高效

- HttpClient JSON 反序列化

在 `HTTPClient` 获取返回 JSON 结果的反序列化的时候，可以是直接使用 `GetFromJsonAsync<T>` 方法避免大的内存对象分配

- 避免一些无意间的内存分配

7、[使用 ErikEJ.DacFX SQL 分析器分析你项目中的 SQL 语句问题](https://erikej.github.io/dacfx/codeanalysis/sqlserver/2024/04/02/dacfx-codeanalysis.html)

这篇文章介绍了如何在 Visual Studio 中对 SQL Server 的 T-SQL 脚本进行静态代码分析，利用 DacFX 库和 MSBuild.SDK.SqlProj 项目模板来提升代码质量和一致性。

🔍 核心要点

1. 启用代码分析

- 在 `.sqlproj` 项目文件中添加：
  ```xml
  <RunSqlCodeAnalysis>True</RunSqlCodeAnalysis>
  ```
- 默认启用 Microsoft 提供的 T-SQL 规则，涵盖设计、命名、性能问题。

2. 添加额外规则集

- 通过 NuGet 引入社区维护的规则集，例如：
  - [`ErikEJ.DacFX.SqlServer.Rules`](https://www.nuget.org/packages/ErikEJ.DacFX.SqlServer.Rules)：包含 120+ 条 SQL 最佳实践规则。
  - [`ErikEJ.DacFX.TSQLSmellSCA`](https://www.nuget.org/packages/ErikEJ.DacFX.TSQLSmellSCA)：检测常见的 T-SQL 异味。
- 这些规则会在构建时自动应用，无需额外配置。

3. 配置规则行为

- 通过 `<CodeAnalysisRules>` 属性启用或禁用特定规则，例如：
  ```xml
  <CodeAnalysisRules>-SqlServer.Rules.SRD0006;-Smells.*</CodeAnalysisRules>
  ```
- 例子中禁用了规则 SRD0006 和所有以 Smells 开头的规则。

4. 在构建过程中运行分析

- 构建项目时自动进行代码检查。
- 违规项以**警告**或**错误**形式出现，可以加强质量控制。

5. 创建自定义规则

- 可以用 .NET/C# 编写自己的 T-SQL 代码分析规则。
- 自定义规则打包为 NuGet 包供项目使用。

6. 跨平台支持

- 使用 `MSBuild.SDK.SqlProj`，可以在 GitHub Actions、Azure DevOps 等 CI/CD 平台上运行代码分析。

通过以上方法，可以在开发早期就发现潜在的 SQL 问题，提升数据库项目的一致性与质量。

8、[使用 .NET 事件查看器调查线程池匮乏问题](https://techblog.criteo.com/investigate-thread-pool-starvation-with-net-events-viewer-1fa8453afd80)

🧩 背景
Criteo 的某个服务在生产环境中出现了响应时间显著上升的情况，P99 响应时间从 40ms 飙升至 500ms，且与流量波动相关。初步怀疑是线程池饥饿（Thread Pool Starvation）导致的性能问题。

🔍 调查过程

1. **线程池线程数波动**  
   使用 .NET 运行时事件计数器观察到线程池线程数大幅波动，表明线程池可能被阻塞。

2. **识别阻塞原因**  
   阻塞可能由以下操作引起：

   - 同步等待异步结果（如 `Task.Result`, `Task.Wait`, `Task.GetAwaiter().GetResult()`）
   - 锁操作（如 `lock`, `Monitor.Enter`, `ManualResetEventSlim.Wait`, `SemaphoreSlim.Wait`）

3. **收集 Trace 数据**  
   使用以下命令收集包含 `WaitHandle` 事件的 trace 数据：

```bash
   dotnet trace collect \
     --clrevents waithandle \
     --clreventlevel verbose \
     --duration 00:00:30 \
     --process-id <PID>
```

4. **分析 Trace 数据**  
   使用 [.NET Events Viewer](https://verdie-g.github.io/dotnet-events-viewer/) 加载 trace 文件，切换到树状视图，合并堆栈信息，识别出主要的阻塞点集中在 Aerospike 客户端和其他组件的同步调用上。

🛠️ 工具优势

- **.NET Events Viewer**：Criteo 开发的跨平台、基于 WebAssembly 的工具，提供友好的 UI，支持堆栈合并视图，便于分析。
- **新事件支持**：.NET 9 引入了 `WaitHandle` 事件，专门用于捕捉线程阻塞情况，提升了诊断效率。

✅ 结论

通过引入新的事件和使用 .NET Events Viewer，Criteo 成功定位并解决了线程池饥饿问题，显著改善了服务的响应时间和稳定性。

9、[简单解释微软的长支持版本](https://blog.inedo.com/dotnet/lts-chart/)

本文旨在简化 Microsoft .NET 各版本的支持周期信息，帮助开发者快速了解当前使用的 .NET 版本的支持状态，避免在众多文档中查找的繁琐。&#8203;:contentReference[oaicite:0]{index=0}

 🗂️ .NET Framework 支持状态

| 版本范围           | 支持状态       | 支持结束时间             |
|--------------------|----------------|--------------------------|
| 1.x – 3.x          | ❌ 不再支持    | 2007–2011 年             |
| 3.5                | ✅ 操作系统支持 | 2029 年 4 月             |
| 4.0 – 4.6.1        | ❌ 不再支持    | 2022 年 4 月             |
| 4.6.2 – 4.7        | ⚠️ 操作系统支持（混合） | 视具体版本而定           |
| 4.8                | ✅ 操作系统支持 | 无明确结束日期（2031 年以后） |&#8203;:contentReference[oaicite:1]{index=1}

---

🚀 .NET Core 与 .NET（5 及以上）支持状态

| 版本   | 发布日期       | 支持结束日期     | 支持类型     |
|--------|----------------|------------------|--------------|
| 5      | 2020 年 11 月  | 2022 年 5 月     | ⚠️ STS（18 个月） |
| 6      | 2021 年 11 月  | 2024 年 11 月    | ✅ LTS（3 年）   |
| 7      | 2022 年 11 月  | 2024 年 5 月     | ⚠️ STS（18 个月） |
| 8      | 2023 年 11 月  | 2026 年 11 月    | ✅ LTS（3 年）   |
| 9      | 2024 年 11 月  | 2026 年 5 月     | ⚠️ STS（18 个月） |&#8203;:contentReference[oaicite:2]{index=2}

> ℹ️ 从 .NET 5 开始，Microsoft 采用了新的发布节奏：奇数版本为 STS（标准期限支持，18 个月），偶数版本为 LTS（长期支持，3 年）。

---

🔍 当前 .NET 支持状态概览

- **不再支持的版本：**
  - .NET Framework 1.x – 4.6.1
  - .NET Core 1.0 – 3.1
  - .NET 5

- **即将结束支持的版本：**
  - .NET 6（2024 年 11 月）
  - .NET 7（2024 年 5 月）

- **当前受支持的版本：**
  - .NET Framework 3.5（至 2029 年 4 月）
  - .NET Framework 4.8（操作系统支持，无明确结束日期）
  - .NET 8（至 2026 年 11 月）
  - .NET 9（至 2026 年 5 月）

---

🛠️ 建议与迁移指南

- **使用已停止支持版本的开发者
- **使用即将结束支持版本的开发者
- **使用 .NET Framework 的开发者

## 视频推荐

1、[Scott Hanselman和Mark Russinovich访谈系列节目](https://www.youtube.com/playlist?list=PL0M0zPgJ3HSf4XZvYgZPUXgSrfzBN26pf)

**Scott and Mark Learn To...** 是由 Scott 和 Mark 主持的系列视频，记录他们尝试学习各种新技能和活动的过程。每集视频中，他们都会挑战自己，尝试从零开始掌握一项新技能，分享学习过程中的乐趣、困难和收获。

2、[为什么雄心勃勃的.NET Aspire可能失败](https://www.youtube.com/watch?v=2L68EldtKFo)

该视频深入探讨了 .NET 平台中一项雄心勃勃的新功能，分析了其设计理念、潜在优势以及可能导致失败的风险因素。作者从技术实现、开发者社区反馈和实际应用场景等多个角度进行了全面评估。

1. 功能简介：介绍了该新功能的目标和预期带来的改进。
2. 技术挑战：分析了在实现过程中遇到的主要技术难题。
3. 社区反应：总结了开发者社区对该功能的看法和反馈。
4. 实际应用：探讨了该功能在现实项目中的适用性和表现。
5. 未来展望：预测了该功能的发展方向及其在 .NET 生态中的地位。

虽然该功能展示了 .NET 平台的创新精神，但其成功与否仍取决于技术实现的成熟度、社区的接受程度以及实际应用中的表现。开发者应密切关注其发展动态，权衡利弊后再决定是否采纳。

3、[用.NET Aspire构建跨云.NET应用程序](https://www.youtube.com/watch?v=yVgr6cRYOPk)

该视频深入探讨了 .NET Aspire 的功能，展示了如何利用该工具在多个云平台上构建和部署 .NET 应用程序。演示涵盖了从本地开发到跨云部署的完整流程，强调了 .NET Aspire 在简化多云环境开发中的优势。

1. .NET Aspire 简介：​.NET Aspire 是一个旨在简化 .NET 应用程序开发和部署的工具，特别是在多云环境中
2. 多云部署支持：​演示了如何使用 .NET Aspire 将应用程序部署到不同的云平台，如 Azure、AWS 和 Google Cloud，强调了其跨平台兼容性。
3. 本地开发与测试：​展示了如何在本地环境中使用 .NET Aspire 进行开发和测试，确保应用程序在部署前的稳定性和性能。
4. 集成与扩展性：​介绍了 .NET Aspire 如何与现有的 .NET 工具链集成，以及其扩展性，允许开发者根据项目需求进行定制。

.NET Aspire 提供了一种高效的方式来构建和部署 .NET 应用程序，特别适用于需要在多个云平台上运行的项目。​其简化的流程和强大的集成功能使开发者能够更专注于业务逻辑，而无需过多关注底层的部署细节。​


4、[基于.NET6的企业级Blazor应用程序](https://www.youtube.com/watch?v=GKu-vRxOWr8)

- GE FlightPulse
- Xplor SPOT
- Microsoft INTRACT - Insider Trading Compliance Platform
- Microsoft Dynamics 365 Connected Store


## 开源项目

1、[ZFS Reader C#实现](https://github.com/AustinWise/ZfsSharp)

**ZfsSharp** 是一个使用 C# 编写的实验性项目，旨在实现对 ZFS 文件系统的读取功能。该项目明确表示不支持写入操作。它支持多种磁盘映像格式，包括：

- 原始磁盘映像（raw）
- 虚拟硬盘（VHD）
- 虚拟硬盘扩展（VHDX）
- 虚拟磁盘映像（VDI）

此外，ZfsSharp 还支持多种虚拟设备（vdev）配置，如 RAIDZ、镜像（mirror）和条带化（stripe）。

2、[Esprima.NET](https://github.com/tgothorp/Gotho.BlazorPdf)

`Esprima.NET` 是 `esprima.org` 的 `.NET` 项目，可以通过 C# 代码解析 `JavaScript` 代码，通常用在词法分析和语法分析。

3、[Gotho.BlazorPdf](https://github.com/BenMorris/NetArchTest)

![Image](https://github.com/user-attachments/assets/e3ca80ea-fbfe-48d9-bbff-871760d87714)

Gotho.BlazorPdf 是一个为 Blazor 框架设计的简单而强大的 PDF 查看器组件。​最初作为 BlazorBootstrap PDF 查看器的移植版本，现已发展为独立项目，支持所有 Blazor 应用程序，并提供专门的 MudBlazor 集成包。

主要特性
PDF 查看与交互： 支持查看、打印、下载 PDF 文件。
绘图功能： 允许用户在 PDF 文档上绘图，打印时包含绘图内容。
本地化与主题支持： 运行时配置标签和颜色，支持动态切换语言和主题。
响应式设计： 优化小屏幕设备上的显示效果。
兼容性： 支持 .NET 8.0 或 9.0。

3、[AzureDesignStudio](https://github.com/chunliu/AzureDesignStudio)

📌 项目概述

**AzureDesignStudio** 是一个开源的 Web 应用程序，旨在简化和加速 Azure 架构设计流程。它允许用户通过可视化方式构建 Azure 解决方案架构图，并自动生成基础设施即代码（IaC），支持导出为 ARM 模板和 Bicep 文件。

✨ 核心功能

- **可视化设计**：使用一致且美观的样式，直观地创建 Azure 架构图。
- **架构校验**：确保设计符合 Azure 资源的规则和约束，减少错误。
- **导出功能**：将设计导出为图像，方便集成到文档和演示中。
- **云端存储**：将设计保存在云端，便于随时随地访问。
- **IaC 生成**：自动为设计生成 ARM 模板和 Bicep 文件，支持基础设施即代码实践。

🛠️ 技术栈

- **前端**：使用现代 Web 技术构建，提供响应式用户界面。
- **后端**：基于 C# 开发，处理业务逻辑和数据存储。
- **部署**：支持本地部署和云端部署，灵活适应不同需求。

4、 [Supersocket](https://github.com/kerryjiang/SuperSocket)

SuperSocket 是一个基于 .NET 的高性能、可扩展的套接字服务器应用程序框架。它提供了强大的架构，支持构建自定义网络通信应用程序，支持多种协议，包括 TCP、UDP 和 WebSocket。

- **多协议支持：** 支持 TCP、UDP 和 WebSocket 等多种协议，满足不同网络通信需求。
- **高性能：** 基于事件驱动的异步模型，支持高并发连接，适用于实时通信场景。
- **可扩展性：** 提供丰富的扩展点，支持自定义协议解析器、命令处理器等，方便集成和扩展。
- **跨平台：** 支持 Windows、Linux 和 macOS，适用于多种部署环境。
- **易于使用：** 提供简单的 API 和配置方式，降低开发门槛，加快开发速度。
- **丰富的文档和示例：** 提供详细的文档和示例，帮助开发者快速上手。

SuperSocket 的架构主要包括以下组件：

- **Session：** 表示与客户端的会话，管理连接的生命周期。
- **ReceiveFilter：** 负责解析接收到的数据，提取有效的请求信息。
- **Command：** 处理解析后的请求，实现具体的业务逻辑。
- **AppServer：** 管理服务器的启动、停止和配置，协调各个组件的工作。