using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LobiAPI;

namespace LobiConsole
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("Welcome to LobiConsole");
            var commander = new CommandExecuter();
            while (true)
            {
                string command = Console.ReadLine();

                if (command.Equals("exit"))
                {
                    break;
                }

                commander.Run(command);
            }
        }
    }

    class CommandExecuter
    {
        BasicAPI api;

        public CommandExecuter()
        {
            api = new BasicAPI();
        }

        public void Run(string command)
        {
            switch (command)
            {
                case "Login":
                    api.Login("maywillogiwara@gmail.com","Ab123456");
                    break;
                case "GetMe":

                    break;
            }
        }
    }
}
