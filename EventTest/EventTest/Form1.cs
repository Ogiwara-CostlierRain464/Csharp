using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventTest
{
    public partial class Form1 : Form
    {
        private MyEventClass _evt = new MyEventClass();

        public Form1()
        {
            InitializeComponent();

            _evt.OnChangingValue += (object　s,EventArgs e) =>
            {
                Invoke(new Action(() => label1.Text = "OnChangingValue : " + _evt.VAL.ToString()));//InvokeはPratform.Start
            };//Actionは引数、戻り値がない処理

            _evt.OnChangedValue += (s, e) =>
            {
                Invoke(new Action(() => label2.Text = "OnChangedValue : " + _evt.VAL.ToString()));
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _evt.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _evt.SetData(0);
        }
    }

    class MyEventClass
    {
        private int? _val = null;

        public int? VAL
        {
            get
            {
                return _val;
            }

            private set
            {
                bool change = _val != value;
                if (change) OnChangingHandler(new EventArgs());//つまり、値が変わった時に呼び出しできる
                _val = value;
                if (change) OnChangedHandler(new EventArgs());
            }
        }

        public EventHandler OnChangingValue;

        public EventHandler OnChangedValue;

        protected virtual void OnChangedHandler(EventArgs e)
        {
            if (OnChangedValue != null)
            {
                OnChangedValue(this,e);
            }
        }

        protected virtual void OnChangingHandler(EventArgs e)
        {
            if (OnChangingValue != null)
            {
                OnChangingValue(this,e);
            }
        }

        public void SetData(int? i)
        {
            VAL = i;
        }

        public void Start()
        {
            var task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);

                    if (VAL == null) SetData(0);
                    else SetData(VAL + 1);
                }
            });
        }
    }
}
