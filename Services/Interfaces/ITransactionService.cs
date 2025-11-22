using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface ITransactionService
{
    public Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId);
    public Task<Transaction> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId);
    public Task<Transaction> CreateTransactionByAccountIdAndUserIdAsync(int accountId, int userId, Transaction transaction);
    public Task<Transaction> UpdateTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, Transaction transaction);
    public Task DeleteTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId);
}
