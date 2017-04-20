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
            Task.Run(async() => {
                await api.Login();
                Console.WriteLine("Login-> while(!)");
                while (run)
                {
                    await api.UpDateTime();
                    Console.WriteLine("startsleep");
                    await Task.Delay(2000);
                    Console.WriteLine("endsleep");
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
        public async void Event(Player p,string[] args)
        {
            if (args.Length >= 1)
            {
                await api.Event(args[1]);
            }
        }

        public async void OnPlayerJoin(object o,PlayerEventArgs a)
        {
            await api.UpDatePlayers("join");
        }

        public async void OnPlayerQuit(object o,PlayerEventArgs a)
        {
            await api.UpDatePlayers("quit");
        }

        public async override void OnDisable()//Override元が非Asyncでも問題ない
        {
            base.OnDisable();
            run = false;
            await api.Logout();
        }
    }
}
