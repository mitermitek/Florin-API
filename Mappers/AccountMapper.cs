using Florin_API.Common;
using Florin_API.DTOs.Account;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class AccountMapper
{
    public static Account ToEntity(this CreateAccountDTO dto)
    {
        return new Account
        {
            Name = dto.Name,
            StartingBalance = dto.StartingBalance
        };
    }

    public static Account ToEntity(this UpdateAccountDTO dto)
    {
        return new Account
        {
            Name = dto.Name,
            StartingBalance = dto.StartingBalance
        };
    }

    public static AccountDTO ToDTO(this Account entity)
    {
        return new AccountDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            StartingBalance = entity.StartingBalance
        };
    }

    public static ICollection<AccountDTO> ToDTOs(this ICollection<Account> entities)
    {
        return [.. entities.Select(e => e.ToDTO())];
    }

    public static Pagination<AccountDTO> ToDTO(this Pagination<Account> pagination)
    {
        return new Pagination<AccountDTO>
        {
            Items = pagination.Items.ToDTOs(),
            Total = pagination.Total
        };
    }
}
