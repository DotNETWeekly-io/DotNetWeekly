# .NET 每周分享第 46 期

## 卷首语

[C# 受欢迎指数超过 Java](https://visualstudiomagazine.com/articles/2023/10/17/tiobe-oct23.aspx)

最新编程语言统计网站 `Tiobe` 显示，`C#` 的受欢迎程度在今年会超过 `JAVA`。

## 行业资讯

1、[.NET 8 新手开发教程](https://devblogs.microsoft.com/dotnet/learn-dotnet8-beginner-videos/)

随着 `.NET 8` 的推出，微软也更新对于新手的教程，包含

1. C# 学习
2. .NET 中的生成式 AI
3. .NET
4. Azure 中的 .NET 开发
5. Visual Studio 中开发 .NET
6. VS Code 开发 .NET
7. NuGet 学习
8. Blazor 混合开发
9. IoT 开发
10. .NET 升级

2、[.NET 生态趋势](https://blog.jetbrains.com/dotnet/2023/11/30/the-developer-ecosystem-in-2023-key-trends-for-csharp/)

JetBrains 发布了最新的 `.NET` 社区调查问卷，可以看出 `.NET` 社区的生态趋势。

## 文章推荐

1、[.NET Native AOT Compatible 的建议](https://devblogs.microsoft.com/dotnet/creating-aot-compatible-libraries/)

.NET Native AOT 是一个非常吸引人的功能，但是有一些限制条件，简单来讲有
1. 代码是可以削减的
    - 不能动态加载运行库
    - 反射可以用，但是浏览执行类型图
2. 不能再运行时产生新的代码

为了让代码和 `Native AOT` 尽可能的兼容，可以通过下面的方式

通过代码静态分析工作，可以找到可能不兼容的代码，第一个可以选择 `Roslyn Analyzer`；另一种是发布测试 AOT 应用程序。

2、.NET 8 新语法

`.NET 8` 提供了一个新的语法，甚至在官方文档都找不到相应的说明。这个新语法非常简单，如果对于空内容的类，结构体或者接口，不需要一组花括号。

```csharp
class Foo;
struct Bar;
interface IFoo;
```

这些都是合法的定义。

3、[C# 中的奇技淫巧](https://www.youtube.com/watch?v=lr1GtiF05EM&ab_channel=NickChapsas)

C# 中有很多奇技淫巧，让一下看上去不合法的代码也能正常运行。

1. 重载操作符

```csharp
public record FilePath(string Path){
    public static FilePath operator / (FilePath left, string right) {
        return new FilePath(System.IO.Path.Combine(left.Path, right));
    }
    public override string ToString() {
        return Path;
    }
}
```

这里重载的 `/` 操作符，这样可以这样使用

```csharp
var basePath = new FilePath("C:/foo");
var filePath = basePath / "bar" / "baz.txt";
```

2. Await

任何一个对象在调用 `GetAwaiter` 方法后能够返回一个 `TaskAwaiter` 的话，就能够使用 `await` 语句
```csharp
public static TaskAwaiter GetAwaiter(this int seconds){
    return Task.Delay(TimeSpan.FromSeconds(seconds)).GetAwaiter();
}
```

这样在调用 `await 2` 方法之后，就能够实现暂停 2 秒钟的效果。

3.  Enumerator 

任何对象实现了 `Current` 和 `MoveNext` 的方法，就可以在 `Foreach` 中使用

```csharp
public static class CutomIntExtensions
{
    public static CustomIntEnumerator GetEnumerator(this Range range
    {
        return new CustomIntEnumerator(range);
    }

    public static CustomIntEnumerator GetEnumerator(this int number)
    {
        return new CustomIntEnumerator(new Range(0, number));
    }
}

public struct CustomIntEnumerator
{
    private int _current;
    private readonly int _end;

    public CustomIntEnumerator(Range range)
    {
        if (range.End.IsFromEnd)
        {
            throw new NotSupportedException();
        }

        _current = range.Start.Value - 1;
        _end = range.End.Value;
    }

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= _end;
    }
}
```

这样可以就能够 `foreach(var i in 10)` 使用。

4、[Keyed Dependency Service](https://blog.elmah.io/dependency-injection-using-keyed-services-is-finally-in-asp-net/)

之前  `M.E.DepdendencyInjection` 包在使用的时候会有一个没法解决的问题，假设一个接口有多种实现，那么在构造其他的服务的时候，该怎么指定所需要的实现呢？
在 `8.0` 版本中推出了 `Keyed service`, 这样可以在构造的时候指定实现的类型。

```csharp
public interface IService;
public class FooService : IService;
public class BarService : IService;
```

`IService` 接口有两个实现累心 `FooService` 和 `BarService`, 将它们注册到服务容器的时候可以指定 `key`。

```csharp
IServiceProvider services = new ServiceCollection()
    .AddKeyedSingleton<IService>("foo", new FooService())
    .AddKeyedSingleton<IService>("bar", new BarService())
```

那么在需要该服务地方的构造函数，指定相应的 `key`.

```csharp
public Application(
    [FromKeyedServices("foo")] IService fooService,
    [FromKeyedServices("bar")] IService barService)
{
    _fooService = fooService;
    _barService = barService;
}
```

5、[关于 Aspire 你需要知道的 5 件事](https://www.growthaccelerationpartners.com/blog/5-things-you-need-to-know-about-aspire-and-net-8)

Aspire 是随着 `.NET 8` 推出的分布式应用程序解决方案，需要知道下面 5 件事情

1. Aspire 包含很多组件，对应了不同的云服务
2. Aspire 能够简化组件编排
3. Aspire 不等同于 `k8s`
4. Aspire 包含不同的项目模板
5. 开发看板

6、[Aspire 实例](https://learn.microsoft.com/en-us/samples/browse/?products=dotnet-aspire)

微软文档提供了很多 `Aspire` 示例。

7、[.NET 在硬件加速方面汇总](https://devblogs.microsoft.com/dotnet/dotnet-8-hardware-intrinsics/)

每次 `.NET` 新版本推出，都利用了硬件加速的功能让 `.NET` 的运行时越来越快。本文回顾了所有 `.NET` 版本中对其的利用，并且介绍了 `.NET 8` 在这方面的工作。

## 开源项目

1、[Debug Output Filter](https://marketplace.visualstudio.com/items?itemName=GrantDavies.NiahTextFilter2022&ssr=false#overview)

Visual Studio output 功能增强拓展，主要包含下面功能
1. 动态过滤
2. 链式过滤
3. 支持文件或者粘贴板
4. 主窗体 debug 
5. 输出项目聚合