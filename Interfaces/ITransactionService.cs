using Florin_API.DTOs;
using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId);
    Task<PagedResultDTO<Transaction>> GetUserTransactionsAsync(int userId, int page, int pageSize);
    Task<Transaction> GetTransactionByIdAndUserIdAsync(int transactionId, int userId);
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
    Task<Transaction> UpdateTransactionAsync(Transaction existingTransaction, Transaction transactionUpdate);
    Task DeleteTransactionAsync(Transaction transaction);
}
