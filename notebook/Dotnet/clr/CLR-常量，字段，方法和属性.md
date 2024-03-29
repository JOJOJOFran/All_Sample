[TOC]

# 设计类型

## 常量

### 1.什么是常量

​	常量是值从不变化的符号，在编译之前值就必须确定。编译后，常量值会保存到程序集元数据中。所以，常量必须是编译器识别的基元类型的常量，如：Boolean,Char,Byte,SByte,...,...,...,UInt64,Single,Double,Decimal,String。另外，C#是可以定义非基元类型的常量的，前提是值必须为null。

```c#
public sealed class SomeType
{
    public const SomeType Empty=null;
}
```

   	

### 2.常量的特性

- 常量成员将创建元数据，它是直接嵌入在代码内部，运行时不需要额外分配内存。
- 常量被视为静态成员，而不是实例成员。
- 不能获取常量的地址
- 不能以引用的方式传递常量
- 参考上面的特性，如果跨程序引用，尝试改变常量初始值，不仅dll需要重新编译，引用者也需要编译

## 字段

### 1.什么是字段

字段是一种数据成员，它可以是值类型的实例也可以是引用类型的引用。

CLR支持类型字段和实例字段，什么是类型字段？它其实就是我们熟悉的静态字段，实例字段就是非静态字段。

#### 1.1类型字段（静态字段）的内存分配过程

类型对象（静态对象）是在类型加载到一个AppDomain时创建的，而所需内存也是在内型对象中分配的。

接着上面的问题，那么，什么时候将类型加载到AppDomain中内？当第一次对引用到该类型的方法进行JIT编译时，

#### 1.2实例字段的内存分配过程

实例字段的内存，是在构造容纳字段的类型进行实例构造时分配的。

### 2.字段特性

字段存储在动态内存中，它不像常量，所以只能在程序运行时，才能够获取到它的值。字段可以是任何类型，不像常量有类型上的限制。

#### 2.1字段修饰符

| Static       | static   | 指定字段为类型的一部分，而不是对象的一部分                   |
| ------------ | -------- | ------------------------------------------------------------ |
| **Instance** | 默认     | 指定字段与实例关联，而不是和类本身关联                       |
| **InitOly**  | readonly | 只能在构造器方法中进行值的写入，否则只读                     |
| **Volatile** | volatile | 表示，编译器和CLR以及硬件，不会对这种字段标识的代码执行“线程不安全的措施”，只有CLR中的基元类型能使用这个修饰符。 |

#### 2.2 readonly和read/write

通常，字段都是read/write，即可读可写的，这也意味着，字段的值会随着运行可能发生值得变化。而当你把字段标记为readonly，那么你就只能在构造函数中，对它进行赋值，编译器是不会允许你在构造器（构造函数）以为的任何方法写入值，或变更值。

当然，C#提供了一种内联初始化的语法糖来进行readonly值的初始化，这种语法也可以对常量和其他形式的字段进行赋值。

```c#
public readonly int =250;
```

当然，使用内联语法，而不是在构造器中构造，可能会有一些性能问题，会在CLR via C#第八章进行讨论。

## 方法

- 实例构造器（引用类型）
- 实例构造器（值类型）
- 类型构造器（静态构造器）
- 扩展方法
- 分部方法

### 实例构造器（引用类型）

#### 什么是构造器？

构造器是将类型的实例初始化到良好状态的特殊方法。在“方法定义元数据表”中始终叫.ctor(constructor的简称)。

#### 引用类型在内存中如何实例化？

首先为实例的数据字段分配内存空间，然后是为初始化对象的附加字段（没错，就是我们经常会提到的同步块索引和类型对象指针）分配内存，然后最后开辟一个空间来调用实例构造函数进行对象的初始化。

在调用构造器之前，为对象分配的内存总是先被归零，为了保证那些被构造器显示重写的字段都获得0或者null的值。

