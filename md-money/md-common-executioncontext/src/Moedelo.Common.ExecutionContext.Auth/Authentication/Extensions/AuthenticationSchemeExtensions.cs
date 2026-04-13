using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Extensions
{
    internal static class AuthenticationSchemeExtensions
    {
        public static AuthenticationTicket GetTicket(this AuthenticationScheme scheme)
        {
            var claimsIdentity = new ClaimsIdentity(scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(claimsPrincipal, scheme.Name);
        }
    }
}
