[TOC]

#CLR的多线程和异步

## 概述

### 进程

进程是应用程序需要的资源的集合，每个进程都被赋予了一个虚拟地址空间，确保一个进程中使用的代码和数据无法由另一个进程之间访问。

### 线程

 线程分为系统线程，托管线程，逻辑线程等等，这是我们需要重点讨论的对象，将放在下一节重点讨论。

### 并发

同时做多件事情，比如处理多个请求等等

### 多线程

并发的一种形式，才有多个线程来执行程序。多线程不是并发的唯一形式，并且，如今多线程不是指系统线程，而是指高级的抽象机制来使得程序功能更强大，更高效。

### 并行处理

把正在执行的大量任务，分隔成小块儿，分配给多个线程去执行，以达到cpu的高度利用。

### 异步

并发的一种形式，采用future模式或者回调机制，通过线程池调度线程，减少线程的创建和切换，虽然也会发生线程的上下文切换，但是是用户态的切换，效率比内核态要高得多。

### 响应式编程

一种声明式编程模式，程序在该模式中对一系列事件做出响应，并进行状态更新。响应式不一定是并发的，但它和并发编程联系紧密。

## 线程

### 线程出现的背景

以前计算机只有一个执行线程，并没有明确的线程概念，所以。无法实现多任务的同时进行，一旦有任务阻塞，只能无奈的等待或者重启，随着硬件的发展和需求的提升，多线程的出现成了必然。

### 系统线程：

系统线程是对CPU资源虚拟化的概念，Windows为每个进程都提供了一个专用的线程（相当于一个CPU）,虽然线程十分的强大，随之带来的就是空间和时间的不可避免的开销。

每个线程都有如下要素：

- 线程内核对象（thread kernel object）

  OS为系统中创建的每个线程都会初始化一个这个数据结构对象。这个数据结构包含一组对线程的描述的属性以及线程上下文。线程上下文就是包含CPU寄存器集合的内存块。对于x86,x64和ARM CPU架构的线程上下文内存分别使用700，1240和350字节的内存。

- 线程环境块（thread enviroment block,TEB）

  TEB是在用户模式中分配和初始化的内存块。TEB消耗一个内存页（x86,x64和ARM CPU中是4kb）。TEB包含线程的异常处理链首（head）。线程进入的每一个try块都在head插入一个节点；线程退出try块时从链表中删除该节点。TEB还包含线程的“线程本地存储”数据，以及GDI和OpenGl图形使用的一些数据结构。

- 用户模式栈（user-mode stack）

  用于存储传给方法的局部变量和实参，它还包含一个地址，这个地址指出当前方法返回时，线程应该从什么地方接着执行。Windows会为每个线程的用户模式分配1mb的内存，具体的说，是保留内存，只有线程实际需要时才会调用内存。

- 内核模式栈（kernel-mode stack）

- DLL线程连接（attach）和线程分离（detach）通知

  任何时候在进程中创建线程，都会调用进程中加载的所有非托管DLL的dllMain方法，并向该方法传递ATTACH的标识。终止时，这些非托管DLL的DLLMain方法也会被调用，并传递DETACH标志，因为有的DLL需要获取这些通知才能为进程中创建或者销毁的每个线程执行特殊的初始化或者资源清理操作。重要的是C#和其他大多数托管编程语言生成的DLL没有DllMain函数，所以托管DLL不会执行这些步骤页自然不会收到这些通知，以提高性能。

Windows任何时刻只将一个线程分配给一个CPU，那个线程能运行一个“时间片”的长度，时间片到期，就必须上下文切换到另一个线程，每次切换上下文都要求Windows执行以下操作。

- 将CPU寄存器的值保存到当前正在运行的线程的内核对象内部的一个上下文结构中
- 从现有线程集合中选出一个线程供调度。如果该线程由另一个进程拥有，Windows在开始执行任何代码或者接触任何数据之前，还必须切换CPU看见的虚拟地址空间。
- 将所选上下文结构中的值加载到CPU的寄存器。

上下文切换完成后，CPU执行所选的线程，直到它的时间片到期。然后再次发生上下文切换。Windwos大约每30毫秒完成一次上下文切换。上下文切换是净开销，并不会到来任何内存或性能的收益，而是为了提供更灵敏的体验。

### 托管线程：

