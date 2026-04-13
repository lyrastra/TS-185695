using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices
{
    public interface IPurchasesCurrencyInvoicesApiClient
    {
        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<IReadOnlyCollection<PurchasesCurrencyInvoicePaidSumsDto>> GetPaidSumByBaseIdsAsync(IReadOnlyCollection<long> ids);
        
        /// <summary>
        /// Возвращает список инвойсов по baseIds (урезанные модели)
        /// </summary>
        Task<IReadOnlyCollection<PurchasesCurrencyInvoiceResponseDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        /// Возвращает список инвойсов по baseIds (урезанные модели) с позициями
        /// </summary>
        Task<IReadOnlyCollection<PurchasesCurrencyInvoiceResponseDto>> GetByBaseIdsWithItemsAndPaymentsAsync(IReadOnlyCollection<long> baseIds);
    }
}