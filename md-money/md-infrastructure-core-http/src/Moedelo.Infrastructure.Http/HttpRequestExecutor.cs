using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces.Factories;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Infrastructure.Http.Extensions;
using Moedelo.Infrastructure.Http.Internals;

namespace Moedelo.Infrastructure.Http
{
    [InjectAsTransient(typeof(IHttpRequestExecuter))]
    internal sealed class HttpRequestExecutor : IDisposableHttpRequestExecutor
    {
        private static readonly HttpQuerySetting DefaultSetting = new HttpQuerySetting();

        private bool disposed;

        private HttpClient httpClient;

        public HttpRequestExecutor(IHttpClientFactory httpClientFactory)
            : this(httpClientFactory.Create(HttpClientSettings.Default))
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="httpClient">экземпляр класса HttpClient.
        /// Экземпляр переходит во владение создаваемого объекта</param>
        private HttpRequestExecutor(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        internal static HttpRequestExecutor Create(HttpClient httpClient)
        {
            return new HttpRequestExecutor(httpClient);
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> GetAsync(
            string uri, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> PostAsync(string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> PostAsync(string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = data;
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> PutAsync(string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null, CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            requestMessage.Content = data;
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> PatchAsync(string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null, CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);
            requestMessage.Content = data;
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> DeleteAsync(string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null, CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> DeleteAsync(string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null, CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            requestMessage.Content = data;
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken).ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<HttpFileModel> DownloadFileAsync(string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            AddHeaders(requestMessage, headers);
            var result = await DownloadFileAsync(requestMessage, setting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<HttpFileModel> DownloadFileAsync(string uri,
            HttpContent data,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = data;
            AddHeaders(requestMessage, headers);
            var result = await DownloadFileAsync(requestMessage, setting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<HttpFileStream> DownloadFileAsync<TRequestBody>(string uri,
            HttpMethod httpMethod,
            HttpContent dataHttpContent,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            setting ??= DefaultSetting;
            
            var stopWatch = Stopwatch.StartNew();

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(setting.Timeout);

            using var requestMessage = new HttpRequestMessage(httpMethod, uri);
            AddHeaders(requestMessage, headers);
            requestMessage.Content = dataHttpContent;

            try
            {
                var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCodeEx(setting);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var httpContentHeaders = response.Content.Headers;
                var fileName = httpContentHeaders.ParseFileName();
                var contentType = httpContentHeaders.ContentType?.MediaType;
                // освобождение объекта response делегируется обёртке над потоком
                var stream = await HttpResponseMessageStream.CreateAsync(response).ConfigureAwait(false);

                return new HttpFileStream(fileName, contentType, stream);
            }
            catch (OperationCanceledException exception) when (cancellationToken.IsCancellationRequested == false)
            {
                throw new HttpRequestTimeoutException(requestMessage, exception, setting.Timeout, stopWatch.Elapsed);
            }
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> UploadFileAsync(string uri,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = file.CreateMultipartFormDataContent();
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        public async Task<string> UploadFileAsync(string uri,
            HttpContent dataHttpContent,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken= default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = file.CreateMultipartFormDataContent(dataHttpContent);
            AddHeaders(requestMessage, headers);
            var result = await SendAsync(requestMessage, setting, cancellationToken).ConfigureAwait(false);

            return result;
        }

        public async Task<string> UploadFileAsync(string uri,
            HttpContent dataHttpContent,
            HttpFileStream file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = file.ToMultipartFormDataContent(dataHttpContent);

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        private static void AddHeaders(
            HttpRequestMessage requestMessage,
            IReadOnlyCollection<KeyValuePair<string, string>> headers)
        {
            if (headers == null)
            {
                return;
            }

            foreach (var keyValuePair in headers)
            {
                requestMessage.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        private async Task<string> SendAsync(
            HttpRequestMessage requestMessage, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers, 
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            requestMessage.Version = HttpVersion.Version11;
            AddHeaders(requestMessage, headers);
            setting ??= DefaultSetting;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(setting.Timeout);
            var stopWatch = Stopwatch.StartNew();

            try
            {
                using var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCodeEx(setting);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return result;
            }
            catch (OperationCanceledException exception) when (cancellationToken.IsCancellationRequested == false)
            {
                throw new HttpRequestTimeoutException(requestMessage, exception, setting.Timeout, stopWatch.Elapsed);
            }
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        private async Task<string> SendAsync(
            HttpRequestMessage requestMessage, 
            HttpQuerySetting setting,
            CancellationToken cancellationToken = default)
        {
            requestMessage.Version = HttpVersion.Version11;

            setting ??= DefaultSetting;

            using var ctsTimeout = new CancellationTokenSource(setting.Timeout);
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeout.Token, cancellationToken);
            var stopWatch = Stopwatch.StartNew();

            try
            {
                using var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                string content = null;
                // при выставленном параметре начитываем тело запроса даже в случае ошибки
                if (setting.SetResponseContentIntoException)
                {
                    content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }

                response.EnsureSuccessStatusCodeEx(setting, content);
                content ??= await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return content;
            }
            catch (OperationCanceledException exception) when(ctsTimeout.IsCancellationRequested)
            {
                throw new HttpRequestTimeoutException(requestMessage, exception, setting.Timeout, stopWatch.Elapsed);
            }
        }

        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestResponseStatusException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestValidationException"></exception>
        /// <exception cref="Moedelo.Infrastructure.Http.Abstractions.Exceptions.HttpRequestTimeoutException"></exception>
        private async Task<HttpFileModel> DownloadFileAsync(
            HttpRequestMessage requestMessage,
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            requestMessage.Version = HttpVersion.Version11;

            setting ??= DefaultSetting;

            using var ctsTimeout = new CancellationTokenSource(setting.Timeout);
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeout.Token, cancellationToken);
            var stopWatch = Stopwatch.StartNew();

            try
            {
                using var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCodeEx(setting);

                var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                var httpContentHeaders = response.Content.Headers;

                return new HttpFileModel(
                    httpContentHeaders.ContentDisposition?.FileNameStar ??
                    httpContentHeaders.ContentDisposition?.FileName,
                    httpContentHeaders.ContentType?.MediaType,
                    new MemoryStream(bytes, false) {Position = 0});
            }

            catch (OperationCanceledException exception) when(ctsTimeout.IsCancellationRequested)
            {
                throw new HttpRequestTimeoutException(requestMessage, exception, setting.Timeout, stopWatch.Elapsed);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
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
                throw new ObjectDisposedException(nameof(HttpRequestExecutor));
            }
        }
        
        ~HttpRequestExecutor()
        {
            Dispose(false);
        }
    }
}