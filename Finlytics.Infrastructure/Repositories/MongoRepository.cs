using Finlytics.Domain.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Finlytics.Infrastructure.Repositories;

public class MongoRepository<T>(IMongoDatabase database) : IMongoRepository<T> where T : IIdentifiable
{
    private readonly IMongoCollection<T> _collection = database.GetCollection<T>("Finance");

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

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

    public async Task<List<T>> FilterByMongoFilterAsync(FilterDefinition<T> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

}
