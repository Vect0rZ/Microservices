using Microservices.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogService
{
    public class Blog : IDataEntity<int>
    {
        public Blog(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public int Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }
    }
}
