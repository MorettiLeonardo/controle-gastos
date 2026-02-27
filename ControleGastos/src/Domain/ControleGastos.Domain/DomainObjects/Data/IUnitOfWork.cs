namespace ControleGastos.Domain.DomainObjects.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}

