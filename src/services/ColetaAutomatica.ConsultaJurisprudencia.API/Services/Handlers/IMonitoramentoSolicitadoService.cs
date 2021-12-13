using ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents;
using MassTransit;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Services.Handlers
{
    public interface IMonitoramentoSolicitadoService
    {
        Task ExecutarAsync(ConsumeContext<IMonitoramentoSolicitadoEvent> context);
    }
}
