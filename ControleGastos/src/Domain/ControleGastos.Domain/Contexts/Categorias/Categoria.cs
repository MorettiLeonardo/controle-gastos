using ControleGastos.Domain.Contexts.Transacoes;
using ControleGastos.Domain.DomainObjects;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.Contexts.Categorias
{
    public class Categoria : BaseEntity
    {
        public Categoria(string descricao, EFinalidade finalidade)
        {
            Descricao = descricao;
            Finalidade = finalidade;
        }

        public string Descricao { get; private set; } = string.Empty;
        public EFinalidade Finalidade { get; private set; }
        public ICollection<Transacao> Transacoes { get; set; } = [];

    }
}
