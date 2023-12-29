using Networking.Interfaces.MessageWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Abstracts.MessageWriters
{
    public abstract class MessageWriter : IMessageWriter
    {
        public abstract byte[] GetBytes();
    }
}
