using Networking.Abstracts.Packets;
using Networking.Anotations;
using Networking.Implemetations.Messages;
using Networking.Implemetations.MessageWriters;
using Networking.Interfaces.Messages;
using Networking.Interfaces.MessageWriters;
using Networking.Interfaces.Packets;
using Networking.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Implemetations.Packets
{
    public class PacketWraper : IPacketWraper
    {
        public static readonly byte[] PACKET_START = "AA55".ToBytes();
        public static readonly byte[] PACKET_END = "55AA".ToBytes();

        public byte[] FullPacket { get; private set; }
        public byte[] Packet { get; private set; }
        public PacketWraper(IPacket Packet)
        {
            FullPacket = WrapPacket(Packet);

        }

        public PacketWraper(byte[] Buffer)
        {
            FullPacket = Buffer;
            Packet = UnwrapPacket(Buffer);
        }

        public byte[] UnwrapPacket(byte[] Buffer)
        {
            if (Buffer.Length < 8)
            {
                throw new Exception("Invalid packet");
            }
            //check packet is start with AA55 and end with 55AA
            if (
                Buffer[0] != PACKET_START[0] ||
                Buffer[1] != PACKET_START[1] ||
                Buffer[Buffer.Length - 2] != PACKET_END[0] ||
                Buffer[Buffer.Length - 1] != PACKET_END[1]
            )
            {
                throw new Exception("Invalid packet");
            }

            ushort PacketLength = BitConverter.ToUInt16(Buffer, 2);
            if (PacketLength != Buffer.Length)
            {
                throw new Exception("Invalid packet");
            }
            return Buffer.Skip(4).Take(Buffer.Length - 6).ToArray();
        }

        public byte[] WrapPacket(IPacket Packet)
        {
            INetworkMessage Message = new NetworkMessage();
            Packet.WriteToMessage(Message);

            byte[] PacketIdBytes = Packet.PacketId.ToBytes();

            byte[] PacketContent = Message.Buffer;

            this.Packet = new byte[PacketContent.Length + PacketIdBytes.Length];
            PacketIdBytes.CopyTo(this.Packet, 0);
            PacketContent.CopyTo(this.Packet, PacketIdBytes.Length);

            ushort PacketLength = (ushort)(PACKET_START.Length + PacketIdBytes.Length + 2 + PacketContent.Length + PACKET_END.Length);
            byte[] FullPacket = new byte[PacketLength];

            PACKET_START.CopyTo(FullPacket, 0);
            PacketLength.ToBytes().CopyTo(FullPacket, PACKET_START.Length);
            PacketIdBytes.CopyTo(FullPacket, PACKET_START.Length + 2);
            PacketContent.CopyTo(FullPacket, PACKET_START.Length + 2 + PacketIdBytes.Length);
            PACKET_END.CopyTo(FullPacket, PACKET_START.Length + 2 + PacketIdBytes.Length + PacketContent.Length);
            return FullPacket;
        }
    }
}
