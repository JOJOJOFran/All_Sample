[TOC]

#Dotnet的多线程和异步

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



### Task

### Parallel

### TaskScheduler

### 线程调度过程

## 异步

