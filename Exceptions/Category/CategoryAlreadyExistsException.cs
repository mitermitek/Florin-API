namespace Florin_API.Exceptions.Category;

public class CategoryAlreadyExistsException : Exception
{
    public CategoryAlreadyExistsException() : base("Category already exists.") { }
    public CategoryAlreadyExistsException(string message) : base(message) { }
    public CategoryAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
}
