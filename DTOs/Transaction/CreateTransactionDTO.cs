using System.ComponentModel.DataAnnotations;
using Florin_API.Domain.Enums;

namespace Florin_API.DTOs.Transaction;

public record CreateTransactionDTO
{
    [Required]
    public int CategoryId { get; init; }

    [Required]
    [EnumDataType(typeof(TransactionType))]
    public TransactionType Type { get; init; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; init; }

    [Required]
    public DateTime Date { get; init; }

    [Required]
    [MaxLength(255)]
    public required string Title { get; init; }

    [MaxLength(255)]
    public string? Description { get; init; }
}
