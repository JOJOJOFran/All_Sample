using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadDemo
{
    public static class TaskDemo
    {
        public static void Go()
        {
            Task.Run(() => { Console.WriteLine("开启一个新线程,线程ID为：{0}", Thread.CurrentThread.ManagedThreadId); });
            
            Console.WriteLine("==等待任务完成并获取结果==");

            Task<int> task = new Task<int>(n => Sum((int)n), 10000000);
            task.Start();
            task.Wait();
            Console.WriteLine("The Sum is:" + task.Result);
        }

        private static int  Sum(int n)
        {
            int i = 0;
            while (i < n)
            {
                i++;
                Thread.Sleep(2);
            }
            return i*2;
        }

        private static async Task<int> Sum1(int n)
        {
            await Task.Run(() => { Sum(n);});
            int i = 0;

            return i * 2;
        }
    }
}
