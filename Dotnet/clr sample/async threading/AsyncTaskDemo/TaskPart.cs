using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTaskDemo
{
    /// <summary>
    /// 任务并行库练习
    /// </summary>
    public static class TaskPart
    {
       

        #region 创建Task和使用
        public static void TaskCreateAndUse()
        {
            //创建任务
            var t1 = new Task(() => TaskMethod("Task 1"));
            var t2 = new Task(() => TaskMethod("Task 2"));
            //运行任务
            t1.Start();
            t2.Start();
            //简化创建并运行Task
            Task.Run(() => TaskMethod("Task 3"));
            Task.Factory.StartNew(() => TaskMethod("Task 4"));
            //TaskCreationOptions.LongRunning=》不是线程池线程
            Task.Factory.StartNew(() => TaskMethod("Task 5"), TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            //带返回值的task，并获取结果
            var task = CreateTask("1");
            task.Start();
            Console.WriteLine(task.Result);


        }

        static void TaskMethod(string name)
        {
            Console.WriteLine($"Task: {name} is running on a thread id:" + $"{Thread.CurrentThread.ManagedThreadId}. Is thread pool thread:" + $"{Thread.CurrentThread.IsThreadPoolThread}");
        }

        static int TaskMethodOutputInt(string name)
        {
            TaskMethod(name);
            return 2;
        }

        static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethodOutputInt(name));
        }
        #endregion

        #region 组合Task
        static int TaskMethod(string name, int sceonds)
        {
            TaskMethod(name);
            return 60 * sceonds;
        }

        /// <summary>
        /// 组合Task
        /// </summary>
        public static void TaskContinueTest()
        {
            var firstTask = new Task<int>(() => TaskMethod("first Task", 4));
            var lastTask = new Task<int>(() => TaskMethod("last Task", 3));

            firstTask.ContinueWith(
            t => {
                Console.WriteLine($"firstTask answer is: {firstTask.Result} is running on a thread id:" + $"{Thread.CurrentThread.ManagedThreadId}. Is thread pool thread:" + $"{Thread.CurrentThread.IsThreadPoolThread}");
            });

            firstTask.Start();
            lastTask.Start();
            Thread.Sleep(TimeSpan.FromSeconds(3));

            Task continuation = lastTask.ContinueWith(
                t =>{
                    Console.WriteLine($"lastTask answer is: {lastTask.Result} is running on a thread id:" + $"{Thread.CurrentThread.ManagedThreadId}. Is thread pool thread:" + $"{Thread.CurrentThread.IsThreadPoolThread}");
                });

            continuation.GetAwaiter().OnCompleted(() => { Console.WriteLine($"lastTask answer is: {lastTask.Result} is running on a thread id:" + $"{Thread.CurrentThread.ManagedThreadId}. Is thread pool thread:" + $"{Thread.CurrentThread.IsThreadPoolThread}"); });

        }
        #endregion

        #region EAP模式转换为任务
        /// <summary>
        /// 基于异步事件转换为任务
        /// </summary>
        public static void EAPTask()
        {
            var tcs = new TaskCompletionSource<int>();
            var worker =new BackgroundWorker();
            worker.DoWork+=(sender,EventArgs)=>{
                EventArgs.Result=TaskMethod("Background worker",5);
            };

            worker.RunWorkerCompleted+=(sender,EventArgs)=>
            {
                if(EventArgs.Error!=null)
                {
                    tcs.SetException(EventArgs.Error);
                }
                else if(EventArgs.Cancelled)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    tcs.SetResult((int)EventArgs.Result);
                }
            };

            worker.RunWorkerAsync();
            int result=tcs.Task.Result;
            Console.WriteLine($"Result is:{result}");

        }
        #endregion

        #region  APM模式
        delegate string 
        AsynchronousTask(string threadName);
        delegate string IncompatileAsynchronousTask(out int threadId);

        static void Callback(IAsyncResult ar)
        {
            Console.WriteLine("Starting a callback...");
            Console.WriteLine($"State passed to a callback:{ar.AsyncState}");
            Console.WriteLine($"Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Thread pool worker thread id:{Thread.CurrentThread.ManagedThreadId}");
        }

        static string Test(string threadName)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine($"Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Thread.CurrentThread.Name=threadName;
            return $"Thread name:{Thread.CurrentThread.Name}";
        }

         static string Test(out int threadId)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine($"Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            threadId=Thread.CurrentThread.ManagedThreadId;
            return $"Thread name:{threadId}";
        }

        /// <summary>
        /// APM模式转换Task
        /// </summary>
        public static void APMTest()
        {
            int ThreadId;
            AsynchronousTask d=Test;
            IncompatileAsynchronousTask e=Test;

            Console.WriteLine("Option 1");
            Task<string> task=Task<string>.Factory.FromAsync(d.BeginInvoke("AsyncTaskThread",Callback,"a delegate asynchronous call"),d.EndInvoke);

            task.ContinueWith(t=>{Console.WriteLine($"Callback is finished ,now running a continuation resul:{t.Result}");});

            while(!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
             Console.WriteLine(task.Status);
             Thread.Sleep(TimeSpan.FromSeconds(1));
        }
        #endregion

        #region 任务取消
        static int TaskMethodWithCancel(string name,CancellationToken token,int second)
        {
            for(int i=1;i<=100;i++)
            {
                var msg= i%100;
                Console.WriteLine($"任务执行进度:{msg} %");
                Thread.Sleep(TimeSpan.FromSeconds(0.1));
                if(token.IsCancellationRequested)
                    return -1;
            }
            return TaskMethod(name,second);
        }

        /// <summary>
        /// 任务取消
        /// </summary>
        public static void TaskCancelTest()
        {
            var cts =new CancellationTokenSource();
            var longTask=new Task<int>(()=>TaskMethodWithCancel("Task1",cts.Token,10),cts.Token);
            
            Console.WriteLine(longTask.Status);
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            longTask.Start();
            Thread.Sleep(TimeSpan.FromSeconds(9));
            cts.Cancel();
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            Console.WriteLine(longTask.Status);
        } 
        #endregion

        #region Task异常处理
        static int TaskMethodWithException(string name ,int second)
        {
             int result= TaskMethod(name,second);
             Thread.Sleep(TimeSpan.FromSeconds(second));
             throw new Exception("Boom!");
             return result;
        }

        /// <summary>
        /// Task异常处理
        /// </summary>
        public static void TaskExceptionTest()
        {
            Task<int> task;
            try
            {
                task=Task.Run(()=>TaskMethodWithException("Exception Task1",3));
                int result=task.Result;
                Console.WriteLine($"Result is {result}");
            }
            catch (Exception e)
            {
                 Console.WriteLine($"Exception Caught :{e.Message}");
            }
            Console.WriteLine("=========================================");
            Console.WriteLine();
            try
            {
                task=Task.Run(()=>TaskMethodWithException("Exception Task2",3));
                int result =task.GetAwaiter().GetResult();
                Console.WriteLine($"Result is {result}");
            }
            catch (Exception e)
            {
                 Console.WriteLine($"Exception Caught :{e.Message}");
            }
            Console.WriteLine("=========================================");
            Console.WriteLine();

            var t1=new Task<int>(()=>TaskMethodWithException("Exception Task 3",3));
            var t2=new Task<int>(()=>TaskMethodWithException("Exception Task 4",2));
            var complexTask=Task.WhenAll(new Task[]{t1,t2});
            var exceptionHandler=complexTask.ContinueWith(t=>{Console.WriteLine($"Exception Caught :{t.Exception.Message}");},TaskContinuationOptions.OnlyOnFaulted);
            t1.Start();
            t2.Start();

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
        #endregion

        #region 任务并行
        public static void TaskAll()
        {
            var firstTask = new Task<int>(() => { return TaskMethod("First Task", 3); });
            var secondTask =new Task<int>(() => { return TaskMethod("Second Task", 4); });

            var whenAllTask= Task.WhenAll(firstTask,secondTask);
            whenAllTask.ContinueWith(t=>{
                Console.WriteLine($"Frist Task Result is:{t.Result[0]},the second is:{t.Result[1]}");
                },TaskContinuationOptions.OnlyOnRanToCompletion);

            firstTask.Start();
            secondTask.Start();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            var tasks=new List<Task<int>>();
            for(int i=0;i<4;i++)
            {
                var task=new Task<int>(()=>TaskMethod($"Task {i}",i));
                tasks.Add(task);
                task.Start();
            }

            while(tasks.Count>0)
            {
                var complexTask=Task.WhenAny(tasks).Result;
                tasks.Remove(complexTask);
                Console.WriteLine($"A Task has been completed with result {complexTask.Result}");
            }

            Thread.Sleep(TimeSpan.FromSeconds(1));

        }
        #endregion

    }
}
