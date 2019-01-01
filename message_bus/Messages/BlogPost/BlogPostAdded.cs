using Microservices.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.BlogPost
{
    [Serializable]
    public class BlogPostAdded : Event
    {
        public BlogPostAdded(int id, int blogId, string title) : base()
        {
            Id = id;
            BlogId = blogId;
            Title = title;
        }

        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
    }
}
