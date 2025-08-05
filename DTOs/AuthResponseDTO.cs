namespace Florin_API.DTOs;

/// <summary>
/// Represents authentication response with tokens
/// </summary>
public class AuthResponseDTO
{
    /// <summary>
    /// User information
    /// </summary>
    public required UserDTO User { get; set; }

    /// <summary>
    /// JWT access token
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// JWT refresh token
    /// </summary>
    public required string RefreshToken { get; set; }
}
