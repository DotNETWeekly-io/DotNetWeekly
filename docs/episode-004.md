# .NET 每周分享第 4 期

## 开卷语

欢迎来到 2022 年！

```Csharp
global using System;
Console.WriteLine("Hello .NET 2022!");
```

## 文章推荐

1、[using 语句的使用](https://www.youtube.com/watch?v=iqt7bqAm27U&ab_channel=NickChapsas)

![](https://dotnetweeklyimages.blob.core.windows.net/004/using.png)

我们都知道使用 `using(var obj = new SomeClass())` 语句可以帮助我们安全的释放需要的内存，从本质上来讲，这是一个编译器的语法糖，上面的语句转换为如下

```Csharp
var obj = new SomeClass();
try
{
    //...
}
finally
{
   obj.Dispose();
}
```

借助这个特性， 我们可在非业务性代码中使用它们，比如说日志。

```Csharp
class LogHandler: IDisposable
{
      private ILogger _logger;
      private StopWatcher _sw;
      public LogHandler(ILogger logger) {
         _logger = logger;
        _sw = StopWatcher.NewStart();
      }

     public void Dispose() {
          _sw.Stop();
         _logger.Log($"Operation has completed in {_sw.ElapsedMilesecond} miliesecond");
    }
}
```

2、[.NET 6 中增加的 API](https://blog.okyrylchuk.dev/20-new-apis-in-net-6)

`.NET 6` 不仅仅带来了性能上提高，还增加了一些 API 以便更加方便使用它们。这篇文章介绍了这些新的 API，如果它正是你需要的，试着去使用它们。

- DateOnly and TimeOnly
- Paralle.ForEachAsync
- ArgumentNullException.ThrowIfNull()
- PriorityQueue
- Reading and Writing Files
- Periodic Timer
- Metrics API
- Reflection API
- Process Path and ID
- Configuration Helper
- CSPNG
- Native Memory API
- WaitAsync
- Math APIs
- ...

3、[readonly 和 const 的区别](https://medium.com/@serhat21zor/c-readonly-vs-const-43a1799fd07d)

![](https://dotnetweeklyimages.blob.core.windows.net/004/const-readonly.png)

`readonly` 和 `const` 广泛使用在 `C#` 代码中，它们的主要目的是降低程序中的可变性。它们的主要区别如下

**readonly**

- 只能在变量的初始化行和构造函数中赋值。
- 可以在构造函数中多次赋值。
- 用 `static` 修饰的 `readonly` 变量只能在 `static` 修饰的构造函数中初始化。

**const**

- 只能在初始化行赋值。
- 不能在构造函数中覆盖。
- 赋值只能是在编译时候已知。
- 不能使用引用类型 (reference type)。
- `static const` 是没有意义的。

注意编译器会修改所有的 `const` 变量，替换为真实的值。所以如果引用了第三方库的 `const` 变量，如果第三方库升级后， 应用程序没有重新编译的话，会导致意想不到的错误。

假设现在有一个库提供了一个 `const` 变量

```Csharp
public class MyLib
{
    public const int StatusCode = 200;
}
```

我们的应用程序使用了这个库中 `StatusCode` 这个变量

```Csharp
Console.WriteLine(MyLib.StatusCode); // 200
```

如果 `MyLib` 修改了 `StatusCode = 304` 变量的字面值，我们的应用程序没有重新编译最新的版本，那么输出的结果仍然是 `200`。

4、[WebAPI 中正确处理异常](https://codeopinion.com/problem-details-for-better-rest-http-api-errors/)

开发 `WebAPI` 中除了要正确地返回需要地结果，还需要对异常情况进行处理。通常会有两种处理方式：

1.  返回非 2xx 的状态码，然后在 body 中填写响应的错误信息；

![](https://dotnetweeklyimages.blob.core.windows.net/004/non200.png)

2. 返回 200 的状态码，然后在 Body 中填写响应的错误信息

![](https://dotnetweeklyimages.blob.core.windows.net/004/status-200.png)

两者有本质上的不同，第一种叫做 `error.info`, 而第二种叫做 `message`。 其实标准的做法叫做 **Problem Details** (RFC7807)

![](https://dotnetweeklyimages.blob.core.windows.net/004/error-body.png)

这里的字段都是有特定的意义。

## 开源项目

1、[2021 最活跃的 .NET 项目](https://twitter.com/sbwalker/status/1476976431972462601)

![](https://dotnetweeklyimages.blob.core.windows.net/004/dotnet-oss.png)

这里是 2021 年最活跃的 `.NET` 开源项目列表，主要 `Pull Request`， `Commit` 和 `New Contributor` 三个指标统计。注意这里并不包含由 Microsoft 维护或者支持的项目。
