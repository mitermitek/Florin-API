using Florin_API.Enums;

namespace Florin_API.DTOs.Responses;

public record TransactionResponse
{
    public int Id { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required CategoryResponse Category { get; init; }
}
