using MessageBus.Network;
using System;
using System.Net;
using Microservices;
using Microservices.CQRS;

namespace MessageBus.Core
{
    public class Bus
    {
        public static int MessageCount = 0;

        private const int PORT = 2377;
        private readonly IPAddress IP = IPAddress.Parse("127.0.0.1");
        private AsyncListener _listener;
        private AsyncRouter _router;
        private EventQueue _messageQueue;

        public event OnClientConnectedEventHandler OnClientConnected;

        public Bus()
        {
            _listener = new AsyncListener(IP, PORT);
            _router = new AsyncRouter();
            _messageQueue = new EventQueue(_router);
        }

        public void Start()
        {
            _messageQueue.StartProcessing();

            _listener.OnClientConnected += OnClientConnectedCallback;
            _listener.OnMessageReceived += OnMessageReceivedCallback;
            _listener.OnConnectionError += OnConnectionError;
            _listener.Listen();
        }

        public void Stop()
        {
            _listener.StopListening();
            _messageQueue.StopProcessing();
        }

        private void OnClientConnectedCallback(object @object)
        {
            MessageHandshakeSocket mhs = @object as MessageHandshakeSocket;

            if (mhs != null)
            {
                _router.RegisterCient(mhs);

                if (OnClientConnected != null)
                {
                    OnClientConnected(mhs);
                }
            }
        }

        private void OnMessageReceivedCallback(object @object)
        {
            if (typeof(Packet).IsAssignableFrom(@object.GetType()))
            {
                Packet packet = @object as Packet;
                MessageCount++;
                Console.WriteLine("Received IMessage. " + MessageCount);
                
                _messageQueue.AppendMessageForProcessing(packet.Message);
                
                //Send event here to all other clients
                //Maybe MessageHanshakeSocket must have Socket field
                //Wich should be set at AsyncListener level.
            }
        }

        private void OnConnectionError(byte[] data)
        {

        }
    }
}
