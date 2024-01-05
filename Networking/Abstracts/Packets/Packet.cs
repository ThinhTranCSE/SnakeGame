using Networking.Enums;
using Networking.Interfaces.Messages;
using Networking.Interfaces.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Abstracts.Packets
{
    public abstract class Packet : IPacket
    {
        public PacketTypes PacketId { get; set; }

        public Packet(PacketTypes PacketId)
        {
            this.PacketId = PacketId;
        }

        public abstract void WriteToMessage(INetworkMessage Message);

        public abstract void Execute(object ConnectionOwner = null);
    }
}
