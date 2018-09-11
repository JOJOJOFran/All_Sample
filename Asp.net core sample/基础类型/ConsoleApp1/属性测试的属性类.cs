using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class 属性测试的属性类
    {
        private string _name;

        public 属性测试的属性类(string name)
        {
            _name = name;
        }

        public void 输出我的名字()
        {
            Console.WriteLine("我是二号属性:{0}", _name);
        }
    }
}
