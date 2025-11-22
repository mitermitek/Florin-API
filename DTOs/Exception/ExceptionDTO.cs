namespace Florin_API.DTOs.Exception;

public record ExceptionDTO
{
    public required string Message { get; init; }
    public required string Type { get; init; }
}
