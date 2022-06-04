# .NET 每周分享第 15 期

## 卷首语

接口（interface）是 `C#` 重要的编程概念，在过去，我们对于接口的认知是如下的

1. 只能包含方法，属性，索引和事件类型
2. 成员访问修饰符只能是 `public`
3. 不能包含字段

C# 8.0 对于接口做出了下面的修改，可以增加默认实现方法。它是基于这样的实现要求，假设需要为接口添加一个方法，而这个接口已经被很多类实现了，通常有两种方法：

1. 修改已经实现的类
2. 或者额外创建一个接口并且继承这个接口，然后这个接口包含这个需要添加的方法

这样做虽然没有大的问题，但是通常并不是完美的解决方案。在 C# 8.0 中可以为接口添加一个

```Csharp
interface ICar
{
    void GetSpeed();
    void GetMileage();
    public void SendCommand() {
      //Send the Command.
    }
}
```

在这里 `ICar` 接口中增加一个方法

```Csharp
public void SendCommand() {
  // Send the Command
}
```

那么实现的类

```Csharp
public class MorrisGarage: ICar
{
    //...
}
```

那么这个类只有在表示为 `ICar` 接口的时候，才能使用 `SendCommand` 方法

```Csharp
MorrisGarage ng = new MorrisGarage();
mg.SendCommand(); // Complier error

ICar car = new MorrisGarage();
car.SendCommand();
```

默认实现方法也会被具体的实现类覆盖掉，这样无论用 `ICar` 还是 `MorrisGarage` 对象调用 `SendCommand` 都是调用类的实现方式。

除此之前外接口还可以其他内容

1. 可以使用私有方法

```csharp
public interface ICar
{
    private void Initialize()
    {
        //Initialize the Command Center
    }
    void GetSpeed();
    void GetMileage();
    public void SendCommand()
    {
        Initialize();
        Console.WriteLine("Command Sent via Interface");
    }
}
```

2. 接口可以用静态成员字段

```csharp
public interface ICar
{
    private static string commandName = "LOCK_CAR";
    private void Initialize(string cName)
    {
        commandName = cName;
        //Initialize the Command Center
    }
    void GetSpeed();
    void GetMileage();
    public void SendCommand(string cName)
    {
        Initialize(cName);
        Console.WriteLine("Command Sent via Interface");
    }
}
```

3. 接口可以用保护方法，该方法只能继承的接口访问，而实现的类不能访问

```Csharp
public interface ICar
{
    public void SendCommand()
    {
        Console.WriteLine("Command Sent via Interface");
    }
    protected void SendCriticalCommand()
    {
        Console.WriteLine("Critical Command Sent via Interface");
    }
}

public interface IAnotherCar : ICar
{
    public void Send(bool bCritical)
    {
        if (bCritical)
            this.SendCriticalCommand();
        else
            Console.WriteLine("Command Sent via Morris Garage Class");
    }
}
```

4. 接口也可以包含 `virtual` 字符串，这里只有继承的接口才能 override 掉

```csharp
public interface ICar
{
    public virtual void SendCommand()
    {
        Console.WriteLine("Command Sent via Interface");
    }
}

public interface IAnotherCar :ICar
{
    void ICar.SendCommand()
    {
        Console.WriteLine("Command Sent via another Interface");
    }
}

class MorrisGarage: ICar, IAnotherCar
{

}

class Program
{
   static void Main()
   {
      ICar mg= new MorrisGarage();
      mg.SendCommand(); //Calls the virtual implementation.

      IAnotherCar mgOverridden = new MorrisGarage();
      mgOverridden.SendCommand(); //Calls the overridden implementation.
  }
}
```

## 行业资讯

