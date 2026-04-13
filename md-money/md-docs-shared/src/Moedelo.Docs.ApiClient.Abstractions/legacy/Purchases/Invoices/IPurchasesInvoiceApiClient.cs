using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Invoices.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Invoices
{
    public interface IPurchasesInvoiceApiClient
    {
        /// <summary>
        /// Создаёт счет-фактуру на покупку через вызов external api
        /// </summary>
        Task<PurchasesInvoiceResponseDto> SaveAsync(FirmId firmId, UserId userId, PurchasesInvoiceSaveRequestDto dto);

        /// <summary>
        /// Получаем обычные счет-фактуры с позициями
        /// </summary>
        Task<List<PurchasesInvoiceResponseDto>> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
    }
}