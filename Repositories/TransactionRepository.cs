using Florin_API.Data;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class TransactionRepository(FlorinDbContext context) : ITransactionRepository
{
    public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId)
    {
        return await context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Transaction> transactions, int totalCount)> GetTransactionsByUserIdAsync(int userId, int skip, int take)
    {
        var query = context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.Id);

        var totalCount = await query.CountAsync();
        var transactions = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return (transactions, totalCount);
    }

    public async Task<Transaction?> GetTransactionByIdAndUserIdAsync(int transactionId, int userId)
    {
        return await context.Transactions
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId);
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        // Reload with category information
        return await GetTransactionByIdAndUserIdAsync(transaction.Id, transaction.UserId) ?? transaction;
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        context.Transactions.Update(transaction);
        await context.SaveChangesAsync();

        // Reload with category information
        return await GetTransactionByIdAndUserIdAsync(transaction.Id, transaction.UserId) ?? transaction;
    }

    public async Task DeleteTransactionAsync(Transaction transaction)
    {
        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();
    }

    public async Task<bool> TransactionExistsAsync(int transactionId, int userId)
    {
        return await context.Transactions
            .AnyAsync(t => t.Id == transactionId && t.UserId == userId);
    }
}
