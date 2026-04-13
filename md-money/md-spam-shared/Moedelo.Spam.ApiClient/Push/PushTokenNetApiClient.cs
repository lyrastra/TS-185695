using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushToken;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Push;

namespace Moedelo.Spam.ApiClient.Push;

[InjectAsSingleton(typeof(IPushTokenNetApiClient))]
internal sealed class PushTokenNetApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PushTokenNetApiClient> logger)
        : BaseApiClient(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("PushNetApiEndpoint"),
            logger), IPushTokenNetApiClient
    {
    private const string ApiRoute = "/api/v1/PushToken";

    public Task SaveAsync(
        PushTokenSaveRequestDto dto,
        CancellationToken cancellationToken)
    {
        return PostAsync($"{ApiRoute}/Save", dto, cancellationToken: cancellationToken);
    }
}