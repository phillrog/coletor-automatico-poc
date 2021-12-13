using ColetaAutomatica.API.Models;
using ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents;
using MassTransit;

namespace ColetaAutomatica.API.EventBus.Consumer
{
    public class MonitoramentoSolicitadoConsumer : IConsumer<IMonitoramentoSolicitadoEvent>
    {
        private readonly ISolicitacaoProcessoRepositoryAsync _solicitacaoProcessoRepository;

        public MonitoramentoSolicitadoConsumer(ISolicitacaoProcessoRepositoryAsync solicitacaoProcessoRepository)
        {
            _solicitacaoProcessoRepository = solicitacaoProcessoRepository;
        }
        public async Task Consume(ConsumeContext<IMonitoramentoSolicitadoEvent> context)
        {
            var mensagem = context.Message;
            var solcitacao = await _solicitacaoProcessoRepository.ObterPorNumeroProcesso(mensagem.NumeroProcesso);
        }
    }
}
