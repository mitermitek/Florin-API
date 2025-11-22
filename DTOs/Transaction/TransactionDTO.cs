using Florin_API.Domain.Enums;
using Florin_API.DTOs.Category;

namespace Florin_API.DTOs.Transaction;

public record TransactionDTO
{
    public int Id { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required CategoryDTO Category { get; init; }
}
