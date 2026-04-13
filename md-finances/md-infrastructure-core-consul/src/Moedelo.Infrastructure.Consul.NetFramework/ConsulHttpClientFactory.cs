using System.Net.Http;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.Consul.NetFramework
{
    [InjectAsSingleton(typeof(IConsulHttpClientFactory))]
    public class ConsulHttpClientFactory : IConsulHttpClientFactory
    {
        public HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Connection.Clear();
            httpClient.DefaultRequestHeaders.ConnectionClose = false;
            httpClient.DefaultRequestHeaders.Connection.Add("Keep-Alive");

            return httpClient;
        }

        public bool IsHttpContentAutoDisposable => true;
    }
}
