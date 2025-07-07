using Finlytics.Application.DTOs;

namespace Finlytics.Application.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyDto>> GetAllAsync();
    Task<CompanyDto> AddAsync(CompanyDto dto);
}
