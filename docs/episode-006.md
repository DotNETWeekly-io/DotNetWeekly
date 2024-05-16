# .NET 每周分享第 6 期

## 开卷语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9e5eb65f-bd1c-484c-a53d-397ae7de5f0c)

`Tiobe` 公布了 2021 年度语言，`Python` 再一次夺得头名。但是之前都是 `C#` 处于领头羊的位置，只不过在最后一个月被 `Python` 反超。不管怎样，都说明了 `C#` 仍然往好的方向发展。

## 行业资讯

1、[在 Visual Studio 2022 中聊天](https://devblogs.microsoft.com/visualstudio/integrated-chat-in-live-share-for-visual-studio-2022/)

在过去的一段时间内，软件开发往往被认为是一种单独的行动

> 程序员在漆黑的屋子里，面对的屏幕敲击着花花绿绿的字符，然后就完成了一件匪夷所思的事情。

随着开源运动的兴起，这个时代已经过去了，我们希望在开发过程中也与他人交流和沟通。现在 `Visual Studio 2022` 中也加入了实时聊天的功能，快去尝试一下！

2、[Visual Studio 2022 格式化文件](https://devblogs.microsoft.com/visualstudio/bringing-code-cleanup-on-save-to-visual-studio-2022-17-1-preview-2/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/33aab5ed-98d3-4a3b-b999-7bd02f6e1e81)

代码有两个主要目的：

- 让别人阅读
- 让机器执行

统一的代码风格是方便他人阅读是重要。 不少开发语言都规定了风格规范，甚至比如 `Go` 语言将代码风格作为语法的一部分。`C#` 也有 `StyleCop` 来定义代码规范。`Visual Studio 2022` 将会引入了一个功能能够帮助我们在保存代码文件的时候，自动按照预先设置的规范调整代码，类似于 `Go` 语言中 `go fmt` 命令。

## 文章推荐

1、[PowerShell 学习课程](https://www.youtube.com/playlist?list=PLDjtLML5l9l-y_MPI5qThjZqvcIwHnMzw)

PowerShell 是一个强大的 Shell 工具，这一系列课程帮你学习如何它。

2、[StringBuilder 探秘](https://www.stevejgordon.co.uk/how-does-the-stringbuilder-work-in-dotnet-part-1-why-do-we-need-a-stringbuilder-and-when-should-we-use-one)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/f17acd1e-6e75-4ea4-b5fe-b44dcdf85811)

`StringBuilder` 是一个广泛使用的 `C#` 类，关于它有哪些具体的细节可以探索的呢？这一系列文章可以帮你了解它们

- [什么时候使用它？](https://www.stevejgordon.co.uk/how-does-the-stringbuilder-work-in-dotnet-part-1-why-do-we-need-a-stringbuilder-and-when-should-we-use-one)
- [理解开销](https://www.stevejgordon.co.uk/how-does-the-stringbuilder-work-in-dotnet-part-2-understanding-the-overhead)
- [Append 的工作机制](https://www.stevejgordon.co.uk/how-does-the-stringbuilder-work-in-dotnet-part-1-why-do-we-need-a-stringbuilder-and-when-should-we-use-one)

## 开源项目

1、[通过 Roslyn analyzer 提高代码质量](https://github.com/meziantou/Meziantou.Analyzer)

`StyleCop` 可以帮助我们检测 `C#` 代码中不符合风格的部分。`Meziantou.Analyzer` 包借助了 `Roslyn analyzer` 来分析代码中是否出现了不是 `Best Practice` 的情况，它包含下面几个大类

- Usage
- Style
- Usage
- Performance
- Security
- ...

[这里](https://github.com/meziantou/Meziantou.Analyzer/tree/main/docs) 通过例子展示了错误的 `C#` 代码姿势。
