using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.PaidConfigurations.Dto;
using Moedelo.Billing.Abstractions.PaidConfigurations.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.PaidConfigurations;

[InjectAsSingleton(typeof(IPaidConfigurationsApiClient))]
public class PaidConfigurationsApiClient : BaseApiClient, IPaidConfigurationsApiClient
{
    private const string paidConfigurationsPath = "v1/configurations/paid";

    public PaidConfigurationsApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeaderGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PaidConfigurationsApiClient> logger)
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

    public Task<PaidConfigurationResponseDto> GetLastPaidGlavUchetPaidProductConfigurationAsync(
        PaidGlavUchetConfigurationsRequestDto requestDto)
    {
        var uri = $"{paidConfigurationsPath}/getLastPaidByCriteria";

        return PostAsync<PaidGlavUchetConfigurationsRequestDto, PaidConfigurationResponseDto>(uri, requestDto);
    }

    public Task<PaidConfigurationResponseDto[]> GetPaidProductConfigurationAsync(
        PaidConfigurationsRequestDto requestDto)
    {
        var uri = $"{paidConfigurationsPath}/get";

        return PostAsync<PaidConfigurationsRequestDto, PaidConfigurationResponseDto[]>(uri, requestDto);
    }
}