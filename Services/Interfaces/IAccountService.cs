using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface IAccountService
{
    Task<ICollection<Account>> GetAccountsByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<Pagination<Account>> GetAccountsByUserIdAsync(int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken);
    Task<Account> GetAccountByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task<Account> CreateAccountByUserIdAsync(int userId, Account account, CancellationToken cancellationToken);
    Task<Account> UpdateAccountByIdAndUserIdAsync(int id, int userId, Account account, CancellationToken cancellationToken);
    Task DeleteAccountByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task IsAccountExistsByNameAndUserIdAsync(string name, int userId, CancellationToken cancellationToken);
}
