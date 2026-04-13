using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ISettlementAccountExApiClient))]
    internal sealed class SettlementAccountExApiClient : BaseLegacyApiClient, ISettlementAccountExApiClient
    {
        public SettlementAccountExApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SettlementAccountExApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<SettlementAccountExDto> GetBySettlementAccountIdAsync(FirmId firmId, UserId userId,
            int settlementAccountId)
        {
            var uri =
                $"/SettlementAccountEx/BySettlementAccountId/{settlementAccountId}?firmId={firmId}&userId={userId}";
            return GetAsync<SettlementAccountExDto>(uri);
        }
    }
}