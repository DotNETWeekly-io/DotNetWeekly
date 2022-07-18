# .NET 每周分享第 17 期

## 卷首语

可枚举类型的选择

![image](https://dotnetweeklyimages.blob.core.windows.net/017/ienumerable.png)

C# 基础库中包含了很多集合，比如 `List`, `Array` 或者 `HashSet`， 它们都实现了 `IEnumerable<T>` 接口。这样就会给编码的时候代码带来困惑，究竟使用哪种方式来实现呢？接下来分三种情况来考虑：

1. 集合作为方法的参数

有个编程原则：

> 对输入宽容

所以最好参数是 `IEnumerable<T>` 类型。下面的两个方法签名

```csharp
void ProcessOrders1(List<Order> orders)
{
    // ...
}
void ProcessOrders2(IEnumerable<Order> orders)
{
    // ...
}
```

这样 `ProcessOrders1` 的方法调用方需要显式 `ToList()` 方法，而 `ProcessOrders2` 方法调用者就没有这样的烦恼。但是如果需要在方法中需要多次迭代集合，那么 `IEnumerable<T>` 就不在适用的，比如：

```csharp
// avoid: multiple enumeration
void ProcessOrders(IEnumerable<Order> orders)
{
    Console.WriteLine($"Processing {orders.Count()} orders");
    var allValid = true;
    foreach(var order in orders)
    {
        if (!IsValid(order))
        {
            Console.WriteLine($"Order {orders.Id} is invalid");
            allValid = false;
        }
    }
    if (allValid)
    {
        foreach(var order in orders)
        {
            PrintShippingNote(order);
        }
    }
}
```

所以在这边需要将 `IEnumerable<T>` 转换成明确的内存的集合，比如 `IReadOnlyCollection<T>` 。
总结如下：

- 尽可能地使用 `IEnumerable<T>` 类型
- 避免接受具体地集合类型
- 避免修改传入的集合
- 如果需要多次迭代集合， 考虑选择 `IReadOnlyCollection<T>` 类型。

2. 集合作为方法的返回值

```csharp
IEnumerable<Order> GetOrders(DateTime orderDate)
{
    //
}
```

选择返回 `IEnumerable<T>` 类型看上去不错，但是调用者不知道下面具体的类型是那种。比如是否可以多次迭代，是否每次迭代是一种耗时操作。通常情况下，在获取集合之后会调用 `.ToList` 方法。推荐方法是返回 `IReadOnlyCollection<T>` 类型。

```csharp
private List<string> validFileExtensions = ...;
// ensure that callers aren't able to modify this list
public IReadOnlyCollection<string> GetValidFileExtensions()
{
    return validFileExtensions;
}
```

总结如下

- 考虑是使用 `IReadOnlyCollection<T>` 如果需要是一个内存集合，这样调用者可以从中获益
- 使用 `IEnumerable<T>` 类型如果只需要进行迭代或者需要实时创建新的类型序列

3. DTO 中的集合

如果在 `DTO` (Data Transfer Objects) 中使用了集合，有下面这些选择

```csharp
class Example1
{
    public IEnumerable<string> People { get; set; }
}

class Example2
{
    public IReadOnlyCollection<string> People { get; set; }
}

class Example3
{
    public ICollection<string> People { get; set; }
}

class Example4
{
    public IList<string> People { get; set; }
}
```

`Newtonsoft.Json` 库都支持上面方案的序列化和反序列化。实际上除了第二种，剩下的都反序列化成一个 `List<string>` 类型。但是更加倾向于选择 `Example2` 类型，因为它是一个不可变类型，并且提供了 `Count` 属性。

## 文章推荐

1、[.NET 类型的内存分配](https://www.youtube.com/watch?v=jONSIhMST9E&ab_channel=NickChapsas)

![image](https://dotnetweeklyimages.blob.core.windows.net/017/memory.png)

在 `C#` 中有两个主要类型，值类型和引用类型。一般我们认为值类型分配在栈上，而引用类型分配在堆上。事实果真如此吗？答案是一半正确，引用类型分配在堆上，而值类型会根据下面情况分别处理：

1. 声明在方法中的值类型 => 栈上
2. 声明在方法参数中的值类型 => 栈上
3. 类的的成员变量的值类型 => 堆上
4. 结构体中的值类型 => 跟结构体分配一致
5. ref 修饰的结构体 => 永远栈上

2、[.NET 101](https://dusted.codes/dotnet-basics)

![my-dotnet-bot-mod](https://dotnetweeklyimages.blob.core.windows.net/017/dotnet101.png)

这篇综述的文章，完整地介绍了 `.NET` 过去和现在，对于新手来说可以很方便地全局了解什么是 `.NET`。

3、[.NET 字符串操作技巧汇总](https://www.youtube.com/watch?v=ioi__WRETk4&ab_channel=IAmTimCorey)

![image](https://dotnetweeklyimages.blob.core.windows.net/017/string.png)

字符串操作几乎是每个应用程序都要关注的内容，这个视频详细介绍了 `C#` 中如何使用操纵字符串。

4、[.NET 中的正则表达式](https://code-maze.com/regular-expressions-csharp/)

![image](https://dotnetweeklyimages.blob.core.windows.net/017/regex.png)

正则表达式是字符串处理的利器，在 `C#` 中 `Regex` 类包含了正则表达式处理的全部功能，这篇文章介绍了如何使用它们，包括

- 匹配
- 计数
- 提取
- 替换

5、[MAUI 学习资源汇总](https://devblogs.microsoft.com/dotnet/learn-dotnet-maui/)

MAUI 学习的官方推荐资料。

6、[C# 中在一个集合中去重的方法](https://code-maze.com/remove-duplicates-from-a-list-csharp/)

在 `C#` 集合中，如何进行去重操作呢？这篇文章介绍了一系列方法。

1. `Distinct` 方法： `listWithDuplicates.Distinct().ToList()`
2. `GroupBy` 和 `Select` 方法： `listWithDuplicates.GroupBy(x => x).Select(d => d.First()).ToList()`
3. `Union` 方法： `listWithDuplicates.Union(listWithDuplicates).ToList()`
4. 使用 `HashSet` 集合：`listWithDuplicates.ToHashSet().ToList()`
5. 使用 `Dictionary`:

```csharp
var dict = new Dictionary<T, int>();
foreach (var s in listWithDuplicates)
{
     dict.TryAdd(s, 1);
}

var distinctList = dict.Keys.ToList();
```

6. 使用 `List`

```csharp
var listWithoutDuplicates = new List<T>();
foreach (T item in listWithDuplicates)
{
    if (!listWithoutDuplicates.Contains(item)) //we can also use !listWithoutDuplicates.Any(x => x.Equals(item))
    {
        listWithoutDuplicates.Add(item);
    }
}
```

7. 迭代列表

```csharp
var n = listWithDuplicates.Count;

for (int i = 0; i < n; i++)
{
    for (int j = i + 1; j < n; j++)
    {
        if (listWithDuplicates.ElementAt(i).Equals(listWithDuplicates.ElementAt(j)))
        {
            for (int k = j; k < n - 1; k++)
            {
                T item = listWithDuplicates.ElementAt(k);
                item = listWithDuplicates.ElementAt(k + 1);
            }
            j--;
            n--;
        }
    }
}
var distinctList = listWithDuplicates.Take(n).ToList();
```

8. 排序

```csharp
var listWithoutDuplicates = new List<T>();
listWithDuplicates = listWithDuplicates.OrderBy(x => x).ToList();

T element = default;

foreach (T result in listWithDuplicates)
{
    if (!result.Equals(element))
    {
        listWithoutDuplicates.Add(result);
        element = result;
    }
}
```

7、[C# Lambda 表达式](https://code-maze.com/lambda-expressions-in-csharp/)

![image](https://dotnetweeklyimages.blob.core.windows.net/017/linq.png)

`Lambda` 表达式式 `C#` 的一个重要的功能，主要会用在如下的情况中

1. Linq
2. Minimal API
3. 事件

这篇文章概述的 `Lambda` 表达式的内容。

## 开源项目

1、[.NET interactive 的 VS Code 插件](https://github.com/dotnet/interactive)

![image](https://dotnetweeklyimages.blob.core.windows.net/017/vscode.png)

Visual Studio Code 是广泛使用的编辑器，其丰富的插件使得生态非常繁荣。`.NET` 的 `interactive` 项目提供了这样的插件，可以在 `VS Code` 中使用 `notebook` 编写 `.NET` 的代码，还支持其他语言，比如 `PowerShell`, `Javascript` 等等。

2、[Java 运行在 .NET 平台上](https://github.com/ikvm-revived/ikvm)

![image](https://dotnetweeklyimages.blob.core.windows.net/017/hava.png)

`IKVM`是一个有意思的开源项目，它可以将 `Java` 编写的 `Jar` 包转换成 `dll`, 并且可以在普通的 `C#` 应用程序中引用这个库。通常它要求 `Jar` 包使用的是 `JDK` 包含的标准库，否则出现错误的概率会非常大。

3、[Ant Design Blazor](https://github.com/ant-design-blazor/ant-design-blazor)

![image](https://dotnetweeklyimages.blob.core.windows.net/017/antdesign.png)

Ant Design Blazor 是基于 `Ant Design` 和 `Blazor` 的 UI 应用框架，支持 `.NET 3.1` 以上版本，并且各种主流浏览器。

4、[roslynpad](https://github.com/roslynpad/roslynpad)

通过 Avalonia UI 支持 Win/Mac/Linux，支持.csx——基于 C# 的脚本语言. 支持下载源代码，自行修改编译。
功能对比 LinqPad 高级版

![image](https://dotnetweeklyimages.blob.core.windows.net/017/roslynpad.png)
