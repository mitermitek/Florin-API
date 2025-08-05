namespace Florin_API.Exceptions;

public class BadCredentialsException : Exception
{
    public BadCredentialsException() : base("Bad credentials") { }

    public BadCredentialsException(string message) : base(message) { }

    public BadCredentialsException(string message, Exception innerException) : base(message, innerException) { }
}
