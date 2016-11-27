using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandlerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Core core = new Core();
            core.StartEvent += onEvent;
            //core.StartEvent = nullはevent修飾子があるのでできない
            core.run();
            Console.Read();
        }

        public static void onEvent(object sender,TestEventArgs a)
        {
            Console.WriteLine("onEvent");
            Console.WriteLine(a.message);
        }
    }

    class Core
    {
        public event EventHandler<TestEventArgs> StartEvent;//EventArgsの型を指定

        public void run()
        {
            StartEvent(this,new TestEventArgs("aha"));
        }
    }

    class TestEventArgs : EventArgs
    {
        private string _message;//メンバ変数
        public string message//プロパティ
        {
            get{ return _message;}
            set{ _message = value;}//ゲッター・セッター
        }

        public TestEventArgs(string str)
        {
            message = str;
        }
    }
}
