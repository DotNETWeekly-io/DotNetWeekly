# .NET 每周分享第 26 期

## 卷首语

[`.NET 7` 正式发布](https://devblogs.microsoft.com/dotnet/announcing-dotnet-7/)

![image](https://user-images.githubusercontent.com/11272110/201447306-03790fce-8b1b-42da-a9a5-df99a4b5e7c4.png)

这周 `.NET` 社区最大的新闻就是 `.NET 7` 正式发布，在之前的预览版本中，我们已经看到过无数次的文章分析过 `.NET 7` 在各个方面的提升。官方的文章主要包含了一下几个部分：

- 同一化
- 现代化
- 云原生应用程序
- 简单化
- 性能提升

## 行业资讯

1、[.NET 的 Current 版本名字改为 Standard Term Support](https://github.com/dotnet/core/blob/main/release-policies.md#release-types)

近期微软将 `.NET` 的短期支持版本（Short-Term Support) 改名为标准支持版本（Standard Term Support)，其实质性内容并没有发生任何改变，仍然是 18 个月支持周期。
这和罗永浩之前演讲中提到的一样，将 VIP 收费方案改名为标准收费方案，而原先标准收费方案叫做优惠收费方案，虽然本质上没有任何不同，只不过让别人舒服一点。对 `.NET` 也是同样如此，之前短期（`Short-Term`） 的概念让开发人员觉得这是一个不稳定的版本，可事实上并不是如此，每个 `.NET` 版本出来之后都值得升级。

2、[.NET Conf 2022 发布汇总](https://www.poppastring.com/blog/net-conf-2022-announcements)

![image](https://user-images.githubusercontent.com/11272110/201447474-902536c5-1d21-4531-8914-b259133c328d.png)

除了 `.NET 7` 的发布，这周还有其他内容的发布，这里给出了汇总

- [NET 7](https://devblogs.microsoft.com/dotnet/announcing-dotnet-7/)
- [ASP.NET Core in .NET 7](https://devblogs.microsoft.com/dotnet/announcing-fsharp-7/)
- [.NET MAUI for .NET 7](https://devblogs.microsoft.com/dotnet/dotnet-maui-dotnet-7/)
- [Entity Framework Core 7 (EF7)](https://devblogs.microsoft.com/dotnet/announcing-ef7/)
- [C# 11](https://devblogs.microsoft.com/dotnet/welcome-to-csharp-11/)
- [F# 7](https://devblogs.microsoft.com/dotnet/announcing-fsharp-7/)
- [Visual Studio 17.4 GA](https://devblogs.microsoft.com/visualstudio/visual-studio-2022-17-4/)
- [Arm64 Visual Studio](https://devblogs.microsoft.com/visualstudio/arm64-visual-studio-is-officially-here/)

3、[.NET MAUI 支持 Xcode 14 和 iOS 16](https://devblogs.microsoft.com/dotnet-ch/%e5%ae%98%e5%ae%a3-net-maui-%e6%94%af%e6%8c%81-xcode-14-%e5%92%8c-ios-16/)

![image](https://user-images.githubusercontent.com/11272110/201470554-b500aa55-d536-4ab8-9a21-9e03f753315b.png)

`.NET MAUI` 正式支持 `Xcode 14` 和 `iOS 16`。

## 文章推荐

1、[Visual Studio + GitHub 学习教程](https://devblogs.microsoft.com/visualstudio/learn-github-in-visual-studio-learning-series/)

![image](https://user-images.githubusercontent.com/11272110/200149224-30c82cb0-47a0-4614-af39-bbda64d0439d.png)

调查显示，有 `34%` 的 Visual Studio 的开发人员没有使用版本控制工具，微软近期开放了学习课程，主要包含下面内容

- Git 和 GitHub 基础知识
- 克隆 GitHub 项目
- 在 `Visual Studio` 查看项目历史
- 同步代码到 GitHub
- 标准合作开发流程

2、[.NET 应用程序获取 dump 的方式](https://www.meziantou.net/how-to-generate-a-dump-file-of-a-dotnet-application.htm)

![image](https://user-images.githubusercontent.com/11272110/200149549-6a969fe8-cac5-4187-bdd1-439485282809.png)

Dump 文件是分析应用程序的重要文件格式，那么 `.NET` 应用程序有那些生成 `Dump` 文件的方式呢？

- Windows

  - dotnet-dump 工具
  - Windows 任务管理器
  - SysInternals 软件
  - Debug 诊断工具
  - Visual Studio Debug 工具
  - WinDbg
  - Windows 错误报告

- Linux

  - dotnet-dump 工具
  - SysInternal 工具 Linux 版本

- Azure App Service

  - Diagnotic 选项

3、[using 语句在 Visual Studio 中高阶使用技巧](https://www.meziantou.net/configuring-visual-studio-to-handle-using-directives-automatically.htm)

![image](https://user-images.githubusercontent.com/11272110/200150178-d10be51c-2ec6-430c-9848-60f3328743e4.png)

`Using` 语句是 C# 中用来导入命令空间，在 `Visual Studio` 中对 `using` 语句有一些高级选项

- 在打开文件的时候合并所有 `using` 语句；
- `IntelliSense` 展示未使用的 `using` 命令空间；
- 拷贝代码的使用，自动拷贝 `using` 语句；
- 保存文件的时候，自动删除未使用的 `using` 语句。

4、[VS2022 中的特色功能](https://devblogs.microsoft.com/visualstudio/cool-features-in-visual-studio-2022/)

![image](https://user-images.githubusercontent.com/11272110/200150487-d407a9de-b84f-4e46-88a1-70e4cd5ccd91.png)

`Visual Studio 2022` 是 Visual Studio 历史上架构支持最大的更新，原生支持 64 位软件。除此之外，还有一个比较 Cool 的功能：

1. 窗口布局管理
2. 文档页面管理
3. 颜色主题
4. AI 辅助编码
5. 临时断点
6. 源码查看
7. 添加新文件
8. VSColorOuput 插件

6、[.NET 6 中的随机数生成](https://dev.to/kasuken/generate-random-numbers-with-net-6-1620)

![image](https://user-images.githubusercontent.com/11272110/200151724-47e3e8c9-36e5-436f-8102-bd7bb3f85097.png)

在以前的 `.NET` 版本中，通过下面的代码就可以生成随机数，

```csharp
var random = new Random();
var value = random.Next();
return value;
```

但是他们并不是真正的随机数，因为只要给定了了相同的 `Seed`，就会生成相同的随机数列。如果不给定，就是用系统时钟。所以下面的两个 `Random` 实例可能产生相同的数列

```csharp
Random rand1 = new Random(42);
Random rand2 = new Random(42);
```

`.NET 6` 推出了新的随机数列，有更强的随机性

```csharp
var r = RandomNumberGenerator.Create();
var bytes = new byte[sizeof(int)];
r.GetNonZeroBytes(bytes);
Console.WriteLine($"{BitConverter.ToInt32(bytes)}");
```

7、[异步锁的使用方法](https://dfederm.com/async-mutex/)

我们都知道，`Mutex` 是不能用在异步方法中的，应为 `mutex` 的释放必须要求使用同一个线程。那么我们改怎么顶一个异步`mutex` 呢？

```csharp
public sealed class AsyncMutex : IAsyncDisposable
{
    private readonly string _name;
    private Task? _mutexTask;
    private ManualResetEventSlim? _releaseEvent;
    private CancellationTokenSource? _cancellationTokenSource;

    public AsyncMutex(string name)
    {
        _name = name;
    }

    public Task AcquireAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        TaskCompletionSource taskCompletionSource = new();

        _releaseEvent = new ManualResetEventSlim();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // Putting all mutex manipulation in its own task as it doesn't work in async contexts
        // Note: this task should not throw.
        _mutexTask = Task.Factory.StartNew(
            state =>
            {
                try
                {
                    CancellationToken cancellationToken = _cancellationTokenSource.Token;
                    using var mutex = new Mutex(false, _name);
                    try
                    {
                        // Wait for either the mutex to be acquired, or cancellation
                        if (WaitHandle.WaitAny(new[] { mutex, cancellationToken.WaitHandle }) != 0)
                        {
                            taskCompletionSource.SetCanceled(cancellationToken);
                            return;
                        }
                    }
                    catch (AbandonedMutexException)
                    {
                        // Abandoned by another process, we acquired it.
                    }

                    taskCompletionSource.SetResult();

                    // Wait until the release call
                    _releaseEvent.Wait();

                    mutex.ReleaseMutex();
                }
                catch (OperationCanceledException)
                {
                    taskCompletionSource.TrySetCanceled(cancellationToken);
                }
                catch (Exception ex)
                {
                    taskCompletionSource.TrySetException(ex);
                }
            },
            state: null,
            cancellationToken,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);

        return taskCompletionSource.Task;
    }

    public async Task ReleaseAsync()
    {
        _releaseEvent?.Set();

        if (_mutexTask != null)
        {
            await _mutexTask;
        }
    }

    public async ValueTask DisposeAsync()
    {
        // Ensure the mutex task stops waiting for any acquire
        _cancellationTokenSource?.Cancel();

        // Ensure the mutex is released
        await ReleaseAsync();

        _releaseEvent?.Dispose();
        _cancellationTokenSource?.Dispose();
    }
}
```

## 开源项目

1、[Respwan](https://github.com/jbogard/Respawn)

![image](https://user-images.githubusercontent.com/11272110/201470154-47c95ee5-9367-4339-a12e-da1bc12b2794.png)

`Respwan` 是测试中的辅助工具库，它可以恢复每个测试过程中对数据的修改。

2、[内嵌 NoSQL 数据库](https://github.com/mbdavid/LiteDB)

![image](https://user-images.githubusercontent.com/11272110/201447187-b7bbb786-5fa2-43b6-824a-b0bea39bc4be.png)

LiteDb 是使用 `C#` 编写的内置 `NoSQL` 数据库，跟 `Sqlite` 一样，非常小巧，可以内置到应用程序中而不依赖外部单独数据库服务器。

3、[CliWrap](https://github.com/Tyrrrz/CliWrap)

![image](https://user-images.githubusercontent.com/11272110/201447943-b6b393a5-557d-474e-905e-1f2551b22e9c.png)

如果要在 `C#` 中调用一个命令行，一般我们使用的方式是 `Process` 类

```csharp
string strCmdText;
strCmdText= "google.com";
System.Diagnostics.Process.Start("ping", strCmdText);
```

`CLIWrap` 这个库封装了这些功能，使他变得更加易于使用

```csharp
var result = await Cli.Wrap("ping")
    .WithArguments(new[] { "google.com" })
    .WithValidation(CommandResultValidation.ZeroExitCode)
    .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
    .ExecuteAsync();
```

- 首先执行的过程是异步的，而且还支持 `CancellationToken` 的模式
- 支持多种输出方式，标准输出，错误输出和进程返回值校验
- 支持管道概念（重载了 `|` 操作符）
- 支持进程解绑 （de-attach)
