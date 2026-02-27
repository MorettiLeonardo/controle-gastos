using ControleGastos.Domain.Contexts.Categorias;
using ControleGastos.Domain.Contexts.Pessoas;
using ControleGastos.Domain.Contexts.Transacoes;
using ControleGastos.Domain.DomainObjects.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        #region Constructor

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        #endregion

        #region DbSets

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        #endregion

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}