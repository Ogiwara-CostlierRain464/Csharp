using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlegateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sub = new Sub();
            sub.Run("Exception on Main");

            Console.Read();
        }
    }

    class Sub
    {
        public delegate void ExceptionHandler(Exception e);
        public ExceptionHandler HandlerException;

        public Sub()
        {
            HandlerException += Excep1;
            HandlerException += Excep2;
        }

        public void Run(string message)
        {
            HandlerException(new Exception(message));
        }

        public void Excep1(Exception e)
        {
            Console.WriteLine($"On Excep1: {e.Message}");
        }

        public void Excep2(Exception e)
        {
            Console.WriteLine($"On Excep2: {e.Message}");
        }
    }
}
