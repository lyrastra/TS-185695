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
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Stock.ApiClient.legacy.models;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IStockApiClient))]
    internal sealed class StockApiClient : BaseLegacyApiClient, IStockApiClient
    {
        public StockApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public async Task<StockDto> GetMainAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/FirmStock/GetMainFirmStock?firmId={firmId}&userId={userId}";
            var dataResponse = await GetAsync<TemplateResponse<StockDto>>(uri).ConfigureAwait(false);
            
            return dataResponse.Value;
        }
        
        public async Task<StockDto> GetAsync(FirmId firmId, UserId userId, long stockId)
        {
            var uri = $"/FirmStock/FindById?firmId={firmId}&userId={userId}&id={stockId}";
            var dataResponse = await GetAsync<DataResponse<StockDto>>(uri).ConfigureAwait(false);

            return dataResponse.Data;
        }

        public Task<List<StockDto>> GetAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> stockIds)
        {
            if (stockIds == null || stockIds.Count == 0)
            {
                return Task.FromResult(new List<StockDto>());
            }

            var uri = $"/FirmStock/GetByIds?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<StockDto>>(uri, stockIds.ToDistinctReadOnlyCollection());
        }

        public async Task<bool> ExistsWorkerDependenciesAsync(int firmId, int userId, int workerId, long? subcontoId)
        {
            const string uri = "/FirmStock/ExistsWorkerDependencies";
            var dataResponse =
                await GetAsync<DataResponse<bool>>(uri, new { firmId, userId, workerId, workerSubcontoId = subcontoId })
                    .ConfigureAwait(false);

            return dataResponse.Data;
        }
    }
}