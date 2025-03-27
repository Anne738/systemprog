using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // 1
    static void TaskWithDelay()
    {
        int taskId = Task.CurrentId ?? -1;
        Console.WriteLine($"Task {taskId} is starting.");

        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(taskId * 200); // затримка 
            Console.WriteLine($"Task {taskId} counter = {count}");
        }

        Console.WriteLine($"Task {taskId} is done.");
    }

    static void Main()
    {
        Console.WriteLine("Main Thread is starting.");

        // 1 задачі
        Task tsk1 = new Task(TaskWithDelay);
        Task tsk2 = new Task(TaskWithDelay);

        tsk1.Start();
        tsk2.Start();

        // 2
        Task.WaitAll(tsk1, tsk2);
        Console.WriteLine("All tasks completed.");

        // 3
        Task tsk3 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Lambda Task is starting.");
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Lambda Task counter = {count}");
            }
            Console.WriteLine("Lambda Task is done.");
        });

        tsk3.Wait(); 

        // 4
        Parallel.Invoke(
            () =>
            {
                Console.WriteLine("Parallel Task 1 is running.");
                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"Parallel Task 1 counter = {count}");
                }
                Console.WriteLine("Parallel Task 1 is done.");
            },
            () =>
            {
                Console.WriteLine("Parallel Task 2 is running.");
                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"Parallel Task 2 counter = {count}");
                }
                Console.WriteLine("Parallel Task 2 is done.");
            }
        );

        Console.WriteLine("Main() is done.");
        Console.ReadLine();
    }
}
