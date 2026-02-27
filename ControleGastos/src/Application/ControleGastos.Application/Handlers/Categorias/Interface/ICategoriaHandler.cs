using ControleGastos.Application.Configurations;
using ControleGastos.Application.Handlers.Categorias.Request;
using ControleGastos.Application.Handlers.Categorias.Response;

namespace ControleGastos.Application.Handlers.Categorias.Interface
{
    public interface ICategoriaHandler
    {
        Task<ApiResponse<CategoriaResponse>> CriarCategoriaAsync(CategoriaRequest request);
        Task<ApiResponse<List<ObterTodasCategoriasResponse>>> ObterTodasCategoriasAsync(int pagina, int tamanhoPagina);
        Task<ApiResponse<CategoriaResponse>> ObterCategoriaPorIdAsync(Guid categoriaId);
        Task<ApiResponse<ConsultaTotaisCategoriaResponse>> ConsultarTotaisPorCategoriaAsync();
    }
}
