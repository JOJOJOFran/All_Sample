using System;
using System.Collections.Generic;
using System.Text;

namespace DatasturctAndAlgo.Algo.Sort
{
    public static class Sort
    {
        /// <summary>
        /// 冒泡排序
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

        /// <summary>
        /// 插入排序
        /// T:逐步扩充已比较部分，每次将已比较部分后面的一位，从已比较部分的后到前进行比较，并后移，在最终的位置插入被比较的数（array[i]）
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
    }
}
