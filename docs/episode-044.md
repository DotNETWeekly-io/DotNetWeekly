# .NET 每周分享第 44 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/f612de05-a497-471f-9369-2521734d319b)

微软推出了 `.NET 8` Hack [项目](https://devblogs.microsoft.com/dotnet/join-us-for-the-great-dotnet-8-hack/)，在 11 月 20 号到 12 月 4 号期间，使用 `.NET 8`开发云原生，AI 或者 MAUI 的项目都可以获取将奖金。

## 行业资讯

1、[F# 编程项目](https://d3s.mff.cuni.cz/teaching/nprg077/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/289def6b-c148-4356-98ec-3c72e7a15141)

这里包含了四门课程，主要是是通过 `F#` 编写自己的编程系统。

## 文章推荐

1、[Logger Message Generator](https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/693e3483-0471-48d4-b3d3-c9c03b54dcc1)

Log 的方法的的签名是这样的

```csharp
 public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, Exception? exception, string? message, params object?[] args)
```

显而易见，最后一个参数 `args` 就是格式化参数，类如 `string.format`。如果对内存有了解下的话，这里会出现装箱的操作，比如传参数的类型基本数据类型，就会发生装箱操作。
`.NET 6` 提供了 logger generation 的功能，它可以生成类型匹配的日志方法从而避免的装箱。

```csharp
public static partial class Log
{
    [LoggerMessage(EventId = 23, Message = "{name} lives in {city}.")]
    public static partial void PlaceOfResidence(this ILogger logger, LogLevel logLevel, string name, string city);
}
```

生成器创建了如下的 `PlaceOfResidence` 方法

```csharp
[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Extensions.Logging.Generators", "7.0.7.1805")]
public static partial void PlaceOfResidence(this global::Microsoft.Extensions.Logging.ILogger logger, global::Microsoft.Extensions.Logging.LogLevel logLevel, global::System.String name, global::System.String city)
{
    if (logger.IsEnabled(logLevel))
    {
        logger.Log(
            logLevel,
            new global::Microsoft.Extensions.Logging.EventId(23, nameof(PlaceOfResidence)),
            new __PlaceOfResidenceStruct(name, city),
            null,
            __PlaceOfResidenceStruct.Format);
    }
}
```

2、[C# 12 介绍](https://pvs-studio.com/en/blog/posts/csharp/1074/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/5b5e8e3e-bcc5-432c-985d-5fc93d936281)

C# 12 即将发布，跟这篇文章看看有哪些新的内容

1. 主构造函数
2. 不同初始化集合的方法
3. 匿名方法中的默认参数
4. 类型别名
5. `nameof` 方法
6. 内联数组
7. 代码切入

3、[使用 Contains 而不是使用 IndexOf](https://dotnettips.wordpress.com/2023/10/25/microsoft-net-code-analysis-consider-using-string-contains-instead-of-string-indexof/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/30c89386-24db-4ec6-8e8d-15558c2aee2a)

.NET Code Analyzer 另外一个建议是选择 `Contains` 而不是 `IndexOf` 来判断一个字符串是否在另一个字符串中。下面是 Benchmark 的结果，可以看出 `Contains` 的性能比 `IndexOf` 要好。

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/2ee85ec1-5043-456e-8655-7a9d7572eb33)

4、[自定义嵌入资源文件名](https://www.meziantou.net/customizing-the-embedded-resource-name-in-dotnet.htm)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/91b064ec-705c-4612-b218-02fb62a9ca14)

C# 支持将一个资源文件，比如图片，文本，数据当作资源作为程序集的一部分。默认的文件名为 `<assembly>.<namesapce>.<folder>` 作为文件的路径，但是 `C#` 还支持指定资源名或者做同意的替换

```xml
<Project>
  <ItemGroup>
    <EmbeddedResource Include="Resources/**/*">
      <LogicalName>$([System.String]::new('%(RelativeDir)').Replace('\','/'))%(FileName)%(Extension)</LogicalName>
    </EmbeddedResource>
  </ItemGroup>
</Project>
```

这样资源的名字就变成和 `Resource` 目录一致的格式。

5、[Pattern Matching 的新用法](https://www.youtube.com/shorts/Av-fv9EOrnw)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/f7b72ec3-d522-4c8e-9579-c3d3884a5124)

模式匹配是 C# 的最近几个版本加入的功能，除了常见的 `switch` 语句查询，还有一个在 `if` 判断的条件中方式

```csharp
var engineer = new Engineer()
{
    IsManager = false,
    IsSenior = true,
    Age = 30
};


if (engineer is
{
    IsManager: false,
    Age: >18
})
{
    Console.WriteLine("Junior Engineer");
}
```

它用来判断一个工程师对象的 `IsManager = false` 而且 `Age` 大于 18，则输出 `Junior Engineer` 的语句。

6、[.NET 8 中的 TimeProvider 类型](https://andrewlock.net/exploring-the-dotnet-8-preview-avoiding-flaky-tests-with-timeprovider-and-itimer/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/54fdcac0-294b-418c-bede-87ef94b7e288)

千呼万唤始出来，`.NET 8` 终于提供了一个公开的时间接口，这样方便我们进行测试，它是一个叫做 `TimeProvider` 的抽象类，

```csharp
public abstract class TimeProvider
{
    public static TimeProvider System { get; }

    protected TimeProvider();

    public virtual TimeZoneInfo LocalTimeZone { get; }
    public virtual long TimestampFrequency { get; }

    public DateTimeOffset GetLocalNow();
    public virtual DateTimeOffset GetUtcNow();
    public virtual long GetTimestamp();
    public TimeSpan GetElapsedTime(long startingTimestamp);
    public TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp);

    public virtual ITimer CreateTimer(TimerCallback callback, object? state,TimeSpan dueTime, TimeSpan period);
}
```

其中 `GetLocalNow()` 和  `GetUtcNow` 用来返回相应的时间。

7、[C# 性能比较的正确方式](https://sergeyteplyakov.github.io/Blog/benchmarking/2023/11/02/Performance_Comparison_For_Classes_vs_Structs.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/7bb1c554-6c28-4568-bf8f-7bde031d1b69)

我们都知道使用 benchmark 来测算不同方式，数据类型的性能。但是在针对获取的数据时候，需要记住以下几点

1. 不要信任你不能解释的东西，尤其是微观上的benchmark
2. 理解你测量的东西，不要着急下结论
3. 看到表面之下的东西，在性能分析之下，考虑更多抽象之下的内容
4. 要非常小心 Linq 的细节

8、[Visual Studio 中配置私有字段命名规则](https://ardalis.com/configure-visual-studio-to-name-private-fields-with-underscore/#sq_het4wkszkg)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/05f72f5c-882d-4aba-a4a0-1739c6a68b00)

在 C# 中有一些明明规则，比如类和方法选择驼峰命名法，类中私有字段要求有下划线作为前缀，等等。所以在 `Visual Studio` 中配置相应的规则，可以帮助我们找到不符合规则的命名方式。


9、[Func, Predict 和 Expression 的区别](https://www.youtube.com/watch?v=PoniDOq5zQw&ab_channel=MilanJovanovi%C4%87)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a9b5c5b8-a380-46d5-9d88-33c6a3a5e335)

C# 中存在三种判断类型的委托，分别为

- `Func<T, bool>`
- `Predicate<T>`
- `Expression<Func<T, bool>>`

三者的区别可以用这一段代码表示

```csharp
Func<User, bool> userFunc = user => user.Age > 29;

Predicate<User> userPredicate = user => user.Age > 29;

Expression<Func<User, bool>> userEf = user => user.Age > 29;

User user = new User {  Age = 29 };

if (userFunc(user)
    || userPredicate(user)
    || userEf.Compile()(user))
{

}
```

10、[Span](https://blog.ndepend.com/improve-c-code-performance-with-spant/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/b1943a70-ae72-4463-9a23-78d2bd83e0f0)


我们都知道 `Span` 类型能够提高 C# 代码运行的性能，那么它是究竟怎么做到的呢？这边文章给出了更加详细的解释。
