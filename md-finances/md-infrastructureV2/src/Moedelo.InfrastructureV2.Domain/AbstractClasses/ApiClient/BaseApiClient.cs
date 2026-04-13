#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Extensions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Internals;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Domain.Extensions;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;

public abstract class BaseApiClient
{
    private readonly string typeName;
    private readonly HttpQuerySetting defaultSetting = new(TimeSpan.FromSeconds(30));

    private readonly IHttpRequestExecutor httpRequestExecutor;
    private readonly IUriCreator uriCreator;
    private readonly IResponseParser responseParser;
    private readonly IAuditTracer auditTracer;
    private readonly IAuditScopeManager auditScopeManager;

    protected BaseApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        string? auditTypeName = null)
    {
        this.httpRequestExecutor = httpRequestExecutor;
        this.responseParser = responseParser;
        this.auditTracer = auditTracer;
        this.auditScopeManager = auditScopeManager;
        this.uriCreator = uriCreator;
        typeName = auditTypeName ?? GetType().Name;
    }

    protected Task<TResponse> GetAsync<TResponse>(string uri,
        object? queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<TResponse>(HttpMethod.Get, uri, queryParams, queryHeaders, setting,
            static (httpRequestExecutor, responseParser, apiCall, cancellation) => httpRequestExecutor
                .GetAsync(apiCall.Uri,
                    static (parser, stream) => parser.Parse<TResponse>(stream),
                    responseParser,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task GetAsync(string uri,
        object? queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(HttpMethod.Get, uri, queryParams, queryHeaders, setting,
            static (httpRequestExecutor, apiCall, cancellation) => httpRequestExecutor
                .GetAsync(apiCall.Uri, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> PostAsync<TRequest, TResponse>(string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope<TRequest, TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, data,
            static (httpRequestExecutor, responseParser, apiCall, request, cancellation) => httpRequestExecutor
                .PostAsync(apiCall.Uri,
                    request,
                    DeserializeResponse<TResponse>,
                    responseParser,
                    apiCall.AuditTrailScope,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> PostAsync<TResponse>(string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting,
            static (httpRequestExecutor, responseParser, apiCall, cancellation) => httpRequestExecutor
                .PostAsync(apiCall.Uri,
                    DeserializeResponse<TResponse>,
                    responseParser,
                    apiCall.AuditTrailScope,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task PostAsync<TRequest>(string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, data,
            static (httpRequestExecutor, apiCall, request, cancellation) => httpRequestExecutor
                .PostAsync(apiCall.Uri, request, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task PostAsync(string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting,
            static (httpRequestExecutor, apiCall, cancellation) => httpRequestExecutor
                .PostAsync(apiCall.Uri, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> PostBodyAsync<TResponse>(string uri,
        string data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<string, TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, data,
            static (httpRequestExecutor, responseParser, apiCall, request, cancellation) => httpRequestExecutor
                .PostBodyAsync(apiCall.Uri,
                    request,
                    DeserializeResponse<TResponse>,
                    responseParser,
                    apiCall.AuditTrailScope,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task PutAsync<TRequest>(string uri,
        TRequest request,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope(HttpMethod.Put,
            uri, queryParams: null, queryHeaders, setting, request,
            static (httpRequestExecutor, apiCall, request, cancellation) => httpRequestExecutor
                .PutAsync(apiCall.Uri, request, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> PutAsync<TRequest, TResponse>(string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope<TRequest, TResponse>(HttpMethod.Put,
            uri, queryParams: null, queryHeaders, setting, data,
            static (httpRequestExecutor, responseParser, apiCall, request, cancellation) => httpRequestExecutor
                .PutAsync(apiCall.Uri,
                    request,
                    DeserializeResponse<TResponse>,
                    responseParser,
                    apiCall.AuditTrailScope,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task DeleteAsync(string uri,
        object? queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(HttpMethod.Delete, uri, queryParams,
            queryHeaders, setting,
            static (httpRequestExecutor, apiCall, cancellation) => httpRequestExecutor
                .DeleteAsync(apiCall.Uri, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task DeleteByRequestAsync<TRequest>(string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope(HttpMethod.Delete,
            uri, queryParams: null, queryHeaders, setting, data,
            static (httpRequestExecutor, apiCall, request, cancellation) => httpRequestExecutor
                .DeleteAsync(apiCall.Uri, request, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task DeleteByRequestAsync<TRequest>(string uri,
        object queryParams,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope(HttpMethod.Delete,
            uri, queryParams, queryHeaders, setting, data,
            static (httpRequestExecutor, apiCall, request, cancellation) => httpRequestExecutor
                .DeleteAsync(apiCall.Uri, request, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Скачать файл с помощью метода GET
    /// </summary>
    protected Task<HttpFileModel> GetFileAsync(string uri,
        object? queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<HttpFileModel>(HttpMethod.Get,
            uri, queryParams, queryHeaders, setting,
            static (httpRequestExecutor, _, apiCall, cancellation) => httpRequestExecutor
                .SendFileAsync<object?>(apiCall.Uri,
                    apiCall.HttpMethod,
                    data: null,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Скачать файл с помощью метода POST
    /// </summary>
    protected Task<HttpFileModel> DownloadFileByPostMethodAsHttpFileModelAsync<TRequest>(
        string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TRequest : class
    {
        return RunInAuditScope<object, HttpFileModel>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, data,
            static (httpRequestExecutor, _, apiCall, request, cancellation) => httpRequestExecutor
                .SendFileAsync<object?>(apiCall.Uri,
                    apiCall.HttpMethod,
                    request,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            CancellationToken.None,
            memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Отправить (выгрузить) файл
    /// </summary>
    protected Task<string> SendFileAsync(string uri,
        HttpFileModel file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<HttpFileModel, string>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, file,
            static (httpRequestExecutor, _, apiCall, file, cancellation) => httpRequestExecutor
                .UploadFileAsync(apiCall.Uri,
                    file.ToHttpFileStream(disposeStream: false),
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<string> PutFileAsync(string uri,
        HttpFileModel file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<HttpFileModel, string>(HttpMethod.Put,
            uri, queryParams: null, queryHeaders, setting, file,
            static (httpRequestExecutor, _, apiCall, file, cancellation) => httpRequestExecutor
                .PutFileAsync(apiCall.Uri,
                    file.ToHttpFileStream(disposeStream: false),
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<string> PutFileAsync(string uri,
        HttpFileStream file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<HttpFileStream, string>(HttpMethod.Put,
            uri, queryParams: null, queryHeaders, setting, file,
            static (httpRequestExecutor, _, apiCall, file, cancellation) => httpRequestExecutor
                .PutFileAsync(apiCall.Uri, file, apiCall.Headers, apiCall.QuerySettings, cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> SendFileAsync<TResponse>(string uri,
        HttpFileModel file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<HttpFileModel, TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, file,
            static async (httpRequestExecutor, responseParser, apiCall, file, cancellation) =>
            {
                var rawResponse = await httpRequestExecutor
                    .UploadFileAsync(apiCall.Uri,
                        file.ToHttpFileStream(disposeStream: false),
                        apiCall.Headers,
                        apiCall.QuerySettings,
                        cancellation).ConfigureAwait(false);

                var response = responseParser.Parse<TResponse>(rawResponse);
                apiCall.AuditTrailScope.TryAddResponseTag(rawResponse);

                return response;
            },
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> SendFileAsync<TResponse>(string uri,
        HttpFileStream file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<HttpFileStream, TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, file,
            static async (httpRequestExecutor, responseParser, apiCall, file, cancellation) =>
            {
                var rawResponse = await httpRequestExecutor
                    .UploadFileAsync(apiCall.Uri, file, apiCall.Headers, apiCall.QuerySettings, cancellation)
                    .ConfigureAwait(false);

                var response = responseParser.Parse<TResponse>(rawResponse);
                apiCall.AuditTrailScope.TryAddResponseTag(rawResponse);

                return response;
            },
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> SendFileAsync<TRequest, TResponse>(string uri,
        TRequest data,
        HttpFileModel file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope<FileModelWithMetadata<TRequest>, TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, new(file, data),
            static async (httpRequestExecutor, responseParser, apiCall, file, cancellation) =>
            {
                var rawResponse = await httpRequestExecutor
                    .UploadFileAsync(apiCall.Uri,
                        file.Metadata,
                        file.File.ToHttpFileStream(disposeStream: false),
                        apiCall.Headers,
                        apiCall.QuerySettings,
                        cancellation)
                    .ConfigureAwait(false);

                var response = responseParser.Parse<TResponse>(rawResponse);
                apiCall.AuditTrailScope.TryAddResponseTag(rawResponse);

                return response;
            },
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> SendFileAsync<TRequest, TResponse>(string uri,
        TRequest data,
        HttpFileStream file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return RunInAuditScope<FileStreamWithMetadata<TRequest>, TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, new(file, data),
            static async (httpRequestExecutor, responseParser, apiCall, file, cancellation) =>
            {
                var rawResponse = await httpRequestExecutor
                    .UploadFileAsync(apiCall.Uri,
                        file.Metadata,
                        file.FileStream,
                        apiCall.Headers,
                        apiCall.QuerySettings,
                        cancellation)
                    .ConfigureAwait(false);

                var response = responseParser.Parse<TResponse>(rawResponse);
                apiCall.AuditTrailScope.TryAddResponseTag(rawResponse);

                return response;
            },
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task SendFileAsync<TRequest>(string uri,
        TRequest data,
        HttpFileModel file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TRequest : class
    {
        return RunInAuditScope<FileModelWithMetadata<TRequest>>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, new(file, data),
            static (httpRequestExecutor, apiCall, file, cancellation) => httpRequestExecutor
                .UploadFileAsync(apiCall.Uri,
                    file.Metadata,
                    file.File.ToHttpFileStream(disposeStream: false),
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<HttpFileStream> DownloadFileByGetMethodAsync(string uri,
        object queryParams,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(HttpMethod.Get,
            uri, queryParams, queryHeaders, setting,
            static (httpRequestExecutor, _, apiCall, cancellation) => httpRequestExecutor
                .DownloadFileAsync<object?>(apiCall.Uri,
                    apiCall.HttpMethod,
                    requestBody: null,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<HttpFileStreamMetadata<TMetadata>> DownloadFileWithMetadataByGetMethodAsync<TMetadata>(string uri,
        object queryParams,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(HttpMethod.Get,
            uri, queryParams, queryHeaders, setting,
            static (httpRequestExecutor, responseParser, apiCall, cancellation) => httpRequestExecutor
                .DownloadFileWithMetadataAsync(apiCall.Uri,
                    apiCall.HttpMethod,
                    DeserializeMetadata<TMetadata>,
                    new ResponseParserOnAudit(responseParser, apiCall.AuditTrailScope),
                    requestBody: default(object?),
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<HttpFileStreamMetadata<TMetadata>> DownloadFileWithMetadataByPostMethodAsync<TRequest, TMetadata>(
        string uri,
        TRequest requestBody,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TRequest : class
    {
        return RunInAuditScope(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, requestBody,
            static (httpRequestExecutor, responseParser, apiCall, request, cancellation) => httpRequestExecutor
                .DownloadFileWithMetadataAsync(apiCall.Uri,
                    apiCall.HttpMethod,
                    DeserializeMetadata<TMetadata>,
                    new ResponseParserOnAudit(responseParser, apiCall.AuditTrailScope),
                    requestBody: request,
                    apiCall.Headers,
                    apiCall.QuerySettings,
                    cancellation),
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected Task<TResponse> PostAsFormUrlEncodedAsync<TResponse>(string uri,
        IReadOnlyList<KeyValuePair<string, string>> data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope<IReadOnlyList<KeyValuePair<string, string>>, TResponse>(HttpMethod.Post,
            uri, queryParams: null, queryHeaders, setting, data,
            static async (httpRequestExecutor, responseParser, apiCall, request, cancellation) =>
            {
                var rawResponse = await httpRequestExecutor
                    .PostAsFormUrlEncodedAsync(apiCall.Uri,
                        request, apiCall.Headers, apiCall.QuerySettings, cancellation)
                    .ConfigureAwait(false);
                var response = responseParser.Parse<TResponse>(rawResponse);
                apiCall.AuditTrailScope.TryAddResponseTag(rawResponse);

                return response;
            },
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    protected abstract string GetApiEndpoint();

    protected virtual IEnumerable<KeyValuePair<string, string>> EnumerateExtraHeaders()
    {
        var auditSpanContext = auditScopeManager.Current?.Span?.Context;

        if (auditSpanContext != null)
        {
            yield return new KeyValuePair<string, string>(
                AuditHeaderParam.ParentScopeContext,
                auditSpanContext.ToJsonString());
        }
    }

    protected virtual HttpQuerySetting DefaultHttpQuerySetting()
    {
        return defaultSetting;
    }

    private IReadOnlyCollection<KeyValuePair<string, string>> MergeWithExtraHeaders(
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders)
    {
        if (queryHeaders is not { Count: > 0 })
        {
            return new List<KeyValuePair<string, string>>(EnumerateExtraHeaders());
        }

        return queryHeaders
            .Concat(EnumerateExtraHeaders())
            .ToArray();
    }

    private IAuditScope StartAuditScope(
        string fullUri,
        HttpMethod httpMethod,
        string memberName,
        string sourceFilePath,
        int sourceLineNumber)
    {
        var spanName = $"{typeName}.{memberName} {new Uri(fullUri).GetAuditSpanName(httpMethod.Method)}";

        return auditTracer
            .BuildSpan(AuditSpanType.OutgoingHttpRequest, spanName)
            .WithFullUri(fullUri)
            .TagCodeSourcePath(memberName, sourceFilePath, sourceLineNumber)
            .Start();
    }

    private async Task<TResponse> RunInAuditScope<TResponse>(HttpMethod httpMethod,
        string uri, object? queryParams,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders,
        HttpQuerySetting? setting,
        Func<IHttpRequestExecutor, IResponseParser, HttpApiCallContext, CancellationToken, Task<TResponse>> action,
        CancellationToken cancellationToken,
        string memberName,
        string sourceFilePath,
        int sourceLineNumber)
    {
        var fullUri = uriCreator.Create(GetApiEndpoint(), uri, queryParams);
        var requestHeaders = MergeWithExtraHeaders(queryHeaders);
        var querySetting = setting ?? DefaultHttpQuerySetting();

        using var scope = StartAuditScope(fullUri, httpMethod, memberName, sourceFilePath, sourceLineNumber);
        var context = new HttpApiCallContext(httpMethod, fullUri, requestHeaders, querySetting, scope);

        try
        {
            return await action(httpRequestExecutor, responseParser, context, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.SetErrorExceptCancellation(ex);
            throw;
        }
    }

    private Task RunInAuditScope(HttpMethod httpMethod,
        string uri, object? queryParams,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders,
        HttpQuerySetting? setting,
        Func<IHttpRequestExecutor, HttpApiCallContext, CancellationToken, Task> action,
        CancellationToken cancellationToken,
        string memberName,
        string sourceFilePath,
        int sourceLineNumber)
    {
        return RunInAuditScope<int>(httpMethod,
            uri, queryParams, queryHeaders, setting,
            async (_, _, context, cancellation) =>
            {
                await action(httpRequestExecutor, context, cancellation).ConfigureAwait(false);
                return default;
            },
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    private async Task<TResponse> RunInAuditScope<TRequest, TResponse>(HttpMethod httpMethod,
        string uri, object? queryParams,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders,
        HttpQuerySetting? setting,
        TRequest request,
        Func<IHttpRequestExecutor, IResponseParser, HttpApiCallContext, TRequest, CancellationToken, Task<TResponse>>
            action,
        CancellationToken cancellationToken,
        string memberName, string sourceFilePath, int sourceLineNumber) where TRequest : class
    {
        var fullUri = uriCreator.Create(GetApiEndpoint(), uri, queryParams);
        var requestHeaders = MergeWithExtraHeaders(queryHeaders);
        var querySetting = setting ?? DefaultHttpQuerySetting();

        using var scope = StartAuditScope(fullUri, httpMethod, memberName, sourceFilePath, sourceLineNumber)
            .TryAddRequestOrFileTag(request);
        var context = new HttpApiCallContext(httpMethod, fullUri, requestHeaders, querySetting, scope);

        try
        {
            return await action(httpRequestExecutor, responseParser, context, request, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            scope.SetErrorExceptCancellation(ex);
            throw;
        }
    }

    private Task RunInAuditScope<TRequest>(HttpMethod httpMethod,
        string uri, object? queryParams,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders,
        HttpQuerySetting? setting,
        TRequest request,
        Func<IHttpRequestExecutor, HttpApiCallContext, TRequest, CancellationToken, Task> action,
        CancellationToken cancellationToken,
        string memberName, string sourceFilePath, int sourceLineNumber) where TRequest : class
    {
        return RunInAuditScope<TRequest, int>(httpMethod,
            uri, queryParams, queryHeaders, setting, request,
            async (_, _, context, request1, cancellation) =>
            {
                await action(httpRequestExecutor, context, request1, cancellation).ConfigureAwait(false);
                return default;
            },
            cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    private static TResponse DeserializeResponse<TResponse>(IResponseParser parser,
        IAuditScope auditTrailScope, Stream responseContentStream)
    {
        var response = parser.Parse<TResponse>(responseContentStream);
        auditTrailScope.TryAddResponseTag(responseContentStream);

        return response;
    }

    private static TMetadata DeserializeMetadata<TMetadata>(ResponseParserOnAudit context,
        Stream responseContentStream)
    {
        var metadata = context.Parser.Parse<TMetadata>(responseContentStream);
        context.AuditScope.Span.AddTag("Metadata", metadata);

        return metadata;
    }
}
