using System;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;

namespace fudamenttype
{
    class Program
    {
        static void Main(string[] args)
        {
            //char的两个常量
            Console.WriteLine(char.MinValue);
            Console.WriteLine(char.MaxValue);
            //返回枚举值System.Globalization.UnicodeCategory
            //表面该字符是标准定义的控制字符，货币符合，小写字母，大写字母，标点符合，数学符合还是其它什么符合
            char a = 'a';
            UnicodeCategory t =char.GetUnicodeCategory(a);
            Console.WriteLine(t);
            Console.WriteLine(char.GetUnicodeCategory('+'));
            //类似这些静态方法内部通过调用GetUnicodeCategory来方便开发
            char.IsLower(a);
            char.IsSymbol('+');
            //GetNumericValue返回字符的数值形式
            Console.WriteLine(char.GetNumericValue(a)); 

            //字符集是与语言文化相关的，所以可以通过System.Threading.Thread类的静态CurrentCulture属性来获得
            //也可以传递CultureInfo类的实例来指定
            //Thread thread=new Thread(()=>Console.WriteLine(""));
            //Console.WriteLine( thread.CurrentCulture.ToString());
            char.ToLower(a, new CultureInfo(1));
            char.ToLowerInvariant(a);

            string s = "hh";
            s = s + Environment.NewLine + "1";
            Console.WriteLine(s);
            StringBuilder builder = new StringBuilder();
            builder.Append("Hello");
            builder.Append(Environment.NewLine);
            builder.Append("World");
            Console.WriteLine(builder.ToString());
            Console.WriteLine("Hello World!");

            string s1 = "Hello";
            string s2 = "Hello";
            Console.WriteLine(object.ReferenceEquals(s1,s2));

            s1 = string.Intern(s1);
            s2 = string.Intern(s2);
            Console.WriteLine(object.ReferenceEquals(s1, s2));
        }
    }
}
