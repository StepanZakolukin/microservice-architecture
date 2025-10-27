using System.Security.Claims;
using UserService.Api.Services.Interfaces;

namespace UserService.Api.Services;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid UserId =>
        Guid.TryParse(_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
            ? id
            : Guid.Empty;
}