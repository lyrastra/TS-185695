using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesInvoices;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesInvoices.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.PurchasesInvoices
{
    [InjectAsSingleton(typeof(IPurchasesInvoicesApiClient))]
    public class PurchasesInvoicesApiClient : BaseApiClient, IPurchasesInvoicesApiClient
    {
        public PurchasesInvoicesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesInvoicesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("InvoicesApiEndpoint"),
                logger)
        {
        }

        public Task<DataPageResponse<DocsPurchasesInvoiceByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsPurchasesInvoicesByCriteriaRequestDto criteria, int? companyId = null)
        {
            return PostAsync<DocsPurchasesInvoicesByCriteriaRequestDto,
                DataPageResponse<DocsPurchasesInvoiceByCriteriaResponseDto>>(
                $"/api/v1/Purchases/GetByCriteria?_companyId={companyId}",
                criteria);
        }
    }
}