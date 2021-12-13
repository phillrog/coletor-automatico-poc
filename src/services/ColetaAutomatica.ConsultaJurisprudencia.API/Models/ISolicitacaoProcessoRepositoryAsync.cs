using ColetaAutomatica.Core.Data;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Models
{
    public interface ISolicitacaoProcessoRepositoryAsync : IRepository<SolicitacaoProcesso>
    {
        Task<SolicitacaoProcesso> ObterPorId(Guid id);
    }
}
