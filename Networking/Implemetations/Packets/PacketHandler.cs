using Networking.Abstracts.Packets;
using Networking.Anotations;
using Networking.Implemetations.Messages;
using Networking.Interfaces.Messages;
using Networking.Interfaces.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Implemetations.Packets
{
    public class PacketHandler : IPacketHandler
    {
        public static readonly Dictionary<ushort, PacketConstructor> PacketFactory = LoadPackets();
        public delegate IPacket PacketConstructor(IMessage Message);


        public IPacket GetPacketInstance(byte[] Buffer)
        {
            ushort PacketId = GetPacketId(Buffer);
            if (PacketFactory.ContainsKey(PacketId))
            {
                byte[] PacketContent = Buffer.Skip(2).ToArray();
                IMessage Message = new Message(PacketContent);
                return PacketFactory[PacketId](Message);
            }
            throw new Exception("Packet not found");
        }

        private static Dictionary<ushort, PacketConstructor> LoadPackets()
        {
            Dictionary<ushort, PacketConstructor> PacketFactory = new Dictionary<ushort, PacketConstructor>();
            var PacketImplemetations = typeof(Packet).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Packet)));
            foreach (var PacketImplemetation in PacketImplemetations)
            {
                var PacketTypeAtt = PacketImplemetation.GetCustomAttributes(typeof(PacketTypeAttribute), false).FirstOrDefault() as PacketTypeAttribute;

                if (PacketTypeAtt != null)
                {
                    var PacketConstructor = PacketImplemetation.GetConstructor(new Type[] { typeof(IMessage) });
                    PacketFactory.Add(PacketTypeAtt.PacketId, (IMessage Message) => { return (IPacket)PacketConstructor?.Invoke(new object[] { Message }); });
                }
            }
            return PacketFactory;
        }

        private ushort GetPacketId(byte[] Buffer)
        {
            return BitConverter.ToUInt16(Buffer, 0);
        }
    }
}
