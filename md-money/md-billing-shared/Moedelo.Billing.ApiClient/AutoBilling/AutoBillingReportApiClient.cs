using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.Report;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.AutoBilling;

[InjectAsSingleton(typeof(IAutoBillingReportApiClient))]
public sealed class AutoBillingReportApiClient : BaseApiClient, IAutoBillingReportApiClient
{
    public AutoBillingReportApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeaderGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<AutoBillingReportApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeaderGetter,
            auditHeadersGetter,
            settingRepository.Get("AutoBillingApiEndpoint"),
            logger)
    {
    }

    public Task<AutoBillingReportResponseDto> GetAsync(GetAutoBillingReportRequestDto requestDto)
    {
        const string uri = "/v1/report/get";

        return PostAsync<GetAutoBillingReportRequestDto, AutoBillingReportResponseDto>(
            uri, requestDto);
    }
}