using ControleGastos.Domain.DomainObjects;
using ControleGastos.Domain.DomainObjects.Data;
using ControleGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Data.Respositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Adicionar(T entity)
    {
        _context.Add(entity);
    }

    public void Remover(T entity)
    {
        _context.Remove(entity);
    }

    //public void Dispose()
    //{
    //    _context?.Dispose();
    //}

}