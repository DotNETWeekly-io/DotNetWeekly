# .NET 每周分享第 20 期

## 卷首语

[关于 .NET 的偏见](https://www.youtube.com/watch?v=ouuTgwblvJI&ab_channel=NickChapsas)

这是一期非常有意思的视频，UP 主浏览了一篇 `Java VS C#` 的文章，虽然是发布日期就在最近，也就是 2022 年 6 月，但是文章中充满了对 `C#` 的偏见和无数个互相排斥的观点。

## 行业资讯

1、[MAUI 设计网站上线](https://devblogs.microsoft.com/dotnet/announcing-dotnet-maui-beautiful-ui-challenge/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/5f2315d8-3c9f-4d09-822a-d9ef3d2388df)

想要在自己 `MUAI` 应用程序中包含漂亮的 UI ？`Snppts` 网站可以帮助到你。它提供了众多的 `MUAI` 应用程序的 UI，并且包含源码。

## 文章推荐

1、[Azure 开发平台](https://michaelscodingspot.com/deploying-to-azure/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/0b63e68e-26ff-4df6-9af5-9ad772ea07ac)

在 `.NET` 的开发世界里，`Azure` 是一个绕不开的开发平台，在这个平台上有多种服务可以选择，这篇文章介绍了 Azure 上常见的服务：

- Window/Linux
- 容器化
- 微服务
- Azure Storage/CDN
- Static Web Apps
- Azure App Service
- Azure 虚拟机
- Azure Functions
- Azure Kubernetes Service
- Container App
- Service Fabric

2、[集合拼接比较](https://code-maze.com/csharp-concatenate-lists/)

将两个集合拼接起来是常见的开发需求，那么在 `C#` 中该如何实现呢？这篇文章尝试了多种方式，主要有

- `List` 的 `Add` 方法
- `Enumerable.Concat` 方法
- `Enumerable.Union` 方法
- `List` 的 `AddRange` 方法
- `Array` 的 `CopyTo` 方法
- `SelectMany` 方法

通过 `Benchmark` 比较，一般而言`AddAndRange` 是性能最好。

3、[线程安全的异步方法](https://www.youtube.com/shorts/mr8kdAauc7E)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/52fd1b84-fd50-4f13-96ce-d7612a22b2b6)

在同步代码中，如果要避免条件竞争，通常会选择 `lock` 关键字来保证任何时刻只有一个线程能够访问该资源，例如：

```csharp
private object obj = new object();
lock(obj)
{
    // code
}
```

但是对于异步方法，首先编译器会报错，因为异步方法不能在 `lock` 的语句中，那么我们该怎么办呢？答案是 `SemaphoreSlim` 类。

```csharp
private SemaphoreSlim semaphore = new SemaphoreSlim(1);

async Task DoWork()
{
    await semaphore.WaitAsync();
    await DoAnotherWork();
    semaphore.Release();
}
```

4、[Linq 在 .NET 6 上的提升](https://medium.com/@omer.ingec24/linq-improvements-in-net-6-280a475d1801)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/c76e34e0-6c10-4368-873e-b4ab2ee9e8cd)

`LINQ` 是 `.NET` 平台上最强大的工具之一，它在 `.NET 6` 中也有新的提升，主要包含

- `FirstOrDefault` 和 `SingleOrDefault` 可以指定默认返回值，而不是类型的默认值
- `MinBy`, `MaxBy`, `DistinctBy` 等方法，可以指定属性
- `Chunk` 方法可以将枚举类型按照相应的数量分块
- `Zip` 方法支持三个枚举类型
- 新的 `Index` 和 `Range` 语法糖支持

5、[GC 文章推荐](https://github.com/Maoni0/mem-doc)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/db4fd897-af0a-4347-b350-7e1f90280ac1)

Maoni Stephens 是微软负责 `.NET` GC 的架构师，这个仓库包含了她过去对 `GC` 内容的分享的材料，也是我们 GC 性能调优的参考资料。

## 开源项目

1、[UnitNet](https://github.com/angularsen/UnitsNet)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/4e5b0271-fedb-4b76-8786-8b260f20aa14)

单位运算是开发过程中常常会遇到的问题，比如长度，面积，体积等等。这时候不仅仅需要编程的知识，还需要相应的物理知识。`UnitsNet` 包可以帮助你解决大部分问题。

2、[.NET Community Toolkit 发布](https://devblogs.microsoft.com/dotnet/announcing-the-dotnet-community-toolkit-800/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/a13a608f-e28e-40cb-835b-ff7e37ee8864)

最近 `Community Toolkit` 包已经发布，它是从 `Windows Community Toolkit` 演化过来，所以版本号直接变成 `8.0.0` , 这次发布移除的 `Windows` 系统的依赖，变成一个通用的跨平台工具包。随着 `MAUI` 的正式发布，该包对 `MUAI` 提供了大量的帮助，最重要的是 `MVVM` 的支持。

3、[Visual Studio 插件 RestClient](https://coderethinked.com/rest-client-for-visual-studio-2022/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/7296e179-adfe-4cf9-9625-a153fec2e8ba)

`Visual Studio` 有一个 `Rest Client` 的插件，可以直接在 `Visual Studio` 中执行一个 `.http` 的问题，该文件可以直接直接发起 `HTTP` 请求。

```bash
GET https://bing.com

@hostname = localhost
@port = 5143
@host = http://{{hostname}}:{{port}}
@contentType = application/json
PUT {{host}}/user/update/1
Content-Type: {{contentType}}
{
  "userName": "kchintala",
  "firstName": "Karthik",
  "lastName": "Chintala"
}
```

4、[quartznet](https://github.com/quartznet/quartznet)

quartnet 是一个开源的作业调度框架，是 OpenSymphony 的 Quartz API 的.NET 移植，它用 C#写成，可用于各种 C#应用中。它提供了巨大的灵活性而不牺牲简单性。你能够用它来为执行一个作业而 创建简单的或复杂的调度。它有很多特征，如：数据库支持，集群，插件，支持 cron-like 表达式等等。
