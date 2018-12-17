using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public static class HeapExtension
    {
       public static  void Swap(this int[] arr,int index1,int index2)
        {
            int tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }

        public static void BuildHeap(this int[] arr) 
        {
            for(int i=(arr.Length-1)/2;i>1;i--)
            {
                HeapifyFromTop(arr, i, arr.Length-1);
            }
        }


        public static void HeapifyTop(this int[] arr, int start, int end) 
        {
            HeapifyFromTop(arr, start, end);
        }
        private static void HeapifyFromTop(int[] array, int start, int end)
        {
            while (true)
            {
                int tmp = start;
                if (start * 2 <= end && array[start] < array[start * 2])
                    tmp = 2 * start;
                if (start * 2 + 1 <= end && array[start * 2] < array[start * 2 + 1])
                    tmp = 2 * start + 1;
                if (tmp == start)
                    break;

                Swap(array,start, tmp);
                start = tmp;
            }
        }

        public static void HeapifyBottom(this int[] arr, int start, int end)
        {
            HeadpifyFromBottom(arr, start, end);
        }

        private static void HeadpifyFromBottom(int[] array, int end, int start) 
        {
            while (true) 
            {
                int tmp = end;
                if (end / 2 >= start && array[end] < array[end / 2])
                    tmp = end >> 1;
                if ((end - 1) / 2 >= start && array[end / 2] > array[(end - 1) / 2])
                    tmp = (end - 1) / 2;
                if (tmp == end)
                    break;

                Swap(array, start, end);
                end = tmp;
            }
        }
    }
}