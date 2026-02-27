using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ControleGastos.Domain.Contexts.Pessoas;

namespace ControleGastos.Infra.Data.Mapping
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.DataCadastro)
                   .IsRequired();

            builder.Property(p => p.DataUltimaAtualizacao)
                   .IsRequired();

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Idade)
                   .IsRequired();

            builder.HasMany(p => p.Transacoes)
                   .WithOne(t => t.Pessoa)
                   .HasForeignKey(t => t.PessoaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}