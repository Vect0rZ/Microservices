using Microservices.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogPostService
{
    public class BlogPostRepository : IRepository<BlogPost, int>
    {
        public int Add(BlogPost entity)
        {
            return 0;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> GetAll()
        {
            throw new NotImplementedException();
        }

        public BlogPost GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(BlogPost entity)
        {
            throw new NotImplementedException();
        }
    }
}
