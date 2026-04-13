using System;
using Moedelo.CommonV2.Auth.OAuth2.Abstractions;
using Moedelo.CommonV2.Auth.OAuth2.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;

namespace Moedelo.CommonV2.Auth.OAuth2.Services
{
    [InjectPerWebRequest(typeof(ITokenReaderService))]
    internal sealed class TokenReaderService : ITokenReaderService
    {
        private readonly Lazy<string> lazyToken;

        public TokenReaderService(IHttpEnviroment httpEnvironment)
        {
            lazyToken = new Lazy<string>(()
                => httpEnvironment.CurrentContext?.Request.GetOAuthToken());
        }

        public string GetOAuthToken()
        {
            return lazyToken.Value;
        }
    }
}