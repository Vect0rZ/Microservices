using Microservices.Transport;
using MessageBusAPI.Exceptions;
using Microservices;
using Microservices.CQRS;
using Networking.ConnectionResolver;
using Networking.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;

namespace MessageBusAPI
{
    public class BusClient : IPublishSubscribe
    {
        private TcpClient _client;
        private TcpStream _stream;

        private IPAddress _ipAddress;
        private int _port;

        private MessageHandshakeSocket _mhs;
        private List<string> _subscriptions;
        private Dictionary<string, Action<string>> _handlers;

        private IConnectionResolver _connectionResolver;

        public BusClient(Guid busId, string ipAddress, int port)
        {
            _client = new TcpClient();
            _connectionResolver = new ConnectionResolver();
            _handlers = new Dictionary<string, Action<string>>();
            _subscriptions = new List<string>();
            _mhs = new MessageHandshakeSocket();
            _mhs.ServiceId = busId;

            if (IPAddress.TryParse(ipAddress, out _ipAddress) == false)
            {
                throw new NonValidIpAddressProvidedException();
            }

            _port = port;
        }

        public void RegisterHandler<T>(Action<string> handler)
        {
            Action<string> _messageHandler = x => handler(x);
            _handlers.Add(typeof(T).Name, _messageHandler);

            _subscriptions.Add(typeof(T).Name);
        }

        [Obsolete]
        public BusClient RegisterHandlersInAssembly(Assembly assembly)
        {
            List<MethodInfo[]> handlerMethods =
                assembly.GetTypes()
                        .Where(t => t.GetInterfaces()
                                     .Select(s => s.GetGenericTypeDefinition())
                                     .Contains(typeof(IHandleEvent<>)))
                        .Select(s => s.GetMethods())
                        .ToList();

            foreach(MethodInfo[] infos in handlerMethods)
            {
                foreach (MethodInfo info in infos)
                {
                    var action = info.CreateDelegate(typeof(Action<IMessage>));
                }
            }

            return this;
        }

        public void Connect()
        {
            _client.Connect(_ipAddress, _port);
            _stream = new TcpStream(_client.GetStream());

            _mhs.MessageSubscriptions = _subscriptions;
            string data = JsonConvert.SerializeObject(_mhs);
            _stream.Send(data);

            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessages));
            receiveThread.Start();
        }

        public void Publish(IMessage message)
        {
            Packet packet = new Packet();
            packet.SenderId = _mhs.ServiceId;
            packet.Message = message;

            string data = JsonConvert.SerializeObject(packet);
            _stream.Send(data);
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                string data = _stream.Receive();

                Handle(data);
            }
        }

        private void Handle(string message)
        {
            object dataMessage = JsonConvert.DeserializeObject<object>(message);

            string messageType = ((dynamic)dataMessage).MessageType;

            Action<string> handler;

            if (_handlers.TryGetValue(messageType, out handler))
            {
                handler(message);
            }
            else
            {
                throw new Exception(string.Format("Bus does not register handler or type {0}.", messageType));
            }
        }

    }
}
