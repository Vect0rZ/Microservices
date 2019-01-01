using Messages.Blog;
using Microservices.CQRS;
using Microservices.Microservices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogService
{
    public class BlogMicroservice : MicroService<Blog, int>
    {
        public BlogMicroservice()
        {
            Initialize();
        }

        public void AddBlog(string name, string description)
        {
            int id = Repository.Add(new Blog(name, description));
            IEvent evnt = new BlogAdded(id, name, description);

            Raise(evnt);
        }

        public Blog GetById(int id)
        {
            return Repository.GetById(id);
        }
    }
}
