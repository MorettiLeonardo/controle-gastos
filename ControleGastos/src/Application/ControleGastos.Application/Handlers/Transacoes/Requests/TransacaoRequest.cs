using ControleGastos.Domain.Contexts.Categorias.Enums;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Handlers.Transacoes.Requests
{
    public class TransacaoRequest
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public ETipo Tipo { get; set; }
        public Guid PessoaId { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
