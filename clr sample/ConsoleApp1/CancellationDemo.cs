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
            CancellationTokenSource cts = new CancellationTokenSource();

            //将CancellationTokenSource和数的数传入操作
            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));

            Console.WriteLine("Press<Enter> to cancel the operation");
            Console.ReadLine();
            cts.Cancel();
        }

        private static void Count(CancellationToken token, int count)
        {
            for (int i = 0; i < count + 1; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("收到结束指令，开始结束任务");
                    break;
                }

                Console.WriteLine(i);
                Thread.Sleep(200);
            }
            Console.WriteLine("数完了");
        }
    }
}
