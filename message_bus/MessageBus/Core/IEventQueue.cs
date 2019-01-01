using Microservices.CQRS;

namespace MessageBus.Core
{
    public interface IEventQueue
    {
        void AppendMessageForProcessing(object message);
        void StartProcessing();
        void StopProcessing();
    }
}
