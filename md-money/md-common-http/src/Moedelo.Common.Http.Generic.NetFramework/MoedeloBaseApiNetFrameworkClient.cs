using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Http.Generic.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using HttpFileModel = Moedelo.Common.Http.Generic.Abstractions.HttpFileModel;
using HttpQuerySetting = Moedelo.Common.Http.Generic.Abstractions.HttpQuerySetting;

// ReSharper disable ExplicitCallerInfoArgument

namespace Moedelo.Common.Http.Generic.NetFramework;

internal sealed class MoedeloBaseApiNetFrameworkClient : BaseApiClient, IMoedeloBaseApiClient
{
    private readonly SettingValue apiEndpoint;

    public MoedeloBaseApiNetFrameworkClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        SettingValue apiEndpoint,
        string auditTypeName) : base(
        httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager, auditTypeName)
    {
        this.apiEndpoint = apiEndpoint;
    }
        
    public Task PostAsync<TRequest>(
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
        return base.PostAsync(
            uri, data, queryHeaders, MapSettings(setting), cancellationToken, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<TResponse> PostAsync<TResponse>(
        string uri,
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
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
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
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
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
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
        object queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
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
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
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
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
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

    public async Task<HttpFileWithMetadataModel<TMetadata>> DownloadFileWithMetadataAsync<TMetadata>(
        string uri,
        object queryParams = null,
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        queryParams ??= EmptyObject;

        var response = await base
            .DownloadFileWithMetadataByGetMethodAsync<TMetadata>(
                uri, queryParams, queryHeaders, MapSettings(setting), cancellationToken,
                memberName, sourceFilePath, sourceLineNumber)
            .ConfigureAwait(false);

        if (setting?.DontThrowOn404 == true && response == null)
        {
            return HttpFileWithMetadataModel<TMetadata>.Empty();
        }

        using var _ = response;

        return new HttpFileWithMetadataModel<TMetadata>(
            response.FileName,
            response.ContentType,
            response.ReleaseStream(),
            response.Metadata);
    }

    public async Task<HttpFileWithMetadataModel<TMetadata>> DownloadFileWithMetadataByPostMethodAsHttpFileModelAsync<TMetadata,
        TRequest>(
        string uri,
        TRequest requestBody,
        IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TRequest : class
    {
        var response = await base
            .DownloadFileWithMetadataByPostMethodAsync<TRequest, TMetadata>(
                uri, requestBody, queryHeaders, MapSettings(setting), cancellationToken,
                memberName, sourceFilePath, sourceLineNumber)
            .ConfigureAwait(false);

        if (setting?.DontThrowOn404 == true && response == null)
        {
            return HttpFileWithMetadataModel<TMetadata>.Empty();
        }

        using var _ = response;

        return new HttpFileWithMetadataModel<TMetadata>(
            response.FileName,
            response.ContentType,
            response.ReleaseStream(),
            response.Metadata);
    }

    private static InfrastructureV2.Domain.Models.ApiClient.HttpQuerySetting MapSettings(HttpQuerySetting setting)
    {
        return setting != null
            ? new InfrastructureV2.Domain.Models.ApiClient.HttpQuerySetting(setting.Timeout)
            {
                DontThrowOn404 = setting.DontThrowOn404
            }
            : null;
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }
}