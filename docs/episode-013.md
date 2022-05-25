# .NET 每周分享第 13 期

## 卷首语

[Nullable 类型](https://blog.maartenballiauw.be/post/2022/04/11/nullable-reference-types-in-csharp-migrating-to-nullable-reference-types-part-1.html)

Null 是一个百万美元的错误，C# 代码花费了很多精力来消灭这个错误，在 `C# 8.0` 中引入了 `nullable reference types` 来解决这个问题。

### Overview 
对于一个值类型，C# 已经有了很好的解决方案，就是 `Nullable<T>` 的类型，它表示该值可以为 `null` 也可以是需要的 `T` 类型的值，一种简写的方案是 `T?` ，比如 `int?`, `Datetime?`, `Guid?` 等等。
但是对于引用类型，它本身就可以表示为 `null`，比如说 

```Csharp
string s = GetValue();
```
这里的 `s` 既可以表示为普通的字符串，也可以表示为一个`null`。在 `C#` 工程文件中，可以通过这种方式开启 `NRT` 

```
<Nullable>enable</Nullable>
```

这时候，我们的代码如下 

```Csharp
string? s  = GetValue();
```
这时候，编译器知道 `s` 既可以是普通的字符串，也可以为 `null`，如果我们的代码如下

```Csharp
string s = GetValue();
```
这时候 `s` 肯定非 `null`，换句话说就是 `NRT` 开启后，普通的引用类型就不能再为 `null`。  

### IL 代码

熟悉 `C#` 的知道，`NRT` 同样也是一种*语法糖*，那么对于这样的代码

```Csharp
int? GetInt() => 1;
string? GetString() => "";
```
转换成 `IL` 代码如下

```IL
.method private hidebysig instance valuetype [System.Runtime]System.Nullable`1<int32>
  GetInt() cil managed
{
  .maxstack 8

  IL_0000: ldc.i4.1
  IL_0001: newobj    instance void valuetype [System.Runtime]System.Nullable`1<int32>::.ctor(!0)
  IL_0006: ret
}

.method private hidebysig instance string
  GetString() cil managed
{
  .custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor([in] unsigned int8)
    = (01 00 02 00 00)
  .maxstack 8

  IL_0000: ldstr      ""
  IL_0005: ret
}
```

显而易见的是，`int?` 代码转换成  `[System.Runtime]System.Nullable1<int32>` 表示，而 `string?` 则添加了`NullableContextAttribute` 的注解。 该注解的构造支持三种参数
- 0： 忽视，保持 `C# 8` 以前版本一致
- 1:  非注解，表示该引用类型不能为 null
- 2： 注解，表示该引用类型可以为 null 

通过这种方式，编译器的分析工具就能工作，为代码提供更多 null 检查。工程文件中的 `Nullable` 元素也有下面四种选择
1. `disable` - 不开启 NRT 
2. `enabled` - 开启 NRT 
3. `warning` - 开始 NRT 不过编译时候出现 Warning
4. `anotations` - 只开启 `?` 语法功能

除此之外，也在在单个文件中开启 NRT。

### 注解代码

虽然有了 `?` 可以帮助编译器分析我们的代码，但是这个还不够，我们需要使用代码注解来提高编译器的分析能力。
```Csharp
public static string? Slugify(string? value)
{
    if (value == null)
    {
        return null;
    }

    return value.Replace(" ", "-").ToLowerInvariant();
}
```

`Slugify` 方法如果传入的参数不为 `null`，那么返回值就不是 `null`， 这样的代码其实并不需要做 `null check`。

```Csharp
var slug = Slugify("This is a test");
Console.WriteLine(slug.Length); /
```

如果我们在方法中添加代码注解，那么编译器就能知道返回值不为  null.

```Csharp
[return: NotNullIfNotNull("value")]
public static string? Slugify(string? value)
{
    // ...
}

var slug = Slugify("This is a test");
Console.WriteLine(slug.Length); // All good! Known to be not null
```
这里的 `[return:NotNullIfNotNull("value")]`  告诉编译器，如果 `value` 参数不为 `null`， 则返回值不为 `null`，这样就跳过了 `null check`。除了这个注解之外，还有更多的注解。

### 如何使用

软件开发没有银弹，NRT 是一个非常棒的特性，但是作为一个 breaking-change 的功能，完全采取 `NRT` 仍然需要大量的工作的要做，一般而言方案如下
1. 如果是小的项目，可以直接在工程文件中开启 `<Nullable>enable<Nullable>` 功能
2. 对于大型项目，可以先在工程项目中关闭 NRT，然后依次在文件中开启该功能
3. 也可以设置为 `warning`, 然后再每个文件中开启，然后逐步解决掉每个 warning. 

## 行业资讯

1、[Visual Studio 中浏览 IEnumerable 对象](https://github.blog/2022-03-29-github-copilot-now-available-for-visual-studio-2022/)

![](https://devblogs.microsoft.com/visualstudio/wp-content/uploads/sites/4/2022/04/a-screenshot-of-a-computer-screen-description-aut.gif)

在 `Visual Studio` 中如果调试 `IEnumerable` 对象的时候，通常是比较难受的体验，因为需要挨个查看其中的每个对象。在  Visual Studio 17.2. Preview 2 中，可以使用表格的方式渲染每个对象，每一行是代表每个对象，而每一列则是对象的属性，表格甚至可以用 Excel 打开。

## 文章推荐

1、[MAUI 尝试](https://codeblog.jonskeet.uk/2022/04/16/taking-net-maui-for-a-spin/)

![](https://pluralsight.imgix.net/author/lg/jon-skeet-v1.jpg)

*Jon Skeet* 是 `.NET`  社区大名鼎鼎的人物，近期尝试了 `MAUI` 的最新版本，将他之前的 `Windows-only` 的应用扩展成一个 `MacOS` 和移动端的应用程序。
总结起来两点：
1. 这个一个非常有用的平台，因为它扩展了原本的限制；
2. 目前还不是很完善，有很大的改进空间。

2、[格式化 C# 代码](https://blog.stephencleary.com/2022/02/cancellation-1-overview.html)

![](https://tugberkugurlu.blob.core.windows.net/bloggyimages/34f855f2-d604-423a-816b-7798b682bfa9.png)

`.NET 6` 将 `dotnet format` 命令集成进来，这个工具可以帮助我们格式化代码，使他们符合我们定义好的规则，通常这些规则定义在 `.editorconfig` 文件中，否则就使用一些默认的规则，或者分析器的规则。
虽然目前 IDE 已经集成了这个工程，但是每个 IDE 都是不一样的，而且开发者需要记住这些命令，因此使用 

```
dotnet format
```
更加是更加好的选择，而 

```
dotnet format --verify-no-changes
```

可以将违反规则的代码展示出来，并且返回非 0 的返回码，比如说

```
Run dotnet format --verify-no-changes
Warning: /home/runner/work/dotnet-format/dotnet-format/DotnetFormatExample/Calculator.cs(7,13):
warning IDE0007: use 'var' instead of explicit type 
[/home/runner/work/dotnet-format/dotnet-format/DotnetFormatExample/DotnetFormatExample.csproj]
Error: Process completed with exit code 2.
```

这样可以将 `dotnet format` 命令集成到 CI 中，这样任何人做出了违反规则的 PR 都失败在 CI 这一步。

3、[ASP.NET Core 面试](https://khalidabuhakmeh.com/aspnet-core-interview-questions-and-answers)

![](https://www.totaljobs.com/advice/wp-content/uploads/most-common-interview-questions.jpg)

作为一个 `ASP.NET Core` 开发人员，那么如果面试中，问了这些问题，你该如何回答呢？
- 你认为 `ASP.NET Core` 是一个理想的 Web 应用程序开发框架吗？如果不是，请问它缺失了什么？
- `ASP.NET Core` 包含了一个依赖注入，那么你主要到不同的生命周期了吗？他们之间有什么不同？
- 你是更加喜欢使用显式的终端路由，还是使用规范的方式？并且说出原因？
- 在 `ASP.NET Core` 中，还使用哪些第三方的库？
- 如果你在网页中定义了一个 `Form`, 但是在提交的时候，并没有预期的结果发生，你会如何 debug? 

4、[C# 程序员学习 C++](https://www.jacksondunstan.com/articles/5530)

![](https://miro.medium.com/max/460/1*sz9n_vb48iaY9vuzIgqJqQ.png) 

对于 `Unity` 开发人员来说，`C#` 是一门必须要掌握的开发语言。但是对于其他游戏开发引擎而言，`C++` 则是一门更加通用的开发语言，这个教程介绍了 `C#` 开发人员如何学习 `C++`，并且比较他们的异同点。

5、[C# 代码规则](https://christianfindlay.com/2022/04/24/code-rules/)

![](https://user-images.githubusercontent.com/46729679/109719841-17b7dd00-7b5e-11eb-8f5e-87eb2d4d1be9.png) 

`Roslyn` 是 C# 编译器，它包含了各种代码规范的规则。在开发过程中，打开 `Roslyn` 分析器可以帮助我们写出更好的代码。

6、[Microsoft Graph 迁移到 .NET 6](https://devblogs.microsoft.com/dotnet/microsoft-graph-dotnet-6-journey/)

![](https://www.drupal.org/files/project-images/MicrosoftGraphAPI.png)

Microsoft Graph 团队分享了代码库从之前的 `.NET Framework` 迁移到 `.NET 6` 的过程。结果是显而易见的，在性能上取得了巨大的成功，而且为将来引入更多先进的功能提供了可能。在文章中也给出了迁移的步骤：
1. 构建系统现代化
2. 准备好架构
3. .NET Framework 依赖的库存
4. 从项目库中移除 `.NET Framework` 的依赖
5. 避免被 block
6. 为新的 Web 服务构建 `ASP.NET Core` 应用
7. 做好 `a/b` 测试
8. 为所有的羡慕迁移至 `.NET Core` 

## 开源项目

1、[运行在 .NET 中的 javascript](https://andrewlock.net/running-javascript-in-a-dotnet-app-with-javascriptengineswitcher/)

如何你想要在 `.NET` 应用程序中运行 `Javascript` 脚本，该选择那个库完成呢？这篇文章比较了目前比较流行的库，并且发现了一个 `JavaScriptEngineSwitcher` 库，它可以自由切换这些流行的开源库。

2、[使用 C# 实现贝叶斯分类](https://visualstudiomagazine.com/articles/2022/05/02/naive-bayes-classification-csharp.aspx)

![](https://miro.medium.com/max/768/1*zPseemLGYHMS8M0phAhhoA.png)

朴素贝叶斯分类是机器学习中一种重要的分类方法， 对于离散型数值分类有着广泛的应用。这篇文章介绍如何使用 `C#` 实现一个简单的朴素贝叶斯分类器。
