using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace BaseDataStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //FranList<int> fran = new FranList<int>(new List<int> { 1, 2, 3, 4 });
            //Console.WriteLine(fran.Count);
            //Console.WriteLine(fran.Capacity);
            //fran.Add(5);
            //Console.WriteLine(fran.Count);
            //Console.WriteLine(fran.Capacity);
            //Console.WriteLine(fran[3]);
            //fran[10] = 10;
            //Console.WriteLine(fran[3]);
            ArrayTest();
            Console.ReadKey();

           
        }

        #region 数组测试
        static void ArrayTest()
        {
           
            int[] array = new int[10] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var list = array.ToList();

            // Array.Clear只能重置数组的元素，不能真正的删除
            //将数组中的某个范围的元素设置为每个元素类型的默认值
            Console.WriteLine(array[0]);
            Array.Clear(array, 0, 1);
            Console.WriteLine(array[0]);
            Console.WriteLine(array.Length);

            //删除会移动其它元素
            while (list.Count > 0)
            {
                Console.WriteLine(list[0]);
                list.RemoveAt(0);
            }

            //ArrayList arrayList = new ArrayList();
            //arrayList.Add(1);
            //arrayList.Add("2");
           


        }
        #endregion



        static void ConsoleTitle(string title)
        {

        }
    }
}
