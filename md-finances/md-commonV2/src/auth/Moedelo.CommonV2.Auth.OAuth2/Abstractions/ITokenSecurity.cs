using Moedelo.CommonV2.Auth.OAuth2.Models;

namespace Moedelo.CommonV2.Auth.OAuth2.Abstractions
{
    public interface ITokenSecurity
    {
        string GetToken(MdClaims data);

        MdClaims GetClaims(string token);
    }
}