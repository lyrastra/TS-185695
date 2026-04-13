using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices
{
    public interface ISalesCurrencyInvoicesApiClient
    {
        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<IReadOnlyCollection<PaidSumDto>> GetPaidSumByBaseIdsAsync(IReadOnlyCollection<long> ids);

        /// <summary>
        /// Возвращает список инвойсов по baseIds (урезанные модели)
        /// </summary>
        Task<IReadOnlyCollection<SalesCurrencyInvoiceResponseDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает список инвойсов по baseIds с позицям
        /// </summary>
        Task<IReadOnlyCollection<SalesCurrencyInvoiceWithItemsResponseDto>> GetByBaseIdsWithItemsAsync(IReadOnlyCollection<long> baseIds);
    }
}