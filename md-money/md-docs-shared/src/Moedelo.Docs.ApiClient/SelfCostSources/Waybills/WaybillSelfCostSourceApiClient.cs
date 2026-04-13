using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Purchases;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Sales;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.SelfCostSources.Waybills
{
    [InjectAsSingleton(typeof(IWaybillSelfCostSourceApiClient))]
    public class WaybillSelfCostSourceApiClient : BaseApiClient, IWaybillSelfCostSourceApiClient
    {
        public WaybillSelfCostSourceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WaybillSelfCostSourceApiClient> logger)
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

        public async Task<IReadOnlyCollection<PurchaseWaybillSelfCostDto>> GetPurchasesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct)
        {
            const string url = "/private/api/v1/SelfCostSources/GetOnDate/Purchases";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<PurchaseWaybillSelfCostDto>>>(
                url,
                request,
                setting: new HttpQuerySetting(new TimeSpan(0, 2, 0)),
                cancellationToken: ct
            );
            return result.Data;
        }

        public async Task<IReadOnlyCollection<SaleWaybillSelfCostDto>> GetSalesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct)
        {
            const string url = "/private/api/v1/SelfCostSources/GetOnDate/Sales";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<SaleWaybillSelfCostDto>>> (
                url,
                request,
                cancellationToken: ct);
            return result.Data;
        }
    }
}