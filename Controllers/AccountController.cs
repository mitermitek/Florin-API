using Florin_API.Common;
using Florin_API.DTOs.Account;
using Florin_API.DTOs.Transaction;
using Florin_API.Mappers;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController(IUserContextService userContextService, IAccountService accountService, ITransactionService transactionService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] PaginationFilter? paginationFilter)
        {
            var userId = userContextService.GetCurrentUserId();

            if (paginationFilter is not null)
            {
                var pagedAccounts = await accountService.GetAccountsByUserIdAsync(userId, paginationFilter);
                var pagedAccountsDto = AccountMapper.ToDto(pagedAccounts);

                return Ok(pagedAccountsDto);
            }

            var accounts = await accountService.GetAccountsByUserIdAsync(userId);
            var accountsDto = AccountMapper.ToDtos(accounts);

            return Ok(accountsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var userId = userContextService.GetCurrentUserId();
            var account = await accountService.GetAccountByIdAndUserIdAsync(id, userId);
            var accountDto = AccountMapper.ToDto(account);

            return Ok(accountDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
        {
            var userId = userContextService.GetCurrentUserId();
            var accountToCreate = createAccountDto.ToEntity();
            var createdAccount = await accountService.CreateAccountByUserIdAsync(userId, accountToCreate);
            var accountDto = AccountMapper.ToDto(createdAccount);

            return CreatedAtAction(
                nameof(GetAccountById),
                new { id = accountDto.Id },
                accountDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] UpdateAccountDto updateAccountDto)
        {
            var userId = userContextService.GetCurrentUserId();
            var accountToUpdate = updateAccountDto.ToEntity();
            var updatedAccount = await accountService.UpdateAccountByIdAndUserIdAsync(id, userId, accountToUpdate);
            var accountDto = AccountMapper.ToDto(updatedAccount);

            return Ok(accountDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var userId = userContextService.GetCurrentUserId();
            await accountService.DeleteAccountByIdAndUserIdAsync(id, userId);

            return NoContent();
        }

        [HttpGet("{accountId}/transactions")]
        public async Task<IActionResult> GetAccountTransactions(int accountId, [FromQuery] PaginationFilter? paginationFilter)
        {
            var userId = userContextService.GetCurrentUserId();

            if (paginationFilter is not null)
            {
                var pagedTransactions = await transactionService.GetTransactionsByAccountIdAndUserIdAsync(accountId, userId, paginationFilter);
                var pagedTransactionsDto = TransactionMapper.ToDto(pagedTransactions);

                return Ok(pagedTransactionsDto);
            }

            var transactions = await transactionService.GetTransactionsByAccountIdAndUserIdAsync(accountId, userId);
            var transactionsDto = TransactionMapper.ToDtos(transactions);

            return Ok(transactionsDto);
        }

        [HttpGet("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> GetAccountTransactionById(int accountId, int transactionId)
        {
            var userId = userContextService.GetCurrentUserId();
            var transaction = await transactionService.GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId);
            var transactionDto = TransactionMapper.ToDto(transaction);

            return Ok(transactionDto);
        }

        [HttpPost("{accountId}/transactions")]
        public async Task<IActionResult> CreateAccountTransaction(int accountId, [FromBody] CreateTransactionDto createTransactionDto)
        {
            var userId = userContextService.GetCurrentUserId();
            var transactionToCreate = createTransactionDto.ToEntity();
            var createdTransaction = await transactionService.CreateTransactionByAccountIdAndUserIdAsync(accountId, userId, transactionToCreate);
            var transactionDto = TransactionMapper.ToDto(createdTransaction);

            return CreatedAtAction(
                nameof(GetAccountTransactionById),
                new { accountId, transactionId = transactionDto.Id },
                transactionDto
            );
        }

        [HttpPut("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> UpdateAccountTransaction(int accountId, int transactionId, [FromBody] UpdateTransactionDto updateTransactionDto)
        {
            var userId = userContextService.GetCurrentUserId();
            var transactionToUpdate = updateTransactionDto.ToEntity();
            var updatedTransaction = await transactionService.UpdateTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId, transactionToUpdate);
            var transactionDto = TransactionMapper.ToDto(updatedTransaction);

            return Ok(transactionDto);
        }

        [HttpDelete("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> DeleteAccountTransaction(int accountId, int transactionId)
        {
            var userId = userContextService.GetCurrentUserId();
            await transactionService.DeleteTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId);

            return NoContent();
        }
    }
}
