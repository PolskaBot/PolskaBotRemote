using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Server server = new Server();
            while (true)
            {
                string[] command = Console.ReadLine().Split(' ');
                switch (command[0])
                {
                    case "addLicense":
                        if (command[1].Length > 0)
                            Console.WriteLine(server.AddPlayerLicense(Convert.ToInt32(command[1])));
                        else
                            Console.WriteLine("User ID is null");
                        break;
                    case "removeLicense":
                        if (command[1].Length > 0)
                            Console.WriteLine(server.RemovePlayerLicense(Convert.ToInt32(command[1])));
                        else
                            Console.WriteLine("User ID is null");
                        break;
                    default:
                        Console.WriteLine("Wrong command");
                        break;
                }

            }
        }
    }
}
