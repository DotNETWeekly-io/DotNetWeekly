# .NET 每周分享第 57 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/599ea12b-ea74-4f8e-9427-dc323a725955)

这是 Reddit 上的一个的[讨论](https://www.reddit.com/r/dotnet/comments/1da6brw/how_to_break_out_of_legacy_net_jobs/)，提问者是一位在 `.NET Framework` 上工作超过 9 年的开发者。当他尝试投递其他更加现代的 `.NET` 或者 Azure 相关的开发工作的时候，在面试结束后，并没有得到后续的回复。他的问题是该如何从传统的 `.NET` 职位跳槽到新的 `.NET` 职位。
社区给了很多建议

- 假装拥有现代 `.NET` 的技能
- 切换语言并不是一件很罕见的事情，我们应该适应它
- 可以在工作中，将小的组件逐步迁移到现代的 `.NET`
- ...

## 行业资讯

1、[关于Blazor流行度的讨论](https://www.reddit.com/r/dotnet/comments/18q5sbr/blazor_popularity/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/87f158b5-3a2f-4756-a042-9231fb659b9d)

`Reddit` 上有一个 `Blazor` 流行度的讨论，总体而言，有不少人在生产环境中使用 `Blazor`，也有一些人在尝试之后就放弃了使用。

## 文章推荐

1、[Refit.NET个人实践](https://medium.com/medialesson/refit-net-my-personal-caller-best-practise-5e0b24ed6486)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e992dcb8-916b-4e1e-8ff5-5d9543540295)

`Refit` 是 `.NET` 中比较著名的开源库。文章作者分享了自己在使用 `Refit` 的一些最佳实践。主要是不通过直接定义的接口来使用它，而是将它封装成另外一个服务，这样可以做额外的工作，比如认证，授权和异常处理。

```csharp
public interface IMyHttpApiClient 
{
    [Get("/users/{username}/repos")]
    Task<List<Repository>> GetUserRepositoriesAsync(string username);
}

// Creating a Refit client
IMyHttpApiClient  gitHubApi = RestService.For<IMyHttpApiClient >("https://api.github.com");

public class MyApiProvider(IMyHttpApiClient Client)
{
    public Task<UserResponseItem> GetUser(string user)
    {
        return Execute(() => Client.GetUser(user));
    }
}
```

2、[掌握 Fluent Builder Design](https://www.youtube.com/watch?v=qCIr30WxJQw&ab_channel=MilanJovanovi%C4%87)

`Builder Pattern` 是一种设计模式，通常有下面的特点

- 创造设计模式
- 简化构建复杂对象
- 简洁，更加直观的代码
- 复杂度增加，许多新类

在 `ASP.NET Core` 中依赖注入中包含了很多这样的设计，这里一个简单的 `C#` 例子来展示这个模式实现

```csharp
var order = OrderBuilder.Empty.SetName("Order 1")
                .WithNumber(1)
                .ShipTo(s => s.City("São Paulo").ZipCode("01310-100"))
                .Build();

public class Order {
  public string Name { get; set; }

  public int Number{ get; set; }

  public Street Street { get; set; }
}

public class Street {
  public string City { get; set; }

  public string ZipCode { get; set; }
}

public class OrderBuilder {
  public static OrderBuilder Empty => new OrderBuilder();

  private OrderBuilder() {}

  private string _name;
  private int _number;

  private StreetBuilder _streetBuilder = StreetBuilder.Empty;

  public OrderBuilder SetName(string name) {
    _name = name;
    return this;
  }

  public OrderBuilder WithNumber(int number) {
    _number = number;
    return this;
  }

  public OrderBuilder ShipTo(Action<StreetBuilder> buildStreet) {
    buildStreet(_streetBuilder);
    return this;
  }

  public Order Build() {
    return new Order{Name = _name, Number = _number,
                     Street = _streetBuilder.Build()};
  }
}

public class StreetBuilder {
  public static StreetBuilder Empty => new StreetBuilder();

  private StreetBuilder() {}
  private string _city;
  private string _zipCode;

  public StreetBuilder City(string city) {
    _city = city;
    return this;
  }

  public StreetBuilder ZipCode(string zipCode) {
    _zipCode = zipCode;
    return this;
  }

  public Street Build() { return new Street{City = _city, ZipCode = _zipCode}; }
}
```

这里的 `Order` 是目标的对象，但是我们增加了 `OrderBuilder` 类，它的 `SetName` 和 `WithNumber` 两个方法是保存相关属性，而 `Street` 是另外个一个对象，所以我们有构建了 `StreetBuilder` 类，在 `OrderBuilder` 中使用接受一个 `Action<StreetBuilder>` 委托来创建这个属性，最后的 `Build` 方法把记录的数据和委托构造出一个 `Order` 对象。

3、[不可变字典比较](https://goatreview.com/choosing-best-immutable-dictionary-csharp-projects/)

在 `C#` 中有 `ReadOnlyDictionary`, `ImmutableDictionary` 和 `ForzenDictionary` 三种类型，那么它们在功能和性能上有什么区别呢？

- 构造

```csharp
[Benchmark]
public void Create_ReadOnlyDictionary() {
  var dictionnary = new Dictionary<string, Goat>();
  for (int i = 0; i < 1000000; i++) {
    dictionnary.Add(i.ToString(), new Goat{Name = i.ToString()});
  }
  var result = new ReadOnlyDictionary<string, Goat>(dictionnary);
}

[Benchmark]
public void Create_ImmutableDictionary() {
  var dictionnary = new Dictionary<string, Goat>();
  for (int i = 0; i < 1000000; i++) {
    dictionnary.Add(i.ToString(), new Goat{Name = i.ToString()});
  }
  var result = dictionnary.ToImmutableDictionary();
}

[Benchmark]
public void Create_FrozenDictionary() {
  var dictionnary = new Dictionary<string, Goat>();
  for (int i = 0; i < 1000000; i++) {
    dictionnary.Add(i.ToString(), new Goat{Name = i.ToString()});
  }
  var result = dictionnary.ToFrozenDictionary();
}
```

Benchmark 的结果如下

| Method                     | Mean       | Error       | StdDev      | Gen0     | Gen1     | Gen2     | Allocated |
|--------------------------- |-----------:|------------:|------------:|---------:|---------:|---------:|----------:|
| Create_ReadOnlyDictionary  |   967.4 us |    742.6 us |    40.70 us | 220.7031 | 220.7031 | 220.7031 |   1.72 MB |
| Create_ImmutableDictionary | 6,556.5 us | 27,352.5 us | 1,499.28 us | 218.7500 | 218.7500 | 218.7500 |   2.33 MB |
| Create_FrozenDictionary    | 2,985.9 us |  7,518.3 us |   412.10 us | 359.3750 | 359.3750 | 359.3750 |    2.6 MB |

可以看出 `ReadOnlyDictionary` 创建最快，`FrozenDictionary` 次之，最后是 `ImmutableDictionary` 最慢。 原因是什么呢？ `ReadOnlyDictionary` 是复用了底层 `Dictionary` 结构，只不过丢弃了修改的操作，比如 `Add` 等，所以如果还能访问底层的 `Dictionary`, 也是有机会去修改 `ReadOnlyDictionary`; `ImmutableDictionary` 是 `Immutable` 的实现，当你在修改这个字典的时候，它会创建一个新的字典，而保持原有的不变；

- 读取

```csharp
[Benchmark]
public void TryGetValue_ReadOnlyDictionary() {
  for (int i = 0; i < 1000000; i++) {
    var index = Random.Shared.Next(0, N);
    _readOnlyDictionary.TryGetValue(index.ToString(), out var value);
  }
}

[Benchmark]
public void TryGetValue_ImmutableDictionary() {
  for (int i = 0; i < 1000000; i++) {
    var index = Random.Shared.Next(0, N);
    _immutableDictionary.TryGetValue(index.ToString(), out var value);
  }
}

[Benchmark]
public void TryGetValue_FrozenDictionary() {
  for (int i = 0; i < 1000000; i++) {
    var index = Random.Shared.Next(0, N);
    _frozenDictionary.TryGetValue(index.ToString(), out var value);
  }
}
```

Benchmark 结果如下

| Method                          | Mean       | Error      | StdDev    | Gen0    | Allocated |
|-------------------------------- |-----------:|-----------:|----------:|--------:|----------:|
| TryGetValue_ReadOnlyDictionary  |   676.7 us |   502.6 us |  27.55 us | 31.2500 | 303.14 KB |
| TryGetValue_ImmutableDictionary | 1,514.5 us |   845.4 us |  46.34 us | 31.2500 | 303.12 KB |
| TryGetValue_FrozenDictionary    |   391.3 us | 1,828.4 us | 100.22 us | 32.7148 | 303.13 KB |

可以看出 `ReadOnlyDictionary` 创建最快，`FrozenDictionary` 次之，最后是 `ImmutableDictionary` 最慢。之前我们讨论 `ImmutableDictionary` 是不可变的，可以在多线程中使用。但是 `FrozenDictionary` 也是不可变的字段，但是它在读取的数据的性能上比 `ImmutableDicionary` 好很多。

所以，我们可以得到结论，在多线程环境中， `ReadOnlyDictionary` 并不是好的类型，`ImmutableDictionary` 可以在多线程写操作中发挥作用，`FrozenDictionary` 在多线程读操作中发挥作用。

4、[Type Alias](https://devblogs.microsoft.com/dotnet/refactor-your-code-using-alias-any-type/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0eab4318-7951-4a45-a2ca-32eeaf7978ee)

这是 C# 新功能介绍的一系列文章，主要介绍了 `Alias Any Type` 的功能。不同于之前的功能介绍，这次作者按照一个具体的 demo 展示如何使用这个功能，大部分内容在 `GlobalUsing.cs` 文件中

```csharp
// Ensures that all types within these namespaces are globally available.
global using Alias.AnyType;
global using Alias.AnyType.Extensions;
global using Alias.AnyType.ResponseModels;

// Expose all static members of math.
global using static System.Math;

// Alias a coordinates object.
global using Coordinates = (double Latitude, double Longitude);

// Alias representation of degrees-minutes-second (DMS).
global using DMS = (int Degree, int Minute, double Second);

// Alias representation of various distances in different units of measure.
global using Distance = (double Meters, double Kilometers, double Miles);

// Alias a stream of coordinates represented as an async enumerable.
global using CoordinateStream = System.Collections.Generic.IAsyncEnumerable<
    Alias.AnyType.CoordinateGeoCodePair>;

// Alias the CTS, making it simply "Signal".
global using Signal = System.Threading.CancellationTokenSource;
```

- `global using Alias.AnyType` 可以导入这个 `namespace` 下所有成员
- `global using static System.Match` 可以导出这个命令空间下的静态成员
- `global using DMS = (int Degree, int Minute, double Second)` 将一个 `Tuple` 类型使用 `DMS` 别名
- `global using Signal = System.Threading.CancellationTokenSource` 是将一个系统类型 `CancellationTokenSource` 使用 `Signal` 别名

5、[Primary Constructor 的优和劣](https://andrewlock.net/thoughts-about-primary-constructors-3-pros-and-5-cons/)

`Primary Constructor` 是 `C#` 12 引入的新的语法糖，其中的好和坏作者都做出的比较

**优势**

1. 基础字段的初始化
2. 简化单元测试类的初始化
3. MVC controller 中的依赖注入

**劣势**

1. 字段和参数冲突
2. 没有办法标记 `readonly`
3. `Struct` 类型没法控制内存分布
4. 命名规则冲突

6、[异常中的 HResult](https://blog.elmah.io/understanding-the-exception-hresult-property-in-c/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9bbc04ef-f1a6-454c-9f4a-4a583292eaf9)

在 C# 异常处理中，通常我们只会关心 `Message`, `Stack Trace` 等等内容，那么 `HResult` 这段字段代表什么意思呢？
首先 `HResult` 是 `Handle to result` 的简写，它是标准的在不同组件之间交流的错误信息的方式，它是一个 32 位的整型数据，包含了三个内容

- Severity
- Facility
- Code

```csharp
try
{
    int a = 1;
    int b = 0;
    int c = a / b;
}
catch (System.Exception e)
{
    var isFailure = (e.HResult & 0x80000000) != 0; // isFailure is true
    var facility = (e.HResult & 0x7FFF0000) >> 16; // facility is 2
    var code = (e.HResult & 0xFFFF); // code is 18
    System.Console.WriteLine($"isFailure: {isFailure}, facility: {facility}, code: {code}");
}
```

你也可以去 [www.hresult.info](https://www.hresult.info/) 这个网站查询详细信息

## 开源项目

1、[Rin](https://github.com/mayuki/Rin)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/2a2079b6-e091-4ac0-b92f-4c25a3c487f9)

`Rin` 是一个 `ASP.NET Core` 的开源插件，它可以记录和展示每个 `Web API` 请求和响应的细节，以及时间线和 Trace 日志。

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e00e749e-feff-4521-8a87-e47923c2ef2e)

2、[AWS .NET Developer Guides](https://github.com/aws-samples/aws-net-guides)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0d2cc084-8ec3-44c8-901b-8837c7e23a76)

如果你想在 `AWS` 上开发和部署自己的 `.NET` 应用程序，那么这个开源项目可以参考一下，它包含了几乎大部分 `AWS` 的服务，比如通信，容器化，部署，监控，存储等等。

3、[OpenAI-dotnet](https://github.com/openai/openai-dotnet)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/72547509-9a32-40b2-a986-1955db203e63)

OpenAI 出品的 `.NET` SDK, 支持各种模型。
