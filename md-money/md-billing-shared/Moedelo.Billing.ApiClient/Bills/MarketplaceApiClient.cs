using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;
using Moedelo.Billing.Abstractions.Bills.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Bills;

[InjectAsSingleton(typeof(IMarketplaceApiClient))]
public class MarketplaceApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeaderGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<MarketplaceApiClient> logger)
    : BaseApiClient(httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeaderGetter,
        auditHeadersGetter,
        settingRepository.Get("BillingBillsApiEndpoint"),
        logger), IMarketplaceApiClient
{
    public Task<PackageProlongationInfoDto> GetProlongationInfoAsync(ProlongationRequestDto request)
    {
        const string uri = "/v1/marketplace/getProlongationInfo";

        return PostAsync<ProlongationRequestDto, PackageProlongationInfoDto>(uri, request);
    }
}