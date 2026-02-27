using ControleGastos.Application.Configurations;
using ControleGastos.Application.Handlers.Pessoas.Requests;
using ControleGastos.Application.Handlers.Pessoas.Responses;
using ControleGastos.Domain.Contexts.Pessoas;

namespace ControleGastos.Application.Handlers.Pessoas.Interfaces
{
    public interface IPessoaHandler
    {
        Task<ApiResponse<PessoaResponse>> CriarPessoaAsync(PessoaRequest request);
        Task<ApiResponse<PessoaResponse>> ObterPessoasPorIdAsync(Guid pessoaId);
        Task<ApiResponse<PessoaResponse>> AtualizarPessoaAsync(Guid pessoaId, PessoaRequest request);
        Task<ApiResponse<object>> DeletarPessoaAsync(Guid pessoaId);
        Task<ApiResponse<ConsultaTotaisPessoasResponse>> ObterTodasPessoasComTransacoesAsync();
    }
}
