using ColetaAutomatica.Core.DomainObjects;

namespace ColetaAutomatica.API.Models
{
    public class SolicitacaoProcesso : Entity, IAggregateRoot
    {
        public Guid NumeroProcesso { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public SolicitacaoProcesso(Guid numeroProcesso, DateTime dataCadastro)
        {
            NumeroProcesso = numeroProcesso;
            DataCadastro = dataCadastro;
        }
    }
}
