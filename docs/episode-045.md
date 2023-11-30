# .NET 每周分享第 45 期

## 卷首语

.NET 8 正式发布

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c050ada1-5ba8-4380-9f99-c80ef22bf2ab)

一年一度的 `.NET` 新版本发布如约而至，上周 `.NET 8` 正式推出，主要包含如下几个主题

1. 性能提升
2. Aspire 推出
3. Native  AOT
4. Artificial Intelligence 集成
5. Blazor
6. .NET MAUI

## 文章推荐

1、[.NET 8 中的新内容](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/7d5dc9eb-439c-4ea2-8ae0-68507ba1a9b8)

官方文档关于 .NET 8 的新内容介绍，主要有
1. .NET Aspire
2. ASP.NET Core
3. Core .NET libraries
4. Extension libraries

2、[C# 12 中所有特色](https://www.youtube.com/watch?v=Gv2uBJzBAms&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/72d38a87-ffee-4797-a994-86b782dc995c)

随着 `.NET 8` 一同发布还有 `C# 12`, 它包含了如下的新功能

1. Primary Constructor 
2. Collection Expression
3. Ref read-only parameter
4. Default Lambda parameter
5. Alias of type
6. Inline array
7. Experimental attribute
8. Interceptor

3、[使用 Switch 简化判断条件](https://www.youtube.com/watch?v=dji9QDstOG0&ab_channel=MilanJovanovi%C4%87)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3f9286f0-b849-4df2-afbb-2d11b272fe9f)

C# 中的 `switch` 语句可以用在模式匹配，而且功能非常强大。

```csharp
internal class Error {}
internal class NotFoundError : Error {}
internal class ConflictError : Error { }
internal class Result
{
    public bool IsSuccess { get; set; }
    public Error Error { get; set; }
}
```

那么 switch 语句可以根据 `Result` 的不同的结果可以选择不同的执行语句

```csharp
Result result = new Result
{
    IsSuccess = false,
    Error = new ConflictError(),
};

var r = result switch
{
    { IsSuccess: true } => 0,
    { Error: NotFoundError } => 404,
    { Error: ConflictError } => 500,
    _ => -1
} ;
```

5、[Dotnet CLI 列表](https://cheatography.com/oba/cheat-sheets/dotnet-cli/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/8f07be00-bc04-41af-b933-3aaf611a7992)

开发 `.NET` 应用程序除了使用 VS 或者 Rider， 我们还可以使用 CLI 命令来操作相关的事情，这里有一张表列出了所有命令行工作的集合


6、[SearchValues](https://www.youtube.com/watch?v=IzDMg916t98&ab_channel=NickChapsas)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c92740c2-96e1-4499-ba2c-cebe0a6adabc)


.NET 8 提供了一个新的类型 `SearchValues`, 它可以用来判断某个数据是否在一个集合。它的性能非常优秀，这是 `Benchmark` 的结果

```csharp
private static string Base64Text = "abcdefghijklmnopgrstuvwxyzABCDEFGHIJKLMNOPGRSTUVWXYZ0123456789+/";

private static char[] base64Chars = new char[]
{
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
    'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/'
};


private static SearchValues<char> base64SearchValue = SearchValues.Create(Base64Text);

public static bool ContainsNaive(string input)
{
    for (int i = 0; i < input.Length; i++)
    {
        var character = input[i];
        if (!base64Chars.Contains(character))
        {
            return false;
        }
    }
    return true;
}

public static bool ContainsAsSpan(string input)
{
    return input.AsSpan().IndexOfAnyExcept(Base64Text) == -1;
}

public static bool ContainsSearchValue(string input)
{
    return input.AsSpan().IndexOfAnyExcept(base64SearchValue) == -1;
}

```

## 开源项目

1、[Aspire](https://github.com/dotnet/aspire)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/cd9279c0-4df9-47f5-a4d1-ad2c1fbb8c50)

`Aspire` 是一个开源的微服务管理平台，云原生的应用程序通常是由小的，互相链接和微服务组成，这个项目能够管理这些项目，包含：
1. Orchestration
2. Components
3. Tooling


2、[refit](https://github.com/reactiveui/refit)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/4a24b07b-3a6a-42fd-9523-f5f6e6a0c158)

`refit` 是一个非常有意思的库，它只需要我们定义好 `HTTP` 请求的格式，它会自动帮我们创建好需要的执行的方法，比如

```csharp
public interface IGitHubApi
{
    [Get("/users/{user}")]
    Task<User> GetUser(string user);
}
```
我们这定义了一个 `IGithubApi` 的接口，然后依赖注入的方式，创建一个具体实现的类

```csharp
var gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");
```

这样就可以用 `gtihubAPi` 实例来调用 `GetUser` 方法。


3、[HashIds](https://github.com/ullmark/hashids.net)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/2cbdc949-edc1-437a-8cc2-e9640b30fdc6)


当我们在浏览 YouTube 的时候，我们会发现 URL类似 `https://www.youtube.com/watch?v=k0AV7cL9qSc`, 其中 query parameter 中的 `k0AV7cL9qSc` 通常是是一个 ID 值的编码，通过这种方式可以隐藏细节。 

`.NET` 社区中的 `HashIds` 库可以完成这一项工作

```csharp
using HashidsNet;

var hashIds = new Hashids("tizan");
var hash = hashIds.Encode(42);
Console.WriteLine($"42 encode => {hash}");

var numbers = hashIds.Decode("jR");
if (numbers.Length == 1)
{
    Console.WriteLine($"jR decode => {numbers[0]}");
}
```

`"tizan"` 是整个 hashIds 的 `salt`, 通过它可以将数值转化成为成字符串，也可以将字符串转换成特定的数值。


4、 [Moonsharp](https://github.com/moonsharp-devs/moonsharp/)


![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6cb74b43-0ffd-4bd4-b903-10d51c5e6db7)

`Moonsharp` 这个库可以实现在 `C#` 和 `Lua` 代码的互操作。
