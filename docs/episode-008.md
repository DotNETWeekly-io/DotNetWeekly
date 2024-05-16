# .NET 每周分享第 8 期

## 开卷语

`.NET` 已经 20 周年啦。现在[官网](https://dotnet.microsoft.com/en-us/?utm_source=dotnetblog&utm_medium=banner&utm_campaign=.netanniversary)正在举办一系列活动。

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/f98a6f25-df51-4616-acf9-0213bab4c86d)

> Check it out

## 行业资讯

1、[.NET MAUI Preview 12 发布](https://devblogs.microsoft.com/dotnet/announcing-net-maui-preview-12/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3b4bb42b-02cc-4fb0-b8f6-07f5f1727ed0)

`.NET MAUI` 发布新的 Preview 版本，该版本包含了如下

- 文档更新
- Android 版本的 `FlyoutView`
- Z index 属性
- .NET 6 统一化
- Windows 拓展工具栏

2、[Null 检查](https://github.com/dotnet/runtime/pull/64720)

本周 `.NET` 宣布了一个热点更新，主要内容可以用一行代码表示。之前在方法参数 `null` 检查如下

```Csharp
void M(object arg)
{
    if (arg is null)
    {
        throw new ArgumentNullException(nameof(arg));
    }
}
```

转变成

```Csharp
void M(object arg!!)
{
    ...
}
```

本质上，这就是一个语法糖。

## 文章推荐

1、[Web Form 到 .NET 6](https://www.poppastring.com/blog/modernizing-dasblog-from-web-forms-to-net-6)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/b18581aa-3705-4131-b004-eb2bb0938348)

这边播客介绍如何将一个 19 年历史基于 `.NET` Web Forms 的播客转换为一个 `.NET 6` 的跨平台的应用程序。这里讨论了如何移除已有的解决方案，并且选择开源社区的解决的各种决策。

2、[Miguel de Icaza 访谈](https://www.dotnetrocks.com/default.aspx?ShowNum=1779)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/221b688d-d00e-4c10-8f31-e0e2f80c9669)

这篇播客是和 `Miguel de Icaza` 讨论一下 `.NET` 20 周年，他是 `Mono Project` 的发起人，之后他创造了 `Mono Touch`, `Xamarin` 等等。在这里对话中，他讨论了开源的演化，科技公司对开源项目的影响还有开源项目维护者在将来的展望。

3、[C# 返回值拆包](https://twitter.com/buhakmeh/status/1488197682392973314)

一张图了解如何实现返回值拆包。

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c5808caf-b5d9-4799-beb4-abeac4c298a7)

4、[Ｃ# 日志处理新方法](https://www.youtube.com/watch?v=MHIheQ2_Yb4&t=329s&ab_channel=NickChapsas)

在防御性编程中，我们通常需要对接受到的参数进行校验，以便脏数据导致程序运行的非正常行为，比如说

```Csharp
public static void IsNotNull<T>(T value)
{
    if (value is null)
    {
        throw new ArgumentNullException(nameof(value));
   }
}
```

那么这样调用的结果是

```Csharp
List<int> list = null;
IsNotNull(list);
```

> System.ArgumentNullException: Value cannot be null. (Parameter 'value')

但是这样在日志中并不会知道究竟是哪个穿入的参数导致了 `Null`。如果 `IsNotNull` 的方法这样定义

```Csharp
public static void IsNotNull<T>(T value, [CallerArgumentExpression("value")] string message = "")
{
    if (value is null)
    {
        throw new ArgumentNullException(message);
    }
}
```

这时候，得到的异常错误是这样的

> System.ArgumentNullException: Value cannot be null. (Parameter 'list')

可以看到它将穿入的参数也也作为 CallStack 的一部分。

5、[Cast 的性能消耗](https://www.tabsoverspaces.com/233888-what-is-the-cost-of-casting-in-net-csharp)

`C#` 是一个强类型开发语言，类型之前转换需要进行 `Cast`, 那么 `Cast` 的性能消耗怎样的，有没有更好的方法呢？

```Csharp
public ITuple UnsafeCastArgument(object o)
{
    return Unsafe.As<ITuple>(o);
}
public ITuple RegularCastArgument(object o)
{
    return (ITuple)o;
}
```

答案是显而易见的，`Benchmark`

| Method              | o                  | Mean      | Error     | StdDev    | Ratio | Code Size |
| ------------------- | ------------------ | --------- | --------- | --------- | ----- | --------- |
| UnsafeCastArgument  | ?                  | 0.0002 ns | 0.0003 ns | 0.0003 ns | 0.000 | 4 B       |
| RegularCastArgument | ?                  | 1.0396 ns | 0.0044 ns | 0.0036 ns | 1.000 | 25 B      |
| UnsafeCastArgument  | (10)               | 0.0003 ns | 0.0005 ns | 0.0005 ns | 0.000 | 4 B       |
| RegularCastArgument | (10)               | 2.1023 ns | 0.0044 ns | 0.0041 ns | 1.000 | 25 B      |
| UnsafeCastArgument  | (10, 20)           | 0.0001 ns | 0.0002 ns | 0.0002 ns | 0.000 | 4 B       |
| RegularCastArgument | (10, 20)           | 2.1028 ns | 0.0056 ns | 0.0049 ns | 1.000 | 25 B      |
| UnsafeCastArgument  | (10, (…)8, 9) [38] | 0.0002 ns | 0.0005 ns | 0.0005 ns | 0.000 | 4 B       |
| RegularCastArgument | (10, (…)8, 9) [38] | 2.3068 ns | 0.0033 ns | 0.0027 ns | 1.000 | 25 B      |

为什么使用 `Unsafe` 就能提高性能呢？从汇编代码中可以看出，它的操作非常简洁。

```assembly
mov       rax,rdx
ret
```

而更加安全的 `Cast` 的汇编代码如下

```Csharp
sub       rsp,28
mov       rcx,offset MT_System.Runtime.CompilerServices.ITuple
call      CORINFO_HELP_CHKCASTINTERFACE
nop
add       rsp,28
ret
```

## 开源项目

1、[依赖注入的生命周期](https://exceptionnotfound.net/dependency-injection-in-dotnet-6-service-lifetimes/)

在 `Microsoft.Extension.DependencyInjection` 包中，服务的生命周期是非常重要的概念，它们有

- Transient
- Scoped
- Singleton

这篇文章用实例展示了这三个概念的定义和区别。

2、[.NET 后端程序员之路 2022](https://github.com/Elfocrash/.NET-Backend-Developer-Roadmap)

对 `.NET` 后端程序员程序员，查看一下 2022 年的 `Roadmap`

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/565e5973-82f0-4465-a52e-6d0b2c64f671)

