using System;
using System.Threading;

namespace ThreadPriorityDemo
{
    class MyThread
    {
        public int Count;
        public Thread Thrd;
        static bool stop = false;
        static string currentName;

        public MyThread(string name)
        {
            Count = 0;
            Thrd = new Thread(Run);
            Thrd.Name = name;
            currentName = name;
        }

        void Run()
        {
            Console.WriteLine("Thread " + Thrd.Name + " is starting.");
            do
            {
                Count++;
                if (currentName != Thrd.Name)
                {
                    currentName = Thrd.Name;
                    Console.WriteLine("In thread " + currentName);
                }
            } while (!stop && Count < 1e6);
            stop = true;
            Console.WriteLine("Thread " + Thrd.Name + " has finished.");
        }
    }

    class Program
    {
        static void Main()
        {
            MyThread mt1 = new MyThread("Highest Priority Thread");
            MyThread mt2 = new MyThread("Lowest Priority Thread");
            MyThread mt3 = new MyThread("Above Normal Priority Thread");
            MyThread mt4 = new MyThread("Normal Priority Thread");
            MyThread mt5 = new MyThread("Below Normal Priority Thread");

            mt1.Thrd.Priority = ThreadPriority.Highest;
            mt2.Thrd.Priority = ThreadPriority.Lowest;
            mt3.Thrd.Priority = ThreadPriority.AboveNormal;
            mt4.Thrd.Priority = ThreadPriority.Normal;
            mt5.Thrd.Priority = ThreadPriority.BelowNormal;

            mt1.Thrd.Start();
            mt2.Thrd.Start();
            mt3.Thrd.Start();
            mt4.Thrd.Start();
            mt5.Thrd.Start();

            mt1.Thrd.Join();
            mt2.Thrd.Join();
            mt3.Thrd.Join();
            mt4.Thrd.Join();
            mt5.Thrd.Join();

            Console.WriteLine();
            Console.WriteLine("Thread " + mt1.Thrd.Name + " counted to " + mt1.Count);
            Console.WriteLine("Thread " + mt2.Thrd.Name + " counted to " + mt2.Count);
            Console.WriteLine("Thread " + mt3.Thrd.Name + " counted to " + mt3.Count);
            Console.WriteLine("Thread " + mt4.Thrd.Name + " counted to " + mt4.Count);
            Console.WriteLine("Thread " + mt5.Thrd.Name + " counted to " + mt5.Count);

            // підрахунок розподілу часу між потоками
            int totalIterations = mt1.Count + mt2.Count + mt3.Count + mt4.Count + mt5.Count;
            double mt1Percentage = (double)mt1.Count / totalIterations * 100;
            double mt2Percentage = (double)mt2.Count / totalIterations * 100;
            double mt3Percentage = (double)mt3.Count / totalIterations * 100;
            double mt4Percentage = (double)mt4.Count / totalIterations * 100;
            double mt5Percentage = (double)mt5.Count / totalIterations * 100;

            Console.WriteLine();
            Console.WriteLine("CPU time distribution:");
            Console.WriteLine(mt1.Thrd.Name + ": " + mt1Percentage.ToString("F2") + "%");
            Console.WriteLine(mt2.Thrd.Name + ": " + mt2Percentage.ToString("F2") + "%");
            Console.WriteLine(mt3.Thrd.Name + ": " + mt3Percentage.ToString("F2") + "%");
            Console.WriteLine(mt4.Thrd.Name + ": " + mt4Percentage.ToString("F2") + "%");
            Console.WriteLine(mt5.Thrd.Name + ": " + mt5Percentage.ToString("F2") + "%");

            Console.ReadLine();
        }
    }
}
