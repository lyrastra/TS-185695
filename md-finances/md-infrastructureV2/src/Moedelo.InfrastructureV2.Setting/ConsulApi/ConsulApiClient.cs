using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.Setting.ConsulApi
{
    [InjectAsSingleton]
    public class ConsulApiClient : IConsulApiClient, IDisposable
    {
        private static readonly TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(-1.0);
        
        private bool disposed;
        
        private const string ConsulHost = "org.moedelo.consul";
        private const string ConsulEndPoint = "http://" + ConsulHost+ ":8500/v1/kv/";

        private HttpClient httpClient;
        
        public ConsulApiClient()
        {
            httpClient = new HttpClient {Timeout = InfiniteTimeout};
            httpClient.DefaultRequestHeaders.Connection.Clear();
            httpClient.DefaultRequestHeaders.ConnectionClose = false;
            httpClient.DefaultRequestHeaders.Connection.Add("Keep-Alive");
        }

        public Task<List<ConsulKvEntry>> GetSettingsAsync(string settingName, HttpQuerySetting setting = null)
        {
            CheckDisposed();

            return GetAsync<List<ConsulKvEntry>>($"{ConsulEndPoint}{settingName}", setting);
        }
        
        private async Task<TR> GetAsync<TR>(string uri, HttpQuerySetting setting)
        {
            CheckDisposed();

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                var resultJson = await SendAsync(requestMessage, setting).ConfigureAwait(false);
                var result = resultJson.FromJsonString<TR>();
                return result;
            }
        }
        
        private async Task<string> SendAsync(HttpRequestMessage requestMessage, HttpQuerySetting setting)
        {
            requestMessage.Version = HttpVersion.Version11;

            if (setting == null)
            {
                setting = new HttpQuerySetting();
            }

            using (var cts = new CancellationTokenSource(setting.Timeout))
            {
                using (var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(
                            $"Consul is not available. HttpCode = {response.StatusCode}. ReasonPhrase = {response.ReasonPhrase}");
                    }
                    
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return result;
                }
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }
            }

            disposed = true;
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ConsulApiClient");
            }
        }
    }
}