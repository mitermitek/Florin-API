using System.Security.Claims;
using Florin_API.Interfaces;

namespace Florin_API.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public int UserId
        {
            get
            {
                var user = httpContextAccessor.HttpContext?.User;

                if (user?.Identity?.IsAuthenticated != true)
                    throw new UnauthorizedAccessException("User is not authenticated");

                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? user.FindFirst("sub")?.Value
                    ?? user.FindFirst("userId")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                    throw new UnauthorizedAccessException("User ID not found in token claims");

                return userId;
            }
        }

        public bool IsAuthenticated => httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;
    }
}
