using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Balance;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// По мотивам md-commonV2/src/clients/stock/Moedelo.StockV2.Client/ClosedPeriods/StockClosingPeriodValidationApiClient.cs
    /// </summary>
    public interface IStockClosingPeriodValidationApiClient
    {
        /// <summary>
        /// Получить сведения о недостатке товаров за период
        /// </summary>
        Task<IReadOnlyList<ProductBalanceInfoDto>> GetStockProductNegativeBalancesAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate);
    }
}
