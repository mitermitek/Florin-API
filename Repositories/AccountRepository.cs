using Florin_API.Common;
using Florin_API.Data;
using Florin_API.Entities;
using Florin_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class AccountRepository(FlorinDbContext ctx) : IAccountRepository
{
    public async Task<ICollection<Account>> GetAccountsByUserIdAsync(int userId)
    {
        return await ctx.Accounts
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Pagination<Account>> GetAccountsByUserIdAsync(int userId, PaginationFilter paginationFilter)
    {
        var query = ctx.Accounts.Where(c => c.UserId == userId);

        var total = await query.CountAsync();
        var items = await query
            .Skip((paginationFilter.Page - 1) * paginationFilter.Size)
            .Take(paginationFilter.Size)
            .ToListAsync();

        return new Pagination<Account>
        {
            Items = items,
            Total = total
        };
    }

    public async Task<Account?> GetAccountByIdAndUserIdAsync(int id, int userId)
    {
        return await ctx.Accounts.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        ctx.Accounts.Add(account);
        await ctx.SaveChangesAsync();

        return account;
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        ctx.Accounts.Update(account);
        await ctx.SaveChangesAsync();

        return account;
    }

    public async Task DeleteAccountAsync(Account account)
    {
        ctx.Accounts.Remove(account);
        await ctx.SaveChangesAsync();
    }

    public async Task<bool> IsAccountExistsByNameAndUserIdAsync(string name, int userId)
    {
        return await ctx.Accounts.AnyAsync(c => c.Name == name && c.UserId == userId);
    }
}
