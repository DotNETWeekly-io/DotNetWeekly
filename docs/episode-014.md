# .NET 每周分享第 14 期

## 卷首语

Result 和 GetResult 的区别

如果代码中存在异步方法，一般而言建议是尽可能使用异步代码，但是有时候我们还是不得已继续使用同步方法来调用异步方法，那么既有两种方式获取异步结果返回的值

- `Result`
- `GetAwaiter().GetResult()`

那么这两种方法有什么区别呢？
使用 `Result` 方法，如果结果抛出异常，则会包装成一个 `AggregateException` 对象，在下面的例子中， catch `HttpException` 异常会被忽略。

```Csharp
try
{
  _ = GetSomethingAsync().Result;
}
catch(HttpException e)
{
  // ignored
}
catch(AggregateException ae)
{
  // hit code
}
```

但是如果使用 `GetAwaiter().GetResult()` 则将正确的异常抛出

```Csharp
try
{
    _ = GetSomethingAsync().GetAwaiter().GetResult();
}
catch(HttpExcpetion e)
{
   // hit.
}
```

## 行业资讯

1、[.NET 7 中 正则表达式性能提升](https://devblogs.microsoft.com/dotnet/regular-expression-improvements-in-dotnet-7/)

![regex](https://dotnetweeklyimages.blob.core.windows.net/014/regex.jpeg)

正则表达式是 `.NET` 中字符串操作必不可少工具，在 `.NET 7` 中重新设计了正则表达式，在性能上取得了很大的提升。

2、[gRPC 和 Rest JSON 互转换](https://devblogs.microsoft.com/dotnet/announcing-grpc-json-transcoding-for-dotnet/)

![grpc](https://dotnetweeklyimages.blob.core.windows.net/014/gprcjson.png)

`gPRC` 和 `REST JSON` 是两种广泛使用的 Web API 数据通信的格式，两者各有优劣。其中 `gPRC` 在性能方面有显著的优势，但是 `REST JSON` 更加格式更加友好，在 `Debug` 的时候更加方便。那么有没有办法同时支持两种方式呢？

`Microsoft.AspNetCore.Grpc.JsonTranscoding` 包可以帮助我们完成这个目标，我们可以在 `proto` 文件中定义好 `REST` 请求的方式，比如

```proto
syntax = "proto3";

import "google/api/annotations.proto";

package greet;

service Greeter {
  rpc SayHello (HelloRequest) returns (HelloReply) {
    option (google.api.http) = {
      get: "/v1/greeter/{name}"
    };
  }
}

message HelloRequest {
  string name = 1;
}

message HelloReply {
  string message = 1;
}
```

通过 `/v1/greeter/world` 请求返回 JSON 格式。

## 文章推荐

1、[M.E.DependencyInjection 添加服务的方法](https://www.youtube.com/watch?v=iQ8cNI7a6mk&ab_channel=NickChapsas)

`Microsoft.Extensions.DependencyInjection` 是微软提供的依赖注入的包，其中可以通过 `ISeriviceCollection` 接口添加相应的服务，那么 `AddSingleton`, `TryAddSingleton` 和 `TryAddEnumerable` 的区别是哪些呢？

- AddSingleton 是将实现接口的服务添加到依赖注入的容器中，不过后来添加的实现优先级大于先添加的
- TryAddSingleton 是尝试将服务添加到实现的接口中，如果接口实现已经存在了，则不会添加进去
- TryAddEnuermable 是尝试将服务添加到容器中，不过判断的标准是接口和实现的类型，如果存在就不再添加

2、[Azure Cosmos DB .NET 6 之旅](https://devblogs.microsoft.com/dotnet/the-azure-cosmos-db-journey-to-net-6/)

![cosmos](https://dotnetweeklyimages.blob.core.windows.net/014/cosmosdb.jpeg)

Azure Cosmos DB 是 Azure 中的 NoSQL 数据库产品，最近他们分享了一篇迁移到 `.NET 6` 之后，在性能上得到的成就。

3、[ASP.NET Core 架构](https://speakerdeck.com/davidfowl/asp-dot-net-core-architecture-overview)

这是来自 David Fowler 分享的 Slides, 从高层角度分享了 ASP.NET Core 的架构。

4、[使用 C# 实现 k-NN 分类](https://visualstudiomagazine.com/articles/2022/05/19/weighted-k-nn-classification.aspx)

K-NN 分类是著名的无监督分类方法，那么如果使用 C# 实现呢？

## 开源项目

1、[CoreWCF 发布](https://github.com/corewcf/corewcf)

![corewcf](https://dotnetweeklyimages.blob.core.windows.net/014/wcf.png)

WCF 是 .NET Framework 3.0 推出的一套服务间通信的标准，随着 `.NET Core` 的推出，微软宣布不再支持 `WCF`, 将全部交给社区维护，近日 `CoreWCF` 1.0 版本正式发布。
