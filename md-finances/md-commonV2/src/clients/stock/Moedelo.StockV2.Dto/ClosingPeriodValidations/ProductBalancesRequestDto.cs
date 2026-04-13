using System;
using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.ClosingPeriodValidations
{
    /// <summary>
    /// Запрос остатков товаров на дату
    /// </summary>
    public class ProductBalancesRequestDto
    {
        /// <summary>
        /// Дата, на которую нужно рассчитать остатки
        /// </summary>
        public DateTime OnDate { get; set; }

        /// <summary>
        /// Идентификаторы товаров
        /// </summary>
        public List<long> ProductIds { get; set; }
    }
}