using System.Collections;
using System.Collections.Generic;
using GameServer;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        Client.Instance.tcp.SendData(packet);
    }
    
    #region Packets

    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int) ClientPackets.welcomeReceived))
        {
            packet.Write(Client.Instance.MyId);
            packet.Write(0);
            
            SendTCPData(packet);
        }
    }

    #endregion
}
