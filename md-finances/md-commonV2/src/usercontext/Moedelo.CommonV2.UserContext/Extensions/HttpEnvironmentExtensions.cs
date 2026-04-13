#nullable enable
using System.Linq;
using System.Security.Claims;
using System.Web;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;

namespace Moedelo.CommonV2.UserContext.Extensions;

internal static class HttpEnvironmentExtensions
{
    internal static int? GetUserClaimIntValue(this IHttpEnviroment http, string claimName)
    {
        return GetIntValue(http.CurrentContext?.GetUserClaim(claimName));
    }

    private static string? GetUserClaim(this HttpContext httpContext, string claimName)
    {
        var claim = (httpContext.User as ClaimsPrincipal)?.Claims
            .FirstOrDefault(c => c.Type == claimName);

        return claim?.Value;
    }
    
    private static int? GetIntValue(string? val)
    {
        if (val == null)
        {
            return null;
        }

        return int.TryParse(val, out var parseVal)
            ? parseVal
            : null;
    }

}
