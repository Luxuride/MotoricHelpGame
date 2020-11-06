using System.Collections;
using System.Collections.Generic;
using GameServer;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        string myId = packet.ReadString();
        Debug.Log($"Message: {msg}");
        Client.Instance.MyId = myId;
        
        ClientSend.WelcomeReceived();
    }
}
