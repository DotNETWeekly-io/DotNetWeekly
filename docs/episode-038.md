# .NET 每周分享第 38 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/acfc18c7-a5cc-47c0-821d-987ab9206a83)

我们的大部分开发工作都是在 `Visual Studio` 中完成，当我们在自己的分支上完成工作，就需要登录到 `GitHUB` 或者 `Azure DevOps` 上创建 PR。在新的  `Visual Studio` 中，我们就可直接在 `Visual Studio` 中直接创建 PR。 基本流程是这样的
1. 创建新的分支
2. 提交和推送分支
3. 创建 PR 
4. 修改并更新 PR

## 行业资讯

1、[qodana](https://blog.jetbrains.com/dotnet/2023/06/29/elevating-csharp-code-quality-with-qodana-a-journey-towards-perfection/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/acaceab3-8701-45d6-8eea-cea8c019ce2e)

JetBrains 为 `.NET` 应用程序的代码质量推出了一个工具叫做 `qodana for .NET`，它既可以集成到 `CI` 工具中，也可以在本地执行，并且生成网页版本的质量报告。

## 文章推荐

1、[C# 中的 GUID](https://code-maze.com/csharp-guid-class/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/17f8603a-2c73-467f-924e-63cd70752b96)

`GUID` 是一个 128 bit 组成的值，用来减少冲突。在 `C#` 中 GUID 有以下几个特征

1. 它是一个值类型，所以默认值为 `00000000-0000-0000-0000-000000000000`， 也等于 `Guid.Empty`
2. 随机创建 GUID 的方式是 `GUID.Empty`
3. 它的 `ToString` 方式有多种：

```C#
var inputGuid = Guid.NewGuid(); 
Console.WriteLine(inputGuid.ToString()); // 081bdc64-b126-4ac4-a1a5-96d41c8622fc
Console.WriteLine(inputGuid.ToString("D")); //081bdc64-b126-4ac4-a1a5-96d41c8622fc
Console.WriteLine(inputGuid.ToString("N")); // 081bdc64b1264ac4a1a596d41c8622fc
Console.WriteLine(inputGuid.ToString("B")); // {081bdc64-b126-4ac4-a1a5-96d41c8622fc}
Console.WriteLine(inputGuid.ToString("P"));  //(081bdc64-b126-4ac4-a1a5-96d41c8622fc)
```

2、[C# 编码过程中的建议](https://scottsauber.com/2023/06/16/iowa-code-camp-2023-10-things-i-do-on-every-net-app/)

Scott Sauber 是 `Microsoft MVP`, 他分享了他关于 `.NET` 应用程序的原则

1. 在 `ASP.NET Core` 中使用功能的方式组织文件夹
2. 将 `Warning` 当作 `Error`, 在 `csproj` 文件中这么配置

```xml
<propertyGroup>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</propertyGroup>
```
3. 正确使用日志
4. 使用`FallbackPolicy` 全局使用 `Authorize` 标注
5. 删除 `Asp.NET Core` 的 Server Header
6. 不要使用 `IOption` 作为依赖注入的选项，使用具体的值
7. `Endpoint` 要版本化
8. 结构化方法，比如正确的结果在方法的最后，使用 `return` 而不是 `if-else`
9. 注意 `HTTP` 安全方面的控制
10. 构建一次到处部署

3、[Visaul Studio NewFile](https://www.youtube.com/watch?v=3dpFeNV1smU&ab_channel=IAmTimCorey)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/bb76c35e-73a3-4e76-a1bd-a1790c42feb5)

在过去的 `Visual Studio` 中，在添加新的文件的时候，通常会弹出上面的窗口，它会展示所有的已经安装的模板。在新版的 `Visual Studio` 中，我们可以使用更加简单的对话框，在这里就直接直接输入文件的名字，Visual Studio 会根据模板创建相应的文件，而且也支持多个文件，文件夹的创建。

4、[ASP.NET Core WebSocket 客户端服务端通信](https://medium.com/bina-nusantara-it-division/implementing-websocket-client-and-server-on-asp-net-core-6-0-c-4fbda11dbceb)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/d53389c0-51d2-4799-8f35-fb8a48283ca1)

`ASP.NET Core` 支持 `WebSocket` 开发，这篇文章展示了如果使用 `WebSocket` 实现一个简单的聊天室功能。

5、[StringBuilder 中 Replace 功能](https://khalidabuhakmeh.com/using-stringbuilder-to-replace-values)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e3078594-f6a3-40a6-8cd5-b7bb4baca795)

我们都知道在 `C#`  中， `StringBuilder` 在字符串处理方面比 `string` 操作更加高效，通常是发生在字符串拼接的使用场景中。但是在 `Replace` 的操作中，`StringBuilder` 的效率也是比 `String` 效率高。

```csharp
Dictionary<string, string> _opMapper = new()
{
	{"×", "*"},
	{"÷", "/"},
	{"SIN", "Sin"},
	{"COS", "Cos"},
};

var retString = new StringBuilder(inputString);
foreach (var key in _opMapper.Keys)
{
     retString.Replace(key, _opMapper[key]);
}
return retString.ToString();
```

6、[Nuget 包中添加 ReadME](https://khalidabuhakmeh.com/adding-a-readme-to-nuget-package-landing-pages)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/22bc4da4-20ad-4490-9da4-15f1ed566a78)

当我们在浏览 `Nuget` 包的时候，通常希望有一个精美的 `README` 文件来描述这个包，并且包含丰富的文档链接。那么在 `csproj` 文件中包含整个就可以了。

```xml
<PropertyGroup>
    <PackageIcon>icon.png</PackageIcon>
    <RepositoryUrl>https://github.com/khalidabuhakmeh/Htmx.Net</RepositoryUrl>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <!-- IMPORTANT Do not forget this -->
    <PackageReadmeFile>README.md</PackageReadmeFile>
    <Description>
        Adds ASP.NET Core tag helpers to make generating urls for Htmx (https://htmx.org) easier. Mimics the ASP.NET Core url tag helpers.
    </Description>
</PropertyGroup>
```

7、[关于 Logging 的八个原则](https://www.youtube.com/watch?v=NlBjVJPkT6M&t=755s&ab_channel=NDCConferences)

关于 `Logging`, `Nick` 在 NDC 大会上分享他的原则：

1. 日志中的 `message` 参数其实是模板
2. 使用结构化日志并且给参数合适的名字
3. 不在模板中使用字符串拼接的方式
4. 对于 `inactive` 的日志级别，避免装箱操作
5. 考虑使用 `Source Generated` 的日志
6. 不要尝试比工具更加聪明
7. 考虑使用 `Warning` 作为默认级别
8. 日志只记录讲好一个故事的必要信息

## 开源项目

1、[html-agility-pack](https://github.com/zzzprojects/html-agility-pack/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/dfcbce3e-8f2d-47c7-b389-1a714510c32c)

当我们在使用 `C#` 对网页 `HTML` 元素进行解析，通常我们会采取类型字符串正则匹配的方式，这样做太繁琐了。`HTMLAgilityPack` 包可以然我们类似 `HTML` 元素选择的方式获取想要的结果。

```csharp
var html = @"http://html-agility-pack.net/";
HtmlWeb web = new HtmlWeb();
var htmlDoc = web.Load(html);
var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
```

2、[AutoFixture](https://github.com/AutoFixture/AutoFixture)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/92893717-5768-4b77-9f7c-cb901c597fe5)

AutoFixture 是一个单元测试辅助开发库，用来帮助我们开发的单元测试库只关心我们需要测试的内容，而不需要被其他的修改而影响。

假设我们现在有一个 `Employee` 的类

```csharp
public class Employee
{
    public Employee(string firstName, string lastName)
    {
        FirstName = firstName; 
        LastName = lastName;
    }
    public string FirstName { get; set; }
    public string LastName {get; set;}
    public string FullName => $"{FirstName} {LastName}";
}
```

我们的单元测试是这样的

```csharp
public void FullName_Return_Expected()
{
    var sut = new Employee("feng", "gao");
    var actual = sut.FullName;
    Assert.Equal("feng gao", actual);
}
```

这个单元测试没有问题，如果我们的 `Employee` 的构造函数被修改成这样

```diff
- public Employee(string firstName, string lastName)
+ public Employee(string firstName, string lastName, int employeeNumber)
```

毫无疑问，我们之前的单元测试肯定失败，甚至无法编译。这时候我们的 `AutoFixture` 起作用的的

```csharp
public void FullName_Return_Expected()
{
    var fixture = new Fixture();
        var sut = fixture.Build<Employee>().With(a => a.FirstName, "feng")
            .With(a => a.LastName, "gao").Create();
    var actual = sut.FullName;
    Assert.Equal("feng gao", actual);
}
```
