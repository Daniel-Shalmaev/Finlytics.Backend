using Finlytics.Application.DTOs;

namespace Finlytics.Application.Interfaces;

public interface IFinanceService
{
    Task DeleteAsync(string id);
    Task<List<DailyFinanceDto>> GetAllAsync();
    Task<DailyFinanceDto> AddAsync(AddDailyFinanceDto dto);
    Task<DailyFinanceDto> UpdateAsync(UpdateDailyFinanceDto dto);
    Task<List<DailyFinanceDto>> GetFinanceData(DateTime? from, DateTime? to);
}
