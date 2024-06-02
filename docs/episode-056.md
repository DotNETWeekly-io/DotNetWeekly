# .NET 每周分享第 56 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/43a5200e-6bbe-425a-8851-27449cc0b566)

Microsoft Build 大会 .NET 内容[汇总](https://devblogs.microsoft.com/dotnet/dotnet-build-2024-announcements/)。

## 行业资讯

1、[API 文档源码查询](https://devblogs.microsoft.com/dotnet/dotnet-docs-link-to-source-code/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/4705f569-d5ca-4f2e-a061-eaaca794e8d4)

之前我们分享过 `.NET` 官方文档中现在提供了 `GitHub` 源码查询的链接，这样大大方便了我们了解更多的实现细节。这篇文章介绍了 `.NET` 组是如何开发这个功能的。

## 文章推荐

1、[Aspire 和 docker 比较](https://www.growthaccelerationpartners.com/blog/simplifying-cloud-native-net-development-net-aspire-vs-docker)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/8c74f940-91b8-41e4-bb29-42d8923538de)

| 功能 | .NET Aspire | Docker |
| --- | --- | --- |
| 编排 | 提供用于管理应用程序组成、服务发现和本地开发环境连接字符串的抽象。这简化了复杂分布式应用程序的设置。 | 主要专注于容器运行时和管理。复杂应用程序的编排通常涉及额外的工具，如Docker Compose或Kubernetes。 |
| 云原生组件 | 提供标准化与流行云服务（Redis、PostgreSQL、Azure Service Bus等）集成的NuGet包（组件）。这简化了设置和配置过程，包括健康检查、遥测和服务发现。 | 虽然Docker可以运行任何容器化服务，但与特定云服务的集成通常通过应用程序代码中的配置或通过单独的工具来处理。 |
| 工具和模板 | 提供适用于Visual Studio和dotnet CLI的项目模板和工具，专为云原生应用程序开发而设计。这包括默认配置和服务的样板代码，如健康检查、遥测和服务发现，加速开发。 | 主要是命令行工具（docker）和相关工具（Docker Compose等）。与IDE的集成可能需要额外的扩展或插件。 |
| 目标用户 | 主要是构建云原生应用程序的.NET开发人员。它假设了解.NET概念，并提供更高层次的抽象，以简化云原生开发体验。 | 从事容器化应用程序的开发人员、DevOps工程师和系统管理员。Docker需要对容器及其管理有更深入的了解。 |
| 生态系统 | 与.NET生态系统和Microsoft Azure云服务紧密集成。 | 一个被广泛采用的行业标准，拥有支持容器化的庞大工具、平台和服务生态系统。 |

2、[ASP.NET Core 中的短路](https://dev.to/moh_moh701/introduction-to-shortcircuit-and-mapshortcircuit-in-net-8-12ml)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/73542067-e404-4a13-aeda-4d719204b3aa)

在 `ASP.NET Core` 中每个请求的处理流程如下：
- 找到相应的 route 定义
- 链式处理每个中间件
- 找到对应的 `Endpoint` 并且处理

在 `ASP.NET Core 8` 中引入的 `Short Circuit`。顾名思义，我们定义好一些请求可以不用经过各种 `Middleware` 而直接处理并且返回，比如像一些静态文件处理，我们可以直接返回结果，通常是为了下述考虑：

- 性能优化
- 资源有效性
- 简化路由

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseMiddleware<CustomerMiddleware>();
app.MapGet("/shortcircuit", () => "Hello From Short Circuit").ShortCircuit();
app.Run();

class CustomerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public Task Invoke(HttpContext context)
    {
        System.Console.WriteLine("Customer Middleware");
        return _next(context);
    }
}
```

当我们访问 `shortcircuit` 路径的时候，`CustomerMiddleware` 并不会执行。除此之外，还有 `MapShortCircuit` 拓展方法

```csharp
app.MapShortCircuit(500, "robots.txt", "favicon.ico");
```

这里定义了所有访问 `robots.txt` 和 `favicon.ico` 路径的请求都会返回 500.

3、[6 条 C# 性能提升的建议](https://www.code4it.dev/blog/top-6-string-performance-tips/)

1. StringBuilder Vs String concatenation 
- 如果只有几个字符串，使用字符串拼接
- 如果有很多字符串，使用 `StringBuilder` 

2. EndsWith(string) Vs EndsWith(char) 

`EndsWith(char)`  性能比较好

3.  IsNullOrEmpty Vs IsNullOrWHitespace Vs IsNullOrEmpty + Trim

- `StringIsNullOrWhitespace` 性能比 `StringIsNullOrEmpty` 慢
- 如果数据是来在外部，使用 `StringIsNullOrWhitespace`
- 如果认为 `\n \n\t` 是合法字符，使用 `StringIsNullOrEmpty` 

4. ToUpper Vs ToUpperInvariant  和 ToLower Vs ToLowerInvariant 

- `ToUpper` 方法通常比 `ToLower` 慢
- `Invariant` 方法通常比 `non-invariant` 慢

5. OrdinalIgnoreCase Vs InvariantCultureIgnoreCase

- `InvariantCultureIgnoreCase` 比 `OrdinalIgnoreCase` 慢， 因为 `InvariantCultureIgnoreCase` 检查字符串的意义

```csharp
var s1 = = "Straße"; // German for "street"
var s2 ="STRASSE";

string.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase); //True
string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase); //False
```

6. Newtonsoft Vs System.Text.Json

`System.Text.Json` 在时间和内存消耗上比 `Newtonsoft` 有更好的表现

4、[不使用 Interface 而完成单元测试的方法](https://www.code4it.dev/blog/unit-tests-without-interfaces/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/db35ce69-22e4-4c74-af6e-ccf40514a28a)

对于单元测试，通常的做法是将一些外部依赖抽象成接口，这样可以通过接口不同的实现来进行单元测试。但是 `C#` 除了接口，还可以通过其他方式完成单元测试：

1. Virtual

C# 中 `Virtual` 关键字用来表示该属性或者方法可以在子类中进行重载，所以在单元测试中可以构造新的子类来重写部分方法。

```csharp
public class NumbersRepository
{
    private readonly int[] _allNumbers;
    public NumbersRepository(){
        _allNumbers = Enumerable.Range(0, 100).ToArray();
    }
    public virtual IEnumerable<int> GetNumbers() => Random.Shared.GetItems(_allNumbers, 50);
}

public class NumbersSearchService
{
    private readonly NumbersRepository _repository;
    public NumbersSearchService(NumbersRepository repository) {
        _repository = repository;
    }
    public bool Contains(int number){
        var numbers = GetNumbers();
        return numbers.Contains(number);
    }
    public IEnumerable<int> GetNumbers() => _repository.GetNumbers();
}

// 单元测试
internal class StubNumberRepo : NumbersRepository
{
    private IEnumerable<int> _numbers;
    public void SetNumbers(params int[] numbers) => _numbers = numbers;
    public override IEnumerable<int> GetNumbers() => _numbers;
}

[TestMethod] 
public void Should_WorkWithStubRepo() {
  // Arrange
  var repository = new StubNumberRepo();
  repository.SetNumbers(1, 2, 3);
  var service = new NumbersSearchService(repository);
  // Act
  var result = service.Contains(3);
  // Assert
  Assert.AreEqual(result, true);
}
```

2. New

C# 中 `new` 关键字也能隐藏父类的方法和属性，这样我们只需要测试其他内容就可以完成单元测试

```csharp
public class NumbersSearchService {
  private readonly NumbersRepository _repository;
  public NumbersSearchService(NumbersRepository repository) {
    _repository = repository;
  }
  public bool Contains(int number) {
    var numbers = GetNumbers();
    return numbers.Contains(number);
  }
  public IEnumerable<int> GetNumbers() => _repository.GetNumbers();
}

internal class StubNumberSearch : NumbersSearchService {
  private IEnumerable<int> _numbers;
  private bool _useStubNumbers;

  public void SetNumbers(params int[] numbers) {
    _numbers = numbers.ToArray();
    _useStubNumbers = true;
  }

  public new IEnumerable<int> GetNumbers() => _useStubNumbers ?
      _numbers:base.GetNumbers();
}
```

在单元你测试中只需要测试 `StubNumberSearch` 即可。

3. Moq

`Moq` 是 `.NET` 社区广泛使用的单元测试框架，使用 `Moq` 可以很方便地构造测试子类

```csharp
public class NumbersRepository {
  private readonly int[] _allNumbers;

  public NumbersRepository() {
    _allNumbers = Enumerable.Range(0, 100).ToArray();
  }

  public virtual IEnumerable<int> GetNumbers() => Random.Shared.GetItems(
      _allNumbers, 50);
}

[TestMethod]
public void Should_WorkWithMockRepo() {
  // Arrange
  var repository = new Moq.Mock<NumbersRepository>();
  repository.Setup(_ => _.GetNumbers()).Returns(new int[]{1, 2, 3});
  var service = new NumbersSearchService(repository.Object);

  // Act
  var result = service.Contains(3);

  // Assert
  Assert.AreEqual(result, true);
}
```

5、[Hanselman & Toub 现场代码秀](https://www.youtube.com/watch?v=TRFfTdzpk-M&ab_channel=MicrosoftDeveloper)

今年 `Build` 大会，`Scott Hanselman` 和  `Stephen Toub` 这对老搭档又来整活，他们通过对 [Humanizer](https://github.com/Humanizr/Humanizer) 这个库的优化，来提升 C# 代码性能。

- Span

```diff
- value.Substring(0, length - truncationString.Length) + truncationString
+ String.Concat(value.AspSpan(0,  length - truncationString.Length), truncationString.AsSpan())
```

- ReadOnlySpan Parameter

```diff
- public static string Transform(this string input, params IStringTransformer[] transformers)
+ public static string Transform(this string input params ReadonlySpan<IStringTransformer> transformers)
```

- Regex 

```diff
- private static readonly Regex ValidRomanNumeral =  new Regex("^(?i:(?=[MDCLXVI])((M{0,3})((C[DM])|(D?C{0,3}))?((X[LC])|(L?XX{0,2})|L)?((I[VX])|(V?(II{0,2}))|V)?))$", RegexOptionsUtil.Compiled);
+ [GeneratedRegex("^(?i:(?=[MDCLXVI])((M{0,3})((C[DM])|(D?C{0,3}))?((X[LC])|(L?XX{0,2})|L)?((I[VX])|(V?(II{0,2}))|V)?))$", RegexOptions.IgnroeCase)]
+ private static partial Regex ValidRomanNumeral();
```

- Switch Pattern

```diff
- private static readonly IDictionary<string, int> RomanNumerals =
-    new Dictionary<string, int>(NumberOfRomanNumeralMaps){
-        {"M", 1000},  {"D", 500},  {"C", 100},  {"L", 50},  {"X", 10}, , {"V", 5},   {"I", 1}};
+ private static int GetValue(char c) =>(c & 0x20) switch {
+    'M' => 1000, 'D' => 500, 'C' => 100, 'L' => 50,
+    'X' => 10,   'V' => 5,   'I' => 1 };
```

6、[升级的你的 .NET 应用程序，以 AWS 为例](https://www.youtube.com/watch?v=zb7v3d7IgYs)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/add62da1-461a-4472-97db-cdf9b17e975b)

如果说你的应用程序是 `.NET Framework` + `SQL Server` 的架构，并且部署在 `On premise` 上，那么你的应用程序就像**囚犯**一样：

- 受限于 `on-premise` 无法动态扩展
- 被 Windows 和 `SQL Server` 授权费用牵制
- 受困于服务器管理
- 受整体架构的限制

使用 `AWS` 和相关工作可以帮助解决上述问题

**阶段一**

- SQL Server 迁移到 SQL Server on EC2 
- 应用程序迁移到 Virtaul Machine (Amazon EC2) 或者 Windows Container (Amazon EKS)

**阶段二**

- 迁移 `.NET Framework` 到 `.NET Core` 
- 迁移 `SQL Server` 到开源数据库，比如 `Amazon Aurora`, `PostgreSQL` 或者 `MySQL`

**阶段三**

- 将服务进行微服务化，使用 `Linux Container` 
- 将数据库中特定服务中数据拆分，比如 `Amazon DynamoDB` 

**阶段四**

- 云原生重构，比如 `Serverless`, `Event-driven`

7、[当开源维护者不再更新 NuGet 包怎么办？](https://www.thereformedprogrammer.net/how-to-update-a-nuget-library-once-the-author-isnt-available/)

这是一篇令人难受的文章，作者是一名 `.NET` 开源库的维护者。但是最近他被诊断为一种叫做 `失智` 的疾病，该疾病导致作者无法再及时更新这些开源库。
微软每年都会发布新的 `.NET` 版本，每个版本都包含新的功能，或者性能的提升，亦或者一些 breaking change。如果这个开源库只依赖 `.NET Standard`, 新发布的 `.NET` 并不会影响他们，以为只依赖 `BCL`。如果依赖特定的 `.NET`，但是使用者想再新的 `.NET` 版本中使用这个库，但是作者并没有及时更新这个库，那该怎么办呢？

- 首先下载这个库的源码到本地，通常 `NuGet` 页面会有指向源码的链接
- 修改项目的 `TargetFramework` 属性到最新的版本
- 更新项目的依赖到最新版本
- 编译代码
- 运行单元测试
- 更新 `NuGet` 文件信息
- 创建本地 `.nupkg` 文件
- 将其加入到 `NuGet` 源

## 开源项目

1、[Aspire GA](https://devblogs.microsoft.com/dotnet/dotnet-aspire-general-availability/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/769b0423-5f9f-40a3-9038-0220cb54fb8d)

最近在 `Build` 大会中，`Aspire` 这个项目已经处于 `GA` 状态，也就是可以可以尝试使用。

2、[ExcelMapper](https://github.com/mganss/ExcelMapper)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/db55f44e-21f0-4f47-a15b-c69774712751)

`ExcelMapper` 库可以帮助我们非常方便的从 Excel 中读取数据，并且转换成相应的 POCO 对象集合。比如 Excel 中的数据如下

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/18ea62cc-2e98-499a-8db3-ff5c78a0370e)

首先定义实体

```csharp
class Product
{
    public string Name { get; set; }
    [Column("Number")]
    public int NumberInStock  { get; set;}
    public decimal Price { get; set; }
}
```

使用 `Fetch` 方法获取对象集合

```csharp
using Ganss.Excel;
var products = new ExcelMapper("Product.xlsx").Fetch<Product>();
System.Console.WriteLine(products.Count());
```

3、[JMESPath.Net](https://github.com/jdevillard/JmesPath.Net)

JMESPath 是一种针对 JSON 的查询语言。您可以从 JSON 文档中提取和转换元素。`JMESPath.Net` 是一个 `C#` 实现的库，可以帮助我们在 `C#` 中使用 `JMESPath` 查询语言。

4、[OFGB](https://github.com/xM4ddy/OFGB)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/05bc8f19-d86d-4cd7-a37c-0acb9dc16955)

Window 11 在最新的一次更新中，包含了大量的广告，比如文件管理系统，开始菜单等等。这个开源工具可以一键关闭这些功能。
