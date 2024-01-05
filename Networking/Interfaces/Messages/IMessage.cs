using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.Messages
{
    public interface IMessage
    {
        byte[] Buffer { get; }
        //read methods
        
        bool ReadBool(int Offset);
        int ReadInt16(int Offset);
        ushort ReadUShort(int Offset);
        int ReadInt32(int Offset);
        long ReadInt64(int Offset);
        string ReadString(int Offset, ref int NextOffset);

    }
}
