
using ControleGastos.Domain.Contexts.Categorias;
using ControleGastos.Domain.Contexts.Transacoes;
using ControleGastos.Domain.Contexts.Transacoes.Interfaces;
using ControleGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Data.Respositories.Transacoes
{
    public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Transacao?> ObterTransacaoPorId(Guid transacaoId)
        {
            return await _context.Transacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == transacaoId);
        }

        public async Task<List<Transacao>> ObterTransacaoPorPessoaId(Guid pessoaId, int pagina, int tamanhoPagina)
        {
            if (pagina <= 0) pagina = 1;
            if (tamanhoPagina <= 0) tamanhoPagina = 10;

            return await _context.Transacoes
                .Where(t => t.PessoaId == pessoaId)
                .OrderByDescending(t => t.DataCadastro)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
