namespace Florin_API.Exceptions.Account;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException() : base("Account not found.") { }
    public AccountNotFoundException(string message) : base(message) { }
    public AccountNotFoundException(string message, Exception inner) : base(message, inner) { }
}
