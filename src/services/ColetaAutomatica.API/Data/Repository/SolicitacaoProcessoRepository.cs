using ColetaAutomatica.API.Models;
using ColetaAutomatica.Core.Data;

namespace ColetaAutomatica.API.Data.Repository
{
    public class SolicitacaoProcessoRepository : ISolicitacaoProcessoRepositoryAsync
    {
        private readonly ColetaAutomaticaContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public SolicitacaoProcessoRepository(ColetaAutomaticaContext context)
        {
            _context = context;
        }

        public async Task Adicionar(SolicitacaoProcesso solicitacaoProcesso)
        {
            await _context.SolicitacaoProcesso.AddAsync(solicitacaoProcesso);
        }


        public Task<SolicitacaoProcesso> ObterPorNumeroProcesso(Guid id)
        {
            return Task.Run(() => _context.SolicitacaoProcesso.FirstOrDefault ( d=> d.NumeroProcesso == id));
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
