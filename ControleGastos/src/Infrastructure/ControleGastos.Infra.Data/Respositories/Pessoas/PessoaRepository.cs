using ControleGastos.Domain.Contexts.Pessoas;
using ControleGastos.Domain.Contexts.Pessoas.Interfaces;
using ControleGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Data.Respositories.Pessoas
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Pessoa?> ObterPessoaPorId(Guid pessoaId)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == pessoaId);
        }

        public async Task<List<Pessoa>> ObterPessoas(int pagina, int tamanhoPagina)
        {
            if (pagina <= 0) pagina = 1;
            if (tamanhoPagina <= 0) tamanhoPagina = 10;

            return await _context.Pessoas
                .OrderBy(p => p.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Pessoa>> ObterTodasPessoasComTransacoes()
        {
            return await _context.Pessoas
                .Include(p => p.Transacoes)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
