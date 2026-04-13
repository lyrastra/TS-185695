using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto;
using Moedelo.Billing.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.BackofficeBilling;

[InjectAsSingleton(typeof(IBackofficeBillingBillsApiClient))]
public class BackofficeBillingBillsApiClient : BaseApiClient, IBackofficeBillingBillsApiClient
{
    public BackofficeBillingBillsApiClient(
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

    public Task<int> InvoiceBillAsync(BillRequestDto request)
    {
        return PostAsync<BillRequestDto, int>(
            "/BackofficeBilling/V2/Bill/Invoice",
            request);
    }

    public Task<BackofficeBillingBillResponseDto> InvoiceBillAndGetInfoAsync(BillRequestDto request, HttpQuerySetting setting = null)
    {
        return PostAsync<BillRequestDto, BackofficeBillingBillResponseDto>(
            "/BackofficeBilling/V2/Bill/InvoiceAndGetInfo",
            request, setting: setting);
    }

    public Task<CostsResponseDto> CalculateCostAsync(BillRequestDto request)
    {
        return PostAsync<BillRequestDto, CostsResponseDto>(
            "/BackofficeBilling/V2/Bill/Cost",
            request);
    }

    public Task<BackofficeBillingBillResponseDto> GetBackofficeBillByPrimaryBillIdAsync(int primaryBillId)
    {
        return GetAsync<BackofficeBillingBillResponseDto>($"/BackofficeBilling/V2/Bill/{primaryBillId}");
    }

    public Task<int> InvoiceAndSwitchOnAsync(BillRequestDto request)
    {
        return PostAsync<BillRequestDto, int>(
            "/BackofficeBilling/V2/Bill/InvoiceAndSwitchOn",
            request);
    }
}