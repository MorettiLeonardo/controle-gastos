using ControleGastos.Domain.DomainObjects.Data;

namespace ControleGastos.Domain.Contexts.Pessoas.Interfaces
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<Pessoa?> ObterPessoaPorId(Guid pessoaId);
        Task<List<Pessoa>> ObterPessoas(int pagina, int tamanhoPagina);
        Task<List<Pessoa>> ObterTodasPessoasComTransacoes();
    }
}
