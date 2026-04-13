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
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKontragentSettlementAccountsApiClient))]
    internal sealed class KontragentSettlementAccountsApiClient : BaseLegacyApiClient, IKontragentSettlementAccountsApiClient
    {
        public KontragentSettlementAccountsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KontragentSettlementAccountsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("KontragentsPrivateApiEndpoint"),
                logger)
        {
        }
        
        public Task<long> SaveAsync(FirmId firmId, UserId userId, KontragentSettlementAccountDto settlementAccount)
        {
            return PostAsync<KontragentSettlementAccountDto, long>($"/SettlementAccountV2/Save?firmId={firmId}&userId={userId}", settlementAccount);
        }
        public Task<List<KontragentSettlementAccountDto>> GetByKontragentAsync(FirmId firmId, UserId userId, int kontragentId)
        {
            return GetAsync<List<KontragentSettlementAccountDto>>($"/SettlementAccountV2/GetByKontragent", new { firmId, userId, kontragentId });
        }
    }
}