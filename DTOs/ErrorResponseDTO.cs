namespace Florin_API.DTOs;

/// <summary>
/// Represents an error response
/// </summary>
public class ErrorResponseDTO
{
    /// <summary>
    /// Error message
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Error type
    /// </summary>
    public required string Type { get; set; }
}