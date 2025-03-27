using System;
using System.Threading.Tasks;

namespace ParallelForEachBreak
{
    class Program
    {
        static double[] data;

        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");
            data = new double[1000000];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }


            data[100000] = -10;

            // розпаралелити цикл методом Parallel.ForEach із використанням лямбда-виразу для тіла циклу
            ParallelLoopResult loopResult = Parallel.ForEach(data, (v, pls) =>
            {
                if (v < 0)
                {
                    pls.Break();  
                    Console.WriteLine("Breaking loop at value: " + v);
                }
                //Console.WriteLine("Value is: " + v); 
            });


            if (!loopResult.IsCompleted)
                Console.WriteLine("Parallel.ForEach was aborted with negative value on iteration " + loopResult.LowestBreakIteration);

            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }
}
