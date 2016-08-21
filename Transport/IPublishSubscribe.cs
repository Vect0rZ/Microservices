using Microservices.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Transport
{
    public interface IPublishSubscribe
    {
        void Publish(IMessage message);
        void RegisterHandler<T>(Action<T> handler) where T : IMessage;
        void Connect();
    }
}
