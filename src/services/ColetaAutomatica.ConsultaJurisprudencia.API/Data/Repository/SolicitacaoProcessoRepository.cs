using ColetaAutomatica.ConsultaJurisprudencia.API.Models;
using ColetaAutomatica.Core.Data;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Data.Repository
{
    public class SolicitacaoProcessoRepository : ISolicitacaoProcessoRepositoryAsync
    {
        private readonly ConsultaJurisprudenciaContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public SolicitacaoProcessoRepository(ConsultaJurisprudenciaContext context)
        {
            _context = context;
        }

        public async Task<SolicitacaoProcesso> ObterPorId(Guid id)
        {
            return await _context.SolicitacaoProcesso.FindAsync(id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
