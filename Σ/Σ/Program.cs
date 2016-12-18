using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Σ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Σ(20,3,(k) => {
                return k*k-1;
            }));
            Console.Read();
        }

        public static int Σ(int n,int k,Func<int,int> fx)
        {
            int result = 0;
            Enumerable.Range(k, n-k+1).ToList().ForEach((e) => {
                Console.WriteLine($"{e} : {fx(e)}");
                result += fx(e);
            });
            return result;
        }
    }
}
