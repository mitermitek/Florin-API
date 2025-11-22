namespace Florin_API.DTOs.Account;

public record AccountDTO
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal? StartingBalance { get; init; }
}
