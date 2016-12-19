using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;

namespace HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient c = new TcpClient("157.7.108.115",80);
            Console.WriteLine("サーバー({0}:{1})と接続しました({2}:{3})。",
                ((IPEndPoint) c.Client.RemoteEndPoint).Address,
                ((IPEndPoint) c.Client.RemoteEndPoint).Port,
                ((IPEndPoint) c.Client.LocalEndPoint).Address,
                ((IPEndPoint) c.Client.LocalEndPoint).Port
                );

            NetworkStream ns = c.GetStream();
            using (MemoryStream ms = new MemoryStream())
            {
                Encoding enc = Encoding.UTF8;
                byte[] resBytes = new byte[256];
                int resSize = 0;
                do
                {
                    resSize = ns.Read(resBytes, 0, resBytes.Length);
                    if (resSize == 0)
                    {
                        Console.WriteLine("サーバーが切断しました。");
                        break;
                    }
                    ms.Write(resBytes, 0, resSize);
                } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');

                string resMsg = enc.GetString(ms.GetBuffer(),0,(int)ms.Length);
                Console.WriteLine("NAN");
            }
            ns.Close();
            c.Close();
            
            Console.Read();
        }
    }
}
