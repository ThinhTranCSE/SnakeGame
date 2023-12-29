using Networking.Implemetations.Connections;
using Networking.Implemetations.Packets;
using Networking.Ultilities;
using System.Net;
using System.Net.Sockets;

namespace TestNetworking
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Thread Server = new Thread(ServerThread);
            Server.IsBackground = true;
            Server.Start();

            //Thread Client = new Thread(ClientThread);
            //Client.IsBackground = true;
            //Client.Start();

            while (true) { }

            
        }

        static void ServerThread()
        {
            Socket ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenSocket?.Bind(new IPEndPoint(IPAddress.Any, 23000));
            ListenSocket?.Listen(10);
            Console.WriteLine("Server is listening...");
            Socket? ClientSocket = ListenSocket?.Accept();
            Connection Connection = new Connection(ClientSocket);     
            
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        static void ClientThread()
        {
            Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ServerEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 23000);
            ClientSocket?.Connect(ServerEndpoint);
            Connection Connection = new Connection(ClientSocket);
            while (true)
            {
                string Message = Console.ReadLine();
                ChatPacket ChatPacket = new ChatPacket(Message);
                Connection.PushPacketToSendBuffer(ChatPacket);
            }
        }
    }
}