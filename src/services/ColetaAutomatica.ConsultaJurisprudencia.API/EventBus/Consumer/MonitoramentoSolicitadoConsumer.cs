using ColetaAutomatica.ConsultaJurisprudencia.API.Services.Handlers;
using ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents;
using MassTransit;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.EventBus.Consumer
{
    public class MonitoramentoSolicitadoConsumer : IConsumer<IMonitoramentoSolicitadoEvent>
    {
        private readonly IMonitoramentoSolicitadoService _monitoramentoSolicitadoService;       

        public MonitoramentoSolicitadoConsumer(IMonitoramentoSolicitadoService monitoramentoSolicitadoService)
        {
            _monitoramentoSolicitadoService = monitoramentoSolicitadoService;
        }
        public async Task Consume(ConsumeContext<IMonitoramentoSolicitadoEvent> context)
        {
            await _monitoramentoSolicitadoService.ExecutarAsync(context);
        }
    }
}
