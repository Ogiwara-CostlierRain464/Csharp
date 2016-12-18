using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace PallarelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            core();
            Console.Read();
        }

        static void core()
        {
            Parallel.Invoke(
                () =>
                {
                    double i = 2;
                    while (true)
                    {
                        i = Math.Sqrt(i);
                        Console.WriteLine("Core1 :" + i);
                        Thread.Sleep(500);
                    }
                },
                () =>
                {
                    double i = 3;
                    while (true)
                    {
                        i = Math.Sqrt(i);
                        Console.WriteLine("Core2 :" + i);
                        Thread.Sleep(500);
                    }
                });

            Console.WriteLine("Done!");
        }
    }
}
