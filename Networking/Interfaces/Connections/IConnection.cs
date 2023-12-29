using Networking.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.Connections
{
    public interface IConnection
    {
        static readonly int RECEIVE_BUFFER_SIZE;
        static readonly int SEND_BUFFER_SIZE;
        static readonly int CHUNK_SIZE;
        Socket Socket { get; }
        ReceiveQueue ReceiveBuffer { get; }
        ReceiveQueue SendBuffer { get; }
    }
}
