using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.PurchasesCurrencyInvoices
{
    [InjectAsSingleton(typeof(IPurchasesCurrencyInvoicesApiClient))]
    public class PurchasesCurrencyInvoicesApiClient : BaseApiClient, IPurchasesCurrencyInvoicesApiClient
    {
        public PurchasesCurrencyInvoicesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesCurrencyInvoicesApiClient> logger)
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

        public async Task<IReadOnlyCollection<PurchasesCurrencyInvoicePaidSumsDto>> GetPaidSumByBaseIdsAsync(IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<PurchasesCurrencyInvoicePaidSumsDto>();
            }

            var url = $"/api/v1/Purchases/GetPaidSumByIds";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<PurchasesCurrencyInvoicePaidSumsDto[]>>(url, ids);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<PurchasesCurrencyInvoiceResponseDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<PurchasesCurrencyInvoiceResponseDto>();
            }

            var url = "/private/api/v1/Purchases/GetByBaseIds";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<PurchasesCurrencyInvoiceResponseDto[]>>(url, baseIds);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<PurchasesCurrencyInvoiceResponseDto>> GetByBaseIdsWithItemsAndPaymentsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<PurchasesCurrencyInvoiceResponseDto>();
            }

            var url = "/private/api/v1/Purchases/GetByBaseIdsWithItemsAndPayments";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<PurchasesCurrencyInvoiceResponseDto[]>>(url, baseIds);
            return response.Data;
        }
    }
}