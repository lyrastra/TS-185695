using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Upds;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Upds.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.RelinkSources.Upds
{
    [InjectAsSingleton(typeof(IUpdRelinkSourceApiClient))]
    public class UpdRelinkSourceApiClient : BaseApiClient, IUpdRelinkSourceApiClient
    {
        public UpdRelinkSourceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UpdRelinkSourceApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("UpdsApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<PurchaseUpdRelinkDto>> GetPurchasesAsync(RelinkSourceRequestDto request)
        {
            var url = $"/private/api/v1/RelinkSources/Purchases";
            var result = await PostAsync<RelinkSourceRequestDto, DataResponse<IReadOnlyCollection<PurchaseUpdRelinkDto>>> (url, request);
            return result.Data;
        }

        public async Task<IReadOnlyCollection<SaleUpdRelinkDto>> GetSalesAsync(RelinkSourceRequestDto request)
        {
            var url = $"/private/api/v1/RelinkSources/Sales";
            var result = await PostAsync<RelinkSourceRequestDto, DataResponse<IReadOnlyCollection<SaleUpdRelinkDto>>> (url, request);
            return result.Data;
        }
    }
}