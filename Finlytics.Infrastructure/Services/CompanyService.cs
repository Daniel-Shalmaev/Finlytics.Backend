using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Finlytics.Application.Interfaces.Repositories;
using Finlytics.Domain.Entities;

namespace Finlytics.Infrastructure.Services;

public class CompanyService(IMongoRepository<Company> repository) : ICompanyService
{
    private readonly IMongoRepository<Company> _repository = repository;

    public async Task<List<CompanyDto>> GetAllAsync()
    {
        var companies = await _repository.GetAllAsync();
        return companies.Select(c => new CompanyDto { Id = c.Id, Name = c.Name }).ToList();
    }

    public async Task<CompanyDto> AddAsync(CompanyDto dto)
    {
        var entity = new Company { Name = dto.Name };
        await _repository.AddAsync(entity);
        return new CompanyDto { Id = entity.Id, Name = entity.Name };
    }
}

