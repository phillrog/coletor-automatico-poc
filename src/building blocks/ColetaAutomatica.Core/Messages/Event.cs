using ColetaAutomatica.Core.Attributes;
using MediatR;

namespace ColetaAutomatica.Core.Messages
{
    public class Event : Message, INotification
	{
		[SkipProperty]
		public DateTime Timestamp { get; private set; }
		[SkipProperty]
		public Type Tipo { get; set; }

		protected Event()
		{
			Timestamp = DateTime.Now;
		}
	}

	public interface IEventBus
    {

    } 
}
