using Microservices.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices
{
    [Serializable]
    public class Packet
    {
        public Guid SenderId { get; set; }
        public object Message { get; set; }
    }
}
