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
            Console.WriteLine(id);
            switch (id)
            {
                case 101:
                    byte[] code = reader.ReadBytes(length - 2);

                    // Generate code
                    Generator generator = new Generator();
                    generator.Build(code);

                    // Return response
                    writer.Write((short)(generator.Output.Length + 2));
                    writer.Write((short)102);
                    writer.Write(generator.Output);
                    writer.Flush();
                    Console.WriteLine("here");
                    break;
                case 103:
                    Console.WriteLine("Console");
                    UserID = reader.ReadInt32();
                    LicenseCheck?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        public void SendLicenseResponse(bool authenticated)
        {
            writer.Write((short)(3));
            writer.Write((short)104);
            writer.Write(authenticated);
            writer.Flush();
        }
    }
}
