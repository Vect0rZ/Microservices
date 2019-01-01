using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.CQRS
{
    public interface IHandleCommand<T> where T : ICommand
    {
        void Handle(T command);
    }
}
