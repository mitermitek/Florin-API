using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId);
    Task<(IEnumerable<Transaction> transactions, int totalCount)> GetTransactionsByUserIdAsync(int userId, int skip, int take);
    Task<Transaction?> GetTransactionByIdAndUserIdAsync(int transactionId, int userId);
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
    Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    Task DeleteTransactionAsync(Transaction transaction);
    Task<bool> TransactionExistsAsync(int transactionId, int userId);
}
