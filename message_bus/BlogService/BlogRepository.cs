using Microservices.DataTransfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogService
{
    public class BlogRepository : IRepository<Blog, int>
    {
        public int Add(Blog entity)
        {
            Debug.WriteLine("Adding blog.");

            return 0;
        }

        public int Delete(int id)
        {
            Debug.WriteLine("Deleting blog.");

            return 0;
        }

        public IEnumerable<Blog> GetAll()
        {
            Debug.WriteLine("Returning all blogs.");

            return null;
        }

        public Blog GetById(int id)
        {
            Debug.WriteLine("Returning blog.");

            return null;
        }

        public int Update(Blog entity)
        {
            Debug.WriteLine("Updating blog.");

            return 0;
        }
    }
}
