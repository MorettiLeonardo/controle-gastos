using ControleGastos.Domain.Contexts.Categorias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastos.Infra.Data.Mapping
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.DataCadastro)
                   .IsRequired();

            builder.Property(c => c.DataUltimaAtualizacao)
                   .IsRequired();

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasMaxLength(400);

            builder.Property(c => c.Finalidade)
                   .IsRequired()
                   .HasConversion<string>();
        }
    }
}
