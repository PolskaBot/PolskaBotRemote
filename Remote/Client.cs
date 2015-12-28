using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiscUtil.IO;
using MiscUtil.Conversion;
using System.Net;
using System.Net.Sockets;

namespace Remote
{
    class Client
    {
        private NetworkStream stream;
        private EndianBinaryReader reader;
        private EndianBinaryWriter writer;

        Task loop;

        public Client(TcpClient client)
        {
            stream = client.GetStream();
            reader = new EndianBinaryReader(EndianBitConverter.Big, stream);
            writer = new EndianBinaryWriter(EndianBitConverter.Big, stream);
            loop = new Task(() => Read());
            loop.Start();
        }

        public void Read()
        {
            short length = reader.ReadInt16();
            short id = reader.ReadInt16();

            if(id == 101)
            {
                byte[] code = reader.ReadBytes(length - 2);
                // TODO: Initialize Stage One
                // TODO: Return SWF
            }
        }
    }
}
