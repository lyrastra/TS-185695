using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Helpers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Http.Abstractions.Extensions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Http.Abstractions
{
    public abstract class BaseApiClientInternal
    {
        private readonly string typeName;
        private readonly IHttpRequestExecuter httpRequestExecutor;
        private readonly IUriCreator uriCreator;
        private readonly IAuditTracer auditTracer;
        private readonly IDefaultHeadersGetter[] defaultHeadersGetterCollection;
        private readonly SettingValue endpointSetting;
        private readonly ILogger logger;

        protected BaseApiClientInternal(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IDefaultHeadersGetter[] defaultHeadersGetterCollection,
            SettingValue endpointSetting,
            ILogger logger,
            string auditTypeName = null)
        {
            this.httpRequestExecutor = httpRequestExecutor;
            this.uriCreator = uriCreator;
            this.auditTracer = auditTracer;
            this.defaultHeadersGetterCollection = defaultHeadersGetterCollection ?? Array.Empty<IDefaultHeadersGetter>();
            this.endpointSetting = endpointSetting;
            this.logger = logger;
            typeName = auditTypeName ?? GetType().Name;
        }

        #region Get

        protected async Task<TResponse> GetAsync<TResponse>(
            HttpGetRequest request,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, request.Uri, request.QueryParams);
            var httpMethod = HttpMethod.Get;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScopeWithName(
                fullUri, memberName, sourceFilePath, sourceLineNumber, request.AuditTrailSpanName);

            try
            {
                var headers = AddDefaultHeaders(request.Headers);
                var response = await httpRequestExecutor
                    .GetAsync(fullUri, headers, request.Settings ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }
        
        protected async Task<TResponse> GetAsync<TResponse>(
            string uri,
            object queryParams = null,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Get;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .GetAsync(fullUri, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        #endregion

        #region Post

        /// <summary>
        /// POST-вызов без тела запроса и без ответа
        /// </summary>
        protected async Task PostAsync(
            string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .PostAsync(fullUri, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        /// <summary>
        /// POST-запрос с телом запроса, но без ответа
        /// </summary>
        protected async Task PostAsync<TRequest>(
            string uri,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);


            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .PostAsync(fullUri, httpContent.HttpContent, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri, data);
                scope.Span.SetError(ex);
                throw;
            }
        }

        /// <summary>
        /// POST-вызов без тела запроса, но с ответом
        /// </summary>
        protected async Task<TResponse> PostAsync<TResponse>(
            string uri,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .PostAsync(fullUri, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        /// <summary>
        /// POST-запрос с телом запроса и ответом
        /// </summary>
        protected async Task<TResponse> PostAsync<TRequest, TResponse>(
            string uri,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .PostAsync(fullUri, httpContent.HttpContent, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri, data);
                scope.Span.SetError(ex);
                throw;
            }
        }

        #endregion

        #region Put

        protected async Task PutAsync<TRequest>(
            string uri,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Put;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .PutAsync(fullUri, httpContent.HttpContent, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri, data);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<TResponse> PutAsync<TRequest, TResponse>(
            string uri,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Put;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .PutAsync(fullUri, httpContent.HttpContent, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri, data);
                scope.Span.SetError(ex);
                throw;
            }
        }

        #endregion

        #region Patch
        protected async Task<TResponse> PatchAsync<TRequest, TResponse>(
            string uri,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Patch;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .PatchAsync(fullUri, httpContent.HttpContent, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri, data);
                scope.Span.SetError(ex);
                throw;
            }
        }
        #endregion

        #region Delete

        protected async Task DeleteAsync(
            string uri,
            object queryParams = null,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Delete;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .DeleteAsync(fullUri, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task DeleteByRequestAsync<TRequest>(
            string uri,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Delete;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .DeleteAsync(fullUri, httpContent.HttpContent, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task DeleteByRequestAsync<TRequest>(
            string uri,
            object queryParams,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Delete;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .DeleteAsync(fullUri, httpContent.HttpContent, headers, setting ?? DefaultSetting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        #endregion

        #region File

        protected async Task<HttpFileModel> DownloadFileAsync(
            HttpGetRequest request,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, request.Uri, request.QueryParams);
            var httpMethod = HttpMethod.Get;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScopeWithName(
                fullUri, memberName, sourceFilePath, sourceLineNumber, request.AuditTrailSpanName);

            try
            {
                var headers = AddDefaultHeaders(request.Headers);
                var result = await httpRequestExecutor
                    .DownloadFileAsync(fullUri, headers, request.Settings, cancellationToken)
                    .ConfigureAwait(false);
                scope.TryAddFileTag(result);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }
        
        protected async Task<HttpFileModel> DownloadFileAsync(
            string uri,
            object queryParams = null,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Get;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var headers = AddDefaultHeaders(queryHeaders);
                var result = await httpRequestExecutor
                    .DownloadFileAsync(fullUri, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
                scope.TryAddFileTag(result);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<HttpFileModel> DownloadFileAsync<TRequest>(
            string uri,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Get;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                var result = await httpRequestExecutor
                    .DownloadFileAsync(fullUri, httpContent.HttpContent, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
                scope.TryAddFileTag(result);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<HttpFileModel> DownloadFileAsync<TRequest>(
            string uri,
            object queryParams,
            TRequest data,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Get;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                var result = await httpRequestExecutor
                    .DownloadFileAsync(fullUri, httpContent.HttpContent, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
                scope.TryAddFileTag(result);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task UploadFileAsync(
            string uri,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .UploadFileAsync(fullUri, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task UploadFileAsync(
            string uri,
            object queryParams,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor.UploadFileAsync(fullUri, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task UploadFileAsync<TRequest>(
            string uri,
            TRequest data,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .UploadFileAsync(fullUri, httpContent.HttpContent, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task UploadFileAsync<TRequest>(
            string uri,
            object queryParams,
            TRequest data,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                await httpRequestExecutor
                    .UploadFileAsync(fullUri, httpContent.HttpContent, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<TResponse> UploadFileAsync<TResponse>(
            string uri,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .UploadFileAsync(fullUri, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<TResponse> UploadFileAsync<TResponse>(
            string uri,
            object queryParams,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .UploadFileAsync(fullUri, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<TResponse> SendFileAsync<TRequest, TResponse>(
            string uri, 
            TRequest data, 
            HttpFileStream file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) 
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri,
                httpMethod.Method,
                memberName,
                sourceFilePath,
                sourceLineNumber);

            try
            {
                scope.TryAddFileTag(file);
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                var headers = AddDefaultHeaders(queryHeaders);
                var querySetting = setting ?? DefaultSetting;
                var response = await httpRequestExecutor
                    .UploadFileAsync(fullUri, httpContent.HttpContent, file, headers, querySetting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<TResponse> UploadFileAsync<TRequest, TResponse>(
            string uri,
            TRequest data,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .UploadFileAsync(fullUri, httpContent.HttpContent, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        protected async Task<TResponse> UploadFileAsync<TRequest, TResponse>(
            string uri,
            object queryParams,
            TRequest data,
            HttpFileModel file,
            IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
            HttpQuerySetting setting = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where TRequest : class
        {
            var apiEndpoint = endpointSetting.Value;
            var fullUri = uriCreator.Create(apiEndpoint, uri, queryParams);
            var httpMethod = HttpMethod.Post;
            logger.LogHttpCall(httpMethod, fullUri, memberName, sourceFilePath);

            using var scope = StartAuditScope(
                fullUri, httpMethod.Method,
                memberName, sourceFilePath, sourceLineNumber);

            try
            {
                var httpContent = data.ToUtf8JsonContent();
                scope.DumpRequestToTag(httpContent.Body);
                scope.TryAddFileTag(file);
                var headers = AddDefaultHeaders(queryHeaders);
                var response = await httpRequestExecutor
                    .UploadFileAsync(fullUri, httpContent.HttpContent, file, headers, setting, cancellationToken)
                    .ConfigureAwait(false);
                var result = response.FromJsonString<TResponse>();
                scope.DumpResponseToTag(response);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogHttpCallError(ex, httpMethod, fullUri);
                scope.Span.SetError(ex);
                throw;
            }
        }

        #endregion

        private Dictionary<string, string> AddDefaultHeaders(IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders)
        {
            var headers = new Dictionary<string, string>();

            foreach (var factory in defaultHeadersGetterCollection)
            {
                foreach (var (key, value) in factory.EnumerateHeaders())
                {
                    headers[key] = value;
                }
            }

            if (queryHeaders != null)
            {
                foreach (var (key, value) in queryHeaders)
                {
                    headers[key] = value;
                }
            }

            return headers;
        }

        private IAuditScope StartAuditScope(
            string fullUri,
            string httpMethod,
            string memberName,
            string sourceFilePath,
            int sourceLineNumber)
        {
            var spanName = $"{typeName}.{memberName} {new Uri(fullUri).GetAuditSpanName(httpMethod)}";

            return StartAuditScopeWithName(fullUri, memberName, sourceFilePath, sourceLineNumber, spanName);
        }

        private IAuditScope StartAuditScopeWithName(string fullUri,
            string memberName,
            string sourceFilePath,
            int sourceLineNumber,
            string spanName)
        {
            return auditTracer
                .BuildSpan(AuditSpanType.OutgoingHttpRequest, spanName)
                .WithStartDateUtc(DateTime.UtcNow)
                .WithFullUri(fullUri)
                .TagCodeSourcePath(memberName, sourceFilePath, sourceLineNumber)
                .Start();
        }

        protected abstract HttpQuerySetting DefaultSetting { get; }
    }
}
