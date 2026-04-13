using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.ClosedPeriods
{
    public class CheckPerionValidationDto
    {
        /// <summary>
        /// Кол-во платежей, не покрытых документами
        /// </summary>
        public int UncoveredPaymentsCount { get; set; }

        /// <summary>
        /// Отрицательные остатки на складе
        /// </summary>
        public IReadOnlyCollection<StockProductNegativeBalanceDto> StockProductNegativeBalances { get; set; }
    }
}
