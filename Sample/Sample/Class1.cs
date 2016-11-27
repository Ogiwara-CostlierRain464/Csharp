using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace Sample
{
    [Plugin(PluginName ="test")]
    public class Class1 : Plugin
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Console.WriteLine("プラグインが読みこまれました");
        }
    }
}
