using MicroserviceModelling.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceModelling
{
    [Serializable]
    public class Packet
    {
        public Guid SenderId { get; set; }
        public IMessage Message { get; set; }
    }
}
