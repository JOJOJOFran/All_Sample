using DatasturctAndAlgo.LinkedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.链表问题
{

    public  class LinkedListAlgo
    {
        /// <summary>
        /// Q:单链表反转
        /// T:首先反转，不能真正的从内存空间上去思考，只要把指针的顺序反转，原来的头节点指向null（相当于变成了最后的节点）即可
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Node<int> SingleListReverse(Node<int> head)
        {
            Node<int> prevNode = null;
            Node<int> nextNode = null;
            while (head != null)
            {
                nextNode = head.Next;
                head.Next = prevNode;
                prevNode = head;
                head = nextNode;
            }

            return prevNode;
        }

        /// <summary>
        /// Q:双链表反转
        /// T:双链表反转更为简单，只要把遍历把每个节点的next指针和prev指针交互，即可达到反转的目的
        ///   类似单链表的反转
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DoubleLinkedList<int> DoubleListReverse(DoubleLinkedList<int> list)
        {
            DoubleNode<int> prevNode = null;
            DoubleNode<int> nextNode = null;
            DoubleNode<int> currentNode = list.Head;
            while (currentNode != null)
            {
               //反转后指针
                nextNode = currentNode.Next;              
                currentNode.Next = prevNode;
                //反转前指针
                currentNode.Prev = nextNode;
                //前节点后移
                prevNode = currentNode;
                //当前节点后移
                currentNode = nextNode;
            }
            list.Head = prevNode;
            return list;
        }

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

        /// 删除倒数第k的位置的节点
        /// <summary>
        /// Q:删除倒数第k的位置的节点
        /// T:先遍历到最后的节点，每遍历一次k--,
        ///   k--遍历之后必定小于等于0
        ///   比如k是倒数第一个，遍历后则为0
        ///   倒数第二个，则是-1
        ///   依次类推
        ///   然后将k累加到0
        ///   归纳一下，如果有N个数，那么遍历减之后，就是-（n-k）(n>k),而倒数第k个实际上是正数n-k+1个，所以在循环（n-k）次后找到第n-k位置的元素还要往后移一位
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

        /// <summary>
        /// Q:找到中间节点
        /// T:使用快慢指针，一个每循环一次位移两次，一个每循环一次位移一个，当两个位移到最末尾时，位移一次的就是中间节点
        /// 不论是走到倒数第二个 还是 倒数第一个都跳出循环
        /// 偶数个的时候会走到倒数第二个跳出 奇数个的时候是走到倒数第一个跳出
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Node<int> FindMiddleNode(SingleLinkedList<int> list)
        {
            if (list == null)
            {
                return null;
            }

            var slowHead = list.Head;
            var fastHead = list.Head;

            //不论是走到倒数第二个 还是 倒数第一个都跳出循环
            while (fastHead.Next != null&& fastHead.Next.Next!=null)
            {
                slowHead = slowHead.Next;
                fastHead = fastHead.Next.Next;

            }

            return slowHead;
        }

        /// <summary>
        /// Q:链表中的回文判断
        /// T:可以反转链表，然后比对两个链表是不是相等(不可行，如果要实现需要，因为引用的关系需要把链表全部复制一遍再进行反转)
        ///   也可以，将链表找到中间节点，然后针对中间节点进行反转，将反转后的半节链表进行比对
        ///   用栈存储一个，再遍历对比
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool PalindromeValidateByLinkedList(SingleLinkedList<int> list)
        {
            if (list.Head == null)
            {
                return true;
            }
            Stack<Node<int>> stack = new Stack<Node<int>>();
            var head = list.Head;
            while (head != null)
            {
                stack.Push(head);
                head = head.Next;
            }

            while (list.Head != null)
            {
                if (list.Head.Data!= stack.Pop().Data)
                {
                    return false;
                }
                list.Head = list.Head.Next;
            }

            return true;
            
        }
    }
}
