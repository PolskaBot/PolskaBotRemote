using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Remote
{
    class Generator
    {
        public const int MAX_SIZE = 32768;

        private List<byte> HEADER = new List<byte>() { 67, 87, 83, 11, 227, 11, 0, 0, 64, 3, 192, 3, 192, 0, 24, 1, 0, 68, 17, 25, 0, 0, 0, 198, 10, 97, 98, 99, 95, 65, 0 };
        private List<byte> FOOTER = new List<byte>() { 10, 19, 1, 0, 0, 0, 100, 105, 100, 73, 68, 0, 64, 0, 0, 0 };
        public byte[] Output { get; private set; }

        public Generator()
        {
            // Fix header
            HEADER[0] = 70;
            HEADER[5] = 2;

            // Fix footer
            FOOTER[6] = 97;
            FOOTER[7] = 98;
            FOOTER[8] = 99;
            FOOTER[9] = 46;
            FOOTER[10] = 65;
        }

        public void Build(byte[] code)
        {
            if (code.Length == 1)
            {
                Output = code;
                return;
            }

            if (code.Length < MAX_SIZE)
            {
                code = PrepareCode(code);
                Write(code);
            }
        }

        private byte[] PrepareCode(byte[] code)
        {
            for(int i = 0; i < code.Length; i++)
            {
                code[i] ^= (byte)(i * 5 & 255);
            }

            return inflate(code);
        }

        private void Write(byte[] code)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            // Write file
            writer.Write(HEADER.ToArray());
            writer.Write(code);
            writer.Write(FOOTER.ToArray());

            Output = stream.ToArray();

            Output[4] = Convert.ToByte(Output.Length & 0xFF);
            Output[5] = Convert.ToByte((Output.Length & 0xFF00) >> 8);
            Output[6] = Convert.ToByte((Output.Length & 0xFF0000) >> 16);
            Output[7] = Convert.ToByte((Output.Length & 0xFF000000) >> 24);
        }

        private byte[] inflate(byte[] input)
        {
            using (var ms = new MemoryStream(input))
            using (var ds = new DeflateStream(ms, CompressionMode.Decompress))
            using (var os = new MemoryStream())
            {
                ds.CopyTo(os);
                return os.ToArray();
            }
        }
    }
}
