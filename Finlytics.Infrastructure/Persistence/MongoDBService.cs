using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Finlytics.Infrastructure.Persistence;

public class MongoDbService
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoDbService(IConfiguration config)
    {
        _client = new MongoClient(config["MongoDBSettings:ConnectionString"]);
        _database = _client.GetDatabase(config["MongoDBSettings:DatabaseName"]);
    }

    // Returns a collection of the specified type
    public IMongoCollection<T> GetCollection<T>(string collectionName) =>
        _database.GetCollection<T>(collectionName);
}