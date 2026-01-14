namespace Florin_API.DTOs.Account;

public record AccountDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal? StartingBalance { get; init; }
}
