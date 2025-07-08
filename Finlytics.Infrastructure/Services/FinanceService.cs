using MongoDB.Driver;
using Finlytics.Application.DTOs;
using Finlytics.Application.Mappings;
using Finlytics.Application.Interfaces;
using Finlytics.Infrastructure.MongoModels;
using Finlytics.Application.Interfaces.Repositories;
using Finlytics.Infrastructure.Repositories.MongoRepositories;

namespace Finlytics.Infrastructure.Services;

// Service that manages financial records using MongoDB
public class FinanceService(IMongoRepository<DailyFinanceDocument> repository) : IFinanceService
{
    private readonly IMongoRepository<DailyFinanceDocument> _repository = repository;

    #region Add

    // Adds a new finance entry and returns it as DTO
    public async Task<DailyFinanceDto> AddAsync(AddDailyFinanceDto dto)
    {
        var entity = dto.ToEntity();
        var doc = entity.ToDocument();
        await _repository.AddAsync(doc);
        return entity.ToDto();
    }

    #endregion

    #region Update

    // Updates an existing finance record
    public async Task<DailyFinanceDto> UpdateAsync(UpdateDailyFinanceDto dto)
    {
        var entity = dto.ToEntity();
        var doc = entity.ToDocument();
        await _repository.UpdateAsync(doc);
        return entity.ToDto();
    }

    #endregion

    #region Delete

    // Deletes a finance record by ID
    public async Task DeleteAsync(string id) =>
        await _repository.DeleteAsync(id);

    #endregion

    #region Get All

    public async Task<List<DailyFinanceDto>> GetAllAsync()
    {
        var documents = await _repository.GetAllAsync();
        return [.. documents.Select(d => d.ToEntity().ToDto())];
    }

    #endregion

    #region Get by Date Range

    // Retrieves finance entries between two dates using raw Mongo filter
    public async Task<List<DailyFinanceDto>> GetFinanceData(DateTime? from, DateTime? to)
    {
        from ??= new DateTime(2021, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        to ??= new DateTime(2021, 12, 31, 23, 59, 59, DateTimeKind.Utc);

        var fromString = from.Value.ToString("yyyy-MM-ddTHH:mm:ss");
        var toString = to.Value.ToString("yyyy-MM-ddTHH:mm:ss");

        var filter = Builders<DailyFinanceDocument>.Filter.And(
            Builders<DailyFinanceDocument>.Filter.Gte("date", fromString),
            Builders<DailyFinanceDocument>.Filter.Lte("date", toString)
        );

        var concreteRepo = _repository as MongoRepository<DailyFinanceDocument>;
        if (concreteRepo == null)
            throw new InvalidCastException("MongoRepository expected");

        var documents = await concreteRepo.FilterByMongoFilterAsync(filter);

        return [.. documents.Select(d => d.ToEntity().ToDto())];
    }

    #endregion
}
