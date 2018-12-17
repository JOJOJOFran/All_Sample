using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BaseDataStruct
{
    /// <summary>
    /// 模拟构建一个自己的泛型List
    /// </summary>
    public class FranList<T>:IList<T>
    {
        //默认容量
        private const int InitCapacity = 4;
        //最大容量
        private const int MaxCapacity = 0X7FEFFFFF;
        private static readonly T[] _emptyArray = new T[0];

        //List的item实际装载的个数，容量可能大于这个数
        private int _count;
        private T[] _items;

        #region 构造函数
        public FranList()
        {
            _items = _emptyArray;
        }

        public FranList(int capacity)
        {
            if (capacity < 0)
            {
                ConsoleException("不能设置小于零的容量,将为您自动修复");
                _items = _emptyArray;
                return;
            }

            if (capacity == 0)
            {
                _items = _emptyArray;
            }
            else
            {
                _items = new T[capacity];
            }
        }

        public FranList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                ConsoleException("传入的集合为Null");
            }

            ICollection<T> list = collection as ICollection<T>;
            if (list != null)
            {
                int count = list.Count;
                if (count == 0)
                {
                    _items = _emptyArray;
                }
                else
                {
                    _items = new T[count];
                    list.CopyTo(_items, 0);
                    //count放在复制完后面赋值，更安全，但是并不能保证线程安全，从这里你可以知道List<T>不是线程安全的
                    //最好还是可以使用原子操作执行这一段，后面尝试写个新的版本
                    _count = count;
                }
            }

        }
        #endregion

        public int Capacity
        {
            get { return _items.Length; }
            set
            {
                if (value < _count)
                {
                    ConsoleException("不能设置比当前List<T>尺寸小的容量,将为您自动修复");
                    value = _count;
                }

                if (value > MaxCapacity)
                {
                    ConsoleException("不能设置超过的MaxCapacity容量,将为您自动修复");
                    value = MaxCapacity;
                }

                if (value != _items.Length)
                {
                    T[] newArray = new T[value];
                    Array.Copy(_items, 0, newArray, 0, _count);
                    _items = newArray;
                }
            }
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                Expand(_count + 1);
            }
            _items[_count++] = item;

        }

        public void Expand(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? InitCapacity : _items.Length * 2;

                if (newCapacity > MaxCapacity)
                {
                    newCapacity = MaxCapacity;
                }

                if (newCapacity < min)
                {
                    newCapacity = min;
                }
                Capacity = newCapacity;
            }

        }

        public T this[int index]
        {
            get { return _items[index]; }
            set
            {
                if (index < 0)
                {
                    ConsoleException("FranList索引不能小于0！");
                }

                if (index > _items.Length)
                {
                    ConsoleException("索引超出了FranList的界限！");
                }

                if (value is T && index > 0 && index <= _items.Length)
                {
                    _items[index] = value;
                }
                
            }
        }


        public void ConsoleException(string msg)
        {
            Console.WriteLine("Exception:{0}", msg);
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
