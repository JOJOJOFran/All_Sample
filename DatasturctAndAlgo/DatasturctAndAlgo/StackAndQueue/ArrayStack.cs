using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.StackAndQueue
{
    public class ArrayStack<T>
    {
        private T[] _items;
        private int _count;


        private int InitSize = 4;
        private int MaxSize = 1000;


        public int Count { get { return _count; } }

        public ArrayStack(int n)
        {
            // if(n<=0)
        }

        public int Size
        {
            get { return _items.Length; }
            set
            {
                if (value < _count)
                {
                    value = _count;
                }

                if (value > MaxSize)
                {
                    value = MaxSize;
                }

                if (value > _items.Length)
                {
                    T[] newArray = new T[value];
                    Array.Copy(_items, 0, newArray, 0, _count);
                    _items = newArray;
                }

            }
        }

        public void Push(T value)
        {
            EnsureCapacity(_count + 1);
            _items[_count + 1] = value;
            _count++;
        }

        public T Pop()
        {
            if (_count == 0)
            {
                return default(T);
            }
              
            var tmp = _items[_count - 1];
            _count--;
            return tmp;
        }
        /// <summary>
        /// 扩容
        /// </summary>
        /// <param name="newCapacity"></param>
        public void EnsureCapacity(int newCapacity)
        {
           
            if (newCapacity > _items.Length)
            {
                newCapacity = _items.Length==0?InitSize:2 * newCapacity;
                if (newCapacity > MaxSize)
                {
                    newCapacity = MaxSize;
                }
                T[] newArray = new T[newCapacity];
                Array.Copy(_items, 0, newArray, 0, _count);
                _items = newArray;
            }
            
        }
    }





}
