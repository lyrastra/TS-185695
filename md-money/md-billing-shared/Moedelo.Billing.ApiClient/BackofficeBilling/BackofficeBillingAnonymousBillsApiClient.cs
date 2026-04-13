using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto;
using Moedelo.Billing.Abstractions.Dto.BackofficeBillingAnonymousBills;
using Moedelo.Billing.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.BackofficeBilling;

[InjectAsSingleton(typeof(IBackofficeBillingAnonymousBillsApiClient))]
public class BackofficeBillingAnonymousBillsApiClient : BaseApiClient, IBackofficeBillingAnonymousBillsApiClient
{
    public BackofficeBillingAnonymousBillsApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<IBackofficeBillingBillsApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("InternalBillingApiEndpoint"),
            logger)
    {
    }

    public Task<CostsResponseDto> CalculateCostAsync(AnonymousCostRequestDto request)
    {
        return PostAsync<AnonymousCostRequestDto, CostsResponseDto>(
            "/BackofficeBilling/V2/Bill/Anonymous/Cost",
            request, setting: new HttpQuerySetting(TimeSpan.FromMinutes(2)));
    }
}