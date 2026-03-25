using Florin_API.Common;
using Florin_API.DTOs.Requests;
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
        public async Task<IActionResult> GetAccounts([FromQuery] PaginationFilter? paginationFilter, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();

            if (paginationFilter is not null)
            {
                var pagedAccounts = await accountService.GetAccountsByUserIdAsync(userId, paginationFilter, cancellationToken);
                var pagedAccountsResponse = AccountMapper.ToResponse(pagedAccounts);

                return Ok(pagedAccountsResponse);
            }

            var accounts = await accountService.GetAccountsByUserIdAsync(userId, cancellationToken);
            var accountsResponse = AccountMapper.ToResponses(accounts);

            return Ok(accountsResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var account = await accountService.GetAccountByIdAndUserIdAsync(id, userId, cancellationToken);
            var accountResponse = AccountMapper.ToResponse(account);

            return Ok(accountResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest accountRequest, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var accountToCreate = accountRequest.ToEntity();
            var createdAccount = await accountService.CreateAccountByUserIdAsync(userId, accountToCreate, cancellationToken);
            var accountResponse = AccountMapper.ToResponse(createdAccount);

            return CreatedAtAction(
                nameof(GetAccountById),
                new { id = accountResponse.Id },
                accountResponse
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountRequest accountRequest, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var accountToUpdate = accountRequest.ToEntity();
            var updatedAccount = await accountService.UpdateAccountByIdAndUserIdAsync(id, userId, accountToUpdate, cancellationToken);
            var accountResponse = AccountMapper.ToResponse(updatedAccount);

            return Ok(accountResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            await accountService.DeleteAccountByIdAndUserIdAsync(id, userId, cancellationToken);

            return NoContent();
        }

        [HttpGet("{accountId}/transactions")]
        public async Task<IActionResult> GetAccountTransactions(int accountId, [FromQuery] PaginationFilter? paginationFilter, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();

            if (paginationFilter is not null)
            {
                var pagedTransactions = await transactionService.GetTransactionsByAccountIdAndUserIdAsync(accountId, userId, paginationFilter, cancellationToken);
                var pagedTransactionsResponse = TransactionMapper.ToResponse(pagedTransactions);

                return Ok(pagedTransactionsResponse);
            }

            var transactions = await transactionService.GetTransactionsByAccountIdAndUserIdAsync(accountId, userId, cancellationToken);
            var transactionsResponse = TransactionMapper.ToResponses(transactions);

            return Ok(transactionsResponse);
        }

        [HttpGet("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> GetAccountTransactionById(int accountId, int transactionId, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var transaction = await transactionService.GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId, cancellationToken);
            var transactionResponse = TransactionMapper.ToResponse(transaction);

            return Ok(transactionResponse);
        }

        [HttpPost("{accountId}/transactions")]
        public async Task<IActionResult> CreateAccountTransaction(int accountId, [FromBody] TransactionRequest transactionRequest, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var transactionToCreate = transactionRequest.ToEntity();
            var createdTransaction = await transactionService.CreateTransactionByAccountIdAndUserIdAsync(accountId, userId, transactionToCreate, cancellationToken);
            var transactionResponse = TransactionMapper.ToResponse(createdTransaction);

            return CreatedAtAction(
                nameof(GetAccountTransactionById),
                new { accountId, transactionId = transactionResponse.Id },
                transactionResponse
            );
        }

        [HttpPut("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> UpdateAccountTransaction(int accountId, int transactionId, [FromBody] TransactionRequest transactionRequest, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var transactionToUpdate = transactionRequest.ToEntity();
            var updatedTransaction = await transactionService.UpdateTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId, transactionToUpdate, cancellationToken);
            var transactionResponse = TransactionMapper.ToResponse(updatedTransaction);

            return Ok(transactionResponse);
        }

        [HttpDelete("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> DeleteAccountTransaction(int accountId, int transactionId, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            await transactionService.DeleteTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId, cancellationToken);

            return NoContent();
        }
    }
}
