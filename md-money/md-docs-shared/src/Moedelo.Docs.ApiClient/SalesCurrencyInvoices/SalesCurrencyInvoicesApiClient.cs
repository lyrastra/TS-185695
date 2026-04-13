using System;
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
using Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices;
using Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesCurrencyInvoices
{
    [InjectAsSingleton(typeof(ISalesCurrencyInvoicesApiClient))]
    public class SalesCurrencyInvoicesApiClient : BaseApiClient, ISalesCurrencyInvoicesApiClient
    {
        public SalesCurrencyInvoicesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesCurrencyInvoicesApiClient> logger)
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

        public async Task<IReadOnlyCollection<PaidSumDto>> GetPaidSumByBaseIdsAsync(IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<PaidSumDto>();
            }
            
            var url = $"/api/v1/Sales/GetPaidSumByIds";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<PaidSumDto[]>>(url, ids);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<SalesCurrencyInvoiceResponseDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Array.Empty<SalesCurrencyInvoiceResponseDto>();
            }

            var url = "/private/api/v1/Sales/GetByBaseIds";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<SalesCurrencyInvoiceResponseDto[]>>(url, baseIds);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<SalesCurrencyInvoiceWithItemsResponseDto>> GetByBaseIdsWithItemsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Array.Empty<SalesCurrencyInvoiceWithItemsResponseDto>();
            }

            var url = "/private/api/v1/Sales/GetByBaseIdsWithItems";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<SalesCurrencyInvoiceWithItemsResponseDto[]>>(url, baseIds);
            return response.Data;
        }
    }
}