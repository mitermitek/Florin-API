using Florin_API.Common;
using Florin_API.DTOs.Account;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class AccountMapper
{
    public static Account ToEntity(this AccountRequestDto dto)
    {
        return new Account
        {
            Name = dto.Name,
            StartingBalance = dto.StartingBalance
        };
    }

    public static AccountDto ToDto(this Account entity)
    {
        return new AccountDto
        {
            Id = entity.Id,
            Name = entity.Name,
            StartingBalance = entity.StartingBalance
        };
    }

    public static Pagination<AccountDto> ToDto(this Pagination<Account> pagination)
    {
        return new Pagination<AccountDto>
        {
            Items = pagination.Items.ToDtos(),
            Total = pagination.Total
        };
    }

    public static ICollection<AccountDto> ToDtos(this ICollection<Account> entities)
    {
        return [.. entities.Select(e => e.ToDto())];
    }
}
