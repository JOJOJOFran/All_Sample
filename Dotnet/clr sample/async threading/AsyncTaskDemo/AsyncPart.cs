using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTaskDemo
{
    public static class AsyncPart
    {
        public static async Task DosomethingAsync()
        {
            int val=13;

            await Task.Delay(TimeSpan.FromSeconds(1));
            val= val << 1;
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine(val);
        }

        #region 一个含有异步的lambda表达式
        public static async Task AwaitInLambda()
        {
            Func<string,Task<string>>   asyncLambda =async name=>{
                await Task.Delay(TimeSpan.FromSeconds(2));
                return $"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId}."+$" Is Thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}";
            };

            string result=await asyncLambda("async lambda");
            Console.WriteLine(result);

        }
        #endregion

        static async Task<string> GetInfoAsync(string name)
        {
            Console.WriteLine($"Task {name} started !");
            await Task.Delay(TimeSpan.FromSeconds(2));

            if(name=="TPL2")
                throw new Exception("TPL2 BOOM!");
            return $"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId}. Is Thread pool thread ：{ Thread.CurrentThread.IsThreadPoolThread}";
        }

        static async Task<string> GetInfoAsync(string name,int second)
        {
            Console.WriteLine($"Task {name} started !");
            await Task.Delay(TimeSpan.FromSeconds(second));

            if(name=="TPL2")
                throw new Exception("TPL2 BOOM!");
            return $"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId}. Is Thread pool thread ：{ Thread.CurrentThread.IsThreadPoolThread}";
        }
        #region 对并行执行的异步 使用await
        public static async Task AsynchronoisProcessing()
        {
            Task<string> t1=GetInfoAsync("Task1",3);
            Task<string> t2=GetInfoAsync("Task2",3);
            string[] results=await Task.WhenAll(t1,t2);
            foreach(string result in results)
            {
                Console.WriteLine(result);
            }

        }
        #endregion

        #region 异步的异常
        public static async Task AsyncExceptionCatch()
        {
            Console.WriteLine("1.Single exception by await");
            try
            {
                string result=await GetInfoAsync("Task 1",2);
                Console.WriteLine(result);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception :{e}");
            }

            Console.WriteLine();
            Console.WriteLine("2.Multiple exception by await");
            Task<string> t1=GetInfoAsync("Task1",3);
            Task<string> t2=GetInfoAsync("Task2",3);
            try
            {
                string[] results=await Task.WhenAll(t1,t2);
                Console.WriteLine(results.Length);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception :{e}");
            }

            Console.WriteLine();
            Console.WriteLine("3.Multiple exception with AggregateException");
            t1=GetInfoAsync("Task1",3);
            t2=GetInfoAsync("Task2",3);
            var t3=Task.WhenAll(t1,t2);
            try
            {
                var results=await t3;
                Console.WriteLine(results.Length);
            }
            catch
            {
                //使用Flatten展开Tasks里的所有异常,通过属性InnerExceptions获取
                var ae=t3.Exception.Flatten();
                var exceptions=ae.InnerExceptions;
                Console.WriteLine($"Exceptions Count :{exceptions.Count}");
                foreach(var item in exceptions)
                {
                    Console.WriteLine($"Exception :{item}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("4.await in catch and finally ");

             try
            {
                string result=await GetInfoAsync("Task 1",2);
                Console.WriteLine(result);
            }
            catch(Exception e)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                Console.WriteLine($"Exception :{e}");
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                Console.WriteLine("Finally block");
            }
        } 
        #endregion
        #region 自定义awaitable类型
        class CustomerAwaitable
        {
            private readonly bool _completeSynchronously;
            public CustomerAwaitable(bool completeSynchronously)
            {
                _completeSynchronously=completeSynchronously;
            }

            public CustomerAwaiter GetAwaiter()
            {
                return new CustomerAwaiter(_completeSynchronously);
            }
        }
        class CustomerAwaiter:INotifyCompletion
        {
            private string _result="Completed synchronously";

            private readonly bool _completeSynchronously;

            public bool IsCompleted => _completeSynchronously;

            public CustomerAwaiter(bool completeSynchronously)
            {
                _completeSynchronously=completeSynchronously;
            }

            public string GetResult()
            {
                return _result;
            }

            public void OnCompleted(Action continuation)
            {
                ThreadPool.QueueUserWorkItem(state=>{
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    _result=$"Task is running on a thread id{Thread.CurrentThread.ManagedThreadId}. Is Thread pool thred:{Thread.CurrentThread.IsThreadPoolThread}";
                    continuation?.Invoke();
                });
            }

        }
        #endregion
    }
}