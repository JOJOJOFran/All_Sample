using System;

namespace BaseTypeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Enum!");
            Console.WriteLine("================GetUnderlyingType==============");
            Console.WriteLine(Enum.GetUnderlyingType(typeof(Color)));
            Console.WriteLine("================GetName&GetNames==============");
            Console.WriteLine(Enum.GetName(typeof(Color), 0));
            foreach (var item in Enum.GetNames(typeof(Color)))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("================GetValues==============");
            foreach (var item in Enum.GetValues(typeof(Color)))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("================IsDefined==============");
            if (Enum.IsDefined(typeof(Color), 0))
            {
                Console.WriteLine("已定义");
            }
            else
            {
                Console.WriteLine("未定义");
            }

            if (Enum.IsDefined(typeof(Color), 3))
            {
                Console.WriteLine("已定义");
            }
            else
            {
                Console.WriteLine("未定义");
            }
            Console.WriteLine("================ToString==============");
            Color c = Color.blue;
            Console.WriteLine(c);               //blue
            Console.WriteLine(c.ToString());    //blue
            Console.WriteLine(c.ToString("G")); //常规 blue
            Console.WriteLine(c.ToString("D")); //十进制
            Console.WriteLine(c.ToString("X")); //十六进制 byte/sbyte输出两位数 uint/int输出八位数，long/ulong输出十六位数
            Console.WriteLine("================Format==============");
            Console.WriteLine(Enum.Format(typeof(Color), 1, "G"));  //blue
            Console.WriteLine(Enum.Format(typeof(Color), 1, "D")); //1
            Console.WriteLine(Enum.Format(typeof(Color), 1, "X"));  //00000001
            Console.WriteLine("================Parse==============");
            Color c1;
            c1 = ((Color)Enum.Parse(typeof(Color), "red", true));
            Console.WriteLine(Enum.Parse(typeof(Color), "red"));    //red
            Console.WriteLine(c1);
            // Console.WriteLine(Enum.Parse(typeof(Color), "brwon"));  //brwon 未定义System.ArgumentException:“Requested value 'brwon' was not found.”
            Console.WriteLine("================TryParse==============");
            Enum.TryParse<Color>("1", false, out c1);
            Console.WriteLine(c1); //blue
            Enum.TryParse<Color>("brown", false, out c1);
            Console.WriteLine(c1); //blue
            Enum.TryParse<Color>("23", false, out c1); //
            Console.WriteLine(c1); //23
            foreach (var item in Enum.GetValues(typeof(Color)))
            {
                Console.WriteLine(item);
            }


            Console.WriteLine(Enum.GetName(typeof(Color), 23));  //name 为空

            Console.ReadKey();

        }

        public enum Color
        {
            red = 0,
            blue = 1,
            green = 2
        }

        public static TEnum[] GetEnumValues<TEnum>() where TEnum : struct
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }
}
