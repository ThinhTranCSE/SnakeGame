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

namespace Networking.Implemetations.Packets.C2L
{
    [PacketType(PacketTypes.LOGIN, typeof(LoginPacket))]
    public class LoginPacket : Packet
    {
        private string Username { get; set; }

        public LoginPacket(string Username) : base(PacketTypes.LOGIN)
        {
            this.Username = Username;
        }

        public LoginPacket(IMessage NetworkMessage) : base(PacketTypes.LOGIN)
        {
            int StringLength = 0;
            this.Username = NetworkMessage.ReadString(0, ref StringLength);
        }
        public override void WriteToMessage(INetworkMessage Message)
        {
            IBlockMessageWriter Writer = new BlockMessageWriter();
            Writer.AddString(Username);
            Message.Accept(Writer);
        }

        public override void Execute(object ConnectionOwner = null)
        {
            Console.WriteLine(Username);
        }
    }
}
