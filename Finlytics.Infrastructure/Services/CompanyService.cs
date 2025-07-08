using Finlytics.Domain.Entities;
using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Finlytics.Application.Interfaces.Repositories;

namespace Finlytics.Infrastructure.Services;

// Service that manages company data using a generic Mongo repository
public class CompanyService(IMongoRepository<Company> repository) : ICompanyService
{
    private readonly IMongoRepository<Company> _repository = repository;

    public async Task<List<CompanyDto>> GetAllAsync()
    {
        var companies = await _repository.GetAllAsync();
        return companies.Select(c => new CompanyDto { Id = c.Id, Name = c.Name }).ToList();
    }

    // Adds a new company and returns its DTO with generated ID
    public async Task<CompanyDto> AddAsync(CompanyDto dto)
    {
        var entity = new Company { Name = dto.Name };
        await _repository.AddAsync(entity);
        return new CompanyDto { Id = entity.Id, Name = entity.Name };
    }
}

