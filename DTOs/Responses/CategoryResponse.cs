namespace Florin_API.DTOs.Responses;

public record CategoryResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
}
