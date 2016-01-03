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
        public int UserID = 0;
        private byte[] code;

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

            switch (id)
            {
                case 101:
                    UserID = reader.ReadInt32();
                    code = reader.ReadBytes(length - 6);
                    LicenseCheck?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        public void InitStageOne(bool authenticated)
        {
            // Generate code
            Generator generator = new Generator();
            if(authenticated)
                generator.Build(code);
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
