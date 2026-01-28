using Florin_API.Common;
using Florin_API.Entities;
using Florin_API.Exceptions.Account;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;

namespace Florin_API.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    public async Task<ICollection<Account>> GetAccountsByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await accountRepository.GetAccountsByUserIdAsync(userId, cancellationToken);
    }

    public async Task<Pagination<Account>> GetAccountsByUserIdAsync(int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken)
    {
        return await accountRepository.GetAccountsByUserIdAsync(userId, paginationFilter, cancellationToken);
    }

    public async Task<Account> GetAccountByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken)
    {
        return await accountRepository.GetAccountByIdAndUserIdAsync(id, userId, cancellationToken) ?? throw new AccountNotFoundException();
    }

    public async Task<Account> CreateAccountByUserIdAsync(int userId, Account account, CancellationToken cancellationToken)
    {
        await IsAccountExistsByNameAndUserIdAsync(account.Name, userId, cancellationToken);

        account.UserId = userId;

        return await accountRepository.CreateAccountAsync(account, cancellationToken);
    }

    public async Task<Account> UpdateAccountByIdAndUserIdAsync(int id, int userId, Account account, CancellationToken cancellationToken)
    {
        Account existingAccount = await GetAccountByIdAndUserIdAsync(id, userId, cancellationToken);

        if (!existingAccount.Name.Equals(account.Name, StringComparison.OrdinalIgnoreCase))
        {
            await IsAccountExistsByNameAndUserIdAsync(account.Name, userId, cancellationToken);
        }

        existingAccount.Name = account.Name;
        existingAccount.StartingBalance = account.StartingBalance;

        return await accountRepository.UpdateAccountAsync(existingAccount, cancellationToken);
    }

    public async Task DeleteAccountByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken)
    {
        Account existingAccount = await GetAccountByIdAndUserIdAsync(id, userId, cancellationToken);

        await accountRepository.DeleteAccountAsync(existingAccount, cancellationToken);
    }

    public async Task IsAccountExistsByNameAndUserIdAsync(string name, int userId, CancellationToken cancellationToken)
    {
        if (await accountRepository.IsAccountExistsByNameAndUserIdAsync(name, userId, cancellationToken))
        {
            throw new AccountAlreadyExistsException();
        }
    }
}
