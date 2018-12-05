using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("根据编号选择方法进行执行,请输入对应编号");
            TitleSet();
            FunctionProvider(Console.ReadLine());
            Console.ReadKey();
        }

        static void TitleSet()
        {
            Console.WriteLine("1.执行上下文测试代码");
            Console.WriteLine("2.执行取消异步操作代码");
        }

        static void FunctionProvider(string type)
        {
            switch (type)
            {
                case "1":
                    ExecutionContextTest();
                    break;
                case "2":
                    CancellationDemo.Go();
                    break;
            }
        }

        /// <summary>
        /// 执行上下文测试代码
        /// </summary>
        static void ExecutionContextTest()
        {
            //将一些数据放到主线程的逻辑调用上下文中
            CallContext.SetData("Name", "Fran");

            //初始化要由一个线程池线程做的一些工作
            //线程池线程能访问逻辑调用上下文数据
            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("Name={0}", CallContext.GetData("Name")));
            Thread.Sleep(1000);
            //阻止线程上下文流动
            ExecutionContext.SuppressFlow();
            Console.WriteLine("现在我们阻止线程上下文流动，ExecutionContext的IsFlowSupper值为:{0}", ExecutionContext.IsFlowSuppressed());
            CallContext.SetData("Age", "25");
            ThreadPool.QueueUserWorkItem(ApartmentState => Console.WriteLine("Age={0}", CallContext.GetData("Age") ?? "?"));
            Thread.Sleep(1000);
            //恢复线程上下文流动
            ExecutionContext.RestoreFlow();
            Console.WriteLine("现在我们恢复线程上下文流动，ExecutionContext的IsFlowSupper值为:{0}", ExecutionContext.IsFlowSuppressed());
            ThreadPool.QueueUserWorkItem(ApartmentState => Console.WriteLine("Age={0}", CallContext.GetData("Age") ?? "?"));


        }

        

       
    }

     
}
