using EventCommandModelling.IOC;
using EventCommandModelling.Transport;
using MicroserviceModelling.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCommandModelling.Microservices
{
    public abstract class MicroService
    {
        private IPublishSubscribe _bus;

        public void Initialize()
        {
            if (!BasicContainer.IOC.IsTypeRegistered<IPublishSubscribe>())
            {
                throw new ArgumentNullException("No bus transport registered.");
            }

            _bus = BasicContainer.IOC.Resolve<IPublishSubscribe>();
        }

        public void Raise(IEvent @event)
        {
            _bus.Publish(@event);
        }
    }
}
