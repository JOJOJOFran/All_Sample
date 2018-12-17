using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.BinarySearch
{
    /// <summary>
    /// 二分查找
    /// </summary>
    public static class BinarySearch
    {
        #region 二分查找（循环）
        /// <summary>
        ///  二分查找（循环）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        public static int BinarySearchWithCycle(int[] array, int pointValue)
        {
            int start = 0;
            int end = array.Length - 1;
            while (start <= end)
            {
                //不用start+end是防止溢出
                int mid = start + (end - start) / 2;
                if (array[mid] == pointValue)
                {
                    return mid;
                }

                if (array[mid] < pointValue)
                {
                    start = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }
            }
            return -1;
        }
        #endregion

        #region 二分查找（递归）
        /// <summary>
        /// 二分查找（递归）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        public static int BinarySearchWithRecursion(int[] array, int pointValue)
        {
            return BinarySearchRecursion(array, 0, array.Length - 1, pointValue);
        }

        private static int BinarySearchRecursion(int[] array, int low, int high, int pointValue)
        {
            if (low > high)
                return -1;

            int mid = low + ((high - low) >> 1);
            if (array[mid] == pointValue)
                return mid;
            if (array[mid] < pointValue)
                return BinarySearchRecursion(array, mid + 1, high, pointValue);
            else
                return BinarySearchRecursion(array, low, mid - 1, pointValue);
        }
        #endregion

        #region 二分查找变种问题
        /// <summary>
        /// 二分查找：查找第一个值等于给定值的元素
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        public static int BinarySearchToFindFristPoint(int[] array, int pointValue)
        {
            int start = 0;
            int end = array.Length - 1;
            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (array[mid] < pointValue)
                {
                    start = mid + 1;
                }

                if (array[mid] > pointValue)
                {
                    end = mid - 1;
                }

                if (array[mid] == pointValue)
                {
                    if (mid < 1 || array[mid - 1] < pointValue)
                    {
                        return mid;
                    }
                    else
                    {
                        end = mid - 1;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 二分查找：查找第一个大于等于给定值的元素
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        public static int BinarySearchToFindFristNotLessThenPoint(int[] array, int pointValue)
        {
            int start = 0;
            int end = array.Length - 1;
            while (start <= end)
            {
                int mid = start + ((end - start) >> 1);
                if (array[mid] >= pointValue)
                {
                    if (mid == array.Length - 1 || array[mid - 1] < pointValue)
                        return mid;
                    else
                        end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// 二分查找：查找第一个大于等于给定值的元素(递归)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        public static int BinarySearchToFindFristNotLessThenPointByRecursion(int[] array, int pointValue, int start, int end)
        {
            int mid = start + ((end - start) >> 1);

            if (start > end)
                return -1;
            if (array[mid] >= pointValue)
            {
                if (mid == array.Length - 1 || array[mid - 1] < pointValue)
                    return mid;
                else
                    return BinarySearchToFindFristNotLessThenPointByRecursion(array, pointValue, start, mid - 1);
            }
            else
                return BinarySearchToFindFristNotLessThenPointByRecursion(array, pointValue, mid + 1, end);


        }

        /// <summary>
        /// 查找最后一个值等于给定值的元素
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        public static int BinarySearchLastPoint(int[] array, int pointValue)
        {
            int start = 0;
            int end = array.Length - 1;
            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (array[mid] < pointValue)
                {
                    start = mid + 1;
                }

                if (array[mid] > pointValue)
                {
                    end = mid - 1;
                }

                if (array[mid] == pointValue)
                {
                    if (mid == array.Length - 1 || array[mid + 1] > pointValue)
                    {
                        return mid;
                    }
                    else
                    {
                        start = mid + 1;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 二分查找：查找最后一个大于等于给定值的元素
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        public static int BinarySearchFindLastNotMoreThanPoint(int[] array, int pointValue)
        {
            int start = 0;
            int end = array.Length - 1;
            while (start < end)
            {
                int mid = start + ((end - start) >> 1);
                if (array[mid] <= pointValue)
                {
                    if (mid == 0 || array[mid - 1] < pointValue)
                        return mid;
                    else
                        start = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }
            }
            return -1;
        }
        #endregion
    }
}
