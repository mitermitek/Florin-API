using Florin_API.DTOs.Account;
using Florin_API.Mappers;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController(IUserContextService userContextService, IAccountService accountService) : ControllerBase
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
    }
}
