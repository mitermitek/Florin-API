using Florin_API.Common;
using Florin_API.Data;
using Florin_API.Entities;
using Florin_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class TransactionRepository(FlorinDbContext ctx) : ITransactionRepository
{
    public async Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, CancellationToken cancellationToken)
    {
        return await ctx.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .Where(t => t.AccountId == accountId && t.Account.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Pagination<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken)
    {
        var query = ctx.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .Where(t => t.AccountId == accountId && t.Account.UserId == userId);

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(t => t.Date)
            .Skip((paginationFilter.Page - 1) * paginationFilter.Size)
            .Take(paginationFilter.Size)
            .ToListAsync(cancellationToken);

        return new Pagination<Transaction>
        {
            Items = items,
            Total = total
        };
    }

    public async Task<Transaction?> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, CancellationToken cancellationToken)
    {
        return await ctx.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == transactionId && t.AccountId == accountId && t.Account.UserId == userId, cancellationToken);
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        ctx.Transactions.Add(transaction);
        await ctx.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        ctx.Transactions.Update(transaction);
        await ctx.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task DeleteTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        ctx.Transactions.Remove(transaction);
        await ctx.SaveChangesAsync(cancellationToken);
    }
}
