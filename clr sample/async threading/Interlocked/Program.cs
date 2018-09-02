using System;
using System.Threading;

namespace Interlocked_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            int test = 4;
            while (test >= 0)
            {
                test--;
                Console.WriteLine(test);
            }
                
            
            MultiWebRequests a = new MultiWebRequests();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
