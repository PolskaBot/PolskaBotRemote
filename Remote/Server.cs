using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Remote
{
    class Server
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
        }
        
        private void Listen()
        {
            while(true)
            {
                clients.Add(new Client(server.AcceptTcpClient()));
            }
        }
    }
}
