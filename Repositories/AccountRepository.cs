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

    public async Task<Account?> GetAccountByIdAndUserIdAsync(int id, int userId)
    {
        return await ctx.Accounts.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<Account> CreateAccountAsync(Account category)
    {
        ctx.Accounts.Add(category);
        await ctx.SaveChangesAsync();

        return category;
    }

    public async Task<Account> UpdateAccountAsync(Account category)
    {
        ctx.Accounts.Update(category);
        await ctx.SaveChangesAsync();

        return category;
    }

    public async Task DeleteAccountAsync(Account category)
    {
        ctx.Accounts.Remove(category);
        await ctx.SaveChangesAsync();
    }

    public async Task<bool> IsAccountExistsByNameAndUserIdAsync(string name, int userId)
    {
        return await ctx.Accounts.AnyAsync(c => c.Name == name && c.UserId == userId);
    }
}
