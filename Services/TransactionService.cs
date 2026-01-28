using Florin_API.Common;
using Florin_API.Entities;
using Florin_API.Exceptions.Transaction;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;

namespace Florin_API.Services;

public class TransactionService(IAccountService accountService, ICategoryService categoryService, ITransactionRepository transactionRepository) : ITransactionService
{
    public async Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, CancellationToken cancellationToken)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId, cancellationToken);

        return await transactionRepository.GetTransactionsByAccountIdAndUserIdAsync(account.Id, userId, cancellationToken);
    }

    public async Task<Pagination<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId, cancellationToken);

        return await transactionRepository.GetTransactionsByAccountIdAndUserIdAsync(account.Id, userId, paginationFilter, cancellationToken);
    }

    public async Task<Transaction> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, CancellationToken cancellationToken)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId, cancellationToken);

        return await transactionRepository.GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, account.Id, userId, cancellationToken) ?? throw new TransactionNotFoundException();
    }

    public async Task<Transaction> CreateTransactionByAccountIdAndUserIdAsync(int accountId, int userId, Transaction transaction, CancellationToken cancellationToken)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId, cancellationToken);
        await categoryService.GetCategoryByIdAndUserIdAsync(transaction.CategoryId, userId, cancellationToken);

        transaction.AccountId = account.Id;

        return await transactionRepository.CreateTransactionAsync(transaction, cancellationToken);
    }

    public async Task<Transaction> UpdateTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, Transaction transaction, CancellationToken cancellationToken)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId, cancellationToken);
        await categoryService.GetCategoryByIdAndUserIdAsync(transaction.CategoryId, userId, cancellationToken);
        var existingTransaction = await GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId, cancellationToken);

        if (existingTransaction.AccountId != account.Id)
        {
            throw new TransactionNotFoundException("Transaction does not belong to the specified account.");
        }

        existingTransaction.CategoryId = transaction.CategoryId;
        existingTransaction.Type = transaction.Type;
        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Date = transaction.Date;
        existingTransaction.Title = transaction.Title;
        existingTransaction.Description = transaction.Description;

        return await transactionRepository.UpdateTransactionAsync(existingTransaction, cancellationToken);
    }

    public async Task DeleteTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, CancellationToken cancellationToken)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId, cancellationToken);
        var existingTransaction = await GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId, cancellationToken);

        if (existingTransaction.AccountId != account.Id)
        {
            throw new TransactionNotFoundException("Transaction does not belong to the specified account.");
        }

        await transactionRepository.DeleteTransactionAsync(existingTransaction, cancellationToken);
    }
}
