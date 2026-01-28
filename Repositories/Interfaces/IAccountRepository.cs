using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<ICollection<Account>> GetAccountsByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<Pagination<Account>> GetAccountsByUserIdAsync(int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken);
    Task<Account?> GetAccountByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task<Account> CreateAccountAsync(Account account, CancellationToken cancellationToken);
    Task<Account> UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task DeleteAccountAsync(Account account, CancellationToken cancellationToken);
    Task<bool> IsAccountExistsByNameAndUserIdAsync(string name, int userId, CancellationToken cancellationToken);
}
