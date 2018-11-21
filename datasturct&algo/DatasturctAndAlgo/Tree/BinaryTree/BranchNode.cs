using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Tree.BinaryTree
{
    public interface ITreeNodeOperation
    {
        void SetLeftNode(int value);
        void SetRightNode(int value);

    }

    public abstract class BaseTreeNode : ITreeNodeOperation
    {
        public abstract void SetLeftNode(int value);


        public abstract void SetRightNode(int value);

    }

    public class BranchNode : BaseTreeNode
    {
        public BranchNode LeftNode { get; private set; }
        public BranchNode RightNode { get; private set; }
        public int Value { get; set; }

        public BranchNode(int value)
        {
            Value = value;
        }

        public sealed override void SetLeftNode(int value)
        {
            LeftNode = new BranchNode(value);
        }


        public sealed override void SetRightNode(int value)
        {
            LeftNode = new BranchNode(value);
        }
    }

    public class SearchTreeNode
    {
        private SearchTreeNode _leftNode;
        private SearchTreeNode _rightNode;
        public SearchTreeNode LeftNode
        {
            get { return _leftNode; }
            set
            {
                if (value.Value <=this.Value)
                    _leftNode = value;
                else
                    Console.WriteLine("插入值:{0}大于当前节点值，不允许插入左子节点", value);
            }
        }

        public SearchTreeNode RightNode
        {
            get { return _rightNode; }
            set
            {
                if (value.Value > this.Value)
                    _rightNode = value;
                else
                    Console.WriteLine("插入值:{0}小于当前节点值，不允许插入右子节点", value);
            }
        }
        public int Value { get; set; }

        public SearchTreeNode(int value)
        {
            Value = value;
        }
    }
}
