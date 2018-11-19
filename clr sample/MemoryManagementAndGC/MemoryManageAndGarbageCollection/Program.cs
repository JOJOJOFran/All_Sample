using System;
using System.Threading;

namespace MemoryManageAndGarbageCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建每2000毫秒就调用一次TimerCallback方法的Timer对象
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            Console.ReadLine();
        }

        private static void TimerCallback(object o)
        {
            Console.WriteLine("In TimerCallback:" + DateTime.Now);
            //
            //GC.Collect();
        }
    }
}
