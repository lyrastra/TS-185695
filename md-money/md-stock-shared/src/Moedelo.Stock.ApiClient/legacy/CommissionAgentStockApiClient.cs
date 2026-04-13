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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ICommissionAgentStockApiClient))]
    internal sealed class CommissionAgentStockApiClient : BaseLegacyApiClient, ICommissionAgentStockApiClient
    {
        public CommissionAgentStockApiClient(
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

        public async Task<List<StockDto>> GetAsync(FirmId firmId, UserId userId)
        {
            var response = await GetAsync<ListResult<StockDto>>(
                "/CommissionAgentStock",
                new { firmId, userId }).ConfigureAwait(false);

            return response.Items;
        }

        public async Task<long> CreateAsync(FirmId firmId, UserId userId, string name)
        {
            var response = await PostAsync<CreateCommissionAgentStockDto, DataResponse<long>>(
                $"/CommissionAgentStock?firmId={firmId}&userId={userId}", new CreateCommissionAgentStockDto { Name = name })
                .ConfigureAwait(false);

            return response.Data;
        }

        public Task RenameAsync(FirmId firmId, UserId userId, long stockId, string name)
        {
            return PostAsync<RenameCommissionAgentStockDto, DataResponse<long>>(
                $"/CommissionAgentStock/{stockId}/Name?firmId={firmId}&userId={userId}", new RenameCommissionAgentStockDto { Name = name });
        }

        public async Task<bool> CanDeleteAsync(FirmId firmId, UserId userId, long stockId)
        {
            var response = await GetAsync<DataResponse<bool>>(
                $"/CommissionAgentStock/CanDelete/{stockId}?firmId={firmId}&userId={userId}")
                .ConfigureAwait(false);

            return response.Data;
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, long stockId)
        {
            return PostAsync($"/CommissionAgentStock/Delete/{stockId}?firmId={firmId}&userId={userId}");
        }
    }
}