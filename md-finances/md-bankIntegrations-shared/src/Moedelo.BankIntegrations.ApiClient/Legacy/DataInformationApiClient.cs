using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy;
using Moedelo.BankIntegrations.ApiClient.Dto.DataInformation;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.Legacy;

[InjectAsSingleton(typeof(IDataInformationApiClient))]
public class DataInformationApiClient : BaseApiClient, IDataInformationApiClient
{
    private const string ControllerName = "DataInformation";

    public DataInformationApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<DataInformationApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("IntegrationApi"),
            logger)
    {
    }

    public Task<IntSummaryBySettlementsResponseDto> GetIntSummaryBySettlementsAsync(IntSummaryBySettlementsRequestDto dto)
    {
        return PostAsync<IntSummaryBySettlementsRequestDto, IntSummaryBySettlementsResponseDto>($"/{ControllerName}/GetIntSummaryBySettlementsAsync", dto);
    }
}