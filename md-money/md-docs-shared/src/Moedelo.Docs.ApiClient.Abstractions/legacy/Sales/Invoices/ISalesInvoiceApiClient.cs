using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Invoices.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Invoices
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/b6526fa94c5a294ddf2e36704ef3d3c58d9c7be0/src/clients/accounting/Moedelo.AccountingV2.Client/Invoice/IInvoiceApiClient.cs#L9
    /// </summary>
    public interface ISalesInvoiceApiClient
    {
        Task<byte[]> DownloadFileAsync(
            FirmId firmId,
            UserId userId,
            long documentBaseId,
            bool useStampAndSign,
            SalesInvoiceFileFormat format);

        Task<SalesInvoiceDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long id);

        /// <summary>
        /// Создаёт счет-фактуру на продажу через вызов external api
        /// </summary>
        Task<SalesInvoiceDto> SaveAsync(FirmId firmId, UserId userId, SalesInvoiceSaveRequestDto dto);

        /// <summary>
        /// Получаем обычные счет-фактуры с позициями
        /// </summary>
        Task<List<SalesInvoiceDto>> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
    }
}