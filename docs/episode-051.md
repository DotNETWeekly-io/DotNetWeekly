# .NET 每周分享第 51 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/56914e24-b8be-4706-aa6c-91ea478a0724)

`2024` 年是一个闰年，也就是存在 2 月 29 日。在 C# 中的时间中，有一个特殊的处理情况，

```csharp
var testDate = new DateTime(2024, 2, 10);  
var var0 = testDate.AddDays(19); 
var var1 = testDate.AddDays(19).AddYears(2);
```

由于 `2024` 年是闰年，所以 `var0` 是 `2024-02-29` 日，但是 2026 年不是闰年，所以 `var1` 的值为  `2026-02-28`， 这是 C# 对 闰年的特殊处理。在 C# 的时间处理库中是这么处理的：

```csharp
public DateTime AddYears(int value)
{
  // ... some validation code
  uint n = DaysToYear((uint)y);

  int m = month - 1, d = day - 1;
  if (IsLeapYear(y))
  {
    n += DaysToMonth366[m];
  }
  else
  {
    if (d == 28 && m == 1) d--; // <-- THIS RIGHT HERE
    n += DaysToMonth365[m];
  }
  // ... return new date
}
```

所以我们在处理这种问题的时候，可以这么处理:

- 如果天数重要，先添加年，然后添加天
- 如果真实日期重要，先添加天，然后添加年

## 行业资讯

1、[WinForms 在 64 位应用程序的策略](https://devblogs.microsoft.com/dotnet/winforms-designer-64-bit-path-forward/)

随着 64 位的 Visual Studio 到来， WinForms 开发也伴随着挑， 比如

1. 32 位遗留组件
2. 进程外设计器
3. 遗留组件支持

## 文章推荐

1、[dotnet profiler tools](https://dev.to/mohammadkarimi/cross-platform-diagnostic-tools-for-net-applications-2366)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6f977ced-49fd-43e5-8026-7fa3ca140315)

对于应用程序而言，profiler 是非常重要的，这样可以帮助我们找到应用程序的瓶颈，然后提高他们。在 `.NET` 生态中提供了三种跨平台的工具:

1. dotnet-counter
2. dotnet-trace
3. dotnet-dump

2、[异步任务集合抛出所有异常](https://www.meziantou.net/getting-all-exceptions-thrown-from-parallel-foreachasync.htm)

在 `Parallel.ForEachAsync` 或者 `Task.WhenAll`  这样的方法中，如果有多个异常抛出，在 `catch` 语句中只会抛出第一个异常，那么改如果那抛出各个线程的异常呢？可以用到 `ConfigureAwaitOptions.SuppressThrowing` 参数

```csharp
public static async Task WithAggregateException(this Task task)
{
 await task.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
 task.Wait();
}

try
{
 var tasks = Enumerable.Range(0, 10).Select(i => WorkAsync(i));
 await Task.WhenAll(tasks).WithAggregateException();
}
catch (Exception e)
{
 Console.WriteLine(e.ToString());
}
```

这里会抛出所有执行的异常。

3、[避免多个 boolean 型参数](https://steven-giesel.com/blogPost/9994b00c-8bc2-4794-ae74-80e6ee4cd5e5/avoid-multiple-boolean-parameters)

如何你看到这样的一个方法， `void Refresh(bool force, bool lazy);`， 你会觉得有多少种调用方法？由于 `boolean` 型有两种情况 `true` 或者 `false` ，那么这种方法就会有 4 （2^2）种，这样增加了使用者的心里负担。
那么该怎么解决这个问题呢？可以添加多个方法

```csharp
void Refresh();
void RefreshForce();
void RefreshLazy();
```

或者使用枚举类型，列举出所有可能情况

```csharp
enum RefreshMode { Default, Force, Lazy }
void Refresh(RefreshMode mode);
```

4、[EntityFrame Core 记录执行 SQL 语句](https://dev.to/karenpayneoregon/ef-core-log-to-file-benefits-13jp)

在 `EntityFramework Core` 应用程序中，如果想要记录所有生成 `SQL` 的语句，可以将它们写入一个文本文件中。

1. 配置文件

```xml
<Target Name="MakeLogDir" AfterTargets="Build">
   <MakeDir Directories="$(OutDir)LogFiles\$([System.DateTime]::Now.ToString(yyyy-MM-dd))" />
</Target>
```

2. 配置写文件

```csharp
public class DbContextToFileLogger
{
    private readonly string _fileName = 
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
            "LogFiles", $"{Now.Year}-{Now.Month:D2}-{Now.Day:D2}", 
            "EF_Log.txt");

    public DbContextToFileLogger()
    {
        if (!File.Exists(_fileName))
        {
            using (StreamWriter w = File.AppendText(_fileName)) ;
        }
    }

    [DebuggerStepThrough]
    public void Log(string message)
    {
        StreamWriter streamWriter = new(_fileName, true);
        streamWriter.WriteLine(message);
        streamWriter.WriteLine(new string('-', 40));
        streamWriter.Flush();
        streamWriter.Close();
    }
}
```

3. 配置 DBContextOptions

```csharp
 optionsBuilder.UseSqlServer(ConnectionString())
            .EnableSensitiveDataLogging()
            .LogTo(new DbContextToFileLogger().Log,
                new[] { DbLoggerCategory.Database.Command.Name }, 
                Microsoft.Extensions.Logging.LogLevel.Information);
```

5、[Blazor Fluent UI 控件故事](https://devblogs.microsoft.com/dotnet/the-fast-and-the-fluent-a-blazor-story/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/12df9ced-ddb9-4a53-b0f0-aa626fc74cc8)

Fluent 是微软推出的 UI 库，用来统一 Web, 桌面和移动应用程序的界面设计。而 Blazor 是随着 `.NET` 一起推出的前端开发框架，可以使用 `C#` 编写前端交互逻辑。那么 `Blazor` 版的 `Fluent` 的历史是怎样的，这篇博客介绍了这段内容。

6、[Visual Studio 使用 Tips](https://www.youtube.com/watch?v=td81h--afxM&ab_channel=IAmTimCorey)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e6c7e744-3dea-47c3-a65b-fad240f55d3a)

Visual Studio 是 `.NET` 开发者广泛使用的开发工具，那么除了开箱即用之外，还有很多配置可以帮助你提供开发体验。

7、[拼接两个 Array](https://www.youtube.com/watch?v=R5Shxt1dx58&ab_channel=MilanJovanovi%C4%87)

C# 中拼接多个数组的简化表示方式：

```csharp
int[] arr1 = new int[] { 1, 2, 3 };
int[] arr2 = Enumerable.Range(10, 15).ToArray();
int[] arr3 = [..arr1, ..arr2];
```

## 开源项目

1、[SimdLinq](https://github.com/Cysharp/SimdLinq)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6cff3595-92ec-4f44-8e64-24a048f7f1b9)

至从 `.NET 7`  支持 `SIMD` 之后，`Linq` 的性能得到直线上升。但是官方的由于安全的兼容的问题，只支持 `int` 和 `long` 类型的加速，`SimdLinq` 库将它支持的类型增加到更多类型。 从 benchmark 结果来看，效果非常明显。