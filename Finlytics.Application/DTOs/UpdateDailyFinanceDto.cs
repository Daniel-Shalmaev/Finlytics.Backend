namespace Finlytics.Application.DTOs;

public class UpdateDailyFinanceDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}
