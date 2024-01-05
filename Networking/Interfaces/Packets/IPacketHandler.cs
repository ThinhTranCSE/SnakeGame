using Networking.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.Packets
{
    public interface IPacketHandler
    {
        static readonly Dictionary<ushort, PacketConstructor> PacketFactory;
        delegate IPacket PacketConstructor(IMessage Message);
        

    }
}
