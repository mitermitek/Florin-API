namespace Florin_API.DTOs;

/// <summary>
/// Represents a validation error response
/// </summary>
public class ValidationErrorResponseDTO
{
    /// <summary>
    /// RFC reference for the error type
    /// </summary>
    public string Type { get; set; } = "https://tools.ietf.org/html/rfc9110#section-15.5.1";

    /// <summary>
    /// Error title
    /// </summary>
    public string Title { get; set; } = "One or more validation errors occurred.";

    /// <summary>
    /// HTTP status code
    /// </summary>
    public int Status { get; set; } = 400;

    /// <summary>
    /// Dictionary of field validation errors
    /// </summary>
    public Dictionary<string, string[]> Errors { get; set; } = new();

    /// <summary>
    /// Request trace identifier
    /// </summary>
    public string? TraceId { get; set; }
}
