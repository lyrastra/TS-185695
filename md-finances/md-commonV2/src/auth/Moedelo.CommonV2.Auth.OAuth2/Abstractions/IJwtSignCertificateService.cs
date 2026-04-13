using System.Security.Cryptography;

namespace Moedelo.CommonV2.Auth.OAuth2.Abstractions
{
    public interface IJwtSignCertificateService
    {
        RSA GetPrivateKey();
    }
}