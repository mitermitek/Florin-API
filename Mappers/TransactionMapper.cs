using Florin_API.Common;
using Florin_API.DTOs.Requests;
using Florin_API.DTOs.Responses;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class TransactionMapper
{
    public static Transaction ToEntity(this TransactionRequest request)
    {
        return new Transaction
        {
            CategoryId = request.CategoryId,
            Type = request.Type,
            Amount = request.Amount,
            Date = request.Date,
            Title = request.Title,
            Description = request.Description,
        };
    }

    public static TransactionResponse ToResponse(this Transaction entity)
    {
        return new TransactionResponse
        {
            Id = entity.Id,
            Type = entity.Type,
            Amount = entity.Amount,
            Date = entity.Date,
            Title = entity.Title,
            Description = entity.Description,
            Category = CategoryMapper.ToResponse(entity.Category),
        };
    }

    public static Pagination<TransactionResponse> ToResponse(this Pagination<Transaction> pagination)
    {
        return new Pagination<TransactionResponse>
        {
            Items = pagination.Items.ToResponses(),
            Total = pagination.Total
        };
    }

    public static ICollection<TransactionResponse> ToResponses(this ICollection<Transaction> entities)
    {
        return [.. entities.Select(e => e.ToResponse())];
    }
}
