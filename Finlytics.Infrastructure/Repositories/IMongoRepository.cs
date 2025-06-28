using MongoDB.Driver;

namespace Finlytics.Infrastructure.Repositories;

public interface IMongoRepository<T>
{
    Task<List<T>> GetAll();
    Task<List<T>> FindByFilter(FilterDefinition<T> filter);
}
