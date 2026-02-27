namespace ControleGastos.Domain.DomainObjects.Data;

public interface IRepository<T> 
{

    IUnitOfWork UnitOfWork { get; }

    void Adicionar(T entity);

    void Remover(T entity);

}
