using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailRefunds;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailRefunds.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SelfCostSources.RetailRefunds
{
    [InjectAsSingleton(typeof(IRetailRefundSelfCostSourcesApiClient))]
    public class RetailRefundSelfCostSourcesApiClient : BaseApiClient, IRetailRefundSelfCostSourcesApiClient
    {
        public RetailRefundSelfCostSourcesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<RetailRefundSelfCostSourcesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("DocsApiEndpoint"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<RetailRefundSelfCostDto>> GetOnDateAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto request)
        {
            var url = $"/RetailRefund/SelfCostSources/GetOnDate?firmId={firmId}&userId={userId}";
            return PostAsync<SelfCostSourceRequestDto, IReadOnlyCollection<RetailRefundSelfCostDto>>(url, request);
        }
    }
}