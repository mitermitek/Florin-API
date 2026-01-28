using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface ITransactionRepository
{
    public Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, CancellationToken cancellationToken);
    public Task<Pagination<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken);
    public Task<Transaction?> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, CancellationToken cancellationToken);
    public Task<Transaction> CreateTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    public Task<Transaction> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    public Task DeleteTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
}
