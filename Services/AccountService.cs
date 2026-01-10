using Florin_API.Common;
using Florin_API.Entities;
using Florin_API.Exceptions.Account;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;

namespace Florin_API.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    public async Task<ICollection<Account>> GetAccountsByUserIdAsync(int userId)
    {
        return await accountRepository.GetAccountsByUserIdAsync(userId);
    }

    public async Task<Pagination<Account>> GetAccountsByUserIdAsync(int userId, PaginationFilter paginationFilter)
    {
        return await accountRepository.GetAccountsByUserIdAsync(userId, paginationFilter);
    }

    public async Task<Account> GetAccountByIdAndUserIdAsync(int id, int userId)
    {
        return await accountRepository.GetAccountByIdAndUserIdAsync(id, userId) ?? throw new AccountNotFoundException();
    }

    public async Task<Account> CreateAccountByUserIdAsync(int userId, Account account)
    {
        await IsAccountExistsByNameAndUserIdAsync(account.Name, userId);

        account.UserId = userId;

        return await accountRepository.CreateAccountAsync(account);
    }

    public async Task<Account> UpdateAccountByIdAndUserIdAsync(int id, int userId, Account account)
    {
        Account existingAccount = await GetAccountByIdAndUserIdAsync(id, userId);

        if (!existingAccount.Name.Equals(account.Name, StringComparison.OrdinalIgnoreCase))
        {
            await IsAccountExistsByNameAndUserIdAsync(account.Name, userId);
        }

        existingAccount.Name = account.Name;
        existingAccount.StartingBalance = account.StartingBalance;

        return await accountRepository.UpdateAccountAsync(existingAccount);
    }

    public async Task DeleteAccountByIdAndUserIdAsync(int id, int userId)
    {
        Account existingAccount = await GetAccountByIdAndUserIdAsync(id, userId);

        await accountRepository.DeleteAccountAsync(existingAccount);
    }

    public async Task IsAccountExistsByNameAndUserIdAsync(string name, int userId)
    {
        if (await accountRepository.IsAccountExistsByNameAndUserIdAsync(name, userId))
        {
            throw new AccountAlreadyExistsException();
        }
    }
}
