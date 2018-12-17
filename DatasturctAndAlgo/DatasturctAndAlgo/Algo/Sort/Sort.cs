using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.Sort
{
    public static class Sort
    {
        #region 复杂度O(n2)的排序
        #region 冒泡排序
        /// <summary>
        /// 冒泡排序
        /// 时间复杂度：O(n2)
        /// 原地排序：是（即空间复杂度O（1））
        /// 稳定排序：是
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static int[] BubbleSort(int[] array)
        {

            for (int i = 0; i < array.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    //每进行一次完整的冒泡，后面的一定是最大的数
                    //所以冒泡进行的比较数，可以逐次递减
                    if (array[j] > array[j + 1])
                    {
                        int tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                        flag = true;
                    }
                    
                }
                //如果没有交换发生，说明已经是有序的，可以提前结束冒泡
                if (!flag)
                {
                    return array;
                }
            }
            return array;
        }
        #endregion

        #region 插入排序
        /// <summary>
        /// 插入排序
        /// T:逐步扩充已比较部分，每次将已比较部分后面的一位，从已比较部分的后到前进行比较，并后移，在最终的位置插入被比较的数（array[i]）
        /// 时间复杂度：O(n2)
        /// 原地排序：是（即空间复杂度O（1））
        /// 稳定排序：是
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static int[] InsertSort(int[] array)
        {
            //从下标一开始，默认第一个元素作为初始的已排序部分
            for (int i = 1; i < array.Length; i++)
            {
                int tmp = array[i];
                int j = i - 1;

                for (; j >= 0; j--)
                {
                    //如果以排序部分有比比较位大的，则后移一位
                    //不要和array[j + 1]比了
                    if (array[j] > tmp)
                    {
                        array[j + 1] = array[j];
                    }
                    else
                    {
                        break;
                    }
                }
                //记录已排序数组中的最后位置，将比较的值插到该位置
                //跳出循环时，j会多自减一次，所以加上一个1
                array[j+1] = tmp;
            }
            return array;
        }
        #endregion

        #region 选择排序
        /// <summary>
        /// 选择排序
        /// 其实思路都和上面类似，主要就是从（0作为初）1开始的未排序部分依次找到最小的，放到i位置逐渐扩大排序部分
        /// 时间复杂度：O(n2)
        /// 原地排序：是（即空间复杂度O（1））
        /// 稳定排序：否
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] SelectSorted(int[] array)
        {
            int tmp = 0;
            for (int i = 0; i < array.Length; i++)
            {
                int min = array[i];
                int minIndex = i;
                for (int j = i+1; j < array.Length; j++)
                {
                    if (array[j]< min)
                    {
                        min = array[j];
                        minIndex = j;
                    }
                }
                array[minIndex] = array[i];
                array[i] = min;
            }
            return array;
        }
        #endregion
        #endregion

        #region 复杂度O(nlogn)的排序
        #region 归并排序
        /// <summary>
        /// 归并排序
        /// 不稳定，
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] MergeSort(int[] array)
        {
            MergeSortRecursive(array, 0, array.Length - 1);
            return array;
        }

      

        private static void MergeSortRecursive(int[] array,int start,int end)
        {
            if (start < end)
            {
                //整形除不尽会舍
                int mid = (start + end) / 2;
                //拆分前半部
                MergeSortRecursive(array, start, mid);
                //拆分后半部
                MergeSortRecursive(array, mid + 1,end);
                MergeSortUnion(array, start, end, mid);
            }

           
        }

        private static void MergeSortUnion(int[] array, int start, int end, int mid)
        {
            int startIndex = start; //拆分左侧的起始位置
            int endIndex = mid+1; //拆分右侧的起始位置
            int count = 0;
            int[] tmpArray = new int[end+1]; //定义一个等长的临时数组

            while (startIndex <= mid && endIndex <= end)
            {
                //将小的一个插入临时数组，并将小的那一边的指针后移一位（同样临时数组插入时，指针也后移以便下一个位置插入元素）
                if (array[startIndex] <= array[endIndex])
                {
                    tmpArray[count++] = array[startIndex++];  
                }
                else
                {
                    tmpArray[count++] = array[endIndex++];
                }
            }

            //下面两个while是避免，上面有一边出现未遍历的数据，插入到临时数组中
            while (startIndex <= mid)
            {
                tmpArray[count++] = array[startIndex++];
            }

            while (endIndex <= end)
            {
                tmpArray[count++] = array[endIndex++];
            }

            //将临时数组转入按顺序转入到原数组中，达到排序的效果
            count = 0;
            for (int i = start; i <= end; i++)
            {
                array[i] = tmpArray[count++];
            }

        }
        #endregion

        #region 快速排序
        /// <summary>
        /// 快速排序
        /// 不稳定，原地排序
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] QuickSort(int[] array)
        {
            QuickSortRecursive(array, 0, array.Length - 1);
            return array;
        }

        private static void QuickSortRecursive(int[] array, int start, int end)
        {
            if (start < end)
            {
                int pivotIndex= QuickSortPartion(array, start, end);
                QuickSortRecursive(array, start, pivotIndex - 1);
                QuickSortRecursive(array, pivotIndex + 1,end);
            }
        }

        private static int QuickSortPartion(int[] array, int start, int end)
        {
            int pivot = array[end];
            int pivotIndex = start;
            for (int i = start; i < end; i++)
            { 
                if (array[i] < pivot)
                {
                    int tmp = array[pivotIndex];
                    array[pivotIndex] = array[i];
                    array[i] = tmp;
                    pivotIndex++;
                }
            }

            int flag = array[pivotIndex];
            array[pivotIndex] = pivot;
            array[end] = flag;
            return pivotIndex;
        }
        #endregion
        #endregion
    }
}
