using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace ParallelFor
{
    class Program
    {
        static double[] data;
        static readonly double targetValue = 15000; // число для порівняння
        static readonly double tolerance = 1000; // допустиме відхилення

        static void MyTransform(int i, ParallelLoopState state)
        {
            data[i] /= 10;
            if (Math.Abs(data[i] - targetValue) <= tolerance)
            {
                Console.WriteLine($"Breaking loop at index {i} with value {data[i]}");
                state.Break();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");

            Stopwatch sw = new Stopwatch();

            data = new double[100000000];
            sw.Start();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }
            sw.Stop();
            Console.WriteLine($"Serial initialization of data = {sw.Elapsed.TotalSeconds} seconds.");

            PerformExperiment((i, state) => MyTransform(i, state), "Original Transformation");

        }

        static void PerformExperiment(Action<int, ParallelLoopState> transformation, string description)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Parallel.For(0, data.Length, (i, state) =>
            {
                transformation(i, state);
            });
            sw.Stop();
            Console.WriteLine($"Parallel {description} = {sw.Elapsed.TotalSeconds} seconds.");

            var result = Parallel.For(0, data.Length, (i, state) => transformation(i, state));
            if (!result.IsCompleted)
            {
                Console.WriteLine("Loop was stopped early.");
            }



        }
    }
}
