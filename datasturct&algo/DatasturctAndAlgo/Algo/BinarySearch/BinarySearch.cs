using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.BinarySearch
{
    /// <summary>
    /// 二分查找
    /// 
    /// </summary>
    public static class BinarySearch
    {
        public static int BinarySearchWithCycle(int[] array,int pointValue)
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


        public static int BinarySearchWithRecursion(int[] array, int pointValue)
        {
            return BinarySearchRecursion(array, 0, array.Length - 1, pointValue);
        }

        private static int BinarySearchRecursion(int[] array, int low, int high, int pointValue)
        {
            if (low >high)
                return -1;

            int mid = low + ((high - low) >>1);
            if (array[mid] == pointValue)
                return mid;
            if (array[mid] < pointValue)
                return BinarySearchRecursion(array, mid + 1, high, pointValue);
            else
                return BinarySearchRecursion(array, low, mid -1, pointValue);
        }
    }
}
