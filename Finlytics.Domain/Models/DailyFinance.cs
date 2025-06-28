using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finlytics.Domain.Models;

public class DailyFinance
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("income")]
    public decimal Income { get; set; }

    [BsonElement("outcome")]
    public decimal Outcome { get; set; }
}