using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadAndAsyncDemo
{
    public static class AsyncTaskTest
    {
        public static void TaskTest()
        {
            Console.WriteLine($"主线程ID为{Thread.CurrentThread.ManagedThreadId}");
            Task task1 = new Task(() => { Console.WriteLine($"Task1将在十秒后执行完成，Task1线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task2 = new Task(() => { Console.WriteLine($"Task2将在九秒后执行完成，Task2线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task3 = new Task(() => { Console.WriteLine($"Task3将在八秒后执行完成，Task3线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task4 = new Task(() => { Console.WriteLine($"Task4将在七秒后执行完成，Task4线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task5 = new Task(() => { Console.WriteLine($"Task5将在六秒后执行完成，Task5线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task6 = new Task(() => { Console.WriteLine($"Task6将在五秒后执行完成，Task6线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task7 = new Task(() => { Console.WriteLine($"Task7将在四秒后执行完成，Task7线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task8 = new Task(() => { Console.WriteLine($"Task8将在三秒后执行完成，Task8线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            Task task9 = new Task(() => { Console.WriteLine($"Task9将在二秒后执行完成，Task9线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
            task1.Wait(10000);
            task1.Start();
            task2.Wait(9000);
            task2.Start();
            task3.Wait(8000);
            task3.Start();
            task4.Wait(7000);
            task4.Start();
            task5.Wait(6000);
            task5.Start();
            task6.Wait(5000);
            task6.Start();
            task7.Wait(4000);
            task7.Start();
            task8.Wait(3000);
            task8.Start();
            task9.Wait(2000);
            task9.Start();
            Task.Run(() => { Console.WriteLine($"Task10执行完成，Task10线程ID为{Thread.CurrentThread.ManagedThreadId}"); });
        }
    }
}
