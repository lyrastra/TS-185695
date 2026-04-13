using System;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Utils.ServerUrl
{
    public interface IServerUriService : IDI
    {
        string GetAuthServerUrl(Uri currentUri);

        string GetBaseServerDomain(Uri currentUrl);

        string GetIFrameUrl(Uri currentUrl);

        string GetLogoutUrl(Uri currentUrl);

        string GetBuroPromoUrl(Uri currentUrl);

        string GetPromoBaseUrl(Uri currentUrl);

        string GetBaseUrl(Uri currentUrl);

        string GetAuthWithBackUrl(Uri requestUri);

        bool IsDevelpmentDomain(string host);

        bool IsProductionDomain(string host);

        string GetSsoUrl(Uri currentUri);
    }
}