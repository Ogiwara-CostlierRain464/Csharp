using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace ServerList
{
    [Plugin(PluginName ="ServerList",Author ="ogiwara",Description ="ServerListのMiNET版",PluginVersion ="1.0")]
    public class Class1 : Plugin
    {
        ServerListAPI api;
        bool run = true;

        protected override void OnEnable()
        {
            base.OnEnable();
            api = new ServerListAPI(this);
            Task.Run(() => {
                while (run)
                {
                    api.Login();
                    Thread.Sleep(10000);
                }
            });
        }

        public MiNetServer getServer()
        {
            return Context.Server;
        }
    }
}
