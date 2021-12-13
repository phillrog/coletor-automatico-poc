using ColetaAutomatica.API.Application.Events;
using ColetaAutomatica.API.Models;
using ColetaAutomatica.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace ColetaAutomatica.API.Application.Commands
{
    public class SolicitarMonitoramentoProcessoCommandHandler : CommandHandler,
        IRequestHandler<SolicitarMonitoramentoProcessoCommand, ValidationResult>
    {
        private readonly ISolicitacaoProcessoRepositoryAsync _solicitacaoProcessoRepository;

        public SolicitarMonitoramentoProcessoCommandHandler(ISolicitacaoProcessoRepositoryAsync solicitacaoProcessoRepository)
        {
            _solicitacaoProcessoRepository = solicitacaoProcessoRepository;
        }
        public async Task<ValidationResult> Handle(SolicitarMonitoramentoProcessoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var solicitacaoProcesso = new SolicitacaoProcesso(message.NumeroProcesso, DateTime.Now);

            _solicitacaoProcessoRepository.Adicionar(solicitacaoProcesso);

            solicitacaoProcesso.AdicionarEvento(new MonitoramentoSolicitadoEvent(message.NumeroProcesso));

            return await PersistirDados(_solicitacaoProcessoRepository.UnitOfWork);
        }
    }
}