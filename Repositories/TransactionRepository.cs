using Florin_API.Common;
using Florin_API.Data;
using Florin_API.Entities;
using Florin_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class TransactionRepository(FlorinDbContext ctx) : ITransactionRepository
{
    public async Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId)
    {
        return await ctx.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .Where(t => t.AccountId == accountId && t.Account.UserId == userId)
            .ToListAsync();
    }

    public async Task<Pagination<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, PaginationFilter paginationFilter)
    {
        var query = ctx.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .Where(t => t.AccountId == accountId && t.Account.UserId == userId);

        var total = await query.CountAsync();
        var items = await query
            .Skip((paginationFilter.Page - 1) * paginationFilter.Size)
            .Take(paginationFilter.Size)
            .ToListAsync();

        return new Pagination<Transaction>
        {
            Items = items,
            Total = total
        };
    }

    public async Task<Transaction?> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId)
    {
        return await ctx.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == transactionId && t.AccountId == accountId && t.Account.UserId == userId);
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        ctx.Transactions.Add(transaction);
        await ctx.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        ctx.Transactions.Update(transaction);
        await ctx.SaveChangesAsync();

        return transaction;
    }

    public async Task DeleteTransactionAsync(Transaction transaction)
    {
        ctx.Transactions.Remove(transaction);
        await ctx.SaveChangesAsync();
    }
}
