using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET;
using System.Threading.Tasks;

namespace TestPlugin
{
    class Class2//コマンド
    {
        [Command(Name = "test1",Description = "testコマンド1")]
        public void aha(Player source)
        {
            source.SendMessage("test1コマンドが実行");
        }
    }
}
