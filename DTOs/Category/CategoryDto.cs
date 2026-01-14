namespace Florin_API.DTOs.Category;

public record CategoryDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
}