1、[MAUI 已经 General Available](https://devblogs.microsoft.com/dotnet/introducing-dotnet-maui-one-codebase-many-platforms/)

![](https://dotnetweeklypics.blob.core.windows.net/015/maui.png)

前一阵子 `MAUI` 宣布 GA，这篇文章进一步介绍了 `MUAI` 的内容，主要包含

- 支持 Native UI
- Accessibility 第一位
- 支持访问平台除 UI 部分，比如加速器，文件系统，通知消息等等
- 更加容易定制化
- 现代化软件开发方式，比如 C# 10 的语法
- 支持 `Blazor` 的桌面和移动应用程序
- 优化允许运行速度

2、[MS Build Twitter 文字直播](https://twitter.com/sinclairinat0r/status/1529521744444276736)

![](https://dotnetweeklypics.blob.core.windows.net/015/msbuild.png)
想要了解 `MS build` 大会的实时内容，可以访问这个 Twitter thread。

## 文章推荐

1、[自动格式化文件](https://www.talkingdotnet.com/code-cleanup-on-save-in-visual-studio-2022/)

![](https://dotnetweeklypics.blob.core.windows.net/015/cleanup.png)

我们知道 `dotnet format` 可以格式化我们的代码，使之更加符合标准。那么在 `Visual Studio` 中能否在保存文件的时候就将格式化呢？答案是肯定在 `2022 17.2.2` 版本中可以配置该功能:
Tools -> Options -> Text Editor -> Code Cleanup ->Run Code Cleanup profile on Save.

2、[MAUI 视频教程](https://www.youtube.com/watch?v=KmLQLSKqvvI&list=PLwOF5UVsZWUjNR3roRK79QgBcKLyOX48I&ab_channel=JamesMontemagno)

MAUI 系列视频教程

3、[ASP.NET 应用迁移到 ASP.NET Core 方案](https://devblogs.microsoft.com/dotnet/incremental-asp-net-to-asp-net-core-migration/)

![](https://dotnetweeklypics.blob.core.windows.net/015/aspnetcore.jpg)

`ASP.NET Core` 作为现代化 Web 开发平台，拥有很多开发优势。但是由于很多历史包袱问题，仍然有很多应用程序仍然使用 `ASP.NET` 框架，该文章介绍了如何从 `ASP.NET` 迁移到 `ASP.NET Core` 的方法

主要有两个问题需要解决

1. 对于大型项目，如何增量式的迁移
2. 如何决绝 `System.Web.dll` 带来的问题

4、[如何缓存 ASP.NET Core 应用的响应 ](https://code-maze.com/aspnetcore-response-caching/)

![](https://dotnetweeklypics.blob.core.windows.net/015/cache.png)

缓存 `Cache` 是提高应用程序性能的常规手段，在 `Web` 服务中，通常使用 `cache-control` header 的字段来控制响应的缓存策略。那么在 `Asp.NET Core` 中提供了 `ResponseCache` 属性（attribute）来辅助 `Controller` 来自定义这些内容。

```Csharp
[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
public IActionResult Get()
{
    return Ok($"Response was generated at {DateTime.Now}");
}
```

那么请求访问这个 `Controller` 的时候，返回的 Header 中的内容如下

```
cache-control: public, max-age=120
```

## 开源项目

1、[HttpClient 单元测试](https://code-maze.com/csharp-mock-httpclient-with-unit-tests/)

![](https://dotnetweeklypics.blob.core.windows.net/015/httpclient.png)

在软件开发过程中，经常会通过网络访问外部资源。那么就会使用 `HttpClient` 这个类，那么该如何如何进行单元测试呢？

1. Mock `HttpMessageHandler`

`HttpClient` 是一个封装类，它封装了 `HttpMessageHandler` 这个类，那么其中一个思路就是使用 `Moq` 类提供一个 `HttpMessageHandler` 的实现，在代码中构造 `HttpClient` 的时候，将 `HttpMessageHandler` 传入。

```Csharp
public static HttpMessageHandler? Handler { get; set; }
public static HttpClient Client = new HttpClient();
public static async Task Main(string[] args)
{
    if (Handler is not null)
    {
        Client = new HttpClient(Handler);
    }
    string baseAddress = "https://reqres.in";
    string apiEndpoint = "/api/users/2";

    await var responseMessage = Client.GetAsync(baseAddress + apiEndpoint);
}
```

单元测试

```Csharp
[TestMethod]
public async Task GivenMockedHandler_WhenRunningMain_ThenHandlerResponds()
{
    var mockedProtected = _msgHandler.Protected();
    var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
        );
    var apiMockedResponse =
        setupApiRequest.ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("mocked API response")
        });
}
```

2. 使用 `MockHttp`

`RichardSzaly.MockHttp` 这个库可以直接 mock 掉底层的 `HttpClient` 的请求，它的 `API` 可以可以概括两种情况 `When` 和 `Except`

- `When` 表示后端服务的请求地址
- `Except` 表示后端服务返回的内容

```Csharp
_msgHandler = new MockHttpMessageHandler();
_msgHandler.When("https://reqres.in/api/users/*").Respond("text/plain", "mocked user response");
```

如果有其它更加复杂的请求，可以通过 `WithHeaders` 和 `WithQueryString` 完成。

```Csharp
_msgHandler.Expect("https://reqres.in/api/users/*")
   .WithHeaders(@"Authorization: Basic abcdef")
   .WithQueryString("Id=2&Version=12")
   .Respond("text/plain", "mocked response");
```

2、[命令行 GUI](https://github.com/migueldeicaza/gui.cs)

![](https://dotnetweeklypics.blob.core.windows.net/015/gui.png)

对于 `console` 应用程序，黑乎乎的命令行有时候仍然不满足我们的需求，我们需要更加丰富的 `GUI` 来满足我们的要求。而 `gui.cs` 是基于 `.NET Core` 的库，这也意味着，它具备了跨平台的能力。

1、[ASP.NET Core 必需的库](https://procodeguide.com/asp-net-core/10-essential-nuget-libraries/)

![](https://dotnetweeklypics.blob.core.windows.net/015/nuget.png)

开发 `ASP.NET Core` 应用程序，这些库是非常有必要的

1. Serilog: 用来记录应用程序日志
2. Dapper: 数据库访问
3. xUnit: 单元测试框架
4. Moq: 单元测试接口 mock
5. AutoMapper: 用来映射应用程序和数据库实体
6. Newtonsoft Json.NET： JSON 数据解决方案
7. FluentValidation: 更优雅的输入数据判断
8. Polly：构建健壮性应用程序
9. Swashbuckle： 创建 WebAPI 文档
10. Health Checks： 健康检查
