using ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents;
using MassTransit;
using MediatR;

namespace ColetaAutomatica.API.Application.Events
{
    public class MonitoramentoSolicitadoEventHandler : INotificationHandler<MonitoramentoSolicitadoEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MonitoramentoSolicitadoEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(MonitoramentoSolicitadoEvent message, CancellationToken cancellationToken)
        {            
            await _publishEndpoint.Publish<IMonitoramentoSolicitadoEvent>(new { NumeroProcesso = message.NumeroProcesso });            
        }
    }
}
