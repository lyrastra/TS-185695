#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.ExternalApi;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.ExternalApi;

[InjectAsSingleton(typeof(IExternalApiClient))]
internal sealed class ExternalApiClient : BaseApiClient, IExternalApiClient
{
    private readonly SettingValue apiEndPoint;
    private readonly IApiKeyTokenDigestCalculator digestCalculator;

    public ExternalApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        IApiKeyTokenDigestCalculator digestCalculator)
        : base(
            httpRequestExecutor,
            uriCreator,
            responseParser, 
            auditTracer, 
            auditScopeManager)
    {
        this.digestCalculator = digestCalculator;
        apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task<ApiKeyAuthorizationResultDto> AuthorizeByApiKeyTokenAsync(string apiKeyToken, CancellationToken cancellationToken)
    {
        var calculation = digestCalculator.CalculateDigest(apiKeyToken);
        var nonce = calculation.Nonce;
        var timestamp = calculation.Timestamp;
        var digest = calculation.Digest;
        var tokenMd5 = calculation.TokenMd5;
            
        var uri = $"/private/account/checkApiKey?tokenMd5={tokenMd5}&nonce={nonce}&timestamp={timestamp:u}&digest={digest}";

        return GetAsync<ApiKeyAuthorizationResultDto>(uri, cancellationToken: cancellationToken);
    }

    public Task<List<ApiKeyDto>> GetApiKeysByAccountAsync(int accountId)
    {
        return GetAsync<List<ApiKeyDto>>($"/Rest/Account/V2/{accountId}/apikeys");
    }

    public Task<bool> HasActiveApiKeyAsync(int accountId, CancellationToken cancellationToken)
    {
        var url = $"/Rest/Account/V2/{accountId}/hasActiveApiKey";

        return GetAsync<bool>(url, cancellationToken: cancellationToken);
    }

    public Task<string> ChangeApiKeyOwnerAsync(int slaveAccountId, int mainAccountId)
    {
        return PostAsync<ChangeApiKeyCreatedUserDto, string>("/Rest/Account/V2/apikeys/ChangeApiKeyOwner",
            new ChangeApiKeyCreatedUserDto()
            {
                NewOwner = mainAccountId,
                OldOwner = slaveAccountId
            });
    }

    public Task<ApiKeyDto> CreateApiKeyOrGetExistingAsync(int accountId, int firmId, int userId)
    {
        return PostAsync<ApiKeyDto>($"/private/account/{accountId}/apikeys/?firmId={firmId}&userId={userId}");
    }

    public Task<ApiKeyDto> CreateApiKeyAsync(int accountId, int firmId, int userId)
    {
        return PostAsync<ApiKeyDto>($"/private/account/{accountId}/apikeys?firmId={firmId}&userId={userId}");
    }

    public async Task<List<ApiKeyDto>> RevokeApiKeysAsync(int accountId, IReadOnlyCollection<int> ids, int firmId, int userId)
    {
        if (ids?.Any() == false)
        {
            return new List<ApiKeyDto>();
        }

        ids = ids.AsSet();

        var dataDto = await PostAsync<IReadOnlyCollection<int>, DataWrapper<List<ApiKeyDto>>>(
            $"/private/account/{accountId}/apikeys/Revoke?firmId={firmId}&userId={userId}",
            ids).ConfigureAwait(false);

        return dataDto.Data;
    }
}