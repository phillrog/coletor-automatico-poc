using ColetaAutomatica.Core.Data;

namespace ColetaAutomatica.API.Models
{
    public interface ISolicitacaoProcessoRepositoryAsync : IRepository<SolicitacaoProcesso>
    {
        Task Adicionar(SolicitacaoProcesso solicitacaoProcesso);
        Task<SolicitacaoProcesso> ObterPorNumeroProcesso(Guid id);
    }
}
