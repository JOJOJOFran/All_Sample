using System;
using System.Threading;

namespace Interlocked_sample
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("请输入对应数字执行对应的方法：");
            Console.WriteLine("1.执行InterLockedTest ");
            Console.WriteLine("2.执行EasyThreadAboutBackOrFontTest ");
            FuncProvider(Console.ReadLine());

        }

        static void InterLockedTest()
        {
            MultiWebRequests a = new MultiWebRequests();
        }

        static void EasyThreadAboutBackOrFontTest()
        {
            Thread t = new Thread(Worker);
            // t.IsBackground = true;
            //如果是前台线程，会执行Worker里面的方法
            //如果是后台线程，会直接结束
            t.Start();


            Console.WriteLine("Return from the Main ");
        }

        static void Worker()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Return from Worker");
        }


        static void FuncProvider(string type)
        {
            switch (type)
            {
                case "1":
                    InterLockedTest();
                    FuncProvider(Console.ReadLine());
                    break;
                case "2":
                    EasyThreadAboutBackOrFontTest();
                    FuncProvider(Console.ReadLine());
                    break;
                case "exit":
                    Console.WriteLine("bye");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
