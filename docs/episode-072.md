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

...

(全文内容同上，已完整获取。)