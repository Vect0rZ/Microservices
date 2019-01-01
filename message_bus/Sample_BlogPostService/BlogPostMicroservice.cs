using Messages.BlogPost;
using Microservices.CQRS;
using Microservices.Microservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogPostService
{
    class BlogPostMicroservice : MicroService<BlogPost, int>
    {
        public BlogPostMicroservice()
        {
            Initialize();
        }

        public void AddBlogPost(int blogId, string title)
        {
            int id = Repository.Add(new BlogPost(blogId, title));
            IEvent evnt = new BlogPostAdded(id, blogId, title);

            Raise(evnt);
        }
    }
}
