using Microservices.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogPostService
{
    public class BlogPost : IDataEntity<int>
    {
        public BlogPost(int blogId, string title)
        {
            BlogId = blogId;
            Title = title;
        }

        public int Id { get; set; }

        public int BlogId { get; set; }

        public string Title { get; set; }
    }

    public class BlogPostComment : IDataEntity<int>
    {
        public int Id { get; set; }

        public string Comment { get; set; }
    }
}
