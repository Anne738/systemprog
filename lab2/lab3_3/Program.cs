using System;
using System.Threading.Tasks;

namespace ParallelForEachBreak
{
    class Program
    {
        static double[] data;

        // метод, який служить як тіло паралельного циклу.
        static void MyTransform(double v, ParallelLoopState pls)
        {
            if (v < 0)
            {
                pls.Break(); 
                Console.WriteLine("Breaking loop at value: " + v);
            }
            //Console.WriteLine("Value is: " + v);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");
            data = new double[10000000];

            // ініціювати дані в звичайному циклі for
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }


            data[1000000] = -10;


            ParallelLoopResult loopResult = Parallel.ForEach(data, MyTransform);


            if (!loopResult.IsCompleted)
                Console.WriteLine("Parallel.For was aborted with negative value on iteration " + loopResult.LowestBreakIteration);

            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }
}
