using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Moedelo.AccountV2.Client.ExternalApi;
using Moedelo.AccountV2.Dto.ExternalApi;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.CommonV2.Auth.OAuth2;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.CommonV2.Auth.External;

[InjectAsSingleton(typeof(ExternalAuthenticationService))]
public class ExternalAuthenticationService : IAuthenticationService
{
    private const string Tag = nameof(ExternalAuthenticationService);
    
    private readonly ILogger logger;
    private readonly IExternalApiClient externalApiClient;
    private readonly OAuth2AuthenticationService oAuth2AuthenticationService;

    public ExternalAuthenticationService(IExternalApiClient externalApiClient,
        OAuth2AuthenticationService oAuth2AuthenticationService,
        ILogger logger)
    {
        this.externalApiClient = externalApiClient;
        this.oAuth2AuthenticationService = oAuth2AuthenticationService;
        this.logger = logger;
    }

    public async Task<AuthenticationInfo> AuthenticateAsync(CancellationToken cancellationToken)
    {
        try
        {
            var token = GetExternalAuthToken();

            if (token == null)
            {
                return await oAuth2AuthenticationService.AuthenticateAsync(cancellationToken).ConfigureAwait(false);
            }

            var authorization = await externalApiClient.AuthorizeByApiKeyTokenAsync(token, cancellationToken)
                .ConfigureAwait(false);

            if (authorization.Status != ApiKeyAuthorizationStatus.Authorized)
            {
                return null;
            }

            var keyInfo = authorization.ApiKey ?? throw new ArgumentNullException(nameof(authorization.ApiKey));

            return new AuthenticationInfo(keyInfo.ApiUserId, keyInfo.FirmId);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // не логируем - операция отменена
            throw;
        }
        catch (Exception exception)
        {
            logger.Error(Tag, "Во время авторизации произошло исключение", exception);

            return null;
        }
    }

    private static string GetExternalAuthToken()
    {
        var token = HttpContext.Current.Request.Headers.Get(AuthorizationTokenHeaderParam.MdApiKeyHeaderName);
        return token;
    }
}