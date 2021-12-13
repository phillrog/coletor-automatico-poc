using ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents;
using MassTransit;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Services.Handlers
{
    public class MonitoramentoSolicitadoService : IMonitoramentoSolicitadoService
    {
        public Task ExecutarAsync(ConsumeContext<IMonitoramentoSolicitadoEvent> context)
        {
            return Task.CompletedTask;
        }
    }
}
