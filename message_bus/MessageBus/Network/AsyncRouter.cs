using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Networking.Protocol;
using Microservices.CQRS;
using Microservices;
using Networking.Serialization;
using Networking.DataTransfer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageBus.Network
{
    public class AsyncRouter
    {
        private Dictionary<string, MessageHandshakeSocket> _clients;
        private IProtocolHandler _protocolHandler;

        public AsyncRouter()
        {
            _clients = new Dictionary<string, MessageHandshakeSocket>();
            _protocolHandler = new ProtocolHandler();
        }

        public void RegisterCient(MessageHandshakeSocket mhs)
        {
            _clients.Add(mhs.ServiceId.ToString(), mhs);
        }

        public void DisconnectClient(MessageHandshakeSocket mhs)
        {
            _clients.Remove(mhs.ServiceId.ToString());
        }

        public void RouteMessage(object message)
        {
            string messageType = ((dynamic)message).MessageType;

            List<MessageHandshakeSocket> registeredClients = _clients.Where(s => s.Value.MessageSubscriptions.Contains(messageType))
                                                                     .Select(s => s.Value)
                                                                     .ToList();

            foreach (MessageHandshakeSocket client in registeredClients)
            {
                TcpStream stream = new TcpStream(client.Client.GetStream());
                string sendMessage = JsonConvert.SerializeObject(message);

                try
                {
                    stream.Send(sendMessage);
                }
                catch //Client has disconnected
                {
                    DisconnectClient(client);
                }
            }
        }
    }
}
