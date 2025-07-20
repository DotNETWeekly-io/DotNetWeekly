# .NET 每周分享第 70 期

## 卷首语

![Image](https://github.com/user-attachments/assets/de698bee-75e1-4191-8f80-8cc02a6b82ce)

Aaron Stannard 表示，他发现 Microsoft 在未经通知的情况下，硬性删除了他们在 NuGet.org 上的两个 Akka.NET 插件包（`Akka.Coordination.Azure` 和 `Akka.Discovery.Azure`），导致其 CI/CD 构建失败。

**背景与争议**

- 这些包并非直接引用 `Microsoft.Identity.Client`，而是间接依赖自 `Azure.Identity`，后者依赖上游存在拼写错误的 XML 注释，指向可用于钓鱼的 typo-squatting 网站，虽被认定为漏洞，但属于“文档级”问题，不会影响运行时。
- 微软在未提前通知的情况下强删第三方包，设置了一个危险先例：即便 NuGet 明文禁止硬删（除非版权、恶意内容等极端情况），但微软团队凭借特殊权限绕开流程 :contentReference[oaicite:3]{index=3}。

**主要担忧**

1. *包可用性信任崩塌*：开发者依赖 NuGet 进行持久分发，若包可能被任意删除，信任基础将坍塌。
2. *权限不对等*：仅微软拥有该“删包特权”，其他组织如 AWS、Google、Petabridge 无此能力。
3. *处理标准缺失*：为什么只是新版本被删除而旧版本保留？相关邮件未提及删包操作，流程不透明。

**社区与平台反应**

- 在 Reddit 上，大量用户明确指出，这种做法违背了 NuGet 的核心设计，对平台信任造成严重伤害。
- NuGet 团队承认“这是个错误”，已在 GitHub 讨论区重开该话题，并恢复了部分包。

虽然微软出发是为处理上游“文档漏洞”，但错误的处理方式引发广泛质疑：为何绕过 CVE 通知与弃用流程？NuGet 的删除机制如何在未来避免滥用？开发者亟待更清晰的机制、流程与公平政策。

## 文章推荐

1、[避免截图和 Recall 应用泄露信息](https://www.meziantou.net/how-to-exclude-your-windows-app-from-screen-capture-and-recall.htm)

该文章介绍了如何通过使用 `SetWindowDisplayAffinity` 函数并设置 WDA_EXCLUDEFROMCAPTURE 标志，将 Windows 应用程序从屏幕捕获和“Recall”功能中排除。作者通过 C# 代码示例展示了如何在 WPF 和 WinForms 应用程序中实现这一功能，并指出该标志可以根据需要启用或禁用，以便在显示敏感信息时进行隐藏。文章还提供了测试此功能的方法和相关资源。

2、[List 比 IList 迭代要快](https://steven-giesel.com/blogPost/c624c170-c16c-4414-8506-2ae25ba54df7/why-is-enumerating-over-list-faster-than-ilist)

为什么在 `List` 和  `IList` 在迭代的时候， `List<T>` 比` IList<T>` 更快的原因：

结构体与装箱枚举器： 当调用 `List<T>.GetEnumerator()` 时，它返回一个轻量级的 `Enumerator` 结构体。而 `IList<T>.GetEnumerator()` 返回的是这个枚举器的一个装箱版本，这会导致 40 字节的内存分配和一些性能开销。
虚方法调用开销： `IList<T>.GetEnumerator()` 涉及一个虚方法调用，这意味着运行时必须在调用正确的方法之前确定所使用的具体类型，这增加了性能损失。相比之下，`List<T>` 已经是一个具体类型，因此避免了这种开销。

| Method        | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| GetSumOfList  |  4.617 us | 0.0352 us | 0.0294 us |  1.00 |    0.01 |         - |          NA |
| GetSumOfIList | 12.306 us | 0.1243 us | 0.1163 us |  2.67 |    0.03 |      40 B |          NA |

3、[dotnet run app.cs 揭秘](https://andrewlock.net/exploring-dotnet-10-preview-features-1-exploring-the-dotnet-run-app.cs/)

在 .NET 10 Preview 4+ 中，引入了“文件级程序”（file‑based programs），允许你在没有 .csproj 项目的情况下运行单个 C# 文件，只需执行：

```bash
dotnet run app.cs
```
输出会像在常规项目中一样工作。

**主要功能**
- Shebang 支持：`#!/usr/bin/dotnet run` 或` #!/usr/bin/env dotnet` 可使脚本在 *nix 系统下直接执行。

- `#:` 指令：
- `#:sdk` ：设定 SDK（如 Microsoft.NET.Sdk 或 Microsoft.NET.Sdk.Web 等）；
- `#:package`：引入 NuGet 包，可使用通配符版本；
- `#:property`：设置 MSBuild 属性（如 UserSecretsId 等）；
- 预览版 6 将支持 `#:project` 以引用外部 .csproj 项目。

发布支持（Preview 6）：可以通过 dotnet publish app.cs 发布为 Native AOT，可使用 shebang 脚本而无需输入项目文件。

代码流输入：支持通过 stdin 执行，例如：

```bash
echo 'Console.WriteLine("Hello, World!");' | dotnet run -
```

**面向对象**
- 主要面向初学者和轻量脚本用途；
- 不需要管理项目文件，降低 C# 入门门槛；
- 不支持多文件场景、Visual Studio 集成，现阶段只能在 CLI 或 VS Code 使用；
- 仅支持 .cs 文件（暂不支持 .vb 或 .fs）


**核心流程**

1. 入口检测：
- 如果给定参数是 .cs 文件或者以 #! 开始，则认为它是单文件入口；
- 支持用 - 读取 stdin 内容。

2. 缓存机制：
- 构建时检查 global.json, NuGet.config 等变化；
- 如果这些元数据自上次构建后未改变，则复用缓存，加快构建。

3. 生成虚拟项目：
- 读取 `#:sdk`、`#:package`、`#:property` 等指令；
- 构造一个 in‑memory .csproj，并导入相应 props/targets；
- 包含 <Compile Include="app.cs" />、<Features>FileBasedProgram</Features> 等节点；
- 支持项目引用和 AOT 发布属性。

4. 构建执行：

- 使用 MSBuild API 分步调用 Restore 和 Build；
- 成功后生成 build-success.cache 提升下一次执行效率；
- 最终运行生成的可执行程序。

**设计意义**
- 利用虚拟 MSBuild 项目实现高度兼容，不需额外 CLI 或运行时；
- 一旦项目“成长”，可直接转换为完整 .csproj 项；
- 架构沿用现有构建流程，保障一致性。

4、[放弃使用 C# region](https://marcduerst.com/2016/10/03/c-regions-are-evil/)

文章探讨了C#中“区域”（Regions）的历史、问题及替代方案。区域最初用于折叠代码块，但随着C# 2.0中分部类的引入，其隐藏生成代码的功能已过时。作者认为，许多开发者在2016年仍使用区域是不良实践，因为区域会隐藏代码，使他人难以理解，并助长开发者逃避良好的代码结构。文章指出，如果一个类需要区域来分组，那它很可能违反了单一职责原则，应拆分为多个类；方法中的区域则表明该方法过长，应进行重构。作者提倡不使用区域，并建议借助工具管理现有代码库中的区域。

5、[按需输出日志](https://devblogs.microsoft.com/dotnet/emit-logs-on-demand-with-log-buffering/)

.NET 9 引入“日志缓冲”（log buffering）机制，可将日志先写入内存缓冲区，再依据运行时条件决定是否输出或丢弃，从而在正常运行时节省存储成本，仅在需要时输出详细日志。

**核心机制**

1. 日志按规则（分类、级别、事件 ID 等）缓存在内存中；
2. 满缓冲区后按策略丢弃旧日志；
3. 在特定时机（如捕获异常），调用 `Flush()` 将缓冲日志输出，或超时后自动丢弃。

**两种缓冲策略**

- **全局缓冲（Global）**：应用范围内统一缓冲，适用于长运行任务。可配置最大缓冲大小、记录大小、自动刷新时间等；
- **按请求缓冲（Per-request，ASP.NET Core）**：每个 HTTP 请求单独缓冲，请求结束时清除；出错时可触发 `PerRequestLogBuffer.Flush()`，同时也刷新全局缓冲。

**可动态配置**
支持运行时热加载配置（如调整缓冲规则或级别），无需重启应用。

**注意事项与适用场景**

- 仅适用于 .NET 9 及以上；
- 日志顺序可能不完全保留，但时间戳保留；
- 不支持记录 Log Scopes、ActivitySpanId 等特定属性；
- 占用内存换存储，适合通过异常或超时触发时才输出日志；
- 推荐与采样功能结合，将常规日志抽样输出，仅在异常时缓冲详细日志。

该机制适合“默认不输出但遇异常时输出”的日志策略，既能提升诊断效果，又能控成本。

6、[通过 nuget 发布 MCP Server](https://devblogs.microsoft.com/dotnet/mcp-server-dotnet-nuget-quickstart/)

![Image](https://github.com/user-attachments/assets/db132173-3ded-42cd-a2fe-8af6fb8a2ba3)

这篇题为“使用 .NET 构建首个 MCP 服务器并发布到 NuGet”的文章，旨在介绍模型上下文协议（MCP）并演示如何使用 .NET 10 构建、测试和发布 MCP 服务器到 NuGet。MCP 是一个开放标准，旨在连接 AI 助手与外部数据源和工具，从而实现 AI 模型与现实世界的互动。

文章强调，NuGet.org 现在支持托管和消费 MCP 服务器，这显著提升了这些 AI 工具的可发现性、版本管理能力和安装便利性。它提供了一份详尽的分步指南，首先列出了先决条件，如 .NET 10.0 SDK 和 NuGet.org 账户，然后指导读者安装 MCP Server 模板并创建新项目。指南中还详细解释了如何添加自定义工具（例如一个天气工具），以及如何配置服务器以与 GitHub Copilot 进行测试。

最后，文章详细阐述了 NuGet 发布流程，包括配置项目、打包项目，并将其发布到 NuGet。这一步骤使得服务器能够被更广泛的 .NET 社区发现和使用。文章结尾提出了实际的 MCP 服务器应用设想，并提供了进一步学习的资源，鼓励开发者探索 MCP 协议的更多可能性。

7、[很少人知道的的简化 C# 代码的方法](https://blog.elmah.io/lesser-known-c-features-that-can-simplify-your-code/)

c# 有些鲜为人知但是很使用的简化代码的方法

1. 记录类型（Records）
- 值类型、不可变、自动支持值比较。
```csharp
public record Coordinate(double Latitude, double Longitude);
```

2. 空合并运算符（??）

提供默认值，避免空引用异常。

```csharp
int result = marks ?? 0;
string name = username ?? "Guest";
```

3. 元组解构（Tuple Deconstruction）
支持多值返回与解构赋值。

```csharp
var (lat, lon) = (49.0232, -98.4822);
```

4. 全局 using（C# 10+）
在项目中统一声明命名空间, 减少每个文件重复 using，提升维护效率。

```csharp
global using System;
global using MyProject.Models;
```

5.  集合表达式（C# 12+
简洁初始化数组和列表，支持扩展语法。

```csharp
int[] nums = [3, 5, 8, 14];
int[] b = [..a, 23, 4];
```

6. 模式匹配
类型、属性、位置皆可匹配, 比传统条件判断代码更具表达力
```csharp
if (obj is int i) { ... }
if (result is { Marks: > 33 }) { ... }
point switch { (0,0) => "...", _ => "..." };
```

7. ref 返回与局部变量
直接返回数据引用并修改原始内容,减少复制、直接操作数据。

```csharp
ref int FindRef(int[] arr, int t) { ... }
ref int x = ref FindRef(arr, 6);
x = 99;
```

8. 类的主构造器（C# 12+）
将构造参数简洁集成到类定义中。

```csharp
public class Course(string name, string instructor) { ... }
```

9. dynamic 关键字
支持运行时类型绑定, 在处理不确定类型（如 JSON、反射）时更灵活。

```csharp
public void Foo(dynamic input) { ... }
```

10. 目标类型 new

简化对象实例化语法

```csharp
List<string> names = new();
```
11. 反射（Reflection）
运行时读取类型信息并动态处理。

```csharp
typeof(T).GetProperties();
```

12. Caller Information 特性

自动获取调用者文件、方法、行号信息。

```csharp
void Log(string msg,
         [CallerMemberName] string name = "",
         [CallerLineNumber] int line = 0,
         [CallerFilePath] string file = "") { }
```

13. switch 表达式
表达式风格简洁 switch
```csharp
string res = marks switch {
  >33 => "Pass",
  <=33 => "Fail",
  _ => "Invalid"
};


14. 文件作用域命名空间（C# 10+）
去除额外缩进，更扁平化组织文件。
```csharp
namespace MyApp.Services;
public class OrdersService { ... }
```

15.  范围与索引运算符
支持切片语法，如 [^2..] 获取数组末尾片段。

16. init‑only 与 required 属性（C# 9/11+）
支持对象初始化后不可变

```csharp
public class Player {
  public required string Name { get; init; }
}
```

## 视频推荐

1、[构造函数中异步调用](https://www.youtube.com/shorts/mxz5ycsqLek)

这个视频讨论了在 C# 中处理异步构造函数的技巧。视频建议避免在构造函数中直接调用异步方法，因为这可能导致线程阻塞和死锁。推荐的解决方案是将异步工作移出构造函数，并创建一个异步工厂方法来处理数据的加载。

```csharp
public class DataLoader
{
    public string Data { get; private set; }
    public DataLoader(string data)
    {
        Data = data;
    }

    public static async Task<DataLoader> CreateDataLoader()
    {
        string data = await LoadDataAsync();
        return new DataLoader(data);
    }
    
    private static async Task<string> LoadDataAsync()
    {
        // Simulate an asynchronous data loading operation
        await Task.Delay(1000);
        return "Loaded Data";
    }
}
```

---

## 开源项目

1、[pythonnet](https://github.com/pythonnet/pythonnet)

**Python.NET**（又称 pythonnet）是一个开源的跨平台框架，提供了Python与.NET（Framework/Core/Mono）之间的无缝集成，支持Python代码与.NET代码相互调用。

 🔧 主要功能

- **双向互操作**
  - **从Python调用.NET**
    通过`clr.AddReference(...)`加载.NET程序集，直接在Python中使用.NET类和方法。
  - **在.NET中嵌入Python**
    在C#程序中通过`PythonEngine.Initialize()`启动Python运行时，使用`Py.GIL()`进行线程安全管理，调用Python脚本或库。

- **跨平台支持**
  支持Windows (.NET Framework/.NET Core)、Linux和macOS (Mono/.NET Core)，通过环境变量`PYTHONNET_RUNTIME=coreclr`显式指定使用.NET Core。

- **保留原生CPython性能**
  直接使用CPython运行时，支持NumPy、SciPy等原生C扩展库，提供更高的运行效率。

- **支持最新Python版本**
  支持Python 3.7至3.13版本，目前最新版为v3.0.5，已增加对Python 3.13的支持。

- **MIT开源许可**
  项目托管于.NET基金会，采用宽松的MIT开源协议，社区活跃度高。

🛠️ 典型应用场景

1. **在Python中调用.NET库**
   用Python快速编写脚本，调用已有的.NET类库（C#/F#/VB.NET）。

2. **在.NET程序中嵌入Python**
   将Python作为扩展插件引擎嵌入到.NET程序中，提供更强大的扩展能力。

3. **混合开发解决方案**
   用于建筑信息建模（如Dynamo BIM）领域，实现Python与.NET的混合开发。

2、[Corvus.JsonSchema](https://github.com/corvus-dotnet/Corvus.JsonSchema/)

**Corvus.JsonSchema** 是由 Endjin 团队维护的高性能开源库，提供对 JSON Schema 的编译时类型生成和验证支持，支持从 Draft‑4 到 Draft‑2020‑12（包括 OpenAPI 3.0/3.1）。

📦 核心功能

- **代码生成（Build‑time Code Generation）**  
  使用 `Corvus.Json.CodeGenerator` 或 `Source Generator` 工具，将 JSON Schema 文件转换为强类型 C# 类，具有 IntelliSense 和零内存分配特性。

- **运行时验证（Runtime Validation）**  
  生成的类自带高效的验证方法，可在运行时校验 JSON 数据是否符合 Schema 定义。

- **序列化支持**  
  可使用生成类在 `System.Text.Json` 环境下高效序列化和反序列化 JSON 数据。

⚙️ 支持平台

- **.NET Standard 2.0**：兼容 .NET 4.8.1 及以上版本  
- **.NET 8.0+**：支持跨平台（Windows、Linux、macOS）并提供优化的 `net80` 包。

📜 支持的 Schema 方言

全面支持多个 JSON Schema 草案版本和 OpenAPI 规范：

- Draft‑4（含 OpenAPI 3.0 变体）  
- Draft‑6  
- Draft‑7  
- 2019‑09  
- 2020‑12（含 OpenAPI 3.1）

详细的实现覆盖情况可以在 Bowtie 基准页面查看。