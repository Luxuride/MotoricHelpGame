using System;
using System.Net.Sockets;

namespace GameServer
{
    public class Client
    {
        public static int dataBufferSize = 4096;
        public string ID;
        public TCP tcp;

        public Client(string clientID)
        {
            ID = clientID;
            tcp = new TCP(ID);
        }
        
        
        public class TCP
        {
            public TcpClient Socket;

            private NetworkStream _stream;
            private Packet receivedData;
            private byte[] _receiveBuffer;
            private readonly string _id;

            public TCP(string id)
            {
                _id = id;
            }

            public void Connect(TcpClient socket)
            {
                Socket = socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                _stream = socket.GetStream();
                
                receivedData = new Packet();
                _receiveBuffer = new byte[dataBufferSize];
                _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                
                ServerSend.Welcome(_id, "Welcome to the server!");
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

                    _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
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
                    Console.WriteLine(e);
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
                            Server.PacketHandlers[packetId](_id, packet);
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
        }
    }
}