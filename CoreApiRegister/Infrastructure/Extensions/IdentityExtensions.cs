
using System.Linq;
using System.Security.Claims;


namespace CoreApiRegister.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user )
            => user
            .Claims
            .FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)
            ?.Value;
    }
}
