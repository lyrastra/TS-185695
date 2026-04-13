namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class AuthorizationTokenHeaderParam
{
    public const string AuthorizationHeader = "Authorization";
    public const string AuthorizationScheme = "Bearer";
    public const string PrivateJwtHeader = "IsPrivate";
    public const string MdApiKeyHeaderName = "md-api-key";
}