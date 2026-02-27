namespace ControleGastos.Domain.DomainObjects
{
    public abstract class BaseEntity
    {
        public Guid Id { get; init; }

        public DateTime DataCadastro { get; init; } = DateTime.UtcNow;

        public DateTime DataUltimaAtualizacao { get; set; } = DateTime.UtcNow;
    }
}
