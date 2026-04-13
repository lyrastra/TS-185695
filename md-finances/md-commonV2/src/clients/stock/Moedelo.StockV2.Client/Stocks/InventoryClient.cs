using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Dto.Inventories;

namespace Moedelo.StockV2.Client.Stocks
{
    [InjectAsSingleton]
    public class InventoryClient : BaseApiClient, IInventoryClient
    {
        private readonly SettingValue apiEndPoint;

        public InventoryClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        /// <inheritdoc />
        public Task<List<InventoryDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<InventoryDto>>(
                $"/InventoryApi/GetByPeriod",
                new { firmId, userId, startDate = startDate.ToString("yyyy-MM-dd"), endDate = endDate.ToString("yyyy-MM-dd") });
        }

        /// <inheritdoc />
        public Task<List<InventoryDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any()!= true)
            {
                return Task.FromResult(new List<InventoryDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<InventoryDto>>(
                $"/InventoryApi/GetInventoryLossItemsByBaseIds?firmId={firmId}&userId={userId}",
                baseIds);
        }
    }
}
