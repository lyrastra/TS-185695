using System;
using System.Web;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.Utils.ServerUrl
{
    [InjectAsSingleton]
    public class ServerUriService : IServerUriService
    {
        private readonly SettingValue environment;

        public ServerUriService(
            ISettingRepository settingRepository
            )
        {
            this.environment = settingRepository.Get("Environment");
        }

        public string GetAuthServerUrl(Uri currentUri)
        {
            var host = currentUri.Host;
            return IsLocalHost(host)
                ? $"http://{host}:5646"
                : $"https://{DomainService.GetOauthDomain(host)}";
 
        }

        public string GetBaseServerDomain(Uri currentUrl)
        {
            return currentUrl.Host.Contains("localhost")
                ? $"{currentUrl.Host}:4303"
                :DomainService.GetBaseDomain(currentUrl.Host);
        }

        /// <summary>
        /// iframe формы авторизации
        /// </summary>
        public string GetIFrameUrl(Uri currentUrl)
        {
            string authUrl = GetAuthServerUrl(currentUrl);
            return $"{authUrl}/Authorize?redirect_uri={authUrl}/Authorize/IFrameCallBack";
        }

        public string GetLogoutUrl(Uri currentUrl)
        {
            return $"{GetAuthServerUrl(currentUrl)}/Logout";
        }

        public string GetBuroPromoUrl(Uri currentUrl)
        {
            return currentUrl.Host == "localhost"
                ? $"http://localhost:4303"
                : IsProductionDomain(currentUrl.Host)
                    ? "https://buro.moedelo.org"
                    : $"https://buro-{DomainService.GetBaseDomain(currentUrl.Host)}";
        }

        public string GetPromoBaseUrl(Uri currentUrl)
        {
            return currentUrl.Host == "localhost"
                ? $"http://localhost:8351"
                : $"https://{DomainService.GetBaseDomain(currentUrl.Host)}";
        }

        public string GetBaseUrl(Uri currentUrl)
        {
            return currentUrl.Host == "localhost"
                ? $"http://localhost:4303"
                : $"https://{DomainService.GetBaseDomain(currentUrl.Host)}";
        }

        public string GetAuthWithBackUrl(Uri requestUri)
        {
            var authUrl = GetAuthServerUrl(requestUri);
            var baseUrl = GetBaseUrl(requestUri);
            var currentUrl = $"{baseUrl}{requestUri.PathAndQuery}";
            return $"{authUrl}/Authorize?redirect_uri={HttpUtility.UrlEncode(currentUrl)}";
        }

        public bool IsDevelpmentDomain(string host)
        {
            return environment.Value != "prod";
        }

        public bool IsProductionDomain(string host)
        {
            return environment.Value == "prod";
        }

        public string GetSsoUrl(Uri currentUri)
        {
            var host = currentUri.Host;
            return IsLocalHost(host)
                ? $"http://{host}:55556"
                : $"https://{DomainService.GetSsoDomain(host)}";
        }

        private bool IsLocalHost(string url)
        {
            return url.Contains("localhost");
        }
    }
}
