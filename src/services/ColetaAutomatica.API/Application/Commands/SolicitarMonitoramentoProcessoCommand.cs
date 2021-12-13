using ColetaAutomatica.Core.Messages;
using FluentValidation;

namespace ColetaAutomatica.API.Application.Commands
{
    public class SolicitarMonitoramentoProcessoCommand : Command
    {
        public Guid NumeroProcesso { get; set; }

        public SolicitarMonitoramentoProcessoCommand(Guid id)
        {
            NumeroProcesso = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtivarMonitoramentoProcessoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtivarMonitoramentoProcessoValidation : AbstractValidator<SolicitarMonitoramentoProcessoCommand>
    {
        public AtivarMonitoramentoProcessoValidation()
        {
            RuleFor(d => d.NumeroProcesso)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do processo inválido");
        }
    }
}
