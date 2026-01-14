using System.ComponentModel.DataAnnotations;
using Florin_API.Enums;

namespace Florin_API.DTOs.Transaction;

public record CreateTransactionDto
{
    [Required]
    public required int CategoryId { get; init; }

    [Required]
    [EnumDataType(typeof(TransactionType))]
    public required TransactionType Type { get; init; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public required decimal Amount { get; init; }

    [Required]
    public required DateTime Date { get; init; }

    [Required]
    [MaxLength(255)]
    public required string Title { get; init; }

    [MaxLength(255)]
    public string? Description { get; init; }
}
