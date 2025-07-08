namespace Finlytics.Application.DTOs;

public class AddDailyFinanceDto
{
    public string Type { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}
