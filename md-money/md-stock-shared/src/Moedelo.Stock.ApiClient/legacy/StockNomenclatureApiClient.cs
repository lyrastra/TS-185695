using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Stock.ApiClient.legacy.models;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IStockNomenclatureApiClient))]
    internal sealed class StockNomenclatureApiClient : BaseLegacyApiClient, IStockNomenclatureApiClient
    {
        public StockNomenclatureApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockOperationApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public async Task<List<StockNomenclatureDto>> GetAllAsync(FirmId firmId, UserId userId)
        {
            var response = await GetAsync<ListResult<StockNomenclatureDto>>(
                $"/StockNomenclature/Get", new { firmId, userId }).ConfigureAwait(false);

            return response.Items;
        }

        public Task CreateDefaultsAsync(FirmId firmId, UserId userId)
        {
            return PostAsync<List<StockNomenclatureDto>>($"/StockNomenclature/CreateDefaults?firmId={firmId}&userId={userId}");
        }

        public async Task<long?> SaveAsync(FirmId firmId, UserId userId, StockNomenclatureDto nomenclature)
        {
            var result = await PostAsync<StockNomenclatureDto, SavedLongId>($"/StockNomenclature/Save?firmId={firmId}&userId={userId}", nomenclature).ConfigureAwait(false);

            return result.SavedId;
        }
    }
}