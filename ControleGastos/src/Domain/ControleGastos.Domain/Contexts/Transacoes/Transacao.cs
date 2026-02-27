using ControleGastos.Domain.Contexts.Categorias;
using ControleGastos.Domain.Contexts.Categorias.Enums;
using ControleGastos.Domain.Contexts.Pessoas;
using ControleGastos.Domain.DomainObjects;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.Contexts.Transacoes
{
    public class Transacao : BaseEntity
    {
        protected Transacao() { }

        public Transacao(string descricao, decimal valor, ETipo tipo, Guid pessoaId, Guid categoriaId)
        {
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
            PessoaId = pessoaId;
            CategoriaId = categoriaId;
        }

        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public ETipo Tipo { get; private set; }

        public Guid CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; } = null!;

        public Guid PessoaId { get; private set; }
        public Pessoa Pessoa { get; private set; } = null!;
    }
}