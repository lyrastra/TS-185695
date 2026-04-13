using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Stock.ApiClient.legacy.models;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.PriceLists;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IPriceListApiClient))]
    internal sealed class PriceListApiClient : BaseLegacyApiClient, IPriceListApiClient
    {
        public PriceListApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PriceListApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public async Task<PriceListDto> GetAsync(FirmId firmId, UserId userId, int id)
        {
            var uri = $"/PriceList/Get?firmId={firmId}&userId={userId}&id={id}";
            var dataResponse = await GetAsync<DataResponse<PriceListDto>>(uri).ConfigureAwait(false);

            return dataResponse.Data;
        }

        public async Task<List<PriceListItemDto>> GetItemsAsync(FirmId firmId, UserId userId, int id,
            IReadOnlyCollection<long> stockProductIds)
        {
            var uri = $"/PriceList/GetItems?firmId={firmId}&userId={userId}&id={id}";
            var dataResponse =
                await PostAsync<IReadOnlyCollection<long>, DataResponse<List<PriceListItemDto>>>(uri,
                    stockProductIds.ToDistinctReadOnlyCollection()).ConfigureAwait(false);

            return dataResponse.Data;
        }

        public async Task<PriceListInfoCollectionDto> GetListAsync(FirmId firmId, UserId userId, int pageNum = 1,
            int pageSize = 50, string name = null)
        {
            var uri = $"/PriceList/GetList?firmId={firmId}&userId={userId}&pageNum={pageNum}&pageSize={pageSize}&name={name}";
            var dataResponse = await GetAsync<DataResponse<PriceListInfoCollectionDto>>(uri).ConfigureAwait(false);

            return dataResponse.Data;
        }
    }
}