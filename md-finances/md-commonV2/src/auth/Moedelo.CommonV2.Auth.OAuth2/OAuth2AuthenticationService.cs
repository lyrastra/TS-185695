using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.CommonV2.Auth.OAuth2.Abstractions;
using Moedelo.CommonV2.Auth.OAuth2.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.OauthToken.Client;
using MdClaims = Moedelo.CommonV2.Auth.OAuth2.Models.MdClaims;

namespace Moedelo.CommonV2.Auth.OAuth2
{
    [InjectAsSingleton]
    public sealed class OAuth2AuthenticationService : IAuthenticationService
    {
        private const string Tag = nameof(OAuth2AuthenticationService);

        private readonly IOAuthUserToFirmAccessChecker userToFirmAccessChecker;
        private readonly IOauthTokenClient oauthTokenClient;
        private readonly ITokenSecurity tokenSecurity;
        private readonly IDIResolver dIResolver;
        private readonly ILogger logger;

        public OAuth2AuthenticationService(
            IOAuthUserToFirmAccessChecker userToFirmAccessChecker,
            IOauthTokenClient oauthTokenClient,
            ITokenSecurity tokenSecurity, 
            IDIResolver dIResolver,
            ILogger logger)
        {
            this.userToFirmAccessChecker = userToFirmAccessChecker;
            this.oauthTokenClient = oauthTokenClient;
            this.tokenSecurity = tokenSecurity;
            this.dIResolver = dIResolver;
            this.logger = logger;
        }

        public async Task<AuthenticationInfo> AuthenticateAsync(CancellationToken cancellationToken)
        {
            try
            {
                var httpRequest = dIResolver.GetInstance<IHttpEnviroment>().CurrentContext?.Request;
                var token = httpRequest.GetOAuthToken();

                if (token == null)
                {
                    return null;
                }

                var claims = tokenSecurity.GetClaims(token);

                return await GetAuthenticationInfoFromClaimsAsync(httpRequest, claims, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch(Exception exception) when (cancellationToken.IsCancellationRequested == false)
            {
                logger.Error(Tag, "При проверке авторизации произошла ошибка", exception: exception);

                return null;
            }
        }

        private async Task<AuthenticationInfo> GetAuthenticationInfoFromClaimsAsync(
            HttpRequest httpRequest,
            MdClaims claims,
            CancellationToken cancellationToken)
        {
            var isTokenValid = await IsTokenGuidValidAsync(claims).ConfigureAwait(false); 

            if(!isTokenValid)
            {
                logger.Debug(Tag, "Authentication token has invalid guid", extraData: claims);

                return null;
            }
            
            if (claims.ExpirationDate < DateTime.Now)
            {
                await ForgetTokenGuidAsync(claims).ConfigureAwait(false);
                logger.Debug(Tag, "Authentication token is expired", extraData: claims);

                return null;
            }

            var userId = claims.UserId;
            var firmId = httpRequest.GetFirmId(claims.FirmId);

            var isFirmAcceptedForUser = await CheckIfUserHasAccessToFirmAsync(
                    httpRequest, firmId, userId, cancellationToken)
                .ConfigureAwait(false);

            if (!isFirmAcceptedForUser)
            {
                // токен "хороший", но этот пользователь на текущий момент не имеет доступа к этой фирме
                logger.Info(Tag, "Авторизация не пройдена: токен существует, но пользователь не имеет доступа к фирме",
                    extraData: new { claims, firmId});

                return null;
            }
            
            return new AuthenticationInfo(userId, firmId, claims.ClientId);
        }

        private Task<bool> CheckIfUserHasAccessToFirmAsync(HttpRequest httpRequest, int firmId, int userId, CancellationToken cancellationToken)
        {
            const string authorizationPageUrl = "/Authorize";

            if (httpRequest.Url.LocalPath.Equals(authorizationPageUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                // хз почему так исторически
                return Task.FromResult(true);
            }

            return userToFirmAccessChecker.DoesUserHaveOAuthAccessToFirmAsync(userId, firmId, cancellationToken);
        }

        private Task<bool> ForgetTokenGuidAsync(MdClaims claims)
        {
            return oauthTokenClient
                .DeleteTokenGuidAsync(claims.Guid, claims.UserId, claims.ClientId, claims.Temporary);
        }

        private async Task<bool> IsTokenGuidValidAsync(MdClaims claims)
        {
            if (claims.Temporary)
            {
                var guid = await oauthTokenClient
                    .GetTokenTemporaryGuidAsync(claims.UserId, claims.ClientId)
                    .ConfigureAwait(false);

                return claims.Guid.Equals(guid);
            }

            return await oauthTokenClient
                .IsPublicTokenGuidExistAsync(claims.UserId, claims.ClientId, claims.Guid)
                .ConfigureAwait(false);
        }
    }
}
