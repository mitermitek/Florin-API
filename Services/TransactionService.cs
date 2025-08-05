using Florin_API.DTOs;
using Florin_API.Exceptions;
using Florin_API.Helpers;
using Florin_API.Interfaces;
using Florin_API.Models;

namespace Florin_API.Services;

public class TransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository) : ITransactionService
{
    public async Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId)
    {
        return await transactionRepository.GetTransactionsByUserIdAsync(userId);
    }

    public async Task<PagedResultDTO<Transaction>> GetUserTransactionsAsync(int userId, int page, int pageSize)
    {
        var (skip, take) = PaginationHelper.CalculateSkipTake(page, pageSize);
        var (transactions, totalCount) = await transactionRepository.GetTransactionsByUserIdAsync(userId, skip, take);

        return PaginationHelper.CreatePagedResult(transactions, totalCount, page, pageSize);
    }

    public async Task<Transaction> GetTransactionByIdAndUserIdAsync(int transactionId, int userId)
    {
        var transaction = await transactionRepository.GetTransactionByIdAndUserIdAsync(transactionId, userId);
        if (transaction == null)
        {
            throw new TransactionNotFoundException($"Transaction with ID {transactionId} not found for user {userId}");
        }

        return transaction;
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        // Verify that the category belongs to the user
        var categoryExists = await categoryRepository.CategoryExistsAsync(transaction.CategoryId, transaction.UserId);
        if (!categoryExists)
        {
            throw new CategoryNotFoundException($"Category with ID {transaction.CategoryId} not found for user {transaction.UserId}");
        }

        return await transactionRepository.CreateTransactionAsync(transaction);
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction existingTransaction, Transaction transactionUpdate)
    {
        // Verify that the category belongs to the user
        var categoryExists = await categoryRepository.CategoryExistsAsync(transactionUpdate.CategoryId, existingTransaction.UserId);
        if (!categoryExists)
        {
            throw new CategoryNotFoundException($"Category with ID {transactionUpdate.CategoryId} not found for user {existingTransaction.UserId}");
        }

        // Update the existing transaction properties
        existingTransaction.Type = transactionUpdate.Type;
        existingTransaction.Date = transactionUpdate.Date;
        existingTransaction.Amount = transactionUpdate.Amount;
        existingTransaction.Description = transactionUpdate.Description;
        existingTransaction.CategoryId = transactionUpdate.CategoryId;

        return await transactionRepository.UpdateTransactionAsync(existingTransaction);
    }

    public async Task DeleteTransactionAsync(Transaction transaction)
    {
        await transactionRepository.DeleteTransactionAsync(transaction);
    }
}
