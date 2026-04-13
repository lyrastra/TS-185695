using System;
using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce
{
    public class OutsourceProductsBlancesHistoryRequestDto
    {
        /// <summary>
        /// Фильтр по складам
        /// </summary>
        public IReadOnlyCollection<long> StockIds { get; set; }

        /// <summary>
        /// Фильтр по товарам
        /// </summary>
        public IReadOnlyCollection<long> ProductIds { get; set; }

        /// <summary>
        /// Получение данных с определенной даты
        /// </summary>
        public DateTime? FromDate { get; set; }
    }
}
