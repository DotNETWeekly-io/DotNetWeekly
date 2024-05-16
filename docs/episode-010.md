# .NET 每周分享第 10 期

## 开卷语

最近俄罗斯和乌克兰的局势从战场蔓延到整个科技圈，很多知名的开源项目已经开始*站队*。`.NET` 社区也不例外，也发出了

> Stand for Ukraine

我的观点是作为标榜自由，开源的项目，这本质上应当保持中立，如果非要是支持，就应当只关心人道主义方面的内容，而不是一味的制裁单方面，因为作为一个开源项目，项目的参与者是整个人类，而不是筛选的部分人。

## 行业资讯

1、[C# 11 抢先看](https://devblogs.microsoft.com/dotnet/early-peek-at-csharp-11-features/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/88705deb-746c-4cd4-acbf-d36cf167ea7c)

随着 `Visual Studio 17.1` 发布，`C# 11` 已经可以尝试了。
首先需要修改 `.csproj` 文件

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net6.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <LangVersion>preview</LangVersion>
    </PropertyGroup>
</Project>
```

1. 插入字符串支持新行

在过去如果多行字符串中添加新行，需要使用 `\r\n` 来进行转义，但是在 `C# 11` 中，我们可以直接使用原始多行字符串

```Csharp
var v = $"Count ist: { this.Is.Really.Something()
                            .That.I.Should(
                                be + able)[
                                    to.Wrap()] }.";
```

2. List Pattern

模式匹配现在可以支持数组或者列表，比如

```Csharp
public static int CheckSwitch(int[] values)
    => values switch
    {
        [1, 2, .., 10] => 1,
        [1, 2] => 2,
        [1, _] => 3,
        [1, ..] => 4,
        [..] => 50
    };
```

也可以捕获列表中的切片变量

```Csharp
public static string CaptureSlice(int[] values)
    => values switch
    {
        [1, .. var middle, _] => $"Middle {String.Join(", ", middle)}",
        [.. var all] => $"All {String.Join(", ", all)}"
    };
```

3. 参数 null 检查

过去需要在函数/方法中对输入的参数进行 null 检查

```Csharp
public static void M(string s)
{
    if (s is null)
    {
        throw new ArgumentNullException(nameof(s));
    }
    // Body of the method
}
```

现在只需要在参数中使用双感叹号（`!!`）， 也能达到同样的效果。

```Csharp
public static void M(string s!!)
{
    // Body of the method
}
```

## 文章推荐

1、[Dictionary 循环的比较](https://code-maze.com/csharp-iterate-through-dictionary/)

对于一个 `Dictionary` 有多少中循环方式呢？
假设存在一个字典如下

```Csharp
var monthsInYear = new Dictionary<int, string>();
```

1. 使用 `Foreach` 方法

```Csharp
public static void SubDictionaryUsingForEach(Dictionary<int,string> monthsInYear)
{
    foreach (var month in monthsInYear)
    {
        Console.WriteLine($"{month.Key}: {month.Value}");
    }
}
```

2. 使用 `For` 方法

```Csharp
public static void SubDictionaryForLoop(Dictionary<int, string> monthsInYear)
{
   for (int index = 0; index < monthsInYear.Count; index++)
   {
       KeyValuePair<int, string> month = monthsInYear.ElementAt(index);

       Console.WriteLine($"{month.Key} : {month.Value}");
   }
}
```

3. 使用 `ParallelEnumerable.ForAll` 方法

```Csharp
public static void SubDictionaryParallelEnumerable(Dictionary<int, string> monthsInYear)
{
    monthsInYear.AsParallel()
                .OrderBy(month => month.Key)
                .ForAll(month => Console.WriteLine($"{month.Key} : {month.Value}"));
}
```

那么结果比较结果如何呢？

| Method                           |          Mean |       Error |      StdDev |
| -------------------------------- | ------------: | ----------: | ----------: |
| WhenDictionaryUsingForEach       |     4.7635 ns |   0.0314 ns |   0.0245 ns |
| WhenDictionaryUsingForLoop       |     0.5715 ns |   0.0475 ns |   0.0421 ns |
| WhenDictionaryParallelEnumerable | 7,662.7620 ns | 150.1402 ns | 172.9016 ns |

显而易见， `For` 和 `Foreach` 的性能更好点，而且 `For` 的性能还更好点。

2、[如何使用 Visual Studio 进行 Debug](https://code-maze.com/debugging-csharp-visual-studio/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9c8138c6-f21d-4fcb-96af-4c0752586a93)

Visual Studio 被誉为

> 宇宙第一 IDE

它的强大不仅仅在于编写代码，而是强大的 `Debug` 功能，这篇文章基本上介绍 `Visual Studio` Debug 功能

- 什么是 Debug
- 如何设置断点
- 如何查看变量
- 条件 debug
- 调用栈

3、[在 .NET 中使用 GitHub Action](https://devblogs.microsoft.com/dotnet/dotnet-loves-github-actions/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/03f542a4-7227-445d-843a-3c4873eb751a)

GitHub Action 是一套 CI/CD 的的工具，通过它能够实现自动化运维的目的，比如自动测试，自动构建和自动部署等功能。对于 .NET 用户而言需要掌握这些 GitHub Action 的功能。

4、[.NET 存在的 6 个误解](https://blog.devgenius.io/6-net-myths-dispelled-celebrating-21-years-of-net-652795c2ea27)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/1474d48b-2465-4f79-889b-fd38278c4e17)

`.NET` 已经存在了 20 年了，但是大家仍然对他存在误解，这里挑选了 6 个误解并且解释它们

- .NET 只为 Windows
- .NET 比 Node，Python，Go，Rust 慢
- .NET 是一个老旧的开发平台
- .NET 开发工具太贵了
- .NET 对开源并不友好
- .NET 只为企业级开发

## 开源项目

1、[C# Reverse Proxy](https://github.com/microsoft/reverse-proxy)

Ngnix 是著名的反向代理工具， 微软开源了一款用 `C#` 编写的反向代理库：`YARP`。

2、[MoreLinq 来增强 Linq 的功能](https://morelinq.github.io/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/62adda23-cb00-4082-a67e-c5b2052ec195)

Linq 是 `C#` 最受欢迎的功能之一，但是内置的 `Linq to Object` 的功能还是无法满足部分开发需求，因此 `MoreLinq` 扩展了这部分内容。

3、[stryker 测试你的单元测试](https://stryker-mutator.io/docs/stryker-net/introduction)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/1bcd99f0-2248-49a1-953b-2dffbc940016)

单元测试是软件开发中重要组成部分，通常我们评价单元测试好坏使用的是**覆盖率**。但是实时真的如此吗？如果单元测试用例正好通过了代码分支，而且是一个特殊用例，这样说明这个单元测试质量并不好。`Stryker.NET` 包能够帮助我们检查我们单元测试质量。

首先它引入了 `Mutation` 的的概念，它是修改我们代码中的部分片段，比如 `+` 修改为 `-`, `i++` 修改为 `i--` 等等，然后再去执行我们单元测试，这是会有两种情况：

- `Killed`: 表明单元测试在修改之后失败
- `Survived`: 表明单元测试在修改之后仍然通过

`Killed` 情况越多表明单元测试质量越高。