CLR线程则是托管线程，它与系统（Windows）线程是映射关系，CLR团队设想过提供不映射到系统线程的逻辑线程，但是，以失败告终（clr via c# 第4版如此描述），CLR线程可以说是等价于Windows线程。但是，有些东西仍然，可以看到去尝试不映射的痕迹，比如System.Enviroment类公开了CurrentManagedThreadId属性，返回线程的CLRID,而System.Diagnostics.ProcessThread类公开了Id属性，返回同一个线程的Windows ID。

但是，从不同点来说，原生线程是在物理机器上执行的原生代码序列；而托管线程则是在CLR虚拟机上执行的虚拟线程。

正如JIT解释器将“虚拟的”中间（IL）指令映射到物理机器上的原声指令，CLR线程基础架构将“虚拟的”托管线程映射到操作系统的原生线程上。

具体讲解：https://zhuanlan.zhihu.com/p/20838172

### 逻辑线程：

逻辑线程是CLR对线程的抽象实现，我们所编写的代码都是抽象线程，Thread,Task都是逻辑线程，Thread不同的是，在执行时需要交给托管线程执行。并且，Thread需要线程上下文切换，消耗非常大，所以Thread应该避免使用。

#### 前台线程，后台线程

CLR线程要么是前台线程，要么是后台线程，一个进程的所有前台线程都停止运行，那么CLR将强制终止仍在运行的任何后台线程。

以提供对AppDomain更好的支持，每个AppDomain都可以运行一个单独的应用程序。而每一个应用程序都必须有一个前台线程，如果前台线程都终止了，则对应的应用程序就退出了，同样的应用程序的退出，也会终止前台线程。上面两种自然也会终止所有后台进程。

任何时候，线程都可以进行前台和后台的切换。一般来说，应用程序的主线程以及通过Thread显示创建的任何线程默认为前台线程。相反，线程池默认为后台线程。（应避免去创建前台线程）

#### 工作者线程

这个不知道是否有必要独立拿出来说，但是为了下文讲线程池更好理解，我们来记一下。一般线程池里从请求队列拿任务并处理任务所创建的抽象线程我们称它为工作者线程。

## 线程池

### 线程池概念
首先托管线程是基本等价于系统线程的，但系统线程的创建，销毁以及内核模式的上下文切换的高损耗是不利于编程人员去频繁调度的。所以，CLR维护了一个托管线程的集合，在此基础上抽象出线程池的概念，线程池维护和管理着里面的线程，线程池里的线程我们称为工作者线程。

每个CLR一个线程池，这个线程池被当前CLR控制下的所有AppDomain共享。如果一个进程中加载了多个CLR，那么每个CLR都有它自己的线程池。
### 线程池的基本运作过程


CLR初始化时，线程池中没有任何线程。线程池内部会维护一个请求队列。应用程序每执行一个异步操作，就调用某个方法，将一个记录项加到请求队列中。线程池会从这个请求队列按先入先出的顺序拿到请求，并创建一个工作者线程去处理这个任务。并且，处理完后，这个线程并不会销毁，而是回归到线程池变成空闲状态等下一个任务的到来。这样能够避免重复的创建线程和销毁线程。
并且，线程池并不会大量的创建线程，而是用一个线程尽可能的去处理请求队列的任务，只有应用程序异步请求的速度超过了线程池处理它们的速度，才会创建额外的线程。所以，线程池会避免创建过多的线程以节省开销。


但是，如果在一波频繁的请求过后，由于线程池中的线程不会主动的去销毁，而是都以空闲的状态回到线程池等待下一次的调度。这样可能出现大量线程空闲的状态，这样也会浪费性能和资源，在长时间空间的状况下线程会被终结，虽然终结线程也会带来损耗，但是空闲状况下这是无妨的也可以节省内存资源。

线程池，可以只容纳少量线程，来避免浪费资源。也可以容纳很多线程，来充分利用CPU。它能在两种状态之间从容切换。

#### 执行上下文（Excution Context）

当当前执行线程，使用另一个线程（辅助线程）执行任务时就会发生执行上下文的切换。

执行上下文：是一种包含安全设置（压缩栈，Thread的Principal属性和windows身份），宿主设置（参见System.Threading.HostExecutionContextManager）以及逻辑调用上下文数据（System.Runtime.Remoting.Messaging.CallContext的LogicalSetData和LogicGetData方法）。作用是为了确保辅助线程能够使用相同的安全设置和宿主设置以及逻辑数据的数据结构。