![实例化内存初始化顺序](G:\Users\Fran\Desktop\blog_image\blog\clrviac#\常量字段方法\实例化内存初始化顺序.png)

#### 实例构造器的特性：

- 实例构造器永远不能被继承，类必须执行自己的构造函数。如果没有，系统默认会构造一个无参的。
- 所以，实例构造器不能用new ,override,sealed和abstract修饰
- 如果类的修饰符为abstract，那么构造器可访问性默认为protected,否则默认为public。
- 如果基类没有提供无参构造函数（意味着显示的实现了有参的构造函数），那么派生类必须显示调用一个基类的构造器（及为了保证参数一致），否则编译报错。
- static（sealed和abstract）修饰的类，编译器不会为它生成默认的构造函数
- 通常情况下，无论如何实例化派生类，基类的构造函数一定会被调用，所以object的构造函数一定会被先调用，但是实时上它什么也不会干。
- 极少数情况下，对象实例不会调用构造函数。如，Object的MemberwiseClone方法，它是用来分配内存，初始化对象的附加字段的，然后将源对象的字节数据复制到新对象中。
- Notice:不要在构造器中调用虚方法。因为，假如被实例化的类型重写了虚方法，就会执行派生类型中的实现，但这个时候，却是没有初始化的，所以，容易导致无法预测的行为。

内联语法（在字段一节提到过）方式实现初始化实例字段，其实也是转换成构造器方法中的代码来实现。

### 实例构造器（值类型）

CLR是允许值类型创建实例，但是c#编译器是不会默认为值类型构建构造函数的，并且值类型构造器必须显示调用才执行。如上面所说，即使你自己定义了一个构造函数，不管它是有参还是无参，编译器都不会去自动调用它，如果你想执行，必须自己显示进行调用。

然而，上面说那么多，在C#中，编译器根本不允许你定义值类型的无参构造函数，它会报：error CS0568:结构不能包含显示的无参构造函数。

同理，你不能对值类型的字段成员进行内联赋值，因为内联语句实际上是通过构造器进行赋值，如下面的代码：

```c#
internal struct SomeValType
{
    private int m=5;
}
```

上面的代码，会报：结构中不能有实例字段初始值设定项。

所以，值类型的字段总是被初始化为0或null,因为没有真正意义上的构造函数为它初始化其他值，只有你手动去调用构造函数（所以这里我们不理解为初始化）。

当你提供一个有参构造函数时，你需要为所有的字段进行赋值，否则会报：error CS0171:在控制返回到调用方法之前，字段XXX必须完全赋值。

### 类型构造器（静态构造器）

#### 什么是类型构造器？

实例构造器是为了让类的实例有一个良好的可验证的初始值。而类型构造器是为静态类型服务，顾名思义，类型构造器则是为了让类型有良好的初始状态。

#### 类型构造器特征

- 默认没有构造函数
- （类型）静态构造器永远不能有参数
- 必须标记为static,因为静态类型的成员必须为静态成员
- 不能赋予任何访问修饰符，默认为隐式类型,C#默认为private
- 类型构造器中的代码只能访问类型的静态字段（常规用途就是初始化这些字段）

#### 类型构造器的调用过程

类型构造器调用过程大致如下：

JIT编译器在编译到一个静态方法时，会查看引用了哪些静态类型。如果这个静态类型定义了一个构造函数，JIT编译器会检查当前AppDomain,是否已经执行过了这个类型构造器。如果已经执行过，就不添加对它的调用。如果从未执行过，JIT编译器会在它的本机代码中添加对类型构造器的调用。

重要的是：为什么静态类型的特性是十分适合做单例呢？因为CLR常常是确保每一个AppDomain中，一个类型构造器都只执行一次，那么上述的机制不足以很好的支撑这个特性，因为，多个线程下如何保证呢？为了保证这一点，调用类型构造器时，每一个调用线程都会获取一个互斥线程同步锁，在这样的机制下，如果多个线程试图同时调用某个类型的静态构造器，只有一个线程可以获得锁，其他的线程会被阻塞。只有第一个线程会执行静态构造器的代码。当一个线程离开构造器后，正在等待的线程才会被唤醒，后面的线程会发现，类型构造器已经被执行过了，将直接从构造方法返回。这样就能确保不会被再次调用。并且以上是线程安全的。

所以，单例模式就是借助上面的特性，你想构建的单例对象，则也应该放到类型构造器中进行初始化。

注意：值类型中也可以定义类型（静态）构造器，但是是不推荐这么做的，因为有时候CLR有时不会调用值类型的静态类型构造器。

```c#
internal struct StructValType
{
    //虽然值类型的构造函数必须有参数，但是这个是静态构造函数，所以它是一定没有参数的，也不用遵守，必须初始化所有成员的值
    static StructValType()
    {
        Console.WriteLine("我会出现吗？")；
    }
    public int x;
}

    class BaseClass
    {
        public string ClassName { get; set; }

         static BaseClass()
        {
            Console.WriteLine("I'm BaseClass static Constructor without param");
        }

        public BaseClass()
        {
            Console.WriteLine("I'm BaseClass Constructor without param");
        }
    }


   
```

上述代码，BaseClass中和StructValType中都有static构造函数，再对两个类进行实例时，你可以发现值类型的静态函数是没有被调用的。

注意：单个线程中，两个类型构造器包含互相引用的代码可能出问题，因为你无法把握两者的实现顺序，也就无法保证能正确的引用。因为是CLR负责类型构造器的调用，所以不能要求以特定的顺序调用类型构造器。

如果，类型构造器抛出未处理的异常，CLR会认为类型不可用。试图访问该类型的任何字段和方法都会抛出System.TypeInitializationException异常。

### 扩展方法

略，随便打开一个开源代码就能找到很多，十分常见。

## 属性

## 参数

没有太多特别需要记录的，日常中编译器的提示都能让你印象深刻，记一个小例子：

```c#
public void M(int x,string s,DateTime dt=default(DateTime),Guid guid=new Guid())
{
    ///
}
```

- ref out 关键字不可设置默认值

### 浅析ref out 关键字

如果从对象的内存模型上去理解，就能清晰的理解这两个关键字的缘由和作用，引用类型都是堆上存储值，栈上存储引用地址，传递参数传的则是引用地址。而值类型则是存储在方法栈上的，字符串虽然是引用类型，但是是驻留池的模式，只有使用了ref out关键字后，才能将方法中的值传递出来，而out则必须在方法内部对参数进行赋值，ref则是在传递进方法前需要赋值。

- 方法不能根据ref和out这两者之间进行重载

### 可变数量参数和带默认值参数

```c#
Add(params int[] values);
Add(int page=1,int limit=20)
```

## 参数设计原则

尽量指定最弱的类型，用接口也不用基类，使得程序更加灵活可扩展，返回类型也是如此。

## 泛型

### 优势

- 源码保护
- 类型安全
- 减少装箱拆箱提高性能
- 代码简洁，减少代码复用

### 开放类型封闭类型

- CLR既无法构造接口实例，也无法构造泛型（开放类型，为所有泛型类型指定实际的数据类型后则变为封闭类型）实例

### 泛型逆变和协变

不变量：泛型类型参数不能更改

逆变量（contravariant）:意味着泛型可以从一个类型转变为它的派生类

协变量（covariant）:意味着泛型可以从一个类型改变为它的基类

```c#
public delegate TResult Func<in T,out TResult>(T args);
Func<Object,ArgumentException> fn1=null;
//不需要显示转换
Func<string,Exception> fn2=fn1;
```

泛型方法无法添加逆变协变的约束

### 主要约束次要约束

- 泛型可以有零个或一个主要约束，可以有多个次要约束

- 主要越是可以是非密封类的一个引用类型，不能指定System.Object,System.Array,System.Delegate,

  System.ValueType,Enum,Void这些特殊类型

- class和struct是很特殊的主要约束，class指定类型必须是引用类型，struct则指定必须是值类型

- 次要约束主要代表接口，还有一组类型参数约束，也叫裸类型约束，如下

```c#
//T必须与TBase类型兼容
private static List<TBase> ConvertIList<T,TBase>(IList<T> list) where T:TBase{}
```

### 构造器约束

```c#
//CLR以及C#编译器，只支持无参构造器约束
internal sealed class Test<T> where T:new()
```

### 泛型设置默认值

因为无法知道泛型是引用类型还是值类型无法直接给默认值，而是通过default(T)来设置

### 泛型类型比较

- 如果无法确定泛型是引用类型，对变量比较是不合法的，也就是说引用类型进行比较是合法的，值类型不合法（除非值类型重载了操作符）
- 泛型类型不能约束为具体的值类型，因为值类型是隐式密封的

### 泛型类型变量作为操作数

由于泛型类型无法确定为某个值类型，进行计算的话很容易出问题，比如操作符无法应用于泛型类型的操作数。这是一个严重的限制，许多人尝试用dynamic和反射，操作符重载的思路来解决，但是这些又影响性能。

## 接口

### 定义以及使用

略