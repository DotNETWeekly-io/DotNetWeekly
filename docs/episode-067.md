# .NET 每周分享第 67 期

## 卷首语

社区人员发现，在某些 .NET 官方文档页面中，出现了关于使用 GitHub Copilot 的提示，例如：[自定义属性名称和顺序](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/customize-properties#use-github-copilot-to-customize-property-names-and-order), [使用 Copilot 进行单元测试](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-copilot)。 他们认为，这些提示可能被视为对 GitHub Copilot 的推广，尤其是在 .NET Foundation 自称为“独立、非营利组织”的背景下。他质疑：如果文档中推广第三方工具（即使是免费的），是否违背了其独立性的承诺？此外，Copilot 并非完全免费，用户可能需要以数据和隐私为代价。他还指出，如果要保持中立，是否也应该在文档中提及其他类似工具？

## 行业资讯

1、[Microsoft Build 2025](https://devblogs.microsoft.com/dotnet/join-us-at-build-2025/)

![Image](https://github.com/user-attachments/assets/291895a3-03c5-4128-b8c0-f541e21771e3)

Microsoft Build 2025 将于 5 月 19 日至 22 日在西雅图及线上举行，面向全球开发者开放。.NET 与 C# 团队将带来超过 75 场精彩内容，涵盖 AI、云计算、应用现代化等主题。

重点议程包括：

- .NET Aspire 与 AI 集成：探索 .NET Aspire 9.3 的新特性，展示如何结合 AI 构建智能应用。
- GitHub Copilot 助力应用现代化：介绍如何使用 Copilot 升级 .NET 应用，实现自动修复与依赖分析。
- Hanselman 与 Toub 的实战演示：两位专家现场调试代码，深入讲解 .NET 性能优化技巧。
- Python 与 .NET 融合：展示如何将 Python 的数据科学能力与 .NET 的企业级开发结合，构建 AI 解决方案。

无论您是经验丰富的开发者，还是初学者，都能在本次大会中收获丰富的知识与实践经验。欢迎通过线上或线下方式参与，与全球开发者共同交流学习。

## 文章推荐

1、[使用反射的四种场景](https://blog.elmah.io/4-real-life-examples-of-using-reflection-in-c/)

反射是 C# 的一项运行时功能，允许程序动态地检查和操作程序集、类型（如类、接口、值类型）、方法、字段和属性的元数据。通过 System.Reflection 命名空间，开发者可以在运行时创建对象实例、调用方法以及加载程序集。

反射适用于以下场景：

- 需要根据外部输入在运行时加载程序集或类型。
- 处理结构未知的 JSON 或 XML 序列化。
- 自动将具有相同名称或属性的属性之间的数据进行映射。
- 开发依赖注入（DI）容器以解析构造函数、注入参数并激活服务。
- 导出数据到 CSV 时，手动映射大量类型的属性不切实际。
- 测试工具（如 NUnit 或 xUnit）使用反射发现测试方法和属性，而无需手动注册每个测试用例。
- 避免为重复的类型方法和属性编写样板代码。反射可以帮助自动访问属性或调用方法。
- 动态访问和验证模型属性。

常见的方法有

1. 方法的调用
2. 动态 CSV/JSON 的导出
3. 自定义类型之间的转换映射
4. 动态插件/程序集加载

虽然反射功能强大，但会带来性能开销。应遵循以下最佳实践：

- 避免在紧密循环中使用反射。
- 在性能关键的代码中，避免使用反射。
- 重复调用 `GetMethod()` 和 `GetProperty()` 可能代价高昂。使用缓存来存储反射结果。
- 使用编译的委托或表达式树来避免重复反射。这可以显著加快性能关键路径中的属性访问或方法调用：

2、[在 System.Text.Json 中使用 Source Generation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)

`System.Text.Json` 的源生成功能（Source Generation）自 .NET 6 起引入，旨在提升 JSON 序列化与反序列化的性能，减少运行时反射的开销。

使用步骤如下：

- 创建一个继承自 `JsonSerializerContext` 的部分类。
- 在该类上应用 `[JsonSerializable]` 特性，指定需要序列化或反序列化的类型。
- 在代码中使用 `JsonSerializer` 的方法，并传入生成的上下文实例或类型信息。

3、[C# 中的模式匹配](https://www.arungudelli.com/csharp-tips/pattern-matching-in-csharp/)

模式匹配是一种机制，允许你将输入值与模式进行比较，并在匹配时执行相应操作。在 C# 中，模式匹配可用于以下结构：

- is 表达式
- switch 语句
- switch 表达式

主要使用类型有如下表格

模式类型 | 最佳使用场景
-- | --
声明/类型 | 类型检查和转换
常量 | 精确值匹配
关系 | 范围比较
逻辑 | 复杂组合
属性 | 匹配对象的属性值
位置 | 基于解构的匹配
var | 通用值提取
丢弃 | 匹配任何内容，忽略值
列表 | 匹配数组/列表及子序列

根据作者的经验，模式匹配在以下情况下表现出色：

- 处理异构类型（例如 object 参数）时
- 避免冗长的空值检查。
- 使条件语句更具可读性。
- 处理复杂的分支逻辑（例如转换 API 响应）时。

4、[Azure Function 构建 MCP Remote Server](https://devblogs.microsoft.com/dotnet/build-mcp-remote-servers-with-azure-functions/)

本文介绍了如何利用 [Azure Functions](https://learn.microsoft.com/azure/azure-functions/) 构建一个MCP远程服务器，用于处理来自远程设备的请求。它强调了以下关键点：

**背景**

- 某些场景需要与远程设备（如物联网设备）通信。
- 这些设备可能不支持现代协议（如 HTTPS 或 gRPC），需要简化的接口。
- Azure Functions 是构建轻量级、事件驱动的后端服务的理想选择。

**架构概述**

- 使用 Azure Functions 提供一个 HTTP 端点，供远程客户端请求。
- 结合 Azure Durable Entities 管理设备状态和指令执行。
- 结合 Azure Storage 实现数据持久化。

**示例实现**

- 提供了基于 .NET 的函数模板，包括：
  - `DeviceCommandFunction`: 接收和处理设备命令。
  - `DeviceStateEntity`: 保存设备状态。
  - `DurableEntityClient`: 调用实体以管理设备状态。

**优势**

- 减少基础设施负担：无需管理服务器。
- 易于扩展：可自动扩容应对大量设备。
- 成本高效：使用按需计费的无服务器架构。

结论
Azure Functions 是构建简洁、高效、可扩展的 MCP 远程服务器的理想工具，适用于各种边缘计算或远程设备通信场景。

5、[使用向量化加速 C# 性能](https://btburnett.com/csharp/2024/12/09/using-vectorization-in-csharp-to-boost-performance)

SIMD（单指令多数据）是一种并行处理技术，允许 CPU 在一个指令周期内处理多个数据项。在 .NET 中，SIMD 通过 Vector<T> 类型实现，例如 `Vector128<T>`、`Vector256<T>` 等，这些类型根据 CPU 的支持情况提供不同大小的向量操作。
例如，使用传统方法对整数数组求和：

```csharp
public int Sum(ReadOnlySpan<int> values)
{
    int accumulator = 0;
    foreach (var i in values)
    {
        accumulator += i;
    }
    return accumulator;
}
```
在一次即兴的 SIMD 教学中，作者查看了 .NET 源代码中 Sum 方法的实现，发现它并未使用向量化：

```csharp
private static TResult Sum<T, TResult>(ReadOnlySpan<T> span)
    where T : struct, INumber<T>
    where TResult : struct, INumber<TResult>
{
    TResult sum = TResult.Zero;
    foreach (T value in span)
    {
        checked { sum += TResult.CreateChecked(value); }
    }
    return sum;
}
```
这让作者认为有机会通过向量化来优化该方法。作者编写了使用 `Vector<T>` 的向量化版本，并在性能测试中取得了数量级的提升。然而，他意识到原始方法使用了 checked 关键字来进行溢出检查，而 SIMD 加法操作默认忽略溢出，因此新方法在功能上并不等价。
为了在向量化的同时保留溢出检测，作者研究了二进制补码的原理，并使用位操作来检测溢出：

```csharp
int sum = x + y;
bool isOverflow = (sum ^ x) & (sum ^ y) & int.MinValue != 0;
```

作者的优化被合并到了 .NET 8 中，并在 Stephen Toub 的年度性能改进博客中提及。对于 1024 个 32 位整数的求和操作，性能从 .NET 7 的 347.28 纳秒提升到了 .NET 8 的 78.26 纳秒，提升了约 77%。

6、[构建 Web API 常见的 5 类错误](https://stefandjokic.tech/posts/building-apis-top-5-mistakes)

构建 Web API 看似简单，实则复杂。本文总结了开发者在构建 API 时常犯的五个错误，并提供了相应的解决方案，旨在帮助开发者构建更健壮、可维护的 API。

错误一：不校验输入
开发者总是信任用户的输入，这样会导致程序崩溃或者安全漏洞
解决办法：

- null 或者空字段校验
- 格式认证
- 业务逻辑认证

错误二：忽视版本控制
许多开发者在设计 API 时未考虑版本控制，导致后续更新难以兼容旧客户端。
解决方案：

- 在 URL 或请求头中明确指定 API 版本，例如 /api/v1/。


错误三：错误的状态码使用
返回不准确的 HTTP 状态码会导致客户端误解响应结果。
解决方案：
正确使用 2xx、4xx、5xx 等状态码，例如：

- 200 OK：请求成功。
- 400 Bad Request：请求参数错误。
- 401 Unauthorized：未授权访问。
- 404 Not Found：资源不存在。
- 500 Internal Server Error：服务器内部错误。
确保错误响应中包含详细的错误信息，便于客户端调试。

错误四：复杂的返回类型
将数据库中的实体对象返回给请求，会导致数据库结构泄露，暴露敏感字段，导致返回请求的 body 过大
解决办法

- 返回数据库对象的的子集和必要字段

错误五：没有中心化的错误处理方式
每个 API 处理逻辑自己定义了错误逻辑，导致非常混乱。
解决方案：

- 使用全局错误处理中间件

## 视频推荐

1、[Null 对象的最新语法](https://www.youtube.com/watch?v=HMSfIkYI5ls&ab_channel=NickChapsas)

C# 的 nullable reference 类型有了新的语法，之前 `T?` 类型只能获取值的方式，而不能进行赋值，举例而言

```csharp
class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
}
Student? student = null;
Console.WriteLine(student?.Name)
```

但是如果我们想要更新 `student` 对象的时候，需要通过这种方式进行判断

```csharp
if (student != null)
{
    student.Age = age;
}
```

现在新的语法 (preview 阶段) 的可以省略 `null` 判断

```csharp
student?.Age = age;
```

2、[如何不用dockerfile容器化一个.NET 应用程序](https://www.youtube.com/watch?v=PtGTU7thBuY)

🎯 视频概述
本视频介绍了如何在不编写 Dockerfile 的情况下，将 .NET 应用容器化。Julio Casal 通过演示，展示了使用 .NET CLI 工具和内置功能，简化容器化流程，适用于希望快速部署 .NET 应用的开发者。

🧰 主要内容

1. 使用 .NET CLI 进行容器化
介绍了如何使用 dotnet publish 命令与相关参数，生成容器镜像，无需手动编写 Dockerfile。
2. 配置容器参数
演示了如何通过项目文件（.csproj）或命令行参数，设置容器的基础镜像、端口等配置。
3. 多平台支持
讨论了如何构建支持多平台的容器镜像，确保应用在不同环境中的兼容性。
4. 部署与运行
展示了如何将生成的容器镜像部署到本地或云端环境，并运行 .NET 应用

✅ 优点

- 简化了容器化流程，降低了入门门槛。
- 减少了对 Dockerfile 的依赖，适合快速开发和测试。
- 利用 .NET CLI 工具，提升了开发效率。

⚠️ 注意事项

- 对于需要复杂配置的生产环境，仍建议使用自定义的 Dockerfile。
- 了解生成的容器镜像的结构和配置，有助于更好地调试和优化。

3、[param 参数支持 Span](https://www.youtube.com/watch?v=xqk_ZabcM1M&ab_channel=NickChapsas)

C# 支持 `param` `参数修饰符，用来表示可变长度参数，这样在调用方法的时候，可以无需构造数组，直接挨个传入对象即可。但是我们知道这个一个语法糖，编译器会在方法调用之前构造一个数组，然后将数组参数传入。
这样就会导致显而易见的问题，就是出现了堆内存分配的问题，如果对于性能敏感的路径，可以选择使用 `Span<T>` 而不是 `T[]` 作为参数类型，可以避免内存分配

```csharp
[MemoryDiagnoser]
public class ParamBenchmarks
{
    private static void UseParamArray(params string[] paramArray)
    {
        foreach (var item in paramArray)
        {
        }
    }

    private static void UseParamsSapn(params Span<string> paramArray)
    {
        foreach (var item in paramArray)
        {
        }
    }

    [Benchmark]
    public void TestParamArray()
    {
        UseParamArray("Hello", "World");
    }

    [Benchmark]
    public void TestParamsSpan()
    {
        UseParamsSapn("Hello", "World");
    }
}
```

结果如下

| Method         | Mean     | Error     | StdDev    | Median   | Gen0   | Allocated |
|--------------- |---------:|----------:|----------:|---------:|-------:|----------:|
| TestParamArray | 3.315 ns | 0.0928 ns | 0.2736 ns | 3.188 ns | 0.0048 |      40 B |
| TestParamsSpan | 1.360 ns | 0.0072 ns | 0.0060 ns | 1.357 ns |      - |         - |

4、[为项目选择最佳.NET UI框架](https://www.youtube.com/watch?v=jIVzKKi0414)

![Image](https://github.com/user-attachments/assets/d6d71cc4-ab62-4d34-9563-370e706d4930)

该视频由 .NET Foundation 发布，主题为“为你的项目选择最佳的 .NET UI 框架”。视频深入探讨了多种 .NET 用户界面框架的特点、适用场景和选择建议，帮助开发者根据项目需求做出明智决策。
视频内容概览：

1. 主要 .NET UI 框架介绍：
- Windows Forms：适用于传统桌面应用，开发简单，适合快速原型设计。
- WPF（Windows Presentation Foundation）：支持丰富的用户界面和数据绑定，适合需要复杂界面的桌面应用。
- UWP（Universal Windows Platform）：面向 Windows 10 及以上平台，支持多设备协同。
- MAUI（.NET Multi-platform App UI）：.NET 6 推出的跨平台框架，支持 iOS、Android、Windows 和 macOS。
- Blazor：基于 Web 的 UI 框架，支持 WebAssembly 和服务器端渲染，适合构建现代 Web 应用。
2. 框架对比与选择建议：
- 平台支持：根据目标平台选择合适的框架，例如 MAUI 适合跨平台需求，WPF 适合 Windows 桌面应用。
- 开发体验：考虑开发工具、社区支持和学习曲线，选择最适合团队的框架。
- 性能与功能：评估框架的性能表现和提供的功能特性，确保满足项目需求。
3. 实际案例分析：
视频中可能包含一些实际项目的案例，展示不同框架在实际应用中的表现和优势。
4. 未来发展趋势：
探讨 .NET UI 框架的未来发展方向，如 MAUI 的持续演进和 Blazor 在 Web 开发中的应用前景。

## 开源项目

1、[csharpier](https://github.com/belav/csharpier)

![Image](https://github.com/user-attachments/assets/92705d55-6d22-4781-b1ff-6ffd670c915a)

CSharpier 是一个为 C# 和 XML 设计的“有主见”（opinionated）代码格式化工具，旨在通过统一的格式规则提升代码一致性和可读性。它借鉴了 [Prettier](https://prettier.io/) 的理念，使用 Roslyn 解析代码，并根据自身规则重新打印代码，自动处理缩进、换行和空格等格式细节。

✨ 特性

- 统一格式：自动处理缩进、换行和空格，确保代码风格一致。
- 最小配置：仅提供少量配置选项（如缩进宽度、使用空格或制表符），避免团队在格式问题上的争论。
- 快速高效：性能优异，适用于大型项目。
- 广泛集成：支持 Visual Studio、VS Code 等主流 IDE，可设置为保存时自动格式化。
- CI/CD 支持：可作为预提交钩子或在持续集成流程中使用，确保提交代码符合格式规范。
- XML 支持：从 1.0.0 版本起，支持格式化 .csproj、.xml、.config 等 XML 文件。