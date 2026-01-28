using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface ITransactionService
{
    public Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, CancellationToken cancellationToken);
    public Task<Pagination<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken);
    public Task<Transaction> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, CancellationToken cancellationToken);
    public Task<Transaction> CreateTransactionByAccountIdAndUserIdAsync(int accountId, int userId, Transaction transaction, CancellationToken cancellationToken);
    public Task<Transaction> UpdateTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, Transaction transaction, CancellationToken cancellationToken);
    public Task DeleteTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, CancellationToken cancellationToken);
}
