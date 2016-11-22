using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using log4net;

namespace TestPlugin
{
    [Plugin(Author = "ogiwara",Description = "This is my first MiNET plugin",PluginName = "Test",PluginVersion = "1.0")]//引数…
    public class Class1 : Plugin
    {

        protected override void OnEnable()
        {
            Console.WriteLine("Plugin loaded.");

            var server = Context.Server;

            server.PlayerFactory.PlayerCreated += (sender,args) =>
            {
                Player player = args.Player;
                player.PlayerJoin += (o, eventArgs) => eventArgs.Player.Level.BroadcastMessage($"{eventArgs.Player.Username}" + "が参加しました");
            };  
        }
    }
}
