using Networking.DataStructures;
using Networking.Implemetations.Packets;
using Networking.Interfaces.Connections;
using Networking.Interfaces.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Implemetations.Connections
{
    public class Connection : IConnection, IDisposable
    {
        public static readonly int MAX_PACKET_SIZE = 1024;
        public static readonly int CHUNK_SIZE = 1024 ;

        private object ConnectionOwner { get; set; }
        public Socket Socket { get; private set; }
        public PacketHandler PacketHandler { get; private set; } = new PacketHandler();

        private object _ReceiveBufferLock = new object();
        public ReceiveQueue ReceiveBuffer { get; private set; } = new ReceiveQueue();

        private object _SendBufferLock = new object();
        public ReceiveQueue SendBuffer { get; private set; } = new ReceiveQueue();

        private Thread ReceiveThread { get; set; }
        
        private Thread SendThread { get; set; }
        public bool IsEmpty => ReceiveBuffer.Count == 0;

        public Connection(Socket Socket, object ConnectionOwner = null)
        {
            this.Socket = Socket;
            this.ConnectionOwner = ConnectionOwner;

            ReceiveThread = new Thread(ReceivePackets);
            ReceiveThread.IsBackground = true;
            ReceiveThread.Start();

            SendThread = new Thread(SendPackets);
            SendThread.IsBackground = true;
            SendThread.Start();
        }

        public Connection(string IpAddress, int Port, object ConnectionOwner = null)
        {
            Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ServerEndpoint = new IPEndPoint(IPAddress.Parse(IpAddress), Port);
            ClientSocket?.Connect(ServerEndpoint);
            Connection Connection = new Connection(ClientSocket);

            this.Socket = ClientSocket;
            this.ConnectionOwner = ConnectionOwner;

            ReceiveThread = new Thread(ReceivePackets);
            ReceiveThread.IsBackground = true;
            ReceiveThread.Start();

            SendThread = new Thread(SendPackets);
            SendThread.IsBackground = true;
            SendThread.Start();
        }

        private async void ReceivePackets()
        {
            try
            {
                while (true)
                {
                    byte[] WaitingBuffer = new byte[MAX_PACKET_SIZE];
                    int PacketSize = await Socket.ReceiveAsync(WaitingBuffer, SocketFlags.None) ;
                    if(PacketSize == 0)
                    {
                        continue;
                    }
                    lock(_ReceiveBufferLock)
                    {
                        ReceiveBuffer.EnqueueRange(WaitingBuffer, PacketSize);
                    }
                    HandleReceivedPackets();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Dispose();
            }
        }

        private void HandleReceivedPackets()
        {
            byte[] PacketStart = PacketWraper.PACKET_START;
            byte[] PacketEnd = PacketWraper.PACKET_END;
            lock(_ReceiveBufferLock)
            {
                while (true)
                {
                    if(ReceiveBuffer.Count < 8)
                    {
                        return;
                    }
                    if (ReceiveBuffer[0] == PacketStart[0] && ReceiveBuffer[1] == PacketStart[1])
                    {
                        ushort PacketLength = BitConverter.ToUInt16(ReceiveBuffer.ToArray(), 2);
                        if(PacketLength > ReceiveBuffer.Count)
                        {
                            return;
                        }
                        byte[] Packet = ReceiveBuffer.DequeueRange(PacketLength);
                        if (Packet[Packet.Length - 2] != PacketEnd[0] || Packet[Packet.Length - 1] != PacketEnd[1])
                        {
                            return;
                        }
                        PacketWraper PacketWraper = new PacketWraper(Packet);
                        IPacket PacketInstance = PacketHandler.GetPacketInstance(PacketWraper.Packet);
                        PacketInstance?.Execute(ConnectionOwner);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private async void SendPackets()
        {
            try
            {
                while (true)
                {
                    byte[] Chunk;
                    if (SendBuffer.IsEmpty)
                    {
                        continue;
                    }
                    int ChunkLength = Math.Min(SendBuffer.Count, CHUNK_SIZE);
                    lock (_SendBufferLock)
                    {
                        Chunk = SendBuffer.DequeueRange(ChunkLength);
                    }
                    Socket.SendAsync(Chunk, SocketFlags.None);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Dispose();
            }
        }

        public void PushPacketToSendBuffer(IPacket Packet)
        {
            PacketWraper PacketWraper = new PacketWraper(Packet);
            lock(_SendBufferLock)
            {
                SendBuffer.EnqueueRange(PacketWraper.FullPacket, PacketWraper.FullPacket.Length);
            }
        }

        public void Dispose()
        {
            Socket?.Dispose();
            SendThread?.Interrupt();
            ReceiveThread?.Interrupt();
        }
    }
}
