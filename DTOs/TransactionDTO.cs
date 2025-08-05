using System.ComponentModel.DataAnnotations;
using Florin_API.Enums;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for transactions
/// </summary>
public class TransactionDTO
{
    /// <summary>
    /// Unique identifier for the transaction
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Type of the transaction (e.g., income, expense)
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
    [Required]
    public required decimal Amount { get; set; }

    /// <summary>
    /// Description of the transaction
    /// </summary>
    [StringLength(255)]
    public string? Description { get; set; }

    /// <summary>
    /// Category associated with the transaction
    /// </summary>
    public CategoryDTO? Category { get; set; }

    /// <summary>
    /// User ID associated with the transaction
    /// </summary>
    public int UserId { get; set; }
}
