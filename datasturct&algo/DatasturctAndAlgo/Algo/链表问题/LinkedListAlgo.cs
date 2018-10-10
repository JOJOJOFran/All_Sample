using DatasturctAndAlgo.LinkedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.链表问题
{
   
    public  class LinkedListAlgo
    {

        /// <summary>
        /// Q：给定两个有序的链表头指针head1和head2,打印两个链表的公共部分
        /// T:因为是有序的，所以不用重复比较,从头往后一一比，谁小就往后移一个节点进行下次比较。相同就输出，并将两个节点都后移一位
        /// </summary>
        public static void PrintCommonPart(SingleLinkedList<int> list1, SingleLinkedList<int> list2)
        {
            var head1 = list1.Head;
            var head2 = list2.Head;
            while (head1 != null && head2 != null)
            {
                if (head1.Data < head2.Data)
                {
                    head1 = head1.Next;
                }
                else if (head1.Data > head2.Data)
                {
                    head2 = head2.Next;
                }
                else if (head1.Data == head2.Data)
                {
                    Console.WriteLine(head1.Data);
                    head1 = head1.Next;
                    head2 = head2.Next;
                }

            }
        }


        /// <summary>
        /// Q:删除倒数第k的位置的节点
        /// T:先遍历到最后的节点，每遍历一次k--,
        ///   k--遍历之后必定小于等于0
        ///   比如k是倒数第一个，遍历后则为0
        ///   倒数第二个，则是-1
        ///   依次类推
        ///   然后将k累加到0
        /// </summary>
        /// <param name="lastIndex"></param>
        public void DeleteSingleListLastKNode(SingleLinkedList<int> list,int k)
        {
            var head = list.Head;
            if (head == null||k<1)
            {
                return;
            }
            while (head != null)
            {
                k--;
                head = head.Next;
            }

            if (k == 0)
            {
                list.Head = list.Head.Next;
            }

            if (k < 0)
            {
                while (k < 0)
                {
                    k++;
                    head = head.Next;
                }
                head.Next = head.Next.Next;
            }
            
        }
    }
}
