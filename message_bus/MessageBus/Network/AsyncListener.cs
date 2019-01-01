using Microservices;
using Networking.DataTransfer;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MessageBus.Network
{
    public delegate void OnClientConnectedEventHandler(object @object);
    public delegate void OnMessageReceivedEventHandler(object message);
    public delegate void OnConnectionError(byte[] err);

    //TODO: change to sync
    class AsyncListener : IListener
    {
        public event OnClientConnectedEventHandler OnClientConnected = null;
        public event OnMessageReceivedEventHandler OnMessageReceived = null;
        public event OnConnectionError OnConnectionError = null;

        private IPAddress _ipAddress;
        private int _port;

        private const int MAX_BUFFER_SIZE = 1024;
        private TcpListener _listener;

        public AsyncListener(IPAddress address, int port)
        {
            _ipAddress = address;
            _port = port;
        }

        public void Listen()
        {
            _listener = new TcpListener(_ipAddress, _port);
            _listener.Start();
            InternalStartListening();
        }

        public void StopListening()
        {
            _listener.Stop();
        }

        private void InternalStartListening()
        {
            while (true)
            {
                TcpClient client = _listener.AcceptTcpClient();
                Thread clientThread = new Thread(new ParameterizedThreadStart(ListenForClientMessages));
                clientThread.Start(client);
            }
        }

        private void ListenForClientMessages(object connectedClient)
        {
            TcpClient client = connectedClient as TcpClient;
            TcpStream stream = new TcpStream(client.GetStream());

            while (true)
            {
                string chars = stream.Receive();
                MessageHandshakeSocket data = JsonConvert.DeserializeObject<MessageHandshakeSocket>(chars);
                Packet packet = null;

                if (data.ServiceId == default(Guid))
                {
                    packet = JsonConvert.DeserializeObject<Packet>(chars);
                }

                if (OnClientConnected != null && packet == null)
                {
                    MessageHandshakeSocket mhs = data as MessageHandshakeSocket;
                    mhs.Client = client;
                    OnClientConnected(mhs);
                }
                else if (OnMessageReceived != null)
                {
                    OnMessageReceived(packet);
                }
            }
        }
    }
}
