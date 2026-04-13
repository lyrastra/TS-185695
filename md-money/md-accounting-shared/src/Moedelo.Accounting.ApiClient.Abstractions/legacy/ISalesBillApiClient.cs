using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// https://github.com/moedelo/md-commonV2/blob/8e722e57b9bfb9563081b621b5bb88b16f59f0a7/src/clients/accounting/Moedelo.AccountingV2.Client/Bills/SalesBillApiClient.cs
    /// </summary>

    public interface ISalesBillApiClient
    {
        /// <summary>
        ///  Используется в md-payment-import (не использовать для других нужд)
        /// </summary>
        Task<SalesBillFullCollectionDto> GetForInternalAsync(FirmId firmId, UserId userId, HttpQuerySetting setting = null);

        Task<SalesBillDto> SaveAsync(FirmId firmId, UserId userId, SalesBillSaveRequestDto dto);

        /// <summary>
        /// Обновляется статус оплаты счетов
        /// </summary>
        Task UpdatePaymentStatusAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
    }
}