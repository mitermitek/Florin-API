namespace Florin_API.Exceptions;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException(string message) : base(message)
    {
    }
}
