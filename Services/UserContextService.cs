using System.Security.Claims;
using Florin_API.Exceptions.Http;
using Florin_API.Services.Interfaces;

namespace Florin_API.Services;

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    public int GetCurrentUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null || !int.TryParse(userIdClaim, out var userId))
        {
            throw new HttpContextException("Unable to retrieve current user ID from HTTP context.");
        }

        return userId;
    }
}
