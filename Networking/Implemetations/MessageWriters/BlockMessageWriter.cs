using Networking.Abstracts.MessageWriters;
using Networking.Interfaces.MessageWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Implemetations.MessageWriters
{
    public class BlockMessageWriter : MessageWriter, IBlockMessageWriter
    {
        public byte[] Buffer { get; private set; }

        public MemoryStream DataStream { get; private set; }

        public BlockMessageWriter()
        {
            Buffer = new byte[1024];
            DataStream = new MemoryStream();
        }

        public void AddByte(byte Value)
        {
            Buffer[0] = Value;
            DataStream.Write(Buffer, 0, 1);
        }

        public void AddInt16(int Value)
        {
            BitConverter.GetBytes(Value).CopyTo(Buffer, 0);
            DataStream.Write(Buffer, 0, 2);
        }

        public void AddInt32(int Value)
        {
            BitConverter.GetBytes(Value).CopyTo(Buffer, 0);
            DataStream.Write(Buffer, 0, 4);
        }

        public void AddInt64(long Value)
        {
            BitConverter.GetBytes(Value).CopyTo(Buffer, 0);
            DataStream.Write(Buffer, 0, 8);
        }

        public void AddString(string Value)
        {
            byte[] StringBuffer = Encoding.UTF8.GetBytes(Value);
            AddInt32(StringBuffer.Length);
            DataStream.Write(StringBuffer, 0, StringBuffer.Length);
        }

        public override byte[] GetBytes()
        {
            return DataStream.ToArray();
        }
    }
}
