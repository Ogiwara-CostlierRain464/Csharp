using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curry
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int,Func<int,int>> adder = (n) =>//return function
            {
                return (m) =>
                {
                    return n + m;
                };
            };
            Console.WriteLine(adder(1)(3));
            Console.Read();
        }
    }
}
