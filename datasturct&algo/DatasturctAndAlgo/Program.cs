using DatasturctAndAlgo.Algo.LeetCode;
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
            Console.WriteLine(LeetCode.LengthOfLongestSubstring("qqwwqer"));

            //return head1;
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











    }


}
