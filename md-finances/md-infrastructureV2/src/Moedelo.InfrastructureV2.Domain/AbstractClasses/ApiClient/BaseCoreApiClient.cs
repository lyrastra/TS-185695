using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;

public abstract class BaseCoreApiClient : BaseApiClient
{
    private readonly record struct TokenCache(DateTimeOffset Expiration, KeyValuePair<string, string> Token);

    private static readonly TimeSpan UndefinedHeaderCacheValidity = TimeSpan.FromMinutes(5);
    private static TokenCache undefinedHeaderCache = new(DateTimeOffset.MinValue, default);

    private readonly IHttpRequestExecutor httpRequestExecutor;
    private readonly SettingValue executionContextEndpointSetting;

    protected BaseCoreApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        string auditTypeName = null)
        : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager,
            auditTypeName)
    {
        this.httpRequestExecutor = httpRequestExecutor;
        executionContextEndpointSetting = settingRepository.Get("AuthContextApiEndpoint");
    }

    protected async Task<KeyValuePair<string, string>> GetPrivateTokenHeader(int firmId, int userId,
        CancellationToken cancellationToken = default)
    {
        var uri = $"{executionContextEndpointSetting.Value}/private/api/Token/FromUserContext";
        var requestDto = new
        {
            UserId = userId,
            FirmId = firmId
        };

        var token = await httpRequestExecutor
            .PostAsync(uri, requestDto, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return CreateAuthorizationHeaderFromToken(token);
    }

    protected async Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetPrivateTokenHeaders(int firmId,
        int userId,
        CancellationToken cancellationToken = default)
    {
        return new[]
        {
            await GetPrivateTokenHeader(firmId, userId, cancellationToken).ConfigureAwait(false)
        };
    }

    protected async Task<KeyValuePair<string, string>> GetUnidentifiedTokenHeader(
        CancellationToken cancellationToken)
    {
        if (undefinedHeaderCache.Expiration > DateTimeOffset.UtcNow)
        {
            return undefinedHeaderCache.Token;
        }

        var uri = $"{executionContextEndpointSetting.Value}/private/api/Token/Unidentified";
        var token = await httpRequestExecutor.PostAsync(uri, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        var header = CreateAuthorizationHeaderFromToken(token);

        undefinedHeaderCache = new TokenCache(DateTimeOffset.UtcNow.Add(UndefinedHeaderCacheValidity), header);

        return header;
    }

    protected async Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetUnidentifiedTokenHeaders(
        CancellationToken cancellationToken = default)
    {
        return new[]
        {
            await GetUnidentifiedTokenHeader(cancellationToken).ConfigureAwait(false)
        };
    }

    private static KeyValuePair<string, string> CreateAuthorizationHeaderFromToken(string token)
    {
        return new KeyValuePair<string, string>(
            AuthorizationTokenHeaderParam.AuthorizationHeader,
            $"{AuthorizationTokenHeaderParam.AuthorizationScheme} {token}");
    }
}
