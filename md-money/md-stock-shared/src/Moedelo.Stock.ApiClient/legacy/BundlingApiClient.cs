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

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IBundlingApiClient))]
    internal sealed class BundlingApiClient : BaseLegacyApiClient, IBundlingApiClient
    {
        public BundlingApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BundlingApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<BundlingFifoSelfCostSourceDto>> GetSelfCostSourcesAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto dto)
        {
            var uri = $"/Bundling/SelfCostSources/GetOnDate?firmId={firmId}&userId={userId}";
            return PostAsync<SelfCostSourceRequestDto, IReadOnlyCollection<BundlingFifoSelfCostSourceDto>>(uri, dto);
        }
    }
}