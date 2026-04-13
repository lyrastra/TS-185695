using System.Collections.Concurrent;
using System.Security.Cryptography;
using Moedelo.CommonV2.Auth.OAuth2.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.Auth.OAuth2.Services
{
    [InjectAsSingleton(typeof(IJwtSignCertificateService))]
    internal sealed class JwtSignCertificateService : IJwtSignCertificateService
    {
        private readonly SettingValue serialNumberSetting;
        private readonly ConcurrentDictionary<string, RSA> privateKeys = new ConcurrentDictionary<string, RSA>();

        public JwtSignCertificateService(ISettingRepository settingRepository)
        {
            serialNumberSetting = settingRepository.Get("IdX509SerialNumber").ThrowExceptionIfNull(true);
        }

        public RSA GetPrivateKey()
        {
            var x509Serial = serialNumberSetting.Value;

            return privateKeys.GetOrAdd(x509Serial, X509Loader.LoadPrivateKey);
        }
    }
}
