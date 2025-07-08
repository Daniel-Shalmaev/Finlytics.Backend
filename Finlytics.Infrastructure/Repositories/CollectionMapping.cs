using Finlytics.Domain.Entities;
using Finlytics.Infrastructure.MongoModels;

namespace Finlytics.Infrastructure.MongoRepositories;

public static class CollectionMapping
{
    public static readonly Dictionary<Type, string> Names = new()
    {
        { typeof(User), "Users" },
        { typeof(DailyFinanceDocument), "FinanceData" }
    };
}
