using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using GameServer;
using UnityEngine;

public class Client: MonoBehaviour
{
    public static Client Instance;
    public readonly int DataBufferSize = 4096;
    
    private readonly string ip = "127.0.0.1";
    private readonly int port = 32750;
    public string MyId { get; set; }
    public TCP tcp;

    private delegate void PacketHandler(Packet packet);

    private static Dictionary<int, PacketHandler> _packetHandlers;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Debug.Log("Client instance already exists. Destroying.");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }

    public void ConnectToServer()
    {
        InitializeClientData();
        tcp.Connect();
    }
    public class TCP
    {
        public TcpClient Socket;

        private NetworkStream _stream;
        private Packet receivedData;
        private byte[] _receiveBuffer;

        public void Connect()
        {
            Socket = new TcpClient
            {
                ReceiveBufferSize = Instance.DataBufferSize,
                SendBufferSize = Instance.DataBufferSize
            };

            _receiveBuffer = new byte[Instance.DataBufferSize];
            Socket.BeginConnect(Instance.ip, Instance.port, ConnectCallback, Socket);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            Socket.EndConnect(result);
            if (!Socket.Connected)
            {
                return;
            }

            _stream = Socket.GetStream();
            receivedData = new Packet();
            _stream.BeginRead(_receiveBuffer, 0, Instance.DataBufferSize, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLenght = _stream.EndRead(result);
                if (byteLenght <= 0)
                {
                    return;
                }

                byte[] data = new byte[byteLenght];
                Array.Copy(_receiveBuffer, data, byteLenght);
                receivedData.Reset(HandleData(data));
                _stream.BeginRead(_receiveBuffer, 0, Instance.DataBufferSize, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private bool HandleData(byte[] data)
        {
            int packetLenght = 0;
            receivedData.SetBytes(data);

            if (receivedData.UnreadLength() >= 4)
            {
                packetLenght = receivedData.ReadInt();
                if (packetLenght <= 0)
                {
                    return true;
                }
            }

            while (packetLenght > 0 && packetLenght <= receivedData.UnreadLength())
            {
                byte[] packetBytes = receivedData.ReadBytes(packetLenght);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        _packetHandlers[packetId](packet);
                    }
                });
                packetLenght = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    packetLenght = receivedData.ReadInt();
                    if (packetLenght <= 0)
                    {
                        return true;
                    }
                }
            }
            // If greater then one, there is partial packet left
            return packetLenght <= 1;
        }

        public void SendData(Packet packet)
        {
            try
            {
                if (Socket != null)
                {
                    _stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    private void InitializeClientData()
    {
        _packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int) ServerPackets.welcome, ClientHandle.Welcome}
        };
        Debug.Log("Initialized packets.");
    }
}
