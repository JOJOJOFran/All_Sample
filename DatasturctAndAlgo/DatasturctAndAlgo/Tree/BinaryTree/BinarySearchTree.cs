using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Tree.BinaryTree
{
    /// <summary>
    /// 二叉查找树
    /// </summary>
    public abstract class BinarySearchTree 
    {
        public SearchTreeNode Root { get; private set; }

        public BinarySearchTree(int value)
        {
            Root = new SearchTreeNode(value);
        }

        public void Insert(int data)
        {
            if (Root == null)
            {
                Root = new SearchTreeNode(data);
            }

            var node = Root;
            while (node != null)
            {
                //这里暂且把等于该节点的值也插入左节点
                if (data < node.Value)
                {
                    if (node.LeftNode == null)
                    {
                        node.LeftNode = new SearchTreeNode(data);
                        return;
                    }
                    node = node.LeftNode;

                }
                else if (data == node.Value && node.IsDeleted)
                {
                    node.IsDeleted = false;
                }
                else if (data > node.Value)
                {
                    if (node.RightNode == null)
                    {
                        node.RightNode = new SearchTreeNode(data);
                        return;
                    }
                    node = node.RightNode;
                }
               
            }             
        }

        public SearchTreeNode Find(int data)
        {
            if (Root == null)
                return null;

            var node = Root;
            while (node != null)
            {
                if (data < node.Value)
                {
                    node = node.LeftNode;
                }
                else if (data > node.Value)
                {
                    node = node.RightNode;
                }
                else if(data==node.Value&&!node.IsDeleted)
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="data"></param>
        public void MarkDelete(int data)
        {
            if (Root == null)
                return;

            var node = Root;
            while (node != null)
            {
                if (data < node.Value)
                {
                    node = node.LeftNode;
                }
                else if (data == node.Value && !node.IsDeleted)
                {
                    node.IsDeleted = true;
                }
                else if(data >node.Value)
                {
                    node = node.RightNode;
                }
            }
        }

        public void Delete(int data)
        {
            if (Root == null)
                return;

            var node = Root;
            var nextNode = node;
            while (node != null)
            {
                if (data < node.Value)
                {

                }
            }
        }


    }
}
