using System;
using System.Collections.Generic;
using System.Threading;

class MyThread
{
    public int Count;
    public Thread Thrd;
    static bool stop = false;
    static string currentName;

    public MyThread(string name, ThreadPriority priority)
    {
        Count = 0;
        Thrd = new Thread(Run);
        Thrd.Name = name;
        Thrd.Priority = priority;
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
        Console.WriteLine("Thread " + Thrd.Name + " counted to " + Count);
    }
}

class Program
{
    static void Main()
    {
        List<MyThread> threads = new List<MyThread>();

        Console.Write("Enter the number of threads: ");
        int threadCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < threadCount; i++)
        {
            Console.Write($"Enter name for thread {i + 1}: ");
            string name = Console.ReadLine();

            Console.WriteLine("Choose priority:");
            Console.WriteLine("1 - Highest\n2 - Above Normal\n3 - Normal\n4 - Below Normal\n5 - Lowest");
            int priorityChoice = int.Parse(Console.ReadLine());

            ThreadPriority priority = priorityChoice switch
            {
                1 => ThreadPriority.Highest,
                2 => ThreadPriority.AboveNormal,
                3 => ThreadPriority.Normal,
                4 => ThreadPriority.BelowNormal,
                5 => ThreadPriority.Lowest,
                _ => ThreadPriority.Normal,
            };

            threads.Add(new MyThread(name, priority));
        }

        foreach (var mt in threads)
            mt.Thrd.Start();

        foreach (var mt in threads)
            mt.Thrd.Join();

        Console.WriteLine("\nCPU time distribution:");
        int totalIterations = 0;
        foreach (var mt in threads) totalIterations += mt.Count;

        foreach (var mt in threads)
        {
            double percentage = (double)mt.Count / totalIterations * 100;
            Console.WriteLine(mt.Thrd.Name + ": " + percentage.ToString("F2") + "%");
        }

        Console.ReadLine();
    }
}
