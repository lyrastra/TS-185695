using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IPriceListApiClient))]
internal sealed class PriceListApiClient : BaseLegacyApiClient, IPriceListApiClient
{
    public PriceListApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PriceListApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("PriceListsApiEndpoint"),
            logger)
    {
    }

    public Task<List<PriceListDto>> GetByTariffIdAsync(int tariffId)
    {
        return GetAsync<List<PriceListDto>>($"/PriceLists/GetByTariffId?tariffId={tariffId}");
    }

    public Task<List<PriceListDto>> GetByIdsAsync(IReadOnlyCollection<int> priceListIds)
    {
        return priceListIds.NullOrEmpty()
            ? Task.FromResult(new List<PriceListDto>())
            : PostAsync<IReadOnlyCollection<int>, List<PriceListDto>>("/PriceLists/GetByIdList",
                priceListIds.ToDistinctReadOnlyCollection());
    }

    public Task<List<PriceListDto>> GetByTariffIdsAsync(List<int> tariffIds)
    {
        return PostAsync<List<int>, List<PriceListDto>>("/PriceLists/GetByTariffIds", tariffIds);
    }
}