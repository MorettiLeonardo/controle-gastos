using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Handlers.Categorias.Response
{
    public class CategoriaResponse
    {
        public string Descricao { get; set; }
        public EFinalidade Finalidade { get; set; }
    }
}
