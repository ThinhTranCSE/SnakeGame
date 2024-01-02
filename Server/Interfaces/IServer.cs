using Networking.Implemetations.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IServer : IDisposable
    {
        Dictionary<uint, Connection> Connections { get; }
        Socket ListenSocket { get; }
        void Start();
        void Stop();
        void Dispose();
    }
}
