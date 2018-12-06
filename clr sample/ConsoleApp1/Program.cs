using MultiThreadDemo;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("根据编号选择方法进行执行,请输入对应编号");
            TitleSet();
            FunctionProvider(Console.ReadLine());
            Console.ReadKey();
        }

        static void TitleSet()
        {
            Console.WriteLine("1.执行上下文测试代码");
            Console.WriteLine("2.执行取消异步操作代码");
            Console.WriteLine("3.执行Task示例");
        }

        static void FunctionProvider(string type)
        {
            switch (type)
            {
                case "1":
                    ThreadContextDemo.ExecutionContextTest();
                    break;
                case "2":
                    CancellationDemo.Go();
                    break;
                case "3":
                    TaskDemo.Go();
                    break;
            }
        }

      

        

       
    }

     
}
