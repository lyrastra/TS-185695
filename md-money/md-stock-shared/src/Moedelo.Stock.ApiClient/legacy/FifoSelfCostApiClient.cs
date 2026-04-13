using System;
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
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IFifoSelfCostApiClient))]
    internal sealed class FifoSelfCostApiClient : BaseLegacyApiClient, IFifoSelfCostApiClient
    {
        public FifoSelfCostApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FifoSelfCostApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public Task<FifoSelfCostProductSummariesDto> GetProductSummariesAsync(
            FirmId firmId, 
            UserId userId,
            DateTime beforeDate,
            DateTime? balancesOperationDate = null)
        {
            return GetAsync<FifoSelfCostProductSummariesDto>(
                "/FifoSelfCost/ProductSummaries", 
                new { firmId, userId, beforeDate, balancesOperationDate});
        }

        public Task<RemainsSelfCostDto> GetSelfCostRemainsAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<RemainsSelfCostDto>(
                "/FifoSelfCost/Remains",
                new { firmId, userId});
        }
    }
}