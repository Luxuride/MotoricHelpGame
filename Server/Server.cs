using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    public static class Server
    {
        public static int Port { get; private set; }
        public static Dictionary<string, Client> clients = new Dictionary<string, Client>();
        public delegate void PacketHandler(string fromClient, Packet packet);

        public static Dictionary<int, PacketHandler> PacketHandlers;
        
        public static TcpListener tcpListener;

        public static void Start(int port)
        {
            Port = port;
            Console.WriteLine("Starting server...");
            InitializeServerData();
            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TcpConnectCallback), null);

            Console.WriteLine($"Server started on port {Port}.");
        }

        private static void TcpConnectCallback(IAsyncResult result)
        {
            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TcpConnectCallback), null);
            
            Console.WriteLine(client.Client.RemoteEndPoint);
            string id = Guid.NewGuid().ToString();
            clients.Add(id, new Client(id));
            clients[id].tcp.Connect(client);
        }

        private static void InitializeServerData()
        {
            PacketHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived }
            };
        }
    }
}