using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Bills.Dto;
using Moedelo.Billing.Abstractions.Bills.Interfaces;
using Moedelo.Billing.Abstractions.Dto.Bills;
using Moedelo.Billing.Abstractions.Dto.PurchasedServices;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.Bills;

[InjectAsSingleton(typeof(IBillingBillsApiClient))]
public class BillingBillsApiClient : BaseApiClient, IBillingBillsApiClient
{
    public BillingBillsApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeaderGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<BillingBillsApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeaderGetter,
            auditHeadersGetter,
            settingRepository.Get("BillingBillsApiEndpoint"),
            logger)
    {
    }

    public Task CreateNewBillingTrialAsync(TrialBillRequestDto requestDto)
    {
        const string uri = "/v1/trial";

        return PostAsync(uri, requestDto);
    }

    public Task<PurchasedServicesDto> GetPurchasedServicesAsync(int firmId)
    {
        const string uri = "/v1/purchasedServices";

        return GetAsync<PurchasedServicesDto>(uri, new { firmId });
    }

    public Task<string> GetCurrentDurationAsync(int firmId, string productConfigurationCode)
    {
        const string uri = "/v1/purchasedServices/duration";

        return GetAsync<string>(uri, new { firmId, productConfigurationCode });
    }

    public Task<InvoicedBillDto> InvoiceBillAsync(InvoiceBillRequestDto request, HttpQuerySetting setting = null)
    {
        const string uri = "/v1/prolongation/invoiceBill";

        return PostAsync<InvoiceBillRequestDto, InvoicedBillDto>(uri, request, setting: setting);
    }

    public Task<RenewalCalculationResultDto> GetConfigurationCostAsync(GetConfigurationCostRequestDto request)
    {
        const string uri = "/v1/prolongation/getConfigurationCost";

        return PostAsync<GetConfigurationCostRequestDto, RenewalCalculationResultDto>(uri, request);
    }

    public Task<PackageParametersDto> GetPackageParametersAsync(GetPackageParametersRequestDto request)
    {
        const string uri = "/v1/prolongation/getPackageParameters";

        return PostAsync<GetPackageParametersRequestDto, PackageParametersDto>(uri, request);
    }
}