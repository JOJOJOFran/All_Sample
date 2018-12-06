using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MultiThreadDemo
{
    public static class ThreadContextDemo
    {
        /// <summary>
        /// 执行上下文测试代码
        /// </summary>
        public static void ExecutionContextTest()
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
