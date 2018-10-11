using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.LinkedList
{
    /// <summary>
    /// 简单实现双链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoubleLinkedList<T>:LinkedList<T>
    {
        private DoubleNode<T> _head;
        public DoubleNode<T> Head
        {
            get { return _head; }
            set { _head = value; }
        }

        public DoubleLinkedList()
        {
            _head = null;
        }

        public DoubleLinkedList(DoubleNode<T> head)
        {
            if (head == null)
            {
                ThrowLinkedListException("头节点不能为空");
            }
            _head = head;
            _head.Prev = null;
            _head.Next = null;
        }

        public void InsertHead(DoubleNode<T> head)
        {
            if (head == null)
            {
                ThrowLinkedListException("头节点不能为空");
            }

            _head = head;
            _head.Prev = null;
        }

        public void InsertAfter(DoubleNode<T> node, DoubleNode<T> newNode)
        {
            if (!InsertValidation(node, newNode))
            {
                return;
            }

            var tempNode = _head;
            while (tempNode != null&& tempNode.Next!= node)
            {
                tempNode = tempNode.Next;
                
            }
            if (tempNode == null)
            {
                ThrowLinkedListException("找不到对应的前置节点");
                return;
            }
            newNode.Next= tempNode.Next;
            newNode.Prev = tempNode;
            tempNode.Next = newNode;
        }


        public void InsertAfterIndex(int index, DoubleNode<T> newNode)
        {
            if (index < 0)
            {
                ThrowLinkedListException("index不能小于0");
                return;
            }

            var tempNode = FindNodeByIndex(index);
            if (tempNode != null)
            {
                ThrowLinkedListException("当前index不能不存在数据元素");
                return;
            }

            newNode.Next = tempNode.Next;
            newNode.Prev = tempNode;
            tempNode.Next = newNode;

        }

        public void InsertBefore(DoubleNode<T> node, DoubleNode<T> newNode)
        {
            if (!InsertValidation(node, newNode))
            {
                return;
            }

            var tempNode = _head;
            while (tempNode != null && tempNode.Next != node)
            {
                tempNode = tempNode.Next;
            }

            newNode.Next = tempNode;
            newNode.Prev = tempNode.Prev;
            tempNode.Prev = newNode;
        }

        public void InsertBeforIndex(int index, DoubleNode<T> newNode)
        {
            if (index < 0)
            {
                ThrowLinkedListException("index不能小于0");
                return;
            }

            var tempNode = FindNodeByIndex(index);
            if (tempNode != null)
            {
                ThrowLinkedListException("当前index不能不存在数据元素");
                return;
            }

            newNode.Next = tempNode;
            newNode.Prev = tempNode.Prev;
            tempNode.Prev = newNode;

        }

        public T FindDataByIndex(int index)
        {
            int offset = 0;
            var tempNode = _head;
            while (tempNode != null && offset != index)
            {
                tempNode = tempNode.Next;
                ++offset;
            }
            return tempNode.Data;
        }

        public DoubleNode<T> FindNodeByValue(T value)
        {
            var tempNode = _head;
            while (!tempNode.Data.Equals(value) && tempNode != null)
            {
                tempNode = tempNode.Next;
            }
            return tempNode;
        }

        public DoubleNode<T> FindNodeByIndex(int index)
        {
            int offset = 0;
            var tempNode = _head;
            while (tempNode != null && offset != index)
            {
                tempNode = tempNode.Next;
                ++offset;
            }

            return tempNode;
        }


        public void Delete(DoubleNode<T> node)
        {
            if (node == null || _head == null)
            {
                return;
            }

            if (node == _head)
            {
                Head = Head.Next;
            }

            var tempNode = _head;
            while (tempNode != null && tempNode.Next != node)
            {
                tempNode = tempNode.Next;
                
            }

            if (tempNode == null)
            {
                ThrowLinkedListException("找不到对应的前置节点");
                return;
            }
            tempNode.Next = node.Next;
        }

        public bool InsertValidation(DoubleNode<T> node, DoubleNode<T> newNode)
        {
            if (node == null)
            {
                ThrowLinkedListException("前置节点不能为空");
                return false;
            }

            if (newNode == null)
            {
                ThrowLinkedListException("插入的节点不能为空");
                return false;
            }

            return true;
        }

        public void ThrowLinkedListException(string msg)
        {
            Console.WriteLine("Exception:{0}", msg);
        }

        public void ThrowSingleLinkedListWarning(string msg)
        {
            Console.WriteLine("Warning:{0}", msg);
        }
    }

    public class DoubleNode<T>
    {
        private T _data;
        private DoubleNode<T> _prev;
        private DoubleNode<T> _next;

        public T Data { get { return _data; } set { _data = value; } }
        public DoubleNode<T> Prev { get { return _prev; } set { _prev = value; } }
        public DoubleNode<T> Next { get { return _next; } set { _next = value; } }

        public DoubleNode(T data, DoubleNode<T> prev,DoubleNode<T> next)
        {
            _data = data;
            _prev = prev;
            _next = next;
        }

        public T GetNode()
        {
            return _data;
        }

        public DoubleNode<T> GetPrevNode()
        {
            return _prev;
        }

        public DoubleNode<T> GetNextNode()
        {
            return _next;
        }
    }
}
