using System;
using System.Threading.Tasks;

namespace AsyncTaskDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("根据编号选择方法进行执行,请输入对应编号");
            TitleSet();
            FunctionProvider(Console.ReadLine());

            Console.ReadKey();
        }

        static void TitleSet()
        {
            Console.WriteLine("1.创建Task和使用");
            Console.WriteLine("2.组合Task");
            Console.WriteLine("3.EAP模式转换为任务");
            Console.WriteLine("4.APM模式");
            Console.WriteLine("5.任务取消");
            Console.WriteLine("6.Task异常处理");
            Console.WriteLine("=============");
            Console.WriteLine("7.Async=>DosomethingAsync");
            Console.WriteLine("待续。。。。");
        }

        static void FunctionProvider(string type)
        {
            switch (type)
            {
                case "1":
                    TaskPart.TaskCreateAndUse();                   
                    break;
                case "2":
                    TaskPart.TaskContinueTest();
                    break;
                case "3":
                    TaskPart.EAPTask();
                    break;
                case "4":
                    TaskPart.APMTest();
                    break;
                case "5":
                    TaskPart.TaskCancelTest();
                    break;
                case "6":
                    TaskPart.TaskExceptionTest();
                    break;
                 case "7":
                    AsyncPart.DosomethingAsync();
                    break;
                case "exit":
                    Console.WriteLine("按任意键将推出程序！");
                    return;
                
            }
            FunctionProvider(Console.ReadLine());
        }
    }
}
