# .NET 每周分享第 40 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/5fb14b14-ed89-4445-b317-29e9494e9316)

今年 `.NET Conf` 日期已经确定下来，11 月 14 到 16 号，有下面几个重要的内容：

1. 拥抱 `.NET` 生态
2. 一系列现场环节
3. 内容需求提交
4. 探索其他主题，比如云原生，Blazor, .NET MAUI 和智能化应用
5. 颁奖
6. 全球本地活动

## 行业资讯

1、[Nuget 包中 Microsoft 签名更新](https://devblogs.microsoft.com/nuget/microsoft-author-signing-certificate-update-2023/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/21e56868-ad97-45e0-8e76-06add5498f03)

公开发行的 `Nuget` 包通常需要作者的证书来进行签名，使用者可以通过certififcate 的 thumbprint 来校验包的一致性。最近微软更新了使用的证书，相应的开发者需要在 `nuget.config` 中增加相应的证书 thumbprint. 


2、[Moq 中的恶意代码](https://github.com/moq/moq/issues/1372)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/66f7cfee-d384-417f-9389-c29068f7dde9)

`Moq` 是 .NET 社区中广泛使用的一个单元测试库。最近的版本（4.20）包含了一个 [SponsorLink](https://github.com/devlooped/SponsorLink) 包，它会在编译阶段扫描用户 `git` 账户下的信息，并且上传到服务端用来检查是否为 `Sponsor`。这个违反了很多国家和公司的隐私条款，因此在社区得到了广泛的讨论。幸运的是最新的版本已经移除了该功能。

3、[Visual Studio 支持文件比较](https://devblogs.microsoft.com/visualstudio/new-in-visual-studio-compare-files-with-solution-explorer/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0f1c345a-2789-4bb1-9746-ab405a35e713)

文件比较是一项日常开发过程中常见的工作，之前一般会借助命令行工具或者第三方软件来进行比较。最近 `Visual Studio` 引入文件比较的功能，它有两种方式
1. 选择多个文件，然后在上下文菜单中选择比较
2. 选择一个文件，然后在文件选择对话框中选择文件进行比较

## 文章推荐

1、[C# 中的 Feature Gate](https://github.com/microsoft/FeatureManagement-Dotnet)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/fcf179e3-aec5-44be-b055-00e675871aaf)

`Microsfot.FeatureManagement` 和 `Microsoft.FeatureManagement.AspNetCore` 是微软维护的 **Feature Gate** 功能的包，主要是借助 `IConfiguration` 接口来实现功能的开关。这样应用程序无需重新编译，部署或者重启来完成功能的打开和关闭。


2、[C# 中 Class 和 Struct 比较](https://blog.ndepend.com/class-vs-struct-in-c-making-informed-choices/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/1c00adee-7db2-4ed2-98c8-d735f9712c07)

C# 中关于 `Class` 和 `Struct` 的比较数不胜数，这边文章给了一个详细的比较

1. Performance 考虑
2. 复杂对象使用 class
3. 轻量级数据用 struct
4. 避免可变 struct
5. 注意过量拷贝
6. 涉及接口使用 class
7. 引用乐类型使用 class
8. 避免嵌套 struct

3、[ASP.NET Core 中使用 ProblemDetail 的 Response](https://timdeschryver.dev/blog/translating-exceptions-into-problem-details-responses)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c28b13f5-ee8d-4cc3-a7cf-e4b081764e2a)

在网络应用程序遇到一场的时候，我们通常会返回如下的 response. 

```text
HTTP/1.1 500 Internal Server Error
Content-Length: 0
Connection: close
Date: Mon, 24 Jul 2023 13:52:12 GMT
Server: Kestrel
Alt-Svc: h3=":5099"; ma=86400
```

这个做法的问题是我们无法知道具体的错误是什么。在 [RFC3986](https://datatracker.ietf.org/doc/html/rfc7807) 中确定使用 `Problem Details` 方式来组织异常。虽然这仍然处于草稿阶段，但是很多应用程序已经接受这个标准。 
在 `ASP.NET Core 8.0` 预览版中，也支持这样的处理操作

1. 首先添加 `IApplicationBuilder.UseExceptionHandler()` 拓展方法

```csharp
builder.Services.AddProblemDetails();
var app = builder.Build();
app.UseStatusCodePages();
app.UseExceptionHandler();
```

2. 实现 `IExceptionHandler` 接口

```csharp
public class ExceptionToProblemDetailsHandler : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "An error occurred",
            Detail = exception.Message,
            Type = exception.GetType().Name,
            Status = (int)HttpStatusCode.BadRequest
        }, cancellationToken: cancellationToken);
 
        return true;
    }
}
```

3. 注册实例对象

```csharp
builder.Services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();
```

4、[Powershell 中 Foreach-Object 中 Parallel 参数](https://www.youtube.com/watch?v=w_4Slu19DcY&list=WL&index=1&t=1940s&ab_channel=JackedProgrammer)

在 `Powershell Core` 中的 `ForeEach-Object` 提供了 `-Parallel` 这个参数，该参数用来进行并发处理请求，提高效率。但是有几个注意点:

1. 并不是 `-Parallel` 就能完全提高运行效率
2. 并行使用全局变量

```powershell
$result = [System.Collections.ArrayList]::Synchronized((New-Object System.Collections.ArrayList))

$logNames=@("Application", "System", "Windows PowerShell")
$logNames | ForEach-Object -Parallel {
    $logs = Get-EventLog -LogName $_ -Newest 100
    $result = $using:result 
    $result.AddRange($logs) | Out-Null
}
Write-Output $result.Count
```

- `result` 类型包含 `Synchronized` 关键字
- 在并行方法体中使用 `$using:result` 来获取全局变量

3. 并行调用定义方法

```powershelll
function SayHello {
    Write-Output "hello"
}

$funcDef = ${function:SayHello}.ToString()
$array = 1..10
$array | foreach-object -parallel {
    ${function:SayHello} = $using:funcDef
    SayHello
}
```
这里定义了 `SayHello` 方法，通过 `funcDef` 保存函数定义。然后在并行方法中使用 `$using:FuncDef` 来找到方法。

5、[Moq 迁移到 NSubstitue](https://timdeschryver.dev/blog/a-cheat-sheet-to-migrate-from-moq-to-nsubstitute#automate-migration)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e3dc7f37-739c-4e21-9dfb-3103f484c2b1)

当上次 `Moq` 出现隐私问题之后，社区一直在鼓励大家迁移到 `NSubstitute`，这里有一份迁移清单。

6、[Unit Test 的建议](https://ardalis.com/mastering-unit-tests-dotnet-best-practices-naming-conventions/)

我们都知道单元测试的重要性，那么单元测试有哪些 Best Practice 呢 ？

- 清晰和可读性
- 简单
- 隔离
- 可重复性
- 快速
- 可维护性
- 覆盖率

那么单元测试命名规则
- ClassNameMethodName.DoesXGivenY
- Given_Precondition_When_Action_Then_ExpectedResult

7、[.NET SDK 装箱操作的优化](https://pvs-studio.com/en/blog/posts/csharp/1060/)

这是一篇 `.NET` SDK 在版本迭代中对装箱操作的优化，比如这样一段代码 
```csharp
string Foo(int a)
{
  return "The value is " + a;
}
```

一般而言，`+` 操作符的定义为 `string string.operator + (string leeft, object right)`，按照我们的理解，这里会发生一次装箱操作，因为 `a` 为 `int` 类型，而方法的接受的数据类型为 `object`，所以要进行装箱。而且  `IL` 代码也是同样如此
```IL
.method private hidebysig static void  Foo(string str,
                                           int32 a) cil managed
{
  ....
  IL_0001:  ldarg.0
  IL_0002:  ldarg.1
  IL_0003:  box      [mscorlib]System.Int32
  IL_0008:  call     string [mscorlib]System.String::Concat(object,
                                                            object)
  IL_000d:  stloc.0
  IL_000e:  ret
}
```

但是如果使用 `Visual Studio 2019` 编译这段代码，我们可以发现生成的 IL 代码却变成了这样

```IL
.method private hidebysig static void  Foo(string str,
                                           int32 a) cil managed
{
  ....
  IL_0001:  ldarg.0
  IL_0002:  ldarga.s   a
  IL_0004:  call       instance string [mscorlib]System.Int32::ToString()
  IL_0009:  call       string [mscorlib]System.String::Concat(string,
                                                              string)
  IL_000e:  stloc.0
  IL_000f:  ret
}
```

这样我们可以发现，这里没有装箱操作，只有 `Int32.ToString()` 方法。

## 开源项目

1、[Sisk](https://github.com/sisk-http/core)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/fb68eb93-9e8c-465b-8d44-771d8452575b)

在 `.NET` 生态中，`ASP.NET` 和 `ASP.NET Core` 占据了服务器端框架的主要市场，而且凭借优异的性能和官方支持背书，得到了广大用户的使用。但是有时候我们并不希望我们的服务端需要这么重量级的框架，那么可以选择 `Sisk` 这个开源包，它的目标就是一个轻量级，无依赖，简单易用的网络开发框架。

```csharp
using Sisk.Core.Http;

using static Sisk.Core.Routing.RouteMethod;

var http = HttpServer.Emit(
    insecureHttpPort: 5000,
    host: out _,
    configuration: out _,
    router: out var router);

router.SetRoute(Get, "/", _ => new HttpResponse(200)
{
    Content = new StringContent("Hello World")
});

router.SetRoute(Get, "/hi/<name>", req =>
{
    var name = req.Query["name"];
    return new(200)
    {
        Content = new HtmlContent($"<h1>Hello, {name}</h1>")
    };
});

http.Start();
Console.WriteLine($"Http server is listening on {http.ListeningPrefixes[0]}");
Console.ReadKey();
```

2、[NBomber](https://github.com/PragmaticFlow/NBomber)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/669ecec6-490f-4b10-bb40-02ed269f324e)

NBomber 是一个开源你的负载测试框架，比如 HTTP，WebSocket 等等。它的客户端逻辑全部由 `C#` 代码实现

```csharp
var scenario = Scenario.Create("hello_world_scenario", async context =>
{
    // you can define and execute any logic here,
    // for example: send http request, SQL query etc
    // NBomber will measure how much time it takes to execute your logic
    await Task.Delay(1_000);

    return Response.Ok();
})
.WithLoadSimulations(
    Simulation.Inject(rate: 10,
                      interval: TimeSpan.FromSeconds(1),
                      during: TimeSpan.FromSeconds(30))
);

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();
```

它有以下这点特点
- 无任何协议的依赖
- 无语义模型的依赖
- 灵活的配置和简单的 API
- 分布式集群支持
- 实时报告
- CI/CD 集成
- 插件和拓展支持
- 数据源支持

