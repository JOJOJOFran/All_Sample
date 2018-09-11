using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class 属性测试
    {
        public string getValue = "first";
        public string setDefaultValue ;
        public string getFromgetValue { get { return getValue; } }

        public string getOnly { get { return "Just Get"; } }
        public string setDefault { set { setDefault = "Default Value"; } }
        public string getsetDefault { get { return setDefaultValue; } set { setDefaultValue = "DefaulValue"; } }

        public 属性测试的属性类 二号属性 { get { return new 属性测试的属性类("new 属性"); } }
    }
}
