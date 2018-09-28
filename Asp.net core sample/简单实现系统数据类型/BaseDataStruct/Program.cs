using System;
using System.Collections.Generic;

namespace BaseDataStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FranList<int> fran = new FranList<int>(new List<int> { 1, 2, 3, 4 });
            Console.WriteLine(fran.Count);
            Console.WriteLine(fran.Capacity);
            fran.Add(5);
            Console.WriteLine(fran.Count);
            Console.WriteLine(fran.Capacity);
            Console.WriteLine(fran[3]);
            fran[10] = 10;
            Console.WriteLine(fran[3]);
            Console.ReadKey();
        }
    }
}
