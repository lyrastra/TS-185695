using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces.Factories;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http.Factories
{
    [InjectAsSingleton(typeof(IDisposableHttpRequestExecutorFactory))]
    internal sealed class DisposableHttpRequestExecutorFactory: IDisposableHttpRequestExecutorFactory
    {
        private readonly IHttpClientFactory clientFactory;

        public DisposableHttpRequestExecutorFactory(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IDisposableHttpRequestExecutor Create(HttpRequestExecutionSettings settings)
        {
            var httpClient = clientFactory.Create(new HttpClientSettings
            {
                AllowUntrustedSslCertificates = settings.AllowUntrustedSslCertificates,
                MaxResponseContentBufferSize = settings.MaxResponseContentBufferSize
            });

            return HttpRequestExecutor.Create(httpClient);
        }
    }
}