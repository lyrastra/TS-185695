using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Auth.Mobile
{
    public interface IIdentityServerClient : IDI
    {
        OAuthTicket SendAuthenticationRequest(string login, string password);
        OAuthTicket SendRefreshTokenRequest(string refreshToken);
    }
}