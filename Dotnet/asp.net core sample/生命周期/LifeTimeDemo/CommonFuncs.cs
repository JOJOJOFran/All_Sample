using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeTimeDemo
{
    public static class CommonFuncs
    {
        public static void OutPut(string msg )
        {
            Console.WriteLine($"时间：{DateTime.Now}，发生了：{msg}");
        }
    }
}
