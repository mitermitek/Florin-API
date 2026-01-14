using Florin_API.Common;
using Florin_API.DTOs.Transaction;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class TransactionMapper
{
    public static Transaction ToEntity(this CreateTransactionDto dto)
    {
        return new Transaction
        {
            CategoryId = dto.CategoryId,
            Type = dto.Type,
            Amount = dto.Amount,
            Date = dto.Date,
            Title = dto.Title,
            Description = dto.Description,
        };
    }

    public static Transaction ToEntity(this UpdateTransactionDto dto)
    {
        return new Transaction
        {
            CategoryId = dto.CategoryId,
            Type = dto.Type,
            Amount = dto.Amount,
            Date = dto.Date,
            Title = dto.Title,
            Description = dto.Description,
        };
    }

    public static TransactionDto ToDto(this Transaction entity)
    {
        return new TransactionDto
        {
            Id = entity.Id,
            Type = entity.Type,
            Amount = entity.Amount,
            Date = entity.Date,
            Title = entity.Title,
            Description = entity.Description,
            Category = CategoryMapper.ToDto(entity.Category),
        };
    }

    public static Pagination<TransactionDto> ToDto(this Pagination<Transaction> pagination)
    {
        return new Pagination<TransactionDto>
        {
            Items = pagination.Items.ToDtos(),
            Total = pagination.Total
        };
    }

    public static ICollection<TransactionDto> ToDtos(this ICollection<Transaction> entities)
    {
        return [.. entities.Select(e => e.ToDto())];
    }
}
