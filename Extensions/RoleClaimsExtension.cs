using System.Security.Claims;
using WebApiJwt.Models;

namespace WebApiJwt.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Email)
        };
        claims.AddRange(user.Roles.Select(
            role => new Claim(ClaimTypes.Role, role.Name)));
        return claims;
    }
}
