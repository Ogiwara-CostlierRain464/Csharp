using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiNET;
using MiNET.Net;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET.Blocks;

namespace BlockJson
{
    [Plugin(Author ="ogiwara",Description ="Convert Json <-> Block",PluginName ="BlockJson")]
    public class Class1 : Plugin
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Console.WriteLine("[INFO] BlockJson Loaded.");
        }

        [PacketHandler]
        public void OnChat(McpeText packet,Player player)
        {
            switch (packet.message)
            {
                case "run":
                    run(player);
                    break;
            }
        }

        async public void run(Player p)//処理部
        {
            //playerのx,y,z + 10近辺の情報を表示
            var data = new List<Block>();
            await Task.Run(() => {
                Enumerable.Range((int)p.KnownPosition.X, 10).ToList().ForEach((x) => {
                    Enumerable.Range((int)p.KnownPosition.Y, 10).ToList().ForEach((y) => {
                        Enumerable.Range((int)p.KnownPosition.Z, 10).ToList().ForEach((z) => {
                            Block b = p.Level.GetBlock(x, y, z);
                            data.Add(b);
                        });
                    });
                });
            });
            var raw = Converter.TOStringData(data);
            Console.WriteLine("Raw Data.");
            raw.ForEach(e => Console.WriteLine(e));
            Console.WriteLine("END///////////");
            Console.WriteLine("Clean Data");
            Converter.Clean(raw, p).ForEach(e => Console.WriteLine(e));
            Console.WriteLine("END////////////");
        }
    }
}
