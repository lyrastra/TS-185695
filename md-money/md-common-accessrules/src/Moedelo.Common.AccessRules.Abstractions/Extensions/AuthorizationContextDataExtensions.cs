using Moedelo.Common.AccessRules.Abstractions.Models;

namespace Moedelo.Common.AccessRules.Abstractions.Extensions;

public static class AuthorizationContextDataExtensions
{
    public static bool HasUserAnyAccessToFirm(this IAuthorizationContextData authData)
    {
        return authData.RoleId > 0;
    }
}
