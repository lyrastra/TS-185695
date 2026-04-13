using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.LimitExcessData.Dto;
using Moedelo.Billing.Abstractions.LimitExcessData.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.LimitExcessData;

[InjectAsSingleton(typeof(ILimitExcessApiClient))]
public class LimitExcessApiClient : BaseApiClient, ILimitExcessApiClient
{
    private const string limitDataPath = "v1/limitsData";

    public LimitExcessApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeaderGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<LimitExcessApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeaderGetter,
            auditHeadersGetter,
            settingRepository.Get("BillingLimitExcessApiEndpoint"),
            logger)
    {
    }

    public Task<LimitValueResponseDto> GetLastMoneyTurnoverLimitAsync(FirmPeriodRequestDto dto)
    {
        var uri = $"{limitDataPath}/moneyTurnover/getLast";

        return PostAsync<FirmPeriodRequestDto, LimitValueResponseDto>(uri, dto);
    }

    public Task<LimitValueResponseDto> GetLastNumberEmployeesInServiceLimitAsync(FirmPeriodRequestDto dto)
    {
        var uri = $"{limitDataPath}/numberEmployeesInService/getLast";

        return PostAsync<FirmPeriodRequestDto, LimitValueResponseDto>(uri, dto);
    }
}