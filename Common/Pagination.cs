namespace Florin_API.Common;

public class Pagination<T>
{
    public ICollection<T> Items { get; set; } = [];
    public int Total { get; set; }
}
