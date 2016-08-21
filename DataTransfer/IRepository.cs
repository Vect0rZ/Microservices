using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.DataTransfer
{
    public interface IRepository<E, EId> where E : IDataEntity
    {
        E GetById(EId id);
        IEnumerable<E> GetAll();
        bool Add(E entity);
        bool Update(E entity);
        bool Delete(EId id);
    }
}
