using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.ProlongationTariffication;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.AutoBilling;

[InjectAsSingleton(typeof(IProlongationTarifficationApiClient))]
public class ProlongationTarifficationApiClient : BaseApiClient, IProlongationTarifficationApiClient
{
    public ProlongationTarifficationApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeaderGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<AutoBillingInitiateApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeaderGetter,
            auditHeadersGetter,
            settingRepository.Get("AutoBillingApiEndpoint"),
            logger) { }
    
    public Task<TarifficationContextDto> GetTarifficationContextAsync(TarifficationRequestDto dto)
    {
        const string uri = "/v1/prolongation/tariffication/getTarifficationContext";

        return GetAsync<TarifficationContextDto>(uri, dto);
    }
    
    public Task<TarifficationDto> GetTarifficationAsync(TarifficationRequestDto dto)
    {
        const string uri = "/v1/prolongation/tariffication/getTariffication";

        return GetAsync<TarifficationDto>(uri, dto);
    }

    public Task<TarifficationDto> GetTarifficationResultAsync(TarifficationContextDto dto)
    {
        const string uri = "/v1/prolongation/tariffication/getTarifficationResult";

        return PostAsync<TarifficationContextDto, TarifficationDto>(uri, dto);
    }
}