所谓切换其实，就是复制一份数据给辅助线程，随着大量的上下文数据的复制，会有效率损耗，某些情况下你可以禁止执行上下文的切换。

System.Threading提供了一个ExecutionContext类，它允许你控制线程的执行上下文如何流向另一个线程，类定义如下：

```c#
public sealed class ExecutionContext:IDisposable,Iserializable
{
    [SecurityCritical]
    public static AsyncFlowControl SuppressFlow();
    public static void RestoreFlow();
    public static Boolean IsFlowSuppressed();
}
```

当你的线程不需要执行上下文时，你可以通过这个类去禁止上下文的流动，在服务器程序上可能有显著提升，示例：

```c#
            //设置数据到逻辑调用上下文中
            CallContext.LogicalSetData("name", "Fran");

            //向线程池插入一个工作项，并将逻辑上下文中的数据传过去
            //并成功传递数据
            ThreadPool.QueueUserWorkItem(state => { 
                Console.WriteLine("Name={0}", CallContext.GetData("name")); });

            //禁止执行上下文的流动
            ExecutionContext.SuppressFlow();
            //查看当前是否允许上下文流动
            Console.WriteLine(ExecutionContext.IsFlowSuppressed());

            //这个时候获取不到name的值
            ThreadPool.QueueUserWorkItem(state => { 
                Console.WriteLine("Name={0}", CallContext.GetData("name")); });

            //恢复上下文的流动
            ExecutionContext.RestoreFlow();
```



### QueueUserWorkItem

#### 介绍

QueueUserWorkItem是ThredPool的一个静态方法，定义如下：

```c#
static Boolean QueueUserWorkItem(WaitCallback callback);
static Boolean QueueUserWorkItem(WaitCallback callback,Object state)
```

示例：

```c#
......
ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);
.......
    
public static void ComputeBoundOp(Object state) 
        {
            Console.WriteLine(state);
            Thread.Sleep(1000);
        }
```

它会往线程池的队列插一个工作项，参数如上主要是回调函数。然后，所有方法会直接返回，相比其他的方法消耗低更简易。你无法捕捉他的结果或允许状态，也无法让他按你的设想的方式或顺序运行，所以这个时候就会提到我们后面要说到的Task。

#### 协作式取消和超时

##### 介绍

想要取消操作都必须依靠System.Threading.Cancellation.TokenSource对象，简单看下如下：

```c#
public sealed class CancellationTokenSource:IDisposable
{
    public CancellationTokenSource();
    public void Dispose();
    
    public Boolean IsCancellationRequested{get;}
    public CancellationToken Token{get;}
    
    public void Cancel();
    public void Cancel(Boolean throwOnFirstException);
    ...
}
```

主要还是要依赖Token属性，CancellationToken是一个轻量的值类型,如下：

```c#
public struct CancellationToken
{
    public static CancellationToken None{get;}  //指定此值得代表不能被取消
    
    public Boolean IsCancellationRequested{get;}; //Task调用的操作中作为判断标志，cts的Cancel方法触发
    public void ThrowIfCancellationRequested(); //由Task调用的操作调用
    
    //CancellationTokenSource取消时，WaitHandle会收到信号
    public WaitHandle{get;}
    
    public Boolean CanBeCanceled{get;} //很少使用
    
    //Registers a delegate that will be called when this System.Threading.CancellationToken
    //is canceled.
    public CancellationTokenReqistration Register(Action<Object> callback,Object state,Boolean useSynchronizationContext);   //还有很多重载方法
}
```

##### 通过调用IsCancellationRequester属性来取消

```c#
public static void Count(CancellationToken token,int n)
{
    for(;n>0;n--)
    {
        if(token.IsCancellationRequested)
        {
            return;
        }
    }
}
...
CancellationTokenSource cts=new CancellationTokenSource();
ThreadPool.QueueUserWorkItem(()=>Count(cts.Token,10000));
cts.Cancel();
```

##### 通过传递Token的属性来禁止取消

如果传给操作的CancellationToken的值是None，则IsCancellationRequested一直返回false,CanBeCanceled属性也为false(一般这个都是true代表可以取消)。

##### 通过Register方法登记

通过Register方法注册一个或多个委托（多个就是多次注册），当CancellationToken被取消时，委托会被调用。

```c#
public CancellationTokenRegistration Register(Action callback);
public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext);
public CancellationTokenRegistration Register(Action<object> callback, object state);
public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext);
```

