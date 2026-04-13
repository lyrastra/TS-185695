using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesInvoices
{
    public interface ISalesInvoicesApiClient
    {
        /// <summary>
        /// Скачивает файл сч-фактуры в формате doc
        /// </summary>
        /// <param name="request">параметры запроса</param>
        Task<HttpFileModel> DownloadDocFileAsync(SalesInvoiceReportOptionsDto request);
        
        Task<DataPageResponse<DocsSalesInvoiceByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsSalesInvoicesByCriteriaRequestDto criteria, 
            int? companyId = null);

        /// <summary>
        /// Продажи - Счета-фактуры - Массовое создание или обновление
        /// </summary>
        /// <param name="salesInvoiceSaveRequest"></param>
        Task SaveAsync(IReadOnlyCollection<SalesInvoiceSaveRequestDto> salesInvoiceSaveRequest);
    }
}
