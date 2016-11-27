using System;
using System.Collections.Generic;
using System.Numerics;

using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET.Net;
using MiNET.Entities;
using MiNET.Worlds;
using MiNET.Entities.Hostile;


namespace Practice
{
    [Plugin(PluginName = "Practice",Author = "ogiwara",Description = "練習用",PluginVersion = "Z")]
    public class Class1 : Plugin
    {
        //既存Eventと、自作Eventの理解
        protected override void OnEnable()
        {
            base.OnEnable();
            MiNetServer server = Context.Server;
            server.PlayerFactory.PlayerCreated += (s, a) =>
            {
                Player player = a.Player;
                player.PlayerJoin += OnJoin;
            };

            server.LevelManager.LevelCreated += (sender,args) =>
            {
                Level level = args.Level;
                level.BlockBreak += OnBreak;
            };
        }

        public void OnBreak(object o,BlockBreakEventArgs e)
        {
            e.Cancel = true;
        }

        [PacketHandler]
        public Package OnText(McpeText packet,Player player)
        {
            Console.WriteLine($"{player.Username} chated {packet.message}");

            if (packet.message == "addEntity")
            {
                McpeAddEntity pk = new McpeAddEntity();
                pk.entityType = (uint) EntityType.Minecart;
                pk.entityId = EntityManager.EntityIdUndefined;
                pk.x = player.KnownPosition.X;
                pk.y = player.KnownPosition.Y;
                pk.z = player.KnownPosition.Z;
                player.SendPackage(pk);
            }

            if (packet.message == "makeEntity")
            {
                Ghast ghast = new Ghast(player.Level);
                Vector3 vec = player.Velocity;
                vec.Y += 10;
                ghast.Velocity = vec;
                ghast.NameTag = "ポンコツ";
                ghast.SpawnEntity();
            }

            if (packet.message == "help")
            {
                Context.PluginManager.HandleCommand(player,"help","",null);
            }

            if (packet.message.StartsWith("gm"))
            {

                Console.WriteLine("here");
                string[] spl = packet.message.Split(' ');
                    switch (spl[1]) {
                        case "0":
                            player.SetGameMode(GameMode.Survival);
                            break;

                        case "1":
                            player.SetGameMode(GameMode.Creative);
                            break;

                        case "2":
                            player.SetGameMode(GameMode.Adventure);
                            break;

                        case "3":
                            player.SetGameMode(GameMode.Spectator);
                            break;
                    }
            }

            return packet;
        }

        private void OnJoin(object o,PlayerEventArgs e)
        {
            Console.WriteLine("PlayerJoinEvent");
        }

        //Server.ProcessMessageを参考に

    }
}
