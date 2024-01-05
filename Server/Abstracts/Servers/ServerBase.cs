using Networking.Implemetations.Connections;
using Server.Implementations;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Abstracts.Servers
{
    public abstract class ServerBase : IServer
    {
        public int ListenPort { get; private set; }
        public Socket ListenSocket { get; private set; }
        public Dictionary<uint, Connection> Connections { get; private set; }

        protected Thread ListenThread { get; private set; }
        public ServerBase(int ListenPort)
        {
            this.Connections = new Dictionary<uint, Connection>();
            this.ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.ListenPort = ListenPort;
        }

        public void Start()
        {
            try
            {
                ListenSocket?.Bind(new IPEndPoint(IPAddress.Any, ListenPort));
                ListenThread = new Thread(Listen);
                ListenThread.IsBackground = true;
                ListenThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private void Listen()
        {
            try
            {
                while (true)
                {
                    ListenSocket?.Listen(10);
                    Socket ClientSocket = ListenSocket?.Accept();
                    Connection Connection = new Connection(ClientSocket);
                    uint Serial = SerialGenerator.Instance.GetSerial();
                    Connections.Add(Serial, Connection);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        public void Stop()
        {
            Dispose();
        }
        public void Dispose()
        {
            ListenSocket?.Close();
            ListenThread?.Abort();
            foreach (var Connection in Connections)
            {
                Connection.Value.Dispose();
            }
        }    
    
    }
}
