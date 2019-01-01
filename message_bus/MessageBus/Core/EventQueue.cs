using MessageBus.Network;
using Microservices.CQRS;
using System.Collections.Concurrent;
using System.Threading;

namespace MessageBus.Core
{
    public class EventQueue : IEventQueue
    {
        ConcurrentQueue<object> _queue;
        AsyncRouter _router;

        private ThreadStart _queueProcessThreadStart;
        private Thread _queueProcessThread;

        public EventQueue(AsyncRouter router)
        {
            _queue = new ConcurrentQueue<object>();
            _router = router;
        }

        public void AppendMessageForProcessing(object message)
        {
            lock(_queue)
            {
                _queue.Enqueue(message);
            }
        }

        public void StartProcessing()
        {
            _queueProcessThreadStart = new ThreadStart(ProcessThread);
            _queueProcessThread = new Thread(_queueProcessThreadStart);
            _queueProcessThread.Start(); 
        }

        public void StopProcessing()
        {
            _queueProcessThread.Abort();
            _queueProcessThread = null;
            _queueProcessThreadStart = null;
        }

        private void ProcessThread()
        {
            while (true)
            {
                while (_queue.Count > 0)
                {
                    object message;
                    if (_queue.TryDequeue(out message))
                    {
                        _router.RouteMessage(message);
                    }
                }

                Thread.Sleep(10);
            }
        }
    }
}
