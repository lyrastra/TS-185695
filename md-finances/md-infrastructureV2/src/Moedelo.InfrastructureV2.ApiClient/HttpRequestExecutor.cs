using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.ApiClient.Extensions;
using Moedelo.InfrastructureV2.ApiClient.Internals;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.ApiClient
{
    [InjectAsTransient(typeof(IHttpRequestExecutor))]
    public class HttpRequestExecutor : IHttpRequestExecutor, IDisposable
    {
        private static readonly TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(-1.0);
        private static readonly HttpQuerySetting DefaultSetting = new ();

        private bool disposed;

        private HttpClient httpClient;

        static HttpRequestExecutor()
        {
            // включение поддержки TLS 1.2
            if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls12) == 0)
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                // добавить SecurityProtocolType.Tls13, когда перейдём на 4.8 
            }
        }

        public HttpRequestExecutor()
        {
            var handler = new HttpClientHandler
            {
                UseProxy = false,
                Proxy = null,
                //UseCookies = false,
            };
            httpClient = new HttpClient(handler) {Timeout = InfiniteTimeout};
            httpClient.DefaultRequestHeaders.Connection.Clear();
            httpClient.DefaultRequestHeaders.ConnectionClose = false;
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        public async Task<string> GetAsync(
            string uri, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> GetAsync<TResponse, TDeserializerArg1>(
            string uri,
            Func<TDeserializerArg1, Stream, TResponse> contentDeserializer,
            TDeserializerArg1 deserializerArg1,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            return await SendAsync(
                    requestMessage,
                    contentDeserializer,
                    deserializerArg1,
                    headers,
                    setting,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> PostAsync(
            string uri, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> PostAsync<TResponse, TDeserializerArg1, TDeserializerArg2>(
            string uri,
            Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
            TDeserializerArg1 deserializerArg1,
            TDeserializerArg2 deserializerArg2,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            return await SendAsync(
                    requestMessage,
                    contentDeserializer,
                    deserializerArg1,
                    deserializerArg2,
                    headers,
                    setting,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> PostAsync<TRequest>(
            string uri, 
            TRequest data, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
            where TRequest : class
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = data.ToJsonObjectHttpContent();

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse, TDeserializerArg1, TDeserializerArg2>(
            string uri,
            TRequest data,
            Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
            TDeserializerArg1 deserializerArg1,
            TDeserializerArg2 deserializerArg2,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken) where TRequest : class
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = data.ToJsonObjectHttpContent();

            return await SendAsync(
                    requestMessage,
                    contentDeserializer,
                    deserializerArg1,
                    deserializerArg2,
                    headers,
                    setting,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> PostAsFormUrlEncodedAsync(
            string uri, 
            IReadOnlyList<KeyValuePair<string, string>> data, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = data.CreateFormUrlEncodedContent();

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> PostBodyAsync(
            string uri, 
            string data, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent(data);

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> PostBodyAsync<TResponse, TDeserializerArg1, TDeserializerArg2>(
            string uri,
            string data,
            Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
            TDeserializerArg1 deserializerArg1,
            TDeserializerArg2 deserializerArg2,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent(data);

            return await SendAsync(
                    requestMessage,
                    contentDeserializer,
                    deserializerArg1,
                    deserializerArg2,
                    headers,
                    setting,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> PutAsync(
            string uri, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> PutAsync<TRequest>(
            string uri, 
            TRequest data, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
            where TRequest : class
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            requestMessage.Content = data.ToJsonObjectHttpContent();

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse, TDeserializerArg1, TDeserializerArg2>(
            string uri,
            TRequest data,
            Func<TDeserializerArg1, TDeserializerArg2, Stream, TResponse> contentDeserializer,
            TDeserializerArg1 deserializerArg1,
            TDeserializerArg2 deserializerArg2,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken) where TRequest : class
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            requestMessage.Content = data.ToJsonObjectHttpContent();

            return await SendAsync(
                    requestMessage,
                    contentDeserializer,
                    deserializerArg1,
                    deserializerArg2,
                    headers,
                    setting,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task<string> SendAsync(
            HttpRequestMessage requestMessage, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers, 
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            requestMessage.Version = HttpVersion.Version11;
            requestMessage.AddHeaders(headers);

            setting ??= DefaultSetting;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(setting.Timeout);

            try
            {
                using var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCodeEx(setting);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return result;
            }
            catch (OperationCanceledException exception) when (cancellationToken.IsCancellationRequested == false)
            {
                throw new HttpRequestTimeoutException(
                    requestMessage.RequestUri,
                    requestMessage.Method,
                    exception);
            }
        }

        private Task<TResponse> SendAsync<TResponse, TDeserializeArg1>(
            HttpRequestMessage requestMessage,
            Func<TDeserializeArg1, Stream, TResponse> deserializeContent,
            TDeserializeArg1 deserializeArg1,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            return SendAsync(
                requestMessage,
                (deserialize, arg1, stream) => deserialize(arg1, stream),
                deserializeContent,
                deserializeArg1,
                headers,
                setting,
                cancellationToken);
        }

        private async Task<TResponse> SendAsync<TResponse, TDeserializeArg1, TDeserializeArg2>(
            HttpRequestMessage requestMessage, 
            Func<TDeserializeArg1, TDeserializeArg2, Stream, TResponse> deserializeContent,
            TDeserializeArg1 deserializeArg1,
            TDeserializeArg2 deserializeArg2,
            IReadOnlyCollection<KeyValuePair<string, string>> headers, 
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            requestMessage.Version = HttpVersion.Version11;
            requestMessage.AddHeaders(headers);

            setting ??= DefaultSetting;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(setting.Timeout);

            try
            {
                using var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCodeEx(setting);

                using var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                return deserializeContent(deserializeArg1, deserializeArg2, contentStream);
            }
            catch (OperationCanceledException exception) when (cancellationToken.IsCancellationRequested == false)
            {
                throw new HttpRequestTimeoutException(
                    requestMessage.RequestUri,
                    requestMessage.Method,
                    exception);
            }
        }

        public async Task<HttpFileModel> SendFileAsync<TRequest>(string uri,
            HttpMethod httpMethod,
            TRequest data = null,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default) where TRequest : class
        {
            CheckDisposed();

            setting ??= DefaultSetting;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(setting.Timeout);

            using var requestMessage = new HttpRequestMessage(httpMethod, uri);
            requestMessage.Version = HttpVersion.Version11;
            requestMessage.AddHeaders(headers);

            if (null != data)
            {
                requestMessage.Content = data.ToJsonObjectHttpContent();
            }

            try
            {
                using var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCodeEx(setting);

                return await response.ReadHttpFileModelAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException exception) when (cancellationToken.IsCancellationRequested == false)
            {
                throw new HttpRequestTimeoutException(
                    requestMessage.RequestUri,
                    requestMessage.Method,
                    exception);
            }
        }

        public async Task<HttpFileStream> DownloadFileAsync<TRequestBody>(
            string uri,
            HttpMethod httpMethod,
            TRequestBody requestBody,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            CheckDisposed();

            setting ??= DefaultSetting;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(setting.Timeout);

            using var requestMessage = new HttpRequestMessage(httpMethod, uri);
            requestMessage.Version = HttpVersion.Version11;
            requestMessage.AddHeaders(headers);

            if (null != requestBody)
            {
                requestMessage.Content = requestBody.ToJsonObjectHttpContent();
            }

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
                throw new HttpRequestTimeoutException(
                    requestMessage.RequestUri,
                    requestMessage.Method,
                    exception);
            }
        }

        public async Task<HttpFileStreamMetadata<TMetadata>> DownloadFileWithMetadataAsync<TRequestBody, TMetadata, TDeserializerArg1>(
            string uri,
            HttpMethod httpMethod,
            Func<TDeserializerArg1, Stream, TMetadata> metadataDeserializer,
            TDeserializerArg1 deserializerArg1,
            TRequestBody requestBody = default,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            setting ??= DefaultSetting;

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(setting.Timeout);

            using var requestMessage = new HttpRequestMessage(httpMethod, uri);
            requestMessage.Version = HttpVersion.Version11;
            requestMessage.AddHeaders(headers);

            if (null != requestBody)
            {
                requestMessage.Content = requestBody.ToJsonObjectHttpContent();
            }

            try
            {
                var response = await httpClient.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCodeEx(setting);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var multipart = await response.Content.ReadAsMultipartAsync(cancellationToken);
                using var responseBodyStream = await multipart.Contents[0].ReadAsStreamAsync().ConfigureAwait(false);
                var metadata = metadataDeserializer(deserializerArg1, responseBodyStream);

                var fileContentPart = multipart.Contents[1];
                var fileName = fileContentPart.Headers.ParseFileName();
                var contentType = fileContentPart.Headers.ContentType?.MediaType;
                // освобождение объекта response делегируется обёртке над потоком
                var fileContentStream = await fileContentPart.ReadAsStreamAsync().ConfigureAwait(false);
                var stream = HttpResponseMessageStream.Create(response, fileContentStream);

                return new HttpFileStreamMetadata<TMetadata>(fileName, stream, contentType, metadata);
            }
            catch (OperationCanceledException exception) when (cancellationToken.IsCancellationRequested == false)
            {
                throw new HttpRequestTimeoutException(
                    requestMessage.RequestUri,
                    requestMessage.Method,
                    exception);
            }
        }

        public Task<string> PutFileAsync(string uri,
            HttpFileStream file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers,
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            return UploadFileAsync(HttpMethod.Put, uri, file, headers, setting, cancellationToken);
        }

        private async Task<string> UploadFileAsync(
            HttpMethod httpMethod,
            string uri, 
            HttpFileStream file, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers, 
            HttpQuerySetting setting,
            CancellationToken cancellationToken)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(httpMethod, uri);
            requestMessage.Content = file.ToMultipartFormDataContent();

            return await SendAsync(requestMessage, headers, setting, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> UploadFileAsync(
            string uri,
            HttpFileStream file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = file.ToMultipartFormDataContent();

            return await SendAsync(requestMessage, headers, setting, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> UploadFileAsync<TRequest>(
            string uri,
            TRequest data,
            HttpFileStream file,
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default) where TRequest : class
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = file.ToMultipartFormDataContent(data);

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> DeleteAsync(
            string uri, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> DeleteAsync<TRequest>(
            string uri, 
            TRequest data, 
            IReadOnlyCollection<KeyValuePair<string, string>> headers = null, 
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default)
            where TRequest : class
        {
            CheckDisposed();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            requestMessage.Content = data.ToJsonObjectHttpContent();

            return await SendAsync(requestMessage, headers, setting, cancellationToken).ConfigureAwait(false);
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
                throw new ObjectDisposedException("HttpRequestExecutor");
            }
        }
    }
}
