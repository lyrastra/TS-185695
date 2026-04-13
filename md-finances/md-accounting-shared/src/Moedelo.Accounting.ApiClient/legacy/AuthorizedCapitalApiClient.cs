using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IAuthorizedCapitalApiClient))]
    internal sealed class AuthorizedCapitalApiClient : BaseLegacyApiClient, IAuthorizedCapitalApiClient
    {
        public AuthorizedCapitalApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AuthorizedCapitalApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<decimal> GetShareByKontragentAsync(FirmId firmId, UserId userId, int kontragentId)
        {
            var uri = $"/AuthorizedCapital/GetShareByKontragent?firmId={firmId}&userId={userId}&kontragentId={kontragentId}";
            return GetAsync<decimal>(uri);
        }
    }
}