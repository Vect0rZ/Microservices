using System;
using MessageBus.Core;

namespace MessageBus
{
    class Program
    {

        static void Main(string[] args)
        {
            Bus bus = new Bus();
            bus.Start();
            Console.ReadLine();
        }

    }
}
