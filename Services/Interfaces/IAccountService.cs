using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface IAccountService
{
    Task<ICollection<Account>> GetAccountsByUserIdAsync(int userId);
    Task<Account> GetAccountByIdAndUserIdAsync(int id, int userId);
    Task<Account> CreateAccountByUserIdAsync(int userId, Account account);
    Task<Account> UpdateAccountByIdAndUserIdAsync(int id, int userId, Account account);
    Task DeleteAccountByIdAndUserIdAsync(int id, int userId);
    Task IsAccountExistsByNameAndUserIdAsync(string name, int userId);
}
