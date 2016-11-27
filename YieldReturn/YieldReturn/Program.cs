using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldReturn
{

    class Sample
    {
        public IEnumerator<char> GetEnumerator()//このメソッド名である必要がある
        {
            Console.Write("[Cを返す]");
            yield return 'C';

            Console.Write("[#を返す]");
            yield return '#';
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            foreach (char c in new Sample())
            {
                Console.Write("{0}",c);
            }
        }
    }
}
