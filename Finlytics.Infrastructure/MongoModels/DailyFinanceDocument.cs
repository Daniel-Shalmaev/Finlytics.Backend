using Finlytics.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finlytics.Infrastructure.MongoModels;

public class DailyFinanceDocument : IIdentifiable
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("income")]
    public decimal Income { get; set; }

    [BsonElement("outcome")]
    public decimal Outcome { get; set; }
}
