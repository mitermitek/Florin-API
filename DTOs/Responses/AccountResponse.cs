namespace Florin_API.DTOs.Responses;

public record AccountResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal? StartingBalance { get; init; }
}