重载大概就以上这几种，重要的参数一个就是一个委托方法，一个useSynchronizationContext 布尔值得参数，以及state数据。callback以及state就不赘述了，主要是useSynchronizationContext ，代表是否调用线程的SynchronizationContext (同步上下文)。如果为false则Cancel会顺序（由于线程池的队列是个先入后出的，所以这里应该是倒序）调用登记的所有方法。如果为true，则回调方法会send给同步上下文对象，由上下文决定哪个线程去调用。

```C#
var cts=new CancellationTokenSource();
cts.Token.Register(()=>Console.WriteLine("Canceled 1"));
cts.Token.Register(()=>Console.WriteLine("Canceled 2"));
cts.Cancel();

得到结果：
Canceled 2
Canceled 1
//如上面所说，是倒序的
```

##### Register中的异常问题

如果多个回调中的某一个方法或者多个方法出现异常，CancellationTokenSource 的Cancel方法有一个带bool值得重载方法，如果传递了true,则抛出异常并阻止后面的回调执行。如果为false，或者选择不传，那么回调方法会一直执行，并将所有的异常添加到一个集合，在Cancel的方法栈上抛出一个AggregateEception的方法，如果想看具体的异常需要从集合属性InnerExceptions去查看，注意不是InnerException,后面这个异常类还会经常看到，我们需要掌握怎么处理它。

### Task

#### 介绍

使用ThreadPool.QueueUserWorkItem执行异步操作，非常简单。大事，它没有内建机制让你知道它什么时候完成，也无法传递结果值。所以，CLR提供了System.Tasks命名空间下的Task类型来构建任务的概念。

#### 使用Task开启一个异步任务

我们可以new一个对象去调用start方法开启一个任务，也可以直接调用静态方法Run去开启，如下：

```c#
new Task(ComputeBoundOp,5).Start();
Task.Run(()=>ComputeBoundOp(5));   //只是Task.Factory.StartNew的快捷方式
Task.Factory.StartNew(()=>ComputeBoundOp(5));
```

#### TaskCreationOptions：控制Task执行方式

通过往Task构造函数传递TaskCreationOptions可以约束Task的行为，TaskCreationOptions定义如下：

```c#
[Flags,Serizlizable]
publi enum TaskCreationOptions
{
    None = 0x0000, //默认
    //提议TaskScheduler尽快执行该任务
    PreferFairness = 0x0001,
    //提议TaskScheduler应尽可能的创建线程池线程
    LongRunning = 0x0002,
    //该提议总是被采纳：将一个Task和它的父Task关联
    AttachedToParent = 0x0004,
    //该提议总是被采纳：如果一个任务试图和这个任务进行父任务连接，那个任务不会是子任务而是普通任务
    DenyChildAttach = 0x0008,
    //该提议总是被采纳：强迫子任务使用默认调度器而不是父任务的调度器
    HideScheduler = 0x0010
}
```

有的只是提议，并不一定会被采纳，有的则一定会采纳。

#### 获取Task的结果

Task的结果一般通过Task的泛型派生类Task<TResult>，TResult用来传递操作的返回值类型，通过Result属性来获取返回值，如下：

```c#
private static int Sum(int n)
{
    int sum=0;
    for(;n>0;n--)
        checked{
            sum+=n;
        }
    return sum;
}
...
Task<int> task=new Task<int>(n=>Sum((int)n),10000);
//启动任务
Task.Start();
//显示等待,会阻塞线程最好不要使用
Task.Wait()； //可以接受timeout和CancellationToken作为参数重载
//获取结果
Console.WriteLine("This Sum is:"+task.Result);
...
```



#### Task的取消

取消同样都必须经过CancellationTokenSource对象的Token属性去完成,使用起来和QueueUserWorkItem类似，如下：

通过ThrowIfCancellationRequestd取消：

```c# 
private static int Sum(int n,CancellationToken token)
{
    int sum=0;
    for(;n>0;n--)
    {
    	//外面调用Cancel方法，如果该操作还在执行就会抛出这个异常
    	token.ThrowIfCancellationRequestd();
        checked{  sum+=n;}
    } 
    return sum;
}
...
CancellationTokenSource cts=new CancellationTokenSource();
Task<int> task=Task.Run(()=>Sum(cts.Token,10000),cts.Token);
cts.Cancel();
try{
    //Task的异常通常只会在调用Wait方法和Result属性才会从内部抛出一个AggregateException的异常
    Console.WriteLine("The Sum is:"+task.Result);
}
catch(AggregateException x)
{
    //将OperationCanceledException异常视为已处理
    x.Handle(e=>e is OperationCanceledException);
    Console.WriteLine("Sum was Canceled");
}
```

