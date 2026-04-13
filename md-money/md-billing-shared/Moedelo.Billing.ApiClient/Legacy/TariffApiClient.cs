using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto.Tariffs;
using Moedelo.Billing.Abstractions.Legacy.Interfaces.Tariffs;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(ITariffApiClient))]
public class TariffApiClient : BaseLegacyApiClient, ITariffApiClient
{
    public TariffApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<TariffApiClient> logger)
        : base(httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("TariffsApiEndpoint"),
            logger)
    {
    }

    public Task<List<TariffDto>> GetAllTariffsAsync()
    {
        return GetAsync<List<TariffDto>>("/Tariffs/GetAll");
    }

    public Task<TariffDto> GetAsync(int id)
    {
        return GetAsync<TariffDto>($"/Tariffs/GetById?id={id}");
    }

    public Task<IReadOnlyCollection<TariffDto>> GetByAsync(TariffFilterDto filter, CancellationToken cancellationToken)
    {
        return PostAsync<TariffFilterDto, IReadOnlyCollection<TariffDto>>("/Tariffs/GetBy",
            filter,
            cancellationToken: cancellationToken);
    }

    public Task<List<TariffDto>> GetListAsync(IReadOnlyCollection<int> ids)
    {
        return PostAsync<IReadOnlyCollection<int>, List<TariffDto>>($"/Tariffs/GetList", ids);
    }

    public async Task<IReadOnlyDictionary<int, TariffDto>> GetByPriceListIdsAsync(IReadOnlyCollection<int> priceListIds)
    {
        if (priceListIds?.Any() != true)
        {
            return new Dictionary<int, TariffDto>();
        }

        var response = await PostAsync<IEnumerable<int>, TariffsByPriceListIdsResponseDto>(
            "/Tariffs/GetByPriceListIds",
            priceListIds).ConfigureAwait(false);

        var tariffsMap = response.Tariffs.ToDictionary(t => t.Id, t => t);

        return response.TariffByPriceList.ToDictionary(
            p => p.Key,
            p => tariffsMap[p.Value]);
    }
}