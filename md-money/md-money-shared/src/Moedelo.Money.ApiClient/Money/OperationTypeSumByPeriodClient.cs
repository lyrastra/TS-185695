using System.Threading;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.Money;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto.OperationTypeSumByPeriod;

namespace Moedelo.Money.ApiClient.Money;

[InjectAsSingleton(typeof(IOperationTypeSumByPeriodClient))]
internal sealed class OperationTypeSumByPeriodClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<OperationTypeSumByPeriodClient> logger)
    : BaseApiClient(httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("MoneyApiEndpoint"),
        logger), IOperationTypeSumByPeriodClient
{
    public async Task<OperationTypeSumByPeriodResponseDto[]> GetAsync(OperationTypeSumByPeriodRequestDto dto, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        const string uri = "/private/api/v1/RegistryOperationTypeSumByPeriod"; 
        var response = await PostAsync<OperationTypeSumByPeriodRequestDto, ApiDataDto<OperationTypeSumByPeriodResponseDto[]>>(
            uri, dto, cancellationToken: ct);

        return response.data;
    }
}
