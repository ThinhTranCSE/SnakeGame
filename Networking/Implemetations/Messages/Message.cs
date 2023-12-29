using Networking.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Implemetations.Messages
{
    public class Message : IMessage
    {
        public byte[] Buffer { get; protected set; }

        public Message(byte[] Buffer)
        {
            this.Buffer = Buffer;
        }

        public bool ReadBool(int Offset)
        {
            return BitConverter.ToBoolean(Buffer, Offset);
        }

        public int ReadInt16(int Offset)
        {
            return BitConverter.ToInt16(Buffer, Offset);
        }

        public ushort ReadUShort(int Offset)
        {
            return BitConverter.ToUInt16(Buffer, Offset);
        }

        public int ReadInt32(int Offset)
        {
            return BitConverter.ToInt32(Buffer, Offset);
        }

        public long ReadInt64(int Offset)
        {
            return BitConverter.ToInt64(Buffer, Offset);
        }

        public string ReadString(int Offset, ref int NextOffset)
        {
            int Length = ReadInt32(Offset);
            Offset += 4;
            string Result = Encoding.UTF8.GetString(Buffer, Offset, Length);
            NextOffset = Offset + Length;
            return Result;
        }
    }
}
