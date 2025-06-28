using Finlytics.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Finlytics.Infrastructure.Repositories;

public class MongoRepository<T> : IMongoRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    // Initializes the repository with the specified collection
    public MongoRepository(MongoDbService mongoDbService, string collectionName) =>
        _collection = mongoDbService.GetCollection<T>(collectionName);

    // Retrieves all documents from the collection
    public async Task<List<T>> GetAll() => await _collection.Find(_ => true).ToListAsync();

    // Retrieves documents that match the given filter
    public async Task<List<T>> FindByFilter(FilterDefinition<T> filter) =>
        await _collection.Find(filter).ToListAsync();

}