using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Bills.Dto;
using Moedelo.Billing.Abstractions.Bills.Interfaces;
using Moedelo.Billing.Abstractions.Dto.Bills;
using Moedelo.Billing.Abstractions.Dto.PurchasedServices;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using HttpAbstractions = Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.ApiClient.NetFramework.Bills;

[InjectAsSingleton(typeof(IBillingBillsApiClient))]
public class BillingBillsApiClient: BaseApiClient, IBillingBillsApiClient
{
    private readonly SettingValue apiEndPoint;

    public BillingBillsApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("BillingBillsApiEndpoint");
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

    public Task<InvoicedBillDto> InvoiceBillAsync(
        InvoiceBillRequestDto request,
        HttpAbstractions.HttpQuerySetting setting = null)
    {
        const string uri = "/v1/prolongation/invoiceBill";

        HttpQuerySetting querySetting = setting == null
            ? null
            : new HttpQuerySetting { Timeout = setting.Timeout };

        return PostAsync<InvoiceBillRequestDto, InvoicedBillDto>(uri, request, setting: querySetting);
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

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }
}
