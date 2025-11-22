using Florin_API.Entities;
using Florin_API.Exceptions.Transaction;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;

namespace Florin_API.Services;

public class TransactionService(IAccountService accountService, ICategoryService categoryService, ITransactionRepository transactionRepository) : ITransactionService
{
    public async Task<ICollection<Transaction>> GetTransactionsByAccountIdAndUserIdAsync(int accountId, int userId)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId);

        return await transactionRepository.GetTransactionsByAccountIdAndUserIdAsync(account.Id, userId);
    }

    public async Task<Transaction> GetTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId);

        return await transactionRepository.GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, account.Id, userId) ?? throw new TransactionNotFoundException();
    }

    public async Task<Transaction> CreateTransactionByAccountIdAndUserIdAsync(int accountId, int userId, Transaction transaction)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId);
        await categoryService.GetCategoryByIdAndUserIdAsync(transaction.CategoryId, userId);

        transaction.AccountId = account.Id;

        return await transactionRepository.CreateTransactionAsync(transaction);
    }

    public async Task<Transaction> UpdateTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId, Transaction transaction)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId);
        await categoryService.GetCategoryByIdAndUserIdAsync(transaction.CategoryId, userId);
        var existingTransaction = await GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId);

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

        return await transactionRepository.UpdateTransactionAsync(existingTransaction);
    }

    public async Task DeleteTransactionByIdAndAccountIdAndUserIdAsync(int transactionId, int accountId, int userId)
    {
        var account = await accountService.GetAccountByIdAndUserIdAsync(accountId, userId);
        var existingTransaction = await GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId);

        if (existingTransaction.AccountId != account.Id)
        {
            throw new TransactionNotFoundException("Transaction does not belong to the specified account.");
        }

        await transactionRepository.DeleteTransactionAsync(existingTransaction);
    }
}
