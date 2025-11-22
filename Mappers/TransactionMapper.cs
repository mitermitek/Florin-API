using Florin_API.Common;
using Florin_API.DTOs.Transaction;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class TransactionMapper
{
    public static Transaction ToEntity(this CreateTransactionDTO dto)
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

    public static Transaction ToEntity(this UpdateTransactionDTO dto)
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

    public static TransactionDTO ToDTO(this Transaction entity)
    {
        return new TransactionDTO
        {
            Id = entity.Id,
            Type = entity.Type,
            Amount = entity.Amount,
            Date = entity.Date,
            Title = entity.Title,
            Description = entity.Description,
            Category = CategoryMapper.ToDTO(entity.Category),
        };
    }

    public static ICollection<TransactionDTO> ToDTOs(this ICollection<Transaction> entities)
    {
        return [.. entities.Select(e => e.ToDTO())];
    }

    public static Pagination<TransactionDTO> ToDTO(this Pagination<Transaction> pagination)
    {
        return new Pagination<TransactionDTO>
        {
            Items = pagination.Items.ToDTOs(),
            Total = pagination.Total
        };
    }
}
