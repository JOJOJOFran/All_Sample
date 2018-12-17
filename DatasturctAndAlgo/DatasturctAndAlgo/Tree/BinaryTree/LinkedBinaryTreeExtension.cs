using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Tree.BinaryTree
{
    /// <summary>
    /// 链式二叉树扩展方法
    /// </summary>
    public static class LinkedBinaryTreeExtension
    {
        /// <summary>
        /// 前序遍历整树（递归）
        /// </summary>
        /// <param name="tree"></param>
        public static void PreTraverseAllTreeByRecursion(this LinkedBinaryTree tree)
        {
            if (tree == null || tree.Root == null)
                return;
            PreTraverse(tree.Root);
        }

        /// <summary>
        /// 中序遍历整树（递归）
        /// </summary>
        /// <param name="tree"></param>
        public static void MidTraverseAllTreeByRecursion(this LinkedBinaryTree tree)
        {
            if (tree == null || tree.Root == null)
                return;
            MidTraverse(tree.Root);
        }

        /// <summary>
        /// 后序遍历整树（递归）
        /// </summary>
        /// <param name="tree"></param>
        public static void PostTraverseAllTreeByRecursion(this LinkedBinaryTree tree)
        {
            if (tree == null || tree.Root == null)
                return;
            PostTraverse(tree.Root);
        }

        /// <summary>
        /// 层级遍历整树（非递归）
        /// </summary>
        /// <param name="tree"></param>
        public static void LevelTraverseAllTreeNoRecursion(this LinkedBinaryTree tree)
        {
            if (tree == null || tree.Root == null)
                return;
            LevelTraverseNoRecursion(tree.Root);
        }

        /// <summary>
        /// 前序遍历（递归）
        /// </summary>
        /// <param name="node"></param>
        public static void PreTraverse(this TreeNode node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Value);
                PreTraverse(node.LeftNode);
                PreTraverse(node.RightNode);
            }
        }

        /// <summary>
        /// 中序遍历（递归）
        /// </summary>
        /// <param name="node"></param>
        public static void MidTraverse(this TreeNode node)
        {
            if (node != null)
            {
                PreTraverse(node.LeftNode);
                Console.WriteLine(node.Value);
                PreTraverse(node.RightNode);
            }
        }

        /// <summary>
        /// 后序遍历（递归）
        /// </summary>
        /// <param name="node"></param>
        public static void PostTraverse(this TreeNode node)
        {
            if (node != null)
            {
                PreTraverse(node.LeftNode);
                PreTraverse(node.RightNode);
                Console.WriteLine(node.Value);
            }
        }

        /// <summary>
        /// 层级遍历（非递归）
        /// </summary>
        /// <param name="node"></param>
        public static void LevelTraverseNoRecursion(this TreeNode node)
        {
            if (node == null)
                return;

            List<TreeNode> dataList = new List<TreeNode>(100);
            dataList.Add(node);
            int flag = 0;
            int index = 0;

            while (index < flag && dataList[index] != null)
            {
                if (dataList[index].LeftNode != null)
                {
                    dataList.Add(dataList[index].LeftNode);
                    flag++;
                }

                if (dataList[index].RightNode != null)
                {
                    dataList.Add(dataList[index].RightNode);
                    flag++;
                }
                index++;
            }

            foreach (var item in dataList)
            {
                Console.WriteLine(item.Value);
            }


        }
    }
}
