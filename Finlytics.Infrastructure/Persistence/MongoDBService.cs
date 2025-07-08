using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Finlytics.Infrastructure.Persistence;

public class MongoDbService
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoDbService(IConfiguration config)
    {
        var connectionString = config["MongoDBSettings:ConnectionString"];
        var dbName = config["MongoDBSettings:DatabaseName"];

        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(dbName);
    }

    public IMongoDatabase GetDatabase() => _database;

    // Returns a collection of the specified type
    public IMongoCollection<T> GetCollection<T>(string collectionName) =>
        _database.GetCollection<T>(collectionName);
}