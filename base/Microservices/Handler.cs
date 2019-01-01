using Microservices.CQRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCommandModelling.Microservices
{
    public class Handler
    {
        public T DeserializeEvent<T>(string @event) where T : Event
        {
            T data = null;

            try
            {
                data = JsonConvert.DeserializeObject<T>(@event);
            }
            catch
            {
                return null;
            }

            return data;
        }
    }
}
