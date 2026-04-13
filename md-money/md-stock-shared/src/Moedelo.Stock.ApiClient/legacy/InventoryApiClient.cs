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
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost.Inventories;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IInventoryApiClient))]
    internal sealed class InventoryApiClient : BaseLegacyApiClient, IInventoryApiClient
    {
        public InventoryApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<InventoryApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<InventoryFifoSelfCostSourceDto>> GetSelfCostSourcesAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto dto)
        {
            var uri = $"/InventoryApi/SelfCostSources/GetOnDate?firmId={firmId}&userId={userId}";
            return PostAsync<SelfCostSourceRequestDto, IReadOnlyCollection<InventoryFifoSelfCostSourceDto>>(uri, dto);
        }
    }
}