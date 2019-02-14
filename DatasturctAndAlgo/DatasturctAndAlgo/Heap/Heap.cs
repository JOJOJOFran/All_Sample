using System;
using System.Collections.Generic;
using System.Text;

 namespace Heap
{
    //实现堆
    public class Heap
    {
        private int[] array; //从下标1开始存储数据      
        public int Count{get;private set;} //数组有的个数    
        public int Capcity{get{return array==null?0:array.Length;}} //堆的容量

        public int? this[int index]
        {
            get
            {
                if(index<1||index>Count)
                    return null;
                return array[index];
            }
        }

        public Heap(int capcity)
        {
            if(capcity<=1)
                return;          
            array=new int[capcity-1];
        }

        public void Insert(int value)
        {
            if(Count>=Capcity)
                return;
            
            int i=Count;
            Count++;
            while(i/2>0&&array[i]>array[i/2])
            {
                array.Swap(i,i/2);
                i=i/2;          
            }
        }      

        public int? PopTop()
        {
            if(Count<=0)
                return null;
            
            int retValue=array[1];
            array[1]=array[Count];
            Count--;
            array.HeapifyTop(1,Count);
            return retValue;
        }

      
    }
}