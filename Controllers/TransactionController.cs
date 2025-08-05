using AutoMapper;
using Florin_API.DTOs;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers;

[Route("Transactions")]
[Authorize]
public class TransactionController(IMapper mapper, ITransactionService transactionService, ICurrentUserService currentUserService) : ApiBaseController
{
    /// <summary>
    /// Gets all transactions for the authenticated user
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Number of items per page (default: 10, max: 100)</param>
    /// <returns>Paginated list of user's transactions</returns>
    /// <response code="200">Transactions retrieved successfully</response>
    /// <response code="400">Invalid pagination parameters</response>
    /// <response code="401">User not authenticated</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDTO<TransactionDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTransactions([FromQuery] PaginationDTO pagination)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pagedTransactions = await transactionService.GetUserTransactionsAsync(currentUserService.UserId, pagination.Page, pagination.PageSize);

        var result = new PagedResultDTO<TransactionDTO>
        {
            Items = mapper.Map<IEnumerable<TransactionDTO>>(pagedTransactions.Items),
            TotalCount = pagedTransactions.TotalCount,
            Page = pagedTransactions.Page,
            PageSize = pagedTransactions.PageSize
        };

        return Ok(result);
    }

    /// <summary>
    /// Gets a specific transaction by ID for the authenticated user
    /// </summary>
    /// <param name="id">Transaction ID</param>
    /// <returns>The requested transaction</returns>
    /// <response code="200">Transaction retrieved successfully</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Transaction not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TransactionDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransaction(int id)
    {
        var transaction = await transactionService.GetTransactionByIdAndUserIdAsync(id, currentUserService.UserId);
        var transactionDTO = mapper.Map<TransactionDTO>(transaction);

        return Ok(transactionDTO);
    }

    /// <summary>
    /// Creates a new transaction for the authenticated user
    /// </summary>
    /// <param name="createTransactionDto">Transaction creation data</param>
    /// <returns>The created transaction</returns>
    /// <response code="201">Transaction created successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="401">User not authenticated</response>
    [HttpPost]
    [ProducesResponseType(typeof(TransactionDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO createTransactionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var transaction = mapper.Map<Transaction>(createTransactionDto);
        transaction.UserId = currentUserService.UserId;
        var createdTransaction = await transactionService.CreateTransactionAsync(transaction);
        var transactionDTO = mapper.Map<TransactionDTO>(createdTransaction);

        return CreatedAtAction(nameof(GetTransaction), new { id = transactionDTO.Id }, transactionDTO);
    }

    /// <summary>
    /// Updates an existing transaction for the authenticated user
    /// </summary>
    /// <param name="id">Transaction ID</param>
    /// <param name="updateTransactionDto">Transaction update data</param>
    /// <returns>The updated transaction</returns>
    /// <response code="200">Transaction updated successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Transaction not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TransactionDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDTO updateTransactionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingTransaction = await transactionService.GetTransactionByIdAndUserIdAsync(id, currentUserService.UserId);
        var transactionUpdate = mapper.Map<Transaction>(updateTransactionDto);
        var updatedTransaction = await transactionService.UpdateTransactionAsync(existingTransaction, transactionUpdate);
        var transactionDTO = mapper.Map<TransactionDTO>(updatedTransaction);

        return Ok(transactionDTO);
    }

    /// <summary>
    /// Deletes a transaction for the authenticated user
    /// </summary>
    /// <param name="id">Transaction ID</param>
    /// <returns>No content</returns>
    /// <response code="204">Transaction deleted successfully</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Transaction not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var transaction = await transactionService.GetTransactionByIdAndUserIdAsync(id, currentUserService.UserId);
        await transactionService.DeleteTransactionAsync(transaction);

        return NoContent();
    }
}
