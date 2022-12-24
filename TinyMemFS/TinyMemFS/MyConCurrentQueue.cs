using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyMemFS
{
    public class MyConCurrentQueue
    {
        private ConcurrentQueue<int> concurrentQueuePrior0;
        private ConcurrentQueue<int> concurrentQueuePrior1;
        public long f_maintance = 0;
        private long taskRunning = 0;
        private volatile bool[] flag = new bool[] { false, false };
        private volatile int turn;

        public  MyConCurrentQueue()
        {
            concurrentQueuePrior0 = new ConcurrentQueue<int>();
            concurrentQueuePrior1 = new ConcurrentQueue<int>();
        }

        public void WaitOne(int priority = 0)
        {
            if (priority == 0)
                enterQueue();
            else if (priority == 1)
               enterMaintance();
        }

        public void Release(int priority = 0)
        {
            if (priority == 0)
               exitQueue();
            else if (priority == 1)
               exitMaintance();
        }

        private void enterQueue()
        {
            concurrentQueuePrior0.Enqueue(Thread.CurrentThread.ManagedThreadId);
            int result = -1;
            while (result != Thread.CurrentThread.ManagedThreadId)
                while (!concurrentQueuePrior0.TryPeek(out result))
                    Thread.Sleep(100);
            flag[0] = true;
            turn = 1;
            while (flag[1] == true && turn == 1) ;
            Console.WriteLine($"Thread Left Queue currectly Prior 0 {Thread.CurrentThread.ManagedThreadId}");

            Interlocked.Exchange(ref taskRunning, 1);
        }

        private void exitQueue()
        {
            int result = -1;
            while (!concurrentQueuePrior0.TryDequeue(out result)) ;
            
            if (result == Thread.CurrentThread.ManagedThreadId)
                Console.WriteLine($"Thread Left Queue currectly Prior 0 {Thread.CurrentThread.ManagedThreadId}");
            else
                Console.WriteLine($" Thread Left Queue, Error Prior 0 {result} != {Thread.CurrentThread.ManagedThreadId}");
            flag[0] = false;
            Interlocked.Exchange(ref taskRunning, 0);
        }


        private void enterMaintance()
        {
            concurrentQueuePrior1.Enqueue(Thread.CurrentThread.ManagedThreadId);
            int result = -1;
            while (result != Thread.CurrentThread.ManagedThreadId)
                while (!concurrentQueuePrior1.TryPeek(out result))
                    Thread.Sleep(100);
            flag[1] = true;
            turn = 0;
            while (flag[0] == true && turn == 0) ;
            Console.WriteLine($"Thread Entered Queue currectly Prior 1 {Thread.CurrentThread.ManagedThreadId}");

            Interlocked.Exchange(ref taskRunning, 1);
        }

        private void exitMaintance()
        {
            int result = -1;
            while (!concurrentQueuePrior1.TryDequeue(out result)) ;

            if (result == Thread.CurrentThread.ManagedThreadId)
                Console.WriteLine($"Thread Left Queue currectly Prior 1{Thread.CurrentThread.ManagedThreadId}");
            else
                Console.WriteLine($" Thread Left Queue, Error Prior 1 {result} != {Thread.CurrentThread.ManagedThreadId}");

            flag[1] = false;
            Interlocked.Exchange(ref taskRunning, 0);
        }
    }
}
