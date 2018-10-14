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
        /// 给定一个字符串，找出不含有重复字符的最长子串的长度。
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
            int count =1;
            for (int i = 0; i <arr.Length-1; i++)
            {
                tmp++;
                var a = arr[i].ToString();
                var b = arr[i+1].ToString();
                if (arr[i] == arr[i +1])
                {                  
                    count = Math.Max(count,tmp);
                    tmp =1;
                }
               
            }

            return count;
        }
    }
}
