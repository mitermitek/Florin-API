using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<ICollection<Account>> GetAccountsByUserIdAsync(int userId);
    Task<Pagination<Account>> GetAccountsByUserIdAsync(int userId, PaginationFilter paginationFilter);
    Task<Account?> GetAccountByIdAndUserIdAsync(int id, int userId);
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task DeleteAccountAsync(Account account);
    Task<bool> IsAccountExistsByNameAndUserIdAsync(string name, int userId);
}
