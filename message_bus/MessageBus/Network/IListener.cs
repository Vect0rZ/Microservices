namespace MessageBus.Network
{
    interface IListener
    {
        void Listen();
        void StopListening();
    }
}
