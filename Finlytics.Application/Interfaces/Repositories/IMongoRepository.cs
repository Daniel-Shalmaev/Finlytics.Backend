using System.Linq.Expressions;

namespace Finlytics.Application.Interfaces.Repositories;

public interface IMongoRepository<T>
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task DeleteAsync(string id);
    Task<T> GetByIdAsync(string id);
    Task<List<T>> FilterByAsync(Expression<Func<T, bool>> filter);
}
