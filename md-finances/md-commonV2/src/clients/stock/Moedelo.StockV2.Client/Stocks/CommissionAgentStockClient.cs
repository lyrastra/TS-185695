using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Stocks
{
    [InjectAsSingleton]
    public class CommissionAgentStockClient : BaseApiClient, ICommissionAgentStockClient
    {
        private readonly SettingValue apiEndPoint;

        public CommissionAgentStockClient(
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

        public async Task<List<StockDto>> GetAsync(int firmId, int userId)
        {
            var response = await GetAsync<ListResponse<StockDto>>(
                "/CommissionAgentStock",
                new { firmId, userId }).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<long> CreateAsync(int firmId, int userId, string name)
        {
            var response = await PostAsync<CreateCommissionAgentStockDto, DataResponse<long>>(
                $"/CommissionAgentStock?firmId={firmId}&userId={userId}",
                new CreateCommissionAgentStockDto { Name = name })
                .ConfigureAwait(false);

            return response.Data;
        }

        public async Task<long> RenameAsync(int firmId, int userId, long stockId, string name)
        {
            var response = await PostAsync<RenameCommissionAgentStockDto, DataResponse<long>>(
                $"/CommissionAgentStock/{stockId}/Name?firmId={firmId}&userId={userId}",
                new RenameCommissionAgentStockDto { Name = name })
                .ConfigureAwait(false);

            return response.Data;
        }

        public async Task<bool> CanDeleteAsync(int firmId, int userId, long stockId)
        {
            var response = await GetAsync<DataResponse<bool>>(
                $"/CommissionAgentStock/CanDelete/{stockId}?firmId={firmId}&userId={userId}")
                .ConfigureAwait(false);

            return response.Data;
        }

        public Task DeleteAsync(int firmId, int userId, long stockId)
        {
            return PostAsync($"/CommissionAgentStock/Delete/{stockId}",
                new { firmId, userId });
        }
    }
}