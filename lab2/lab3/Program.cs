using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ParallelFor
{
    class Program
    {
        static double[] data;

        // Метод, який служить як тіло паралельного циклу.
        static void MyTransform(int i)
        {
            data[i] /= 10;

            if (data[i] < 10000) data[i] = 0;
            if ((data[i] >= 10000) & (data[i] < 20000)) data[i] = 100;
            if ((data[i] >= 20000) & (data[i] < 30000)) data[i] = 200;
            if (data[i] > 30000) data[i] = 300;
        }

        static void TransformDivideByPi(int i)
        {
            data[i] /= Math.PI;
        }

        static void TransformExpXOverXPi(int i)
        {
            data[i] = Math.Exp(data[i]) / Math.Pow(data[i], Math.PI);
        }

        static void TransformExpPiXOverXPi(int i)
        {
            data[i] = Math.Exp(Math.PI * data[i]) / Math.Pow(data[i], Math.PI);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");

            Stopwatch sw = new Stopwatch();

            data = new double[100000000];

            // ініціалізація даних у звичайному циклі
            sw.Start();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }
            sw.Stop();
            Console.WriteLine($"Serial initialization of data = {sw.Elapsed.TotalSeconds} seconds.");

            PerformExperiment(MyTransform, "Original Transformation");
            PerformExperiment(TransformDivideByPi, "Divide by Pi");
            PerformExperiment(TransformExpXOverXPi, "Exp(x) / x^Pi");
            PerformExperiment(TransformExpPiXOverXPi, "Exp(Pi*x) / x^Pi");

            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }

        static void PerformExperiment(Action<int> transformation, string description)
        {
            Stopwatch sw = new Stopwatch();

            // послідовне виконання
            sw.Start();
            for (int i = 0; i < data.Length; i++)
            {
                transformation(i);
            }
            sw.Stop();
            Console.WriteLine($"Serial {description} = {sw.Elapsed.TotalSeconds} seconds.");

            // паралельне виконання
            sw.Restart();
            Parallel.For(0, data.Length, transformation);
            sw.Stop();
            Console.WriteLine($"Parallel {description} = {sw.Elapsed.TotalSeconds} seconds.");
        }
    }
}
