using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesUpds;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesUpds.Models;
using Moedelo.Docs.ApiClient.PurchasesCurrencyInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.PurchasesUpds
{
    [InjectAsSingleton(typeof(IPurchasesUpdsApiClient))]
    public class PurchasesUpdsApiClient : BaseApiClient, IPurchasesUpdsApiClient
    {
        public PurchasesUpdsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesUpdsApiClient> logger)
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
        
        public Task<DataPageResponse<DocsPurchasesUpdByCriteriaResponseDto>> GetByCriteriaAsync(DocsPurchasesUpdsByCriteriaRequestDto criteria, int? companyId = null)

        {
            return PostAsync<DocsPurchasesUpdsByCriteriaRequestDto, DataPageResponse<DocsPurchasesUpdByCriteriaResponseDto>>(
                $"/api/v1/Purchases/GetByCriteria?_companyId={companyId}", 
                criteria);
        }

        public async Task<IReadOnlyCollection<PurchasesUpdWithItemsDto>> GetWithoutInvoices(DateTime startDate,
            DateTime endDate)
        {
            var url = "/private/api/v1/Purchases/GetWithoutInvoices";
            var response = await GetAsync<DataResponse<PurchasesUpdWithItemsDto[]>>(url, new {startDate, endDate}, null,
                new HttpQuerySetting(TimeSpan.FromSeconds(60)));
            return response.Data;
        }
    }
}