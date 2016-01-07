using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Remote.Database;
using System.IO;

namespace Remote
{
    class Server : Database.Database
    {
        private TcpListener server = null;
        private Task loop;
        private List<Client> clients = new List<Client>();

        public Server()
        {
            server = new TcpListener(IPAddress.Parse(Environment.GetEnvironmentVariable("PB_SERVER_IP")), 8082);
            server.Start();
            loop = new Task(() => Listen());
            loop.Start();
            Console.WriteLine("Server initialized");
        }
        
        private void Listen()
        {
            while(true)
            {
                Client client = new Client(server.AcceptTcpClient());
                WriteLog($"New client connected from {client.IPAddress}");
                client.LicenseCheck += (s, e) =>
                {
                    WriteLog($"License check: {client.UserID}");
                    client.InitStageOne(CheckUserLicence(client.UserID));
                };
                clients.Add(client);
            }
        }

        private void WriteLog(string log)
        {
            string output = DateTime.Now.ToString("[hh:mm:ss] ") + log;
            File.AppendAllText("Log.txt", output + Environment.NewLine);
        }

        public bool AddPlayerLicense(int ID)
        {
            if (!databaseSchema.users.ToList().Exists(user => user.id == ID))
            {
                databaseSchema.users.Add(new User(ID));
                return true;
            }
            return false;

        }

        public bool RemovePlayerLicense(int ID)
        {
            return databaseSchema.users.Remove(databaseSchema.users.ToList().Find(user => user.id == ID));
        }

        private bool CheckUserLicence(int ID)
        {
            return databaseSchema.users.ToList().Exists(user => user.id == ID);
        }
    }
}
