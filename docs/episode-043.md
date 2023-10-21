# .NET 每周分享第 43 期

## 卷首语

C# 的受欢迎指数超过 Java

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e647d1a3-d4e8-4a7b-bb7b-d4b9d47516b2)

最近的市场调研显示，C# 受欢迎指数正在接近并超越 Java


## 行业资讯

1、[Jetbrains .NET Day 录像](https://blog.jetbrains.com/dotnet/2023/10/02/recordings-jetbrains-dotnet-day-online-23/)

今年 JetBrains 的 .NET Day 活动录像。

2、[VS Code 插件 Dev Kit GA](https://devblogs.microsoft.com/dotnet/csharp-dev-kit-now-generally-available/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/5af224a3-456f-415a-9aff-96f9d28d7922)

在 `VS Code` 中 `C#`  开发插件 `Dev Kit` 已经可以公开使用。 


## 文章推荐

1、[ASP.NET Core 学习路线](https://roadmap.sh/aspnet-core)

![aspnet-core](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/8755c449-d483-4f1e-a9e2-5c065a5f1ebe)

ASP.NET Core 学习路线，包含了四种类型
1. 个人建议
2. 可选
3. 不是严格
4. 不建议

2、[.NET Roll Forward 配置](https://weblog.west-wind.com/posts/2023/Oct/02/Rolling-Forward-to-Major-Versions-in-NET)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6c228451-85c3-4c2b-9502-b48a24406ee9)

得益于 `.NET` 的兼容性，`.NET 6` 编写的应用程序可以运行在 `.NET 7` 的运行时中，那么该如何配置这个呢？

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>    
    <TargetFramework>net7.0-windows</TargetFramework>
    <RollForward>LatestMajor</RollForward>
  <PropertyGroup>    
</Project>
```

除了 `LastMajor` 外，还有

- `Minor`
- `Major`
- `LatestMinor` 

等配置。如果是 `Preview` 版本的 SDK 该怎么办呢？比如 `net8.0.0-rc-123419.4`， 则需要配置 `DOTNET_ROLL_FORWARD_TO_PRERELEASE` 环境变量

```powershell
$env:DOTNET_ROLL_FORWARD_TO_PRERELEASE=1
```


3、[序列化中的未知枚举类型](https://gaevoy.com/2023/09/26/dotnet-serialization-unknown-enums-handling-api.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/07b66238-7568-4c78-906f-f332337503f5)

在序列化成对象的时候，如果为 Enum (枚举) 类型中不存在的类型时候，通常会抛出异常，那么在不同序列化场景中怎么解决好这个问题呢？分别为 `Newtonsoft`, `System.Text.Json` 和 `System.Xml.Serializaton` 等库给出了示例。

4、[如何在 UT 中测试 Logger](https://www.meziantou.net/how-to-test-the-logs-from-ilogger-in-dotnet.htm)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3bf29b30-cace-48d3-8f1d-c810ddcdb551)

在单元测试用该如何测试 `ILogger` 对象呢？有很多方法，比如
- 使用 `Mock` 库来构造 `ILogger` 实现
- 将所有日志保存在内存中，然后比较
- 将所有日志保存在文件中

第一种方式比较直接，也是大众最为熟悉的方式，第二种就比较不常见，但是有现成的库能够帮助我们实现, `Meziantou.Extensions.Logging.InMemory` 

```csharp
// Create the logger
using var loggerProvider = new InMemoryLoggerProvider();
var logger = loggerProvider.CreateLogger("MyLogger");

// Call the method to test
MethodToTest(logger);

// Validate the logs
Assert.Empty(loggerProvider.Logs.Errors);
Assert.Single(loggerProvider.Logs, log => log.Message.Contains("test") && log.EventId.Id == 1);
```

5、[用内存映射文件](https://blog.stephencleary.com/2023/09/memory-mapped-files-overlaid-structs.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/5821abf8-8e7d-4d5e-a664-cf184af709ac)

在 `C#` 这样托管的编程语言中，我们也可以使用访问内存的方式来修改文件内容，这样可以频繁的避免磁盘读写操作，操作系统自会需要的时候才会进行磁盘读写。那么该怎么做呢？

```csharp
using FileStream file = new FileStream(@"tmp.dat", FileMode.Create, FileAccess.ReadWrite,
    FileShare.None, 4096, FileOptions.RandomAccess);
using MemoryMappedFile mapping = MemoryMappedFile.CreateFromFile(file, null, 1000,
    MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, leaveOpen: true);
using MemoryMappedViewAccessor view = mapping.CreateViewAccessor();
```

这里创建一个`view` 对象，它代表了一个内存指针，这个对象包含了很多个操作方法，通常我们不需要关心，可以直接创建一个 `Overlay` 的对象来操作它

```csharp
public sealed unsafe class Overlay : IDisposable
{
  private readonly MemoryMappedViewAccessor _view;
  private readonly byte* _pointer;

  public Overlay(MemoryMappedViewAccessor view)
  {
    _view = view;
    view.SafeMemoryMappedViewHandle.AcquirePointer(ref _pointer);
  }

  public void Dispose() => _view.SafeMemoryMappedViewHandle.ReleasePointer();

  public ref T As<T>() where T : struct => ref Unsafe.AsRef<T>(_pointer);
}
```

通过它可以方位 `T` 类型的结构

```csharp
public struct Data
{
  public int First;
  public int Second;
}

using Overlay overlay = new Overlay(view);
ref Data data = ref overlay.As<Data>();
data.First = 1;
data.Second = 2;
```



6、[本地运行 .NET 的 LLM](https://blog.maartenballiauw.be/post/2023/06/15/running-large-language-models-locally-your-own-chatgpt-like-ai-in-csharp.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e08420f3-3655-415e-8869-c1d1ca656134)

随着 LLM 的流行，我们特别希望能够一款运行在本地的大模型。Meta 公司的 LLaMA 开源模型给了这件事情的可能性，SciSharp 项目就是将机器学习和 AI  框架引入到 .NET 生态中。这里介绍如何使用 `C#` 实现本地大模型


## 开源项目

1、[YamlDotNet](https://github.com/aaubry/YamlDotNet)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6609b43e-cb0a-461a-aad4-a5ebf95e6eae)

很多时候，我们需要进行将 `Yaml` 和 `C#` 中的对象进行转换，`YamlDotNet` 库可以很方便的进行序列化和反序列化。


```csharp
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
...

 var person = new Person
{
    Name = "Abe Lincoln",
    Age = 25,
    HeightInInches = 6f + 4f / 12f,
    Addresses = new Dictionary<string, Address>{
        { "home", new  Address() {
                Street = "2720  Sundown Lane",
                City = "Kentucketsville",
                State = "Calousiyorkida",
                Zip = "99978",
            }},
        { "work", new  Address() {
                Street = "1600 Pennsylvania Avenue NW",
                City = "Washington",
                State = "District of Columbia",
                Zip = "20500",
            }},
    }
};

var serializer = new SerializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .Build();
var yaml = serializer.Serialize(person);
System.Console.WriteLine(yaml);
// Output: 
// name: Abe Lincoln
// age: 25
// heightInInches: 6.3333334922790527
// addresses:
//   home:
//     street: 2720  Sundown Lane
//     city: Kentucketsville
//     state: Calousiyorkida
//     zip: 99978
//   work:
//     street: 1600 Pennsylvania Avenue NW
//     city: Washington
//     state: District of Columbia
//     zip: 20500
```


2、[Source Code Generator Playgroud](https://github.com/davidwengier/SourceGeneratorPlayground)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/b2b40480-d541-459f-a6a3-4520738bf9bd)


Source Generator 是 C# 的引入和的新功能，那么这个在线工具可以测试该功能。