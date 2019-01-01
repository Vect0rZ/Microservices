using System;
using Microservices.CQRS;
using System.Threading;
using Microservices.IOC;
using Microservices.Transport;

namespace Microservices
{
    class Program
    {

        static void Main(string[] args)
        {
            IPublishSubscribe bus = BasicContainer.IOC.Resolve<IPublishSubscribe>();

            ProjectionOrSomething projection = new ProjectionOrSomething();
            UserService userService = new UserService();

            //bus.RegisterHandler<EventSending>(projection.Handle);
            //bus.RegisterHandler<Event2>(projection.Handle);
            //
            //bus.RegisterHandler<RegisterUser>(userService.Handle);

            bus.Connect();

            Console.WriteLine("How many ms per message: ");
            int delay = int.Parse(Console.ReadLine());
            Console.WriteLine("How many kilobytes per message: ");
            int kbs = int.Parse(Console.ReadLine());
            Console.WriteLine("Started.");

            EventSending s = new EventSending();
            for (int i = 0; i < 10 * kbs; i++)
            {
                s.big.Add(s.bytes_100);
            }

            Console.WriteLine("Build message. Total Megabytes: " + ( (s.big.Count * 100) / (1000 * 1000)).ToString() + "Megabyte");
            while (true)
            {
                bus.Publish(s);
                Thread.Sleep(delay);
            }

        }
    }

    class ProjectionOrSomething : IHandleEvent<EventSending>,
        IHandleEvent<Event2>
    {
        public void Handle(EventSending @event)
        {
            Console.WriteLine("Handling EventSending.");
        }

        public void Handle(Event2 @event)
        {
            Console.WriteLine("Handling Event2.");
        }
    }

    class UserService : IHandleCommand<RegisterUser>
    {
        public void Handle(RegisterUser command)
        {
            Console.WriteLine("Registering user {0}, {1}", command.UserName, command.Email);
        }
    }
}
