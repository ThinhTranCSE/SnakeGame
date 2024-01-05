using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.MessageWriters
{
    public interface IBlockMessageWriter : IMessageWriter
    {
        byte[] Buffer { get; }
        MemoryStream DataStream { get; }

        //write methods

        void AddByte(byte Value);
        void AddInt16(int Value);
        void AddInt32(int Value); 
        void AddInt64(long Value);
        void AddString(string Value);


    }
}
