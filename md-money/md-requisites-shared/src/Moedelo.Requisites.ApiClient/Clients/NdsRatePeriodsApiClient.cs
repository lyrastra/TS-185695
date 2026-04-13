using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Clients;
using Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto;

namespace Moedelo.Requisites.ApiClient.Clients;

[InjectAsSingleton(typeof(INdsRatePeriodsApiClient))]
public class NdsRatePeriodsApiClient : BaseApiClient, INdsRatePeriodsApiClient
{
    public NdsRatePeriodsApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<NdsRatePeriodsApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("RequisitesApiEndpoint"),
            logger)
    {
    }

    public async Task<NdsRatePeriodDto[]> GetAsync(GetNdsRatePeriodsFilterDto dto, CancellationToken ct = default)
    {
        var onDate = dto?.OnDate?.ToString("yyyy-MM-dd");
        var response = await GetAsync<DataResponseWrapper<NdsRatePeriodDto[]>>(
            $"/api/v1/NdsRatePeriods?OnDate={onDate}",
            cancellationToken: ct);

        return response.Data;
    }

    public async Task<NdsRatePeriodDto[]> GetAsync(int userId, int firmId, CancellationToken ct = default)
    {
        var response = await GetAsync<DataResponseWrapper<NdsRatePeriodDto[]>>(
            $"/api/v1/NdsRatePeriods?firmId={firmId}&userId={userId}",
            cancellationToken: ct);
        return response.Data;
    }
}