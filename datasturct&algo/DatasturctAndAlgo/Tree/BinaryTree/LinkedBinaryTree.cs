using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Tree
{
    public class LinkedBinaryTree
    {
        public TreeNode Root { get; set; }

        public LinkedBinaryTree(TreeNode root)
        {
            Root = root;
        }

        public LinkedBinaryTree(int value)
        {
            Root = new TreeNode(value);
        }

        public void InsertLeft(TreeNode originNode, TreeNode newNode)
        {
            originNode.LeftNode = newNode;
        }

        public void InsertLeft(TreeNode originNode, int value)
        {
            originNode.LeftNode = new TreeNode(value);
        }

        public void InsertRight(TreeNode originNode, TreeNode newNode)
        {
            originNode.RightNode = newNode;
        }

        public void InsertRight(TreeNode originNode, int value)
        {
            originNode.RightNode = new TreeNode(value);
        }

        

    }

    public class TreeNode
    {
        public TreeNode LeftNode { get; set; }
        public TreeNode RightNode { get; set; }
        public int Value { get; set; }

        public TreeNode(int value)
        {
            Value = value;
        }
    }
}