通过IsCancellationRequested取消：

```c#
   static int TaskMethodWithCancel(string name,CancellationToken token,int second)
    {
        for(int i=1;i<=100;i++)
        {
            var msg= i%100;
            Console.WriteLine($"任务执行进度:{msg} %");
            Thread.Sleep(TimeSpan.FromSeconds(0.1));
            //当IsCancellationRequested被改变为true时则跳出当前操作
            if(token.IsCancellationRequested)
                return -1;
        }
        return TaskMethod(name,second);
    }

    public static void TaskCancelTest()
    {
        var cts =new CancellationTokenSource();
        var longTask=new Task<int>(()=>TaskMethodWithCancel("Task1",cts.Token,10),cts.Token);
        
        Console.WriteLine(longTask.Status);
        Thread.Sleep(TimeSpan.FromSeconds(0.5));
        longTask.Start();
        Thread.Sleep(TimeSpan.FromSeconds(9));
        //CancellationTokenSource的Cancel方法改变IsCancellationRequested的值，从而从操作中跳出
        cts.Cancel();
        Thread.Sleep(TimeSpan.FromSeconds(0.5));
        Console.WriteLine(longTask.Status);
    } 
```

在Task的构造函数中传递CancellationTokenSource，从而使两者关联。无论是new一个，还是使用Run都是将其绑定起来。虽然，Task关联了CancellationTokenSource对象，但是却没有办法访问它，所以一般是将cts.Token作为闭包变量传递给lambda表达式，如上面两个示例一样。

如果，任务还未开始就取消了，那么Task无法继续执行并且会抛出一个InvalidOperationException。

#### 等待Task的完成

#### Task完成时启动新任务

前面说过，Wait方法会阻塞线程。通常，我们无法预料Task任务的执行顺序，但是有时候，我们又明确希望任务2在任务1执行完之后才执行，或者说我们想查询Task的Result属性，但是我们却无法保证不用Wait查询的时候Task指定的任务已经执行完成了，这时候我们无法获取正确的结果，还会浪费资源去获取结果。

以上，为了解决上述问题，我们可以通过ContinuWith来在指定任务完成后去进行某些操作。

```c#
private static int Sum(int n)
{
    int sum=0;
    for(;n>0;n--)
        checked{
            sum+=n;
        }
    return sum;
}

new Task<int>(n=>Sum((int)n),10000).Start().ContinueWith(task=>Console.WriteLine("The Sum is:"+task.Result));
```

#### Task开启子任务

任务支持父子关系，如下：

```c#
Task<int[]> parent =new Task<int[]>(()=>{
    var result=new int[];
    new Task(()=>result[0]=Sum(10000),TaskCreationOption.AttachedToParent).Start();
    new Task(()=>result[0]=Sum(20000),TaskCreationOption.AttachedToParent).Start();
    new Task(()=>result[0]=Sum(30000),TaskCreationOption.AttachedToParent).Start();
    return result;
});

var cwt=parent.ContinueWith(parent=>Array.ForEach(parent.Result,Console.WriteLine));
parent.Start();
```

虽然里面的三个任务是parent内部创建的，但是都会被认为是顶级任务，与谁创建的无关。而当被创建任务被赋值TaskCreationOption.AttachedToParent将此任务与创建它们的任务关联，这个时候，只有所有子任务结束的时候，父任务才被认为是已经结束。

#### Task异常处理

假如Task调用一个或多个方法，或开启子任务如果这些方法抛出异常，它会被吞噬，你无法知道是哪一个方法报错了，直到Task调用Result属性或者Wait方法时，会抛出一个System.AggregateException对象。

AggregateException内部会有一个集合装着子任务或多个方法抛出的未处理的异常。我们可以通过InnerExceptions属性来访问这些异常，注意是InnerExceptions不是InnerException，它是一个ReadOnlyCollection<Exception>对象。

我们可以通过AggregateException的GetBaseException方法找到问题根源最内部的AggregateException（最内层的异常）。

还提供了一个Flatten方法，它会在原基础上建立一个新的AggregateException，这个AggregateException的就是遍历外层直到最内层的AggregateException得到的，我们可以通过InnerExceptions去访问具体的异常。

