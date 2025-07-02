namespace Finlytics.Application.DTOs;

public class AddDailyFinanceDto
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } // "Income" / "Expense"
    public string Category { get; set; }
    public string Description { get; set; }
}
