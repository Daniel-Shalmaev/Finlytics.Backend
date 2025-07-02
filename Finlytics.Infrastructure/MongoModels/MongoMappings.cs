using Finlytics.Domain.Entities;

namespace Finlytics.Infrastructure.MongoModels;

public static class MongoMappings
{
    // Document ↔ Entity
    public static DailyFinance ToEntity(this DailyFinanceDocument doc)
    {
        return new DailyFinance
        {
            Id = doc.Id,
            Date = doc.Date,
            Income = doc.Income,
            Outcome = doc.Outcome
        };
    }

    public static DailyFinanceDocument ToDocument(this DailyFinance entity)
    {
        return new DailyFinanceDocument
        {
            Id = entity.Id,
            Date = entity.Date,
            Income = entity.Income,
            Outcome = entity.Outcome
        };
    }
}
