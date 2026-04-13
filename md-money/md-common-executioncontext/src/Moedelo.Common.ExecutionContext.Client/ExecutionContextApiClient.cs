using System.Threading.Tasks;
using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.ExecutionContext.Client;

[InjectAsSingleton(typeof(IExecutionContextApiClient))]
internal sealed class ExecutionContextApiClient : IExecutionContextApiClient
{
    private readonly IExecutionContextApiCaller apiCaller;
    private readonly IApiKeyTokenDigestCalculator digestCalculator;

    public ExecutionContextApiClient(IExecutionContextApiCaller apiCaller,
        IApiKeyTokenDigestCalculator digestCalculator)
    {
        this.apiCaller = apiCaller;
        this.digestCalculator = digestCalculator;
    }

    public Task<string> GetTokenFromPublicAsync(string token, int? companyId) =>
        GetTokenFromPublicAsync(token, companyId, CancellationToken.None);

    public Task<string> GetTokenFromPublicAsync(string token, int? companyId, CancellationToken cancellationToken)
    {
        var requestJsonBody = new
        {
            PublicToken = token,
            CompanyId = companyId
        }.ToJsonString();

        return apiCaller.PostWithRetryAsync(ExecutionContextApiMethod.FromPublic, requestJsonBody, cancellationToken);
    }

    public Task<string> GetTokenFromUserContextAsync(int firmId, int userId) =>
        GetTokenFromUserContextAsync(firmId, userId, CancellationToken.None);

    public Task<string> GetTokenFromUserContextAsync(int firmId, int userId, CancellationToken cancellationToken)
    {
        var requestJson = $"{{\"FirmId\":{firmId},\"UserId\":{userId}}}";

        return apiCaller.PostWithRetryAsync(ExecutionContextApiMethod.FromUserContext, requestJson, cancellationToken);
    }

    public Task<string> GetTokenFromApiKeyAsync(string apiKey) =>
        GetTokenFromApiKeyAsync(apiKey, CancellationToken.None);

    public Task<string> GetTokenFromApiKeyAsync(string apiKey, CancellationToken cancellationToken)
    {
        var calculation = digestCalculator.CalculateDigest(apiKey);
        var requestJsonBody = calculation.ToJsonString();

        return apiCaller.PostWithRetryAsync(ExecutionContextApiMethod.FromApiKey, requestJsonBody, cancellationToken);
    }

    public Task<string> GetUnidentifiedTokenAsync() => GetUnidentifiedTokenAsync(CancellationToken.None);

    public Task<string> GetUnidentifiedTokenAsync(CancellationToken cancellationToken)
    {
        return apiCaller.PostWithRetryAsync(ExecutionContextApiMethod.Unidentified, cancellationToken);
    }
}
