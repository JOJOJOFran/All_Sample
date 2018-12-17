using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    internal static class CancellationDemo
    {
        public static void Go()
        {
            Console.WriteLine("当前Main线程的ID是：{0}", Thread.CurrentThread.ManagedThreadId);
            CancellationTokenSource cts = new CancellationTokenSource();

            
            //将CancellationTokenSource和数的数传入操作
            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));

            Console.WriteLine("Press<Enter> to cancel the operation,如果不按，会在十秒后自己结束");
            cts.CancelAfter(10000);
            Console.ReadLine();
            //参数：throwOnFirstException
            //true if exceptions should immediately propagate; otherwise, false.
            //意思是说，如果设为true，注册的回调方法抛出异常了，后面的回调方法就不会执行，并且异常从Cancel处抛出
            cts.Cancel();
        }


        //CancellationToken登记方法
        private static void WaitCallbackRegister(CancellationTokenSource cts)
        {
            //登记的方法，类似于存储到方法栈中，所以是先进后出
            //参数state=》传递给回调方法的参数
            //参数useSynchronizationContext=》指明是否要使用调用线程的SynchronizationContext
            cts.Token.Register((state) => Console.WriteLine("收到结束指令，线程回收完毕,工作编号{0}", state), "10号", false);
            cts.Token.Register(() => Thread.Sleep(1000), false);
            cts.Token.Register(() => Console.WriteLine("收到结束指令，线程回收中。。。。"), false);
            cts.Token.Register(() => Console.WriteLine("收到结束指令，开始结束任务,当前线程ID是：{0}", Thread.CurrentThread.ManagedThreadId), false);
        }

        private static void Count(CancellationToken token, int count)
        {
            Console.WriteLine("当前Count线程的ID是：{0}", Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < count + 1; i++)
            {
                if (token.IsCancellationRequested)                  
                    break;

                Console.WriteLine(i);
                Thread.Sleep(200);
            }
            Console.WriteLine("数完了");
        }
    }
}
