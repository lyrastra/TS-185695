using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKontragentDebtApiClient))]
    internal sealed class KontragentDebtApiClient : BaseLegacyApiClient, IKontragentDebtApiClient
    {
        public KontragentDebtApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KontragentDebtApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("KontragentsPrivateApiEndpoint"),
                logger)
        {
        }
        
        public Task<decimal> GetKontragentDebtAsync(FirmId firmId, UserId userId, int kontragentId, HttpQuerySetting setting = null)
        {
            return GetAsync<decimal>($"/KontragentDebt/GetKontragentDebt?firmId={firmId}&userId={userId}&kontragentId={kontragentId}", setting: setting);
        }
        
        public Task UpdateDebtsAsync(FirmId firmId, UserId userId, UpdateKontragentsDebtsDto request, HttpQuerySetting setting = null)
        {
            return PutAsync($"/KontragentDebt/UpdateDebts?firmId={firmId}&userId={userId}", request, setting: setting);
        }
    }
}