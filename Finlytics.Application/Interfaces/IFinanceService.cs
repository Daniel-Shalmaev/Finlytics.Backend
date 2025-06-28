using Finlytics.Domain.Models;

namespace Finlytics.Application.Interfaces;

public interface IFinanceService
{
    Task<List<DailyFinance>> GetFinanceData(DateTime? from = null, DateTime? to = null);
}
