using MongoDB.Driver;

namespace Finlytics.Infrastructure.MongoRepositories;

public interface IMongoRawFilter<T>
{
    Task<List<T>> FilterByMongoFilterAsync(FilterDefinition<T> filter);
}
