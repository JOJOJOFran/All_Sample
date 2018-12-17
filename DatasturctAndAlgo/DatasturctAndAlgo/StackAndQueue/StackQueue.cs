using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.StackAndQueue
{
    /// <summary>
    /// 用栈实现一个队列
    /// </summary>
    public class StackQueue
    {
        private Stack<int> inStack;
        private Stack<int> outStack;
        /** Initialize your data structure here. */
        public StackQueue()
        {
            inStack = new Stack<int>();
            outStack = new Stack<int>();
        }

        /** Push element x to the back of queue. */
        public void Push(int x)
        {
            inStack.Push(x);
        }

        /** Removes the element from in front of queue and returns that element. */
        public int Pop()
        {
            if (outStack.Count == 0)
            {
                //这里不能用for循环,stack pop后count会变小
                while (inStack.Count > 0)
                {
                    outStack.Push(inStack.Pop());
                }
            }


            return outStack.Pop();
        }

        /** Get the front element. */
        public int Peek()
        {
            if (outStack.Count == 0)
            {

                //这里不能用for循环,stack pop后count会变小
                while (inStack.Count > 0)
                {
                    outStack.Push(inStack.Pop());
                }
            }
            return outStack.Peek();
        }

        /** Returns whether the queue is empty. */
        public bool Empty()
        {
            if (outStack.Count == 0 && inStack.Count == 0)
            {
                return true;
            }
            return false;
        }

    }
}
