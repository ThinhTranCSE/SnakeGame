using Networking.Abstracts.Packets;
using Networking.Enums;
using Networking.Interfaces.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Anotations
{
    public class PacketTypeAttribute: Attribute
    {
        public ushort PacketId { get; set; }
        public Type Packet { get; set; }
        public PacketTypeAttribute(PacketTypes PacktType, Type Packet)
        {
            this.PacketId = (ushort)PacktType;
            this.Packet = Packet;
        }
    }
}
