using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AccessNukkit
{
    class Program
    {
        static void Main(string[] args)
        {
            new Core();
        }
    }

    internal class Core
    {
        public string Java = @"C:\Program Files\Java\jre1.8.0_101\bin\java.exe";

        public string CurrentDirectory = @"C:\Users\ogiwara\Documents\Nukkit";

        public Core()
        {
            Directory.SetCurrentDirectory(CurrentDirectory);

            var Nukkit = new ProcessStartInfo
            {
                FileName = Java,
                Arguments = "-jar nukkit-1.0-SNAPSHOT.jar",
                UseShellExecute = false,
                RedirectStandardInput = true
            };

            var process = Process.Start(Nukkit);

            try
            {
                TryToKill(process);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

            process.WaitForExit();
        }

        public async void TryToKill(Process p)
        {
            await Task.Run(() => 
            {
                try
                {
                    System.Threading.Thread.Sleep(10000);

                    p.StandardInput.WriteLine("stop\r\r\n");

                    Console.WriteLine("Success!");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
        }
    }
}
