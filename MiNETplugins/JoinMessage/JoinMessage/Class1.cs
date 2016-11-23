using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace JoinMessage
{
    [Plugin(Author ="ogiwara",Description = "JoinMessage Plugin",PluginName = "JoinMessage",PluginVersion = "1.0")]//plugin.ymlではなく、ここにプラグイン情報を書く
    public class Class1 : Plugin
    {
        protected override void OnEnable()
        {
            base.OnEnable();//オーバーライドしているので、親クラスのOnEnableも呼び出す
            Console.WriteLine("[INFO] JoinMessage loaded.");
            var server = Context.Server;

            server.PlayerFactory.PlayerCreated += (sender,args) => {
                Player player = args.Player;
                player.PlayerJoin += onPlayerJoin;//イベントに処理を追加
                player.PlayerLeave += onPlayerLeave;
            };
        }

        private void onPlayerJoin(object o,PlayerEventArgs e)
        {
            e.Player.Level.BroadcastMessage($"{e.Player.NameTag} が参加しました");
            Console.WriteLine($"[INFO]{e.Player.NameTag} が参加しました");
        }

        private void onPlayerLeave(object o,PlayerEventArgs e)
        {
            e.Player.Level.BroadcastMessage($"{e.Player.NameTag} が退出しました");
            Console.WriteLine($"[INFO]{e.Player.NameTag} が退出しました");
        }
    }
}
