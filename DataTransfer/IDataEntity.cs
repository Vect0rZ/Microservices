using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.DataTransfer
{
    public interface IDataEntity<T>
    {
        T Id { get; set; }
    }
}
