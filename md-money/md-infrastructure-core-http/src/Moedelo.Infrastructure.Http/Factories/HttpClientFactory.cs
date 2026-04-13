using System;
using System.Net.Http;
using System.Threading;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces.Factories;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http.Factories
{
    [InjectAsSingleton(typeof(IHttpClientFactory))]
    internal sealed class HttpClientFactory : IHttpClientFactory
    {
        private static readonly TimeSpan InfiniteTimeout = Timeout.InfiniteTimeSpan;

        public HttpClient Create(HttpClientSettings settings)
        {
            var httpClientHandler = new HttpClientHandler
            {
                UseProxy = false,
                Proxy = null,
                UseCookies = false,
                MaxConnectionsPerServer = int.MaxValue
            };

            if (settings.AllowUntrustedSslCertificates)
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
            }
            
            var httpClient = new HttpClient(httpClientHandler) {Timeout = InfiniteTimeout};
            
            httpClient.DefaultRequestHeaders.Connection.Clear();
            httpClient.DefaultRequestHeaders.ConnectionClose = false;
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");

            if (settings.MaxResponseContentBufferSize.HasValue)
                httpClient.MaxResponseContentBufferSize = settings.MaxResponseContentBufferSize.Value;

            return httpClient;
        }
    }
}