using System;
using System.Threading;

namespace Interlocked_sample
{
    public class InterlockTest
    {
        #region Interlockd简单使用
        public static class CouterBase
        {
            public abstract void Increment();

            public abstract void Decrement();
        }
        static void TestCounter(CouterBase c)
        {

        }

        public static class Counter:CouterBase
        {
            private int _count;

            public int Count=>_count;

            public override void Increment()
            {
                _count++;
            }

            public override void Decrement()
            {
                _count--;
            }
            
        }

        public static class CounterWithInterlock:CouterBase
        {
            private int _count;

            public int Count=>_count;

            public override void Increment()
            {
                Interlocked.Increment(_count);
            }

            public override void Decrement()
            {
                Interlocked.Decrement(_count);
            }

        }
        #endregion

        #region 
        #endregion
    }
}