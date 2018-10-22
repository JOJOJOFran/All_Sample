using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.LeetCode
{
    static class LeetCode
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        /// <summary>
        /// LeetCode 1
        /// Q:给定一个整数数组和一个目标值，找出数组中和为目标值的两个数
        ///   你可以假设每个输入只对应一种答案，且同样的元素不能被重复利用。
        ///   给定 nums = [2, 7, 11, 15], target = 9
        ///   所以返回 [0, 1]
        /// T:每次循环判断哈希表中是否存在 （目标值-当前数组值）,将数组的值和下标存到哈希表中，以减少查找的时间
        ///   需要注意的是，将数组的值存在键，数组值对应的下标存到值中
        ///   并且，需要注意数组中可能存在重复的值，所以插入到哈希表中需要判断是否已经存在重复的键
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TowSum(int[] nums, int target)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int[] array = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                int result = target - nums[i];
                if (dic.ContainsKey(result))
                {
                    return new[] { dic[result], i };
                }
                if (!dic.ContainsKey(nums[i]))
                dic.Add(nums[i], i);

            }
            return null;
        }

        /// <summary>
        ///  LeetCode 3
        /// 给定一个字符串，找出不含有重复字符的最长子串的长度。？？
        /// 输入: "abcabcbb"
        /// 输出: 3 
        /// 解释: 无重复字符的最长子串是 "abc"，其长度为 3。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            char[] arr = s.ToCharArray();
            int tmp = 1;
            int count = 1;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                tmp++;
                var a = arr[i].ToString();
                var b = arr[i + 1].ToString();
                if (arr[i] == arr[i + 1])
                {
                    count = Math.Max(count, tmp);
                    tmp = 1;
                }

            }

            return count;
        }


        #region 链表问题
        /// <summary>
        /// LeetCode 2
        /// 给定两个非空链表来表示两个非负整数。位数按照逆序方式存储，它们的每个节点只存储单个数字。将两数相加返回一个新的链表。
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode headNode = new ListNode(0);
            ListNode tmpNode = headNode;
            int sum = 0;
            int tmp = 0;
            while (l1 != null || l2 != null)
            {
                //sum=
            }

            return headNode;
        }

        /// <summary>
        /// leetcode 21
        /// Q:将两个有序链表合并为一个新的有序链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。
        /// 输入：1->2->4, 1->3->4
        /// 输出：1->1->2->3->4->4
        /// T:重点：这是两个有序的，所以当一个值比另一个链表的值大的时候，我们只用关心当前比较小的这个链表，直到值反超，再换链表插入
        ///   也就是说一次只需要插入一个节点，也只用位移一个链表
        ///   然后就是边界情况的考虑，这个比较简单，就不细说
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode MergeTwoList(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
            {
                return null;
            }

            ListNode dummyNode = new ListNode(0);
            ListNode tmpNode = dummyNode;
            while (l1 != null || l2 != null)
            {

                if (l1 != null && l2 == null)
                {
                    tmpNode.next = l1;
                    break;
                }

                if (l2 != null && l1 == null)
                {
                    tmpNode.next = l2;
                    break;
                }

                tmpNode.next = new ListNode(0);
                if (l1.val < l2.val)
                {
                    tmpNode.next.val = l1.val;
                    l1 = l1.next;
                }
                else
                {
                    tmpNode.next.val = l2.val;
                    l2 = l2.next;
                }
                tmpNode = tmpNode.next;

            }

            return dummyNode.next;
        }

        /// <summary>
        /// LeetCode 141
        /// Q:给定一个链表，判断链表中是否有环。
        /// T:用快慢节点，如果有环，一定会出现快节点和慢节点重合的时候
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return false;
            }
            ListNode fastNode = head;
            ListNode slowNode = head;

            while (fastNode != null && fastNode.next != null && slowNode != null)
            {
                fastNode = fastNode.next.next;
                slowNode = slowNode.next;
                if (fastNode == slowNode)
                {
                    return true;
                }
            }

            return false;
        }
            #endregion





        }
}
