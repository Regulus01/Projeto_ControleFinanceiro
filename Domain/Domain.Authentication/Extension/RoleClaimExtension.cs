using System.Security.Claims;
using Domain.Authentication.Entities;

namespace Domain.Authentication.Extension;

public static class RoleClaimExtension
{
    public static IEnumerable<Claim> GetClaims(this Usuario user)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.Name)
        };
        return result;
    }
}