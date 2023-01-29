# .NET 每周分享第 30 期

## 卷首语

![image](https://dotnetweeklyimages.blob.core.windows.net/031/suvery.png)

Jetbrains 公司发起的 `.NET` 生态开发者的调查问卷结果，主要包含下面的问题:

- C# 使用的版本
- .NET 项目使用的语言
- 开发项目的类型
- CLR 使用的版本
- IDE 或者编辑器使用
- VS 插件使用
- VS Code 插件使用
- 单元测试框架
- 性能调试频率

## 行业资讯

1、[VS 上搜索体验提升](https://devblogs.microsoft.com/visualstudio/new-better-search-in-visual-studio/)

![image](https://dotnetweeklyimages.blob.core.windows.net/031/vssearch.png)

Visual Studio 提升了搜索的体验，主要分为了代码搜索和功能搜索。选择 `Ctrl + T` 快捷键进行搜索，而且可以使用 `f:` , `t:` 和 `m:` 前缀分别表示文件，类型和成员搜索。

## 文章推荐

1、[C# 代码加密算法汇总](https://code-maze.com/dotnet-cryptography-implementations/)

![image](https://dotnetweeklyimages.blob.core.windows.net/031/encryption.png)

数据加密是现在计算机网络的基础，通过加密可以确保我们数据的安全性，那么在 `C#` 中如何使用这些加密算法呢？

1. Hash 加密

Hash 加密是一种单向加密，也就是说将一个输入转换到一个特定空间的数据。通常不能进行反向操作，通过这种方法可以判断输入的内容事否一致。通常有三种类别

- MD5

该算法已经被标记为不安全算法。

```csharp
var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes("This is my password! Dont read me!"));
byte[] hashOne;
using (var hasher = MD5.Create())
{
    hashOne = await hasher.ComputeHashAsync(strStreamOne);
}
var hashAsString = Convert.ToHexString(hashOne);
Console.WriteLine("Hash Value:\n" + hashAsString)
```

- SHA 族

通常有 `Sha-1`, `sha2` 这样的算法

```csharp
var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes("This is my password! Dont read me!"));
byte[] hashOne;
using (var sha256 = SHA256.Create())
{
    hashOne = await sha256.ComputeHashAsync(strStreamOne);
}
var hashAsString = Convert.ToHexString(hashOne);
```

- HMAC

这是一个需要密钥才能生成的哈希算法

```csharp
var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes("This is my password! Dont read me!"));
byte[] hashOne;
byte[] key = Encoding.UTF8.GetBytes("superSecretH4shKey1!");
using (var hmac = new HMACSHA256(key))
{
    hashOne = await hmac.ComputeHashAsync(strStreamOne);
}
var hashAsString = Convert.ToHexString(hashOne);
```

2. 对称加密

这是一个可以进行加密和解密的算法，两端使用相同的密钥进行工作，通常使用 `AES` 算法

```csharp
var dataStr = "This is corporate research! Dont read me!";
var data = Encoding.UTF8.GetBytes(dataStr);
var key = GenerateAESKey();
var encryptedData = Encrypt(data, key, out var iv);
var encryptedDataAsString = Convert.ToHexString(encryptedData);

public static byte[] Encrypt(byte[] data, byte[] key, out byte[] iv)
{
    using (var aes = Aes.Create())
    {
        aes.Mode = CipherMode.CBC; // better security
        aes.Key = key;
        aes.GenerateIV(); // IV = Initialization Vector

        using (var encryptor = aes.CreateEncryptor())
        {
            iv = aes.IV;
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }
    }
}

public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
{
    using (var aes = Aes.Create())
    {
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC; // same as for encryption

        using (var decryptor = aes.CreateDecryptor())
        {
            return decryptor.TransformFinalBlock(data, 0, data.Length);
        }
    }
}
public static byte[] GenerateAESKey()
{
    var rnd = new RNGCryptoServiceProvider();
    var b = new byte[16];
    rnd.GetNonZeroBytes(b);
    return b;
}
```

3. 非对称加密

就是加密方和解密方使用不同的密钥来操作，通常分为公钥和私钥，公钥可以公开，任何人都可以加密。但是只有私钥才能解开，反之亦然，私钥加密的内容，只有公钥才能解开。这样可以保证私钥的安全性。最著名的就是 `RSA` 算法，这也是 `HTTPS` 协议的基石。

```csharp
var dataStr = "This is corporate research! Dont read me!";
var data = Encoding.UTF8.GetBytes(dataStr);
var keyLength = 2048; // size in bits
GenerateKeys(keyLength , out var publicKey, out var privateKey);
var encryptedData = Encrypt(data, publicKey);
var encryptedDataAsString = Convert.ToHexString(encryptedData);
public void GenerateKeys(int keyLength, out RSAParameters publicKey, out RSAParameters privateKey)
{
    using (var rsa = RSA.Create())
    {
        rsa.KeySize = keyLength;
        publicKey = rsa.ExportParameters(includePrivateParameters: false);
        privateKey = rsa.ExportParameters(includePrivateParameters: true);
    }
}
public byte[] Encrypt(byte[] data, RSAParameters publicKey)
{
    using (var rsa = RSA.Create())
    {
        rsa.ImportParameters(publicKey);

        var result = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
        return result;
    }
}
public byte[] Decrypt(byte[] data, RSAParameters privateKey)
{
    using (var rsa = RSA.Create())
    {
        rsa.ImportParameters(privateKey);
        return rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
    }
}
```

另外一种是 `DSA` 算法，用来进行数字签名，因为它只需要校验事否匹配，性能上有优势。

```csharp
var dsa = DSA.Create();
var dataStr = "This is corporate research! Dont read me!";
var data = Encoding.UTF8.GetBytes(dataStr);
var signedData = Sign(dsa, data);
dsa.Dispose();
public byte[] Sign(DSA dsa, byte[] data)
{
    if(dsa is null)
        throw new NullReferenceException(nameof(dsa));
    var result = dsa.SignData(data, HashAlgorithmName.SHA256);
    return result;
}
public bool VerifySignature(DSA dsa, byte[] data, byte[] signedData)
{
    if (dsa is null)
        throw new NullReferenceException(nameof(dsa));
    return dsa.VerifyData(data, signedData, HashAlgorithmName.SHA256);
}
```

2、[C# 如何混淆反编译器](https://washi.dev/blog/posts/debugger-proxy-objects/)

这是一篇有意思的文章，作者是一名逆向工程师。我们都知道 `C#` 作为一个包含中间状态的编程语言（IL), 通常使用反编译工具就能得到源代码。那么如何做到混淆反编译工具，让它无法得到源代码呢？

1. Proxy Object

首先通过是 `Proxy Object` 来封装真正的类，而且 C#执行隐式操作符重载，比如创建 `PersonProxy` 类

```csharp
public sealed class PersonProxy
{
    private readonly Person _value;
    public Person(Person A_1)
    {
        _value = A_1;
    }
    // Implicit conversion operators:
    public static implicit operator PersonProxy(Person A_0) => new(A_0);
    public static implicit operator Person(PersonProxy A_0) => A_0._value;
}
```

这样任何可以使用 `Person` 的地方都可以使用 `PersonProxy`。同理可以为系统类型进行代理。

2. Display

C# 中的 `DebuggerDisplay` 注解可以方便调试，如何通过反编译工具对程序进行调试，那么可以在 `DebuggerDisplay` 中展示错误信息。

```csharp
using System;
using System.Diagnostics;
namespace System;
[DebuggerDisplay("{Display}")]
public struct Int32
{
    [CompilerGenerated]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int <0this>k__BackingField; // Renamed by our obfuscator

    public Int32(int A_1)
    {
        this.<0this>k__BackingField = A_1;
    }

    // Implicit conversion operators:
    public static implicit operator int(int A_0) => new(A_0);
    public static implicit operator int(int A_0) => A_0.<0this>k__BackingField;

    // Random display object:
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public int Display => 31; // Randomly selected by our obfuscator.
}
```

这里 `Display` 使用了一个 `31` 固定值，这样在调试 `Int32` 类型的时候，总是得到一个错误的值。或者可以抛出一个未知的异常。

```csharp
[DebuggerBrowsable(DebuggerBrowsableState.Never)]
public string Display
{
    get
    {
        Environment.FailFast("The CLR encountered an internal limitation.");
        return null;
    }
}

[DebuggerBrowsable(DebuggerBrowsableState.Never)]
public string Display => Display
```

亦或者在 Debug 的时候，修改程序的状态

```csharp
[DebuggerBrowsable(DebuggerBrowsableState.Never)]
public string Display
{
    get
    {
        // Change the state of the actual object with random values selected by our obfuscator.
        // No luck for the reverse engineer to ever find the original values back!
        this.<0this>k__BackingField.FirstName = "???";
        this.<0this>k__BackingField.LastName = "???";
        this.<0this>k__BackingField.CoolnessFactor = 0.0324466228f;
        return null;
    }
}
```

3、C# 构造函数简化

![image](https://dotnetweeklyimages.blob.core.windows.net/031/constructor.png)

```csharp
class Person
{
    public int Age { get; set; }

    public string Name { get; set; }

    public Person(string name, int age) => (Name, Age) = (name, age);
}
```

## 开源项目

1、[bflat](https://github.com/bflattened/bflat)

![image](https://dotnetweeklyimages.blob.core.windows.net/031/bflat.png)

bflat 是一个开源的 `C#` 编译工具，它可以将 `C#` 代码编译成可执行并且 NativeAOT 的执行文件。它和官方的编译工具的区别在于它可以运行在 `UEFI` 中，而且编译出来的文件体积小。

2、[.NET 平台上的 COBOL 编译器](https://github.com/otterkit/otterkit)

![image](https://dotnetweeklyimages.blob.core.windows.net/031/cobol.png)

`Cobol` 是一门古老的编程语言，至今仍然有不少机器任然运行者 Cobol 编写的程序。`.NET` 由于开放性，任何编程语言都可以在上面运行。`otterkit` 即使在 `.NET` 上实现了 `Cobol` 编译器。

3、[QuestPDF](https://github.com/QuestPDF/QuestPDF)

![image](https://dotnetweeklyimages.blob.core.windows.net/031/questpdf.png)

`QuestPDF` 是一个开源的 `.NET` PDF 生成器，它可以按照指定的内容生成相应的 PDF 文件。而且它内置了实时浏览工具，可以动态查看生成的 PDF 内容。

4、[MethodTimer](https://github.com/Fody/MethodTimer)

![image](https://dotnetweeklyimages.blob.core.windows.net/031/methodtimer.png)

我们常常需要测量一个方法执行时间，最直接的方法是使用 `Stopwatch` 测量，比如说

```csharp
var stopwatch = Stopwatch.StartNew();
try
{
    // Your method
}
finally
{
    stopwatch.Stop();
}
```

这样做肯定是正确的，那么有什么办法简化这个流程呢？就跟 `Python` 中的中装饰器一样呢？`MethodTimer` 库可以做到，它可以通过 `C#` 的 `Source Generation` 方式生成上述的类。而且还提供了 `MethodTimerLogger` 这个类定义耗时操作的日志输出。

```csharp
public static class MethodTimeLogger
{
    public static void Log(MethodBase methodBase, long milliseconds, string message)
    {
        //Do some logging here
    }
}
```
