using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.System;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.AutoBilling;

[InjectAsSingleton(typeof(IAutoBillingSystemApiClient))]
public sealed class AutoBillingSystemApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeaderGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<AutoBillingSystemApiClient> logger) : BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeaderGetter,
        auditHeadersGetter,
        settingRepository.Get("AutoBillingApiEndpoint"),
        logger), IAutoBillingSystemApiClient
{
    private const string RoutePrefix = "/v1/system";

    public Task<AutoBillingSystemDto> GetAsync(AutoBillingSystemRequestDto requestDto)
    {
        return GetAsync<AutoBillingSystemDto>(RoutePrefix, requestDto);
    }

    public Task<AutoBillingSystemDto> SetAsync(SetAutoBillingSystemRequestDto requestDto)
    {
        return PostAsync<SetAutoBillingSystemRequestDto, AutoBillingSystemDto>(RoutePrefix, requestDto);
    }
}