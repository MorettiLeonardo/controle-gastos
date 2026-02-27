using ControleGastos.Domain.DomainObjects.Data;

namespace ControleGastos.Domain.Contexts.Categorias.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<List<Categoria>> ObterTodasCategorias(int pagina, int tamanhoPagina);
        Task<Categoria?> ObterCategoriaPorId(Guid categoriaId);
        Task<List<Categoria>> ObterCategoriasComTransacoes();
    }
}
