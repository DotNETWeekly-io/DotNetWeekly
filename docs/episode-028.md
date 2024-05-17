# .NET 每周分享第 28 期

## 卷首语

[ChatGPT 在 .NET 上的表现](https://www.youtube.com/watch?v=z2CKQFi746Q&ab_channel=NickChapsas)

![image](https://user-images.githubusercontent.com/11272110/206719245-40d422ed-ba74-48ec-a7d9-9dfc6c4f1e26.png)

`ChatGpt` 是当前 IT 界的当红炸子鸡，几乎所有人都在讨论它。这是 OpenAI 公司基于 GPT 神经网络模型推出的聊天机器人，它不仅仅可以回答问题，还可以帮助我们编写代码。在这个视频中，展示了如果让 `ChatGPT` 帮助我们编写按照要求的代码。

- 首先要求编写一个方法，接受一个 `guid` 字符串，然后将它转换成 `Base64` 的形式并且返回。

```csharp
public string GetBase64FromGuid(string guidString)
{
    Guid guid = Guid.Parse(guidString);
    byte[] bytes = guid.ToByteArray();
    string base64 = Convert.ToBase64String(bytes);
    return base64;
}
```

- 需要处理不合法的输入

```Csharp
public string GetBase64FromGuid(string guidString)
{
    if (!Guid.TryParse(guidString, out guid))
    {
        // Return an empty string if the input is not a valid GUID
        return "";
    }
    byte[] bytes = guid.ToByteArray();
    string base64 = Convert.ToBase64String(bytes);
    return base64;
}
```

- 优化代码，避免推内存分配

```csharp
public string GetBase64FromGuid(string guidString)
{
    if (!Guid.TryParse(guidString, out guid))
    {
        // Return an empty string if the input is not a valid GUID
        return "";
    }
    Span<byte> bytes = stackalloc byte[16];
    guid.TryWriteBytes(bytes);
    string base64 = Convert.ToBase64String(bytes);
    return base64;
}
```

`ChatGPT` 完全超出了人们的预期，尤其是在编程这一领域。但是：

> 人们总是高估技术的短期效益，而忽视的技术的长期效益

## 行业资讯

1、[.NET Interactive Notebook 改名](https://devblogs.microsoft.com/dotnet/dotnet-interactive-notebooks-is-now-polyglot-notebooks/)

![image](https://user-images.githubusercontent.com/11272110/205306787-6459a1f1-2fad-4b0c-bf1e-6b338a9b10e2.png)

微软*改名部*再次发力，将直接的 `.NET Interactive Notebook` 更名为 `Polyglot Notebook`。不过这样做也是合理的，因为现在这个 `Kernal` 不单单只支持 `.NET` 语言，还支持很多其他语言:

- C#
- F#
- PowerShell
- JavaScript
- SQL
- KQL (Kusto Query Language)
- HTML\*
- Mermaid\*

2、[官方文档中事否推荐收费的开源库？](https://twitter.com/runfaster2000/status/1599196714539560960)

![image](https://user-images.githubusercontent.com/11272110/206716814-a83d3ff3-f441-4ac5-950d-db6b19579412.png)

ImageSharp 是 `.NET` 社区中广泛使用的 2D 图像处理库，但是最近该库的作者将改为商业库。所以在 `.NET` 官方文档中，将该库被移除，该库的作者最近在 `Twitter` 上抱怨了这件事。你怎么看这件事呢？

3、[Fluent UI 支持 Blazor 2.0](https://medium.com/fast-design/whats-new-in-the-microsoft-fluent-ui-library-for-blazor-version-2-0-b3ac0eb5d02c)

![image](https://user-images.githubusercontent.com/11272110/206718593-b1e446c4-b927-4378-997c-e26246267122.png)

Fluent UI 和 Blazor 都是微软推出的前端 UI 框架，最近 2.0 版本已经发布。

## 文章推荐

1、[.NET 7 在网络上的提升](https://devblogs.microsoft.com/dotnet/dotnet-7-networking-improvements/)

.NET 7 在网络处理上有很大的提升，主要有

- HTTP 协议
  - 提高链接错误处理能力
  - HttpHeader 线程安全
  - 删除 HTTP/2 和 HTTP/3 协议错误
  - HTTP/3 支持
- QUIC 协议
  - 开放更多的 API 接口
  - 提供 QuicStream 类型
- 安全
  - Negotiate API
  - Certificates 校验选项
  - TLS 协议

2、[Raw string Literals 最大支持引号数量](https://www.tabsoverspaces.com/233911-having-fun-with-csharp-11-raw-string-literals)

![image](https://user-images.githubusercontent.com/11272110/205310996-22730768-57ff-4f05-b12b-42bbdd4e6a8e.png)

C# 11 推出了很强大的 `Raw String Literals`, 通过至少三个引号来表示一个字面字符串，那么最大可以有多少个引号呢？这篇文章作者尝试编写出一个很大的代码文件，其中包含了很多的引号的字符串。

3、[NET AOT 对于 Serverless 服务痛点的解决](https://www.youtube.com/watch?v=3QJDJl-zDFM&ab_channel=NickChapsas)

![image](https://user-images.githubusercontent.com/11272110/206711626-d204af36-a987-4cb8-8e45-078cbc385714.png)

.NET 7 正式推出了 `AOT`, 它可以将 C# 代码编译成目标机器的机器码，这样在运行的过程中就不再需要转换，大大提高了的运行效率。对于 `Serverless` 服务，通常是事件驱动，那么每次在加载的时候，都需要进行机器码转换，而 `.NET AOT` 就解决了这一痛点。

4、[Visual Studio 愿望清单](https://michaelscodingspot.com/extending-visual-studio-wish-list/)

![image](https://user-images.githubusercontent.com/11272110/206852966-f6f83786-a387-46fb-96bc-703b9ed36557.png)

圣诞节要到了，这篇文章的作者列出了一些列对于 `Visual Studio` 和 `Nuget` 的愿望清单，希望 `.NET` 生态能够像 `JavaScript` 生态一样开放。

## 开源项目

1、[TodoApi](https://github.com/davidfowl/TodoApi)

![image](https://user-images.githubusercontent.com/11272110/205304657-ca5f122f-45cd-4451-ab68-3be54dbd9fba.png)

这是个 `.NET` 示例的仓库，通过 `ToDo` 这样简单的应用，展示很多 `.NET` 最新的技术，主要有

- Blazor WebAssembly
- Minimal APIs
- EF + SQLite
- OpenAPI
- ASP.NET Core Identity
- Cookie Authentication
- JWT Authentction
- YARP's Proxy
- Rate Limiting
- Integration Tests

2、[SmartEnum](https://github.com/ardalis/SmartEnum)

SmartEnum 库是 John Skeet 关于 C# [Enum 类型的增强提议](https://codeblog.jonskeet.uk/2006/01/05/classenum/)。 它主要解决 `C#` 中枚举类型只是简单的继承 `Int` 类型，因为很多时候，我们需要将枚举类型同其他数据放在一起。在 `C#` 中只能通过 if-else 或者 switch 语句完成。`SmartEnum` 可以实现类似 `Java` 这样的语法。

```csharp
public sealed class Subscription : SmartEnum<Subscription, double>
{
    public static readonly Subscription Standard = new Subscription("Standard", 1.0);

    public static readonly Subscription Prime = new Subscription("Prime", 0.75);

    public static readonly Subscription VIP = new Subscription("VIP", 0.5);

    private Subscription(string name, double discount) : base(name, discount) { }


    public double GetPrice(double price)
    {
        return price * this.Value;
    }
}

// program.cs
Console.WriteLine($"{Subscription.VIP}'s price is {Subscription.VIP.GetPrice(1000)}");
```
