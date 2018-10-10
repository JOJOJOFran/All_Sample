using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.LinkedList
{
    public class SingleLinkedList<T>
    {
        private Node<T> _head;
        //private const int MaxSize = 10000;
        //private int _size;

        public Node<T> Head { get { return _head; } set { _head = value; } }


        public SingleLinkedList()
        {
            _head = null;
         
        }

        public SingleLinkedList(Node<T> head)
        {
            _head = head;
            _head.Next = head;
        }

        public T FindDataByValue(T value)
        {
            var tempNode = _head;
            while (!tempNode.Data.Equals(value) && tempNode != null)
            {
                tempNode = tempNode.Next;
            }
            return tempNode.Data;
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

        public Node<T> FindNodeByValue(T value)
        {
            var tempNode = _head;
            while (!tempNode.Data.Equals(value) && tempNode != null)
            {
                tempNode = tempNode.Next;
            }
            return tempNode;
        }

        public Node<T> FindNodeByIndex(int index)
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

        public void InsertHead(T value)
        {
            Node<T> newNode = new Node<T>(value, null);
            InsertHead(newNode);

        }

        public void InsertHead(Node<T> head)
        {
            if (head == null)
            {
                _head = head;
            }
            else
            {
                head.Next=  _head;
                _head = head;
            }
        }

        public void InsertAfter(Node<T> node, T value)
        {
            if (node == null)
            {
                ThrowLinkedListException("前置节点不能为空");
                return;
            }
            Node<T> newNode = new Node<T>(value, node.Next);
            node.Next = newNode;
        }

        public void InsertAfter(Node<T> node, Node<T> newNode)
        {
            if (!InsertValidation(node, newNode))
            {
                return;
            }

            newNode.Next = node.Next;
            node.Next = newNode;
        } 
       
         
        public void InsertBefore(Node<T> node, Node<T> newNode)
        {
            if (!InsertValidation(node, newNode))
            {
                return;
            }

            var tempNode = _head;
            while (tempNode != null&&tempNode.Next!= node && tempNode.Next!=null)
            {
                tempNode = tempNode.Next;
                if (tempNode == null || tempNode.Next==null)
                {
                    ThrowLinkedListException("找不到对应的前置节点");
                    return;
                }

            }
            tempNode.Next = newNode;
            newNode.Next = node;
        }

        public void Delete(Node<T> node)
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
                if (tempNode == null || tempNode.Next == null)
                {
                    ThrowLinkedListException("找不到对应的前置节点");
                    return;
                }
            }
            tempNode.Next = node.Next;
        }

        public void DeleteByIndex(int index)
        {
            var tempNode = FindNodeByIndex(index);
            if (tempNode == null)
            {
                ThrowLinkedListException("找不到对应的节点");
                return;
            }
        }

       

        public void ShowAll()
        {
            var tempNode = _head;
            while (tempNode != null && tempNode.Next != null)
            {
                Console.WriteLine(tempNode.Data);
                tempNode = tempNode.Next;
            }
        }

      

        public List<T> ToList()
        {
            List<T> list = new List<T>();
            if (_head == null)
            {

            }
            else
            {
                var tmp = _head;
                while ( tmp != null)
                {
                    list.Add(tmp.Data);
                    tmp = tmp.Next;
                }
            }
            
            return list;
        }

        public bool InsertValidation(Node<T> node, Node<T> newNode)
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
            Console.WriteLine("Exception:{0}",msg);
        }

        public void ThrowSingleLinkedListWarning(string msg)
        {
            Console.WriteLine("Warning:{0}", msg);
        }
    }

    public class Node<T>
    {
        private T _data;
        private Node<T> _next;

        public T Data { get { return _data; } set { _data = value; } }
        public Node<T> Next { get { return _next; } set { _next = value; } }

        public Node(T data, Node<T> next )
        {
            _data = data;
            _next = next;
        }

        public T GetNode()
        {  
            return _data;
        }

        public Node<T> GetNextNode()
        {
            return _next;
        }


    }
}
