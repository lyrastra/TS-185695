using System;
using Jose;
using Moedelo.CommonV2.Auth.OAuth2.Abstractions;
using Moedelo.CommonV2.Auth.OAuth2.Models;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.CommonV2.Auth.OAuth2.Services
{
    [InjectAsSingleton(typeof(ITokenSecurity))]
    internal sealed class TokenSecurity : ITokenSecurity
    {
        private const string JwtAlgHeaderName = "alg";
        private const JwsAlgorithm JwtSignAlgorithm = JwsAlgorithm.RS256;
        
        private readonly IJwtSignCertificateService jwtSignCertificateService;
        private readonly SettingValue useSignSetting;

        static TokenSecurity()
        {
            JWT.DefaultSettings.RegisterMapper(new DateTimeMapper());
        }

        public TokenSecurity(
            IJwtSignCertificateService jwtSignCertificateService,
            ISettingRepository settingRepository)
        {
            useSignSetting = settingRepository.Get("CheckJwtSign");
            this.jwtSignCertificateService = jwtSignCertificateService;
        }

        private bool UseCryptography => useSignSetting.GetBoolValueOrDefault(false); 

        public string GetToken(MdClaims data)
        {
            if (UseCryptography)
            {
                var rsaPrivateKey = jwtSignCertificateService.GetPrivateKey();

                return JWT.Encode(data, rsaPrivateKey, JwtSignAlgorithm);
            }
            
            return JWT.Encode(data, null, JwsAlgorithm.none);
        }

        public MdClaims GetClaims(string token)
        {
            if (UseCryptography)
            {
                var rsaPrivateKey = jwtSignCertificateService.GetPrivateKey();
                var jwtHeaders = JWT.Headers(token);
                Enum.TryParse<JwsAlgorithm>(jwtHeaders[JwtAlgHeaderName].ToString(), out var signAlgorithm);
                if (signAlgorithm != JwtSignAlgorithm)
                {
                    throw new Exception($"Обнаружен токен с неподдерживаемым алгоритмом подписи: {signAlgorithm}. Поддерживается только {JwtSignAlgorithm}");
                }

                JWT.Decode<MdClaims>(token, rsaPrivateKey, JwtSignAlgorithm);
            }

            return JWT.Payload<MdClaims>(token);
        }

        private class DateTimeMapper : IJsonMapper
        {
            public string Serialize(object obj)
            {
                return obj.ToJsonString();
            }

            public T Parse<T>(string json)
            {
                return json.FromJsonString<T>();
            }
        }
    }
}