using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET.Net;

namespace Messager
{
    [Plugin(PluginName = "Messager",Description = "最低限のイベントメッセージを提供します",Author = "ogiwara",PluginVersion = "1.0")]
    public class Class1 : Plugin
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            MiNetServer server = Context.Server;
            server.PlayerFactory.PlayerCreated += (s, e) =>
            {
                Player player = e.Player;
                player.PlayerJoin += new EventHandler<PlayerEventArgs>(OnPlayerJoin);//本来はこの方法
                player.PlayerLeave += OnPlayerLeave;//C# ver2.0から省略可
            };
        }

        public void OnPlayerJoin(object s,PlayerEventArgs args)
        {
            Console.WriteLine($"[MESSAGER]{args.Player.Username}がゲームに参加しました");
            args.Player.Level.BroadcastMessage($"{args.Player.Username}がゲームに参加しました");
        }

        public void OnPlayerLeave(object s,PlayerEventArgs args)
        {
            Console.WriteLine($"[MESSAGER]{args.Player.Username}がゲームから退出しました");
            args.Player.Level.BroadcastMessage($"{args.Player.Username}がゲームから退出しました");
        }

        [PacketHandler]
        public Package OnPlayerChat(McpeText packet,Player player)
        {
            Console.WriteLine($"[MESSAGER]<{player.Username}>{packet.message}");
            return packet;
        }
    }
}
