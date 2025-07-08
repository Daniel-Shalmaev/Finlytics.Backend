using MongoDB.Driver;

namespace Finlytics.Infrastructure.MongoRepositories;

public interface IMongoRawFilter<T>
{
    // Allows filtering using raw MongoDB filter definitions
    Task<List<T>> FilterByMongoFilterAsync(FilterDefinition<T> filter);
}
