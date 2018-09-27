using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDataStruct
{
    /// <summary>
    /// 模拟构建一个自己的泛型List
    /// </summary>
    public class FranList<T>
    {
        //默认容量
        private const int InitCapacity = 4;
        //最大容量
        private const int MaxCapacity = 0X7FEFFFFF;
        private static readonly T[] _emptyArray = new T[0];

        //List的item个数
        private int _count;
        private T[] _items;

        public int Capacity
        {
            get { return _items.Length; }
            set {
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

                if (value > _items.Length)
                {
                    MigrationList(value);
                }
            }
        }


        public int Expand(int capacity)
        {
            MigrationList(capacity);
            return 0;
        }

        public void MigrationList(int newCapacity)
        {

        }

        public void ConsoleException(string msg)
        {
            Console.WriteLine("Exception:{0}", msg);
        }

        

        
    }
}
