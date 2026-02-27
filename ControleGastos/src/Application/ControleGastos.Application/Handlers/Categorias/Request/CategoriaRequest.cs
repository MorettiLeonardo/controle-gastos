using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Handlers.Categorias.Request
{
    public class CategoriaRequest
    {
        public string Descricao { get; set; }
        public EFinalidade Finalidade { get; set; }
    }
}
