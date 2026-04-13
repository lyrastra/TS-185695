namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

public static class AuthorizationContextExtensions
{
    public static bool IsAuthenticated(this IAuthorizationContext context)
        => context.UserId > 0 || context.FirmId > 0;

    public static bool HasOnlyFirmIdDefined(this IAuthorizationContext context)
        => context.FirmId > 0 && context.UserId is 0 or -1;

    public static bool HasOnlyUserIdDefined(this IAuthorizationContext context)
        => context.UserId > 0 && context.FirmId is 0 or -1;
}