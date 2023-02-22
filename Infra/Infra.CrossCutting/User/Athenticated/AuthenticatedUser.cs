using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Infra.CrossCutting.User.Athenticated;

public class AuthenticatedUser
{
    private readonly IHttpContextAccessor _accessor;

    public AuthenticatedUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    public Guid? GetUserId()
    {
        var userId = _accessor.HttpContext?.User.Claims.First().Value;
        if(userId != null)
            return Guid.Parse(userId);
        
        return null;
    }
}