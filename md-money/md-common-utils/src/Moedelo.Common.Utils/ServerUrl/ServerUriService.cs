using System;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Utils.ServerUrl
{
    [InjectAsSingleton(typeof(IServerUriService))]
    public class ServerUriService : IServerUriService
    {
        public string GetBaseUrl(Uri uri)
        {
            var host = uri.Host;
            return IsLocalHost(host)
                ? $"http://{host}:4303"
                : $"https://{DomainService.GetBaseDomain(host)}";
        }

        public string GetSsoUrl(Uri uri)
        {
            var host = uri.Host;
            return IsLocalHost(host)
                ? $"http://{host}:55556"
                : $"https://{DomainService.GetSsoDomain(host)}";
        }

        public string GetAuthServerUrl(Uri uri)
        {
            var host = uri.Host;
            return IsLocalHost(host)
                ? $"http://{host}:5646"
                : $"https://{DomainService.GetOauthDomain(host)}";
        }

        private bool IsLocalHost(string url)
        {
            return url.Contains("localhost");
        }
    }
}
