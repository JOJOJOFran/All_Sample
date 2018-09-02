using System;
using System.Threading;

namespace Interlocked_sample
{
    internal class AsyncCoordinator
    {
        //AllBegun 内部调用JustEnded递减它
        private int m_opCount = 1;
        private int m_statusReported = 0;
        private Action<CoordinationStatus> m_callback;
        private Timer m_timer;

        internal void AboutToBegin(int opsToAdd=1)
        {
            //将两个参数的和代替第一个参数，并且是原子操作
            Interlocked.Add(ref m_opCount, opsToAdd);

        }

        internal void AllBegun(Action<CoordinationStatus> callback, int timeout=Timeout.Infinite)
        {
            m_callback = callback;
            if (timeout != Timeout.Infinite)
            {
                m_timer = new Timer(TimeExpired, null, timeout, Timeout.Infinite);
            }
            JustEnded();
        }

        private void TimeExpired(object state)
        {
            ReportStatus(CoordinationStatus.TimeOut);
        }

       
        internal void JustEnded()
        {
            Console.WriteLine("JustEnded,{0}", m_opCount);
            //Interlocked.Decrement(ref m_opCount);
           //var result= --m_opCount;
            if (Interlocked.Decrement(ref m_opCount) == 0)
            {
                ReportStatus(CoordinationStatus.AllDone);
            }
            //Console.WriteLine(result);
        }

        private void ReportStatus(CoordinationStatus status)
        {
            if (Interlocked.Exchange(ref m_statusReported, 1) == 0)
            {
                m_callback(status);
            }
        }


        internal void Cancle()
        {
            ReportStatus(CoordinationStatus.Cancel);
        }
    }
}