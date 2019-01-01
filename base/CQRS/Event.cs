using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.CQRS
{
    public class Event : IEvent
    {
        public string MessageType;

        public Event()
        {
            MessageType = this.GetType().Name;
        }
    }
}
