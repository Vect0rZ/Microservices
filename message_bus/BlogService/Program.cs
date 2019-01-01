using EventCommandModelling.Microservices;
using MessageBusAPI;
using Messages.Blog;
using Messages.BlogPost;
using Microservices.CQRS;
using Microservices.DataTransfer;
using Microservices.IOC;
using Microservices.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogService
{
    class Program
    {
        static void Entry()
        {
            BasicContainer.IOC.Register<IPublishSubscribe, BusClient>(new BusClient(Guid.NewGuid(), "127.0.0.1", 2377));
            BasicContainer.IOC.Register<IRepository<Blog, int>, BlogRepository>(new BlogRepository());
            BasicContainer.IOC.Register<BlogMicroservice, BlogMicroservice>(new BlogMicroservice());
            BasicContainer.IOC.Register<BlogPostHandlers, BlogPostHandlers>(new BlogPostHandlers());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Blog Service.");
            Console.ReadLine();

            Entry();

            IPublishSubscribe bus = BasicContainer.IOC.Resolve<IPublishSubscribe>();

            BlogMicroservice blog = BasicContainer.IOC.Resolve<BlogMicroservice>();
            BlogPostHandlers handlers = BasicContainer.IOC.Resolve<BlogPostHandlers>();
            bus.RegisterHandler<BlogPostAdded>(handlers.BlogPostAdded);
            bus.RegisterHandler<BlogAdded>(handlers.BlogAdded);

            bus.Connect();
            Console.WriteLine("Connected.");

            Console.ReadLine();
            blog.AddBlog("New Blog", "Blog's description.");
            Console.WriteLine("Sent data.");

            Console.ReadLine();
            blog.AddBlog("New Blog", "Blog's description.");
            Console.WriteLine("Sent data.");

            Console.ReadLine();
            blog.AddBlog("New Blog", "Blog's description.");
            Console.WriteLine("Sent data.");

            Console.ReadLine();
            blog.AddBlog("New Blog", "Blog's description.");
            Console.WriteLine("Sent data.");

            Console.ReadLine();
        }
    }

    class BlogPostHandlers : Handler
    {
        public void BlogAdded(string @event)
        {
            BlogAdded data = DeserializeEvent<BlogAdded>(@event);
            Console.WriteLine($"Added Blog{data.Id} - {data.Name}");
        }

        public void BlogPostAdded(string @event)
        {
            BlogPostAdded data = DeserializeEvent<BlogPostAdded>(@event);

            Console.WriteLine($"Added BlogPost {data.Id} to Blog {data.BlogId}");
        }
    }
}
