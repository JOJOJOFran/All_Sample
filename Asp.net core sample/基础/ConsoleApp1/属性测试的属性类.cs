using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class 属性测试的属性类
    {
        private string _name;
        private int _age;
        public 属性测试的属性类(string name)
        {
            _name = name;
        }

        public Tuple<string,int> tuple(string name,int age)
        {
            //Tuple创建好后就不可变了（所有属性都只读）
            return new Tuple<string, int>(name, age);
        }

        public void 输出我的名字()
        {
            Console.WriteLine("我是二号属性:{0}", _name);
        }

        public string this[int num]
        {
            get { return String.Format("我是{0}号属性,来自索引", num); }
        }
    }
}
