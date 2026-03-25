using Florin_API.Common;
using Florin_API.DTOs.Requests;
using Florin_API.DTOs.Responses;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class AccountMapper
{
    public static Account ToEntity(this AccountRequest request)
    {
        return new Account
        {
            Name = request.Name,
            StartingBalance = request.StartingBalance
        };
    }

    public static AccountResponse ToResponse(this Account entity)
    {
        return new AccountResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            StartingBalance = entity.StartingBalance
        };
    }

    public static Pagination<AccountResponse> ToResponse(this Pagination<Account> pagination)
    {
        return new Pagination<AccountResponse>
        {
            Items = pagination.Items.ToResponses(),
            Total = pagination.Total
        };
    }

    public static ICollection<AccountResponse> ToResponses(this ICollection<Account> entities)
    {
        return [.. entities.Select(e => e.ToResponse())];
    }
}
