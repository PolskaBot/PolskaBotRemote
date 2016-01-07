using System;
using System.Threading.Tasks;
using MiscUtil.IO;
using MiscUtil.Conversion;
using System.Net.Sockets;

namespace Remote
{
    class Client : Database.Database
    {
        private NetworkStream stream;
        private EndianBinaryReader reader;
        private EndianBinaryWriter writer;

        public event EventHandler<EventArgs> LicenseCheck;

        public string IPAddress { get; private set; }
        public int UserID { get; private set; }
        private byte[] Code { get; set; }

        Task loop;

        public Client(TcpClient client)
        {
            IPAddress = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
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

            switch (id)
            {
                case 101:
                    UserID = reader.ReadInt32();
                    Code = reader.ReadBytes(length - 6);
                    LicenseCheck?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        public void InitStageOne(bool authenticated)
        {
            // Generate code
            Generator generator = new Generator();
            if(authenticated)
                generator.Build(Code);
            else
                generator.Build(new byte[1]);

            // Return response
            writer.Write((short)(generator.Output.Length + 2));
            writer.Write((short)102);
            writer.Write(generator.Output);
            writer.Flush();
        }
    }
}
