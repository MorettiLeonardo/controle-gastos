using ControleGastos.Domain.Contexts.Transacoes;
using ControleGastos.Domain.DomainObjects;

namespace ControleGastos.Domain.Contexts.Pessoas
{
    public class Pessoa : BaseEntity
    {
        protected Pessoa() { }

        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public string Nome { get; private set; } = string.Empty;
        public int Idade { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = [];

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }

        public void AtualizarDados(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }
    }
}
