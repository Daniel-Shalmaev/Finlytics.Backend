using Finlytics.Domain.Entities;

namespace Finlytics.Infrastructure.MongoModels;

// Handles mapping between MongoDB documents and domain entities.
public static class MongoMappings
{
    // Converts DailyFinanceDocument → DailyFinance (domain model)
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

    // Converts DailyFinance → DailyFinanceDocument (Mongo document)
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
