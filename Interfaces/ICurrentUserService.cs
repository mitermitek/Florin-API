namespace Florin_API.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        bool IsAuthenticated { get; }
    }
}
