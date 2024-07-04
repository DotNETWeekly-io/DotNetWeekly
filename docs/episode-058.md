# .NET 每周分享第 58 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9be2f5e1-ec9b-4eba-8494-8638f73634e3)

这是 Reddit 上的一个的[讨论](https://www.reddit.com/r/dotnet/comments/1db9f0t/most_desirable_skills_for_experienced_net/), 问的是一个有经验的 `.NET` 开发者应该具备哪些技能。

## 行业资讯

1、[MAUI VS Code 插件 GA](https://devblogs.microsoft.com/dotnet/the-dotnet-maui-extension-for-visual-studio-code-is-now-generally-available/)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/1ccb21ab-b340-417c-ab08-ba9b35ae618b)

.NET MAUI 扩展现已在 Visual Studio Code 中正式发布。该扩展支持在 Visual Studio Code 中开发 .NET MAUI 应用，提供 XAML 智能感知、热重载以及移动和桌面设备的支持。它增强了 XAML 编辑功能，并允许在开发过程中实时更新 C# 和 XAML 文件。

## 文章推荐

1、[Lambda 参数默认值](https://devblogs.microsoft.com/dotnet/refactor-your-code-with-default-lambda-parameters/)

在 `C#` 12 之后， `Lambda` 表达式的参数可以使用默认参数。举例来说，在 `C#` 12 之前，我们的 lambda 定义如下

```csharp
// Before C# 12
var IncrementBy = static (int source, int? increment) => 
{
    return source + (increment ?? 1);
};
```

那么现在可以这样使用

```csharp
var IncrementBy = static (int source, int increment = 1) => 
{
    return source + increment;
};
```

2、[ASP.NET Core 中的 HSTS](https://khalidabuhakmeh.com/what-is-hsts-and-why-is-it-in-my-aspnet-core-app)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/557802ac-97cf-4249-8137-84e17d254f25)

在 `ASP.NET Core` 应用程序中，有一个中间件是 `app.UseHsts()`, 那么这个中间件是做什么的呢？简单来讲就是强制客户端的请求是使用 `HTTPS` 协议，如果是 `HTTP` 协议访问，就重定向到 `HTTPS` 协议。主要包含以下几点内容

- 永远使用 `HTTPS` 才能进一步通信
- `includeSubdomains` 指向所有的子域名
- `preload` 是浏览器永远使用 `HTTPS` 请求
- `max-age` 指向这个策略的最大有效期

体现在响应头中是

```txt
`Strict-Transport-Security:` `max-age=63072000; includeSubDomains; preload`
```

在 `ASP.NET Core` 的代码是这样配置的

```csharp
builder.Services.Configure<HstsOptions>(o =>
{
    o.Preload = true;
    o.MaxAge = new TimeSpan(730 /* 2 years */);
    o.IncludeSubDomains = true;
});
```

3、[如何正确的测量 C# 代码速度](https://dev.to/byteminds_agency/how-to-properly-measure-code-speed-in-net-158o)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/cda1c57e-f146-4d73-b33e-9b58bff893a1)

[BenchmarkDotNet](https://benchmarkdotnet.org/articles/overview.html) 是 C# 中最出名的性能测试框架，这边文章介绍了使用这个框架的基本用法。

4、[C# 代码规范最佳实践](https://blog.jetbrains.com/dotnet/2024/06/18/code-style-for-better-productivity-tips-and-tools-from-the-metalama-team/)

一致性的 `C#` 代码有两个作用

1. 保证代码更加容易阅读，因为一个代码被阅读的次数肯定大于编写的次数
2. 统一的代码规范可以很方便合作，比如代码合并

那个统一代码规范的最佳实践是步骤包含哪些？

- 配置 IDE： 比如 `.editorconfig` 文件
- 配置代码整洁：当配置完代码格式化偏好，可以通过工作自动化格式代码。不过要注意这个过程并不是 100% 正确，需要非常小心
- 报告违反风格的代码：在 `C#` 工程中，可以让这些违反格式的代码作为编译的 `warning`. 在 `.editorconfig` 文件的中配置如下 `dotnet_style_qualification_for_event = true:warning`, 同时在 `csproj` 文件中配置 `<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>`
- 进一步提高代码规范：在 `C#` 社区中，有很多开源工作可以帮助我们让代码规范更上一层楼

5、[针对初学者的.NET解决方案架构师学习路线](https://medium.com/@scholarhatblogs/net-solution-architect-roadmap-for-beginners-1f303aeb29c0)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/71496315-5f6d-4f53-9e4d-497fab5b88c9)

如果你是一个架构师，当你负责一个项目的时候，并且选择 `.NET` 作为主要的技术栈。那么这篇文章的路线可以帮助你成长为一名合格的架构师

1. 理解架构师的角色的职责
2. .NET 解决方案的关键

- 编程语言
- 框架
- 数据访问
- 库和设计
- 云和部署

3. 前端
- Web 技术

4. 数据库关键
- 数据管理和授权
- 安全和合规
- 大数据分析

5. 云平台关键
- 云服务
- 云平台
- 云架构和设计

6. API 设计和集成
7. Docker 和 K8S
8. DevOps

6、[基于.NET的垂直切分架构](https://medium.com/codenx/vertical-slicing-architecture-in-net-6efa39b139e7)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e507ed94-5423-4eaf-876a-6ef8e6640467)

通常 `.NET` 应用程序的架构是按照水平层次的划分，划分的标准是技术层面，比如 UI 层，业务逻辑层和数据访问层。有一种其他的划分方式是垂直的方式，通常将每个功能封装一个切片，每个切片包含三个层：

1. 表示层
2. 业务逻辑层
3. 数据访问层

以 `ASP.NET Core Web API` 为例，整个项目架构如下：

```csharp
VerticalSlicingExample
│
├── Features
│   ├── Orders
│   │   ├── CreateOrder
│   │   │   ├── CreateOrderCommand.cs
│   │   │   ├── CreateOrderHandler.cs
│   │   │   └── CreateOrderController.cs
│   │   └── GetOrder
│   │       ├── GetOrderQuery.cs
│   │       ├── GetOrderHandler.cs
│   │       └── GetOrderController.cs
│   └── Customers
│       ├── CreateCustomer
│       │   ├── CreateCustomerCommand.cs
│       │   ├── CreateCustomerHandler.cs
│       │   └── CreateCustomerController.cs
│       └── GetCustomer
│           ├── GetCustomerQuery.cs
│           ├── GetCustomerHandler.cs
│           └── GetCustomerController.cs
├── Program.cs
└── Startup.cs
```

和传统分层的架构相比较，有如下的优缺点：

**传统分层架构**

- 结构：
根据技术关注点将应用程序划分为水平层。
典型的层包括表示层、业务逻辑层和数据访问层。

- 内聚性：
功能内聚性较低，因为相关代码分散在多个层中。
层内内聚性高，但跨功能的内聚性不一定高。

- 依赖性：
层之间以层级方式相互依赖。
较低层的变更（例如，数据访问层）可能对较高层（例如，表示层）产生连锁反应。

- 功能开发：
实现单个功能需要触及多个层。
由于跨层变更，开发速度较慢。

- 可扩展性：
水平扩展需要复制整个层。
难以独立扩展特定功能。

- 维护：
维护更具挑战性，因为变更通常需要跨多个层进行更新。
由于功能逻辑的分散，调试可能复杂。

- 测试：
单元测试关注各个层，使得整体验证的集成测试至关重要。
测试设置更复杂，以确保所有层正确协同工作。

**垂直切片架构**

- 结构：
根据功能将应用程序划分为垂直切片。
每个切片包括自己的表示层、业务逻辑层和数据访问组件。

- 内聚性：
每个功能切片内部高度内聚，所有相关代码位于一起。
每个切片独立运作。

- 依赖性：
功能彼此独立。
一个功能切片的变更对其他切片的影响最小。

- 功能开发：
由于每个功能可以独立实现，开发速度更快。
开发者可以一次专注于一个切片，无需担心应用程序的其他部分。

- 可扩展性：
更容易独立扩展特定功能。
每个功能可以根据需要部署和扩展，而不会影响其他功能。

- 维护：
由于变更局限于功能切片内，维护更容易。
由于所有相关代码都包含在一个切片内，调试简化。

- 测试：
单元测试关注个别功能切片，使得隔离和测试功能更容易。
集成测试更为直接，因为每个切片都是一个完整的功能。

## 开源项目

1、[Stateless](https://github.com/dotnet-state-machine/stateless)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/b575b6b7-cdc9-44c3-aace-68560239d800)

状态机（State Machine） 是软件开发过程中很常见的概念，在编程语言的语法层面，我们需要写很多 `if-else` 语句，如果想要在但是我们可以借助 `Stateless` 开源库来简化这个过程。 举例来讲，一个灯控制系统，它的控制逻辑是这样的：

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9301f2d8-4ce9-44ca-880c-0bad30a59b58)

- 当灯开启是需要的，开和关的动作可以控制状态
- 当灯不需要的，开和关都不会控制状态

那么使用编程语言实现的话，代码如下：

```csharp
if (CurrentState == State.ON)
{
    CurrentState = State.OFF;
}
else
{
    if(_enforceTimeConstraint)
    {
        if (IsLightNeeded()) CurrentState = State.ON;
    }
    else
    {
        CurrentState = State.ON;
    }
}
```

当控制逻辑变得的复杂的时候，我们可以借助 `Stateless` 简化我们的代码

```csharp
enum trigger { Toggle }
enum State { On, Off }
var CurrentState = State.Off;
StateMachine<State, trigger> _machine = new StateMachine<State, trigger>(() => CurrentState, s => CurrentState = s);    
_machine.Configure(State.On)
    .Permit(trigger.Toggle, State.Off);
_machine.Configure(State.Off)
.PermitIf(trigger.Toggle, State.On, () => IsLightNeeded(), "Toggle allowed")
.PermitReentryIf(trigger.Toggle, () => !IsLightNeeded(), "Toggle not allowed");
_machine.Fire(trigger.Toggle);
```

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/e00e749e-feff-4521-8a87-e47923c2ef2e)

2、[MagicScaler](https://github.com/saucecontrol/PhotoSauce)

MagicScaler 是 `C#` 开源的高性能图片处理库。

3、[ComputeSharp](https://github.com/Sergio0694/ComputeSharp)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/ce6aefa6-7838-439b-8740-85dc1bc16454)

ComputeSharp 是一个 .NET 库，通过 DX12、D2D1 以及动态生成的 HLSL 计算和像素着色器在 GPU 上并行运行 C# 代码。可用的 API 使您能够访问 GPU 设备、分配 GPU 缓冲区和纹理、在它们与 RAM 之间移动数据、完全用 C# 编写计算着色器，并让它们在 GPU 上运行。
