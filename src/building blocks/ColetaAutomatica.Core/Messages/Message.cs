using ColetaAutomatica.Core.Attributes;

namespace ColetaAutomatica.Core.Messages
{
    public abstract class Message
    {
        [SkipProperty]
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
