using System;
using System.Net.Http;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Infrastructure.Consul.Net5
{
    [InjectAsSingleton(typeof(IConsulHttpClientFactory))]
    public class ConsulHttpClientFactory : HttpClient, IConsulHttpClientFactory
    {
        public HttpClient CreateHttpClient()
        {
            return new HttpClient(new SocketsHttpHandler
            {
                // держим соединение открытым (обращения на обновление настроек будут чаще
                PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                // одно соединение на сервер
                MaxConnectionsPerServer = 1
            });
        }

        /// <summary>
        /// Начиная с .net core 3.0 клиент не вызывает Dispose для переданных HttpContent
        /// </summary>
        public bool IsHttpContentAutoDisposable => false;
    }
}
