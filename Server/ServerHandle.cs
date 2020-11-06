using System;

namespace GameServer
{
    public class ServerHandle
    {
        public static void WelcomeReceived(string fromClient, Packet packet)
        {
            string clientIdCheck = packet.ReadString();
            int clientType = packet.ReadInt();
            
            Console.WriteLine($"{Server.clients[fromClient].tcp.Socket.Client.RemoteEndPoint} connected sucesfully widh id {fromClient}");
        }
    }
}