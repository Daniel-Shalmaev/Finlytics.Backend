using MongoDB.Driver;
using System.Linq.Expressions;
using Finlytics.Domain.Interfaces;
using Finlytics.Infrastructure.MongoRepositories;
using Finlytics.Application.Interfaces.Repositories;

namespace Finlytics.Infrastructure.Repositories.MongoRepositories;

// Generic MongoDB repository using IIdentifiable and type-to-collection mapping
public class MongoRepository<T> : IMongoRepository<T>, IMongoRawFilter<T> where T : IIdentifiable
{
    private readonly IMongoCollection<T> _collection;

    // Initializes the repository by resolving the collection name
    public MongoRepository(IMongoDatabase database)
    {
        var type = typeof(T);
        if (!CollectionMapping.Names.TryGetValue(type, out var collectionName))
            collectionName = type.Name;

        _collection = database.GetCollection<T>(collectionName);
    }

    #region CRUD

    public async Task<List<T>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter);
    }

    #endregion

    #region Filters

    // Filters using LINQ expression (standard query)
    public async Task<List<T>> FilterByAsync(Expression<Func<T, bool>> filter) =>
        await _collection.Find(filter).ToListAsync();

    // Filters using native MongoDB filter (advanced use cases)
    public async Task<List<T>> FilterByMongoFilterAsync(FilterDefinition<T> filter) =>
        await _collection.Find(filter).ToListAsync();

    #endregion
}
