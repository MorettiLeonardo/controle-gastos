namespace ControleGastos.Application.Handlers.Categorias.Response
{
    public class CategoriasTotaisResponse
    {
        public Guid CategoriaId { get; set; }
        public string Descricao { get; set; }

        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }

        public decimal Saldo => TotalReceitas - TotalDespesas;
    }

    public class ConsultaTotaisCategoriaResponse
    {
        public List<CategoriasTotaisResponse> Categorias { get; set; } = new();

        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }

        public decimal SaldoLiquido => TotalGeralReceitas - TotalGeralDespesas;
    }
}