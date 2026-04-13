using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto.Gateway.Bills;
using Moedelo.Billing.Abstractions.Gateway;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.Gateway;

[InjectAsSingleton(typeof(IBillingGatewayBillsApiClient))]
public class BillingGatewayBillsApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<BillingGatewayBillsApiClient> logger)
    :BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("GatewayBillingApiEndpoint"),
        logger), IBillingGatewayBillsApiClient
{
    public Task<BillingGatewayBillInfoDto> GetBillInfoAsync(
        string billNumber,
        HttpQuerySetting setting = null)
    {
        const string uri = "/v1/bills/byBillNumber";

        return GetAsync<BillingGatewayBillInfoDto>(uri, new { billNumber }, setting: setting);
    }
}
