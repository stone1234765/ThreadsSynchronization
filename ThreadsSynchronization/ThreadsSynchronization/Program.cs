using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadsSync.RunSyncSemaphoreThread();

            ThreadsSyncNotStatic threadsSyncNotStatic = new ThreadsSyncNotStatic();
            threadsSyncNotStatic.RunNotSyncedThreads();

            bool existed;
            string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            Mutex mutexObj = new Mutex(true, guid, out existed);

            if (existed)
            {
                Console.WriteLine("Application is working");
            }
            else
            {
                Console.WriteLine("Application is already running. Shutdown in 3 seconds");
                Thread.Sleep(3000);
                return;
            }
            Console.ReadKey();
        }
    }
}
