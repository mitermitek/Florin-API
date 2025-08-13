namespace Florin_API.Exceptions;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException() : base("Email already exists") { }

    public EmailAlreadyExistsException(string message) : base(message) { }

    public EmailAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
}
