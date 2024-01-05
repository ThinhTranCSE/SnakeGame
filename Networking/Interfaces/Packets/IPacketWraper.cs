using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.Packets
{
    public interface IPacketWraper
    {
        static readonly byte[] PACKET_START;
        static readonly byte[] PACKET_END;

        byte[] FullPacket { get; }

        byte[] Packet { get; }
        byte[] WrapPacket(IPacket Packet);

        byte[] UnwrapPacket(byte[] Buffer);
    }
}
