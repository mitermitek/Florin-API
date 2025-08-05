using System.ComponentModel.DataAnnotations;
using Florin_API.Enums;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for creating a new transaction
/// </summary>
public class CreateTransactionDTO
{
    /// <summary>
    /// Type of the transaction (Income or Expense)
    /// </summary>
    [Required]
    public required TransactionType Type { get; set; }

    /// <summary>
    /// Date of the transaction
    /// </summary>
    [Required]
    public required DateTime Date { get; set; }

    /// <summary>
    /// Amount of the transaction
    /// </summary>
    /// <remarks>
    /// Must be greater than 0
    /// </remarks>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public required decimal Amount { get; set; }

    /// <summary>
    /// Description of the transaction
    /// </summary>
    [StringLength(255)]
    public string? Description { get; set; }

    /// <summary>
    /// Category ID for the transaction
    /// </summary>
    [Required]
    public required int CategoryId { get; set; }
}
