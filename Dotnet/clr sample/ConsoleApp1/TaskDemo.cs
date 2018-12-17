using System;
using System.Collections.Generic;
using System.Linq;
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

            Task<int> task = new Task<int>(n => Sum((int)n), 100);
            task.Start();
            task.Wait();     
            Task.Run(() => { Console.WriteLine("开启ling一个新线程,线程ID为：{0}", Thread.CurrentThread.ManagedThreadId); });

            Console.WriteLine("The Sum is:" + task.Result);
        }

        public static void WaitAnyTest()
        {
            Task<int> task1 = new Task<int>(n => (Sum((int)n)), 5);
            Task<int> task2 = new Task<int>(n => (Sum((int)n)), 6);
            Task<int> task3 = new Task<int>(n => (Sum((int)n)), 7);
            Task<int>[] tasks = new Task<int>[] { task1, task2, task3 };
            task3.Start();
            task2.Start();
            task1.Start();
            var index =Task.WaitAny(tasks);
            Console.WriteLine("The Index is:{0},Result is:{1}" , index, tasks[index].Result);
        }
       

        public static void TaskCancelTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> t= Task.Run(() => SumCancel(1000, cts.Token), cts.Token);

            try
            {
                Thread.Sleep(1000);
                cts.Cancel();
                Console.WriteLine("The Sum is:{0}", t.Result);
            }
            catch (AggregateException e)
            {
                var list = e.InnerExceptions;
                e.Handle(x => x is OperationCanceledException);
                Console.WriteLine("Sum was canceled,And Result is:{0}",t.Result);
            }
           
        }

        public static void TaskContinueAfterFinishTest()
        {
            Task<int> t = Task.Run(() => SumCancel(1000,CancellationToken.None ));
            t.ContinueWith(task => Console.WriteLine("The sum is:{0}", t.Result));
            Console.WriteLine("我会比结果先出来吗？");
        }

        public static void TaskFactoryTest()
        {
            Task parent = new Task(() =>
            {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<int>(cts.Token,
                                            TaskCreationOptions.AttachedToParent,
                                            TaskContinuationOptions.ExecuteSynchronously,
                                            TaskScheduler.Default);
                var children = new[] { tf.StartNew(() => SumCancel(1000, cts.Token)),
                                       tf.StartNew(() => SumCancel(2000, cts.Token)),
                                       tf.StartNew(() => SumCancel(int.MaxValue, cts.Token))};

                //任何子任务异常，就取消其余子任务
                for (int task = 0; task < children.Length; task++)
                    children[task].ContinueWith(
                        t => { Console.WriteLine("发生了取消！"); cts.Cancel(); }, TaskContinuationOptions.OnlyOnFaulted);

                //所有子任务完成后，从未出错、未取消的任务中获取返回的最大值
                //然后将最大值传给另一个任务来显示最大值
                tf.ContinueWhenAll(children, c => c.Where(v => !v.IsFaulted && !v.IsCanceled).Max(t => t.Result), CancellationToken.None)
                   .ContinueWith(t => Console.WriteLine("The Maxium is:" + t.Result), TaskContinuationOptions.ExecuteSynchronously);


            }); 

            parent.ContinueWith(p => {
                //将所有文本放到一个StringBuilder中，并只调用Console.WriteLine一次
                //因为这个任务可能和上面任务并行执行，而我不希望任务的输出变得不连续
                StringBuilder sb = new StringBuilder("the following exception(s) occurred:" + Environment.NewLine);

                foreach (var e in p.Exception.Flatten().InnerExceptions)
                {
                    sb.AppendLine(" " + e.GetType().ToString());
                    Console.WriteLine(sb.ToString());
                }
            },TaskContinuationOptions.OnlyOnFaulted);

            //启动父任务，使得子任务能够启动
            parent.Start();
        }

        private static int Sum(int n)
        {
            int i = 0;
            while (i < n)
            {
                i++;
                Thread.Sleep(2);
            }
            return i * 2;
        }

        private static int SumCancel(int n, CancellationToken token)
        {
            int i = 0;
            try
            {
               
                for (; i < n; i++)
                {
                    i++;
                    //if (token.IsCancellationRequested)
                    //    break;
                    token.ThrowIfCancellationRequested();              
                   
                }
           
            }
            catch (OperationCanceledException e)
            {
                throw e;
            }
            i = i * 2;
            return i;

        }
       
    }
}
