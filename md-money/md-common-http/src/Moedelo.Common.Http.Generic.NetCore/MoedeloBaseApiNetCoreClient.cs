using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Http.Generic.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using HttpFileModel = Moedelo.Common.Http.Generic.Abstractions.HttpFileModel;
using HttpQuerySetting = Moedelo.Common.Http.Generic.Abstractions.HttpQuerySetting;
using CoreHttpQuerySetting = Moedelo.Infrastructure.Http.Abstractions.Models.HttpQuerySetting;

// ReSharper disable ExplicitCallerInfoArgument

namespace Moedelo.Common.Http.Generic.NetCore;

internal sealed class MoedeloBaseApiNetCoreClient : BaseApiClient, IMoedeloBaseApiClient
{
    public MoedeloBaseApiNetCoreClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        SettingValue endpointSetting,
        ILogger logger,
        string? auditTypeName = null) : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            endpointSetting,
            logger,
            auditTypeName)
    {
    }

    public Task PostAsync<TRequest>(
        string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return base.PostAsync(
            uri, data, queryHeaders, MapSettings(setting), cancellationToken, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<TResponse> PostAsync<TResponse>(
        string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return base.PostAsync<TResponse>(
            uri, queryHeaders, MapSettings(setting), cancellationToken, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task PostAsync(
        string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return base.PostAsync(
            uri, queryHeaders, MapSettings(setting), cancellationToken, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<TResponse> PostAsync<TRequest, TResponse>(
        string uri,
        TRequest data,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        where TRequest : class
    {
        return base.PostAsync<TRequest, TResponse>(
            uri, data, queryHeaders, MapSettings(setting), cancellationToken, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<TResponse> GetAsync<TResponse>(
        string uri,
        object? queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return base.GetAsync<TResponse>(
            uri, queryParams, queryHeaders, MapSettings(setting), cancellationToken, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task DeleteAsync(
        string uri,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return base.DeleteAsync(uri, null, queryHeaders, MapSettings(setting), cancellationToken,
            memberName, sourceFilePath, sourceLineNumber);
    }

    public async Task<TResponse> UploadFileAsync<TRequest, TResponse>(
        string uri,
        TRequest requestBody,
        HttpFileModel file,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TRequest : class
    {
        // ожидаем, что потоком управляет вызывающая сторона 
        const bool disposeStream = false;
        using var nfwFileModel = new HttpFileStream(file.FileName, file.ContentType, file.Stream, disposeStream);

        return await base
            .SendFileAsync<TRequest, TResponse>(uri, requestBody, nfwFileModel,
                queryHeaders, MapSettings(setting), cancellationToken,
                memberName, sourceFilePath, sourceLineNumber)
            .ConfigureAwait(false);
    }

    private static readonly object EmptyObject = new {};

    public Task<HttpFileWithMetadataModel<TMetadata>> DownloadFileWithMetadataAsync<TMetadata>(
        string uri,
        object? queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        // todo: Этот метод пока не реализован для core-инфраструктуры. Один-в-один из v2 перенести не получится, надо разбираться и тестировать
        throw new NotImplementedException("Этот метод пока не реализован для core-инфраструктуры. Один-в-один из v2 перенести не получится, надо разбираться и тестировать");
    }

    public Task<HttpFileWithMetadataModel<TMetadata>> DownloadFileWithMetadataByPostMethodAsHttpFileModelAsync<TMetadata,
        TRequest>(
        string uri,
        TRequest requestBody,
        IReadOnlyCollection<KeyValuePair<string, string>>? queryHeaders = null,
        HttpQuerySetting? setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TRequest : class
    {
        // todo: Этот метод пока не реализован для core-инфраструктуры. Один-в-один из v2 перенести не получится, надо разбираться и тестировать
        throw new NotImplementedException("Этот метод пока не реализован для core-инфраструктуры. Один-в-один из v2 перенести не получится, надо разбираться и тестировать");
    }

    private static CoreHttpQuerySetting? MapSettings(HttpQuerySetting? setting)
    {
        return setting != null
            ? new CoreHttpQuerySetting(setting.Timeout)
            {
                DontThrowOn404 = setting.DontThrowOn404
            }
            : null;
    }
}
