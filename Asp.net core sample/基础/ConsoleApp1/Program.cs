using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("这是一次属性的测试，对get访问器，set访问器进行测试！");

            属性测试 新属性 = new 属性测试();
            Console.WriteLine("第一次输出getFromgetValue值:{0}", 新属性.getFromgetValue);
            新属性.getValue = "second";
            Console.WriteLine("更改getValue值后，第二次输出getFromgetValue值:{0}", 新属性.getFromgetValue);

            Console.WriteLine("第一次输出getOnly值:{0}", 新属性.getOnly);
            Console.WriteLine("尝试set getOnly的值:新属性.getOnly = ''123'';"); 
            Console.WriteLine("set失败，编译器提示getOnly是只读的");

            Console.WriteLine("输出setDefault的默认值失败因为没有get访问器");

            Console.WriteLine("输出字段setDefaultValue的值：{0}", 新属性.setDefaultValue);
            //set并不生效
            Console.WriteLine("输出属性getsetDefault的值：{0}", 新属性.getsetDefault);
            新属性.二号属性.输出我的名字();
            新属性.二号属性.输出我的名字();
            新属性.索引测试(1);

            test.Rest.GetRemark();
            Console.ReadKey();

           
        }
    }
}
