using ControleGastos.Application.Configurations;
using ControleGastos.Application.Handlers.Transacoes.Requests;
using ControleGastos.Application.Handlers.Transacoes.Responses;
using ControleGastos.Domain.Contexts.Transacoes;

namespace ControleGastos.Application.Handlers.Transacoes.Interfaces
{
    public interface ITransacaoHandler
    {
        Task<ApiResponse<TransacaoResponse>> CriarTransacaoAsync(TransacaoRequest request);
        Task<ApiResponse<TransacaoResponse>> ObterTransacaoPorIdAsync(Guid transacaoId);
        Task<ApiResponse<List<Transacao>>> ObterTransacoesPorPessoaIdAsync(Guid pessoaId, int pagina, int tamanhoPagina);
    }
}
