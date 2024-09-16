# .NET 每周分享第 61 期

## 卷首语

![image](https://github.com/user-attachments/assets/981b936f-4fac-4178-a4d7-a90ad5fa8003)

11 月 12 到 14 号是一年一度的 `.NET` 开发者大会，届时 `.NET 9` 将会正式发布。到时候会有一系列的现场直播的环节，而且现在向所有开发者征求演讲。

## 行业资讯

1、[Mono 装交给 Wine 项目](https://www.omgubuntu.co.uk/2024/08/microsoft-mono-project-to-wine)

![image](https://github.com/user-attachments/assets/5fb32b65-c293-42a9-8b2e-a5c44d321f1b)

最近微软将 [mono](https://github.com/mono/mono) 项目交给了 `WineHQ` 组织，也就是在 `Linux` 中著名的 `wine` 项目的组织。注意这个是支持 `.NET Framework` 中的 mono 项目，而不是 [dotnet/runtime](https://github.com/dotnet/runtime/tree/main/src/mono) 中的 mono。

2、[JetBrain .NET Day](https://blog.jetbrains.com/dotnet/2024/09/02/dotnet-days-online-2024/)

![image](https://github.com/user-attachments/assets/60ee15b5-3c02-4090-a650-e9abb1551a26)

2024 年 Jetbrains  `.NET Day` 会在 9 月 25， 26 号举办，目前的演讲主题已经确定：

25 号

– Not Your Father’s ReSharper by Andrew Karpov

– Crafting Blazor Components With Precision and Assurance by Mariekie Coetzee

– A Homage to the Good Old MVC by Alexander Zeitler

– Overcoming Broken Window Syndrome: Code Verification Techniques for .NET Developers by Gael Fraiteur

– Enhancing ASP.NET Core Razor Pages With HTMX: A Simplicity-First Approach by Chris Woodruff

– No More SQLite – How to Write Tests With EF Core Using Testcontainers by Daniel Ward

26 号

– Building Functional DSLs for Life-Saving Applications by Roman Provazník

– Pushing ‘await’ to the Limits by Konstantin Saltuk

– Functional Programming Made Easy in C# With Language Extensions by Stefano Tempesta

– Into the Rabbit Hole of Blazor Wasm Hot Reload by Andrii Rublov

– Orchestration vs. Choreography: The Good, the Bad, and the Trade-Offs by Laila Bougria

– Contract Testing Made Easy: Mastering Pact for Microservices in C# by Irina Scurtu

– Composing Distributed Applications With .NET Aspire by Cecil Phillip

## 文章推荐

1、[.NET 9 中移除 BinaryFormatter](https://devblogs.microsoft.com/dotnet/binaryformatter-removed-from-dotnet-9/)

![image](https://github.com/user-attachments/assets/65a84ef5-b6e1-4c30-9e8c-ac542caeb547)

由于安全方面的要求，从一个二进制文件反序列化成个对象是危险的，因为攻击者可以替换文件的内容，从而在反序列化的时候，注入攻击代码。所以在 `.NET 9` 中移除了 `BinaryFormatter` 的实现，而 `.NET Framework` 不受影响。
解决办法有两个

1. 使用 [System.Runtime.Serialization.Formatters](https://learn.microsoft.com/dotnet/standard/serialization/binaryformatter-migration-guide/compatibility-package) 包，其包含了原本的实现
2. 使用其他反序列化工具

2、[Visual Studio 中的新特性](https://devblogs.microsoft.com/visualstudio/new-ide-features-in-visual-studio-v17-11/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/73542067-e404-4a13-aeda-4d719204b3aa)

在 `Visual Studio 2022 v17.11`  增加了如下的特性

1. 在 `Search` 功能可以限制查询的范围
2. 和 `VS Code` 同意快捷键： `Ctrl+/` 注释，`Ctrl+Shift+P` 打开控制面板
3. `*.vsconfig` 文件支持
4. Authentication 功能加强
5. Teams 开发套件模板

3、[Aspire 介绍](https://www.youtube.com/watch?v=-jrUiq21gS4)

`Scott Hunter` 在 NDC 会议长介绍了 `Aspire` 。主要包含以下几点

- 弹性: 最早在 ASP.NET Core 3.0 中引入，Aspire 增强了应用程序的弹性，使其能够优雅地处理云相关的故障（如间歇性失败）。
- 可观测性: Aspire 内置对 OpenTelemetry 的支持，提供了应用性能和错误的全面洞察。
- 可扩展性: Aspire 轻松管理本地和云端的资源（如容器），简化了部署过程。

在演示与实际应用,  Scott 展示了一个中间件，它随机导致应用程序失败，以展示 Aspire 的弹性功能如何确保应用程序继续运行。
Aspire 引入了服务发现，自动管理云部署中的 IP 地址和端口。Aspire 提供了一个仪表盘，用于本地和云端监控应用健康状况，并跟踪错误和性能指标。Aspire 支持部署到多个云平台，包括 Azure 和 AWS。它抽象了跨云平台管理基础设施的复杂性。

4、[使用 CollectionsMarshal 访问 Dictionary](https://blog.ndepend.com/faster-dictionary-in-c/)

使用 `CollectionMarshal`  可以提高访问集合类型的性能，比如以 `Dictionary` 为例

- GetValueRefOrAddDefault

如果字典至类型为 `Struct` 类型， 那么可以通过这个方法判断是否存在，如果不存在，可以创建一个这个 key 指向的值的引用

```csharp
static readonly Dictionary<Guid, string> s_Dico1 = new();
[Benchmark]
public void CreateValIfKeyNotInDico() {
  var key = Guid.NewGuid();
  if (!s_Dico1.ContainsKey(key)) {
    // Create the object to be used as key's value
    // because the key is not present in the dictionary
    string val = key.ToString();
    s_Dico1.Add(key, val);
  }
}

static readonly Dictionary<Guid, string> s_Dico2 = new();
[Benchmark]
public void CreateValIfKeyNotInDico_Faster() {
  var key = Guid.NewGuid();
  ref string pointerToValLocation =
      ref CollectionsMarshal.GetValueRefOrAddDefault(s_Dico2, key,
                                                     out bool exists);
  if (!exists) {
    // Create the object to be used as key's value
    // because the key is not present in the dictionary
    var val = key.ToString();
    pointerToValLocation = val;
  }
}
```

| Method                         | Mean     | Error      | StdDev   | Gen0   | Gen1   | Allocated |
|------------------------------- |---------:|-----------:|---------:|-------:|-------:|----------:|
| CreateValIfKeyNotInDico        | 527.0 ns | 2,118.9 ns | 116.1 ns | 0.0095 | 0.0086 |      96 B |
| CreateValIfKeyNotInDico_Faster | 452.9 ns | 1,859.4 ns | 101.9 ns | 0.0100 | 0.0095 |      96 B |

- GetValueRefOrNullRef

第二种方式是获取值的一个引用，方便修改和读取

```csharp
static readonly Guid s_Guid = Guid.NewGuid();

static readonly Dictionary<Guid, int> s_Dico1 = new() { { s_Guid, 0 } };

[Benchmark]
public void StructValueInDico() {
  for (int i = 0; i < 1000; i++) {
    s_Dico1[s_Guid] += 1;
  }
}

static readonly Dictionary<Guid, int> s_Dico2 = new() { { s_Guid, 0 } };
[Benchmark]
public void StructValueInDico_Faster() {
  for (int i = 0; i < 1000; i++) {
    ref int pointerToValLocation =
        ref CollectionsMarshal.GetValueRefOrNullRef(s_Dico2, s_Guid);
    // if(!Unsafe.IsNullRef(pointerToValLocation))
    pointerToValLocation++;
  }
}
```

| Method                   | Mean     | Error    | StdDev    | Allocated |
|------------------------- |---------:|---------:|----------:|----------:|
| StructValueInDico        | 7.463 us | 2.470 us | 0.1354 us |         - |
| StructValueInDico_Faster | 3.069 us | 1.018 us | 0.0558 us |         - |

5、[.NET 开源贡献者名单](https://dotnet.microsoft.com/en-us/thanks)

![image](https://github.com/user-attachments/assets/15e056fb-2e30-4d2f-ac42-4158e2937582)

随着 `.NET` 开源，越来越多的开发者参与进来，并且做出贡献，该网页可以查看每个 `.NET` 开源依赖所有的开发者。

## 开源项目

1、[Grok.net](https://github.com/Marusyk/grok.net)

![image](https://github.com/user-attachments/assets/1f3ba6e9-cdb9-4540-8bec-8bfc7bb7fe9b)

`Gork.net` 借助正则表达式，可以帮助我们很方便的在一些非结构化数据中找到结构化数据。

```csharp
Grok grok = new Grok("%{MONTHDAY:month}-%{MONTHDAY:day}-%{MONTHDAY:year} %{TIME:timestamp};%{WORD:id};%{LOGLEVEL:loglevel};%{WORD:func};%{GREEDYDATA:msg}");

string logs = @"06-21-19 21:00:13:589241;15;INFO;main;DECODED: 775233900043 DECODED BY: 18500738 DISTANCE: 1.5165
                06-22-19 22:00:13:589265;156;WARN;main;DECODED: 775233900043 EMPTY DISTANCE: --------";

var grokResult = grok.Parse(logs);
foreach (var item in grokResult)
{
    Console.WriteLine($"{item.Key} : {item.Value}");
}
```

![image](https://github.com/user-attachments/assets/d8300ed3-6002-4913-a124-737b48d0db39)

2、[Sisk](https://github.com/sisk-http/core)

![image](https://github.com/user-attachments/assets/60bd250e-f7e0-44d9-a20e-c8778bcbd7ce)

`Sisk` 是除了 `ASP.NET Core` 之外，又一个网络框架。和 `ASP.NET Core` 相比，它更加简单，轻便。不到 10 行代码就可以启动一个 Http 服务

```csharp
using Sisk.Core.Http;

using var app = HttpServer.CreateBuilder(5555).Build();
app.Router.MapGet("/", request => {
    return new HttpResponse("Hello World!");   
});

await app.StartAsync();
```

3、[EntityFramework.Exceptions](https://github.com/jdevillard/JmesPath.Net)

![image](https://github.com/user-attachments/assets/ba76391f-0e15-4d06-a28a-49e43d71e846)

在 `Entity Framework Core` 中，在插入数据的时候如果违反了数据库的条件，就会抛出 `DbUpdateException` 异常，但是这个有一个问题是，需要从详细的异常信息中找到错误的原因。而且如果换了不同的数据库，有需要重新提取错误信息。`EntityFramework.Exceptions` 库可以帮助我们统一数据库异常信息。

```csharp
public class EFCoreContext : DbContext
{
    public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }
}

using var context = new EFCoreContext(options);
context.Products.Add(new Product { Name = "Product 1" , Price = 10});
context.Products.Add(new Product { Name = "Product 1" , Price = 20});

try
{
    context.SaveChanges();
}
catch (UniqueConstraintException  e)
{
    Console.WriteLine($"Unique constraint {e.ConstraintName} violated. Duplicate value for {e.ConstraintProperties[0]}");
}
```

这里的 `UniqueConstraintException` 包含了异常的详细信息。除此之外，还支持如下的异常

- CannotInsertNullException
- MaxLengthExceededException
- NumericOverflowException
- ReferenceConstraintException
