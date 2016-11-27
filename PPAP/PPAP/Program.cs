using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAP
{
    class Program
    {
        static void Main(string[] args)
        {
            APPLE apple = new APPLE();
            PINEAPPLE pinapple = new PINEAPPLE();
            PEN pen = new PEN();

            APPLEPEN applepen = apple + pen;
            PINEAPPLEPEN pineapplepen = pinapple + pen;
            PENPINEAPPLEAPPLEPEN pico = pineapplepen + applepen;

            Console.WriteLine(pico.toString());
            Console.Read();
        }
    }

    class APPLE
    {
        public static APPLEPEN operator+(APPLE a,PEN p)
        {
            return new APPLEPEN();
        }
    }

    class PEN
    { 
    }

    class PINEAPPLE
    {
        public static PINEAPPLEPEN operator+ (PINEAPPLE a,PEN p)
        {
            return new PINEAPPLEPEN();
        }
    }

    class APPLEPEN
    {

    }

    class PINEAPPLEPEN
    {
        public static PENPINEAPPLEAPPLEPEN operator+ (PINEAPPLEPEN p,APPLEPEN a)
        {
            return new PENPINEAPPLEAPPLEPEN();
        }
    }

    class PENPINEAPPLEAPPLEPEN
    {
        public string toString()
        {
            return this.GetType().FullName;
        }
    }
}
