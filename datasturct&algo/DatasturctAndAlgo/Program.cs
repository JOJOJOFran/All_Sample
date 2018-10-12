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


            //SingleLinkedListTest();
            LinkedListAlgoTest();
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
        #endregion 











    }


}
