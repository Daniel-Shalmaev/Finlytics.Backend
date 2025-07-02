using System.Linq.Expressions;

namespace Finlytics.Application.Interfaces.Repositories;

public interface IMongoRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
    Task<List<T>> FilterByAsync(Expression<Func<T, bool>> filter);
}
