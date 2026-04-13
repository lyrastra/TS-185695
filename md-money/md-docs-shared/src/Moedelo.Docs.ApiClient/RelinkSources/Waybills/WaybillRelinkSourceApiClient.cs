using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Waybills.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.RelinkSources.Waybills
{
    [InjectAsSingleton(typeof(IWaybillRelinkSourceApiClient))]
    public class WaybillRelinkSourceApiClient : BaseApiClient, IWaybillRelinkSourceApiClient
    {
        public WaybillRelinkSourceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WaybillRelinkSourceApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("WaybillsApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<PurchaseWaybillRelinkDto>> GetPurchasesAsync(RelinkSourceRequestDto request)
        {
            var url = $"/private/api/v1/RelinkSources/Purchases";
            var result = await PostAsync<RelinkSourceRequestDto, DataResponse<IReadOnlyCollection<PurchaseWaybillRelinkDto>>> (url, request);
            return result.Data;
        }

        public async Task<IReadOnlyCollection<SaleWaybillRelinkDto>> GetSalesAsync(RelinkSourceRequestDto request)
        {
            var url = $"/private/api/v1/RelinkSources/Sales";
            var result = await PostAsync<RelinkSourceRequestDto, DataResponse<IReadOnlyCollection<SaleWaybillRelinkDto>>> (url, request);
            return result.Data;
        }
    }
}