using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("这是一次属性的测试，对get访问器，set访问器进行测试！");

            //属性测试 新属性 = new 属性测试();
            //Console.WriteLine("第一次输出getFromgetValue值:{0}", 新属性.getFromgetValue);
            //新属性.getValue = "second";
            //Console.WriteLine("更改getValue值后，第二次输出getFromgetValue值:{0}", 新属性.getFromgetValue);

            //Console.WriteLine("第一次输出getOnly值:{0}", 新属性.getOnly);
            //Console.WriteLine("尝试set getOnly的值:新属性.getOnly = ''123'';"); 
            //Console.WriteLine("set失败，编译器提示getOnly是只读的");

            //Console.WriteLine("输出setDefault的默认值失败因为没有get访问器");

            //Console.WriteLine("输出字段setDefaultValue的值：{0}", 新属性.setDefaultValue);
            ////set并不生效
            //Console.WriteLine("输出属性getsetDefault的值：{0}", 新属性.getsetDefault);
            //新属性.二号属性.输出我的名字();
            //新属性.二号属性.输出我的名字();
            //新属性.索引测试(1);


            //test.Rest.GetRemark();

            DateTime dt = DateTime.Now;  //当前时间  

            //DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).Date;  //本周周一  
            //DateTime endWeek = startWeek.AddDays(7).AddSeconds(-1);  //本周周日  
            //Console.WriteLine(startWeek);
            //Console.WriteLine(endWeek);
            DateTime startMonth = dt.AddDays(1 - dt.Day).Date;  //本月月初  
            DateTime endMonth = startMonth.AddMonths(1).AddSeconds(-1);  //本月月末  
           //DateTime endMonth = startMonth.AddDays((dt.AddMonths(1) - dt).Days - 1);  //本月月末  
            Console.WriteLine(startMonth);
            Console.WriteLine(endMonth);                                                         

            DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day).Date;  //本季度初  
            DateTime endQuarter = startQuarter.AddMonths(3).AddSeconds(-1);  //本季度末  
            Console.WriteLine(startQuarter);
            Console.WriteLine(endQuarter);

            DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初  
            DateTime endYear = new DateTime(dt.Year, 12, 31).Date.AddDays(1).AddSeconds(-1);  //本年年末  
            Console.WriteLine(startYear);
            Console.WriteLine(endYear);

            Console.ReadKey();

           
        }
    }
}
