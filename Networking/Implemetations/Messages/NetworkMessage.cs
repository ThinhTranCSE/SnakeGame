using Networking.Interfaces.Messages;
using Networking.Interfaces.MessageWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Implemetations.Messages
{
    public class NetworkMessage : Message, INetworkMessage
    {
        public NetworkMessage() : base(new byte[0])
        {
        }

        public void Accept(IMessageWriter Writer)
        {
            Buffer = Writer.GetBytes();
        }
    }
}
