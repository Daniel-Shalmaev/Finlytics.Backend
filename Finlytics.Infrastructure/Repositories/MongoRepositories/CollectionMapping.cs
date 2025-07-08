using Finlytics.Domain.Entities;
using Finlytics.Infrastructure.MongoModels;

namespace Finlytics.Infrastructure.Repositories.MongoRepositories;

// Maps entity/document types to their MongoDB collection names
public static class CollectionMapping
{
    public static readonly Dictionary<Type, string> Names = new()
    {
        { typeof(User), "Users" },
        { typeof(DailyFinanceDocument), "Finance" }
    };
}
