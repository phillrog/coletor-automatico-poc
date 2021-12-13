using ColetaAutomatica.Core.Messages;
using FluentValidation.Results;
using MediatR;
using ColetaAutomatica.Core.Data.EventSourcing;

namespace ColetaAutomatica.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediator,
            IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _eventSourcingRepository.SalvarEvento(evento);
            await _mediator.Publish(evento);
        }
    }
}
