using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public static class AttributeSample
    {
        
    }

    [AttributeUsage(AttributeTargets.Enum|AttributeTargets.Field)]
    public class RemarkAttribute : Attribute
    {
        public string Remark { get; set; }

        public RemarkAttribute(string remark)
        {
            Remark = remark;
        }
    }

    public static class RemarkExtend
    {
        public static void GetRemark(this Enum enumValue)
        {
            Type type = enumValue.GetType();
            var filed = type.GetField(enumValue.ToString());
            RemarkAttribute attribute = (RemarkAttribute) filed.GetCustomAttributes(typeof(RemarkAttribute), true)[0];
            Console.WriteLine("Attribute测试：{0}", attribute.Remark);
        }
    }
  

    [Remark("测试枚举")]
    public enum test
    {
        [Remark("工作")]
        Work =1,
        [Remark("休息")]
        Rest =2,
        [Remark("睡觉")]
        Sleep =3
    }



}
