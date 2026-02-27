using ControleGastos.Domain.DomainObjects.Data;

namespace ControleGastos.Domain.Contexts.Transacoes.Interfaces
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        Task<List<Transacao>> ObterTransacaoPorPessoaId(Guid pessoaId, int pagina, int tamanhoPagina);
        Task<Transacao?> ObterTransacaoPorId(Guid transacaoId);
    }
}
