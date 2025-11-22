using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface ITransactionRepository
{
    public Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId);
    public Task<Pagination<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, PaginationFilter paginationFilter);
    public Task<Transaction?> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId);
    public Task<Transaction> CreateTransactionAsync(Transaction transaction);
    public Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    public Task DeleteTransactionAsync(Transaction transaction);
}
