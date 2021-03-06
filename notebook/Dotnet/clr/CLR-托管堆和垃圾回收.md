[TOC]

# CLR-托管堆和垃圾回收

## 概要

本文不会非常细节的去研究每一个步骤，主要是对CLR托管堆垃圾回收的主要思想的理解和巩固，详细一点的笔记可以看我记录的[托管堆和垃圾回收](https://d.docs.live.net/8227dd73d1d5e82a/文档/DONET%20CORE/NetCore/CLR%20CSharp笔记.one#托管堆和垃圾回收&section-id={055322B0-538C-402D-8F08-F739A405E908}&page-id={F6FB4D2C-7CAD-4B18-AE2C-DEE632B8A350}&end)  ([Web 视图](https://onedrive.live.com/view.aspx?resid=8227DD73D1D5E82A%2113333&id=documents&wd=target%28NetCore%2FCLR%20CSharp%E7%AC%94%E8%AE%B0.one%7C055322B0-538C-402D-8F08-F739A405E908%2F%E6%89%98%E7%AE%A1%E5%A0%86%E5%92%8C%E5%9E%83%E5%9C%BE%E5%9B%9E%E6%94%B6%7CF6FB4D2C-7CAD-4B18-AE2C-DEE632B8A350%2F%29)) 或者直接阅读CLR Via C#。

## 托管堆

### 什么是托管堆

#### 定义：

初始化新的进程时，CLR会为进程维护一个连续的地址区域空间。这个保留的地址空间被成为托管堆。

#### 作用：

托管堆会维护一个指针，这个指针指向将在堆中分配的下一个对象的地址，最初这个指针指向托管堆的基址，托管堆的作用自然就是维护堆上的内存对象。

##### 托管资源：

我们把针对CLR开发的应用程序的代码叫做托管代码，而托管代码中所有的实例和数据对象都会生成在托管堆上（当然还有非托管资源是不在堆上生成），这样的我们可以理解为托管资源。

##### 非托管资源：

CLR中值类型这样的轻量级类型，一般直接维护在线程栈上，这样的资源我们成为非托管资源，当然，一些如文件操作句柄的这些本机资源，也不会在托管堆上直接进行清理。

#### 托管堆分配对象过程：
    CLR划出一个地址空间区域，并维护一个指针，称作NextObjPtr.该指针指向下一个对象在堆中分配的位置，刚开始的时候，是指向地址空间区域的基地址。
    一个区域被非垃圾对象填满后，CLR会分配更多的区域。这个过程会一直重复，直到地址空间都被填满了。所以你的应用程序收进程的虚拟地址空间限制。32位的进程最多能分配1.5GB。64位最多能分配8T。
- 1.调用IL指令newObj,代表着为类型分配内存（如：c#中的new操作）
- 2.初始化内存，设置资源的初始状态并使资源可用。类型的实例构造器负责设置初始化状态。
- 3.访问类的成员来使用资源（可以重复访问）
- 4.破坏资源的状态以表示可以清理（标记托管资源可以回收了）
- 5.释放内存（垃圾回收器完成这一步）

## 垃圾回收

### CLR垃圾回收算法

引用跟踪法：对需要回收的引用对象的变量进行标记并压缩，引用跟踪法主要就是针对根进行处理，因为只有变量才能引用堆上的对象，当堆上对象映射的所有的变量都不在使用，才会认为该对象资源可以不再需要。

### 基础概念

#### 根：

我们把引用类型的变量，字段等统称为根。

#### 不可达对象（unreachable）：

一旦根离开作用域，它引用的对象就会变得不可达。

#### 可达对象(reachable)：

相反，作用域内的根引用的对象是可达的

#### 代：

垃圾回收是按代进行回收的，CLR将托管堆上的资源共分为3代，每一代都会分配一定的预算空间。

- 共分为0，1，2三代
- 代越新越早被GC
- 按代划分，减少GC检查的部分和压缩的部分，提高性能

##### 代的划分

- 0代：托管堆初始化时，不包含任何对象，进入托管堆的任何对象都成为0代。包括每次GC后，预算内存内分配的对象是新的0代。
- 1代：进行0代GC后，幸存下的对象成为1代
- 2代：进行1代GC后，幸存下的对象成为2代

当0代的预算空间满了，就会对0代进行GC。0代幸存的对象就会晋升为1代，0代就会被清空，新进入的对象就算作0代，而下一次进行GC，也是优先对0代进行。那么什么时候对1代进行GC呢？当0代剩余对象晋升为1代时，造成了1代内存空间超出了预算空间，此时，就会对1代进行回收，也就是说可能进行多次回收后，才有一定的可能性对1代进行回收。同理，2代自然是1代对象幸存晋升的，也是在超出预算空间后进行GC。

##### 大对象

CLR根据对象所占的内存大小，分为大对象和小对象，目前认为大于等于85000字节的都是大对象。CLR对待大对象与小对象相比有如下区别：

- 大对象分配在进程地址空间（每个进程都会有一个独立的进程地址空间，并且都为虚拟地址）
- GC不会对大对象进行压缩，因为它们占用内存太大，移动他们代价过大，所以进程中的大对象之间可能存在碎片化的内存空间，甚至导致抛OutOfMemoryException。CLR将来的版本可能会考虑压缩大对象。
- 大对象总是存在于第2代，绝不可能存在于第1代或者第0代，所以，如果不是需要长期保存的的对象，就不应该创建为大对象。因为，大对象的存在可能导致第2代被频繁回收并损害性能，毕竟占用的空间大，容易触发GC。

大对象，一般是大字符串(比如XML或JSON)或者用于I/O操作的字节数组（比如从文件或网络将字节读入缓冲区），很大程度上可以忽略它们。仅在出现解释不了的情况时（比如地址空间碎片化）才对它进行特殊处理。

##### 垃圾回收触发条件：

- 如上面所说，内存空间超过代的预算空间时，进行GC,这也是最常见的。
- 代码显示调用System.GC的静态方法Collect方法，一般都强烈建议避免你去使用它。
- Windows报告内存低，CLR内部使用Win32函数CreateMemoryResourceNotification和QueryMemoryResourceNotification方法监视系统的总体内存情况。如果Windows报告内存低，CLR将强制进行垃圾回收以释放死对象，来释放内存空间。
- CLR正在卸载AppDomain,AppDomain卸载时，内部的一切对象都将不被视为根，CLR会对所有代都进行GC。
- CLR正在关闭，CLR关闭期间，进程中一切对象都不是根。但是CLR不会视图压缩或者清理对象。因为整个进程都要终止了，自然Windows会回收进程的全部内存。

##### 垃圾回收模式：

CLR启动时，会选择GC的模式，一般进程终止前模式是不会改变的，当然也有手段对它进行切换。

分类：服务器模式，工作站模式

子分类：并发模式（默认），非并发

###### 工作站模式：

应用程序一般默认是工作站模式，并且如果你寄宿的服务器只是单核处理器，将也只能以工作站模式运行了

适用场景：针对客户端应用程序优化GC

特点：GC延时低，线程挂起时间短，并且会假定其他应用程序不会占用太多CPU资源

###### 服务器模式：

应用程序可以将模式切换为服务器模式，当然这要求服务器不能是单核

适用场景：多核服务器端优化GC

特点：优化吞吐量和资源利用，并且会假定没有其它应用程序运行，也会假设所有的CPU资源都能用来协作GC。该模式会导致托管堆分为几个区域，每个CPU一个。开始垃圾回收时，垃圾回收器会在每一个Cpu下运行一个特殊的线程，每个线程都和其它线程并发回收自己区域的垃圾。所以需要在多核机器上运行，以保证线程能真正的同时工作，从而获得性能的提升。

也可以为独立应用程序创建一个可配置文件告诉CLR使用服务区回收器，配置文件注意为应用添加一个gcServer元素。下面是一个示例：

```xml
<configuration>
	<runtime>
		<gcServer enabled="true"/>
	</runtime>
</configuration>
```

应用程序运行时，可查询GCSettings类的只读Boolean属性IsServerGC来询问CLR,是否在服务器模式下运行 。

###### 并发模式：

并发方式时，垃圾回收器有一个额外的后台线程，它能在应用程序运行时并发标记对象。

应用程序运行时，垃圾回收器会在后台运行一个普通优先级的后台线程来查找不可达对象，找到之后，GC的时候，仍然会挂起所有线程，判断是否要进行垃圾回收。如果决定压缩，内存会被压缩并回收，根引用也会被修正，然后应用程序恢复运行。因为，提前标记了对象，所以GC时间会比非并发模式花费的少。事实上，如果可用内存很多，垃圾回收器不会轻易压缩堆，这样能够减少压缩带来的开销，提升性能。

当然，由于并发模式需要额外的线程去维护，它消耗的内存资源自然比非并发模式要多。

可通过如下配置告诉CLR不使用并发模式

```xml
<configuration>
	<runtime>
		<gcConcurrent enabled="false"/>
	</runtime>
</configuration>
```

##### 更改模式：

GC模式是针对进程配置的，进程运行期间不能更改。但是，你的应用程序可以使用GCSettings类的GCLatencyMode属性对垃圾回收进行某种程度的控制。这些属性可以设为GCLatencyMode枚举类型中的任何值。 

| Batch（服务器GC模式默认值）       | 关闭并发GC                                                   |
| --------------------------------- | ------------------------------------------------------------ |
| Interactive（工作站GC模式模式值） | 打开并发GC                                                   |
| LowLatency                        | 在短期的，时间敏感的操作中（比如动画绘制）使用这个延迟模式。这些操作不适合对第二代进行回收 |
| SustainedLowLatency               | 使用这个延迟模式，应用程序大多数操作都不会发生长的GC暂停。只要有足够内存，它将禁止所有会阻塞的第2代回收动作。事实上，这种应用程序应该考虑安装更多RAM来防止发生长的GC暂停。 |

LowLatency。一般用它执行一次一次短期的，时间敏感的操作，再将模式设回普通的Batch或Interactive。Interactive会极力避免任何第二代的2代回收，因为那样会花费的时间较多。当然GC.Collect仍会强制回收第二代。但是，LowLatency（低延迟）模式，更容易抛出OutOfMemoryException的机率大一些。所以处于该模式的时间应尽量端，也应避免分配太多对象，避免分配大对象，并用一个约束执行区（CER）（try  fianlly块）将模式切换回去。 

##### 强制垃圾回收

```c#
System.GC.MaxGeneration //查询托管堆支持的最大代数，该属性总是返回2
System.GC.Generation  //最多回收第几代
System.GC.Mode  //回收模式
System.GC.Blocking //是否阻塞
System.GC.Collect() //强制垃圾回收
```

大多数时候要避免调用任何Collect方法，最好让GC自己决定。 

### 托管堆资源回收

让我们针对托管堆上的资源回收，把上面的概念串起来整个过程去理解。

我们上面说了，CLR采用的是引用跟踪法，而Microsoft的COM使用的是引用计数法。

我们先简单说一下，引用计数法，它会把堆上内存被使用的地方进行计数，每当这个对象的一个引用运行到了不可达的阶段，计数就减1 ，直到计数变为0，该对象从内存中删除，但是有些循环引用的地方就不能很好的处理了，所以CLR采取了引用跟踪法，下面我们来讲引用跟踪法。

引用跟踪法的核心在于，它只关系引用类型的变量，因为只有变量才能使用堆上的对象（值类型变量直接包含值类型的实例在线程栈上）。这些变量我们都称为根。

然后，现在假设，内存堆上什么都没有，然后往里面分别插入根A,B,C,D,E,F这些都是0代。

当插入G的时候，0代的预算空间满了或者超出了，这时候就会触发GC的发生。

#### 标记过程

任何根引用了堆上的对象，CLR都会标记那个对象，也就是把该对象的同步块索引中的位设为1。一个对象被标记后，CLR会检查那个对象的根，标记它们引用的对象，如果已经标记过就不再进行重复标记，已避免循环引用造成死循环。

直至应用程序中所有的根都检查完毕，再次之后，堆中被标记的对象，表示有根引用，则是可达对象，不能够被垃圾回收。未标记的对象则是不可达对象。我们假设A,C,F被标记了，也就是可达对象不可回收，而B,D,E,G是不可达对象。

#### 压缩过程：

我们说过，并发模式在可用内存非常富余的情况下可能选择不压缩直接回收。但是我们这里来讨论一下，GC是如何回收的，首先，不可达对象不一定是连续的，而堆上是一片连续的内存空间，如果直接移除不可达对象，那么势必会造成内存的浪费和碎片化，如果可用内存不够多，那么压缩内存空间就变得十分必要。

压缩则是指，删除需要删除的对象，然后移动幸存的对象，让它们形成连续的内存（它们此时晋升为1代），并将NextObjPtr指针进行相对的偏移，以指示新的对象插入堆的位置。但是，注意那些幸存对象的根的引用地址会与现在对象的内存地址对不上，所以，还需要把还在使用的根的引用地址进行相对的偏移，以保证能够对应上。

通过压缩解决了堆空间碎片化的问题，也避免了内存的浪费，当然，会有一定的性能损耗，但是也是非常有必要的。

#### 代的回收

上面的过程以后，A,C,F已经成了1代对象，假设，后面又新插入了L,M,N对象，这些对象将是新的0代。而如果对象O插入导致0代预算又满了，那么GC还是会进行0代的标记和垃圾回收（官方测试，对0代进行垃圾回收，耗时不超过1毫秒）。

因为CLR认为，0代产生垃圾的概率更大，这样可以更有效的进行垃圾回收，也不用重复去检查对象，以提高性能和效率。所以说越老的对象活得越长。

那么，1代和2代的回收，都是会在代超过了CLR分配的预算空间进行GC，这个上面讲代的时候也已经提过了。

### 非托管堆资源回收

