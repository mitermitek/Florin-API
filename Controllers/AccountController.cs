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
        public async Task<IActionResult> GetAccounts()
        {
            var userId = userContextService.GetCurrentUserId();
            var accounts = await accountService.GetAccountsByUserIdAsync(userId);
            var accountsDto = AccountMapper.ToDTOs(accounts);

            return Ok(accountsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var userId = userContextService.GetCurrentUserId();
            var account = await accountService.GetAccountByIdAndUserIdAsync(id, userId);
            var accountDto = AccountMapper.ToDTO(account);

            return Ok(accountDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO createAccountDTO)
        {
            var userId = userContextService.GetCurrentUserId();
            var accountToCreate = createAccountDTO.ToEntity();
            var createdAccount = await accountService.CreateAccountByUserIdAsync(userId, accountToCreate);
            var accountDto = AccountMapper.ToDTO(createdAccount);

            return CreatedAtAction(
                nameof(GetAccountById),
                new { id = accountDto.Id },
                accountDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] UpdateAccountDTO updateAccountDTO)
        {
            var userId = userContextService.GetCurrentUserId();
            var accountToUpdate = updateAccountDTO.ToEntity();
            var updatedAccount = await accountService.UpdateAccountByIdAndUserIdAsync(id, userId, accountToUpdate);
            var accountDto = AccountMapper.ToDTO(updatedAccount);

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
        public async Task<IActionResult> GetAccountTransactions(int accountId)
        {
            var userId = userContextService.GetCurrentUserId();
            var transactions = await transactionService.GetTransactionsByAccountIdAndUserIdAsync(accountId, userId);
            var transactionsDto = TransactionMapper.ToDTOs(transactions);

            return Ok(transactionsDto);
        }

        [HttpGet("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> GetAccountTransactionById(int accountId, int transactionId)
        {
            var userId = userContextService.GetCurrentUserId();
            var transaction = await transactionService.GetTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId);
            var transactionDto = TransactionMapper.ToDTO(transaction);

            return Ok(transactionDto);
        }

        [HttpPost("{accountId}/transactions")]
        public async Task<IActionResult> CreateAccountTransaction(int accountId, [FromBody] CreateTransactionDTO createTransactionDTO)
        {
            var userId = userContextService.GetCurrentUserId();
            var transactionToCreate = createTransactionDTO.ToEntity();
            var createdTransaction = await transactionService.CreateTransactionByAccountIdAndUserIdAsync(accountId, userId, transactionToCreate);
            var transactionDto = TransactionMapper.ToDTO(createdTransaction);

            return CreatedAtAction(
                nameof(GetAccountTransactionById),
                new { accountId, transactionId = transactionDto.Id },
                transactionDto
            );
        }

        [HttpPut("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> UpdateAccountTransaction(int accountId, int transactionId, [FromBody] UpdateTransactionDTO updateTransactionDTO)
        {
            var userId = userContextService.GetCurrentUserId();
            var transactionToUpdate = updateTransactionDTO.ToEntity();
            var updatedTransaction = await transactionService.UpdateTransactionByIdAndAccountIdAndUserIdAsync(transactionId, accountId, userId, transactionToUpdate);
            var transactionDto = TransactionMapper.ToDTO(updatedTransaction);

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
