# .NET 每周分享第 9 期

## 开卷语

20 年前，微软发布了 `Visual Studio .NET` 这个产品，20 年后，`.NET` 平台已经发生了巨大的变化，最大的变化是从之前 `Windows-Only`  平台变成了开源跨平台的产品，并且得到了开源社区的广泛支持。

## 行业资讯

1、[Visual Studio 生命周期支持到期](https://devblogs.microsoft.com/visualstudio/support-ends-for-older-versions-of-visual-studio-feb2022/)

![](https://offlineinstallersofts.com/wp-content/uploads/2017/10/Microsoft_Visual_Studio_2012.jpg)

微软发布了老版本的 `Visual Studio` 的支持周期，`Visual Studio 2012` 和 `Visual Studio 2017` 已经快到达支持周期的末尾。

2、[.NET 7 Preview 发布](https://devblogs.microsoft.com/dotnet/announcing-net-7-preview-1/)

![](https://res.cloudinary.com/practicaldev/image/fetch/s--oFeBR7WR--/c_imagga_scale,f_auto,fl_progressive,h_420,q_auto,w_1000/https://dev-to-uploads.s3.amazonaws.com/uploads/articles/ozqillljnmv8ao1jmnxb.png)
作为 `.NET` 20 周年的活动的一部分，微软已经发布了 `.NET 7` Preview 版本，主要包含一下几个重要的内容:

- MAUI
- 云原生和容器支持
- .NET 应用程序无缝升级
- ...

## 文章推荐

1、[C# 代码中 Nullable Type](https://csharp.christiannagel.com/2022/02/14/nullable/)

![](https://miro.medium.com/max/638/1*G9V4FjA0RK4nqZfsEak1Gg.jpeg)

我们都知道 `NULL` 类型是一个
> 百万美元的错误 

在 `C#` 代码中就会抛出 `NullReferenceException` 的异常， C# 在消除这种异常的道路上不停的探索

- Nullable Value Type

因为值类型不能为 `Null`, 那么就会带来歧义：1） 值本身就是指空值；2）不存在。 因此在 `C# 2` 中引入了这个值空类型

- Null-Conditional Operator

在使用对象的方法之前，需要判断一下是否为 `null`

```C#
if (instance  != null) 
{
   instance.DoSomething();
}
```
在 `C# 6` 之后，就可以这样使用 

```C#
instance?.DoSomething();
```

- Null-Coalescing Operator 

如果为 `null` 之后，需要进行其他的操作，通常需要写 `if-else` 语句，在 `C# 8` 之后，可以使用 `??` 开执行 `null` 条件下的语句。

```C#
get => instance ??= new Instance();
```

- Null Reference Type 

值类型不可以赋值 `null`, 但是引用类型可以赋值 `null`, 这也是很多 `NullReferenceException` 异常的来源，那么在 C# 中，如果给一个变量没有显式的标记为 `nullable` 类型，那么就不能直接赋值 `null`，由于这是一个 break change, 需要手动开启这个功能

```C#
string? s = ReturnANullableString();
if (s is not null) 
{
    string uppercase1 = s.ToUpper();
}
string uppercase2 = s?.ToUpper();

- ArgumentNullException 

在防御性编程中，需要对输入的参数进行校验，如果为 `null` ,  则抛出异常，通常需要写很多 `if-else` 语句。 在 `.NET 6` 和 `C# 10` 中可以简化这个流程

```C#
public void Foo(string s)
{
    ArgumentNullException.ThrowIfNull(s);
}
```

2、[关于 .NET 运行在 Linux， 你需要这些事情](https://dotnetcore.show/episode-92-a-few-things-i-wish-i-knew-before-writing-net-on-linux/)

![](https://upload.wikimedia.org/wikipedia/commons/3/35/Tux.svg)

对于 `.NET` 开发者而言，`Windows` 是最熟悉的开发平台。但是 `.NET` 已经是一个跨平台的开发平台，了解 `Linux` 已经是 `.NET` 开发人员必需能力。如果你是 `Linux` 的小白的话，这篇文章带你熟悉这个平台。

## 开源项目

1、[字面字符串提案](https://github.com/dotnet/csharplang/blob/main/proposals/raw-string-literal.md)

我们都知道在 `C#` 中，字面字符串支持的还是不够好，虽然说可以通过 `@""` 完成，但是还是需要填上大量转义的字符， 因此该提案借鉴了 `Python` 的方式，比如

```C#
var xml = """
<element attr="content"/>
""";
```

通过至少 3 个 `"` 符号表示字面字符串的开始，用相同数量的 `"` 表示结束，这样就可以很方便的表示字面字符串。