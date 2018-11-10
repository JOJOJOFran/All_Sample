using DatasturctAndAlgo.Algo.BinarySearch;
using DatasturctAndAlgo.Algo.LeetCode;
using DatasturctAndAlgo.Algo.Sort;
using DatasturctAndAlgo.Algo.链表问题;
using DatasturctAndAlgo.LinkedList;
using System;
using System.Collections.Generic;

namespace DatasturctAndAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //LeetCode.LengthOfLongestSubstring("aabc");
            //Console.WriteLine(LeetCode.LengthOfLongestSubstring("aabc"));
            //Console.WriteLine(LeetCode.LengthOfLongestSubstring("qqqq"));
            //Console.WriteLine(LeetCode.LengthOfLongestSubstring("qqwwqer"));
            // SortedDictionary<>

            //SortedSet<int> set = new SortedSet<int>();
            //SortedList<int, int> list = new SortedList<int, int>();
            //int[] arry = new int[] { 1, 0, 9, 9, 7, 8, 53, 4, 46, 4, 4, 4, 7, 8, 9, 65, 3, 2 };
            //for (int i = 0; i < arry.Length; i++)
            //{
            //    list.Add((arry[i]),i );
            //    set.Add(arry[i]);
            //}
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}


            //return head1;
            SortTest();

            List<int> a = null;

            Console.WriteLine(a?.Count);
            string b = null;

            Console.WriteLine(b ?? "111");
            Console.WriteLine(BinartSearchTest());
            Console.WriteLine(BinartSearchTest1());
            Console.WriteLine(BinartSearchTest2());
            Console.WriteLine(BinartSearchTest3());
            Console.ReadKey();
        }

        #region 链表结构测试
        /// <summary>
        /// 单链表测试
        /// </summary>
        static void SingleLinkedListTest()
        {
            SingleLinkedList<int> singleLinkedList = new SingleLinkedList<int>();
            singleLinkedList.InsertHead(1);
            singleLinkedList.InsertAfter(singleLinkedList.Head, 2);
            Console.WriteLine(singleLinkedList.FindDataByIndex(1));

            SingleLinkedList<string> strList = new SingleLinkedList<string>();
            strList.InsertHead("head");
            strList.InsertAfter(strList.Head, "body");
            strList.InsertAfter(strList.FindNodeByValue("body"), "leg");
            strList.InsertAfter(strList.FindNodeByValue("leg"), "foot");
            strList.InsertBefore(strList.FindNodeByValue("leg"), new Node<string>("Knee", null));
            var list = strList.ToList();

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

        }
        #endregion

        # region 链表算法测试
        static void LinkedListAlgoTest()
        {
            SingleLinkedList<int> strList = new SingleLinkedList<int>();
            strList.InsertHead(1);
            strList.InsertAfter(strList.Head, 2);
            strList.InsertAfter(strList.Head.Next, 2);
            strList.InsertAfter(strList.Head.Next.Next, 1);
  

            LinkedListAlgo algo = new LinkedListAlgo();
            if (algo.PalindromeValidateByLinkedList(strList))
            {
                Console.WriteLine("是回文链表");
            }
            else
            {
                Console.WriteLine("不是回文链表");
            }
        }

        static void test()
        {
            SingleLinkedList<int> strList = new SingleLinkedList<int>();
            strList.InsertHead(9);
            strList.InsertAfter(strList.Head, 9);
            strList.InsertAfter(strList.Head.Next, 9);

            SingleLinkedList<int> strList1 = new SingleLinkedList<int>();
            strList1.InsertHead(9);
            strList1.InsertAfter(strList1.Head, 9);
            strList1.InsertAfter(strList1.Head.Next, 9);
            LinkedListAlgo algo = new LinkedListAlgo();
            algo.AddTowList(strList.Head, strList1.Head);
        }
        #endregion

        #region 排序测试
        public static int[] array = new int[] { 6, 4, 3, 1, 2, 7, 8, 10, 9, 0 };
        public static int[] array1 = new int[] { 6, 4, 3, 1, 2, 7, 8, 10, 9, 0 };
        public static int[] array2 = new int[] { 6, 4, 3, 1, 2, 7, 8, 10, 9, 0 };
        public static int[] array3 = new int[] { 6, 4, 3, 1, 2, 7, 8, 10, 9, 0 };
        public static void SortTest()
        {
            int[] newArray = new int[10];
            array.CopyTo(newArray,0);

            Console.WriteLine("===冒泡测试===");
            Sort.SelectSorted(array);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }

            Console.WriteLine("===插排测试===");
            Sort.InsertSort(array1);
            for (int i = 0; i < array1.Length; i++)
            {
                Console.WriteLine(array1[i]);
            }

            Console.WriteLine("===归并测试===");
            Sort.MergeSort(array2);
            for (int i = 0; i < array2.Length; i++)
            {
                Console.WriteLine(array2[i]);
            }

            Console.WriteLine("===快速测试===");
            Sort.QuickSort(array3);
            for (int i = 0; i < array3.Length; i++)
            {
                Console.WriteLine(array3[i]);
            }


        }
        #endregion

        #region 二分查找测试
        public static int BinartSearchTest()
        {
            int[] array = new int[2];
            for (int i = 0; i < 2; i++)
            {
                array[i] = i;
            }

          return  BinarySearch.BinarySearchWithCycle(array,1);
        }

        public static int BinartSearchTest1()
        {
            int[] array = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                array[i] = i;
            }

            return BinarySearch.BinarySearchWithRecursion(array, 367);
        }

        public static int BinartSearchTest2()
        {
            int[] array = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                if (i % 2 == 0)
                {
                    array[i] = i;
                }
                else
                {
                    array[i] = i-1;
                }
               
            }
            return BinarySearch.BinarySearchToFindFristPoint(array, 2);
        }


        public static int BinartSearchTest3()
        {
            int[] array = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                if (i % 2 == 0)
                {
                    array[i] = i;
                }
                else
                {
                    array[i] = i - 1;
                }

            }
            return BinarySearch.BinarySearchLastPoint(array, 2);
        }
        #endregion










    }


}
