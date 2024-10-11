using System.Security.Claims;

namespace Dima.Core.Requests;

public abstract class Request
{
    public string UserId { get; private set; } = string.Empty;

    public void SetUserId(ClaimsPrincipal user)
        => UserId = user.Identity?.Name ?? string.Empty;
}
