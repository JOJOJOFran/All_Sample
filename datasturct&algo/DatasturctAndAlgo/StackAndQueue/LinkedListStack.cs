using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.StackAndQueue
{
    public class LinkedListStack<T>
    {

    }

    public class StackNode<T>
    {
        private T _data;
        private StackNode<T> _next;

        public T Data { get { return _data; } set { _data = value; } }
        public StackNode<T> Next { get { return _next; } set { _next = value; } }

        public StackNode(T data, StackNode<T> next)
        {
            _data = data;
            _next = next;
        }
    }
}
