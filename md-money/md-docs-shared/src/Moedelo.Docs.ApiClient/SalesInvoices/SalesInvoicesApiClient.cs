using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.SalesInvoices
{
    [InjectAsSingleton(typeof(ISalesInvoicesApiClient))]
    public class SalesInvoicesApiClient : BaseApiClient, ISalesInvoicesApiClient 
    {
        public SalesInvoicesApiClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuthHeadersGetter authHeadersGetter, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository,
            ILogger<SalesInvoicesApiClient> logger) 
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

        public Task<HttpFileModel> DownloadDocFileAsync(SalesInvoiceReportOptionsDto request)
        {
            var url = $"/api/v1/Sales/Download/{request.DocumentBaseId}/doc?useStampAndSign={request.UseStampAndSign}&includeContractorCopy={request.IncludeContractorCopy}";
            return DownloadFileAsync(url);
        }
        
        public Task<DataPageResponse<DocsSalesInvoiceByCriteriaResponseDto>> GetByCriteriaAsync(DocsSalesInvoicesByCriteriaRequestDto criteria, int? companyId = null)
        {
            return PostAsync<DocsSalesInvoicesByCriteriaRequestDto, DataPageResponse<DocsSalesInvoiceByCriteriaResponseDto>>(
                $"/api/v1/Sales/GetByCriteria?_companyId={companyId}", 
                criteria);
        }

        public Task SaveAsync(IReadOnlyCollection<SalesInvoiceSaveRequestDto> salesInvoiceSaveRequest)
        {
            var url = $"/api/v1/Sales/Save";
            return  PostAsync(url, salesInvoiceSaveRequest);
        }
    }
}