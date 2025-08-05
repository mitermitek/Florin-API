namespace Florin_API.Exceptions;

public class DuplicateCategoryNameException : Exception
{
    public DuplicateCategoryNameException() : base("A category with the same name already exists") { }

    public DuplicateCategoryNameException(string message) : base(message) { }

    public DuplicateCategoryNameException(string message, Exception innerException) : base(message, innerException) { }
}
