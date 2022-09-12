namespace Calc.Data.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> All();
    Task<bool> Add(T entity);
    Task<bool> Delete(int Id);
    Task<bool> Upsert(T entity);
}