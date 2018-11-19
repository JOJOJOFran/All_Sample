using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.StackAndQueue
{
    /// <summary>
    /// 简单实现顺序存储队列
    /// </summary>
    public class EasyArrayQueue
    {
        private string[] array;
        private int head = 0;
        private int tail = 0;

        public EasyArrayQueue(int capacity)
        {
            array = new string[capacity];
        }

        public bool Push(string value)
        {
            if (tail == array.Length)               
                return false;

            array[tail] = value;
            tail++;
            return true;
        }

        public string Pop()
        {
            if (head == tail)
                return "";
            if (array.Length < 1)
                return null;

            string ret= array[head];
            head++;
            return ret;
            

        }
    }
}
