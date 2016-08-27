using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.DataTransfer
{
    public interface IRepository<E, EId> where E : IDataEntity<EId>
    {
        E GetById(EId id);
        IEnumerable<E> GetAll();
        EId Add(E entity);
        EId Update(E entity);
        EId Delete(EId id);
    }
}
