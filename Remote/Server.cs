using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Remote.Database;

namespace Remote
{
    class Server : Database.Database
    {
        private TcpListener server = null;
        private Task loop;
        private List<Client> clients = new List<Client>();
        //private LicenseManager licenseManager = new LicenseManager();

        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8082);
            server.Start();
            loop = new Task(() => Listen());
            loop.Start();
            Console.WriteLine("Server initialized");
        }
        
        private void Listen()
        {
            while(true)
            {
                Console.WriteLine("New client connected");
                Client client = new Client(server.AcceptTcpClient());
                client.LicenseCheck += (s, e) =>
                {
                    Console.WriteLine("License check");
                    client.SendLicenseResponse(CheckUserLicence(client.UserID));
                };
                clients.Add(client);
            }
        }

        private void AddPlayerLicense(int ID)
        {
            if (!databaseSchema.users.ToList().Exists(user => user.id == ID))
                databaseSchema.users.Add(new User(ID));
        }

        private void RemovePlayerLicense(int ID)
        {
            databaseSchema.users.Remove(databaseSchema.users.ToList().Find(user => user.id == ID));
        }

        private bool CheckUserLicence(int ID)
        {
            return databaseSchema.users.ToList().Exists(user => user.id == ID);
        }
    }
}
