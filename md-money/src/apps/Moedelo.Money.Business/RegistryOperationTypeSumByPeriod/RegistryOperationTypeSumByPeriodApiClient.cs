using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.Business.Abstractions.RegistryOperationTypeSumByPeriod;
using Moedelo.Money.Domain.Registry.OperationTypeSumByPeriod;
using Moedelo.Money.Registry.Dto.OperationTypeSumByPeriod;

namespace Moedelo.Money.Business.RegistryOperationTypeSumByPeriod;

[InjectAsSingleton(typeof(IRegistryOperationTypeSumByPeriodApiClient))]
internal sealed class RegistryOperationTypeSumByPeriodApiClient : BaseApiClient, IRegistryOperationTypeSumByPeriodApiClient
{
    private const string prefix = "/private/api/v1";

    public RegistryOperationTypeSumByPeriodApiClient(
        ISettingRepository settingRepository,
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ILogger<RegistryOperationTypeSumByPeriodApiClient> logger)
        : base(
              httpRequestExecuter,
              uriCreator,
              auditTracer,
              authHeadersGetter,
              auditHeadersGetter,
              settingRepository.Get("RegistryApiEndpoint"),
              logger)
    {
    }

    public async Task<OperationTypeSumByPeriodResponse[]> GetAsync(OperationTypeSumByPeriodRequest request, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var url = $"{prefix}/OperationTypeSumByPeriod";
        var dto = RegistryOperationTypeSumByPeriodMapper.MapToRequest(request);
        var response = await PostAsync<OperationTypeSumByPeriodRequestDto, ApiDataResponseWrapper<OperationTypeSumByPeriodResponseDto[]>>(url, dto, cancellationToken: ct);
        return response.Data.Select(RegistryOperationTypeSumByPeriodMapper.MapToResponse).ToArray();
    }
}
