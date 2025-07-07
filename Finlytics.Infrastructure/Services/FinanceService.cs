using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Finlytics.Application.Interfaces.Repositories;
using Finlytics.Application.Mappings;
using Finlytics.Infrastructure.MongoModels;
using Finlytics.Infrastructure.Repositories;
using MongoDB.Driver;


namespace Finlytics.Infrastructure.Services;


public class FinanceService(IMongoRepository<DailyFinanceDocument> repository) : IFinanceService
{
    private readonly IMongoRepository<DailyFinanceDocument> _repository = repository;

    public async Task<List<DailyFinanceDto>> GetAllAsync()
    {
        var documents = await _repository.GetAllAsync();
        return [.. documents.Select(d => d.ToEntity().ToDto())];
    }

    public async Task<DailyFinanceDto> AddAsync(AddDailyFinanceDto dto)
    {
        var entity = dto.ToEntity();
        var doc = entity.ToDocument();
        await _repository.AddAsync(doc);
        return entity.ToDto();
    }

    public async Task<List<DailyFinanceDto>> GetFinanceData(DateTime? from, DateTime? to)
    {
        from ??= new DateTime(2021, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        to ??= new DateTime(2021, 12, 31, 23, 59, 59, DateTimeKind.Utc);

        var fromString = from.Value.ToString("yyyy-MM-ddTHH:mm:ss");
        var toString = to.Value.ToString("yyyy-MM-ddTHH:mm:ss");

        // Converts query range to ISO string filters for Mongo
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

    public async Task<DailyFinanceDto> UpdateAsync(UpdateDailyFinanceDto dto)
    {
        var entity = dto.ToEntity();
        var doc = entity.ToDocument();
        await _repository.UpdateAsync(doc);
        return entity.ToDto();
    }

    public async Task DeleteAsync(string id) => await _repository.DeleteAsync(id);
}
