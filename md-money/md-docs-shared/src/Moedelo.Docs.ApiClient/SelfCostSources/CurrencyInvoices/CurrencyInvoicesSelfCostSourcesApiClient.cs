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
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Purchases;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Sales;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SelfCostSources.CurrencyInvoices
{
    [InjectAsSingleton(typeof(ICurrencyInvoicesSelfCostSourcesApiClient))]
    public class CurrencyInvoicesSelfCostSourcesApiClient: BaseApiClient, ICurrencyInvoicesSelfCostSourcesApiClient
    {
        public CurrencyInvoicesSelfCostSourcesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CurrencyInvoicesSelfCostSourcesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("CurrencyInvoicesApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<PurchaseCurrencyInvoicesSelfCostDto>> GetPurchasesOnDateAsync(
            SelfCostSourceRequestDto request, CancellationToken ct)
        {
            var url = $"/private/api/v1/SelfCostSources/GetOnDate/Purchases";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<PurchaseCurrencyInvoicesSelfCostDto>>>(
                url,
                request,
                cancellationToken: ct);
            return result.Data;
        }

        public async Task<IReadOnlyCollection<SaleCurrencyInvoicesSelfCostDto>> GetSalesOnDateAsync(
            SelfCostSourceRequestDto request, CancellationToken ct)
        {
            var url = $"/private/api/v1/SelfCostSources/GetOnDate/Sales";
            var result = await PostAsync<SelfCostSourceRequestDto, DataResponse<IReadOnlyCollection<SaleCurrencyInvoicesSelfCostDto>>>(
                url,
                request,
                cancellationToken: ct);
            return result.Data;
        }

        public async Task<bool> HasPurchasesOnDateAsync(DateTime? startDate, DateTime endDate)
        {
            var url = $"/private/api/v1/SelfCostSources/HasPurchasesOnDate";
            var result = await GetAsync<DataResponse<bool>>(url, new { startDate, endDate });
            return result.Data;
        }
    }
}