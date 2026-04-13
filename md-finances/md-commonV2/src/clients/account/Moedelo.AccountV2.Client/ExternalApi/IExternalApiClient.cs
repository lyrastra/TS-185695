#nullable enable
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.ExternalApi;

namespace Moedelo.AccountV2.Client.ExternalApi;

public interface IExternalApiClient
{
    Task<ApiKeyAuthorizationResultDto> AuthorizeByApiKeyTokenAsync(string apiKeyToken, CancellationToken cancellationToken);

    Task<List<ApiKeyDto>> GetApiKeysByAccountAsync(int accountId);

    Task<bool> HasActiveApiKeyAsync(int accountId, CancellationToken cancellationToken);

    Task<string> ChangeApiKeyOwnerAsync(int slaveAccountId, int mainAccountId);

    Task<ApiKeyDto> CreateApiKeyOrGetExistingAsync(int accountId, int firmId, int userId);

    Task<ApiKeyDto> CreateApiKeyAsync(int accountId, int firmId, int userId);

    Task<List<ApiKeyDto>> RevokeApiKeysAsync(int accountId, IReadOnlyCollection<int> ids, int firmId, int userId);
}