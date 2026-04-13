using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesWaybills;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesWaybills.Models;
using Moedelo.Docs.ApiClient.PurchasesCurrencyInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.PurchasesWaybills
{
    [InjectAsSingleton(typeof(IPurchasesWaybillsApiClient))]
    public class PurchasesWaybillsApiClient : BaseApiClient, IPurchasesWaybillsApiClient
    {
        public PurchasesWaybillsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesWaybillsApiClient> logger)
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
        
        public Task<DataPageResponse<DocsPurchasesWaybillByCriteriaResponseDto>> GetByCriteriaAsync(DocsPurchasesWaybillsByCriteriaRequestDto criteria, int? companyId = null)
        {
            return PostAsync<DocsPurchasesWaybillsByCriteriaRequestDto, DataPageResponse<DocsPurchasesWaybillByCriteriaResponseDto>>(
                $"/api/v1/Purchases/GetByCriteria?_companyId={companyId}", 
                criteria);
        }

        public async Task<IReadOnlyCollection<PurchasesWaybillWithItemsDto>> GetWithoutInvoices(DateTime startDate,
            DateTime endDate)
        {
            var url = "/private/api/v1/Purchases/GetWithoutInvoices";
            var response = await GetAsync<DataResponse<PurchasesWaybillWithItemsDto[]>>(url, new {startDate, endDate}, null,
                new HttpQuerySetting(TimeSpan.FromSeconds(60)));
            return response.Data;
        }
    }
}