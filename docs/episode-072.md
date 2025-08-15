# .NET 每周分享第 72 期
## 行业资讯
1、 [.NET Conf 2025 内容招募](https://devblogs.microsoft.com/dotnet/dotnet-conf-2025-announcing-the-call-for-content/)
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
1、 [在浏览器中运行 .NET 但不使用 Blazor](https://andrewlock.net/running-dotnet-in-the-browser-without-blazor/)
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


2、 [.NET 开发者学会的 Prompts](https://devblogs.microsoft.com/dotnet/5-copilot-chat-prompts-dotnet-devs-should-steal-today/)
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
Facet 是一個 C# Source Generator，允許開發者直接從領域模型在編譯期產生輕量投影類型（DTO、ViewModel、API Model 等）。只需在目標類型上標註 `[Facet(typeof(Source))]`，即可自動生成帶建構子、屬性及可選 LINQ 投影的 partial class/record/struct，全程零執行期開銷。

主要特點：
• 透過「Facetting」機制排除或保留屬性，建立專注視圖，消除手寫 DTO、Mapper 與 Select 的重複程式碼。
• 生成 `Expression<Func<TSource,TTarget>>` 靜態投影，可與 Entity Framework Core 等 LINQ Provider 無縫結合。
• 支援自訂同步 (`IFacetMapConfiguration`) 與非同步 (`IFacetMapConfigurationAsync`) 映射，能利用 DI 注入外部服務並完整支援 CancellationToken。
• 提供平行處理與集合轉換 API，兼顧效能與易用性。

生態系 NuGet 套件：
1. Facet：核心產生器與 API。
2. Facet.Extensions：與任何 LINQ Provider 通用的投影/映射擴充方法。
3. Facet.Extensions.EFCore：針對 EF Core 6+ 提供 `ToFacetsAsync`、`SelectFacets` 等非同步擴充。
4. Facet.Mapping：進階靜態或 DI 驅動映射設定，支援同步、非同步與平行轉換。

快速使用：
```csharp
_dotnet add package Facet
[Facet(typeof(User))]
public partial class UserDto { }

var dto = user.ToFacet<User, UserDto>();
var dtos = users.SelectFacets<User, UserDto>();
```
若需自訂欄位或 I/O 操作，可撰寫對應的 Mapper 類別並於屬性中指定；於 EF Core 查詢鏈中使用 `ToFacetsAsync`，僅拉取必要欄位以降低 IO，生成的 Expression 亦可被轉譯為最佳化 SQL。

總結：Facet 以編譯期程式碼產生消弭樣板，讓 .NET 開發者能在保持型別安全與高效能的同時，快速建立各式 DTO/視圖與映射流程。