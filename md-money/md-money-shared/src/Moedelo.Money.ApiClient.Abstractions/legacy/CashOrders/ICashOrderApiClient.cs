using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.CashOrders
{
    /// <summary>
    /// Клиент кассовых ордеров
    /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/accounting/Moedelo.AccountingV2.Client/CashOrder/ICashOrderApiClient.cs#L8
    /// </summary>
    public interface ICashOrderApiClient
    {
        Task<FirmCashOrderDto[]> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
    }
}