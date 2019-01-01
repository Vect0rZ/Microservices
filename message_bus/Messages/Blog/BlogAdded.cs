using Microservices.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Blog
{
    [Serializable]
    public class BlogAdded : Event
    {
        public BlogAdded(int id, string name, string description) : base()
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id;
        public string Name;
        public string Description;
    }
}
