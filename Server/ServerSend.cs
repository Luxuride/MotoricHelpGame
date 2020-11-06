namespace GameServer
{
    public class ServerSend
    {
        public static void Welcome(string toClient, string msg)
        {
            using (Packet packet = new Packet((int) ServerPackets.welcome))
            {
                packet.Write(msg);
                packet.Write(toClient);
                SendTCPData(toClient, packet);
            }
        }

        private static void SendTCPData(string toClient, Packet packet)
        {
            packet.WriteLength();
            Server.clients[toClient].tcp.SendData(packet);
        }
    }
}