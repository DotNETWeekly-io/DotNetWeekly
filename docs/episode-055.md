# .NET 每周分享第 53 期

## 卷首语

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/43a5200e-6bbe-425a-8851-27449cc0b566)

5 月 21 号到 23 号是一年一度的 Microsoft Build 大会。大会中会涉及到很多 `.NET` 相关的内容，包裹 `Aspire`,  `AI` 与 `.NET`, `Visual Studio` 等相关内容，如果感兴趣，可以线上加入。

## 行业资讯

1、[Avalonia项目宣布离开.NET基金会](https://github.com/AvaloniaUI/Avalonia/discussions/14666)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/ca12318a-e245-4d7c-b2e7-4d6484f05647)
 
Avalonia 团队宣布离开 `.NET Foundation` , 主要原因是想要让项目的主导权保留在开发核心团队。至于说具体的原因导致这次分手，声明中并没有提及，不过这一篇[文章](https://www.glennwatson.net/posts/dnf-problems-solutions)指出了 `.NET Foundation` 存在的问题和解决方案。

2、[Twitter .NET团队成员列表](https://twitter.com/i/lists/120961876)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/9af91c03-b051-4ae7-b70c-91f494edddc3)

`.NET` 团队成员的 `Twitter` 账号列表，关注他们，获得最新的资讯。

3、[Twitter Azure Cloud Advocates 列表](https://x.com/i/lists/847470660712505346)

`Azure Cloud Advocates` 团队成员的 `Twitter` 账号列表，关注他们，获得最新的资讯。

## 文章推荐

1、[跟 Stephen Toub 学习 Span](https://www.youtube.com/watch?v=5KdICNWOfEQ)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3dbfb0b2-99dd-48d8-8b9b-ec8f95b60fb6)

`Span<T>` 是高性能 `C#` 代码的秘诀之一，`.NET` 社区大佬 `Stephen Toub` 深入探究了什么是 `Span` 并且从头完成一个简易版的实现。

首先 `Span<T>` 要解决什么问题？假设我们现在有一个方法是这样的，

```csharp
private int Sum(int[] array)
{
    int sum = 0;
    foreach(var val in array) sum += val;
    return sum;
}
```
如果 `Sum` 方法的是求和数组的部分内容，那么方法的签名需要修改成这样

```
private int Sum(int[] array, int offset, int length)
{
    int sum = 0;
    for(int i = offset; i < length; i++) sum += array[offset+i];
    return sum;
}
```

这样会带来一个问题，就是这个方法只支持 `int[]` 数据类型，而 `C#` 中有很多类型都是表示连续的一段空间，比如 `List` 。所以 `Span` 这个类型结构就被提出来了，如果仅仅是表示一段连续空间，`Span` 并没有什么特殊之处，`C/C++` 中的指针，或者 `C#` 中的 `unsafe` 代码块也能够完成同样的工作，但是 `Span` 是内存安全的类型，而且还是一个值类型。

```csharp
readonly ref struct MySpan<T>
{
    private readonly ref T _reference;
    private readonly int _length;


    public MySpan(T[] array)
    {
        _reference = ref MemoryMarshal.GetArrayDataReference(array);
        _length = array.Length;
    }

    public MySpan(ref T reference)
    {
        _reference = ref reference;
        _length = 1;
    }

    public MySpan(ref T reference, int length)
    {
        _reference = ref reference;
        _length = length;
    }


    public ref T this[int index]
    {
        get
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }

            return ref Unsafe.Add(ref _reference, index);
        }
    }

    public MySpan<T> Slice(int offset)
    {
        if (offset < 0 || offset >= _length)
        {
            throw new IndexOutOfRangeException();
        }

        return new MySpan<T>(ref Unsafe.Add(ref _reference, offset), _length - offset);
    }
}
```

- `ref struct` 表明它只能用在方法中，而不能作为一个类的成员
- `ref T _reference` 指向了连续空间的第一个元素
- `ref T this[int index]` 说明连续空间的每个元素获取都是引用类型
- `Unsafe.Add` 该方法避免了访问非法内存

2、[如何往已有的代码中添加 Nullability](https://blog.maartenballiauw.be/talk/2024/01/21/bringing-csharp-nullability-into-existing-code.html)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/fb6fb8d9-0bf4-47cb-9531-0c8d200bba5b)

`Nullable Reference Type` 是 `C# 8` 引入新的语法，它可以解决我们应用程序中的 `System.NullReferenceException` 的异常。但是从来没有银弹，这个工作需要程序在编译时候付出额外的付出。那么如何将已有的项目中开启这个功能呢？这个幻灯片介绍了其中的概念，方法和工具。

3、[JavaScript, TypeScript, C#代码实现对比](https://github.com/CharlieDigital/js-ts-csharp)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/3c4f8172-b019-4fbf-802f-0f8039432451)

对于前端开发人员，`JavaScript` 或者 `TypeScript` 是两个非常熟悉的开发编程语言。但是 `C#` 这种后端开发语言和 `JavaScript` 或者 `TypeScript` 却越来越像，它们在语法，工具链上面相互学习。

4、[.NET 9 将要移除 Swagger，那怎么替换呢？](https://www.youtube.com/watch?v=8xEkVmqlr4I)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/180ad6df-ebd3-4851-ba73-bc86537e7da1)

`.NET 9` 中将会移除之前内置的 `Swagger` ，并且全面拥抱 `OpenAPI`。那么以后就不会有 Swagger 页面，只有一个 Web API 定义的 JSON 文件，但是 [Scalar](https://github.com/scalar/scalar?tab=readme-ov-file)  项目可以将其渲染成漂亮的 UI。

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/b957ddb3-cd97-4050-869a-1ec6d0442abb)

5、[.NET 9 LINQ 性能提升](https://steven-giesel.com/blogPost/783a404a-e39e-480f-bc99-a514a75d752d?utm_source=devdigest.today&utm_medium=website&utm_campaign=feature_promo&utm_content=link_click)

`.NET 9` 在 Linq 上继续有新的性能上的更新

- `Orderby.ToList`

通过 `Vector` 这个 SIMD 指令集提升

- `Chunk`

通过 `ReadOnlySpan` 提升性能

- `OfType`

通过处理特定的类型而不是通用的类型

- `Any`

通过调用 `TryGetNonEnumeratedCount` 方法提高性能

## 开源项目

1、[TeslaLogger](https://github.com/bassmaster187/TeslaLogger)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/6bc12ebc-ec11-4080-86d2-8fc610eb26d6)

TeslaLogger 是一个自托管的数据记录器，适用于您的 Tesla Model S/3/X/Y。目前，它支持 Raspberry Pi 3B、3B+、4B、Docker 和 Synology NAS。

2、[ILGPU](https://github.com/m4rs-mt/ILGPU)

ILGPU 是一个即时编译器（JIT），用于编写高性能 GPU 程序的 .NET 语言。ILGPU 完全用 C# 编写，没有任何本地依赖。它结合了 C++ AMP 的灵活性和便利性，以及 CUDA 程序的高性能。内核范围内的函数不需要注解（默认 C# 函数），并且可以作用于值类型。所有内核（包括共享内存和原子操作等硬件特性）都可以使用集成的多线程 CPU 加速器在 CPU 上执行和调试。

3、[以太坊.NET库](https://github.com/Nethereum/Nethereum)

Nethereum 是 .NET 的以太坊集成库，简化了与公共或许可的以太坊节点（如 Geth、Parity 或 Quorum）的访问和智能合约交互。

4、[币安.NET客户端库](https://github.com/JKorf/Binance.Net)

![image](https://github.com/DotNETWeekly-io/DotNetWeekly/assets/11272110/d5bc7acd-a0b8-446b-90b9-abc9b5e4bb8d)

Binance.Net 是一个强类型的客户端库，用于访问 Binance 的 REST 和 Websocket API。所有数据都映射到可读的模型和枚举值。其他功能包括实现客户端订单簿维护、与其他交易所客户端库的轻松集成等。