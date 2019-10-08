using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    class ThreadsSyncNotStatic
    {
        public int x = 0;

        public void RunNotSyncedThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => Count());
            }
            Console.ReadKey();
        }
        public void Count()
        {
            Console.WriteLine($"Thread started:{Thread.CurrentThread.ManagedThreadId}");
            x = 1;
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{x}");
                x++;
                Thread.Sleep(100);
            }
        }





        public object Locker = new object();

        public void RunSyncLockThread()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedCount());
            }
            Console.ReadKey();
        }
        public void SynchronizedCount()
        {
            Console.WriteLine($"Thread started:{Thread.CurrentThread.ManagedThreadId}");
            lock (Locker)
            {
                x = 1;
                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
            for (int j = 0; j < 9; j++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{j}");
                Thread.Sleep(100);
            }
        }




        public object Mon = new object();

        public void RunSyncMonitorThread()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedMonitorCount());
            }
            Console.ReadKey();
        }
        public void SynchronizedMonitorCount()
        {
            Console.WriteLine($"Thread started:{Thread.CurrentThread.ManagedThreadId}");
            try
            {
                Monitor.Enter(Mon);
                x = 1;
                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                Monitor.Exit(Mon);
            }
            for (int j = 0; j < 9; j++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{j}");
                Thread.Sleep(100);
            }
        }





        public Mutex mutex = new Mutex();

        public void RunSyncMutexThread()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedMutexCount());
            }
            Console.ReadKey();
        }
        public void SynchronizedMutexCount()
        {
            Console.WriteLine($"Thread started:{Thread.CurrentThread.ManagedThreadId}");
            mutex.WaitOne();
            x = 1;
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{x}");
                x++;
                Thread.Sleep(100);
            }
            mutex.ReleaseMutex();
            for (int j = 0; j < 9; j++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{j}");
                Thread.Sleep(100);
            }
        }





        public Semaphore semaphore = new Semaphore(0, 4);

        public void RunSyncSemaphoreThread()
        {
            Task.Run(() => ReadFile());
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => ProcessFile());
            }
            Console.ReadKey();
        }
        public void ReadFile()
        {
            Console.WriteLine($"File read started:{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500);
            Console.WriteLine($"File read ended:{Thread.CurrentThread.ManagedThreadId}");
            semaphore.Release(3);
        }
        public void ProcessFile()
        {
            Console.WriteLine($"Waiting for data:{Thread.CurrentThread.ManagedThreadId}");
            semaphore.WaitOne();
            Console.WriteLine($"File processing started:{Thread.CurrentThread.ManagedThreadId}");
            x = 1;
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}:{x}");
                x++;
                Thread.Sleep(100);
            }
            Console.WriteLine($"File processing ended:{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
