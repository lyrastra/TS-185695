using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Bills.Dto;
using Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;
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

[InjectAsSingleton(typeof(IMarketplaceApiClient))]
public class MarketplaceApiClient: BaseApiClient, IMarketplaceApiClient
{
    private readonly SettingValue apiEndPoint;

    public MarketplaceApiClient(
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

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task<PackageProlongationInfoDto> GetProlongationInfoAsync(ProlongationRequestDto request)
    {
        const string uri = "/v1/marketplace/getProlongationInfo";

        return PostAsync<ProlongationRequestDto, PackageProlongationInfoDto>(uri, request);
    }
}
