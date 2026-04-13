using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.ProlongationTariffication;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.AutoBilling;

[InjectAsSingleton(typeof(IProlongationTarifficationApiClient))]
public class ProlongationTarifficationApiClient : BaseApiClient, IProlongationTarifficationApiClient
{
    private readonly SettingValue apiEndPoint;

    public ProlongationTarifficationApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("AutoBillingApiEndpoint");
    }
    
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

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }
}