using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerSample
{
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        //是否正在处理的工作项
        private static bool _currentThreadIsProcessingItems;
        //被执行的任务链表
        private readonly LinkedList<Task> tasks = new LinkedList<Task>();  //protected by lock(_task)
        //最大并发数
        private readonly int _maxDegreeofParallelism;
        //当前处理的工作项数量
        private int _delegateQueueOrRunning = 0; //protected by lock(_task)

        /// <summary>
        /// 构造函数限定最大并发数
        /// </summary>
        /// <param name="maxDegreeofParallelism"></param>
        public  LimitedConcurrencyLevelTaskScheduler(int maxDegreeofParallelism)
        {
            if (maxDegreeofParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
            _maxDegreeofParallelism = maxDegreeofParallelism;
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            throw new NotImplementedException();
        }

        protected override void QueueTask(Task task)
        {
            lock (tasks)
            {
                tasks.AddLast(task);
                if (_delegateQueueOrRunning < _maxDegreeofParallelism)
                {
                    _delegateQueueOrRunning++;
                    NotifyThreadPoolOfPendingWork();
                }
            }
        }

        private void NotifyThreadPoolOfPendingWork()
        {

        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            throw new NotImplementedException();
        }
    }
}
