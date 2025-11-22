namespace Florin_API.Exceptions.Category;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException() : base("Category not found.") { }
    public CategoryNotFoundException(string message) : base(message) { }
    public CategoryNotFoundException(string message, Exception inner) : base(message, inner) { }
}
