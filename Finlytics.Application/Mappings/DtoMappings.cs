using Finlytics.Application.DTOs;
using Finlytics.Domain.Entities;

namespace Finlytics.Application.Mappings;

public static class DtoMappings
{

    // Entity ↔ DTO
    public static DailyFinanceDto ToDto(this DailyFinance entity)
    {
        return new DailyFinanceDto
        {
            Id = entity.Id,
            Date = entity.Date,
            Income = entity.Income,
            Outcome = entity.Outcome
        };
    }

    // Converts DTO received from API to domain entity
    public static DailyFinance ToEntity(this DailyFinanceDto dto)
    {
        return new DailyFinance
        {
            Id = dto.Id,
            Date = dto.Date,
            Income = dto.Income,
            Outcome = dto.Outcome
        };
    }

    // Mapping AddDailyFinanceDto → Domain Entity
    public static DailyFinance ToEntity(this AddDailyFinanceDto dto)
    {
        return new DailyFinance
        {
            Id = Guid.NewGuid().ToString(),
            Date = dto.Date,
            Income = dto.Type == "Income" ? dto.Amount : 0,
            Outcome = dto.Type == "Outcome" ? dto.Amount : 0
        };
    }

    // Mapping UpdateDailyFinanceDto → Domain Entity
    public static DailyFinance ToEntity(this UpdateDailyFinanceDto dto)
    {
        return new DailyFinance
        {
            Id = dto.Id,
            Date = dto.Date,
            Income = dto.Type == "Income" ? dto.Amount : 0,
            Outcome = dto.Type == "Outcome" ? dto.Amount : 0
        };
    }

}