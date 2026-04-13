using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Stocks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Stocks
{
    [InjectAsSingleton]
    public class StockClient : BaseApiClient, IStockClient
    {
        private readonly SettingValue apiEndPoint;

        public StockClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) :
            base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<StockDto> GetMainStockAsync(int firmId, int userId)
        {
            var response = await GetAsync<ValueResponse<StockDto>>(
                "/FirmStock/GetMainFirmStock",
                new { firmId, userId }).ConfigureAwait(false);

            return response.Value;
        }

        public async Task<StockDto> GetAsync(int firmId, int userId, long stockId)
        {
            var response = await GetAsync<DataResponse<StockDto>>(
                "/FirmStock/FindById",
                new { firmId, userId, id = stockId }).ConfigureAwait(false);

            return response.Data;
        }

        public Task<List<StockDto>> GetAsync(int firmId, int userId, IReadOnlyCollection<long> stockIds)
        {
            if (stockIds == null || !stockIds.Any())
            {
                return Task.FromResult(new List<StockDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<StockDto>>(
                $"/FirmStock/GetByIds?firmId={firmId}&userId={userId}",
                stockIds);
        }

        public async Task<List<StockDto>> GetAllAsync(int firmId, int userId)
        {
            var response = await GetAsync<ListResponse<StockDto>>(
                "/FirmStock/Get",
                new { firmId, userId }).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<long?> SaveAsync(int firmId, int userId, StockDto dto)
        {
            var response = await PostAsync<StockDto, SavedLongId>(
                $"/FirmStock/Save?firmId={firmId}&userId={userId}",
                dto).ConfigureAwait(false);

            return response.SavedId;
        }

        public async Task UpdateAsync(int firmId, int userId, StockUpdateDto dto)
        {
            await PostAsync(
                $"/FirmStock/Update?firmId={firmId}&userId={userId}",
                dto).ConfigureAwait(false);
        }

        public Task<long> CreateDefaultAsync(int firmId, int userId)
        {
            return PostAsync<long>(
                $"/FirmStock/CreateDefault?firmId={firmId}&userId={userId}");
        }

        public async Task<bool> ExistsWorkerDependenciesAsync(int firmId, int userId, int workerId, long? subcontoId)
        {
            var response = await GetAsync<DataResponse<bool>>(
                "/FirmStock/ExistsWorkerDependencies",
                new { firmId, userId, workerId, workerSubcontoId = subcontoId }).ConfigureAwait(false);

            return response.Data;
        }

        public async Task<bool> IsYearClosedAsync(int firmId, int userId, int year)
        {
            var response = await GetAsync<DataResponse<bool>>(
                "/FirmStock/IsYearClosed",
                new { firmId, userId, year }).ConfigureAwait(false);

            return response.Data;
        }

        public async Task<List<StockDto>> GetStocksAsync(int firmId, int userId, int count, string query)
        {
            var response = await GetAsync<ListResponse<StockDto>>(
                "/FirmStock/GetStocks",
                new { firmId, userId, offset = 0, count, query }).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<bool> HasAccessAsync(int firmId, int userId)
        {
            var response = await GetAsync<DataResponse<bool>>($"/FirmStock/HasAccess?firmId={firmId}&userId={userId}").ConfigureAwait(false);
            return response.Data;
        }
    }
}