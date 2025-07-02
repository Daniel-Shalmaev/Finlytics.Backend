namespace Finlytics.Application.DTOs;

public class DailyFinanceDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Income { get; set; }
    public decimal Outcome { get; set; }
}

