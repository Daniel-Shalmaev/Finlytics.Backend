using Finlytics.Domain.Interfaces;

namespace Finlytics.Domain.Entities;

public class DailyFinance : IIdentifiable
{
    public string Id { get; set; } 
    public DateTime Date { get; set; }
    public decimal Income { get; set; }
    public decimal Outcome { get; set; }

}