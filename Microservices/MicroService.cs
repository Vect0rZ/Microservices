using Microservices.IOC;
using Microservices.Transport;
using Microservices.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.DataTransfer;

namespace Microservices.Microservices
{
    public abstract class MicroService<E, EId> where E : IDataEntity<EId>
    {
        private IPublishSubscribe _bus;

        protected IRepository<E, EId> Repository;

        public void Initialize()
        {
            if (!BasicContainer.IOC.IsTypeRegistered<IPublishSubscribe>())
            {
                throw new ArgumentNullException("No bus transport registered.");
            }

            if (!BasicContainer.IOC.IsTypeRegistered<IRepository<E, EId>>())
            {
                throw new ArgumentNullException($"No repository of type {typeof(E).Name} with Id {typeof(EId).Name} registered.");
            }

            _bus = BasicContainer.IOC.Resolve<IPublishSubscribe>();
            Repository = BasicContainer.IOC.Resolve<IRepository<E, EId>>();
        }

        public void Raise(IEvent @event)
        {
            _bus.Publish(@event);
        }
    }
}
