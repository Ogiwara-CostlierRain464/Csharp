using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

using static System.Console;

namespace RxFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            new Rx01().Start();
            Read();
        }
    }

    class Rx01
    {
        public void Start()
        {
            var i = 1;
            //Observable.Return(i).Subscribe(event1);
            //Observable.Range(0, 10).Subscribe(event1);
            IDisposable test =  Observable.Range(0, 10)
                .Where(x => 0 == x % 2)
                .Select(x => x + 100)
                .Delay(TimeSpan.FromSeconds(1))
                .Take(3)
                .Subscribe(OnNext,OnError,OnCompleted);
            test.Dispose();
        }

        public void event1(int value)
        {
            WriteLine($"Event fired! Value is {value} !");
        }

        public void OnNext(int value)
        {
            WriteLine($"OnNext: {value}");
        }

        public void OnCompleted()
        {
            WriteLine("Complete");
        }

        public void OnError(Exception e)
        {
            WriteLine($"{e}");
        }
    }
}
