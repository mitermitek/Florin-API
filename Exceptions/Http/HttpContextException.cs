namespace Florin_API.Exceptions.Http;

public class HttpContextException : Exception
{
    public HttpContextException() : base("HTTP context error.") { }
    public HttpContextException(string message) : base(message) { }
    public HttpContextException(string message, Exception inner) : base(message, inner) { }
}
