using MessageBusAPI;
using Microservices.DataTransfer;
using Microservices.IOC;
using Microservices.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_BlogPostService
{
    class Program
    {

        static void Entry()
        {
            BasicContainer.IOC.Register<IPublishSubscribe, BusClient>(new BusClient(Guid.NewGuid(), "127.0.0.1", 2377));
            BasicContainer.IOC.Register<IRepository<BlogPost, int>, BlogPostRepository>(new BlogPostRepository());
            BasicContainer.IOC.Register<BlogPostMicroservice, BlogPostMicroservice>(new BlogPostMicroservice());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Blog post service.");
            Console.ReadLine();
            Entry();

            IPublishSubscribe bus = BasicContainer.IOC.Resolve<IPublishSubscribe>();
            BlogPostMicroservice blog = BasicContainer.IOC.Resolve<BlogPostMicroservice>();

            bus.Connect();
            //blog.AddBlogPost(0, "First post");

            Console.ReadLine();
        }
    }
}
