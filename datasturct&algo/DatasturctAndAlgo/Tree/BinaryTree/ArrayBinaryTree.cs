using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Tree
{
    public class ArrayBinaryTree
    {
        private static readonly int DefaultSize = 7;
        private static readonly int MaxSize = Int32.MaxValue;

        public int?[] Tree { get; private set; } = new int?[2];

        public ArrayBinaryTree(int value)
        {
            Tree[1] = value;
        }

        /// <summary>
        /// 插入左子节点
        /// </summary>
        /// <param name="originIndex"></param>
        /// <param name="value"></param>
        public void InsertLeft(int originIndex, int value)
        {
            LimitsWarning(originIndex, 0);
            ExpandBinaryTree(originIndex*2);
            Tree[2 * originIndex] = value;

        }

        /// <summary>
        /// 插入右子节点
        /// </summary>
        /// <param name="originIndex"></param>
        /// <param name="value"></param>
        public void InsertRight(int originIndex, int value)
        {
            LimitsWarning(originIndex, 1);
            ExpandBinaryTree(2 * originIndex + 1);
            Tree[2 * originIndex+1] = value;

        }

        /// <summary>
        /// 扩容
        /// </summary>
        /// <param name="index"></param>
        public void ExpandBinaryTree(int index)
        {
          
            if (index >= Tree.Length)
            {
                int tmp = index;
                index = Tree.Length == 0 ? DefaultSize : Tree.Length * 2;

                if (index < tmp)
                    index = tmp;

                if ((uint)index > MaxSize)
                    index = MaxSize;
                 
                var tmpArray = new int?[index];
                Array.Copy(Tree, 0, tmpArray, 0, Tree.Length); 
                Tree = tmpArray;
            }
        }

        /// <summary>
        /// 数组索引边界检查
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type">0代表左插入，1代表右插入</param>
        public void LimitsWarning(int index,int type)
        {
            if (index < 1)
                throw new OutOfMemoryException("索引不能小于1");
            if (index > (type<=1?MaxSize / 2: (MaxSize / 2-1)))
                throw new OutOfMemoryException("索引超过最大限制，不允许继续插入数据");
        }
    }
}
