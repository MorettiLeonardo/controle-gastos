using ControleGastos.Application.Configurations;
using ControleGastos.Application.Handlers.Categorias.Interface;
using ControleGastos.Application.Handlers.Categorias.Request;
using ControleGastos.Application.Handlers.Categorias.Response;
using ControleGastos.Domain.Contexts.Categorias;
using ControleGastos.Domain.Contexts.Categorias.Enums;
using ControleGastos.Domain.Contexts.Categorias.Interfaces;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Handlers.Categorias
{
    public class CategoriaHandler : ICategoriaHandler
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ApiResponse<CategoriaResponse>> CriarCategoriaAsync(CategoriaRequest request)
        {
            if (string.IsNullOrEmpty(request.Descricao))
                return new ApiResponse<CategoriaResponse>(400, "Descricao não pode ser nulo ou vazio", null!);

            if (!Enum.IsDefined(typeof(EFinalidade), request.Finalidade))
                return new ApiResponse<CategoriaResponse>(400, "Finalidade inválida", null!);

            var categoria = new Categoria(request.Descricao, request.Finalidade);

            _categoriaRepository.Adicionar(categoria);
            await _categoriaRepository.UnitOfWork.Commit();

            var response = new CategoriaResponse
            {
                Descricao = request.Descricao,
                Finalidade = request.Finalidade,
            };

            return new ApiResponse<CategoriaResponse>(201, "Categoria criada com sucesso", response);
        }

        public async Task<ApiResponse<List<ObterTodasCategoriasResponse>>> ObterTodasCategoriasAsync(int pagina, int tamanhoPagina)
        {
            var categorias = await _categoriaRepository.ObterTodasCategorias(pagina, tamanhoPagina);

            if (categorias.Count <= 0)
            {
                return new ApiResponse<List<ObterTodasCategoriasResponse>>(404, "Nenhuma categoria encontrada", null!);
            }

            var response = categorias.Select(c => new ObterTodasCategoriasResponse
            {
                Id = c.Id,
                Descricao = c.Descricao,
                Finalidade = c.Finalidade
            }).ToList();

            return new ApiResponse<List<ObterTodasCategoriasResponse>>(200, string.Empty, response);
        }

        public async Task<ApiResponse<CategoriaResponse>> ObterCategoriaPorIdAsync(Guid categoriaId)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(categoriaId);

            if (categoria is null)
                return new ApiResponse<CategoriaResponse>(404, "Categoria não encontrada", null!);

            var response = new CategoriaResponse
            {
                Descricao = categoria.Descricao,
                Finalidade = categoria.Finalidade,
            };

            return new ApiResponse<CategoriaResponse>(200, string.Empty, response);
        }

        public async Task<ApiResponse<ConsultaTotaisCategoriaResponse>> ConsultarTotaisPorCategoriaAsync()
        {
            var categorias = await _categoriaRepository.ObterCategoriasComTransacoes();

            var lista = categorias.Select(c => new CategoriasTotaisResponse
            {
                CategoriaId = c.Id,
                Descricao = c.Descricao,

                TotalReceitas = c.Transacoes
                    .Where(t => t.Tipo == ETipo.RECEITA)
                    .Sum(t => t.Valor),

                TotalDespesas = c.Transacoes
                    .Where(t => t.Tipo == ETipo.DESPESA)
                    .Sum(t => t.Valor)
            }).ToList();

            var response = new ConsultaTotaisCategoriaResponse
            {
                Categorias = lista,
                TotalGeralReceitas = lista.Sum(x => x.TotalReceitas),
                TotalGeralDespesas = lista.Sum(x => x.TotalDespesas)
            };

            return new ApiResponse<ConsultaTotaisCategoriaResponse>(200, string.Empty, response);
        }
    }
}
