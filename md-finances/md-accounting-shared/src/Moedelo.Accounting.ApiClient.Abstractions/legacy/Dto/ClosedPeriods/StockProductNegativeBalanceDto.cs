using System;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.ClosedPeriods
{
    public class StockProductNegativeBalanceDto
    {
        /// <summary>
        /// Id склада, на котором обнаружен отрицательный остаток
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Название склада, на котором обнаружен отрицательный остаток 
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// Дата, на которую совершена проверка
        /// </summary>
        public DateTime OnDate { get; set; }

        /// <summary>
        /// Список отрицательных остатков
        /// </summary>
        public IReadOnlyCollection<StockProductBalanceDto> ProductBalances { get; set; }
    }
}
