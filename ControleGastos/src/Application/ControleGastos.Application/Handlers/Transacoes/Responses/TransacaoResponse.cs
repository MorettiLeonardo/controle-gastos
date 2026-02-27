using ControleGastos.Domain.Contexts.Categorias;
using ControleGastos.Domain.Contexts.Categorias.Enums;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Handlers.Transacoes.Responses
{
    public class TransacaoResponse
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public ETipo Tipo { get; set; }
        public Categoria Categoria { get; set; } = null!;
    }
}
