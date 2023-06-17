# .NET 每周分享第 37 期

## 卷首语

VS Code C# 官方插件

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6ede63a7-4765-4b43-8b59-3a2d80ed4984)

之前 VS Code 中的 `C#` 支持都是由 `OmniSharp` 提供的。但是官方并没有明确的 `C#` 支持，现在 `C# Dev Kit` 插件弥补了这个缺失。
安装完成后，你可与在 `VS code` 中体验 `VS` 开发的感觉：

1. 创建项目

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/00a5e3be-a01c-4928-97ac-a019565e96e6)

2. 解决方案视图

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/f13eb789-aeb7-4d0a-9b7a-27f9312cca64)

3. 运行单测

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/03ff46f8-2517-4a24-895e-62e63ae729c6)

4. Debug

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/f98a7250-8af7-4eaf-a5b7-88b79ae71a18)

注意，这个插件不是开源免费的，商业使用需要购买许可证。

## 行业资讯

1、[CLI 版的 Upgrade Assistant](https://devblogs.microsoft.com/dotnet/upgrade-assistant-cli/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/bdb87860-5772-4644-8d37-2f86cdcd54bd)

`.NET Upgrade Assistant` 是`.NET`应用程序的升级工具，比如从`.NET Framework`迁移到`.NET`, 或者不同的 `.NET`版本升级，之前这个是作为`Visual Studio`的一个插件，现在提供了`CLI` 版本。

## 文章推荐

1、[动态构建查询表达式](https://code-maze.com/dynamic-queries-expression-trees-csharp/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/47938402-df3e-488f-bec7-695ffc609878)

在 `C#` LINQ 中，除了我们显式地写查询语句，比如 `persons.Where(p => p.FirstName == "feng")` 之外，我们还可以通过 `Experssion` 的方式动态构建查询语句。这样查询就可以在动态运行时执行，达到数据驱动的的目的，比如

```csharp
static Expression<Func<Person, bool>> CreateEqualExpression(string propertyName, object value)
{
    var param = Expression.Parameter(typeof(Person), "p");

    var member = Expression.Property(param, propertyName);

    var constant = Expression.Constant(value);

    var body = Expression.Equal(member, constant);

    return Expression.Lambda<Func<Person, bool>>(body, param);
}

var expression = CreateEqualExpression("FirstName", "feng");
persion.Where(expression);
```

2、[ASP.NET Core 应用程序部署到 Linux Nginx 中](https://code-maze.com/deploy-aspnetcore-linux-nginx/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/fbd68ddf-7b4a-4461-9bc7-bf49ac04577a)

这篇文章介绍了如何将一个 ASP.NET Core 的应用程序部署到 `Linux` 中，并且使用 `Nginx` 作为反向代理

3、[ASP.NET Core 健康检查](https://www.youtube.com/watch?v=p2faw9DCSsY&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6445589c-0de5-4514-b7a8-a098e0156e0b)

在 `ASP.NET Core` 应用程序中，我们需要检查服务的健康状态，比如 `database`, 外部依赖服务等等。`ASP.NET Core` 提供了 `HealthCheck` 的组件，可以帮助我们完成这样工作。

```csharp
// 实现
public class DatabaseHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            string connectionString = "Server=localhost,1433;database=TestDb;User Id=SA;Password=xxxxxx";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "Select 1";
            command.ExecuteScalar();
            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception e)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
// 注册服务
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database");

// 定义路由
app.MapHealthChecks("/_health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```

4、[.NET StringComparer 优化](https://blog.ndepend.com/net-micro-optimization-and-refactoring-trick/)

下面两个 `Dictionary<string, string>` 对象

```csharp
var dict1 = new Dictionary<string, string>();
var dict2 = new Dictionary<string, string>(StringComparer.Ordinal);
```

它们在 `.NET framework` 和 `.NET Core` 中性能表现是不一样的，也就是说，在 `.NET Core` 中 `dict1` 的性能比 `dict2` 好。这是为什么呢？在这个[ Github issue](https://github.com/dotnet/runtime/issues/29714#issuecomment-497537103) 中解释说，在 `dict2` 中使用的是 `EqualityComparer<string>.Default`, 它其实并不是 `StringComparer.Ordinal` 类型，它避免了随机哈希操作直到必要的时候。

5、[使用连接池优化性能](https://devblogs.microsoft.com/premier-developer/the-art-of-http-connection-pooling-how-to-optimize-your-connections-for-peak-performance/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a26eb06f-7669-434f-988b-9c6f66012d79)

这是微软工程师帮助科技工程从 `on-prem` 向 `cloud` 转型过程中，遇到了并发性的问题，通过连接池 `connection pool` 技术，解决了客户的问题

```csharp
builder.setKeepAliveStrategy((response, context) -> {
String header = response.getFirstHeader("Keep-Alive").getValue();
if (header == null) {
header = response.getFirstHeader("Connection").getValue();
}
if (header != null && header.equalsIgnoreCase("keep-alive")) {
return 30 * 1000; // keep the connection alive for 30 seconds
}
return -1; // close the connection otherwise
});
```

6、[.NET Native AOT](https://ericsink.com/native_aot/index.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/63c008a0-9e77-43e7-ac21-27bd97f22e07)

这是 `.NET Native AOT` 介绍的网站，作者在这里会分享 `Native AOT` 细节。

## 开源项目

1、[.NET roadmap 查询](https://themesof.net/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0ec56d3f-2daf-470c-a19e-1f83f979a118)

这是一个非常有意思的项目，它收集了 .NET 的运行时库，工具以及其他的基础设施中的 issue，Task 和 Milestone 以方便查询。

2、[serilog](https://github.com/serilog/serilog)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/518f5bdb-bb3d-45e0-9c40-f0689792417d)

Serilog 是 `.NET` 社区著名地日志库，相对于 `Microsoft.Extensions.Logging` , 它是一个结构化日志输出库。
