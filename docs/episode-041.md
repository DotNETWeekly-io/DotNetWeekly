# .NET 每周分享第 41 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c5b23ca3-e0d8-4552-b4a8-02f10763333e)

最近 `.NET` 社区的大新闻是微软将在明年退役 `Visual Studio for Mac` 这个产品，该消息在社区的引发了巨大的讨论。

## 行业资讯

1、[Rider 65% 折扣](https://blog.jetbrains.com/dotnet/2023/09/01/65-off-rider/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/103705ae-de2e-4378-8dba-41f4beb3422c)

当微软宣布不再支持 `Visual Studio for Mac` 之后，`JetBrains` 就立马宣布旗下 `Rider` IDE 进行 65% 的折扣销售，可谓是**杀人诛心**。


## 文章推荐

1、[Rust 代码运行在 .NET CLR 中](https://fractalfir.github.io/generated_html/rustc_codegen_clr_v0_0_1.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/331352cb-bf8e-4faf-8536-0bbe42a67dcf)

这是一篇非常有意思的博客，作者尝试了将 Rust 代码生成的结果运行在 .NET CLR 上

2、[使用 String.Globalization.StringInfo 计算 Unicode 字符的长度](https://khalidabuhakmeh.com/measuring-unicode-string-lengths-with-csharp)

[image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/49c5fc63-029b-4a18-836e-5a67fddb21b2)

在处理 `Unicode` 字符串的时候，我们需要知道字符串所占用的字节长度。尤其是涉及到 Emoji 时候，它有时候是多个 Unicode 字符串拼接而成，所以在 C#  中我们可以通过 `String.Globalization.StringInfo.GetNextTextElementLength` 方法来获得字符所占用的字节数量。

```csharp
using System.Globalization;

var characters = new[] { "a", "1", "👩‍🚀", "あ", "👨‍👩‍👧‍👦", "✨" };

var lengths = characters.Select(c => (value: c, length: StringInfo.GetNextTextElementLength(c)));

foreach(var (val, length) in lengths)
{
    Console.WriteLine($"{val} (length: {length}");
}
```


3、[Visual Studio 9 个隐藏功能](https://blog.elmah.io/9-hidden-features-in-visual-studio-that-you-may-not-know/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/14e33986-066f-479e-ba70-7766358ebd5a)

`Visual Studio` 是一款广泛使用的 IDE，那么对于 Visual Studio 还有一些隐藏的功能。

1. Drag & Drop 添加工程应用
2. GitHub Action 在 Visual Studio 解决方案视图
3. CPU/Memory Profiler: 通过在不同断点之间开启 CPU profile
4. 拷贝带缩进
5. Visual Studio 通常会保存上次的 profile，但是 Visual Studio 支持在启动的时候配置不同的 profile. 
6. 清理不同的 Azure Functional 工具
7. 内嵌展示诊断信息
8. 智能 JSON 校验
9. 不同事件播放不同声音


4、[HttpClient DelegatingHandler](https://www.youtube.com/watch?v=goxI3rOMnmY&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/52988af4-6a9b-4867-8843-2f05d86cc426)

 我们都知道 `ASP.NET Core` 通过中间件的方式，处理一个个请求，对于定制化的业务逻辑，我们可以编写自己的中间件。同样的在 `HttpClient` 类中，我们 `DelegateHandler` 也可以当作中间件，可以对client 发出的请求进行定制化处理，比如缓存请求。

```csharp
internal class CacheHttpMessageHandler : DelegatingHandler
{
    private readonly IMemoryCache _cache;
    public CacheHttpMessageHandler(IMemoryCache cache)
    {
        _cache = cache;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var queryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
        var key = queryString["city"];
        if (_cache.TryGetValue<string>(key, out var content))
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.NotModified)
            {
                Content = new StringContent(content)
            };
        }

        var response = await base.SendAsync(request, cancellationToken);
        content = await response.Content.ReadAsStringAsync();
        _cache.Set<string>(key, content, TimeSpan.FromSeconds(10));
        return response;
    }
}
```

那么可以将它注册到容器中
```csharp
IServiceCollection service = new ServiceCollection();

service.AddMemoryCache();
service.AddTransient<CacheHttpMessageHandler>();
service.AddHttpClient("httpbin")
    .AddHttpMessageHandler<CacheHttpMessageHandler>();

IServiceProvider provider = service.BuildServiceProvider();

var httpClientFactory = provider.GetService<IHttpClientFactory>();

HttpClient httpClient = httpClientFactory.CreateClient("httpbin");

for (int i = 0; i < 20; i++)
{
    using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://httpbin.org/get?city=beijing");
    using var response = await httpClient.SendAsync(httpRequestMessage);
    Console.WriteLine(response.StatusCode);
    await Task.Delay(TimeSpan.FromSeconds(1));
}
```

5、[.NET 生成时 AI ](https://devblogs.microsoft.com/dotnet/demystifying-retrieval-augmented-generation-with-dotnet/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/90781485-ccbf-4153-8c42-7e25410e7ca2)

这是一份来自 `Stephen Toub` 的文章，介绍了如何使用 `Semantic Kernal` 和 `Azuer OpenAI` 来一步步创建一个 `.NET` 的对话应用程序。

6、[.NET Runtime Bug](https://www.youtube.com/watch?v=1oR7L6kHnCI&ab_channel=NickChapsas)


```csharp
using System.Collections;

IStructuralEquatable one = new ValueTuple<int, int, int, int, int>(1, 2, 3, 4, 5);
IStructuralEquatable two = new ValueTuple<int, int, int, int, int>(1, 2, 3, 5, 5);

Console.WriteLine(one.Equals(two, EqualityComparer<int>.Default));
```

上面这段代码的返回输出是什么？应该是 `false`，因为第四个元素的值不一样，但是在目前的 `.NET` SDK 中，编译出来的结果是 `true`。这是 `.NET` BCL 的 bug，因为 `Tuple` 类型的 `Equal` 方法跳过了第四个元素。

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/ac8ed09f-aea6-4334-b94b-f9d8ad99a389)

目前 `Fix` PR 已经合并。


## 开源项目

1、[ConsoleTables](https://github.com/khalidabuhakmeh/ConsoleTables)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/81759be7-46cc-45a1-8f84-6c2b9eead986)

在 `C#` 的控制台应用程序中，我们有时候需要输出一些表格的内容来更加直观的展示内容。`ConsoleTables` 这个开源库可以帮助我们很方便的输出。使用方法也是非常简单

```csharp
var table = new ConsoleTable("one", "two", "three");
table.AddRow(1, 2, 3)
        .AddRow("this line should be longer", "yes it is", "oh");
table.Write();
```

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6d800acf-ab05-4085-8954-f2aba5becf7b)

淡然可以控制输出表格的样式

```csharp
var rows = Enumerable.Repeat(new Something(), 10);
ConsoleTable
        .From<Something>(rows)
        .Configure(o => o.NumberAlignment = Alignment.Right)
        .Write(Format.Alternative);

```

2、[Visual Studio 中预览图片](https://github.com/MadsKristensen/ImagePreview)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/727cbd7a-5a17-406d-bee5-f876c102bf7a)

这是 Visual Studio 的可以插件，当你把鼠标放在上面的时候，它会根据图片的地址获取，然后以缩略图的方式展示出来。支持下面的图片路径格式

- 绝对 URI 地址
- 相对 URI 地址
- 文件路径
- 数据 URI
- 包 URI

3、[CSharpier](https://github.com/belav/csharpier)


`CSharpier` 是一个 `.NET` 的代码格式化工具。首先安装工具

```
dotnet tool install csharpier -g
```

然后使用 `dotnet charpier .` 命令可以格式化相应的 `.NET` 项目。