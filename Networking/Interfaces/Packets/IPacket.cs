using Networking.Enums;
using Networking.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.Packets
{
    public interface IPacket
    {
        PacketTypes PacketId { get; }

        void WriteToMessage(INetworkMessage Message);

        void Execute(object ConnectionOwner = null);
    }
}
