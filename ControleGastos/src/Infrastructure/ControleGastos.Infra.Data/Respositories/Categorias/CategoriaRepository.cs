using ControleGastos.Domain.Contexts.Categorias;
using ControleGastos.Domain.Contexts.Categorias.Interfaces;
using ControleGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Data.Respositories.Categorias
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Categoria>> ObterTodasCategorias(int pagina, int tamanhoPagina)
        {
            if (pagina <= 0) pagina = 1;
            if (tamanhoPagina <= 0) tamanhoPagina = 10;

            return await _context.Categorias
                .OrderBy(p => p.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Categoria?> ObterCategoriaPorId(Guid categoriaId)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == categoriaId);
        }

        public async Task<List<Categoria>> ObterCategoriasComTransacoes()
        {
            return await _context.Categorias
                .Include(c => c.Transacoes)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
