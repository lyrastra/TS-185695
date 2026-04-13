using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Dto.LogService;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.LogService;

[InjectAsSingleton(typeof(IRequestResponseLogSaverApiClient))]
public class RequestResponseLogSaverApiClient : BaseApiClient, IRequestResponseLogSaverApiClient
{
    private const string ControllerName = "/private/api/v1/integration/requestResponse/log";

    public RequestResponseLogSaverApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<RequestResponseLogSaverApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("IntegrationApiNetCore"),
            logger)
    {
    }

    public Task<string> SaveRequestResponseLogAsync(
        SaveRequestResponseLogRequestDto request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync<SaveRequestResponseLogRequestDto, string>(
            $"{ControllerName}/SaveRequestResponseLog",
            request,
            cancellationToken: cancellationToken);
    }

}
