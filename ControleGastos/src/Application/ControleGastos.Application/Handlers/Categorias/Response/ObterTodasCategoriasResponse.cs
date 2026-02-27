using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Handlers.Categorias.Response
{
    public class ObterTodasCategoriasResponse
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public EFinalidade Finalidade { get; set; }
    }
}
