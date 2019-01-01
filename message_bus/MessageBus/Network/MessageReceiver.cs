using System;
using System.Net.Sockets;

namespace MessageBus.Network
{

    //TODO: change to sync
    //OBSOLETE
    [Obsolete]
    public class MessageReceiver
    {
        private TcpClient _client;
        public string AuthToken = string.Empty;

        public MessageReceiver(TcpClient client)
        {
            _client = client;
        }

        public void AcceptClientAuthentication()
        {
            byte[] result = new byte[512];
            _client.Client.BeginReceive(result, 0, result.Length, SocketFlags.None, new AsyncCallback(MessageCallback), result);
        }

        private void MessageCallback(IAsyncResult result)
        {
            int bufferSize = _client.Client.EndReceive(result);
            byte[] data = new byte[bufferSize];
            byte[] receivedData = (byte[])result.AsyncState;

            Buffer.BlockCopy(receivedData, 0, data, 0, bufferSize);

            AuthToken = new Guid(data).ToString();
        }
    }
}
