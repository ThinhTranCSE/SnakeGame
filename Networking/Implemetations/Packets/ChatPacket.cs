using Networking.Abstracts.Packets;
using Networking.Anotations;
using Networking.Enums;
using Networking.Implemetations.MessageWriters;
using Networking.Interfaces.Messages;
using Networking.Interfaces.MessageWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Implemetations.Packets
{
    [PacketType(PacketTypes.MESSAGE, typeof(ChatPacket))]
    public class ChatPacket : Packet
    {
        public string Message { get; set; }

        public ChatPacket(string Message) : base(PacketTypes.MESSAGE)
        {
            this.Message = Message;
        }

        public ChatPacket(IMessage NetworkMessage) : base(PacketTypes.MESSAGE)
        {
            int StringLength = 0;
            this.Message = NetworkMessage.ReadString(0, ref StringLength);
        }

        public override void WriteToMessage(INetworkMessage NetworkMessage)
        {
            IBlockMessageWriter Writer = new BlockMessageWriter();
            Writer.AddString(Message);
            NetworkMessage.Accept(Writer);
        }

        public override void Execute(object ConnectionOwner = null)
        {
            Console.WriteLine(Message);
        }
    }
}
