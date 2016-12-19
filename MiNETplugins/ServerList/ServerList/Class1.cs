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

        public MiNetServer Server
        {
            get
            {
                return Context.Server;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            api = new ServerListAPI(this);
            Task.Run(() => {
                api.Login();
                while (run)
                {
                    api.UpDateTime();
                    Thread.Sleep(20000);
                }
            });

            Context.Server.PlayerFactory.PlayerCreated += (s,a) =>
            {
                Player p = a.Player;
                p.PlayerJoin += OnPlayerJoin;
                p.PlayerLeave += OnPlayerQuit;
            };
        }

        [Command(Name ="event",Description = "Change server state")]
        public void Event(Player p,string[] args)
        {
            if (args.Length >= 1)
            {
                api.Event(args[1]);
            }
        }

        public void OnPlayerJoin(object o,PlayerEventArgs a)
        {
            api.UpDatePlayers("join");
        }

        public void OnPlayerQuit(object o,PlayerEventArgs a)
        {
            api.UpDatePlayers("quit");
        }

        public override void OnDisable()
        {
            run = false;
            api.Logout();
        }
    }
}
