using Networking.Interfaces.MessageWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.Messages
{
    public interface INetworkMessage : IMessage
    {
        void Accept(IMessageWriter Writer);
    }
}
