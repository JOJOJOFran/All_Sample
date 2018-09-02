using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interlocked_sample
{
    internal sealed class MultiWebRequests
    {
        //用于协调所有的异步操作
        private AsyncCoordinator m_ac = new AsyncCoordinator();

        //相要查询的web服务器及其响应的集合
        //多个线程访问该字典不需要以同步方式进行
        //因为这里构造后，键是只读的
        private Dictionary<string, Object> m_servers = new Dictionary<string, object>
        {
            {"http://Wintellect.com/",null },
            {"http://Microsoft.com/",null },
            {"http://1.1.1.1/",null }
        };

        //Timeout.Infinite=-1
        public MultiWebRequests(Int32 timeout = Timeout.Infinite)
        {
            //以异步的方式一次性发起所有请求
            var httpClient = new HttpClient();

            foreach (var server in m_servers.Keys)
            {
                m_ac.AboutToBegin(1);
                httpClient.GetByteArrayAsync(server).ContinueWith(task => ComputeResult(server, task));
            }

            //告诉AsyncCoordinator所有操作都已发起，并在所有操作完成调用Cancel或者超市时的时候调用AllDone
            m_ac.AllBegun(AllDone, timeout);

        }

        private void ComputeResult(string server, Task<Byte[]> task)
        {
            Console.WriteLine(server);
            object result;
            if (task.Exception != null)
            {
                result = task.Exception.InnerException;
            }
            else
            {
                //这里添加I/O操作或计算操作，此处只存放返回的长度作示例
                result = task.Result.Length;
            }
            
            m_servers[server] = result;
            m_ac.JustEnded();
        }

        public void Cancel() { m_ac.Cancle(); }

        public void AllDone(CoordinationStatus status)
        {
            switch (status)
            {
                case CoordinationStatus.Cancel:
                    Console.WriteLine("Operation canceled");
                    break;
                case CoordinationStatus.TimeOut:
                    Console.WriteLine("Operation time-out");
                    break;
                case CoordinationStatus.AllDone:
                    Console.WriteLine("Operation completed");
                    foreach (var server in m_servers)
                    {
                        Console.WriteLine("{0}",server.Key);
                        var result = server.Value;
                        if (result is Exception)
                        {
                            Console.WriteLine("failed due to {0}", result.GetType().Name);
                        }
                        else
                        {
                            Console.WriteLine("retruned {0:N0} bytes",result);
                        }
                    }
                    break;
            }
           
        }

    }

        public enum CoordinationStatus
        {
            Cancel,
            TimeOut,
            AllDone
        }
}
