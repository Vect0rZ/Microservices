using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusAPI.Exceptions
{
    public class NonValidIpAddressProvidedException : Exception
    {
        public NonValidIpAddressProvidedException() : base("The provided IpAddress format is not valid.")
        {

        }
    }
}
