namespace ControleGastos.Application.Handlers.Pessoas.Responses
{
    public class PessoaTotaisResponse
    {
        public Guid PessoaId { get; set; }
        public string Nome { get; set; }

        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }

    public class ConsultaTotaisPessoasResponse
    {
        public List<PessoaTotaisResponse> Pessoas { get; set; }

        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }
        public decimal SaldoLiquido => TotalGeralReceitas - TotalGeralDespesas;
    }
}
