using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.KudirOsno.Client.DtoWrappers;
using Moedelo.KudirOsno.Client.FixedAsset.Dto;

namespace Moedelo.KudirOsno.Client.FixedAsset
{
    public class InventoryCardExpenseClient : BaseCoreApiClient, IInventoryCardExpenseClient
    {
        private readonly ISettingRepository settingRepository;

        public InventoryCardExpenseClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("KudirOsnoApiEndpoint").Value;
        }

        public async Task<InventoryCardExpenseSummaryDto[]> GetSummaryAsync(
            int firmId, 
            int userId, 
            DateTime onDate,
            long? inventoryCardBaseId = null)
        {
            var url = $"/api/v1/InventoryCardExpense/Summary";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var queryParams = new
            {
                firmId,
                userId,
                onDate,
                inventoryCardBaseId
            };

            var response = await GetAsync<ApiDataDto<InventoryCardExpenseSummaryDto[]>>(
                url, queryParams, queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
            
            return response.data;
        }
    }
}