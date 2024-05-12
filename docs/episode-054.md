# .NET 每周分享第 53 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/53c2ddc2-5170-462f-b466-fb73f4298085)

Hack News 上关于 `Powershell` 的[讨论](https://news.ycombinator.com/item?id=40204256)，每个人都有自己的看法和偏好。

## 文章推荐

1、[和 Stephen Toub 学习 Linq](https://www.youtube.com/watch?v=xKr96nIyCFM&ab_channel=dotnet)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/2645d86f-c451-42b3-85ef-758935fbd6e3)

`LINQ`  是 `C#` 中非常要中的功能，其中 `IEnunerable<T>` 是重要的接口。我们都知道 `C#` 编译器为其中做了很多工作，这里 `.NET` 社区著名开发者 `Stephen Toub` 展示了徒手实现编译器完成的这个工作。

2、[使用 Primary 构造函数重构 C# 代码](https://devblogs.microsoft.com/dotnet/csharp-primary-constructors-refactoring/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/7100cdb7-a289-41d9-8cce-f67732b4d3de)

C# 12引入了 `Primary Constructor` 这个新的语法糖，这篇文章介绍了这个语法糖的使用

1. 它是从 `Record`  类型启发而来
2. 它会创建同名的的成员变量
3. 如果使用使用 `readonly` 修饰成员变量，那么需要显示写出
4. 如果有多个构造函数，那么其他的构造函数必须使用 `this` 调用 `primary` 构造函数

3、[IO操作没有线程](https://blog.stephencleary.com/2013/11/there-is-no-thread.html?ref=codetraveler.io)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c8991779-3edd-40a4-b2ea-064bbed34616)

在 `C#` 异步代码中，常常有人会有这样的一个疑问，是不是有一个线程在等待异步的完成，比如

```csharp
private async void Button_Click(object sender, RoutedEventArgs e)
{
  byte[] data = ...
  await myDevice.WriteAsync(data, 0, data.Length);
}
```

在 `WriteAsync` 方法中，是不是有一个线程在调用这个方法。答案是否定的。没有任何一个线程调用这个方法，这个要从 CPU 的硬件底层来讲，当硬件开始一个 `I/O` 操作的时候，CPU 会继续处理其他事情，当 `I/O` 操作完成之后，会给 `CPU` 发出一个中断的信号，这时候内容的应用程序会将后续的操作作为一个 `continuation` 塞入某个线程的队列中。

4、[ThreadPool](https://learn.microsoft.com/en-us/dotnet/standard/threading/the-managed-thread-pool?WT.mc_id=dotnet-0000-bramin&ref=codetraveler.io)

ThreadPool 是 `.NET` 异步并发编程中的重要的概念，这个官方文章详细介绍这个概念。首先 `ThreadPool` 是 `backgroud` 线程，而且通过 `pool` 的方式，在完成任务后，线程还会放回 pool 中。
而且 `ThreadPool` 中的线程的数量是由 [ThreadPool.GetMaxThreads](https://learn.microsoft.com/en-us/dotnet/api/system.threading.threadpool.getmaxthreads) 决定的，任务的数量是由内存大小决定。使用 `ThreadPool` 也非常简单，只需要调用 `Task` 或者 `Task<Result>` 相关方法就能将一个任务塞给线程池。

5、[.NET 8 中的 ConfigureAwait](https://blog.stephencleary.com/2023/11/configureawait-in-net-8.html?ref=codetraveler.io)

在 `.NET` 异步中， `ConfigureAwait` 方法通常用来只配置异步方法后续调用过程中的执行情况，默认为 `true`, 那么它会捕获当前线程的 `Context`，然后在异步操作完成后，在该线程上执行后续操作，反之则使用任何一个线程来处理后续的 `continuation·。
在`.NET 8` 中增加了新的参数，它是一个枚举类型

```csharp
public enum ConfigureAwaitOptions
{
    None = 0x0,
    ContinueOnCapturedContext = 0x1,
    SuppressThrowing = 0x2,
    ForceYielding = 0x4,
}
```

- None
相当于 `ConfigureAwait(false)`, 表明不需要任何捕获之前 `Context`

- ContinueOnCapturedContext

相当于 `ConfigureAwait(true)` 后续操作是在之前的 `context` 执行

- SuppressThrowing

它会把所有的异常忽略掉

- ForceYielding

当异步中遇到一个异步操作的时候，如果这个操作已经完成，则不会发生线程回退，但是 `ForceYielding` 会强制交出线程。

6、[ValueTask 和 ValueTask<Result>](https://devblogs.microsoft.com/dotnet/understanding-the-whys-whats-and-whens-of-valuetask/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/769b9435-39f6-4b3d-abfb-3a07fe76312a)

`Task` 和 `Task<TResult>`  C# 异步编程中主要的组成部分，在`.NET 2.0` 之后，又引入了 `ValueTask` 和 `ValueTask<TResult>` 类型，显而易见，它是一个值类型，主要是为了解决那些非异步操作的在内存消耗上的性能问题，当然在使用的时候还有一些注意点。

7、[F# 学习之旅](https://www.youtube.com/watch?v=l6-WurPT5K8&ab_channel=AmplifyingFSharp)

`F#` 社区的 `Scott Wlaschin` 分享了自己 F# 学习之旅。

8、[C# 中的设计模式](https://dev.to/adrianbailador/design-patterns-in-c-n9)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/28052344-35ad-42d8-a37b-b4e298c5bc07)

这篇文章详细介绍了使用 `C#` 实现常见的设计模式。

9、[使用集合表达式重构代码](https://devblogs.microsoft.com/dotnet/refactor-your-code-with-collection-expressions/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/019c6a61-4274-45ae-a77b-8d5db97154b3)

C# 12 中引入集合表达式，它可以辅助我们写出更加简洁的代码。

- 初始化

```csharp
var numbers1 = new int[3] { 1, 2, 3 };
var numbers2 = new int[] { 1, 2, 3 };
var numbers3 = new[] { 1, 2, 3 };
int[] numbers4 = { 1, 2, 3 };
```

注意最后一个不能使用 `var` 变量。

- 空集合

```csharp
int[] emptyCollection = [];
```

通过这种方式，编译器可以生成最好的的空集合表达方式。

- 展开

可以使用 `..` 命令，将多个集合凭借起来

```csharp
int[] onetwothree = [1, 2, 3];
int[] fourfivesix = [4, 5, 6];

int[] all = [.. fourfivesix, 100, .. onetwothree];
```

这里可以优化我们的代码，比如 `ToList` 

```csharp
static List<Query> QueryStringToList(string queryString) => [.. from queryPart in queryString.Split('&')
                                                              let keyValue = queryPart.Split('=')
                                                              where keyValue.Length is 2
                                                              select new Query(keyValue[0], keyValue[1])];
```

- 性能区别

下面两种集合初始化的方法有什么区别？

```csharp
List<int> someList1 = new() { 1, 2, 3 };
List<int> someList2 = [1, 2, 3];
```

如果我们查看编译器生成的代码，可以发现其中的不同

```csharp
List<int> list = new List<int>();
 list.Add(1);
list.Add(2);
list.Add(3);
List<int> list2 = list;
List<int> list3 = new List<int>();

CollectionsMarshal.SetCount(list3, 3);
 Span<int> span = CollectionsMarshal.AsSpan(list3);
 int num = 0;
span[num] = 1;
num++;
 span[num] = 2;
num++;
span[num] = 3;
num++;
List<int> list4 = list3;
```

第一种方式首先创建了一个 `List` 对象，然后将元素依次加入其中；第二种方式是创建 `List` 对象，然后修改内部集合数据大小，然后在相应的位置修改元素值。当数据量大的时候，第二种方式性能更加好。

## 开源项目

1、[roslynpad](https://github.com/roslynpad/roslynpad)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/d5c360ba-d48b-495a-8921-e224bf94d2e2)

使用 Roslyn 和 AvalonEdit 开发的开源 C# 编辑器，可以简单替换 `LinqPad`。

2、[.NET Benchmark 集锦](https://dotnetbenchmarks.com/)

`.NET` 在每个版本中都强调了在性能上的优化，相同结果的不同代码在性能上也有不同的表现。这个网站收集了很多社区提交的性能比较的案例，大家可以在浏览，借鉴和学习，当然也可以通过提交自己的 Benchmark 的例子丰富这个社区。
