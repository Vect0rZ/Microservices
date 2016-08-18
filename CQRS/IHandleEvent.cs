using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceModelling.CQRS
{
    public interface IHandleEvent<T> where T : IEvent
    {
        void Handle(T @event);
    }
}
