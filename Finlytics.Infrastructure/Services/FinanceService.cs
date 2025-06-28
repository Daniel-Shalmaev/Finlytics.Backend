using MongoDB.Driver;
using Finlytics.Domain.Models;
using Finlytics.Application.Interfaces;
using Finlytics.Infrastructure.Persistence;
using Finlytics.Infrastructure.Repositories;

namespace Finlytics.Infrastructure.Services;

public class FinanceService : MongoRepository<DailyFinance>, IFinanceService
{
    public FinanceService(MongoDbService mongoDbService) : base(mongoDbService, "Finance") { }

    // Retrieves financial data within the specified date range.
    public async Task<List<DailyFinance>> GetFinanceData(DateTime? from = null, DateTime? to = null)
    {
        // Set default date range if not provided
        from ??= new DateTime(2021, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        to ??= new DateTime(2021, 12, 31, 23, 59, 59, DateTimeKind.Utc);

        // Create a filter to query financial data based on date range
        var filter = Builders<DailyFinance>.Filter.And(
            Builders<DailyFinance>.Filter.Gte("date", from.Value.ToString("yyyy-MM-ddTHH:mm:ss")),
            Builders<DailyFinance>.Filter.Lte("date", to.Value.ToString("yyyy-MM-ddTHH:mm:ss"))
        );

        return await FindByFilter(filter);
    }
}
