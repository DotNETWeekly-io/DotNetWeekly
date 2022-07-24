# .NET 每周分享第 16 期

## 卷首语

Global Using 怎么使用？

![using](https://dotnetweeklyimages.blob.core.windows.net/016/globalusing.jfif)

`using` 关键字在 `C#` 中有多种用途，最主要的是引入命名空间，以方便该文件中的代码可以使用这些命名空间中的类，委托等等。但是在 `C# 10` 之前，`Using` 的作用域是文件级别的，也就是说不同的文件，都要包含响应的的 `using` 语句，这些难免有写繁琐。在 `C# 10` 引入了 `Global using` 的功能，它主要有两种实现方式，当然也可以混合使用。

1. 声明 `Global` 应用

在任何使用 `using` 的语句前面添加上 `global`, 那么这个命令空间就变成了全局引用。

```Csharp
global using System;
global using MyNamespace;
```

2. 隐式引用

在 `csproj` 文件中，包含了 `ImplictUsings` 的属性，通过该开关可以引入全局引用。那么它是怎么做到的呢？答案是在编译的时候生成了 `xxx.GlobalUsings.g.cs` 文件，该文件中就是各种 `global using` 语句。生成的文件地址是 `obj\Debug\net6.0\xxx.GlobaUsing.g.cs` 。对于常用的 `Console` 和 `ASP.NET Core` 应用程序，已经自动添加了全局引用空间。r 如果想要手动添加或者移除命令空间，可以在 `csproj` 文件中显示标出

```xml
<ItemGroup>
  <Using Remove="System.Collections.Generic"></Using>
  <Using Include="MyNamespace"></Using>
</ItemGroup>
```

## 行业资讯

1、[.NET 用户态线程要来了？](https://twitter.com/davidfowl/status/1532880744732758018)

![greenthread](https://dotnetweeklyimages.blob.core.windows.net/016/greenthread.png)

总所周知，线程上下文切换是非常耗时的操作，在 `.NET` 的 `async` 和 `await` 都是交给线程池来处理。 `.NET` 团队打算实现一种用户态的线程，也叫做 `Green Thread`，它是由 `runtime` 来控制，并且非常轻量，就跟 `Go` 语言中的 `goroutine` 一样。

2、[.NET VSCode 插件不再开源？](https://github.com/OmniSharp/omnisharp-vscode/issues/5276)

![image](https://dotnetweeklyimages.blob.core.windows.net/016/omnisharp.png)

最近 `.NET` 社区又一个充满争议的话题出现了，我们都知道在使用 `VS Code` 开发 `.NET` 项目的时候，`OmniSharp` 是一个不可缺少的插件，但是由于历史的原因改插件没有采用 `LSP` 协议。现在微软打算不使用开源协议方式，开发新的 `VS Code` 插件，这个在社区引发了剧烈的讨论。大多人认为这个违反了 `Microsoft Love Open Source` 的宣传。

## 文章推荐

1、[Exchange Online 迁移 .NET Core](https://devblogs.microsoft.com/dotnet/exchange-online-journey-to-net-core/)

![exchagneonline](https://dotnetweeklyimages.blob.core.windows.net/016/exchange.jpg)

`CosmosDB` 和 `GraphAPI` 之后，微软的另一个重要服务 `Exchange Online` 也尝试将之前的 `.NET Framework` 迁移到 `.NET Core`。由于 `Exchange Online` 的庞大的仓库，首先从使用量不大的 `Pop3` 和 `IMap4` 开始，逐步从 `.NET Framework` 到 `.NET Core 3.1` ，再到 `.NET 5` `.NET 6` 等等，答案是显著的，不管是内存使用，CPU 的使用量，以及 GC 的时间和次数，都得到了显著的提升。

2、[AWS Lambda 推广者关于 .NET 的故事](https://fbouteruche.medium.com/why-i-have-been-in-love-with-c-and-net-for-more-than-15-years-34af4ddce0d8)

![image](https://dotnetweeklyimages.blob.core.windows.net/016/lambdadotnet.png)

这个一篇来自 `.NET` 开发者的文章，他回顾了自己 `.NET` 经历的开端，然后分析了 `.NET` 社区的缺陷和进步，并且分析了 `AWS` 中 的 `Lambda` 中使用 `.NET` 的故事。

3、[MAUI 教程](https://jesseliberty.com/2022/06/05/learning-net-maui-posting-0/)

![image](https://dotnetweeklyimages.blob.core.windows.net/016/maui.png)

`MAUI` 已经发布了一段时间了，这一系列文章列出了 `MAUI` 的学习资源。

4、[.NET 内存测验](https://tooslowexception.com/net-quiz-check-your-level-of-knowledge-about-net-memory-management/)

![image](https://dotnetweeklyimages.blob.core.windows.net/016/memory.png)

这是这是一些列测试题目问卷，考察一下你对 `.NET` 内存的了解程度。

5、[string.Empty 和 "" 比较](https://www.youtube.com/watch?v=qWBi32-Njm8&ab_channel=NickChapsas)

![image](https://dotnetweeklyimages.blob.core.windows.net/016/stringempty.png)

在 `C#` 中， `string.Empty` 和 `""` 是一样的吗？首先 `string.Empty` 不是一个字面常量，也就是说它不能作为一个函数的默认参数。那么他们之间有什么性能上的差别吗？

```Csharp
string emptyString1 = string.Empty;
string emptyString2 = "";
```

首先看一下 `IL` 代码，

```IL
IL_0000: ldarg.0
IL_0001: ldsfld string [System.Runtime]System.String::Empty
IL_0006: stfld string C::emptyString1
IL_000b: ldarg.0
IL_000c: ldstr ""
IL_0011: stfld string C::emptyString2
IL_0016: ldarg.0
IL_0017: call instance void [System.Runtime]System.Object::.ctor()
```

`ldsfld` 操作是将一个静态字段入栈，而 `ldstr`则将一个对象应用压入一个字符串对象的引用。好像看去两个是不一样。那么再看一下汇编机器码：

```asm
L0015: mov ecx, [0x84e2018]
L001b: mov edx, [ebp-4]
L001e: lea edx, [edx+4]
L0021: call 0x719cc280
L0026: mov ecx, [0x84e2018]
L002c: mov edx, [ebp-4]
L002f: lea edx, [edx+8]
L0032: call 0x719cc280
```

好像两者都是一样，都是从同样地址 `[ebp-4]` 的位置，放入 `edx` 这个寄存器栈中，然后放入 `[edx+4]` 和 `[edx+8]` 两个不同的位置，因此两者的方式本质上是一样的。

6、[为什么你需要关心 .NET GC 的问题](https://tooslowexception.com/why-should-you-care-about-net-gc/)

![gc](https://dotnetweeklyimages.blob.core.windows.net/016/gc.png)

很多时候，我们并不关心 `.NET` GC 的问题，因为它看上去好像跟我们没有什么关系。`.NET` 的抽象已经将这些细节全部隐藏起来了， 只需要关心使用即可。但是仅仅是抽象是不够的，因为

> All non-trivial abstractions, to some degree, are leaky.

当这些抽象变得不在可靠的时候，你就需要面对这些细节了。在日常工作中，**80%** 规则同样起作用，因为大部分是时间你并不需要关心细节，但是如果你想要成为那 `20%` 的专家，就需要了解 `.NET GC` 的内容。可以主要分为两种角色来定位

- 应用程序的开发者：你需要知道测量你的应用程序，哪里是瓶颈，知道什么是最佳实践等等。
- 库开发者：你需要尽可能将你的库的性能变得很高。

所以可以将 `.NET` 应用程序的开发者划分为下面 4 个层次

1. 不关心任何内存相关的东西，只要能跑起来就够了
2. 心中对抽象有清新的认知，比如使用 `Struct`, 不适用 `Linq`, 配置 List 的容量等等，做到这些，你可以在 `80%` 中作为一个专家了
3. 对其更加感兴趣，知道如何去测量和分析一些异常，对于 `measure` 和 `Troubleshooting 有很好的工具箱，对于抽象中可能出现的泄露非常清楚。
4. 对内存有着狂热的认知，能够熟练使用 `C#` 的高级技巧，比如 `refs` , `Span` 和 `stackalloc` 等等。

## 开源项目

1、[Enum 增强版](https://github.com/andrewlock/NetEscapades.EnumGenerators)

![image](https://dotnetweeklyimages.blob.core.windows.net/016/enum.png)

`Enum` 类型是 `.NET` 中重要的类型，它的很多方法是采用反射的方式实现，比如 `ToString`， `IsDefined` 或者 `TryParse` 方法。我们都知道反射的都是非常慢，因此 `NetEscapades.EnumGenerators` 这个库可以帮你解决这个问题，它主要采用 `.NET 6` 的 `Source Generator` 方法，通过自动生成相关的代码来避免反射带来的开销。

使用方法也是非常简单，通过 `EnumExtensions` 这个 `Attribute`，这样编译器会生成 `xxx.g.cs` 文件，通过扩展方法实现了 `ToStringFast`, `IsDefined` 和 `TryParse` 方法。Benchmark 测试结果如下：

```csharp
[MemoryDiagnoser]
public class EnumRunner
{
    [Benchmark]
    public string ColorToString()
    {
        return Color.Black.ToString();
    }

    [Benchmark]
    public string ColorToStringFast()
    {
        return Color.Black.ToStringFast();
    }

    [Benchmark]
    public bool ColorIsDefined()
    {
        return Enum.IsDefined(typeof(Color), (Color)10);
    }

    [Benchmark]
    public bool ColorIsDefinedFast()
    {
        return ColorExtensions.IsDefined((Color)5);
    }

    [Benchmark]
    public (bool, Color) ColorTryParse()
    {
        if (Enum.TryParse<Color>("Green", false, out var color))
        {
            return (true, color);
        }

        return (false, Color.Blue);
    }

    [Benchmark]
    public (bool, Color) ColorTryParseFase()
    {
        if (ColorExtensions.TryParse("Green", out var color))
        {
            return (true, color);
        }

        return (false, Color.White);
    }
}
```

![benchmark](https://dotnetweeklyimages.blob.core.windows.net/016/benchmark.png)
