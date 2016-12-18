using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiNET;
using MiNET.Blocks;

namespace BlockJson
{
    static class Converter
    {
        public static List<string> TOStringData(List<Block> blocks)
        {
            return blocks.Select(e => $"{e.Coordinates.X},{e.Coordinates.Y},{e.Coordinates.Z},{e.Id},{e.Metadata}").ToList();
        }

        public static List<string> Clean(List<string> data,Player p)
        {
            var pos = p.SpawnPosition;
            return data.Select((e) => {
                string[] split = e.Split(',');
                int x = int.Parse(split[0]) - (int) pos.X;
                int y = int.Parse(split[1]) - (int)pos.Y;
                int z = int.Parse(split[2]) - (int)pos.Z;
                return $"{x},{y},{z},{split[3]},{split[4]}";
            }).ToList();
        }
    }
}
