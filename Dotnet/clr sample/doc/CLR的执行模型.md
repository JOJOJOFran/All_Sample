##CLR的执行模型

###什么是CLR

英文是Common language Runtime,翻译过来就是公共语言运行时，可以理解为多种语言共同使用的运行时.

运行时的主要作用（核心功能）：内存管理，程序集加载，安全性，异常处理和线程同步）

### 编译步骤如下顺序走下去：

- 源码文件：C#,Basic.....

- 编辑器：语法检查器和正确代码分析器，确定你写的一切都有意义，并输出对你的意图的进行描述的中间语言代码。

- 托管模块：托管模块是标准的32位MS Windows 可移植执行体（PE32）文件

  ​      托管程序集会利用两个功能增强整个系统的安全性，一个是DEP(Data Excecution Prevention)数据执行保护，一个是ASLR(Address Space Layout Randomization,ASLR)地址空间布局随机化。

#### 托管模块组成

- PE32或PE32+头
- CLR头
- 元数据
- IL代码（中间语言）

