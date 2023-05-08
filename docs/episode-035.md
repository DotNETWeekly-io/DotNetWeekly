# .NET 每周分享第 35 期

## 卷首语

![image](https://dotnetweeklyimages.blob.core.windows.net/035/javacsharp.png)

从诞生的第一天起， `C#` 就被认为是 `Java` 的模仿者。从历史来看，我们不能否认这一个观点。现在，每个编程语言都借鉴了其他的编程语言，那么从 `.NET Core` 之后，`C#` 又从 `Java` 中借鉴了什么呢？主要包含三个

1. Record 类型

Record 类型是用来表示纯数据的结构，`C#` 编译器能够将它转换成一个 `class` 类型，并且重写了 `Equals` 方法。

2. 字面字符串

在 `C#` 中，我们使用双引号 `"` 来表示一个字符串，但是对于一些复杂的字符串，比如换行，特殊字符，我们需要通过转义的方式来表示。这样带来了很多使用的不便。`C#` 引入的字面字符串的方式

```csharp
var text = """
   {
       "name": "fung kao"
   }
""";
```

3. 枚举类型的箭头表达

对于枚举类型，我们一般采用 `swtich` 的方式，比如

```csharp
enum Role
{
     User,
     Admin
}

var role = Role.User;

switch(role)
{
      case Role.User:
            Console.WriteLine("User");
            break;
      case Role.Admin:
            Console.WriteLine("Admin");
            break;
      default:
            Console.WriteLine("Unknown");
            break;
}
```

这是这个常见的用法，C# 引入了模式匹配的方式，我们可以这样使用

```csharp
var text = role swtich
{
Role.User => "User",
Role.Admin => "Admin"
_ => "unknown"
}
```

## 行业资讯

1、[.NET 虚拟大会](https://devblogs.microsoft.com/dotnet/lets-learn-dotnet-anywhere-in-the-world/)

![image](https://dotnetweeklyimages.blob.core.windows.net/035/virutalevent.png)

通常而言，英语是 `.NET` 社区常用的沟通语言。但是最近 `.NET` 社区举办了全球的虚拟大会，而且在不同的时区和不同语言，值得大家参加。

## 文章推荐

1、[.NET 的 Timer](https://www.meziantou.net/too-many-timers-in-dotnet.htm)

![image](https://dotnetweeklyimages.blob.core.windows.net/035/timer.png)

C# 中包含了很多 `Timer` 类，一般都用来进行一些定时的操作。主要分为两大类：

1. UI 定时类

- `System.Windows.Forms.Timer`
- `System.Windows.Threading.DispatcherTimer`
  它们都是 UI 线程执行回调，所以它们能够更新 UI 上的元素，而且每次只执行一次，因此不用担心线程安全的问题

2. 通用定时器

- `System.Threading.Timer`
- `System.Threading.PeriodicTimer`
- `System.Timers.Timer`

其中 `System.Threading.Timer` 是最基础的定时器，它将回调委托给线程池执行。但是如果回调执行的时间超过定时器的间隔，那么会发生同时执行多次的情况。

```csharp
var timer = new System.Threading.Timer(
    callback: state => Console.WriteLine("tick"), // callback can be executed in parallel
                                                  // if the previous one is not completed
                                                  // before the next tick
    state: null, // Can be used to pass data to the callback (to avoid using a closure)
    dueTime: TimeSpan.Zero,           // Start the timer immediately
    period: TimeSpan.FromSeconds(1)); // Tick every second

// Pause the timer
timer.Change(dueTime: Timeout.Infinite, period: Timeout.Infinite);
```

`System.Timer.Timer` 是对 `System.Threading.Timer` 的封装，而且暴露了其他几个方法，比如 `AutoReset`, `Enabled`，`SynchronizingObject` 等等，而且它还支持多个回调的调用。

```csharp
var timer = new System.Timers.Timer(TimeSpan.FromSeconds(1));

// Support multiple handlers
timer.Elapsed += (sender, e) => Console.WriteLine("Handler 1");
timer.Elapsed += (sender, e) => Console.WriteLine("Handler 2");

// Support customizing the way the callback is executed (on the ThreadPool if not set)
timerComponent.SynchronizingObject = ...;

// Stop the timer after the first tick
timerComponent.AutoReset = false;

// Start the timer
timer.Start();
```

`System.Threading.PeriodicTimer` 是最近加入的类型，它的主要功能是允许定时器在循环中使用，而且执行异步。

```csharp
using var cts = new CancellationTokenSource();
using var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));

// Simple usage, no concurrent callbacks, supports async _handlers_
while (await periodicTimer.WaitForNextTickAsync(cts.Token))
{
    Console.WriteLine("Tick");
    await AsyncOperation();
}
```

通过 `while` 循环判断，避免了回调的并行执行。

2、[.NET Standard 介绍](https://andrewlock.net/understanding-the-dotnet-ecosystem-the-introduction-of-dotnet-standard/)

![image](https://dotnetweeklyimages.blob.core.windows.net/035/netstandard.png)

`.NET Standard` 作为一个短暂的技术名词，在 `.NET` 历史中存在过一段时间。主要目的是解决 `.NET` 各个平台的之间代码的复用性，比如 `.NET Framework`, `Mono` 或者 `.NET Core`。它是一组 `API` 定义的集合，而各个平台包含这些 `API` 的实现。

3、[String 和对象互转换](https://csharp.christiannagel.com/2023/04/14/iparsable/)

![image](https://dotnetweeklyimages.blob.core.windows.net/035/stringobject.png)

将一个对象实例和字符串之间相互转换是很常见的要求，在 `C#` 中定义了接口来进行这些转换。

1. 对象实例转换字符串

- IFormattable
- ISpanFormattable

只要对象实现了其中的接口，那么可以将对象实例按照要求转换成字符串类型

2. 字符串转换成对象

- IParsable
- ISpanParsable

`.NET 7` 为接口类型提供了静态方法，所以在 `.NET 7` 之前，将一个字符串转换成对象的话，通常是这么处理的

```csharp
class Person  {
   public string FirstName { get; }
   public string FullName { get; }
   public string Country { get; }

   public Person(string firstName, string fullName, string country) {
      FirstName = firstName;
      FullName = fullName;
      Country = country;
   }
}

static class ExtensionMethods {
   internal static Person  Parse(this string s) {
      string[] strings = s.Split(new[] { ',', ';' });
      if(strings.Length != 3) { throw new OverflowException("Expect: FirstName,LastName,Country"); }
      return new Person(strings[0], strings[1], strings[2]);
   }
}
```

通常我们需要编写 `string` 类型的扩展方法，那么在 `.NET 7` 中，我们可以通过集成 `IParsable` 接口完成更优雅的实现方式。

```csharp
sealed class Person : IParsable<Person> {
   public string FirstName { get; }
   public string FullName { get; }
   public string Country { get; }

   // Private constructor used from the Parse() method below
   private Person(string firstName, string fullName, string country) {
      FirstName = firstName;
      FullName = fullName;
      Country = country;
   }

   // IParsable<Person>  implementation
   public static Person Parse(string s, IFormatProvider? provider) {
      string[] strings = s.Split(new[] { ',', ';' });
      if(strings.Length != 3) { throw new OverflowException("Expect: FirstName,LastName,Country"); }
      return new Person(strings[0], strings[1], strings[2]);
   }

   public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Person result) {
      result = null;
      if (s == null) { return false; }
      try {
         result = Parse(s, provider);
         return true;
      } catch { return false; }
   }
}

Person person = "Bill,Gates,US".Parse<Person>();
```

4、[Azure OpenAI 使用](https://devblogs.microsoft.com/dotnet/getting-started-azure-openai-dotnet/)

![image](https://dotnetweeklyimages.blob.core.windows.net/035/openai.png)

作为一名 `.NET` 开发者，该如何上手 `Azure OpenAI` 呢？这篇官方博客给出了建议。

5、ASP.NET Core 中间件处理流程

![image](https://dotnetweeklyimages.blob.core.windows.net/035/aspnetcore.png)

6、[WASM 介绍](https://speakerdeck.com/christianweyer/wasm-wasi-wtf-webassembly-101-for-net-developers)

![image](https://dotnetweeklyimages.blob.core.windows.net/035/wasm.png)

WASM 是新的技术，`.NET` 也不能落后，这个幻灯片介绍了 `WASM` 和 `C#` 入门知识。

7、[Mutation Test](https://blog.jetbrains.com/dotnet/2023/05/05/stefan-polz-how-to-test-csharp-unit-tests-with-mutation-testing-webinar-recording/)

Mutation Test 是用来检测单元测试的质量，这个视频详细介绍这个概念。

## 开源项目

1、[Powershell 中的 chatgpt](https://dfinke.github.io/powershellai,%20powershell,%20chatgpt/2023/04/04/PowerShellAI-ChatGPT-Conversation-Mode.html)

![image](https://dotnetweeklyimages.blob.core.windows.net/035/ai.png)

`ChatGPT` 火了，各种针对 `OpenAI` 的 API 开发的 AI 应用程序也数不胜数。 `PowerShellAI` 是一个开源的 PowerShell 库，使用它我们可以在 `Powershell` 中使用我们的人工智能助手。

2、.NET 知名开源库

![image](https://dotnetweeklyimages.blob.core.windows.net/035/package.png)
