using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesStatements;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesStatements.Models;
using Moedelo.Docs.ApiClient.PurchasesCurrencyInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.PurchasesStatements
{
    [InjectAsSingleton(typeof(IPurchasesStatementsApiClient))]
    public class PurchasesStatementsApiClient : BaseApiClient, IPurchasesStatementsApiClient
    {
        public PurchasesStatementsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesStatementsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("StatementsApiEndpoint"),
                logger)
        {
        }
        
        public Task<DataPageResponse<DocsPurchasesStatementByCriteriaResponseDto>> GetByCriteriaAsync(DocsPurchasesStatementsByCriteriaRequestDto criteria, int? companyId = null)
        {
            return PostAsync<DocsPurchasesStatementsByCriteriaRequestDto, DataPageResponse<DocsPurchasesStatementByCriteriaResponseDto>>(
                $"/api/v1/Purchases/GetByCriteria?_companyId={companyId}", 
                criteria);
        }

        public async Task<IReadOnlyCollection<PurchasesStatementsWithItemsDto>> GetWithoutInvoices(DateTime startDate,
            DateTime endDate)
        {
            var url = "/private/api/v1/Purchases/GetWithoutInvoices";
            var response = await GetAsync<DataResponse<PurchasesStatementsWithItemsDto[]>>(url, new {startDate, endDate}, null,
                new HttpQuerySetting(TimeSpan.FromSeconds(60)));
            return response.Data;
        }
    }
}