AggregateException还提供了一个Handle方法来处理异常，Handle会为每个内部的异常提供一个回调方法，通过这个回调方法来决定如何处理异常，如果回调方法返回true则表示处理了异常，反之则为未处理，如果未处理则抛出新的AggregateException异常。

如果一直不调用调用Result属性或者Wait方法，代码就注意不到异常的发生。

### Parallel

#### 静态For,ForEach和Invoke方法

一些场景下使用任务可以提升性能，静态类Parallel内部封装了Task,通过这个类可以简化我们的代码。

##### For方法

```c#
Paraller.For(0,1000,i=>DoWork(i));
```

##### ForEach方法

```c#
Parallel.ForEach(collection,item=>DoWork(item))
```

##### Invoke方法

```c#
Parallel.Invoke(
	()=>Method1(),
	()=>Method2(),
	()=>Method3()
);
```

我们说过，通过Parallel类的方法可以提高性能，但是并不是需要我们去替换所有的代码中的可以使用Parallel的地方。比如，如果要求工作项顺序执行或者修改共同的数据，这些Parallel都无法保证，如果盲目的使用可能会得到错误的结果。我们可以用线程同步锁来解决这个问题，但是线程同步以后，就只有一个线程来操作，无法获得Paraller带来的好处。

另外，Parallel本身也有开销，委托对象必须分配内存，如果只是少量的简单的任务，使用这个得不偿失。但如果是大量的任务，或者每一项任务工作量巨大，则使用这些方法可以获得性能的提升。

### TaskScheduler&TaskFactory&Task的简单剖析

#### Task的内部构造

首先我们来记住一个结论，Task的执行都是要依托于TaskScheduler。



#### TaskScheduler任务调度器

Task运行十分灵活，而它的灵活则依托于TaskScheduler，FCL默认提供了两个TaskScheduler派生类,一个是线程池任务调度器，一个是同步上下文任务调度器。

默认情况下，所有应用程序都使用线程池任务调度器，而同步上下文任务调度器适用于图形界面。

同时，Microsofft的ParallelExtensionExtra包中提供了其他的和任务有关的示例代码。

我们可以看看TaskScheduler的结构

```c#
[DebuggerDisplay("Id={Id}")]
    [DebuggerTypeProxy(typeof(SystemThreadingTasks_TaskSchedulerDebugView))]
    public abstract class TaskScheduler
    {
        protected TaskScheduler();

        ~TaskScheduler();
        //默认的 System.Threading.Tasks.TaskScheduler 实例
        public static TaskScheduler Default { get; }
        //当前正在执行的任务关联的 System.Threading.Tasks.TaskScheduler
        public static TaskScheduler Current { get; }
        //最大并发级别的一个整数
        public virtual int MaximumConcurrencyLevel { get; }
        //System.Threading.Tasks.TaskScheduler 的唯一 ID
        public int Id { get; }
	   //当出错的Task 的未观察到的异常将要触发异常升级策略时发生，默认情况下，这将终止进程
        private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;
        public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
        {
            [System.Security.SecurityCritical]
            add
            {
                if (value != null)
                {
#if !PFX_LEGACY_3_5
                    RuntimeHelpers.PrepareContractedDelegate(value);
#endif
                    lock (_unobservedTaskExceptionLockObject) _unobservedTaskException += value;
                }
            }
 
            [System.Security.SecurityCritical]
            remove
            {
                lock (_unobservedTaskExceptionLockObject) _unobservedTaskException -= value;
            }
        }

        //创建一个与当前同步上下文关联的TaskScheduler
        public static TaskScheduler FromCurrentSynchronizationContext();
        //生成当前排队到计划程序中等待执行的 System.Threading.Tasks.Task 实例的枚举
        [SecurityCritical]
        protected abstract IEnumerable<Task> GetScheduledTasks();
        [SecurityCritical]
        //执行Task
        [SecurityCritical]
        protected bool TryExecuteTask(Task task)
        {
            if (task.ExecutingTaskScheduler != this)
            {
                throw new InvalidOperationException(
                    Environment.GetResourceString("TaskScheduler_ExecuteTask_WrongTaskScheduler"));
            }
            return task.ExecuteEntry(true);
        }

        [SecurityCritical]
        protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);
        [SecurityCritical]
        protected internal abstract void QueueTask(Task task);
        [SecurityCritical]
        protected internal virtual bool TryDequeue(Task task);
    }
```



### 线程调度过程

## 异步

